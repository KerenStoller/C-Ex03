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

    private enum menuOptions
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

    private GarageLogic m_GarageLogic;

    private bool m_IsRunning = true;
    public UserInterface()
    {
        m_GarageLogic = new GarageLogic();
    }

    public void GarageMenu()
    {
        //TODO: create a running loop
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
        Console.Clear();

        if (!int.TryParse(i_UserInput, out int input)) // TODO: move to input validator class and use exception handling
        {
            Console.WriteLine("Invalid input. Please enter a number corresponding to the menu options.");
            return;
        }


        menuOptions selectedOption = (menuOptions)input;

        switch (selectedOption)
        {
            case menuOptions.LoadVehiclesFromFile:
                loadVehiclesFromFile();
                break;

            case menuOptions.AddVehicleToGarage:
                addVehicle();
                break;

            case menuOptions.ShowAllVehiclesInGarage:
                PrintAllVehiclesInGarage();
                break;

            case menuOptions.ChangeVehicleState:
                changeCarState();
                break;

            case menuOptions.InflateTires:
                inflateTires();
                break;

            case menuOptions.FuelVehicle:
                fuelVehicle();
                break;

            case menuOptions.ChargeVehicle:
                chargeVehicle();
                break;

            case menuOptions.ShowVehicleDetails:
                showVehicleDetails();
                break;

            case menuOptions.Exit:
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


    // Vehicle type (string)
    // License ID (string)
    // Model name (string)
    // Energy percentage (string, float-parsable)
    // Tire model (string)
    // Current air pressure (string, float-parsable) 
    // Specific detail 1 (string)
    // Specific detail 2 (string)


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

    private void getDetailsAndCreateVehicle(string i_LicenseID)
    {
        List<string> creationDetails = new List<string>();
        string vehicleType, modelName;
        
        Console.Clear();
        Console.WriteLine("Vehicle is not in the garage. Please select vehicle type from the following options:");
        Console.WriteLine(k_VehicleOptionsMenu);
        //TODO convert to chosen option
        vehicleType = Console.ReadLine();
        vehicleType = "FuelCar";
        Console.WriteLine("Please enter vehicle model name: ");
        modelName = Console.ReadLine();
        creationDetails.Add(vehicleType);
        creationDetails.Add(i_LicenseID);
        creationDetails.Add(modelName);
        m_GarageLogic.CreateVehicle(creationDetails);
    }

    private void getVehicleDetailsAndUpdate(string licenseId)
    {
        List<string> updateVehicleDetails = new List<string>();
        string batteryPercentage, fuelPercentage, tireModel;
        
        if(m_GarageLogic.IsElectric(licenseId))
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
        setTiresState(licenseId, updateVehicleDetails);
        setVehicleDetails(licenseId, updateVehicleDetails);
        m_GarageLogic.UpdateVehicle(licenseId, updateVehicleDetails);
    }

    private void setVehicleDetails(string i_licenseId, List<string> i_vehicleDetails)
    {
        if (m_GarageLogic.IsCar(i_licenseId))
        {
            addCarDetails(i_vehicleDetails);
        }
        else if (m_GarageLogic.IsMotorcycle(i_licenseId))
        {
            addMotorcycleDetails(i_vehicleDetails);
        }
        else if (m_GarageLogic.IsTruck(i_licenseId))
        {
            addTruckDetails(i_vehicleDetails);
        }
    }

    private void addCarDetails(List<string> i_vehicleDetails)
    {
        Console.WriteLine("Please enter the number of doors from the following: ");

        foreach (Car.eNumberOfDoors dorNum in Enum.GetValues(typeof(Car.eNumberOfDoors)))
        {
            Console.Write($"{dorNum} ");
        }

        string doorsInput = Console.ReadLine();

        i_vehicleDetails.Add(doorsInput);
        Console.WriteLine("Please enter the color from the following: ");

        foreach (Car.eColor color in Enum.GetValues(typeof(Car.eColor)))
        {
            Console.Write($"{color} ");
        }

        string colorInput = Console.ReadLine();
        i_vehicleDetails.Add(colorInput);
    }

    private void addMotorcycleDetails(List<string> i_vehicleDetails)
    {
        Console.WriteLine("Please enter the motorcycle license type from the following: ");
        foreach (Motorcycle.eLicenseType licenseType in Enum.GetValues(typeof(Motorcycle.eLicenseType)))
        {
            Console.Write($"{licenseType} ");
        }
        string licenseTypeInput = Console.ReadLine();
        i_vehicleDetails.Add(licenseTypeInput);

        Console.WriteLine("Please enter the motorcycle engine size: ");
        string engineSizeInput = Console.ReadLine();
        i_vehicleDetails.Add(engineSizeInput);
    }

    private void addTruckDetails(List<string> i_vehicleDetails)
    {
        Console.WriteLine("Please enter the truck cargo capacity: ");
        string cargoCapacityInput = Console.ReadLine();
        i_vehicleDetails.Add(cargoCapacityInput);

        Console.WriteLine("Is the truck dangerous? (true/false): ");
        string isDangerousInput = Console.ReadLine();
        i_vehicleDetails.Add(isDangerousInput);
    }

    private void setTiresState(string i_licenseId, List<string> i_vehicleDetails)
    {
        //TODO: add logic to set tires state in the details string array
        //functions u can use:
        //1) public void AddSpecificTires(List<KeyValuePair<string, float>> i_TireModelNamesAndPressures)
        //      input: list of pairs: modelName and currentPressure. 
        //      throws 2 exceptions: 1) wrong number of tires 2) wrong current pressure (more than max or less than 0)
        //2) public void AddDetailsForAllTires(string i_TireModelName, float i_CurrentAirPressure)
        //      input: modelName and currentPressure. 
        //      throws only wrong current pressure
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

    private void PrintAllVehiclesInGarage()
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
            //TODO: beutify
            List<string> details = m_GarageLogic.GetDetails(licenseId);
            foreach (string detail in details)
            {
                Console.WriteLine(detail);
            }
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e.Message);
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