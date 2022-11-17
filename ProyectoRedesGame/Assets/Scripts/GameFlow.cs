using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class GameFlow : NetworkBehaviour
{
    public AudioSource bombSound;
    private const float bombTimer = 20.0f;
    private List<GameObject> players;
    private bool gameEnded = false;
    private float currentTime;

    void Start()
    {
        players = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));

        if (IsServer)
        {
            RandomBombGrab();
            ResetTimer();
        }
    }

    void Update()
    {
        if (gameEnded || !IsServer) return;

        currentTime -= Time.deltaTime;
        if (currentTime <= 0)
        {
            ExplodeBombClientRpc();
            if (players.Count == 1)
            {
                Debug.Log("Player " + players[0].name + " wins!");
                gameEnded = true;
            }
            else
            {
                RandomBombGrab();
                ResetTimer();
            }
        }
    }

    [ClientRpc]
    private void ExplodeBombClientRpc()
    {
        foreach (GameObject player in players)
        {
            if (player.GetComponent<GrabBomb>().HasBomb())
            {
                players.Remove(player);
                bombSound.Play();
                //Agregar animación de explosión
                Destroy(player);
                break;
            }
        }
    }


    private void RandomBombGrab()
    {
        players[Random.Range(0, players.Count)].GetComponent<GrabBomb>().GrabClientRpc();
    }

    private void ResetTimer()
    {
        currentTime = bombTimer;
    }
}
