using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{   

    public GameObject player;
    public Player playerScript;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Border")
        {
            //Destroy(this.gameObject);
        }else if (collision.tag == "Player")
        {
            playerScript.restartPlayer();
        }
    }

}
