using Ex03.GarageLogic.EnergySystem;

namespace Ex03.ConsoleUI;

using Ex03.GarageLogic;

public class UserInterface
{
    public const string k_MenuOptions = @"1) Load vehicles from file
2) Add a new vehicle to the garage
3) Show all vehicles in the garage
4) Change vehicle status
5) Inflate tires
6) Fuel vehicle (only available for fuel vehicles)
7) Charge vehicle (only available for electric vehicles)
8) Show vehicle details
9) Exit";

    private const string k_VehicleOptionsMenu = @"1) FuelCar
2) ElectricCar
3) FuelMotorcycle
4) ElectricMotorcycle
5) Truck";

    private enum eMenuOptions
    {
        LoadVehiclesFromFile = 1,
        AddVehicleToGarage,
        ShowAllVehiclesInGarage,
        ChangeVehicleState,
        InflateTires,
        FuelVehicle,
        ChargeVehicle,
        ShowVehicleDetails,
        Exit
    }

    public enum eVehicleTypesOptions
    {
        FuelCar = 1,
        ElectricCar,
        FuelMotorcycle,
        ElectricMotorcycle,
        Truck
    }

    private GarageLogic m_GarageLogic;

    private bool m_IsRunning = true;
    public UserInterface()
    {
        m_GarageLogic = new GarageLogic();
    }

    public void GarageMenu()
    {
        while (m_IsRunning)
        {
            Console.WriteLine("Welcome to the Garage Management System!");
            Console.WriteLine("Please select an option:");
            Console.WriteLine(k_MenuOptions);
            string userInput = Console.ReadLine();
            handleUserInput(userInput);
        }
    }

    private void handleUserInput(string i_UserInput)
    {
        int input;

        try
        {
            input = InputValidator.checkValidInt(i_UserInput);
        }
        catch(FormatException e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine("Press any key to return to menu");
            Console.ReadLine();
            Console.Clear();
            return;
        }


        eMenuOptions selectedOption = (eMenuOptions)input;

        switch (selectedOption)
        {
            case eMenuOptions.LoadVehiclesFromFile:
                loadVehiclesFromFile();
                break;

            case eMenuOptions.AddVehicleToGarage:
                addVehicle();
                break;

            case eMenuOptions.ShowAllVehiclesInGarage:
                printAllVehiclesInGarage();
                break;

            case eMenuOptions.ChangeVehicleState:
                changeCarState();
                break;

            case eMenuOptions.InflateTires:
                inflateTires();
                break;

            case eMenuOptions.FuelVehicle:
                fuelVehicle();
                break;

            case eMenuOptions.ChargeVehicle:
                chargeVehicle();
                break;

            case eMenuOptions.ShowVehicleDetails:
                showVehicleDetails();
                break;

            case eMenuOptions.Exit:
                m_IsRunning = false;
                Console.Clear();
                Console.WriteLine("Exiting the program. Goodbye!");
                return;

            default:
                Console.WriteLine("Invalid menu option. Please try again, press any key to continue");
                Console.ReadLine();
                break;
        }
        Console.WriteLine("Press any key to return to menu");
        Console.ReadLine();
        Console.Clear();
    }



    private void addVehicle()
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
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }

    private void getDetailsAndCreateVehicle(string i_LicenseId)
    {
        List<string> creationDetails = new List<string>();
        string userInput, modelName;
        
        Console.Clear();
        Console.WriteLine("Vehicle is not in the garage. Please select vehicle type from the following options:");
        Console.WriteLine(k_VehicleOptionsMenu);
        //TODO convert to chosen option
        userInput = Console.ReadLine();
        
        handleVehicleChoice(userInput, out string vehicleType);

        if(vehicleType.Length == 0)
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

    private void handleVehicleChoice(string i_UserInput, out string io_vehicleType)
    {
        int input;
        io_vehicleType = "";

        try
        {
            input = InputValidator.checkValidInt(i_UserInput);
        }
        catch(FormatException e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine("Press any key to return to menu");
            Console.ReadLine();
            return;
        }

        if(Enum.IsDefined(typeof(eVehicleTypesOptions), input))
        {
            eVehicleTypesOptions selectedOption = (eVehicleTypesOptions)input;

            switch (selectedOption)
            {
                case eVehicleTypesOptions.FuelCar:
                    io_vehicleType  = "FuelCar";
                    break;

                case eVehicleTypesOptions.ElectricCar:
                    io_vehicleType = "ElectricCar";
                    break;

                case eVehicleTypesOptions.FuelMotorcycle:
                    io_vehicleType = "FuelMotorcycle";
                    break;

                case eVehicleTypesOptions.ElectricMotorcycle:
                    io_vehicleType = "ElectricMotorcycle";
                    break;

                case eVehicleTypesOptions.Truck:
                    io_vehicleType = "Truck";
                    break;
            }
        }

        else
        {
            Console.WriteLine("Invalid vehicle type selected. Please try again.");
            Console.WriteLine("Press any key to return to menu");
            Console.ReadLine();
        }

    }


    //    1.    [0]: energyPercentage(string, should be parseable as float)
    //    2.	[1]: tireModel(string)
    //    3.	[2]: currentAirPressure(string, should be parseable as float)
    //    4.    [3]: ownerName(string)
    //    5.	[4]: ownerPhone(string)
    //    6.	[5]: detail1(string, vehicle-specific)
    //    7.	[6]: detail2(string, vehicle-specific)

    private void getVehicleDetailsAndUpdate(string i_LicenseId)
    {
        bool returnToMenu = false;

        List<string> updateVehicleDetails = new List<string>();
        string batteryPercentage, fuelPercentage, tireModel;
        
        if(m_GarageLogic.IsElectric(i_LicenseId))
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

        Console.WriteLine("please enter the tire model: ");
        tireModel = Console.ReadLine();
        updateVehicleDetails.Add(tireModel);
        setTiresState(i_LicenseId, updateVehicleDetails, tireModel);

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
        catch(FormatException e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine("Press any key to return to menu");
            Console.ReadLine();
            returnToMenu = true;
        }

        catch(ArgumentException e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine("Press any key to return to menu");
            Console.ReadLine();
        }

        if(returnToMenu)
        {
            return;
        }

        setVehicleDetails(i_LicenseId, updateVehicleDetails);

        try
        {
            m_GarageLogic.UpdateVehicle(i_LicenseId, updateVehicleDetails);
        }
        catch(FormatException e)
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
        Console.WriteLine("Please enter the truck cargo capacity: ");
        string cargoCapacityInput = Console.ReadLine();
        i_VehicleDetails.Add(cargoCapacityInput);

        Console.WriteLine("Is the truck dangerous? (true/false): ");
        string isDangerousInput = Console.ReadLine();
        i_VehicleDetails.Add(isDangerousInput);
    }

    private void setTiresState(string i_LicenseId, List<string> i_VehicleDetails, string i_TireModel)
    {
        bool allTiresAtOnce = false;
        Console.WriteLine("Do you want to set the tire details for all the tires at once? press y , otherwise any key");
        string? userInput = Console.ReadLine();

        if (userInput?.ToLower() == "y")
        {
            allTiresAtOnce = true;
        }

        if(allTiresAtOnce)
        {
            setAllTiresAtOnce(i_LicenseId, i_VehicleDetails);
        }

        else
        {
            setTiresStateIndividually(i_LicenseId, i_TireModel);
        }
    }

    private void setTiresStateIndividually(string i_LicenseId, string i_ModelName)
    {
        List<KeyValuePair<string, float>> tireDetails = new List<KeyValuePair<string, float>>();
        int numberOfTires = m_GarageLogic.GetNumberOfTires(i_LicenseId);
        for(int i = 0; i < numberOfTires; i++)
        {
            Console.WriteLine($"Please enter the current air pressure for tire {i + 1}: ");
            string currentAirPressureInput = Console.ReadLine();

            try
            {
                float currentAirPressure = InputValidator.checkValidFloat(currentAirPressureInput);
                tireDetails.Add(new KeyValuePair<string, float>(i_ModelName, currentAirPressure));
            }
            catch(FormatException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Press any key to return to menu");
                Console.ReadLine();
                return;
            }
        }

        try
        {
            m_GarageLogic.AddSpecificTires(i_LicenseId,tireDetails);
        }
        catch (ValueRangeException vre)
        {
            Console.WriteLine(vre.Message);
            Console.WriteLine("Press any key to return to menu");
            Console.ReadLine();
        }
        catch (ArgumentException ae)
        {
            Console.WriteLine(ae.Message);
            Console.WriteLine("Press any key to return to menu");
            Console.ReadLine();
        }
    }

    private void setAllTiresAtOnce(string i_LicenseId, List<string> i_VehicleDetails)
    {
        Console.WriteLine("Please enter the current air pressure: ");
        string currentAirPressureInput = Console.ReadLine();
        float currentAirPressure;

        try
        {
            currentAirPressure = InputValidator.checkValidFloat(currentAirPressureInput);
        }
        catch (FormatException e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine("Press any key to return to menu");
            Console.ReadLine();
            return;
        }

        i_VehicleDetails.Add(currentAirPressure.ToString());
    }

    private void loadVehiclesFromFile()
    {
        try
        {
            m_GarageLogic.AddVehiclesFromDb();
            Console.WriteLine("Vehicles loaded from file.");
        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    private void printAllVehiclesInGarage()
    {
        Console.WriteLine("What state do you want to show? chose from the following: press any other key if all ");

        //TODO:
        //PrintAllVehiclesInGarage();

        string? userInput = Console.ReadLine();

        List<string> vehicles = m_GarageLogic.GetLicenseIdOfAllVehiclesInGarage(userInput);

        if(vehicles.Count == 0)
        {
            Console.WriteLine("There are no vehicles in garage.");    
        }
        
        foreach (string reg in vehicles)
        {
            Console.WriteLine(reg);
        }
    }

    private void changeCarState()
    {
        Console.WriteLine("Please Enter your vehicle registration number: ");

        string licenseId = Console.ReadLine();

        Console.WriteLine("Please enter the new state from the following: ");
        printValidStatesOfVehicle();


        string stateInputFromUser = Console.ReadLine();

        try
        {
            m_GarageLogic.ChangeVehicleState(licenseId, stateInputFromUser);
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine("Press any key to return to menu");
            Console.ReadLine();
        }
    }

    private void inflateTires()
    {
        Console.WriteLine("Please enter your vehicle registration number: ");
        string licenseId = Console.ReadLine();
        try
        {
            m_GarageLogic.InflateTires(licenseId);
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e.Message);
        }
    }

    private void fuelVehicle()
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
        //TODO check positive number
        float fuelAmount = float.Parse(fuelAmountInput);

        try
        {
            m_GarageLogic.FillTank(licenseId, fuelTypeInput, fuelAmount);
        }
        catch (FormatException e)
        {
            Console.WriteLine(e.Message);
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e.Message);
        }
    }

    private void chargeVehicle()
    {
        Console.WriteLine("Please enter your vehicle registration number: ");

        string licenseId = Console.ReadLine();
        Console.WriteLine("Please enter the amount of time to charge (in minutes): ");

        string chargeTimeInput = Console.ReadLine();
        //TODO check positive number
        float chargeTime = float.Parse(chargeTimeInput);

        try
        {
            m_GarageLogic.ChargeBattery(licenseId, chargeTime);
        }
        catch (FormatException e)
        {
            Console.WriteLine(e.Message);
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e.Message);
        }
    }

    private void showVehicleDetails()
    {
        Console.WriteLine("Please enter your vehicle registration number: ");
        string licenseId = Console.ReadLine();
        try
        {
            List<string> details = m_GarageLogic.GetDetails(licenseId);
            Console.WriteLine("=========================================");
            Console.WriteLine("         Vehicle Details Summary         ");
            Console.WriteLine("=========================================");
            foreach (string detail in details)
            {
                // Try to split on ':' for key-value formatting
                int separatorIndex = detail.IndexOf(':');
                if (separatorIndex > 0 && separatorIndex < detail.Length - 1)
                {
                    string key = detail.Substring(0, separatorIndex).Trim();
                    string value = detail.Substring(separatorIndex + 1).Trim();
                    Console.WriteLine($"{key,-25}: {value}");
                }
                else
                {
                    Console.WriteLine(detail);
                }
            }
            Console.WriteLine("=========================================");
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine("Press any key to return to menu");
            Console.ReadLine();
        }
    }

    private void printValidStatesOfVehicle()
    {
        foreach (Vehicle.eVehicleState state in Enum.GetValues(typeof(Vehicle.eVehicleState)))
        {
            Console.Write($"{state} ");
        }

        Console.WriteLine();
    }
}