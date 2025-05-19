using Ex03.GarageLogic.EnergySystem;

namespace Ex03.GarageLogic;

public class Truck : Vehicle
{
    private const FuelSystem.e_FuelType k_FuelType = FuelSystem.e_FuelType.Soler;
    private const float k_FuelTankCapacityLiter = 135;
    private const int k_NumberOfTires = 12;
    private const float k_MaxTireAirPressure = 27;
    
    private bool ContainsDangerousMaterials { get; set; }
    private float CargoVolume { get; set; }

    public Truck(string i_LicenseID, string i_ModelName) : base( i_LicenseID, i_ModelName)
    {
        m_EnergySystem = new FuelSystem(k_FuelType, k_FuelTankCapacityLiter);
        m_Tires = new Tires(k_NumberOfTires, "ModelName", k_MaxTireAirPressure);
    }
    
    public override List<string> GetDetails()
    {
        List<string> details = GetGeneralVehicleDetails();
        details.Add(ContainsDangerousMaterials.ToString());
        details.Add(CargoVolume.ToString());
        return details;
    }
}