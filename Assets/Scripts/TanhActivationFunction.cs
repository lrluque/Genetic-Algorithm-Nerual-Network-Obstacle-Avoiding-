
using System;

public class TanhActivationFunction : ActivationFunction
{
    float ActivationFunction.CalculateAF(float x)
    {
        return (float)System.Math.Tanh(x);
    }
}