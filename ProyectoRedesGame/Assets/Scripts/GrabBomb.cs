using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class GrabBomb : NetworkBehaviour
{    
    public Transform holdSpot;
    public GameObject bomb;
    private bool hasBomb;
    private GameObject itemHolding;
    private bool grabbing = true;

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject other = collision.gameObject;
        if (other.tag == "Player" && hasBomb && other.GetComponent<GrabBomb>().grabbing) // Player layer
        {
            StartCoroutine(PassBomb(other));
        }
    }

    public void Grab()
    {
        hasBomb = true;
        itemHolding = Instantiate(bomb, holdSpot.position, Quaternion.identity);
        itemHolding.transform.position = holdSpot.position;
        itemHolding.transform.parent = transform;
        itemHolding.GetComponent<Rigidbody2D>().simulated = false;
        hasBomb = true;
    }

    private IEnumerator PassBomb(GameObject otherPlayer)
    {
        grabbing = false;
        Destroy(itemHolding);
        hasBomb = false;
        otherPlayer.GetComponent<GrabBomb>().Grab();
        yield return new WaitForSeconds(2f);
        grabbing = true;
    }

    public bool HasBomb()
    {
        return hasBomb;
    }
}