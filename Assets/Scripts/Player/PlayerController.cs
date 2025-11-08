using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController: MonoBehaviour
{
    private InputAction moveAction, jumpAction;

    [Header("Player Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 30f;
    [SerializeField] private float gravity = 20f;
    [SerializeField] private float groundDrag = 1f;
    [SerializeField] private float airDrag = 1f;

    [Header("Ground Check Settings")]
    [SerializeField] private float playerHeight = 1f;
    [SerializeField] private LayerMask groundLayer;
    private bool isGrounded = false;

    [Header("References")]
    [SerializeField] private Transform orientation;
    [SerializeField] private Rigidbody rb;

    private Vector3 moveDirection;
    private Vector2 moveVector;

    private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;
    private float jumpBufferCounter;
    private float jumpCooldownCounter;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");

        jumpAction.performed += OnJumpPerformed;
    }

    // Update is called once per frame
    void Update()
    {
        Inputs();
        SpeedCap();
        if (CheckIsGrounded() && jumpCooldownCounter <= 0f)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        jumpBufferCounter -= Time.deltaTime;
        jumpCooldownCounter -= Time.deltaTime;

        Jump();
    }

    // FixedUpdate is called at a fixed interval and is independent of frame rate
    void FixedUpdate()
    {
        MovePlayer();
        ApplyGravity();
        ApplyDrag();
    }

    // Handle jump input
    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        jumpBufferCounter = coyoteTime;
    }

    // Execute jump if conditions are met
    private void Jump()
    {
        if (coyoteTimeCounter > 0f && jumpBufferCounter > 0f && jumpCooldownCounter <= 0f)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            coyoteTimeCounter = 0f;
            jumpBufferCounter = 0f;
            jumpCooldownCounter = coyoteTime;
        }
    }

    // Handle player inputs
    private void Inputs()
    {
        moveVector = moveAction.ReadValue<Vector2>();
    }

    // Move the player based on input
    private void MovePlayer()
    {
        moveDirection = orientation.forward * moveVector.y + orientation.right * moveVector.x;

        rb.AddForce(moveDirection.normalized * moveSpeed * 20f, ForceMode.Force);
    }

    // Check if the player is grounded
    public bool CheckIsGrounded()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, groundLayer);
        return isGrounded;
    }

    // Apply drag based on whether the player is grounded
    private void ApplyDrag()
    {
        if (isGrounded)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x * groundDrag, rb.linearVelocity.y, rb.linearVelocity.z * groundDrag);
        }
        else
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x * airDrag, rb.linearVelocity.y, rb.linearVelocity.z * airDrag);
        }

        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);

        if (flatVel.magnitude <= 0.1f)
        {
            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
        }
    }

    private void ApplyGravity()
    {
        rb.AddForce(Vector3.down * gravity);
    }

    // Cap the player's speed to the defined move speed
    private void SpeedCap()
    {
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
        }
    }
}
