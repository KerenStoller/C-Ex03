namespace Ex03.GarageLogic;

internal abstract class Vehicle
{
    internal enum eVehicleState
    {
        InRepair = 1, 
        Repaired,
        Paid
    }
    
    internal readonly string r_ModelName;
    internal readonly string r_LicenseId;
    protected Tires m_Tires;
    protected string m_OwnerName;
    private string m_OwnerPhoneNumber;
    internal eVehicleState VehicleState { get; set; } =  eVehicleState.InRepair;
    protected EnergySystem m_EnergySystem;
    
    protected Vehicle(string i_LicenseId, string i_ModelName)
    {
        r_LicenseId = i_LicenseId;
        r_ModelName = i_ModelName;
    }
    
    internal abstract void AddSpecificDetails(string i_Detail1, string i_Detail2);
    
    internal abstract Dictionary<string, string> GetDetails();

    internal void AddGeneralDetails(string i_OwnerName, string i_OwnerPhone, float i_EnergyPercentage)
    {
        m_OwnerName = i_OwnerName;
        m_OwnerPhoneNumber = i_OwnerPhone;
        m_EnergySystem.AddDetails(i_EnergyPercentage);
    }

    internal void AddGeneralTires(string i_TireModelName, float i_CurrentAirPressure)
    {
        m_Tires.AddDetailsForAllTires(i_TireModelName, i_CurrentAirPressure);
    }
    
    internal void AddSpecificTires(List<KeyValuePair<string, float>> i_TireModelNamesAndPressures)
    {
        m_Tires.AddDetailsForTires(i_TireModelNamesAndPressures);
    }
    
    internal bool IsElectric()
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
    
    private void validateFuelType(string i_FuelType)
    {
        validateNotElectric();
        
        try
        {
            FuelSystem.eFuelType fuelType = (FuelSystem.eFuelType)Enum.Parse
                (typeof(FuelSystem.eFuelType), i_FuelType, true);

            if (((FuelSystem)m_EnergySystem).GetFuelType() != fuelType)
            {
                throw new ArgumentException("Fuel type is different");
            }
        }
        catch (Exception)
        {
            throw new ArgumentException("Invalid fuel type");
        }
    }
    
    internal int NumberOfTires()
    {
        return m_Tires.NumberOfTires;
    }

    internal void InflateTires()
    {
        m_Tires.InflateTires();
    }

    internal void FillTank(string i_FuelType, float i_AmountOfFuelToAdd)
    {
        validateFuelType(i_FuelType);
        m_EnergySystem.RefillEnergy(i_AmountOfFuelToAdd);
    }

    internal static eVehicleState validateState(string i_State)
    {
        try
        {
            eVehicleState newState = (eVehicleState)Enum.Parse
                (typeof(eVehicleState), i_State, true);
            return newState;
        }
        catch (Exception)
        {
            throw new ArgumentException("Invalid state type");
        }
    }

    internal void UpdateState(string i_State)
    {
        VehicleState = validateState(i_State);
    }

    internal void ChargeBattery(float i_TimeToChargeInMinutes)
    {
        validateElectric();
        m_EnergySystem.RefillEnergy(i_TimeToChargeInMinutes);
    }
    
    protected Dictionary<string, string> GetGeneralVehicleDetails()
    {
        Dictionary<string, string> details = new Dictionary<string, string>();
        details.Add("License ID", r_LicenseId);
        details.Add("Model Name", r_ModelName);
        details.Add("Owner Name", m_OwnerName);
        details.Add("Owner Phone Number", m_OwnerPhoneNumber);
        details.Add("Vehicle State", VehicleState.ToString());
        
        foreach (KeyValuePair<string, string> tireDetails in m_Tires.GetDetails())
        {
            details.Add(tireDetails.Key, tireDetails.Value);
        }
        
        details.Add("Energy Details", m_EnergySystem.GetDetails());
        return details;
    }
    
    internal void SetPressureOfSingleTire(int i_Index, float i_AirPressure)
    {
        m_Tires.SetPressureOfSingleTire(i_Index, i_AirPressure);
        
    }

    internal void SetPressureOfAllTires(string i_ModelName, float i_AirPressure)
    {
        m_Tires.AddDetailsForAllTires(i_ModelName, i_AirPressure);
    }

    internal abstract float GetMaxAirPressure();
}