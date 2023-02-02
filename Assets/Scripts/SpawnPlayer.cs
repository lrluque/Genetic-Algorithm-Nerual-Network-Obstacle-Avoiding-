using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{

    public GameObject playerPrefab;
    public GameObject playerPosition;
    public int numberOfPlayers;

    private void Start()
    {
        for (int i = 0; i < numberOfPlayers; i++)
        {
            GameObject player = Instantiate(playerPrefab, playerPosition.transform.position, Quaternion.identity);
            player.tag = "Player";
        }
    }
}
