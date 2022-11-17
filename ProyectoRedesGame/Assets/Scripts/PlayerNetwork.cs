using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerNetwork : NetworkBehaviour
{   
    public CharacterController2D controller;
    public Animator animator;    
    bool jump = false;
    private float horizontal;
    private float runSpeed = 40f;

    void Update()
    {
        if (!IsOwner) { //only move the player object that is owned
            horizontal = Input.GetAxisRaw("Horizontal") * runSpeed;
            
            animator.SetFloat("Speed", Mathf.Abs(horizontal));

            if (Input.GetButtonDown("Jump")){
                jump = true;
            }
        }
    }

    private void FixedUpdate()
    {
        controller.Move(horizontal * Time.fixedDeltaTime, false, jump);
        jump = false;
    }
}
