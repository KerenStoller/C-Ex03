
namespace Ex03.ConsoleUI;


public class MenuManager
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


    public static void PrintMenu()
    {
        Console.Clear();
        Console.WriteLine("Welcome to the Garage Management System!");
        Console.WriteLine("Please choose an option from the menu below:");
        Console.WriteLine(k_MenuOptions);
    }
    public static void PrintVehicleOptionsMenu()
    {
        Console.WriteLine(k_VehicleOptionsMenu);
    }

    public static void PrintError(string message)
    {
        Console.WriteLine(message);
        Console.WriteLine("Press any key to return to menu");
        Console.ReadLine();
        Console.Clear();
    }

    public static void PrintSuccess(string message)
    {
        Console.WriteLine(message);
        Console.WriteLine("Press any key to return to menu");
        Console.ReadLine();
        Console.Clear();
    }

    public static void PauseAndClear()
    {
        Console.WriteLine("Press any key to return to menu");
        Console.ReadKey();
        Console.Clear();
    }
}
