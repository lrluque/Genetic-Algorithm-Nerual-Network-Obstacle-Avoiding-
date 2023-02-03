using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeuralNetworkManager : MonoBehaviour
{
    
    public RaycastManager raycastManager;

    public float[] inputs;
    public float output;

    private Neuron n11, n12, n13, n14, n15, n16, n21, n22, n23, n31;

    private Layer hiddenLayer1, hiddenLayer2, outputLayer;

    public NeuralNetwork network;

    void Start()
    {   
        raycastManager = this.GetComponent<RaycastManager>();
        
        inputs = new float[] {15f, 15f, 15f, 15f, 15f, 15f, 15f, 15f, 15f, 15f, 15f, 15f, 15f, 15f, 15f, 15f, 15f, 15f, 15f};

        hiddenLayer1 = new Layer();
        hiddenLayer2 = new Layer();
        outputLayer = new Layer();

        n11 = new Neuron(19);
        n12 = new Neuron(19);
        n13 = new Neuron(19);
        n14 = new Neuron(19);
        n15 = new Neuron(19);
        n16 = new Neuron(19);
        n21 = new Neuron(6);
        n22 = new Neuron(6);
        n23 = new Neuron(6);
        n31 = new Neuron(3);

        hiddenLayer1.AddNeuron(n11);
        hiddenLayer1.AddNeuron(n12);
        hiddenLayer1.AddNeuron(n13);
        hiddenLayer1.AddNeuron(n14);
        hiddenLayer1.AddNeuron(n15);
        hiddenLayer1.AddNeuron(n16);
        hiddenLayer2.AddNeuron(n21);
        hiddenLayer2.AddNeuron(n22);
        hiddenLayer2.AddNeuron(n23);
        outputLayer.AddNeuron(n31);

        network = new NeuralNetwork();

        network.AddLayer(hiddenLayer1);
        network.AddLayer(hiddenLayer2);
        network.AddLayer(outputLayer);

    }
    // Update is called once per frame
    void Update()
    {
        UpdateInputs();
        output = CalculateOutput();
    }

    private float CalculateOutput(){
        return network.ComputeOutput(inputs)[0];
    }

    public float GetOutput(){
        return output;
    }


    private void UpdateInputs(){
        for (int i = 0; i < inputs.Length; i++){
            inputs[i] = raycastManager.distances[i];
        }
    }

    public void Reset(){
        Start();
    }
    
}
