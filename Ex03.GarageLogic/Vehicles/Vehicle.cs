namespace Ex03.GarageLogic;

public abstract class Vehicle
{
    private enum e_VehicleState
    {
        InRepair, 
        Repaired,
        Paid
    }
    
    private string m_ModelName;
    private string m_LicensePlate;
    private float m_FuelOrEnergyCapacity;
    protected Tires m_Tires;
    private string m_OwnerName;
    private string m_OwnerPhoneNumber;
    private e_VehicleState m_VehicleState =  e_VehicleState.InRepair;
    
    protected EnergySystem.EnergySystem m_EnergySystem;

    public bool IsElectric()
    {
        return m_EnergySystem.IsElectric;
    }

    public int NumberOfTires()
    {
        return m_Tires.NumberOfTires;
    }
}