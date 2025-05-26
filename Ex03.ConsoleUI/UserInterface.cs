namespace Ex03.ConsoleUI;

using GarageLogic;

public class UserInterface
{
    private GarageLogicUIBridge m_GarageLogicUIBridge;
    private bool m_UserDidntPressExit = true;
    
    public UserInterface()
    {
        m_GarageLogicUIBridge = new GarageLogicUIBridge();
    }

    public void GarageMenu()
    {
        while (m_UserDidntPressExit)
        {
            handleUserInput();
        }
    }

    private void handleUserInput()
    {
        MenuManager.PrintMenu();
        InputHelper.eMenuOptions selectedOption = InputHelper.GetEnum<InputHelper.eMenuOptions>
            ("menu option");
        
        Console.Clear();
        switch (selectedOption)
        {
            case InputHelper.eMenuOptions.LoadVehiclesFromFile:
                m_GarageLogicUIBridge.LoadVehiclesFromFile();
                break;

            case InputHelper.eMenuOptions.AddVehicleToGarage:
                m_GarageLogicUIBridge.AddVehicle();
                break;

            case InputHelper.eMenuOptions.ShowAllVehiclesInGarage:
                m_GarageLogicUIBridge.GetLicenseIdOfAllVehiclesInGarage();
                break;

            case InputHelper.eMenuOptions.ChangeVehicleState:
                m_GarageLogicUIBridge.ChangeVehicleState();
                break;

            case InputHelper.eMenuOptions.InflateTires:
                m_GarageLogicUIBridge.InflateTires();
                break;

            case InputHelper.eMenuOptions.FuelVehicle:
                m_GarageLogicUIBridge.FuelVehicle();
                break;

            case InputHelper.eMenuOptions.ChargeVehicle:
                m_GarageLogicUIBridge.ChargeVehicle();
                break;

            case InputHelper.eMenuOptions.ShowVehicleDetails:
                m_GarageLogicUIBridge.ShowVehicleDetails();
                break;

            case InputHelper.eMenuOptions.Exit:
                m_UserDidntPressExit = false;
                Console.Clear();
                Console.WriteLine("Exiting the program. Goodbye!");
                return;
        }
        Console.Clear();
    }
}