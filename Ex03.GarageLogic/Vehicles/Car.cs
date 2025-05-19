using Ex03.GarageLogic.EnergySystem;

namespace Ex03.GarageLogic;

public abstract class Car : Vehicle
{
    private const int k_NumberOfTires = 5;
    private const float k_MaxTireAirPressure = 32;
    
    public enum e_Color
    {
        Yellow, Black, White, Silver
    }

    public enum e_NumberOfDoors
    {
        two, three, four, five
    }
    
    private e_Color Color { get; set; }
    private e_NumberOfDoors NumberOfDoors { get; set; }

    protected Car(string i_LicenseID, string i_ModelName) : base( i_LicenseID, i_ModelName)
    {
        m_Tires = new Tires(k_NumberOfTires, "ModelName", k_MaxTireAirPressure);
    }

    public override List<string> GetDetails()
    {
        List<string> details = GetGeneralVehicleDetails();
        details.Add(Color.ToString());
        details.Add(NumberOfDoors.ToString());
        return details;
    }
}

public class ElectricCar : Car
{
    private const float k_MaxBatterCapacityHours = 4.8f;
    public ElectricCar(string i_LicenseID, string i_ModelName) : base( i_LicenseID, i_ModelName)
    {
        m_EnergySystem = new Battery(k_MaxBatterCapacityHours);
    }
}

public class FuelCar : Car
{
    private const FuelSystem.e_FuelType k_FuelType = FuelSystem.e_FuelType.Octan95;
    private const float k_FuelTankCapacityLiter = 48f;
    
    public FuelCar(string i_LicenseID, string i_ModelName) : base(i_LicenseID, i_ModelName)
    {
        m_EnergySystem = new FuelSystem(k_FuelType, k_FuelTankCapacityLiter);
    }
}