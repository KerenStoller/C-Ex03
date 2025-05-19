namespace Ex03.GarageLogic;

public class Tires
{
    private class Tire
    {
        private string ModelName { get; set;}
        public float MaxAirPressure {get; private set;}
        private float CurrAirPressure {get; set;}

        public Tire(string i_ModelName, float i_MaxAirPressure)
        {
            ModelName =  i_ModelName;
            MaxAirPressure = i_MaxAirPressure;
            CurrAirPressure = i_MaxAirPressure;
        }
        
        public void AddAirPressure(float i_PressurToAdd)
        {
            CurrAirPressure = CurrAirPressure + i_PressurToAdd > MaxAirPressure ?
                MaxAirPressure : CurrAirPressure + i_PressurToAdd;  
        }

        public List<string> GetDetails()
        {
            List<string> details = new List<string>();
            details.Add(CurrAirPressure.ToString());
            details.Add(ModelName);
            return details;
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

    public void InflateTires()
    {
        foreach (Tire tire in m_Tires)
        {
            tire.AddAirPressure(tire.MaxAirPressure);
        }
    }

    public List<string> GetDetails()
    {
        List<string> details = new List<string>();
        
        foreach (Tire tire in m_Tires)
        {
            details.AddRange(tire.GetDetails());
        }
        
        return details;
    }
}