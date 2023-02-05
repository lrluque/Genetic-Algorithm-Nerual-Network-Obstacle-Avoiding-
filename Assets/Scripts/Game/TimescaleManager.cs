using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimescaleManager : MonoBehaviour
{   

    public float time;
    // Start is called before the first frame update
    void Start()
    {
      Time.timeScale = time;  
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = time;
    }
}
