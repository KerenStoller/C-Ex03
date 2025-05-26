namespace Ex03.ConsoleUI;

public static class InputHelper
{
    public static readonly List<string> k_MenuOptions = new List<string> 
        { "Load vehicles from file", 
            "Add a new vehicle to the garage",
            "Show all vehicles in the garage",
            "Change vehicle status",
            "Inflate tires",
            "Fuel vehicle (only available for fuel vehicles)",
            "Charge vehicle (only available for electric vehicles)",
            "Show vehicle details",
            "Exit"
        };

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
    
    private static void printErrorAndTryAgain(string message)
    {
        Console.WriteLine(message);
        Console.WriteLine("Press any key to try again");
        Console.ReadLine();
        Console.Clear();
    }
    
    public static string GetEnumInString(string enumType, List<string> options, bool i_AllowNone = false) 
    {
        string returnString = "";
        while (true)
        {
            Console.WriteLine($"Please choose the {enumType} from the following:");
            for (int i = 0; i < options.Count; i++)
            {
                Console.WriteLine($"{i + 1}) {options[i]}");
            }
        
            Console.WriteLine();
            string? input = Console.ReadLine();
            bool foundOption = false;
            
            if (int.TryParse(input, out int index) && index >= 1 && index <= options.Count)
            {
                Console.Clear();
                returnString = options[index - 1];
            }
            else
            {
                foreach (string option in options)
                {
                    if (input.ToLower() == option.ToLower())
                    {
                        returnString = option;
                        foundOption = true;
                        break;
                    }
                }
                if (i_AllowNone && !foundOption)
                {
                    Console.WriteLine("Invalid choice. Showing all instead.");
                    returnString = "";
                }
            }
            
            if (!i_AllowNone && returnString == "")
            {
                printErrorAndTryAgain("Invalid input.");
            }
            else
            {
                return returnString;
            }
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
                printErrorAndTryAgain(e.Message);
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

    public static string GetEnumInStringOrNone(string enumType, List<string> options , bool i_AllowNone = false)
    {
        string returnString = "";
        while (true)
        {
            Console.WriteLine($"Please choose the {enumType} from the following:");
            for (int i = 0; i < options.Count; i++)
            {
                Console.WriteLine($"{i + 1}) {options[i]}");
            }
        
            Console.WriteLine();
            string? input = Console.ReadLine();
            bool foundOption = false;
            
            if (int.TryParse(input, out int index) && index >= 1 && index <= options.Count)
            {
                Console.Clear();
                returnString = options[index - 1];
            }
            else
            {
                foreach (string option in options)
                {
                    if (input.ToLower() == option.ToLower())
                    {
                        returnString = option;
                        foundOption = true;
                        break;
                    }
                }
                if (i_AllowNone && !foundOption)
                {
                    Console.WriteLine("Invalid input, showing all instead.");
                    returnString = "";
                }
            }

            
            if (!i_AllowNone && returnString == "")
            {
                printErrorAndTryAgain("Invalid input, please try again.");
            }
            else
            {
                return returnString;
            }
        }
    }
}