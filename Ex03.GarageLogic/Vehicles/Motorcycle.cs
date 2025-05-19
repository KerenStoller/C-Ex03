using Ex03.GarageLogic.EnergySystem;

namespace Ex03.GarageLogic;

public abstract class Motorcycle : Vehicle
{
    private enum e_LicenseType
    {
        A, A2, Ab, B2
    }
    
    private e_LicenseType m_LicenseType;
    private int m_EngineCapacity;

    protected Motorcycle()
    {
        NumberOfTires = 2;
    }
}

public class ElectricMotorcycle : Motorcycle
{
    public ElectricMotorcycle()
    {
        m_EnergySystem = new Battery();
    }
}

public class FuelMotorcycle : Motorcycle
{
    public FuelMotorcycle()
    {
        m_EnergySystem = new FuelSystem();
    }
}