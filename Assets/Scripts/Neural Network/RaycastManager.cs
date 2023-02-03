using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class RaycastManager : MonoBehaviour
{

    private LayerMask obstacleLayer;
    public float[] distances;

    void Start()
    {
        distances = new float[19];
    }

    // Update is called once per frame
    void Update()
    {
        CheckForObstacle();
    }

    void CheckForObstacle()
    {
        RaycastHit2D hit;
        Vector2 rayDirection;
        int arrayCounter = 0;
        for (float i = -90f; i <= 90f; i += 10f) {
            rayDirection = new Vector2(Mathf.Cos(i * Mathf.Deg2Rad), Mathf.Sin(i * Mathf.Deg2Rad));
            hit = Physics2D.Raycast(transform.position, rayDirection, 15f);
            if(hit.collider != null && hit.collider.CompareTag("Obstacle")) {
                Debug.DrawRay(transform.position, rayDirection * 15f, Color.red);
                distances[arrayCounter] = hit.distance;
            } else {
                Debug.DrawRay(transform.position, rayDirection * 15f, Color.green);
                distances[arrayCounter] = 16f;
            }
            arrayCounter++;
        }
    }

}
