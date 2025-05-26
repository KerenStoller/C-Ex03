namespace Ex03.ConsoleUI;
using GarageLogic;
using EnumOptions = Ex03.GarageLogic.GarageLogic.eEnumOptions;

public class UserInterface
{
    private bool m_UserDidntPressExit = true;
    private readonly GarageLogic m_GarageLogic;

    public UserInterface()
    {
        m_GarageLogic = new GarageLogic();
    }

    public void GarageMenu()
    {
        while (m_UserDidntPressExit)
        {
            Console.WriteLine("Welcome to the Garage Management System!");
            handleUserInput();
        }
    }

    private void handleUserInput()
    {
        string menuOption = InputHelper.GetEnumInString("menu option", InputHelper.k_MenuOptions);
        
        Console.Clear();

        if (menuOption == InputHelper.k_MenuOptions[0])
        {
            LoadVehiclesFromFile();
        }
        else if (menuOption == InputHelper.k_MenuOptions[1])
        {
            AddVehicle();
        }
        else if (menuOption == InputHelper.k_MenuOptions[2])
        {
            GetLicenseIdOfAllVehiclesInGarage();
        }
        else if (menuOption == InputHelper.k_MenuOptions[3])
        {
            ChangeVehicleState();
        }
        else if (menuOption == InputHelper.k_MenuOptions[4])
        {
            InflateTires();
        }
        else if (menuOption == InputHelper.k_MenuOptions[5])
        {
            FuelVehicle();
        }
        else if (menuOption == InputHelper.k_MenuOptions[6])
        {
            ChargeVehicle();
        }
        else if (menuOption == InputHelper.k_MenuOptions[7])
        {
            ShowVehicleDetails();
        }
        else if (menuOption == InputHelper.k_MenuOptions[8])
        {
            m_UserDidntPressExit = false;
            
            Console.Clear();
            Console.WriteLine("Exiting the program. Goodbye!");
            return;
        }
        Console.Clear();
    }
    
    private static void printMessageAndGoBackToMenu(string i_Message)
    {
        Console.WriteLine(i_Message);
        Console.WriteLine("Press any key to return to menu.");
        Console.ReadLine();
        Console.Clear();
    }
    
    private static void pauseClearAndReturnToMenu()
    {
        Console.WriteLine("Press any key to return to menu");
        Console.ReadKey();
        Console.Clear();
    }
    
