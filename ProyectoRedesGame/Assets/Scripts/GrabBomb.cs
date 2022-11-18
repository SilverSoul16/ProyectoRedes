using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class GrabBomb : NetworkBehaviour
{    
    public Transform holdSpot;
    public GameObject bomb;
    private GameObject itemHolding;
    private NetworkVariable<bool> hasBomb = new NetworkVariable<bool>(false);
    private NetworkVariable<bool> grabbing = new NetworkVariable<bool>(true);

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (IsHost && collision.gameObject.tag == "Player")
        {
            GrabBomb other = collision.gameObject.GetComponent<GrabBomb>();
            if (hasBomb.Value && other.grabbing.Value)
            {
                DropBomb();
                other.TakeBomb();
                StartCoroutine(DelayGrab());
            }
        }
    }

    public void TakeBomb()
    {
        if (IsHost)
        {
            hasBomb.Value = true;
            TakeBombClientRpc();
        }
    }

    [ClientRpc]
    private void TakeBombClientRpc()
    {
        itemHolding = Instantiate(bomb, holdSpot.position, Quaternion.identity);
        itemHolding.transform.position = holdSpot.position;
        itemHolding.transform.parent = transform;
        itemHolding.GetComponent<Rigidbody2D>().simulated = false;
    }

    public void DropBomb()
    {
        if (IsHost)
        {
            hasBomb.Value = false;
            DropBombClientRpc();
        }
    }

    [ClientRpc]
    private void DropBombClientRpc()
    {
        Destroy(itemHolding);
    }

    private IEnumerator DelayGrab()
    {
        if (IsHost)
        {
            grabbing.Value = false;
            yield return new WaitForSeconds(1);
            grabbing.Value = true;
        }
    }

    [ClientRpc]
    public void ExplodeClientRpc()
    {
        //Explosion aqui
        Destroy(gameObject);
    }

    public bool HasBomb()
    {
        return hasBomb.Value;
    }
}