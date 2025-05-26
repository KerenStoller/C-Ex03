namespace Ex03.GarageLogic;

public abstract class EnergySystem
{
    private const float k_MaxPercentage = 100;
    private const float k_MinPercentage = 0;
    protected float CurrentEnergyAmount { get; set; }
    protected readonly float r_MaxEnergyCapacity;
    protected float EnergyPercentage { get; set; }
    internal abstract bool IsElectric { get; }
    
    internal abstract string GetDetails();
    
    internal void RefillEnergy(float i_EnergyToAdd)
    {
        CurrentEnergyAmount = CurrentEnergyAmount + i_EnergyToAdd > r_MaxEnergyCapacity ?
            r_MaxEnergyCapacity : CurrentEnergyAmount + i_EnergyToAdd;
        EnergyPercentage = CurrentEnergyAmount / r_MaxEnergyCapacity * k_MaxPercentage;
    }

    internal void AddDetails(float i_EnergyPercentage)
    {
        if (i_EnergyPercentage >= k_MinPercentage && i_EnergyPercentage <= k_MaxPercentage)
        {
            CurrentEnergyAmount = r_MaxEnergyCapacity * i_EnergyPercentage * 1/k_MaxPercentage;
            EnergyPercentage = i_EnergyPercentage;
        }
        else
        {
            throw new ValueRangeException
                (i_EnergyPercentage, k_MinPercentage, "Energy percentage outside of range");
        }
    }
    
    internal EnergySystem(float i_MaxEnergyCapacity)
    {
        r_MaxEnergyCapacity = i_MaxEnergyCapacity;
        CurrentEnergyAmount = r_MaxEnergyCapacity;
    }

    protected string GetGeneralDetails()
    {
        return $"\nEnergy Percentage: {EnergyPercentage}%" +
               $"\nCurrent Energy Amount: {CurrentEnergyAmount}" +
               $"\nMax Energy Capacity: {r_MaxEnergyCapacity}";
    }
}

internal class Battery : EnergySystem
{
    internal override bool IsElectric
    {
        get { return true; }
    }
    internal Battery(float i_MaxEnergyCapacity) : base(i_MaxEnergyCapacity) {}

    internal override string GetDetails()
    {
        return GetGeneralDetails();
    }
}

internal class FuelSystem : EnergySystem
{
    internal enum eFuelType
    {
        Soler = 1, Octan95, Octan96, Octan98
    }

    private readonly eFuelType r_MFuelType;
    
    internal override bool IsElectric
    {
        get { return false; }
    }

    internal FuelSystem(eFuelType i_FuelType,  float i_MaxEnergyCapacity) : base(i_MaxEnergyCapacity)
    {
        r_MFuelType = i_FuelType;
    }

    internal eFuelType GetFuelType()
    {
        return r_MFuelType;
    }

    internal override string GetDetails()
    {
        string details = $"{GetGeneralDetails()}\nFuel Type: {r_MFuelType}";
        return details;
    }
}