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
    [SerializeField] private float output, previousMovement;

    public int numberOfMovements;

    private float fitness, fitnessSum, time;

    private NeuralNetwork[] networks;
    private NeuralNetwork currentNetwork;

    public TextMeshProUGUI populationTXT, mutationRateTXT, currentGenerationTXT, currentGenomeTXT, fitnessTXT, fitnessSumTXT;
    
    

    void Start()
    {   
        raycastManager = this.GetComponent<RaycastManager>();
        inputs = new float[] {15f, 15f, 15f, 15f, 15f, 15f, 15f, 15f, 15f, 15f, 15f, 15f, 15f, 15f, 15f, 15f, 15f, 15f, 15f};
        fitness = 0;
        fitnessSum = 0;
        previousMovement = -10f;
        numberOfMovements = 1;
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
        previousMovement = CalculateMovement();
        time += 1 * Time.deltaTime;
        CalculateFitness();
    }

    public float CalculateFitness()
    {
        fitness = time;
        return fitness;
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
        fitnessTXT.text = "Fitness: " + fitness;
        fitnessSumTXT.text = "Average fitness: " + CalculateAveragefitness();
    }
    public void Reset()
    {
        networks[currentGenome].SetFitness(fitness);
        fitnessSum += fitness;
        fitness = 0;
        time = 0;
        previousMovement = -10;
        numberOfMovements = 0;
        currentGenome++;
        if (currentGenome < population)
        {
            currentNetwork = networks[currentGenome];
        }else{
            currentGeneration++;
            fitnessSum = 0;
            currentGenome = 0;
            TrainPopulation();
        }
    }

    private void TrainPopulation()
    {
        networks = TournamentSelection();
        Crossover();
        Mutate();
    }

    private NeuralNetwork[] TournamentSelection()
    {
        NeuralNetwork[] newNetworks = new NeuralNetwork[population];
        for (int i = 0; i < population / 2; i++)
        {
            int[] selection = Utils.GetTwoRandomNetworks(networks);
            if (networks[selection[0]].GetFitness() >= networks[selection[1]].GetFitness())
            {
                newNetworks[i] = networks[selection[0]];
            }else{
                newNetworks[i] = networks[selection[1]];
            }
            networks[selection[0]] = null;
            networks[selection[1]] = null;
        }
        return newNetworks;
    }

    private void Mutate()
    {
        for (int i = 0; i < networks.Length; i++)
        {
            for (int j = 0; j < networks[i].GetLayers().Count; j++)
            {
                foreach (Neuron neuron in networks[i].GetLayers()[j].getNeurons())
                {
                    neuron.MutateWeights(mutationRate);
                }
            }
        }
    }

    public float CalculateMovement()
    {
        float movement;
        if (output >= -1 && output <= -0.3)
        {
            movement = -1f;
        }else if (output > -0.3 && output <= 0.3)
        {
            movement = 0f;
        }else{
            movement = 1f;
        }
        if (movement != previousMovement)
        {
            numberOfMovements++;
        }
        return movement;
    }

    private void Crossover()
    {
        for (int i = (population / 2); i < population; i++)
        {
            int[] selection = Utils.GetTwoRandomNumbers(0, (population / 2) - 1);
            NeuralNetwork newNet = Cross(networks[selection[0]], networks[selection[1]]);
            networks[i] = newNet;
        }
    }

    private NeuralNetwork Cross(NeuralNetwork nn1, NeuralNetwork nn2)
    {
        NeuralNetwork newNet = new NeuralNetwork();
        int numLayers = nn1.GetLayers().Count;
        for (int i = 0; i < numLayers; i++)
        {
            Layer layer1 = nn1.GetLayers()[i];
            Layer layer2 = nn2.GetLayers()[i];
            Layer newNetLayer = new Layer();

            int numNeurons = layer1.getNeurons().Count;
            for (int j = 0; j < numNeurons; j++)
            {
                Neuron neuron1 = layer1.getNeurons()[j];
                Neuron neuron2 = layer2.getNeurons()[j];
                Neuron newNetNeuron = new Neuron(neuron1.weights.Length);

                for (int k = 0; k < neuron1.weights.Length; k++)
                {
                    float randomValue = UnityEngine.Random.value;
                    if (randomValue < 0.5f)
                    {
                        newNetNeuron.weights[k] = neuron1.weights[k];
                    }
                    else
                    {
                        newNetNeuron.weights[k] = neuron2.weights[k];
                    }
                }
                newNetLayer.AddNeuron(newNetNeuron);
            }
            newNet.AddLayer(newNetLayer);
        }
        return newNet;
    }

    private float CalculateAveragefitness()
    {
        return fitnessSum / (currentGenome + 1);
    }

    private NeuralNetwork[] GeneratePopulation()
    {
        NeuralNetwork[] networkList = new NeuralNetwork[population];
        for (int i = 0; i < population; i++)
        {
            Debug.Log(i);
            networkList[i] = CreateRandomNetwork();
        }
        return networkList;
    }
    
    private NeuralNetwork CreateRandomNetwork()
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
