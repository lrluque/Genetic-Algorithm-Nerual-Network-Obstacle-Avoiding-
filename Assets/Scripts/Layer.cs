using System.Collections.Generic;

public class Layer
{
    private List<Neuron> neurons;
    private List<float> outputs;

    public Layer()
    {
        this.neurons = new List<Neuron>();
        this.outputs = new List<float>();
    }

    public void AddNeuron(Neuron neuron)
    {
        this.neurons.Add(neuron);
    }

    public List<Neuron> getNeurons()
    {
        return this.neurons;
    }

    public List<float> ComputeOutputs()
    {
        this.outputs.Clear();
        foreach (Neuron neuron in neurons)
        {
            outputs.Add(neuron.ComputeOutput());
        }
        return this.outputs;
    }


}
