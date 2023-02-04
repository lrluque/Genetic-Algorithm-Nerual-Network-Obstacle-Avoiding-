using System.Collections.Generic;

public class NeuralNetwork
{

    private List<Layer> layers;
    private float score;

    public NeuralNetwork()
    {
        this.layers = new List<Layer>();
    }

    public void AddLayer(Layer layer)
    {
        this.layers.Add(layer);
    }

    public float[] ComputeOutput(float[] inputs)
    {
        float[] latestLayerOutput = layers[0].ComputeOutput(inputs);
        for (int i = 1; i < layers.Count; i++)
        {
            latestLayerOutput = layers[i].ComputeOutput(latestLayerOutput);
        }
        return latestLayerOutput;
    }
    
    public void SetScore(float score){
        this.score = score;
    }

}