using Ex03.GarageLogic; 
namespace Ex03.ConsoleUI;

public class UserInterfaceLogic
{
    public static void GetUserOption()
    {
        string option = Console.ReadLine();
        bool validUserInput = false;
        while (!validUserInput)
        {
            try
            {
                throw new ArgumentException();
                // TODO:validUserInput = inputValidator.ValidateUserInput(option);
            }
            catch (ArgumentException e)
            {
                Console.Clear();
                Console.WriteLine("Invalid input. Please try again.");
                Console.WriteLine("Please select an option:");
                Console.WriteLine(UserInterface.menuOptions);
                option = Console.ReadLine();
            }
        }
    }

    public static void HandleAddVehicle(string VehicleModelName)
    {
        // TODO: check the input
        
        Vehicle createdVehicle = VehicleCreator.CreateVehicle(VehicleModelName);
        if (createdVehicle.IsElectric())
        {
            Console.WriteLine("Please enter the state of the battery");
            
        }
        else
        {
            Console.WriteLine("Please enter the state of the fuel tank");
        }
            
        int numberOfWheels = int.Parse(Console.ReadLine());
        bool allWheelsAtOnce = false;
        Console.WriteLine("Do you want to set the state of the wheels at once? press y if yes?");
        string input = Console.ReadLine().ToLower();
        
        // TODO: check for valid input
        if (input == "y")
        {
            allWheelsAtOnce = true;
        }
        
        handleStateOfWheels(allWheelsAtOnce, createdVehicle);
        
        if (createdVehicle is Car)
        {
            Console.WriteLine("please chose a color from the following list: ");
            foreach (Car.e_Color carColor in Enum.GetValues(typeof(Car.e_Color)))
            {
                Console.WriteLine(carColor);
            }
            //TODO: get color
        }

        if (createdVehicle is Truck)
        {
            // TODO: if contains dangerous materials put it
            Console.WriteLine("Does the truck contain dangerous Materials? press y if yes");
        }

        if (createdVehicle is Motorcycle)
        {
            Console.WriteLine("please chose a license from the following list: "); // TODO: add the options for colors
            foreach (Motorcycle.e_LicenseType licenseType in Enum.GetValues(typeof(Motorcycle.e_LicenseType)))
            {
                Console.WriteLine(licenseType);
            }
            // TODO: get license
        }
    }

    public static bool checkIfVeicleExists(string i_RegistrationNumber)
    {
        /// TODO: Implement the logic to check if the vehicle exists in the garage
        /// return true if it exists, false otherwise
        return false;
    }
    
    private static void handleStateOfWheels(bool allWheelsAtOnce, Vehicle vehicle)
    {
        if (allWheelsAtOnce)
        {
            Console.WriteLine("What is the state of the wheels of the vehicle?");
            // TODO: get the input and do something
        }
        else
        {
            for (int i = 0; i < vehicle.NumberOfTires(); i++)
            {
                Console.WriteLine($"What is the state of the wheel {i + 1} of the vehicle?");
                // TODO: do something with the wheel
            }
        }
    }
}