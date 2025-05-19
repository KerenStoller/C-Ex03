using Ex03.GarageLogic.EnergySystem;

namespace Ex03.GarageLogic;

public class Truck : Vehicle
{
    private bool ContainsDangerousMaterials { get; set; }
    private float CargoVolume { get; set; }

    public Truck()
    {
        m_EnergySystem = new FuelSystem(EnergySystem.FuelSystem.e_FuelType.Soler, 135);
        m_Tires = new Tires(12, "ModelName", 27);
    }
}