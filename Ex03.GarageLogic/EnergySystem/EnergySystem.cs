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
}