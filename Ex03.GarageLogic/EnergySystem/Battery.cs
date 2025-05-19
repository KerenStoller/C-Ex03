namespace Ex03.GarageLogic.EnergySystem;

public class Battery
{
    private float m_TimeLeft;
    private float m_MaxTime;
    
    public void Charge(float i_TimeToCharge)
    {
        m_TimeLeft = m_TimeLeft + i_TimeToCharge > m_MaxTime ? m_MaxTime : m_TimeLeft + i_TimeToCharge;
    }
}