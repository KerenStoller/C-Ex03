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
6) Refuel vehicle (only available for fuel vehicles)
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
        RefuelVehicle,
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
                Console.Clear();
                Console.WriteLine("Vehicles loaded from file.");
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

            case menuOptions.RefuelVehicle:
                refuelVehicle();
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
        catch (ArgumentException e)
        {
            List<string> vehicleDetails = new List<string>();
            List<string> initialDetails = new List<string>();

            Console.Clear();
            Console.WriteLine("Vehicle is not in the garage. Please select vehicle type from the following options:");
            Console.WriteLine(k_VehicleOptionsMenu);

            string vehicleTypeInput = Console.ReadLine();

            initialDetails.Add(vehicleTypeInput);
            initialDetails.Add(licenseId);
            Console.WriteLine("Please enter vehicle model name: ");

            string modelName = Console.ReadLine();

            initialDetails.Add(modelName);
            m_GarageLogic.initialVehicleCreation(initialDetails);

            if (m_GarageLogic.isElectric(licenseId))
            {
                Console.WriteLine("please enter the battery percentage (0-100): ");
                string batteryPercentage = Console.ReadLine();
                vehicleDetails.Add(batteryPercentage);
            }
            else
            {
                Console.WriteLine("please enter the fuel percentage (0-100): ");
                string fuelPercentage = Console.ReadLine();
                vehicleDetails.Add(fuelPercentage);
            }

            Console.WriteLine("please enter the tire model: ");
            string tireModel = Console.ReadLine();
            vehicleDetails.Add(tireModel);

            setTiresState(licenseId, vehicleDetails);

            setVehicleDetails(licenseId, vehicleDetails);

            m_GarageLogic.AddVehicleFromDetails(vehicleDetails);

        }

    }

    private void setVehicleDetails(string i_licenseId, List<string> i_vehicleDetails)
    {
        if (m_GarageLogic.isCar(i_licenseId))
        {
            addCarDetails(i_vehicleDetails);
        }
        else if (m_GarageLogic.isMotorcycle(i_licenseId))
        {
            addMotorcycleDetails(i_vehicleDetails);
        }
        else if (m_GarageLogic.isTruck(i_licenseId))
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
    }

    private void loadVehiclesFromFile()
    {
        m_GarageLogic.AddVehiclesFromDb();
        Console.WriteLine("Vehicles loaded from file.");
        Console.WriteLine("Press any key to return to menu");
        Console.ReadLine();
    }

    private void PrintAllVehiclesInGarage()
    {
        Console.WriteLine("What state do you want to show? chose from the following: press any other key if all ");

        PrintAllVehiclesInGarage();

        string? userInput = Console.ReadLine();

        List<string> vehicles = m_GarageLogic.GetLicenseIdOfAllVehiclesInGarage(userInput);

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

    private void refuelVehicle()
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

        try
        {
            m_GarageLogic.FillTank(licenseId, fuelTypeInput, fuelAmountInput);
        }
        catch (FormatException e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine("Press any key to return to menu");
            Console.ReadLine();
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine("Press any key to return to menu");
            Console.ReadLine();
        }
    }

    private void chargeVehicle()
    {
        Console.WriteLine("Please enter your vehicle registration number: ");

        string licenseId = Console.ReadLine();
        Console.WriteLine("Please enter the amount of time to charge (in minutes): ");

        string chargeTimeInput = Console.ReadLine();

        try
        {
            m_GarageLogic.ChargeBattery(licenseId, chargeTimeInput);
        }
        catch (FormatException e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine("Press any key to return to menu");
            Console.ReadLine();
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine("Press any key to return to menu");
            Console.ReadLine();
        }
    }

    private void showVehicleDetails()
    {
        Console.WriteLine("Please enter your vehicle registration number: ");
        string licenseId = Console.ReadLine();
        try
        {
            m_GarageLogic.GetDetails(licenseId);
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