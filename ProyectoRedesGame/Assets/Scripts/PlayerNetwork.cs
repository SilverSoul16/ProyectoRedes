using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerNetwork : NetworkBehaviour
{   
    private float horizontal;
    private float speed = 8f;

    [SerializeField] private Rigidbody2D rb;

    void Update()
    {
        if (!IsOwner) return; //only move the player object that is owned
        horizontal = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }
}
