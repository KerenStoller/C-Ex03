using Ex03.GarageLogic.EnergySystem;

namespace Ex03.GarageLogic;

public abstract class Car : Vehicle
{
    public enum e_Color
    {
        Yellow, Black, White, Silver
    }

    public enum e_NumberOfDoors
    {
        two, three, four, five
    }
    
    private e_Color Color { get; set; }
    private e_NumberOfDoors NumberOfDoors { get; set; }

    protected Car()
    {
        m_Tires = new Tires(5, "ModelName", 32);
    }
}

public class ElectricCar : Car
{
    public ElectricCar()
    {
        m_EnergySystem = new Battery(4.8f);
    }
}

public class FuelCar : Car
{
    public FuelCar()
    {
        m_EnergySystem = new FuelSystem(EnergySystem.FuelSystem.e_FuelType.Octan95, 48f);
    }
}