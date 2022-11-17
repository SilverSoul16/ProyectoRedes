using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class GameFlow : NetworkBehaviour
{
    public AudioSource bombSound;
    private float bombTimer = 20.0f;
    private List<GameObject> players;
    private bool gameEnded = false;
    void Start()
    {
        players = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));
        RandomBombGrab();
    }

    void Update()
    {
        if (gameEnded) return;
        bombTimer -= Time.deltaTime;
        if (bombTimer <= 0)
        {
            ExplodeBomb();
            if (players.Count == 1)
            {
                Debug.Log("Player " + players[0].name + " wins!");
                gameEnded = true;
            }
            else
            {
                RandomBombGrab();
                bombTimer = 20.0f;
            }
        }
    }

    private void ExplodeBomb()
    {
        foreach (GameObject player in players)
        {
            if (player.GetComponent<GrabBomb>().HasBomb())
            {
                bombSound.Play();
                players.Remove(player);
                Destroy(player);
                break;
            }
        }
    }

    private void RandomBombGrab()
    {
        Debug.Log(players.Count);
        players[Random.Range(0, players.Count)].GetComponent<GrabBomb>().Grab();
    }
}
