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
}

public class ElectricMotorcycle : Motorcycle
{
    private Battery m_Battery;
}

public class FuelMotorcycle : Motorcycle
{
    private FuelSystem m_FuelSystem;
}