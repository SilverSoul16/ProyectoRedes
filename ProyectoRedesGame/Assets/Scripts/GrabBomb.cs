using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabBomb : MonoBehaviour
{    
    public Transform holdSpot;
    public LayerMask pickUpMask;
    public Vector3 Direction { set; get; } //direction of player, where its facing
    private GameObject itemHolding;
    public GameObject bomb;

    void Start() {
        itemHolding = Instantiate(bomb, holdSpot.position, Quaternion.identity);
        itemHolding.transform.position = holdSpot.position;
        itemHolding.transform.parent = transform;
        Debug.Log(itemHolding.transform.parent);
        Debug.Log(transform);
        if(itemHolding.GetComponent<Rigidbody2D>())
            itemHolding.GetComponent<Rigidbody2D>().simulated = false;
    }

    /*void Update() {
        if(Input.GetKeyDown(KeyCode.E)){
            if (itemHolding){
                itemHolding.transform.parent = null;
                if(itemHolding.GetComponent<Rigidbody2D>())
                        itemHolding.GetComponent<Rigidbody2D>().simulated = true;
                itemHolding = null;
            } else {
                Collider2D pickUp = Physics2D.OverlapCircle(transform.position + Direction, .8f, pickUpMask);
                if (pickUp) {
                    itemHolding = pickUp.gameObject;
                    itemHolding.transform.position = holdSpot.position;
                    itemHolding.transform.parent = transform;
                    Debug.Log(itemHolding.transform.parent);
                    Debug.Log(transform);
                    if(itemHolding.GetComponent<Rigidbody2D>())
                        itemHolding.GetComponent<Rigidbody2D>().simulated = false;
                }
            }
        }
    }*/
}