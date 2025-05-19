namespace Ex03.GarageLogic.EnergySystem;

public abstract class EnergySystem
{
    protected float CurrentEnergyAmount { get; set; }
    protected readonly float m_MaxEnergyCapacity;
    protected float EnergyPercentage { get; set; }
    
    public void RefillEnergy(float i_EnergyToAdd)
    {
        CurrentEnergyAmount = CurrentEnergyAmount + i_EnergyToAdd > m_MaxEnergyCapacity ?
            m_MaxEnergyCapacity : CurrentEnergyAmount + i_EnergyToAdd;
    }

    public void AddDetails(float i_EnergyPercentage, float i_CurrentEnergy)
    {
        if (i_CurrentEnergy <= m_MaxEnergyCapacity && i_CurrentEnergy > 0)
        {
            CurrentEnergyAmount = i_CurrentEnergy;
            //TODO: calculate and throw if needed
            EnergyPercentage = i_EnergyPercentage;
        }
        else
        {
            throw new ValueRangeException(m_MaxEnergyCapacity, 0);
            //TODO: how to add message
        }
    }

    public abstract bool IsElectric { get; }

    public EnergySystem(float i_MaxEnergyCapacity)
    {
        m_MaxEnergyCapacity = i_MaxEnergyCapacity;
        CurrentEnergyAmount = m_MaxEnergyCapacity;
    }
    
    public abstract List<string> GetDetails();

    protected List<string> GetGeneralDetails()
    {
        List<string> details = new List<string>();
        details.Add(CurrentEnergyAmount.ToString());
        details.Add(m_MaxEnergyCapacity.ToString());
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
    public enum e_FuelType
    {
        Soler, Octan95, Octan96, Octan98
    }

    private readonly e_FuelType m_FuelType;
    
    public override bool IsElectric => false;

    public FuelSystem(e_FuelType i_FuelType,  float i_MaxEnergyCapacity) : base(i_MaxEnergyCapacity)
    {
        m_FuelType = i_FuelType;
    }

    public e_FuelType getFuelType()
    {
        return m_FuelType;
    }

    public override List<string> GetDetails()
    {
        List<string> details = GetGeneralDetails();
        details.Add(m_FuelType.ToString());
        return details;
    }
}