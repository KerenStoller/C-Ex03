namespace Ex03.GarageLogic;

using System;

public class ValueRangeException : Exception
{
    float maxValue, MinValue;

    public ValueRangeException(float i_MaxValue, float i_MinValue)
    {
        this.maxValue = i_MaxValue;
        this.MinValue = i_MinValue;
    }
    
    public ValueRangeException()
    {
    }

    public ValueRangeException(string message)
        : base(message)
    {
    }

    public ValueRangeException(string message, Exception inner)
        : base(message, inner)
    {
    }
}