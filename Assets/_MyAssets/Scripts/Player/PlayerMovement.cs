using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 7f;
    Vector3 limitedVelocity;

    public float playerHeight = 2f;
    public LayerMask groundMask;
    bool isGrounded;
    public float groundDrag = 5f;

    public float jumpForce = 20f;
    public float jumpCooldown = 0.25f;
    public float airMultiplier = 0.2f;
    bool canJump = true;

    bool canInput = true;

    [SerializeField] Transform orient;
    float horizontalInput;
    float verticalInput;
    Vector3 moveDir;
    Rigidbody rb;

    Animator animator;

    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();    
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void PlayerInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        if(Input.GetKeyDown(jumpKey) && canJump && isGrounded)
        {
            canJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown); //invokes using cooldown as delay!!!
        }
        if(Input.GetKeyDown(sprintKey))
        {
            moveSpeed *= 2;
        }
        if(Input.GetKeyUp(sprintKey))
        {
            moveSpeed /= 2;
        }
    }

    public void SetCanInput(bool set)
    {
        canInput = set;
    }

    private void MovePlayer()
    {
        moveDir = orient.forward * verticalInput + orient.right * horizontalInput;

        if(isGrounded)
        {
            rb.AddForce(moveDir.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        else if(!isGrounded)
        {
            rb.AddForce(moveDir.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }
    }

    private void SpeedClamper()
    {
        Vector3 flatVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        if(flatVelocity.magnitude > moveSpeed)
        { 
            limitedVelocity = flatVelocity.normalized * moveSpeed; //calculate what max velocity WOULD be
            rb.velocity = new Vector3(limitedVelocity.x, rb.velocity.y, limitedVelocity.z); //apply it
        }
    }

    private void Jump()
    {
        animator.SetBool("jump", true);
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        animator.SetBool("jump", false);
        canJump = true;
    }

    private void UpdateAnimation()
    {
        float forwardSpeed = Vector3.Magnitude(rb.velocity);
        animator.SetFloat("speed", forwardSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, groundMask);
        if(canInput)
        {
            PlayerInput();
        }

        if(isGrounded)
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
        if (canInput)
        {
            MovePlayer();
            SpeedClamper();
            UpdateAnimation();
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            ButtonManager.Instance.QuitGame();
        }

    }
}
