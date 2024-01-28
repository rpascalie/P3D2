using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerMovement2D : MonoBehaviour
{
    private BoxCollider coll;  
    [SerializeField] private LayerMask jumpableGround;

    [Header("Horizontal Movement")]
    private float dirX;
    //private float dirZ;
    [SerializeField] private float moveSpeed = 7f;
    private Vector3 displacement;
    [SerializeField] private Rect allowedArea = new Rect(-4.5f, -4.5f, 9.9f, 8.93f);
    private Vector3 newPosition;

    [Header("Vertical Movement")]    
    private float jumpDelay = 0.2f;
    private float jumpTimer;
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float mass = 10;
    private Vector3 velocity;
    private float gravity = 9.81f;
    private Vector3 gravityF;

    private bool jumpButtonHeld = false;
    //private float coyoteTime = 0.1f;
    //private float coyoteTimer;

    private float jumpDurationTimer;
    [SerializeField] private float jumpDuration = 0.1f;

    [Header("Collision")]
    [SerializeField] private bool onGround = false;
    [SerializeField] private float groundCheckOffset = .1f;
    //[SerializeField] private bool playerDead = false;

    private void Start()
    {
        coll = GetComponent<BoxCollider>();
        transform.localPosition = new Vector3(0f, 0.1f, -4.5f);
    }

    private void Update()
    {
        onGround = IsGrounded();
        dirX = Input.GetAxis("Horizontal"); // get horizontal movement input   
        //dirZ = Input.GetAxis("Vertical"); // get vertical movement input   
        velocity = new Vector3(dirX, 0f, 0f) * moveSpeed;

        if (onGround)
        { 
            //coyoteTimer = Time.time + coyoteTime;  // set timer for coyote time
        }

        if (Input.GetButtonDown("Jump")) // register jump potentially before landing
        {
            //jumpTimer = Time.time + jumpDelay; // timer for jump buffer
            jumpButtonHeld = true;         
        }

        if (jumpButtonHeld && onGround) 
        {
            jumpDurationTimer = Time.time + jumpDuration;
        }

        if (Input.GetButtonUp("Jump") || jumpDurationTimer < Time.time)
        {
            jumpButtonHeld = false;
        }
    }

    private void FixedUpdate()
    {
        displacement = velocity * Time.deltaTime;
        if (onGround)
        {
            gravityF = Vector3.zero;
        }
        else
        {
            gravityF = new Vector3(0f, 0f, 9.81f) * Mathf.Pow(Time.deltaTime, 2);
        }
        newPosition = transform.localPosition + displacement - gravityF * mass;        

        if (jumpButtonHeld && jumpDurationTimer > Time.time)
        {
            Jump();
        }

        if (!allowedArea.Contains(new Vector2(newPosition.x, newPosition.z)))
        {
            newPosition.x =
                Mathf.Clamp(newPosition.x, allowedArea.xMin, allowedArea.xMax);
            newPosition.z =
                Mathf.Clamp(newPosition.z, allowedArea.yMin, allowedArea.yMax);
        }
        transform.localPosition = newPosition;
                 
    }
    private void Jump()
    {
        newPosition.z += Mathf.Sqrt(2f * gravity * jumpHeight) * Time.deltaTime;
        //jumpTimer = 0;   //reset jump buffer timer        
    }

    private bool IsGrounded()
    {
        return Physics.BoxCast(coll.bounds.center, coll.bounds.size * .3f, Vector3.down, Quaternion.identity, groundCheckOffset, jumpableGround);
    }
}