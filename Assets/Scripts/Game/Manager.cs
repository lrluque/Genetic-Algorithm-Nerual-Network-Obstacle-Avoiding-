using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public Vector2 initialPosition;
    void Start()
    {
        initialPosition = this.transform.position;
    }

    public void ResetPosition()
    {
        this.transform.position = initialPosition;
    }
}
