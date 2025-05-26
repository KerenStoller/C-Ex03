namespace Ex03.GarageLogic;

internal class Tires
{
    private class Tire
    {
        private string ModelName { get; set;}
        private readonly float r_MaxAirPressure;
        private float CurrAirPressure {get; set;}
        internal const float k_MinPressure = 0f;
        internal const float k_MaxPressure = 100f;

        internal Tire(string i_ModelName, float i_MaxAirPressure)
        {
            ModelName =  i_ModelName;
            r_MaxAirPressure = i_MaxAirPressure;
            CurrAirPressure = i_MaxAirPressure;
        }

        internal void SetModelName(string i_ModelName)
        {
            ModelName = i_ModelName;
        }

        internal void SetCurrentAirPressure(float i_CurrentAirPressure)
        {
            CurrAirPressure = i_CurrentAirPressure;
        }
        
        internal void AddAirPressure(float i_PressurToAdd)
        {
            CurrAirPressure = CurrAirPressure + i_PressurToAdd > r_MaxAirPressure ?
                r_MaxAirPressure : CurrAirPressure + i_PressurToAdd;  
        }

        internal string GetDetails()
        {
            return $"{ModelName} - {CurrAirPressure} psi";
        }
    }
    
    private List<Tire> m_Tires;
    internal int NumberOfTires {get;}
    private readonly float r_MaxAirPressure;
    
    internal Tires(int i_NumberOfTires, string i_ModelName, float i_MaxAirPressure)
    {
        NumberOfTires = i_NumberOfTires;
        m_Tires = new List<Tire>(NumberOfTires);
        r_MaxAirPressure = i_MaxAirPressure;
        
        for (int i = 0; i < NumberOfTires; i++)
        {
            m_Tires.Add(new Tire(i_ModelName, r_MaxAirPressure));
        }
    }

    internal void AddDetailsForAllTires(string i_TireModelName, float i_CurrentAirPressure)
    {
        if (i_CurrentAirPressure <= r_MaxAirPressure)
        {
            foreach(Tire tire in m_Tires)
            {
                tire.SetCurrentAirPressure(i_CurrentAirPressure);
                tire.SetModelName(i_TireModelName);
            }
        }
        else
        {
            throw new ValueRangeException(r_MaxAirPressure, Tire.k_MinPressure, "Pressure outside of range");
        }
    }

    internal void AddDetailsForTires(List<KeyValuePair<string, float>> i_TireModelNamesAndPressures)
    {
        if(i_TireModelNamesAndPressures.Count != NumberOfTires)
        {
            throw new ArgumentException("Wrong number of Tires");
        }
        for(int i = 0; i < NumberOfTires; i++)
        {
            KeyValuePair<string, float> tireInfo = i_TireModelNamesAndPressures[i];
            string tireModelName = tireInfo.Key;
            float currentAirPressure = tireInfo.Value;
            
            if(currentAirPressure <= r_MaxAirPressure)
            {
                m_Tires[i/2].SetCurrentAirPressure(currentAirPressure);
                m_Tires[i/2].SetModelName(tireModelName);
            }
            else
            {
                throw new ValueRangeException(r_MaxAirPressure, Tire.k_MinPressure, "Pressure outside of range");
            }
        }
    }

    internal void InflateTires()
    {
        foreach (Tire tire in m_Tires)
        {
            tire.AddAirPressure(r_MaxAirPressure);
        }
    }

    internal Dictionary<string, string> GetDetails()
    {
        Dictionary<string, string> details = new Dictionary<string, string>();
        
        int tireIndex = 1;
        
        foreach (Tire tire in m_Tires)
        {
            details.Add($"Tire {tireIndex++} ", tire.GetDetails());
        }
        return details;
    }
    
    internal void SetPressureOfSingleTire(int i_TireIndex, float i_PressurePercentage)
    {
        if(i_TireIndex < 0 || i_TireIndex >= m_Tires.Count)
        {
            throw new ArgumentOutOfRangeException(nameof(i_TireIndex));
        }
        if(i_PressurePercentage < Tire.k_MinPressure || i_PressurePercentage > Tire.k_MaxPressure)
        {
            throw new ArgumentOutOfRangeException(nameof(i_PressurePercentage));
        }
        float newPressure = (i_PressurePercentage / Tire.k_MaxPressure) * r_MaxAirPressure;
        m_Tires[i_TireIndex].SetCurrentAirPressure(newPressure);
    }
}