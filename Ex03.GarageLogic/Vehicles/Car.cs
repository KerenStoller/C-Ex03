using Ex03.GarageLogic.EnergySystem;

namespace Ex03.GarageLogic;

public abstract class Car : Vehicle
{
    private enum e_Color
    {
        Yellow, Black, White, Silver
    }

    private enum e_NumberOfDoors
    {
        two, three, four, five
    }
    
    private e_Color m_Color;
    private e_NumberOfDoors m_NumberOfDoors;
}

public class ElectricCar : Car
{
    private Battery m_Battery;
}

public class FuelCar : Car
{
    private FuelSystem m_FuelSystem;
}