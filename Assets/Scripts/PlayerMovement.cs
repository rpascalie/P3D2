using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    private BoxCollider coll;
    private SpriteRenderer sprite;

    [SerializeField] private LayerMask jumpableGround;

    [Header("Horizontal Movement")]
    [SerializeField] private float dirX = 0f;
    [SerializeField] private float dirZ = 0f;
    [SerializeField] private float moveSpeed = 7f;  

    [Header("Vertical Movement")]
    [SerializeField] private float jumpForce = 14f;
    [SerializeField] private float jumpDelay = 0.2f;
    private float jumpTimer;

    [SerializeField] private bool canDoubleJump = true;
    private bool jumpButtonHeld = false;
    [SerializeField] private float couterJumpForce = 40f;
    [SerializeField] private float maxFallVelocity = -20f;

    [SerializeField] private float coyoteTime = 0.1f;
    private float coyoteTimer;

    [Header("Collision")]
    [SerializeField] private bool onGround = false;    
    [SerializeField] private float groundCheckOffset = .1f;    
    [SerializeField] private bool playerDead = false;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<BoxCollider>();
        sprite = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        onGround = IsGrounded(); // check if grounded       
        dirX = Input.GetAxis("Horizontal"); // get horizontal movement input                
        dirZ = Input.GetAxis("Vertical"); // get horizontal movement input   

        if (onGround)
        {
            canDoubleJump = true;      // restore double jump when on ground            
            coyoteTimer = Time.time + coyoteTime;  // set timer for coyote time
        }     
        

        if (Input.GetButtonDown("Jump")) // register jump potentially before landing
        {
            jumpTimer = Time.time + jumpDelay; // timer for jump buffer
            jumpButtonHeld = true;
        }

        if (Input.GetButtonUp("Jump"))
        {
            jumpButtonHeld = false;
        }    
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector3(
            dirX * moveSpeed,
            Mathf.Clamp(rb.velocity.y, maxFallVelocity, float.MaxValue), 
            dirZ * moveSpeed); // make character move (snappy), impose max fall speed
       

        if (jumpTimer > Time.time && coyoteTimer > Time.time) // Jump with buffer and coyote time
        {
            //jumpSoundEffect.Play();
            Jump();
        }

        if (!jumpButtonHeld && rb.velocity.y > 0)  // Make jump height depends on button press
        {
            rb.AddForce(Vector3.down * couterJumpForce);
        }       

        if (jumpTimer > Time.time && canDoubleJump)
        // Double jump
        {
            //doubleJumpSoundEffect.Play();
            Jump();
            canDoubleJump = false;
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);  //keep horizontal velocity
        rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse); //add impulse for snappy jump
        jumpTimer = 0;                                               //reset jump buffer timer
    }

    private bool IsGrounded()
    {
        return Physics.BoxCast(coll.bounds.center, coll.bounds.size * .9f, Vector3.down, Quaternion.identity, groundCheckOffset, jumpableGround);
    }
}

