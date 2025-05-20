using Ex03.GarageLogic.EnergySystem;

namespace Ex03.GarageLogic;

public abstract class Car : Vehicle
{
    private const int k_NumberOfTires = 5;
    private const float k_MaxTireAirPressure = 32;
    public enum eColor
    {
        Yellow, Black, White, Silver
    }
    public enum eNumberOfDoors
    {
        Two, Three, Four, Five
    }
    private eColor Color { get; set; }
    private eNumberOfDoors NumberOfDoors { get; set; }

    protected Car(string i_LicenseId, string i_ModelName) : base( i_LicenseId, i_ModelName)
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

    public override void AddSpecificDetails(string i_Color, string i_NumberOfDoors)
    {
        try
        {
            Color = (eColor)Enum.Parse(typeof(eColor), i_Color);
            NumberOfDoors = (eNumberOfDoors)Enum.Parse(typeof(eNumberOfDoors), i_NumberOfDoors);
        }
        catch (FormatException e)
        {
            throw new FormatException(e.Message);
        }
    }
}

public class ElectricCar : Car
{
    private const float k_MaxBatterCapacityHours = 4.8f;
    public ElectricCar(string i_LicenseId, string i_ModelName) : base( i_LicenseId, i_ModelName)
    {
        m_EnergySystem = new Battery(k_MaxBatterCapacityHours);
    }
}

public class FuelCar : Car
{
    private const FuelSystem.eFuelType k_FuelType = FuelSystem.eFuelType.Octan95;
    private const float k_FuelTankCapacityLiter = 48f;
    
    public FuelCar(string i_LicenseId, string i_ModelName) : base(i_LicenseId, i_ModelName)
    {
        m_EnergySystem = new FuelSystem(k_FuelType, k_FuelTankCapacityLiter);
    }
}