using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float playerSpeed;
    public float score;

    public LayerMask obstacleLayer;
    
    private Rigidbody2D rb;
    
    private NeuralNetworkManager networkManager;

    private Vector2 initialPos;
    private Vector2 playerDirection;

    public GameObject networkObject;
    public GameObject playerPosition;

    // Start is called before the first frame update
    void Start()
    {
        initialPos = this.transform.position;
        rb = GetComponent<Rigidbody2D>();
        networkManager = networkObject.GetComponent<NeuralNetworkManager>();
    }

    // Update is called once per frame
    void Update()
    {
        float directionY = networkManager.CalculateMovement();
        playerDirection = new Vector2(0, directionY).normalized;
        initialPos = playerPosition.transform.position;
    }

    void FixedUpdate() {
        rb.velocity = new Vector2(0, playerDirection.y * playerSpeed);
    }

    public void restartPlayer(){
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        foreach(GameObject obj in obstacles) {
            if(obj.transform.name == "Obstacle(Clone)"){
                Destroy(obj);
            }
        }
        score = 0;
        this.transform.position = initialPos;
        networkManager.Reset();
    }

}