    private void LoadVehiclesFromFile()
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
        pauseClearAndReturnToMenu();
    }
    
    private void ChargeVehicle()
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
            printMessageAndGoBackToMenu(e.Message);
            return;
        }

        printMessageAndGoBackToMenu("Vehicle charged successfully.");
    }
    
    private void FuelVehicle()
    {
        string licenseId = InputHelper.GetLicenseId();
        string fuelType = InputHelper.GetEnumInString("type of fuel", 
            GarageLogic.GetEnumOptions(EnumOptions.FuelType));
        
        Console.WriteLine("Please enter the amount of fuel to add: ");
        float fuelAmount = InputHelper.GetNonNegativeFloat();

        try
        {
            m_GarageLogic.FillTank(licenseId, fuelType, fuelAmount);
        }
        catch (Exception e)
        {
            printMessageAndGoBackToMenu(e.Message);
            return;
        }

        printMessageAndGoBackToMenu("Vehicle fueled successfully.");
    }
    
    private void InflateTires()
    {
        string licenseId = InputHelper.GetLicenseId();

        try
        {
            m_GarageLogic.InflateTires(licenseId);
        }
        catch (Exception e)
        {
            printMessageAndGoBackToMenu(e.Message);
        }

        printMessageAndGoBackToMenu("Tires inflated successfully.");
    }
    
    private void ChangeVehicleState()
    {
        string licenseId = InputHelper.GetLicenseId();
        string vehicleState = InputHelper.GetEnumInString("new state of the vehicle", GarageLogic.GetEnumOptions
            (EnumOptions.VehicleState));

        try
        {
            m_GarageLogic.ChangeVehicleState(licenseId, vehicleState);
        }
        catch (Exception e)
        {
            printMessageAndGoBackToMenu(e.Message);
        }
        printMessageAndGoBackToMenu("Vehicle state changed successfully.");
    }
    
    private void GetLicenseIdOfAllVehiclesInGarage()
    {
        string filterState = InputHelper.GetEnumInString("state to filter by or enter 'None'", 
            GarageLogic.GetEnumOptions(EnumOptions.VehicleState), true);
        List<string> vehiclesLicense = m_GarageLogic.GetLicenseIdOfAllVehiclesInGarage(filterState);

        if (vehiclesLicense.Count == 0)
        {
            Console.WriteLine("There are no vehicles in garage for the selected state.");
        }
        else
        {
            foreach (string vehicleId in vehiclesLicense)
            {
                Console.WriteLine(vehicleId);
            }
        }
        
        pauseClearAndReturnToMenu();
    }
    
    private void ShowVehicleDetails()
    {
        string licenseId = InputHelper.GetLicenseId();
        Dictionary<string, string> vehicleDetails =  new Dictionary<string, string>();

        try
        {
            vehicleDetails = m_GarageLogic.GetDetails(licenseId);
            Console.Clear();
            Console.WriteLine("Vehicle details: \n");
            foreach (KeyValuePair<string, string> detail in vehicleDetails)
            {
                Console.WriteLine($"{detail.Key}: {detail.Value} ");
            }
        
            pauseClearAndReturnToMenu();
        }
        catch (Exception e)
        {
            printMessageAndGoBackToMenu(e.Message);
        }
    }
    
    private void addEnumValueToDetails(string i_Description, EnumOptions i_OptionsType, List<string> i_DetailsList)
    {
        string value = InputHelper.GetEnumInString(i_Description, GarageLogic.GetEnumOptions(i_OptionsType));
        
        i_DetailsList.Add(value);
        Console.WriteLine();
    }

    private void addStringToDetails(string i_Description, List<string> i_DetailsList)
    {
        Console.WriteLine("Please enter the " + i_Description + ": ");
        string value = Console.ReadLine();
        
        i_DetailsList.Add(value);
        Console.WriteLine();
    }

    private void addNonNegativeFloatToDetails(string i_Description, List<string> i_DetailsList)
    {
        Console.WriteLine("Please enter " + i_Description + ": ");
        string value = InputHelper.GetNonNegativeFloat().ToString();
        
        i_DetailsList.Add(value);
        Console.WriteLine();
    }

    private void addBoolToDetails(string i_Question, List<string> i_DetailsList)
    {
        string isDangerousInput = InputHelper.GetYesOrNo(i_Question).ToString();
        
        i_DetailsList.Add(isDangerousInput);
        Console.WriteLine();
    }

    private void addPhoneNumberToDetails(List<string> i_DetailsList)
    {
        string ownerPhone = InputHelper.GetPhoneNumber();
        
        i_DetailsList.Add(ownerPhone);
        Console.WriteLine();
    }
    
    private void getDetailsAndCreateVehicle(string i_LicenseId)
    {
        List<string> creationDetails = new List<string>();
        
        addEnumValueToDetails("vehicle type", EnumOptions.SupportedTypes, creationDetails);
        creationDetails.Add(i_LicenseId);
        addStringToDetails("vehicle model name", creationDetails);
        m_GarageLogic.CreateVehicle(creationDetails);
    }

    private void getVehicleDetailsAndUpdate(string i_LicenseId)
    {
        bool returnToMenu = false;
        List<string> updateVehicleDetails = new List<string>();
        
        if (m_GarageLogic.IsElectric(i_LicenseId))
        {
            addStringToDetails("battery percentage (0-100)",  updateVehicleDetails);
        }
        else
        {
            addStringToDetails("fuel percentage (0-100)", updateVehicleDetails);
        }

        bool allTiresAtOnce = setTiresState(i_LicenseId, updateVehicleDetails);
        
        addStringToDetails("owner name", updateVehicleDetails);
        addPhoneNumberToDetails(updateVehicleDetails);
        addSpecificVehicleDetails(i_LicenseId, updateVehicleDetails);
        try
        {
            m_GarageLogic.UpdateVehicle(i_LicenseId, updateVehicleDetails, allTiresAtOnce);
        }
        catch (FormatException e)
        {
            printMessageAndGoBackToMenu(e.Message);
        }
    }
    
    private void AddVehicle()
    {
        string licenseId = InputHelper.GetLicenseId();

        try
        {
            m_GarageLogic.WorkOnVehicle(licenseId);
            printMessageAndGoBackToMenu("Vehicle in garage, updated vehicle status successfully.");
        }
        catch
        {
            try
            {
                Console.WriteLine("\nCreating new vehicle:\n");
                getDetailsAndCreateVehicle(licenseId);
                getVehicleDetailsAndUpdate(licenseId);
                printMessageAndGoBackToMenu("Vehicle added to garage successfully.");
            }
            catch (Exception e)
            {
                printMessageAndGoBackToMenu(e.Message);
            }
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
        addEnumValueToDetails("car color", EnumOptions.CarColor, i_VehicleDetails);
        addEnumValueToDetails("number of doors", EnumOptions.CarDoors,  i_VehicleDetails);
    }

    private void addMotorcycleDetails(List<string> i_VehicleDetails)
    {
        addEnumValueToDetails("license type", EnumOptions.MotorcycleLicense,  i_VehicleDetails);
        addStringToDetails("motor type", i_VehicleDetails);
        addNonNegativeFloatToDetails("the motorcycle engine size",  i_VehicleDetails);
    }

    private void addTruckDetails(List<string> i_VehicleDetails)
    {
        addBoolToDetails("Is the truck's load dangerous?", i_VehicleDetails);
        addNonNegativeFloatToDetails("the truck cargo capacity", i_VehicleDetails);
    }
    
    private bool setTiresState(string i_LicenseId, List<string> i_VehicleDetails)
    {
        bool setTiresAtOnce = InputHelper.GetYesOrNo
            ("Do you want to set the tire details for all the tires at once?");

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
        addStringToDetails("tire model",  i_VehicleDetails);
        addNonNegativeFloatToDetails("the current air pressure", i_VehicleDetails);
    }

    private void setTiresStateIndividually(string i_LicenseId, List<string> i_VehicleDetails)
    {
        int numberOfTires = m_GarageLogic.GetNumberOfTires(i_LicenseId);
        
        for (int i = 0; i < numberOfTires; i++)
        {
            Console.WriteLine($"Tire #{i + 1}:");
            setTireDetails(i_VehicleDetails);
        }
    }
}