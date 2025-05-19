namespace Ex03.GarageLogic.EnergySystem;

public class FuelSystem : EnergySystem
{
    private enum e_FuelType
    {
        Soler, Octan95, Octan96, Octan98
    }

    private e_FuelType m_FuelType;
    
    public override bool IsElectric => false;
}