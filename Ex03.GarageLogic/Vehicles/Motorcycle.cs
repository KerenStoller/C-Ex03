using Ex03.GarageLogic.EnergySystem;

namespace Ex03.GarageLogic;

public abstract class Motorcycle : Vehicle
{
    public enum e_LicenseType
    {
        A, A2, Ab, B2
    }
    
    private e_LicenseType LicenseType { get; set; }
    private int EngineCapacity { get; set; }

    protected Motorcycle()
    {
        m_Tires = new Tires(2, "ModelName", 30);
    }
}

public class ElectricMotorcycle : Motorcycle
{
    public ElectricMotorcycle()
    {
        m_EnergySystem = new Battery(3.2f);
    }
}

public class FuelMotorcycle : Motorcycle
{
    public FuelMotorcycle()
    {
        m_EnergySystem = new FuelSystem(EnergySystem.FuelSystem.e_FuelType.Octan98, 5.8f);
    }
}