namespace Ex03.GarageLogic.EnergySystem;

public abstract class EnergySystem
{
    private const float k_MaxPercentage = 100;
    private const float k_MinPercentage = 0;
    protected float CurrentEnergyAmount { get; set; }
    protected readonly float r_MaxEnergyCapacity;
    protected float EnergyPercentage { get; set; }
    public abstract bool IsElectric { get; }
    
    public abstract string GetDetails();
    
    public void RefillEnergy(float i_EnergyToAdd)
    {
        CurrentEnergyAmount = CurrentEnergyAmount + i_EnergyToAdd > r_MaxEnergyCapacity ?
            r_MaxEnergyCapacity : CurrentEnergyAmount + i_EnergyToAdd;
    }

    public void AddDetails(float i_EnergyPercentage)
    {
        if (i_EnergyPercentage >= k_MinPercentage && i_EnergyPercentage <= k_MaxPercentage)
        {
            CurrentEnergyAmount = r_MaxEnergyCapacity * i_EnergyPercentage * 1/k_MaxPercentage;
            EnergyPercentage = i_EnergyPercentage;
        }
        else
        {
            throw new ValueRangeException(i_EnergyPercentage, k_MinPercentage, "Energy percentage outside of range");
        }
    }
    
    public EnergySystem(float i_MaxEnergyCapacity)
    {
        r_MaxEnergyCapacity = i_MaxEnergyCapacity;
        CurrentEnergyAmount = r_MaxEnergyCapacity;
    }

    protected string GetGeneralDetails()
    {
        return $"\n Energy Percentage: {EnergyPercentage}%\n Current Energy Amount: {CurrentEnergyAmount}\n Max Energy Capacity: {r_MaxEnergyCapacity}";
    }
}

public class Battery : EnergySystem
{
    public override bool IsElectric => true;
    public Battery(float i_MaxEnergyCapacity) : base(i_MaxEnergyCapacity) {}

    public override string GetDetails()
    {
        return GetGeneralDetails();
    }
}

public class FuelSystem : EnergySystem
{
    public enum eFuelType
    {
        Soler, Octan95, Octan96, Octan98
    }

    private readonly eFuelType r_MFuelType;
    
    public override bool IsElectric => false;

    public FuelSystem(eFuelType i_FuelType,  float i_MaxEnergyCapacity) : base(i_MaxEnergyCapacity)
    {
        r_MFuelType = i_FuelType;
    }

    public eFuelType GetFuelType()
    {
        return r_MFuelType;
    }

    public override string GetDetails()
    {
        string details = $"{GetGeneralDetails()}\nFuel Type: {r_MFuelType}";
        return details;
    }
}