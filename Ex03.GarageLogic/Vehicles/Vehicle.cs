using Ex03.GarageLogic.EnergySystem;

namespace Ex03.GarageLogic;

public abstract class Vehicle
{
    public enum eVehicleState
    {
        InRepair, 
        Repaired,
        Paid
    }
    
    public readonly string r_ModelName;
    public readonly string r_LicenseId;
    protected Tires m_Tires;
    protected string m_OwnerName;
    private string m_OwnerPhoneNumber;
    public eVehicleState VehicleState { get; set; } =  eVehicleState.InRepair;
    protected EnergySystem.EnergySystem m_EnergySystem;
    
    protected Vehicle(string i_LicenseId, string i_ModelName)
    {
        r_LicenseId = i_LicenseId;
        r_ModelName = i_ModelName;
    }
    
    public abstract void AddSpecificDetails(string i_Detail1, string i_Detail2);
    
    public abstract List<string> GetDetails();

    public void AddGeneralDetails(string i_OwnerName, string i_OwnerPhone, float i_EnergyPercentage)
    {
        m_OwnerName = i_OwnerName;
        m_OwnerPhoneNumber = i_OwnerPhone;
        m_EnergySystem.AddDetails(i_EnergyPercentage);
        //Throws ValueRangeException
    }

    public void AddGeneralTires(string i_TireModelName, float i_CurrentAirPressure)
    {
        m_Tires.AddDetailsForAllTires(i_TireModelName, i_CurrentAirPressure);
    }
    
    public void AddSpecificTires(List<KeyValuePair<string, float>> i_TireModelNamesAndPressures)
    {
        m_Tires.AddDetailsForTires(i_TireModelNamesAndPressures);
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
    
    private void validateFuelType(string i_FuelTypeString)
    {
        validateNotElectric();
        if (!Enum.IsDefined(typeof(FuelSystem.eFuelType), i_FuelTypeString))
        { 
            throw new ArgumentException($"{i_FuelTypeString} is not a valid fuel type");
        }
        
        FuelSystem.eFuelType fuelType = (FuelSystem.eFuelType)Enum.Parse(typeof(FuelSystem.eFuelType), i_FuelTypeString);
        
        if (((FuelSystem)m_EnergySystem).GetFuelType() != fuelType)
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

    public void FillTank(string i_FuelType, float i_AmountOfFuelToAdd)
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
        details.Add(r_LicenseId);
        details.Add(r_ModelName);
        details.Add(m_OwnerName);
        details.Add(VehicleState.ToString());
        details.AddRange(m_Tires.GetDetails());
        details.AddRange(m_EnergySystem.GetDetails());
        return details;
    }
    
    public void SetPressureOfSingleTire(int i_Index, float i_AirPressure)
    {
        m_Tires.SetPressureOfSingleTire(i_Index, i_AirPressure);
    }
}