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
            Console.Clear();
            Console.WriteLine("Vehicle is not in the garage. Please select vehicle type from the following options:");
            Console.WriteLine(k_VehicleOptionsMenu);
            
            string vehicleTypeInput = Console.ReadLine();
            // TODO: validate input
            string modelName = Console.ReadLine();
            Vehicle vehicle = VehicleCreator.CreateVehicle(vehicleTypeInput, licenseId, modelName);
            
            if(vehicle.IsElectric())
            {
                Console.WriteLine("Please enter battery percentage (0-100): ");
                string batteryPercentage = Console.ReadLine();
                //TODO: validate input
                float batteryPercentageFloat = float.Parse(batteryPercentage);
                m_GarageLogic.ChargeBattery(licenseId, batteryPercentageFloat); // TODO: function that set the battery percentage 
            }
            else
            {
                Console.WriteLine("Please enter the amount of fuel in the tank in percentage (0 - 100): ");
                string fuelPercentage = Console.ReadLine();
                //TODO: validate input
            }
            
            Console.WriteLine("Please enter tire model name: ");
            string tireModelName = Console.ReadLine();
            vehicle.SetTireModelName(tireModelName); // TODO: function that set the tire model name
            
            setTiresState(vehicle);

            if(vehicle is Car)
            {
                handleCarSpecifics(vehicle);
            }
            
            else if(vehicle is Motorcycle)
            {
                handleMotorcycleSpecifics(vehicle);
            }
            
            else if(vehicle is Truck)
            {
                handleTruckSpecifics(vehicle);
            }
            
            
            //TODO: add owner details
            //TODO: add the vehicle to the garage
        }
    }

    private void handleCarSpecifics(Vehicle i_Vehicle)
    {
        Car car = i_Vehicle as Car;
        Console.WriteLine("what is the color of the car? please select one of the following options: ");
        foreach(Car.eColor color in Enum.GetValues(typeof(Car.eColor)))
        {
            Console.Write($"{color.ToString()} ");
        }
        string colorInput = Console.ReadLine();
        // TODO: validate input
        Console.WriteLine("how many doors does the car have? please select one of the following options: ");
        foreach (Car.eNumberOfDoors door in Enum.GetValues(typeof(Car.eNumberOfDoors)))
        {
            Console.Write($"{door.ToString()} ");
        }
        string doorInput = Console.ReadLine();
        // TODO: validate input
        car.AddSpecificDetails(colorInput, doorInput);
        // TODO: try catch it throws exception
    }

    private void handleMotorcycleSpecifics(Vehicle i_Vehicle)
    {
        Motorcycle motorcycle = i_Vehicle as Motorcycle;
        Console.WriteLine("what is the license type of the motorcycle? please select one of the following options: ");
        foreach (Motorcycle.eLicenseType licenseType in Enum.GetValues(typeof(Motorcycle.eLicenseType)))
        {
            Console.Write($"{licenseType.ToString()} ");
        }
        string licenseTypeInput = Console.ReadLine();
        Console.WriteLine("what is the engine capacity of the motorcycle? ");
        string engineCapacityInput = Console.ReadLine();
        motorcycle.AddSpecificDetails(licenseTypeInput, engineCapacityInput);
    }

    private void handleTruckSpecifics(Vehicle i_Vehicle)
    {
        Truck truck = i_Vehicle as Truck;
        Console.WriteLine("Does the truck contain dangerous materials? (true/false)");
        string containsDangerousMaterialsInput = Console.ReadLine();
        //TODO handle exception
        Console.WriteLine("What is the cargo volume of the truck?");
        string cargoVolumeInput = Console.ReadLine();
        //TODO handle exception
        truck.AddSpecificDetails(containsDangerousMaterialsInput, cargoVolumeInput);
    }

    private void setTiresState(Vehicle i_Vehicle)
    {
        Console.WriteLine("Do you want to inflate all tires at once? press y if yes");
        string userInput = Console.ReadLine();
        if (userInput == "y")
        {
            Console.WriteLine("Please enter the air pressure in percentage (0-100):");
            string airPressure = Console.ReadLine();
            //TODO: validate input
            float percentage = float.Parse(airPressure);
            i_Vehicle.InflateTires(); // TODO: function that inflates in percentage
        }
        else
        {
            for(int i = 0; i < i_Vehicle.NumberOfTires(); i++)
            {
                Console.WriteLine($"Enter air pressure in percentage (0-100) for tire {i + 1}: ");
                string airPressure = Console.ReadLine();
                //TODO: validate input
                float percentage = float.Parse(airPressure);
                //TODO: function that inflates in percentage individual tires
            }
        }
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