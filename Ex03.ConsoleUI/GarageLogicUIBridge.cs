
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

    public GarageLogicUIBridge(GarageLogic i_GarageLogic)
    {
        m_GarageLogic = i_GarageLogic;
    }

    public void LoadVehiclesFromFile()
    {
        try
        {
            m_GarageLogic.AddVehiclesFromDb();
            Console.WriteLine("Vehicles loaded from file.");
            Console.WriteLine("Press any key to return to menu");
            Console.ReadLine();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine("Press any key to return to menu");
            Console.ReadLine();
        }
    }


    public void ChargeVehicle()
    {
        Console.WriteLine("Please enter your vehicle registration number: ");

        string licenseId = Console.ReadLine();

        Console.WriteLine("Please enter the amount of time to charge (in minutes): ");

        string chargeTimeInput = Console.ReadLine();

        float chargeTime = InputValidator.checkValidPositiveFloat(chargeTimeInput);

        try
        {
            m_GarageLogic.ChargeBattery(licenseId, chargeTime);
        }
        catch (Exception e)
        {
            MenuManager.PrintError(e.Message);
            return;
        }

        MenuManager.PrintSuccess("Vehicle charged successfully.");
    }


    public void FuelVehicle()
    {
        Console.WriteLine("Please enter registration number: ");
        string licenseId = Console.ReadLine();

        Console.WriteLine("Please enter the type of fuel from the following: ");

        foreach (FuelSystem.eFuelType fuel in Enum.GetValues(typeof(FuelSystem.eFuelType)))
        {
            Console.Write($"{fuel} ");
        }

        Console.WriteLine();

        string fuelTypeInput = Console.ReadLine();

        Console.WriteLine("Please enter the amount of fuel to add: ");

        string fuelAmountInput = Console.ReadLine();
        
        float fuelAmount = InputValidator.checkValidPositiveFloat(fuelAmountInput);

        try
        {
            m_GarageLogic.FillTank(licenseId, fuelTypeInput, fuelAmount);
        }
        catch (Exception e)
        {
            MenuManager.PrintError(e.Message);
            return;
        }

        MenuManager.PrintSuccess("Vehicle fueled successfully.");
    }


    public void InflateTires()
    {
        Console.WriteLine("Please enter your vehicle registration number: ");
        string licenseId = Console.ReadLine();

        try
        {
            m_GarageLogic.InflateTires(licenseId);
        }
        catch (Exception e)
        {
            MenuManager.PrintError(e.Message);
        }

        MenuManager.PrintSuccess("Tires inflated successfully.");
    }

    public void ChangeVehicleState()
    {
        Console.WriteLine("Please enter your vehicle registration number: ");

        string licenseId = Console.ReadLine();

        Console.WriteLine("Please enter the new state of the vehicle from the following options: ");

        printValidStatesOfVehicle();

        Console.WriteLine();

        string newStateInput = Console.ReadLine();

        try
        {
            m_GarageLogic.ChangeVehicleState(licenseId, newStateInput);
        }
        catch (Exception e)
        {
            MenuManager.PrintError(e.Message);
        }
        MenuManager.PrintSuccess("Vehicle state changed successfully.");
    }

    private void printValidStatesOfVehicle()
    {
        foreach (Vehicle.eVehicleState state in Enum.GetValues(typeof(Vehicle.eVehicleState)))
        {
            Console.Write($"{state} ");
        }

        Console.WriteLine();
    }


    public void ShowVehicleDetails()
    {
        Console.WriteLine("Please enter your vehicle registration number: ");
        
        string licenseId = Console.ReadLine();

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
            MenuManager.PrintError(e.Message);
        }

        MenuManager.PauseAndClear();
    }


    public void addVehicle()
    {
        Console.Clear();
        Console.WriteLine("Please enter vehicle registration number: ");
        string licenseId = Console.ReadLine();

        try
        {
            m_GarageLogic.WorkOnVehicle(licenseId);
        }
        catch
        {
            try
            {
                getDetailsAndCreateVehicle(licenseId);
                getVehicleDetailsAndUpdate(licenseId);
            }
            catch (Exception e)
            {
                MenuManager.PrintError(e.Message);
            }
        }

        MenuManager.PrintSuccess("Vehicle to the garage added successfully.");
    }

    private void getDetailsAndCreateVehicle(string i_LicenseId)
    {
        List<string> creationDetails = new List<string>();
        string userInput, modelName;

        Console.Clear();
        Console.WriteLine("Vehicle is not in the garage. Please select vehicle type from the following options:");
        MenuManager.PrintVehicleOptionsMenu();
        userInput = Console.ReadLine();

        UserInterface.handleVehicleChoice(userInput, out string vehicleType);

        if (vehicleType.Length == 0)
        {
            return;
        }

        Console.WriteLine("Please enter vehicle model name: ");
        modelName = Console.ReadLine();
        creationDetails.Add(vehicleType);
        creationDetails.Add(i_LicenseId);
        creationDetails.Add(modelName);
        m_GarageLogic.CreateVehicle(creationDetails);
    }


    private void getVehicleDetailsAndUpdate(string i_LicenseId)
    {
        bool returnToMenu = false;

        List<string> updateVehicleDetails = new List<string>();
        string batteryPercentage, fuelPercentage, tireModel;

        if (m_GarageLogic.IsElectric(i_LicenseId))
        {
            Console.WriteLine("please enter the battery percentage (0-100): ");
            batteryPercentage = Console.ReadLine();
            updateVehicleDetails.Add(batteryPercentage);
        }
        else
        {
            Console.WriteLine("please enter the fuel percentage (0-100): ");
            fuelPercentage = Console.ReadLine();
            updateVehicleDetails.Add(fuelPercentage);
        }

        setTiresState(i_LicenseId, updateVehicleDetails, out bool allTiresAtOnce);

        Console.WriteLine("Please enter the owner name: ");
        string ownerName = Console.ReadLine();
        updateVehicleDetails.Add(ownerName);

        Console.WriteLine("Please enter the owner phone number: ");
        string ownerPhone = Console.ReadLine();

        try
        {
            InputValidator.checkValidPhoneNumber(ownerPhone);
            updateVehicleDetails.Add(ownerPhone);
        }
        catch(Exception e)
        {
            MenuManager.PrintError(e.Message);
            returnToMenu = true;
        }

        if (returnToMenu)
        {
            return;
        }

        setVehicleDetails(i_LicenseId, updateVehicleDetails);

        try
        {
            m_GarageLogic.UpdateVehicle(i_LicenseId, updateVehicleDetails, allTiresAtOnce);
        }
        catch (FormatException e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine("Press any key to return to menu");
            Console.ReadLine();
        }

    }

    private void setVehicleDetails(string i_LicenseId, List<string> i_VehicleDetails)
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
        Console.WriteLine("Please enter the color from the following: ");

        foreach (Car.eColor color in Enum.GetValues(typeof(Car.eColor)))
        {
            Console.Write($"{color} ");
        }

        Console.WriteLine();
        string colorInput = Console.ReadLine();
        i_VehicleDetails.Add(colorInput);

        Console.WriteLine("Please enter the number of doors from the following: ");

        foreach (Car.eNumberOfDoors dorNum in Enum.GetValues(typeof(Car.eNumberOfDoors)))
        {
            Console.Write($"{dorNum} ");
        }

        Console.WriteLine();
        string doorsInput = Console.ReadLine();
        i_VehicleDetails.Add(doorsInput);
    }

    private void addMotorcycleDetails(List<string> i_VehicleDetails)
    {
        Console.WriteLine("Please enter the motorcycle license type from the following: ");
        foreach (Motorcycle.eLicenseType licenseType in Enum.GetValues(typeof(Motorcycle.eLicenseType)))
        {
            Console.Write($"{licenseType} ");
        }
        Console.WriteLine();
        string licenseTypeInput = Console.ReadLine();
        i_VehicleDetails.Add(licenseTypeInput);

        Console.WriteLine("Please enter the motorcycle engine size: ");
        string engineSizeInput = Console.ReadLine();
        i_VehicleDetails.Add(engineSizeInput);
    }

    private void addTruckDetails(List<string> i_VehicleDetails)
    {
        Console.WriteLine("Is the truck dangerous? (true/false): ");
        string isDangerousInput = Console.ReadLine();
        i_VehicleDetails.Add(isDangerousInput);

        Console.WriteLine("Please enter the truck cargo capacity: ");
        string cargoCapacityInput = Console.ReadLine();
        i_VehicleDetails.Add(cargoCapacityInput);
    }

    private void setTiresState(string i_LicenseId, List<string> i_VehicleDetails, out bool io_allTiresAtOnce)
    {
        io_allTiresAtOnce = false;
        Console.WriteLine("Do you want to set the tire details for all the tires at once? press y, otherwise any key");
        string? userInput = Console.ReadLine();

        if (userInput?.ToLower() == "y")
        {
            io_allTiresAtOnce = true;
        }

        if (io_allTiresAtOnce)
        {
            setAllTiresAtOnce(i_LicenseId, i_VehicleDetails);
        }

        else
        {
            setTiresStateIndividually(i_LicenseId, i_VehicleDetails);
        }
    }



    private void setTiresStateIndividually(string i_LicenseId, List<string> i_VehicleDetails)
    {
        List<KeyValuePair<string, float>> tireDetails = new List<KeyValuePair<string, float>>();
        int numberOfTires = m_GarageLogic.GetNumberOfTires(i_LicenseId);
        for (int i = 0; i < numberOfTires; i++)
        {
            Console.WriteLine($"Please enter the tire model for tire {i + 1} : ");
            string i_ModelName = Console.ReadLine();

            Console.WriteLine($"Please enter the current air pressure for tire {i + 1} chose from the range 0 - {m_GarageLogic.getMaxAirPressure(i_LicenseId)}:");
            string currentAirPressureInput = Console.ReadLine();

            try
            {
                float currentAirPressure = InputValidator.checkValidPositiveFloat(currentAirPressureInput);
                tireDetails.Add(new KeyValuePair<string, float>(i_ModelName, currentAirPressure));
            }
            catch (Exception e)
            {
                MenuManager.PrintError(e.Message);
                return;
            }
        }
        foreach (KeyValuePair<string, float> modelAndTire in tireDetails)
        {
            i_VehicleDetails.Add(modelAndTire.Key);
            i_VehicleDetails.Add(modelAndTire.Value.ToString());
        }
    }

    private void setAllTiresAtOnce(string i_LicenseId, List<string> i_VehicleDetails)
    {
        Console.WriteLine("Please enter the tire model: ");
        string tireModel = Console.ReadLine();
        Console.WriteLine($"Please enter the current air pressure chose from the range 0 - {m_GarageLogic.getMaxAirPressure(i_LicenseId)} : ");

        string currentAirPressureInput = Console.ReadLine();
        float currentAirPressure;

        try
        {
            currentAirPressure = InputValidator.checkValidPositiveFloat(currentAirPressureInput);
        }
        catch (Exception e)
        {
            MenuManager.PrintError(e.Message);
            return;
        }

        i_VehicleDetails.Add(tireModel);
        i_VehicleDetails.Add(currentAirPressure.ToString());
    }
}