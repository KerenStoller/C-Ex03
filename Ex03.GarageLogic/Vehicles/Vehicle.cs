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

    public bool IsElectric()
    {
        return m_EnergySystem.IsElectric;
    }

    private bool IsRightFuel(EnergySystem.FuelSystem.e_FuelType i_FuelType)
    {
        bool returnValue = false;
        if (!IsElectric())
        {
            if (((FuelSystem)m_EnergySystem).getFuelType() == i_FuelType)
            {
                returnValue = true;
            }
            else
            {
                //TODO: throw exception
            }
        }
        else
        {
            //TODO: throw Exception;
        }
        return returnValue;
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
        if (IsRightFuel(i_FuelType))
        {
            m_EnergySystem.RefillEnergy(i_AmountOfFuelToAdd);
        }
        else
        {
            //TODO throw exception either electric or wrong fuel
        }
    }

    public void ChargeBattery(float i_TimeToChargeInMinutes)
    {
        if (IsElectric())
        {
            m_EnergySystem.RefillEnergy(i_TimeToChargeInMinutes);
        }
        else
        {
            //TODO: throw exc
        }
    }

    public abstract List<string> GetDetails();

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