using System.Collections.Generic;
using System;

using Random = UnityEngine.Random;

public class Neuron
{
   private List<float> weights;
   private float bias;
   private float output;
   private ActivationFunction activationFunction;
   private List<float> inputs;


    public Neuron()
    {
        this.inputs = new List<float>();
        this.weights = new List<float>();
        this.bias = 1f;
        inputs.Add(bias);
        this.weights.Add(GenerateRandom());
        this.activationFunction = new TanhActivationFunction();
    }

    public void AddInput(float input, float weight)
    {
        this.inputs.Add(input);
        this.weights.Add(weight);
    }

    public void AddInputs(float[] inputs, bool generateWeights)
    {
        this.inputs.Clear();
        for(int inputIndex = 0; inputIndex < inputs.Length; inputIndex++){
            this.inputs.Add(inputs[inputIndex]);
            this.weights.Add(GenerateRandom());
        }
    }



    public float ComputeOutput()
    {
        this.output = 0;
        int indexCounter = 0;
        foreach (float input in inputs)
        {
            output += (input * weights[indexCounter]);
            indexCounter++;
        }
        output = this.activationFunction.CalculateAF(output);
        return output;
    }

    private float GenerateRandom()
    {
        return Random.Range(-1f, 1f);
    }


}
