namespace Ex03.GarageLogic;

using System;

public class ValueRangeException : Exception
{
    float m_MaxValue, m_MinValue;

    public ValueRangeException(float i_MaxValue, float i_MinValue, string i_Message) : base(i_Message)
    {
        this.m_MaxValue = i_MaxValue;
        this.m_MinValue = i_MinValue;
    }
    
    public ValueRangeException(string i_Message, Exception i_Inner)
        : base(i_Message, i_Inner)
    {
    }
}