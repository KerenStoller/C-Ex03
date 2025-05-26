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
    private const int k_LengthOfPhoneNumber = 10;

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
    
    private static void printErrorAndTryAgain(string i_Message)
    {
        Console.WriteLine(i_Message);
        Console.WriteLine("Press any key to try again");
        Console.ReadLine();
        Console.Clear();
    }
    
    public static string GetEnumInString(string i_EnumType, List<string> i_Options, bool i_AllowNone = false) 
    {
        string returnString = "";
        
        while (true)
        {
            Console.WriteLine($"Please choose the {i_EnumType} from the following:");
            for (int i = 0; i < i_Options.Count; i++)
            {
                Console.WriteLine($"{i + 1}) {i_Options[i]}");
            }
        
            Console.WriteLine();
            string? input = Console.ReadLine();
            bool resultIsEnum = false;
            
            if (int.TryParse(input, out int index) && index >= 1 && index <= i_Options.Count)
            {
                Console.Clear();
                returnString = i_Options[index - 1];
            }
            else
            {
                foreach (string option in i_Options)
                {
                    if (input.ToLower() == option.ToLower())
                    {
                        returnString = option;
                        resultIsEnum = true;
                        break;
                    }
                }
                if (i_AllowNone && !resultIsEnum)
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
    
    public static void CheckValidPhoneNumber(string i_InputToCheck)
    {
        if(i_InputToCheck.Length != k_LengthOfPhoneNumber)
        {
            throw new ArgumentException("Phone number must be 10 digits");
        }

        if (!int.TryParse(i_InputToCheck, out int number))
        {
            throw new FormatException("Phone number must contain only digits");
        }
    }
    
    public static bool GetYesOrNo(string i_Question)
    {
        Console.WriteLine(i_Question);
        Console.WriteLine("Please enter yes or no:");

        bool typedYesOrNo = false;
        bool returnValue = false;

        while (!typedYesOrNo)
        {
            string? input = Console.ReadLine()?.Trim().ToLower();

            if (input == "yes" || input == "y")
            {
                returnValue = true;
                typedYesOrNo = true;
            }
            else if (input == "no" || input == "n")
            {
                typedYesOrNo = false;
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter yes or no:");
            }
        }
        
        return returnValue;
    }

    public static string GetEnumInStringOrNone(string i_EnumType, List<string> i_Options , bool i_AllowNone = false)
    {
        string returnString = "";
        
        while (true)
        {
            Console.WriteLine($"Please choose the {i_EnumType} from the following:");
            for (int i = 0; i < i_Options.Count; i++)
            {
                Console.WriteLine($"{i + 1}) {i_Options[i]}");
            }
        
            Console.WriteLine();
            string? input = Console.ReadLine();
            bool foundOption = false;
            
            if (int.TryParse(input, out int index) && index >= 1 && index <= i_Options.Count)
            {
                Console.Clear();
                returnString = i_Options[index - 1];
            }
            else
            {
                foreach (string option in i_Options)
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