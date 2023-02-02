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

    public List<NeuralNetwork> networks;

    void Start()
    {   
        raycastManager = this.GetComponent<RaycastManager>();
        
        inputs = new float[] {15f, 15f, 15f, 15f, 15f, 15f, 15f, 15f, 15f, 15f, 15f, 15f, 15f, 15f, 15f, 15f, 15f, 15f, 15f};

        hiddenLayer1 = new Layer();
        hiddenLayer2 = new Layer();
        outputLayer = new Layer();

        n11 = new Neuron();
        n12 = new Neuron();
        n13 = new Neuron();
        n14 = new Neuron();
        n15 = new Neuron();
        n16 = new Neuron();
        n21 = new Neuron();
        n22 = new Neuron();
        n23 = new Neuron();
        n31 = new Neuron();

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

        NeuralNetwork network = new NeuralNetwork();

        networks = new List<NeuralNetwork>();
        networks.Add(network);
        output = CalculateOutput(true);

    }
    // Update is called once per frame
    void Update()
    {
        UpdateInputs();
        output = CalculateOutput(false);
    }

    private float CalculateOutput(bool generateWeights){
        n11.AddInputs(Utils.SubArray(inputs, 0, 4), generateWeights);
        n12.AddInputs(Utils.SubArray(inputs, 3, 4), generateWeights);
        n13.AddInputs(Utils.SubArray(inputs, 6, 4), generateWeights);
        n14.AddInputs(Utils.SubArray(inputs, 9, 4), generateWeights);
        n15.AddInputs(Utils.SubArray(inputs, 12, 4), generateWeights);
        n16.AddInputs(Utils.SubArray(inputs, 15, 4), generateWeights);
        List<float> hiddenLayer1Outputs = hiddenLayer1.ComputeOutputs();
        n21.AddInputs(new float[] {hiddenLayer1Outputs[0], hiddenLayer1Outputs[1]}, generateWeights);
        n22.AddInputs(new float[] {hiddenLayer1Outputs[2], hiddenLayer1Outputs[3]}, generateWeights);
        n23.AddInputs(new float[] {hiddenLayer1Outputs[4], hiddenLayer1Outputs[5]}, generateWeights);
        List<float> hiddenLayer2Outputs = hiddenLayer2.ComputeOutputs();
        n31.AddInputs(new float[] {hiddenLayer2Outputs[0], hiddenLayer2Outputs[1], hiddenLayer2Outputs[2]}, generateWeights);
        List<float> outputLayerOutputs = outputLayer.ComputeOutputs();
        output = outputLayerOutputs[0];        
        return output;
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
