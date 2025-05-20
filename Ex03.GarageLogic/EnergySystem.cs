namespace Ex03.GarageLogic.EnergySystem;

public abstract class EnergySystem
{
    protected float CurrentEnergyAmount { get; set; }
    protected readonly float r_MMaxEnergyCapacity;
    protected float EnergyPercentage { get; set; }
    public abstract bool IsElectric { get; }
    
    public abstract List<string> GetDetails();
    
    public void RefillEnergy(float i_EnergyToAdd)
    {
        CurrentEnergyAmount = CurrentEnergyAmount + i_EnergyToAdd > r_MMaxEnergyCapacity ?
            r_MMaxEnergyCapacity : CurrentEnergyAmount + i_EnergyToAdd;
    }

    public void AddDetails(float i_EnergyPercentage, float i_CurrentEnergy)
    {
        if (i_CurrentEnergy <= r_MMaxEnergyCapacity && i_CurrentEnergy > 0)
        {
            CurrentEnergyAmount = i_CurrentEnergy;
            //TODO: calculate and throw if needed
            EnergyPercentage = i_EnergyPercentage;
        }
        else
        {
            throw new ValueRangeException(r_MMaxEnergyCapacity, 0);
            //TODO: how to add message
        }
    }
    
    public EnergySystem(float i_MaxEnergyCapacity)
    {
        r_MMaxEnergyCapacity = i_MaxEnergyCapacity;
        CurrentEnergyAmount = r_MMaxEnergyCapacity;
    }

    protected List<string> GetGeneralDetails()
    {
        List<string> details = new List<string>();
        details.Add(CurrentEnergyAmount.ToString());
        details.Add(r_MMaxEnergyCapacity.ToString());
        return details;
    }
}

public class Battery : EnergySystem
{
    public override bool IsElectric => true;
    public Battery(float i_MaxEnergyCapacity) : base(i_MaxEnergyCapacity) {}

    public override List<string> GetDetails()
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

    public override List<string> GetDetails()
    {
        List<string> details = GetGeneralDetails();
        details.Add(r_MFuelType.ToString());
        return details;
    }
}