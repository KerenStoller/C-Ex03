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
    
    private GarageLogic m_GarageLogic;
    public UserInterface()
    {
        m_GarageLogic = new GarageLogic();
    }
    
    public void GarageMenu()
    {
        //TODO: create a running loop
        Console.WriteLine("Welcome to the Garage Management System!");
        Console.WriteLine("Please select an option:");
        Console.WriteLine(k_MenuOptions);
        string userInput = Console.ReadLine();
        handleUserInput(userInput);
    }

    private void handleUserInput(string i_UserInput)
    {
        //TODO: validate input
        int input = int.Parse(i_UserInput);
        switch(input)
        {
            //TODO: add cases for all options and call the appropriate methods
            case 1:
                loadVehiclesFromFile();
                Console.Clear();
                Console.WriteLine("Vehicles loaded from file.");
                break;
            case 2:
                addVehicle();
                break;
            case 3:
                printAllVehiclesInGarage();
                break;
            
        }
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
        catch(ArgumentException e)
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

            if(m_GarageLogic.isElectric(licenseId))
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
        if(m_GarageLogic.isCar(i_licenseId))
        {
            addCarDetails(i_vehicleDetails);
        }
        else if(m_GarageLogic.isMotorcycle(i_licenseId))
        {
            addMotorcycleDetails(i_vehicleDetails);
        }
        else if(m_GarageLogic.isTruck(i_licenseId))
        {
            addTruckDetails(i_vehicleDetails);
        }
    }

    public void addCarDetails(List<string> i_vehicleDetails)
    {
        Console.WriteLine("Please enter the number of doors from the following: ");
        foreach(Car.eNumberOfDoors dorNum in Enum.GetValues(typeof(Car.eNumberOfDoors)))
        {
            Console.Write($"{dorNum} ");
        }
        string doorsInput = Console.ReadLine();
        i_vehicleDetails.Add(doorsInput);
        Console.WriteLine("Please enter the color from the following: ");
        foreach(Car.eColor color in Enum.GetValues(typeof(Car.eColor)))
        {
            Console.Write($"{color} ");
        }
        string colorInput = Console.ReadLine();
        i_vehicleDetails.Add(colorInput);
    }
    
    public void addMotorcycleDetails(List<string> i_vehicleDetails)
    {
        Console.WriteLine("Please enter the motorcycle license type from the following: ");
        foreach(Motorcycle.eLicenseType licenseType in Enum.GetValues(typeof(Motorcycle.eLicenseType)))
        {
            Console.Write($"{licenseType} ");
        }
        string licenseTypeInput = Console.ReadLine();
        i_vehicleDetails.Add(licenseTypeInput);
        
        Console.WriteLine("Please enter the motorcycle engine size: ");
        string engineSizeInput = Console.ReadLine();
        i_vehicleDetails.Add(engineSizeInput);
    }
    
    public void addTruckDetails(List<string> i_vehicleDetails)
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
    }
    
    private void printAllVehiclesInGarage()
    {
        List<string> vehicles;
        Vehicle.eVehicleState orderByState;
        Console.WriteLine("What state do you want to show?");
        // TODO: show options
        string userInput = Console.ReadLine();
        bool state = Vehicle.eVehicleState.TryParse(userInput, out orderByState);
        if(!state)
        {
            vehicles = m_GarageLogic.GetLicenseIdOfAllVehiclesInGarage();
        }
        else
        {
            vehicles = m_GarageLogic.GetLicenseIdOfAllVehiclesInGarage(orderByState);
        }

        foreach(string reg in vehicles)
        {
            Console.WriteLine(reg);
        }
    }
}