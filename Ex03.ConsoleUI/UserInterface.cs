namespace Ex03.ConsoleUI;

public class UserInterface
{
    private const string menuOptions = @"1) Load vehicles from file
2) Add a new vehicle to the garage
3) Show all vehicles in the garage
4) Change vehicle status
5) Inflate tires
6) Refuel vehicle (only available for fuel vehicles)
7) Charge vehicle (only available for electric vehicles)
8) Show vehicle details
9) Exit";
    
    public void PresentUserOptions()
    {
        Console.WriteLine("Welcome to the Garage Management System!");
        Console.WriteLine("Please select an option:");
        Console.WriteLine(menuOptions);
        GetUserOption();
    }

    public void GetUserOption()
    {
        string option = Console.ReadLine();
        bool validUserInput = false;
        while (!validUserInput)
        {
            try
            {
                throw new ArgumentException();
                //validUserInput = inputValidator.ValidateUserInput(option);
            }
            catch (ArgumentException e)
            {
                Console.Clear();
                Console.WriteLine("Invalid input. Please try again.");
                Console.WriteLine("Please select an option:");
                Console.WriteLine(menuOptions);
                option = Console.ReadLine();
            }
        }
    }

    
}