using Ex03.GarageLogic.EnergySystem;

namespace Ex03.GarageLogic;

public class Truck : Vehicle
{
    private bool m_ContainsDangerousMaterials;
    private float m_CargoVolume;

    public Truck()
    {
        NumberOfTires = 12;
        m_EnergySystem = new FuelSystem();
    }
}