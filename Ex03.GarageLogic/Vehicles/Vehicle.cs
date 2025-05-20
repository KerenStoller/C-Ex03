using Ex03.GarageLogic.EnergySystem;

namespace Ex03.GarageLogic;

public abstract class Vehicle
{
    public enum e_VehicleState
    {
        InRepair, 
        Repaired,
        Paid
    }
    public readonly string r_ModelName;
    public readonly string r_LicenseID;
    protected Tires m_Tires;
    protected string m_OwnerName;
    private string m_OwnerPhoneNumber;
    public e_VehicleState VehicleState { get; set; } =  e_VehicleState.InRepair;
    protected EnergySystem.EnergySystem m_EnergySystem;
    
    protected Vehicle(string i_LicenseID, string i_ModelName)
    {
        r_LicenseID = i_LicenseID;
        r_ModelName = i_ModelName;
    }
    
    public abstract void AddSpecificDetails(string i_Detail1, string i_Detail2);
    
    public abstract List<string> GetDetails();

    public void AddGeneralDetails(float i_EnergyPercentage, float i_CurrentEnergy, 
        string i_TireModelName, float i_currentAirPressure)
    {
        m_EnergySystem.AddDetails(i_EnergyPercentage, i_CurrentEnergy);
        m_Tires.AddDetails(i_TireModelName, i_currentAirPressure);
        //Throws ValueRangeException
    }
    
    public bool IsElectric()
    {
        return m_EnergySystem.IsElectric;
    }
    
    private void validateElectric()
    {
        if (!IsElectric())
        {
            throw new ArgumentException("Vehicle isn't electric");
        }
    }
    
    private void validateNotElectric()
    {
        if (IsElectric())
        {
            throw new ArgumentException("Vehicle is electric");
        }
    }
    
    private void validateFuelType(EnergySystem.FuelSystem.e_FuelType i_FuelType)
    {
        validateNotElectric();
        if (((FuelSystem)m_EnergySystem).getFuelType() != i_FuelType)
        {
            throw new ArgumentException("Fuel type is different");
        }
    }
    
    public int NumberOfTires()
    {
        return m_Tires.NumberOfTires;
    }

    public void InflateTires()
    {
        m_Tires.InflateTires();
    }

    public void FillTank(EnergySystem.FuelSystem.e_FuelType i_FuelType, float i_AmountOfFuelToAdd)
    {
        validateFuelType(i_FuelType);
        m_EnergySystem.RefillEnergy(i_AmountOfFuelToAdd);
    }

    public void ChargeBattery(float i_TimeToChargeInMinutes)
    {
        validateElectric();
        m_EnergySystem.RefillEnergy(i_TimeToChargeInMinutes);
    }
    
    protected List<string> GetGeneralVehicleDetails()
    {
        List<string> details = new List<string>();
        details.Add(r_LicenseID);
        details.Add(r_ModelName);
        details.Add(m_OwnerName);
        details.Add(VehicleState.ToString());
        details.AddRange(m_Tires.GetDetails());
        details.AddRange(m_EnergySystem.GetDetails());
        return details;
    }
}