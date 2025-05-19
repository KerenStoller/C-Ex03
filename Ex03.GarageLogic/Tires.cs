namespace Ex03.GarageLogic;

public class Tires
{
    private class Tire
    {
        private string ModelName { get; set;}
        private readonly float r_MaxAirPressure;
        private float CurrAirPressure {get; set;}

        public Tire(string i_ModelName, float i_MaxAirPressure)
        {
            ModelName =  i_ModelName;
            r_MaxAirPressure = i_MaxAirPressure;
            CurrAirPressure = i_MaxAirPressure;
        }

        public void setModelName(string i_ModelName)
        {
            ModelName = i_ModelName;
        }

        public void setCurrentAirPressure(float i_CurrentAirPressure)
        {
            CurrAirPressure = i_CurrentAirPressure;
        }
        
        public void AddAirPressure(float i_PressurToAdd)
        {
            CurrAirPressure = CurrAirPressure + i_PressurToAdd > r_MaxAirPressure ?
                r_MaxAirPressure : CurrAirPressure + i_PressurToAdd;  
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
    private readonly float r_MaxAirPressure;
    
    public Tires(int i_NumberOfTires, string i_ModelName, float i_MaxAirPressure)
    {
        NumberOfTires = i_NumberOfTires;
        m_Tires = new List<Tire>(NumberOfTires);
        r_MaxAirPressure = i_MaxAirPressure;
        
        for (int i = 0; i < NumberOfTires; i++)
        {
            m_Tires.Add(new Tire(i_ModelName, r_MaxAirPressure));
        }
    }

    private void setCurrentAirPressure(float i_CurrentAirPressure)
    {
        foreach (Tire tire in m_Tires)
        {
            tire.setCurrentAirPressure(i_CurrentAirPressure);
        }
    }
    
    private void setModelName(string i_ModelName)
    {
        foreach (Tire tire in m_Tires)
        {
            tire.setModelName(i_ModelName);
        }
    }

    public void AddDetails(string i_TireModelName, float i_currentAirPressure)
    {
        if (i_currentAirPressure <= r_MaxAirPressure)
        {
            setCurrentAirPressure(i_currentAirPressure);
            setModelName(i_TireModelName);
        }
        else
        {
            throw new ValueRangeException(r_MaxAirPressure, 0);
            //TODO: how to add message
        }
    }

    public void InflateTires()
    {
        foreach (Tire tire in m_Tires)
        {
            tire.AddAirPressure(r_MaxAirPressure);
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