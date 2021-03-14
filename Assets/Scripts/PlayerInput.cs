using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.XR;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Player player;

    internal float horizontalInput;
    internal float verticalInput;

    internal bool isRightKeyPressed = false;
    internal bool isLeftKeyPressed = false;
    internal bool isJumpKeyPressed = false;
    internal bool isDashing = false;
    internal bool isFacingRight = true;

    private float dashTimer = 0;
    void Start()
    {
        dashTimer = player.dashTime;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        if(horizontalInput > 0)
        {
            isRightKeyPressed = true;
            isFacingRight = true;
        }else
        if (horizontalInput < 0)
        {
            isLeftKeyPressed = true;
            isFacingRight = false;
        }
        else
        {
            isRightKeyPressed = false;
            isLeftKeyPressed = false;
        }      
        Debug.Log("facing : " + isFacingRight);
        
        if (Input.GetKey(KeyCode.Space))
        {
            isJumpKeyPressed = true;
        }
        else
            isJumpKeyPressed = false;


        dashTimer += Time.deltaTime;
        if (Input.GetKey(KeyCode.L) && dashTimer >= player.dashTime)
        {
            dashTimer = 0;          
        }
        if (dashTimer <= player.dashTime)
        {
            isDashing = true;
        }
        else    
        if (dashTimer > player.dashTime)
        {
            isDashing = false;           
        }
            
        
            
    }
}
