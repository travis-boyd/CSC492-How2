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
    public Transform orientation;
    float horizontalInput;
    float verticalInput;
    Vector3 moveDirection;
    Rigidbody rb;
    public MovementState state;

    [Header("Movement Speed")]
    private float moveSpeed;
    public float walkSpeed;
    public float sprintSpeed;
    public float groundDrag;

    [Header("Ground Check")]
    public float playerHeight = 5;
    public LayerMask whatIsGround;
    bool grounded;

    [Header("Falling")]
    public float inAirTimer;
    public float leapingVelocity = 0.25f;
    public float fallingVelocity = 20;
    public float rayCastHeightOffSet = 0.5f;
    public float maxDistance = 0.5f;

    [Header("Jumping")]
    //public float jumpForce;
    public float jumpCooldown = 0.25f;
    public float airMultiplier = 0.15f;
    bool readyToJump;

    [Header("Jump Speeds")]
    public float jumpingHeight = 5;
    public float gravityIntensity = -15;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode forwardKey = KeyCode.W;
    public KeyCode backwardKey = KeyCode.S;
    public KeyCode rightKey = KeyCode.D;
    public KeyCode leftKey = KeyCode.A;

    public enum MovementState
    {
        walking,
        sprinting,
        jumping,
        air,
        landing, 
        idle
    }

    private void Awake()
    {
        //animatorManager = GetComponent<AnimatorManager>();
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        //grounded = true;
        readyToJump = true;

        //animation
        animator = playerObject.GetComponent<Animator>();
    }

    private void Update()
    {
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

    public void FixedUpdate()
    {
        MovePlayer();
        HandleFallingAndLanding();
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

    private void StateHandler()
    {
        if (grounded)
        {
            // Mode - Sprinting
            if (Input.GetKey(sprintKey))
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
            else if(Input.GetKey(jumpKey))
            {
                state = MovementState.jumping;
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

    private void HandleFallingAndLanding()
    {
        RaycastHit hit;
        Vector3 rayCastOrigin = transform.position;
        rayCastOrigin.y = rayCastOrigin.y + rayCastHeightOffSet;

        //Jumping in a direction
        if (!grounded && Input.GetKey(forwardKey) || Input.GetKey(backwardKey) || Input.GetKey(rightKey) || Input.GetKey(leftKey))
        {
            inAirTimer = inAirTimer + Time.deltaTime;
            rb.AddForce(transform.forward * leapingVelocity);
            rb.AddForce(Vector3.down * fallingVelocity * inAirTimer);
        }
        //Jumping in place
        else if (!grounded)
        {
            inAirTimer = inAirTimer + Time.deltaTime;
            rb.AddForce(Vector3.down * fallingVelocity * inAirTimer);
        }

        //Ground Check
        if (Physics.SphereCast(rayCastOrigin, 0.2f, Vector3.down, out hit, maxDistance, whatIsGround))
        {
            inAirTimer = 0;
            grounded = true;
        }
        else
        {
            grounded = false;
        }
    }

    private void Jump()
    {
        
        float jumpingVelocity = Mathf.Sqrt(-2 * gravityIntensity * jumpingHeight);
        Vector3 playerVelocity = moveDirection;
        playerVelocity.y = jumpingVelocity;
        rb.velocity = playerVelocity;
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
        else if (grounded && state == MovementState.idle)
        {
            animator.SetBool("isMoving", false);
            animator.SetBool("isSprinting", false);
            animator.SetBool("isJumping", false);
        }
        else if(!grounded && state == MovementState.jumping)
        {
            animator.SetBool("isJumping", false); //set isJumping boolean to true
        }
        // set all to false
        else
        {
            animator.SetBool("isMoving", false);
            animator.SetBool("isSprinting", false);
            animator.SetBool("isJumping", false);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Objective_1"))
        {
            Debug.Log("Player touched Objective_1");
            ObjectiveManager objectiveManager = FindObjectOfType<ObjectiveManager>();
            /*
            if (objectiveManager != null) {Debug.Log("not null!");}
            else {Debug.Log("null :(");}
            */

            objectiveManager.CompleteObjective(1);
        }
    }

}
