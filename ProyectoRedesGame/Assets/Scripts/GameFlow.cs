using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class GameFlow : NetworkBehaviour
{
    public AudioSource bombSound;
    private const float bombTimer = 10.0f;
    private List<GameObject> players;
    private bool gameEnded = false;
    private NetworkVariable<float> currentTime = new NetworkVariable<float>(bombTimer);

    void Start()
    {
        if (IsHost)
        {
            players = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));
            RandomBombGrab();
            RestartTimer();
        }
    }

    void Update()
    {
        if (gameEnded || !IsHost) return;

        currentTime.Value -= Time.deltaTime;
        if (currentTime.Value <= 0)
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
                RestartTimer();
            }
        }
    }

    private void ExplodeBomb()
    {
        foreach (GameObject player in players)
        {
            if (player.GetComponent<GrabBomb>().HasBomb())
            {
                PlayExplosionSoundClientRpc();
                players.Remove(player);
                player.GetComponent<GrabBomb>().ExplodeClientRpc();
                break;
            }
        }
    }

    [ClientRpc]
    private void PlayExplosionSoundClientRpc()
    {
        bombSound.Play();
    }

    private void RandomBombGrab()
    {
        players[Random.Range(0, players.Count)].GetComponent<GrabBomb>().TakeBomb();
    }

    private void RestartTimer()
    {
        currentTime.Value = bombTimer;
    }
}
