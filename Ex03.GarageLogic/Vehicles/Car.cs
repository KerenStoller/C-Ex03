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

    protected Car()
    {
        NumberOfTires = 5;
    }
}

public class ElectricCar : Car
{
    public ElectricCar()
    {
        m_EnergySystem = new Battery();
    }
}

public class FuelCar : Car
{
    public FuelCar()
    {
        m_EnergySystem = new FuelSystem();
    }
}