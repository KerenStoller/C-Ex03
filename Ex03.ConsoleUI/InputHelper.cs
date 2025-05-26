using Ex03.GarageLogic.EnergySystem;

namespace Ex03.ConsoleUI;
using Ex03.GarageLogic;

public static class InputHelper
{
    public enum eMenuOptions
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

    public static string GetLicenseId()
    {
        Console.WriteLine("Please enter registration number: ");
        return Console.ReadLine();
    }
    
    public static float GetNonNegativeFloat()
    {
        float result = 0f;
        bool isValid = false;

        while (!isValid)
        {
            string? userInput = Console.ReadLine();

            if (float.TryParse(userInput, out result))
            {
                if (result >= 0)
                {
                    isValid = true;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a non negative float number:");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a non negative float number:");
            }
        }

        return result;
    }
    
    public static T GetEnum<T>(string enumType) where T : struct
    {
        while (true)
        {
            Console.WriteLine($"Please choose the {enumType} from the following:");
            int enumNumberValue = 1;
            foreach (var val in Enum.GetValues(typeof(T)))
            {
                Console.Write($"{enumNumberValue}) {val}");
                Console.WriteLine();
                enumNumberValue++;
            }
        
            Console.WriteLine();
            string? input = Console.ReadLine();
        
            if (Enum.TryParse<T>(input, true, out var result) && Enum.IsDefined(typeof(T), result))
            {
                Console.Clear();
                return result;
            }

            MenuManager.PrintErrorAndTryAgain("Invalid input, please try again.");
        }
    }

    public static string GetPhoneNumber()
    {
        while (true)
        {
            Console.WriteLine("Please enter the phone number: ");
            string phoneNumber = Console.ReadLine();

            try
            {
                CheckValidPhoneNumber(phoneNumber);
                return phoneNumber;   
            }
            catch (Exception e)
            {
                MenuManager.PrintErrorAndTryAgain(e.Message);
            }
        }
    }
    
    public static void CheckValidPhoneNumber(string inputToCheck)
    {
        if(inputToCheck.Length != 10)
        {
            throw new ArgumentException("Phone number must be 10 digits");
        }

        if (!int.TryParse(inputToCheck, out int number))
        {
            throw new FormatException("Phone number must contain only digits");
        }
    }
    
    public static bool GetYesOrNo(string i_Question)
    {
        Console.WriteLine(i_Question);
        Console.WriteLine("Please enter yes or no:");

        while (true)
        {
            string? input = Console.ReadLine()?.Trim().ToLower();

            if (input == "yes" || input == "y")
                return true;

            if (input == "no" || input == "n")
                return false;

            Console.WriteLine("Invalid input. Please type 'yes' or 'no'.");
        }
    }
    
    public static Vehicle.eVehicleState? GetValidVehicleStateOrNull()
    {
        Vehicle.eVehicleState? returnValue = null;
        
        Console.WriteLine("Select vehicle state to filter by:");
        foreach (string state in Enum.GetNames(typeof(Vehicle.eVehicleState)))
        {
            Console.WriteLine($"- {state}");
        }
        
        Console.WriteLine("Or press Enter to show all vehicles.");
        string? userInput = Console.ReadLine();
        
        if (Enum.TryParse<Vehicle.eVehicleState>(userInput, ignoreCase: true, out var vehicleState) &&
            Enum.IsDefined(typeof(Vehicle.eVehicleState), vehicleState))
        {
            returnValue = vehicleState;
        }
        else
        {
            Console.WriteLine("Invalid state. Showing all vehicles instead.");
        }

        return returnValue;
    }
}