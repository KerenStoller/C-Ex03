
namespace Ex03.ConsoleUI;

using GarageLogic;

public class UserInterface
{
    
    private enum eMenuOptions
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

    private GarageLogic m_GarageLogic;

    private GarageLogicUIBridge m_GarageLogicUIBridge;

    private bool m_IsRunning = true;
    public UserInterface()
    {
        m_GarageLogic = new GarageLogic();
        m_GarageLogicUIBridge = new GarageLogicUIBridge(m_GarageLogic);
    }

    public void GarageMenu()
    {
        while (m_IsRunning)
        {
            MenuManager.PrintMenu();
            string userInput = Console.ReadLine();
            handleUserInput(userInput);
        }
    }

    private void handleUserInput(string i_UserInput)
    {
        int input;

        try
        {
            input = InputValidator.checkValidInt(i_UserInput);
        }
        catch(FormatException e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine("Press any key to return to menu");
            Console.ReadLine();
            Console.Clear();
            return;
        }


        eMenuOptions selectedOption = (eMenuOptions)input;

        switch (selectedOption)
        {
            case eMenuOptions.LoadVehiclesFromFile:
                m_GarageLogicUIBridge.LoadVehiclesFromFile();
                break;

            case eMenuOptions.AddVehicleToGarage:
                m_GarageLogicUIBridge.addVehicle();
                break;

            case eMenuOptions.ShowAllVehiclesInGarage:
                printAllVehiclesInGarage();
                break;

            case eMenuOptions.ChangeVehicleState:
                m_GarageLogicUIBridge.ChangeVehicleState();
                break;

            case eMenuOptions.InflateTires:
                m_GarageLogicUIBridge.InflateTires();
                break;

            case eMenuOptions.FuelVehicle:
                m_GarageLogicUIBridge.FuelVehicle();
                break;

            case eMenuOptions.ChargeVehicle:
                m_GarageLogicUIBridge.ChargeVehicle();
                break;

            case eMenuOptions.ShowVehicleDetails:
                m_GarageLogicUIBridge.ShowVehicleDetails();
                break;

            case eMenuOptions.Exit:
                m_IsRunning = false;
                Console.Clear();
                Console.WriteLine("Exiting the program. Goodbye!");
                return;

            default:
                Console.WriteLine("Invalid menu option. Please try again, press any key to continue");
                Console.ReadLine();
                break;
        }
        // Console.WriteLine("Press any key to return to menu");
        // Console.ReadLine();
        Console.Clear();
    }

    public static void handleVehicleChoice(string i_UserInput, out string io_vehicleType)
    {
        int input;
        io_vehicleType = "";

        try
        {
            input = InputValidator.checkValidInt(i_UserInput);
        }
        catch(FormatException e)
        {
            MenuManager.PrintError(e.Message);
            return;
        }

        if(Enum.IsDefined(typeof(eVehicleTypesOptions), input))
        {
            eVehicleTypesOptions selectedOption = (eVehicleTypesOptions)input;

            switch (selectedOption)
            {
                case eVehicleTypesOptions.FuelCar:
                    io_vehicleType  = "FuelCar";
                    break;

                case eVehicleTypesOptions.ElectricCar:
                    io_vehicleType = "ElectricCar";
                    break;

                case eVehicleTypesOptions.FuelMotorcycle:
                    io_vehicleType = "FuelMotorcycle";
                    break;

                case eVehicleTypesOptions.ElectricMotorcycle:
                    io_vehicleType = "ElectricMotorcycle";
                    break;

                case eVehicleTypesOptions.Truck:
                    io_vehicleType = "Truck";
                    break;
            }
        }

        else
        {
            MenuManager.PrintError("Invalid vehicle type option. Please try again.");
        }

    }

    private void printAllVehiclesInGarage()
    {
        Console.WriteLine("Select vehicle state to filter by:");
        foreach (string state in Enum.GetNames(typeof(Vehicle.eVehicleState)))
        {
            Console.WriteLine($"- {state}");
        }
        Console.WriteLine("Or press Enter to show all vehicles.");

        string? userInput = Console.ReadLine();
        string? filterState = string.IsNullOrWhiteSpace(userInput) ? null : userInput;

        List<string> vehicles = m_GarageLogic.GetLicenseIdOfAllVehiclesInGarage(filterState);

        if (vehicles.Count == 0)
        {
            Console.WriteLine("There are no vehicles in garage for the selected state.");
        }
        else
        {
            foreach (string reg in vehicles)
            {
                Console.WriteLine(reg);
            }
        }

        Console.WriteLine("Press any key to return to menu");
        Console.ReadLine();
        Console.Clear();
    }


   

    
}