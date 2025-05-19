namespace Ex03.GarageLogic;

public class Tire
{
    public string ModelName {get; set;}
    public float CurrAirPressure {get; set;}
    public float m_MaxAirPerssure {get; set;}

    public void AddAirPressure(float i_PressurToAdd)
    {
        CurrAirPressure = CurrAirPressure + i_PressurToAdd > m_MaxAirPerssure ?
            m_MaxAirPerssure : CurrAirPressure + i_PressurToAdd;  
    }
}