using Ex03.GarageLogic;

namespace Ex03.ConsoleUI;

public class UserInterface
{
    public const string menuOptions = @"1) Load vehicles from file
2) Add a new vehicle to the garage
3) Show all vehicles in the garage
4) Change vehicle status
5) Inflate tires
6) Refuel vehicle (only available for fuel vehicles)
7) Charge vehicle (only available for electric vehicles)
8) Show vehicle details
9) Exit";

    private const string vehicleOptionsMenu = @"1) FuelCar
2) ElectricCar
3) FuelMotorcycle
4) ElectricMotorcycle
5) Truck";
    
    public void PresentUserOptions()
    {
        Console.WriteLine("Welcome to the Garage Management System!");
        Console.WriteLine("Please select an option:");
        Console.WriteLine(menuOptions);
        UserInterfaceLogic.GetUserOption();
    }
    
    
    private void handleAddANewVehicle()
    {
        Console.Clear();
        // TODO: TODO: Implement the logic to add a new vehicle with garageLogic
        Console.WriteLine("Enter registration number: ");
        string registrationNumber = Console.ReadLine();
        if (UserInterfaceLogic.checkIfVeicleExists(registrationNumber))
        {
            // TODO:Change vehicle status to in repair
            Console.WriteLine("Vehicle already exists in the garage.");
        }
        else
        {
            Console.WriteLine("Chose A vehicle from the following list: ");
            Console.WriteLine(vehicleOptionsMenu);
            string option = Console.ReadLine();
            UserInterfaceLogic.HandleAddVehicle(option);
        }
    }

    
}