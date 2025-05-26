namespace Ex03.GarageLogic;

internal class Truck : Vehicle
{
    private const FuelSystem.eFuelType k_FuelType = FuelSystem.eFuelType.Soler;
    private const float k_FuelTankCapacityLiter = 135;
    private const int k_NumberOfTires = 12;
    private const float k_MaxTireAirPressure = 27;
    private bool ContainsDangerousMaterials { get; set; }
    private float CargoVolume { get; set; }

    internal Truck(string i_LicenseId, string i_ModelName) : base( i_LicenseId, i_ModelName)
    {
        m_EnergySystem = new FuelSystem(k_FuelType, k_FuelTankCapacityLiter);
        m_Tires = new Tires(k_NumberOfTires, "ModelName", k_MaxTireAirPressure);
    }
    
    internal override Dictionary<string, string> GetDetails()
    {
        Dictionary<string, string> details = GetGeneralVehicleDetails();
        details.Add("Contains Dangerous Materials", ContainsDangerousMaterials.ToString());
        details.Add("Cargo Volume", CargoVolume.ToString());
        return details;
    }
    
    internal override void AddSpecificDetails(string i_ContainsDangerousMaterial, string i_CargoVolume)
    {
        try
        {
            ContainsDangerousMaterials = bool.Parse(i_ContainsDangerousMaterial);
            CargoVolume = float.Parse(i_CargoVolume);
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