using System.Collections.Generic;

public class Layer
{
    private List<Neuron> neurons;

    public Layer()
    {
        this.neurons = new List<Neuron>();

    }

    public void AddNeuron(Neuron neuron)
    {
        neurons.Add(neuron);
    }
        

    public List<Neuron> getNeurons()
    {
        return this.neurons;
    }

    public float[] ComputeOutput(float[] inputs)
    {
        float[] output = new float[neurons.Count];
        for (int i = 0; i < neurons.Count; i++)
        {
            output[i] = neurons[i].ComputeOutput(inputs);
        }
        return output;
    }


}
