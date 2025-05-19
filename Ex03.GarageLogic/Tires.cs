namespace Ex03.GarageLogic;

public class Tires
{
    private class Tire
    {
        private string ModelName { get; set;}
        private float MaxAirPressure {get; set;}
        public float CurrAirPressure {get; private set;}
        public void AddAirPressure(float i_PressurToAdd)
        {
            CurrAirPressure = CurrAirPressure + i_PressurToAdd > MaxAirPressure ?
                MaxAirPressure : CurrAirPressure + i_PressurToAdd;  
        }

        public Tire(string i_ModelName, float i_MaxAirPressure)
        {
            ModelName =  i_ModelName;
            MaxAirPressure = i_MaxAirPressure;
            CurrAirPressure = i_MaxAirPressure;
        }
    }
    private List<Tire> m_Tires;
    public int NumberOfTires {get; protected set;}
    
    public Tires(int i_NumberOfTires, string i_ModelName, float i_MaxAirPressure)
    {
        NumberOfTires = i_NumberOfTires;
        m_Tires = new List<Tire>(NumberOfTires);
        
        for (int i = 0; i < NumberOfTires; i++)
        {
            m_Tires.Add(new Tire(i_ModelName, i_MaxAirPressure));
        }
    }
}