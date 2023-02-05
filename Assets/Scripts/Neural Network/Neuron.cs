using System.Collections.Generic;
using System;

using Random = UnityEngine.Random;

public class Neuron
{
   public float[] weights;
   private float output;
   private float bias;
   private ActivationFunction activationFunction;


    public Neuron(int numInputs)
    {
        weights = new float[numInputs];
        for (int i = 0; i < numInputs; i++)
        {
            weights[i] = Random.Range(-1f, 1f); 
        }
        bias = Random.Range(-1f, 1f);
        this.activationFunction = new TanhActivationFunction();
    }


    public float ComputeOutput(float[] inputs)
    {
        float sum = bias;
        for (int i = 0; i < inputs.Length; i++)
        {
            sum += inputs[i] * weights[i];
        }
        output = activationFunction.CalculateAF(sum);
        return output;
    }

    public void MutateWeights(float mutationRate)
    {
        for (int i = 0; i < weights.Length; i++)
        {
            float random = Random.Range(0f, 1f);
            if (random <= mutationRate)
            {
                weights[i] = Random.Range(-1f, 1f);
            }
        }
    }

}
