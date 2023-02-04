using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NeuralNetworkManager : MonoBehaviour
{
    
    private RaycastManager raycastManager;

    private NeuralNetwork[] genomes;

    [SerializeField] private int numberOfGenerations;
    [SerializeField] private int population;
    [SerializeField] private float mutationRate;
    [SerializeField] private int currentGeneration;

    [SerializeField] private int currentGenome;

    [SerializeField] private float[] inputs;
    [SerializeField] private float output;

    private float score, scoreSum;

    private NeuralNetwork[] networks;
    private NeuralNetwork currentNetwork;

    public TextMeshProUGUI populationTXT, mutationRateTXT, currentGenerationTXT, currentGenomeTXT, scoreTXT, scoreSumTXT;
    
    

    void Start()
    {   
        raycastManager = this.GetComponent<RaycastManager>();
        inputs = new float[] {15f, 15f, 15f, 15f, 15f, 15f, 15f, 15f, 15f, 15f, 15f, 15f, 15f, 15f, 15f, 15f, 15f, 15f, 15f};
        score = 0;
        scoreSum = 0;
        networks = GeneratePopulation();
        Debug.Log(networks.Length);
        currentGenome = 0;
        currentGeneration = 1;
        populationTXT.text = "Population: " + population;
        mutationRateTXT.text = "Mutation rate: " + mutationRate;
        currentNetwork = networks[currentGenome];
    }
    // Update is called once per frame
    void Update()
    {
        UpdateText();
        UpdateInputs();
        output = CalculateOutput();
        score += 1 * Time.deltaTime;
    }

    private float CalculateOutput(){
        return currentNetwork.ComputeOutput(inputs)[0];
    }

    public float GetOutput()
    {
        return output;
    }


    private void UpdateInputs()
    {
        for (int i = 0; i < inputs.Length; i++){
            inputs[i] = raycastManager.distances[i];
        }
    }

    private void UpdateText()
    {
        currentGenerationTXT.text = "GENERATION " + currentGeneration;
        currentGenomeTXT.text = "GENOME " + (currentGenome + 1);
        scoreTXT.text = "Score: " + score;
        scoreSumTXT.text = "Average score: " + CalculateAverageScore();
    }
    public void Reset()
    {
        networks[currentGenome].SetScore(score);
        scoreSum += score;
        score = 0;
        currentGenome++;
        if (currentGenome < population)
        {
            currentNetwork = networks[currentGenome];
        }else{
            currentGeneration++;
            scoreSum = 0;
            currentGenome = 0;
            networks = GeneratePopulation();
        }
    }

    public float CalculateAverageScore()
    {
        return scoreSum / (currentGenome + 1);
    }

    public NeuralNetwork[] GeneratePopulation()
    {
        NeuralNetwork[] networkList = new NeuralNetwork[population];
        for (int i = 0; i < population; i++)
        {
            Debug.Log(i);
            networkList[i] = CreateRandomNetwork();
        }
        return networkList;
    }
    
    public NeuralNetwork CreateRandomNetwork()
    {
        Layer hiddenLayer1 = new Layer();
        Layer hiddenLayer2 = new Layer();
        Layer outputLayer = new Layer();

        Neuron n11 = new Neuron(19);
        Neuron n12 = new Neuron(19);
        Neuron n13 = new Neuron(19);
        Neuron n14 = new Neuron(19);
        Neuron n15 = new Neuron(19);
        Neuron n16 = new Neuron(19);
        Neuron n21 = new Neuron(6);
        Neuron n22 = new Neuron(6);
        Neuron n23 = new Neuron(6);
        Neuron n31 = new Neuron(3);

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

        network.AddLayer(hiddenLayer1);
        network.AddLayer(hiddenLayer2);
        network.AddLayer(outputLayer);
        return network;
    }

}
