using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

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
    public KeyCode forwardKey = KeyCode.W;
    public KeyCode backwardKey = KeyCode.S;
    public KeyCode rightKey = KeyCode.D;
    public KeyCode leftKey = KeyCode.A;


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
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        MyInput();
        SpeedControl();
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

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    bool keepMomentum;
    private void StateHandler()
    {

        // Mode - Sprinting


        // Mode - Walking / Idle
        if (grounded)
        {
            if ((Input.GetKey(forwardKey) || Input.GetKey(backwardKey) || Input.GetKey(rightKey) || Input.GetKey(leftKey)) && Input.GetKey(sprintKey))
            {
                state = MovementState.sprinting;
                moveSpeed = sprintSpeed;
            }
            else if (Input.GetKey(forwardKey) || Input.GetKey(backwardKey) || Input.GetKey(rightKey) || Input.GetKey(leftKey))
            {
                state = MovementState.walking;
                moveSpeed = walkSpeed;

            }
            // if not set player in idle state
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

    private void Jump()
    {
        //Reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
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

        // check if player is jumping
        if (grounded && Input.GetKey(jumpKey))
        {
            animator.SetBool("isJumping", true);
            animator.SetBool("isGrounded", false);
            isJumping = true;
        } else if(!grounded && isJumping )
        {
            animator.SetBool("isFalling", true);
        } else
        {
            animator.SetBool("isGrounded", true);
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", false);
        }

    }

}
