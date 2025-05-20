using Ex03.GarageLogic.EnergySystem;

namespace Ex03.GarageLogic;

public abstract class Motorcycle : Vehicle
{
    private const int k_NumberOfTires = 2;
    private const float k_MaxTireAirPressure = 30;
    public enum eLicenseType
    {
        A, A2, Ab, B2
    }
    private eLicenseType LicenseType { get; set; }
    private int EngineCapacity { get; set; }

    protected Motorcycle(string i_LicenseId, string i_ModelName) : base( i_LicenseId, i_ModelName)
    {
        m_Tires = new Tires(k_NumberOfTires, "ModelName", k_MaxTireAirPressure);
    }
    
    public override List<string> GetDetails()
    {
        List<string> details = GetGeneralVehicleDetails();
        details.Add(LicenseType.ToString());
        details.Add(EngineCapacity.ToString());
        return details;
    }
    
    public override void AddSpecificDetails(string i_LicenseType, string i_EngineCapacity)
    {
        try
        {
            LicenseType = Enum.Parse<eLicenseType>(i_LicenseType);
            EngineCapacity = Convert.ToInt32(i_EngineCapacity);
        }
        catch (FormatException e)
        {
            throw new FormatException(e.Message);
        }
    }
}

public class ElectricMotorcycle : Motorcycle
{
    private const float k_MaxBatterCapacityHours = 3.2f;
    
    public ElectricMotorcycle(string i_LicenseId, string i_ModelName) : base( i_LicenseId, i_ModelName)
    {
        m_EnergySystem = new Battery(k_MaxBatterCapacityHours);
    }
}

public class FuelMotorcycle : Motorcycle
{
    private const FuelSystem.eFuelType k_FuelType = FuelSystem.eFuelType.Octan98;
    private const float k_FuelTankCapacityLiter = 5.8f;
    
    public FuelMotorcycle(string i_LicenseId, string i_ModelName) : base( i_LicenseId, i_ModelName)
    {
        m_EnergySystem = new FuelSystem(k_FuelType, k_FuelTankCapacityLiter);
    }
}