namespace Ex03.GarageLogic;

internal abstract class Motorcycle : Vehicle
{
    private const int k_NumberOfTires = 2;
    private const float k_MaxTireAirPressure = 30;
    internal enum eLicenseType
    {
        A = 1, A2, AB, B2
    }
    private eLicenseType LicenseType { get; set; }
    private int EngineCapacity { get; set; }

    protected Motorcycle(string i_LicenseId, string i_ModelName) : base( i_LicenseId, i_ModelName)
    {
        m_Tires = new Tires(k_NumberOfTires, "ModelName", k_MaxTireAirPressure);
    }
    
    internal override Dictionary<string, string> GetDetails()
    {
        Dictionary<string, string> details = GetGeneralVehicleDetails();
        
        details.Add("License Type", LicenseType.ToString());
        details.Add("Engine Capacity", EngineCapacity.ToString());
        return details;
    }
    
    internal override void AddSpecificDetails(string i_LicenseType, string i_EngineCapacity)
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

    internal override float GetMaxAirPressure()
    {
        return k_MaxTireAirPressure;
    }
}

internal class ElectricMotorcycle : Motorcycle
{
    private const float k_MaxBatterCapacityHours = 3.2f;
    
    internal ElectricMotorcycle(string i_LicenseId, string i_ModelName) : base( i_LicenseId, i_ModelName)
    {
        m_EnergySystem = new Battery(k_MaxBatterCapacityHours);
    }
}

internal class FuelMotorcycle : Motorcycle
{
    private const FuelSystem.eFuelType k_FuelType = FuelSystem.eFuelType.Octan98;
    private const float k_FuelTankCapacityLiter = 5.8f;
    
    internal FuelMotorcycle(string i_LicenseId, string i_ModelName) : base( i_LicenseId, i_ModelName)
    {
        m_EnergySystem = new FuelSystem(k_FuelType, k_FuelTankCapacityLiter);
    }
}