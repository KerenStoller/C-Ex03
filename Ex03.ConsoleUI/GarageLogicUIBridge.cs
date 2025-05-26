
//TODO:
//2) filter vehicles by state
//3) print nicer



//1) Load vehicles from file
//2) Add a new vehicle to the garage
//3) Show all vehicles in the garage
//4) Change vehicle status
//5) Inflate tires
//6) Fuel vehicle (only available for fuel vehicles)
//7) Charge vehicle (only available for electric vehicles)
//8) Show vehicle details
//9) Exit

namespace Ex03.ConsoleUI;

using Ex03.GarageLogic;
using Ex03.GarageLogic.EnergySystem;

class GarageLogicUIBridge
{
    private readonly GarageLogic m_GarageLogic;

    public GarageLogicUIBridge()
    {
        m_GarageLogic = new GarageLogic();
    }

    public void LoadVehiclesFromFile()
    {
        try
        {
            m_GarageLogic.AddVehiclesFromDb();
            Console.WriteLine("Vehicles loaded from file.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        MenuManager.PauseClearAndReturnToMenu();
    }

    public void ChargeVehicle()
    {
        string licenseId = InputHelper.GetLicenseId();
        Console.WriteLine("Please enter the amount of time to charge (in minutes): ");
        float timeToCharge = InputHelper.GetNonNegativeFloat();

        try
        {
            m_GarageLogic.ChargeBattery(licenseId, timeToCharge);
        }
        catch (Exception e)
        {
            MenuManager.PrintMessageAndGoBackToMenu(e.Message);
            return;
        }

        MenuManager.PrintMessageAndGoBackToMenu("Vehicle charged successfully.");
    }

    public void FuelVehicle()
    {
        string licenseId = InputHelper.GetLicenseId();
        FuelSystem.eFuelType fuelType = InputHelper.GetEnum<FuelSystem.eFuelType>
            ("type of fuel");
        Console.WriteLine("Please enter the amount of fuel to add: ");
        float fuelAmount = InputHelper.GetNonNegativeFloat();

        try
        {
            m_GarageLogic.FillTank(licenseId, fuelType, fuelAmount);
        }
        catch (Exception e)
        {
            MenuManager.PrintMessageAndGoBackToMenu(e.Message);
            return;
        }

        MenuManager.PrintMessageAndGoBackToMenu("Vehicle fueled successfully.");
    }
    
    public void InflateTires()
    {
        string licenseId = InputHelper.GetLicenseId();

        try
        {
            m_GarageLogic.InflateTires(licenseId);
        }
        catch (Exception e)
        {
            MenuManager.PrintMessageAndGoBackToMenu(e.Message);
        }

        MenuManager.PrintMessageAndGoBackToMenu("Tires inflated successfully.");
    }

    public void ChangeVehicleState()
    {
        string licenseId = InputHelper.GetLicenseId();
        Vehicle.eVehicleState vehicleState = InputHelper.GetEnum<Vehicle.eVehicleState>
            ("new state of the vehicle");

        try
        {
            m_GarageLogic.ChangeVehicleState(licenseId, vehicleState);
        }
        catch (Exception e)
        {
            MenuManager.PrintMessageAndGoBackToMenu(e.Message);
        }
        MenuManager.PrintMessageAndGoBackToMenu("Vehicle state changed successfully.");
    }

    public void GetLicenseIdOfAllVehiclesInGarage()
    {
        Vehicle.eVehicleState? filterState = InputHelper.GetValidVehicleStateOrNull();
        List<string> vehiclesLicense = m_GarageLogic.GetLicenseIdOfAllVehiclesInGarage(filterState);

        if (vehiclesLicense.Count == 0)
        {
            Console.WriteLine("There are no vehicles in garage for the selected state.");
        }
        else
        {
            foreach (string vehicleID in vehiclesLicense)
            {
                Console.WriteLine(vehicleID);
            }
        }
        
        MenuManager.PauseClearAndReturnToMenu();
    }
    
    public void ShowVehicleDetails()
    {
        string licenseId = InputHelper.GetLicenseId();

        try
        {
            Dictionary<string, string> vehicleDetails = m_GarageLogic.GetDetails(licenseId);
            Console.WriteLine("Vehicle details:");
            foreach (KeyValuePair<string, string> detail in vehicleDetails)
            {
                Console.WriteLine($"{detail.Key}: {detail.Value} ");
            }
            Console.WriteLine();
        }
        catch (Exception e)
        {
            MenuManager.PrintMessageAndGoBackToMenu(e.Message);
        }

        MenuManager.PauseClearAndReturnToMenu();
    }

    public void AddVehicle()
    {
        string licenseId = InputHelper.GetLicenseId();

        try
        {
            m_GarageLogic.WorkOnVehicle(licenseId);
            MenuManager.PrintMessageAndGoBackToMenu("Vehicle in garage, updated vehicle status successfully.");
        }
        catch
        {
            try
            {
                Console.WriteLine("\nCreating new vehicle:\n");
                getDetailsAndCreateVehicle(licenseId);
                getVehicleDetailsAndUpdate(licenseId);
                MenuManager.PrintMessageAndGoBackToMenu("Vehicle added to garage successfully.");
            }
            catch (Exception e)
            {
                MenuManager.PrintMessageAndGoBackToMenu(e.Message);
            }
        }
    }

    private void getDetailsAndCreateVehicle(string i_LicenseId)
    {
        List<string> creationDetails = new List<string>();
        string vehicleType = InputHelper.GetEnum<InputHelper.eVehicleTypesOptions>
            ("vehicle type").ToString();
        Console.WriteLine("Please enter vehicle model name: ");
        string modelName = Console.ReadLine();
        Console.WriteLine();
        creationDetails.Add(vehicleType);
        creationDetails.Add(i_LicenseId);
        creationDetails.Add(modelName);
        m_GarageLogic.CreateVehicle(creationDetails);
    }

    private void getVehicleDetailsAndUpdate(string i_LicenseId)
    {
        bool returnToMenu = false;
        List<string> updateVehicleDetails = new List<string>();

        if (m_GarageLogic.IsElectric(i_LicenseId))
        {
            Console.WriteLine("please enter the battery percentage (0-100): ");
            string batteryPercentage = InputHelper.GetNonNegativeFloat().ToString();
            updateVehicleDetails.Add(batteryPercentage);
            Console.WriteLine();
        }
        else
        {
            Console.WriteLine("please enter the fuel percentage (0-100): ");
            string fuelPercentage = InputHelper.GetNonNegativeFloat().ToString();
            updateVehicleDetails.Add(fuelPercentage);
            Console.WriteLine();
        }

        bool allTiresAtOnce = setTiresState(i_LicenseId, updateVehicleDetails);
        Console.WriteLine("Please enter the owner name: ");
        string ownerName = Console.ReadLine();
        updateVehicleDetails.Add(ownerName);
        Console.WriteLine();
        string ownerPhone = InputHelper.GetPhoneNumber();
        updateVehicleDetails.Add(ownerPhone);
        Console.WriteLine();
        addSpecificVehicleDetails(i_LicenseId, updateVehicleDetails);
        try
        {
            m_GarageLogic.UpdateVehicle(i_LicenseId, updateVehicleDetails, allTiresAtOnce);
        }
        catch (FormatException e)
        {
            MenuManager.PrintMessageAndGoBackToMenu(e.Message);
        }
    }

    private void addSpecificVehicleDetails(string i_LicenseId, List<string> i_VehicleDetails)
    {
        if (m_GarageLogic.IsCar(i_LicenseId))
        {
            addCarDetails(i_VehicleDetails);
        }
        else if (m_GarageLogic.IsMotorcycle(i_LicenseId))
        {
            addMotorcycleDetails(i_VehicleDetails);
        }
        else if (m_GarageLogic.IsTruck(i_LicenseId))
        {
            addTruckDetails(i_VehicleDetails);
        }
    }

    private void addCarDetails(List<string> i_VehicleDetails)
    {
        string colorInput = InputHelper.GetEnum<Car.eColor>
            ("car color").ToString();
        i_VehicleDetails.Add(colorInput);
        Console.WriteLine();
        string doorsInput = InputHelper.GetEnum<Car.eNumberOfDoors>
            ("number of doors").ToString();
        i_VehicleDetails.Add(doorsInput);
        Console.WriteLine();
    }

    private void addMotorcycleDetails(List<string> i_VehicleDetails)
    {
        string licenseType = InputHelper.GetEnum<Motorcycle.eLicenseType>
            ("license type").ToString();
        i_VehicleDetails.Add(licenseType);
        Console.WriteLine();
        Console.WriteLine("Please enter the motorcycle engine size: ");
        string engineSizeInput = InputHelper.GetNonNegativeFloat().ToString();
        i_VehicleDetails.Add(engineSizeInput);
        Console.WriteLine();
    }

    private void addTruckDetails(List<string> i_VehicleDetails)
    {
        string isDangerousInput = InputHelper.GetYesOrNo("Is the truck's load dangerous?").ToString();
        i_VehicleDetails.Add(isDangerousInput);
        Console.WriteLine();
        Console.WriteLine("Please enter the truck cargo capacity: ");
        string cargoCapacityInput = InputHelper.GetNonNegativeFloat().ToString();
        i_VehicleDetails.Add(cargoCapacityInput);
        Console.WriteLine();
    }

    private bool setTiresState(string i_LicenseId, List<string> i_VehicleDetails)
    {
        bool setTiresAtOnce = InputHelper.GetYesOrNo
            ("Do you want to set the tire details for all the tires at once? press y, otherwise any key");

        if (setTiresAtOnce)
        {
            setTireDetails(i_VehicleDetails);
        }
        else
        {
            setTiresStateIndividually(i_LicenseId, i_VehicleDetails);
        }

        return setTiresAtOnce;
    }
    
    private void setTireDetails(List<string> i_VehicleDetails)
    {
        Console.WriteLine("Please enter the tire model: ");
        string tireModel = Console.ReadLine();
        i_VehicleDetails.Add(tireModel);
        Console.WriteLine();
        Console.WriteLine("Please enter the current air pressure: ");
        string currentAirPressure = InputHelper.GetNonNegativeFloat().ToString();
        i_VehicleDetails.Add(currentAirPressure);
        Console.WriteLine();
    }

    private void setTiresStateIndividually(string i_LicenseId, List<string> i_VehicleDetails)
    {
        int numberOfTires = m_GarageLogic.GetNumberOfTires(i_LicenseId);
        
        for (int i = 0; i < numberOfTires; i++)
        {
            Console.WriteLine("Tire #1:");
            setTireDetails(i_VehicleDetails);
        }
    }
}