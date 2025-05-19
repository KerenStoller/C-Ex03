namespace Ex03.GarageLogic.EnergySystem;

public abstract class EnergySystem
{
    protected float CurrentEnergyAmount { get; set; }
    protected float MaxEnergyCapacity { get; set; }
    protected void RefillEnergy(float i_EnergyToAdd)
    {
        CurrentEnergyAmount = CurrentEnergyAmount + i_EnergyToAdd > MaxEnergyCapacity ?
            MaxEnergyCapacity : CurrentEnergyAmount + i_EnergyToAdd;
    }

    public abstract bool IsElectric { get; }

    public EnergySystem(float i_MaxEnergyCapacity)
    {
        MaxEnergyCapacity = i_MaxEnergyCapacity;
        CurrentEnergyAmount = MaxEnergyCapacity;
    }
}

public class Battery : EnergySystem
{
    public override bool IsElectric => true;
    public Battery(float i_MaxEnergyCapacity) : base(i_MaxEnergyCapacity) {}
}

public class FuelSystem : EnergySystem
{
    public enum e_FuelType
    {
        Soler, Octan95, Octan96, Octan98
    }

    private e_FuelType m_FuelType;
    
    public override bool IsElectric => false;

    public FuelSystem(e_FuelType i_FuelType,  float i_MaxEnergyCapacity) : base(i_MaxEnergyCapacity)
    {
        m_FuelType = i_FuelType;
    }
}