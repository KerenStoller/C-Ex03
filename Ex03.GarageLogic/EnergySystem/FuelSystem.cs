namespace Ex03.GarageLogic.EnergySystem;

public class FuelSystem
{
    private enum e_FuelType
    {
        Soler, Octan95, Octan96, Octan98
    }

    private e_FuelType m_FuelType;
    private float m_CurrFuelAmount;
    private float m_MaxFuelCapacity;

    public void AddFuel(float i_FuelToAdd)
    {
        m_CurrFuelAmount = m_CurrFuelAmount + i_FuelToAdd > m_MaxFuelCapacity ?
            m_MaxFuelCapacity : m_CurrFuelAmount + i_FuelToAdd;
    }
}