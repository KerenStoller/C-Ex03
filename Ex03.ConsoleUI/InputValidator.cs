
namespace Ex03.ConsoleUI
{
    public class InputValidator
    {
        private static float checkValidFloat(string inputToCheck)
        {
            bool isValid = float.TryParse(inputToCheck, out float result);

            if (!isValid)
            {
                throw new FormatException("Input is not a valid float number.");
            }
            return result;
        }

        public static float checkValidPositiveFloat(string inputToCheck)
        {
            float result = checkValidFloat(inputToCheck);
            
            if (result < 0)
            {
                throw new ArgumentOutOfRangeException("Input must be a positive number.");
            }
            return result;
        }

        public static int checkValidInt(string inputToCheck)
        {
            bool isValid = int.TryParse(inputToCheck, out int result);
            if (!isValid)
            {
                throw new FormatException("Input is not a valid number.");
            }
            return result;
        }

        public static void checkValidPhoneNumber(string inputToCheck)
        {

            if(inputToCheck.Length != 10)
            {
                throw new ArgumentException("Phone number must be 10 digits");
                
            }

            foreach(char c in inputToCheck)
            {
                if(c < '0' || c > '9')
                {
                    throw new FormatException("Phone number must contain only digits");
                }
            }
        }
    }
}
