using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.XR;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Player player;
    SpriteRenderer spriteRenderer;
    [SerializeField]
    Transform groundCheck;

    Animator anim;

    bool isGrounded= false;
    float dashTimer = 0;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        dashTimer = player.dashTime;
    }

    
    void Update()
    {
        if (Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground")))
        {
            isGrounded = true;
           
        } else
        {
            isGrounded = false;
           
        }


        if (player.playerInput.isRightKeyPressed)
        {
            MovePlayerRight();
            if(isGrounded)
                anim.Play("Player_Run");
        }
        else
        if (player.playerInput.isLeftKeyPressed)
        {
            MovePlayerLeft();

            if (isGrounded) 
                anim.Play("Player_Run");
        }
        else
        {
            PlayerStand();
              
            anim.Play("Player_Idle");
        }


        if (player.playerInput.isJumpKeyPressed && isGrounded)
        {
            PlayerJump();
            anim.Play("Player_Jump");
        }
        
        if (player.playerInput.isDashing)
        {
            PlayerDash();
           
        }
    }

    private void PlayerJump()
    {       
        player.rb2D.velocity = new Vector2(player.rb2D.velocity.x, 4f);
        
    }

    void MovePlayerLeft()
    {
        player.rb2D.velocity = new Vector2(-player.moveSpeed, player.rb2D.velocity.y);
        //spriteRenderer.flipX = true;
        Flip();
    }
    void MovePlayerRight()
    {
        player.rb2D.velocity = new Vector2(player.moveSpeed, player.rb2D.velocity.y);
        //spriteRenderer.flipX = false;
        Flip();
    }
    void PlayerStand()
    {
        player.rb2D.velocity = new Vector2(0f, player.rb2D.velocity.y);
    }

    void PlayerDash()
    {
        if (player.playerInput.isFacingRight)
        {
            player.rb2D.velocity = new Vector2(player.dashSpeed, player.rb2D.velocity.y);
        }else
        if (!player.playerInput.isFacingRight)
        {
            player.rb2D.velocity = new Vector2(-player.dashSpeed, player.rb2D.velocity.y);
        }
        //anim.Play("Player_Dash");
    }
    void Flip()
    {
        if (player.playerInput.isFacingRight)
        {
            Vector3 theScale = transform.localScale;
            theScale.x = 1;
            transform.localScale = theScale;
        }
        else if(!player.playerInput.isFacingRight)
        {
            Vector3 theScale = transform.localScale;
            theScale.x = -1;
            transform.localScale = theScale;
        }
    }
}
