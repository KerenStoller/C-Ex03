namespace Ex03.GarageLogic;

internal abstract class Car : Vehicle
{
    private const int k_NumberOfTires = 5;
    private const float k_MaxTireAirPressure = 32;
    internal enum eColor
    {
        Yellow = 1, Black, White, Silver
    }
    internal enum eNumberOfDoors
    {
        Two = 1, Three, Four, Five
    }
    private eColor Color { get; set; }
    private eNumberOfDoors NumberOfDoors { get; set; }

    protected Car(string i_LicenseId, string i_ModelName) : base( i_LicenseId, i_ModelName)
    {
        m_Tires = new Tires(k_NumberOfTires, "ModelName", k_MaxTireAirPressure);
    }

    internal override Dictionary<string, string> GetDetails()
    {
        Dictionary<string, string> details = GetGeneralVehicleDetails();
        
        details.Add("Color", Color.ToString());
        details.Add("Number of Doors", NumberOfDoors.ToString());
        return details;
    }

    internal override void AddSpecificDetails(string i_Color, string i_NumberOfDoors)
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
    
    internal override float GetMaxAirPressure()
    {
        return k_MaxTireAirPressure;
    }
}

internal class ElectricCar : Car
{
    private const float k_MaxBatterCapacityHours = 4.8f;
    internal ElectricCar(string i_LicenseId, string i_ModelName) : base( i_LicenseId, i_ModelName)
    {
        m_EnergySystem = new Battery(k_MaxBatterCapacityHours);
    }
}

internal class FuelCar : Car
{
    private const FuelSystem.eFuelType k_FuelType = FuelSystem.eFuelType.Octan95;
    private const float k_FuelTankCapacityLiter = 48f;
    
    internal FuelCar(string i_LicenseId, string i_ModelName) : base(i_LicenseId, i_ModelName)
    {
        m_EnergySystem = new FuelSystem(k_FuelType, k_FuelTankCapacityLiter);
    }
}