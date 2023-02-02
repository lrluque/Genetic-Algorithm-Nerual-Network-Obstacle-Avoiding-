using System.Collections.Generic;

public class NeuralNetwork
{

    private List<Layer> layers;
    public float score;

    public NeuralNetwork()
    {
        this.layers = new List<Layer>();
    }

    public void AddLayer(Layer layer)
    {
        this.layers.Add(layer);
    }
    
    public void SetScore(float score){
        this.score = score;
    }


}