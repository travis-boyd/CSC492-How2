using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class PlayerMovement : MonoBehaviour
{

    private Animator animator;
    public GameObject playerObject;

    [Header("Movement")]
    private float moveSpeed;
    public float walkSpeed;
    public float sprintSpeed;
    
    public float groundDrag;

    [Header("Jumping")]
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;
    /*public KeyCode forwardKey = KeyCode.W;
    public KeyCode backwardKey = KeyCode.S;
    public KeyCode rightKey = KeyCode.D;
    public KeyCode leftKey = KeyCode.A;*/


    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    public MovementState state;

    public bool isJumping;
    public bool isGrounded;
    public enum MovementState
    {
        walking,
        sprinting,
        idle,
        air
    }

    public float currYPos;
    public float lastYPos;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyToJump = true;

        //animation
        animator = playerObject.GetComponent<Animator>();
    }

    private void Update()
    {
        //Ground Check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.1f, whatIsGround);
        currYPos = transform.position.y; // Get current position after jump

        jumpAnimation();
        MyInput();
        StateHandler();
        Animation();
        


        //Handle drag
        if (state == MovementState.walking || state == MovementState.sprinting || state == MovementState.idle)
        {
            rb.drag = groundDrag;

        }
        else
        {
            rb.drag = 0;
        }
            
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        //When to jump
        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;
            lastYPos = transform.position.y; // Get last position before jump
            Jump();
            lastYPos = currYPos;
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void StateHandler()
    {



        if (grounded)
        {
            // Mode - Sprinting
            if ((Input.GetKey(forwardKey) || Input.GetKey(backwardKey) || Input.GetKey(rightKey) || Input.GetKey(leftKey)) && Input.GetKey(sprintKey))
            {
                state = MovementState.sprinting;
                moveSpeed = sprintSpeed;
            }
            // Mode - Walking
            else if (Input.GetKey(forwardKey) || Input.GetKey(backwardKey) || Input.GetKey(rightKey) || Input.GetKey(leftKey))
            {
                state = MovementState.walking;
                moveSpeed = walkSpeed;

            }
            // Mode - Idle
            else
            {
                state = MovementState.idle;

            }              
        }
        // Mode - Air
        else
        {
            state = MovementState.air;

        }
    }

    private void MovePlayer()
    {
        //Calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        //On ground
        if (grounded) rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        //In air
        else if (!grounded) rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            //Limiting velocity
            if (flatVel.magnitude > moveSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * moveSpeed;
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
            }
    }

    public float ySpeed;
    private void Jump()
    {
        //Reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        rb.AddForce(Vector3.down * 400f, ForceMode.Force);
    }
    private void ResetJump()
    {
        readyToJump = true;
    }

    private void Animation()
    {

        // check if player in walking state
        if (grounded && state == MovementState.walking)
        {
            animator.SetBool("isMoving", true); // set isMoving boolean to true 
            animator.SetBool("isSprinting", false); // set isSprinting is set false here to stop sprinting when going from sprinting to walking'

        }
        else if (grounded && state == MovementState.sprinting)
        {
            animator.SetBool("isSprinting", true); // set isSprinting boolean to true 
        }
        // set isMoving and isSprinting to false when in idle state
        else if(grounded && state == MovementState.idle)
        {
            animator.SetBool("isMoving", false);
            animator.SetBool("isSprinting", false);
        }
        // set all to false
        else
        {
            animator.SetBool("isMoving", false);
            animator.SetBool("isSprinting", false);
        }

    }

    public void jumpAnimation()
    {

    }

}
