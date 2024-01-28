using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerPlayerMovement : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    public float speed = 25f;
    public float jumpSpeed = 8f;
    public float gravity = 20f;
    private Vector3 moveDirection = Vector3.zero;
    private float mouseHorizontal;
    public float sensitivity = 5f;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }    
    private void Update()
    {
        
        // ground check
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);            
            moveDirection *= speed;

            //Jump
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }               
        }
        mouseHorizontal = Input.GetAxis("Mouse X") * sensitivity;
        
        if (mouseHorizontal != 0)
        {
            //Horizontal mouse mouvement
            transform.eulerAngles += new Vector3(0, mouseHorizontal, 0);
        }
        
        //Applying gravity to the controller
        moveDirection.y -= gravity * Time.deltaTime;
        //Making the character move
        controller.Move(moveDirection * Time.deltaTime);
    }
}
