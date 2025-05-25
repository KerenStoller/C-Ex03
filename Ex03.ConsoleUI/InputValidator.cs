
namespace Ex03.ConsoleUI
{
    public class InputValidator
    {
        public static float checkValidFloat(string inputToCheck)
        {
            bool isValid = float.TryParse(inputToCheck, out float result);

            if (!isValid)
            {
                throw new FormatException("Input is not a valid float number.");
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
    }
}
