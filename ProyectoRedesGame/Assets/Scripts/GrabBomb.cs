using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabBomb : MonoBehaviour
{    
    public Transform holdSpot;    
    public GameObject item;
    
    private GameObject itemHolding;

    void Start() {
        itemHolding = Instantiate(item, holdSpot.position, Quaternion.identity);
        itemHolding.transform.position = holdSpot.position;
        itemHolding.transform.parent = transform;
        Debug.Log(itemHolding.transform.parent);
        Debug.Log(transform);
        if(itemHolding.GetComponent<Rigidbody2D>())
            itemHolding.GetComponent<Rigidbody2D>().simulated = false;
    }
}