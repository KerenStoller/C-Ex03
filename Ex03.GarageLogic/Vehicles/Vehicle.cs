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
    private int m_numberOfTires;
    private List<Tire> m_Tires;
    private string m_OwnerName;
    private string m_OwnerPhoneNumber;
    private e_VehicleState m_VehicleState =  e_VehicleState.InRepair;
}