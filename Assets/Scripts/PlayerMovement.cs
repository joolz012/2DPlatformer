using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour, IInteractable
{
    public static PlayerMovement Instance;
    public Animator playerAnim;
    private float moveSpeed;
    AudioSource source;
    public AudioClip[] clip;

    public Rigidbody2D rb;

    [Header("Jump")]
    public float jumpHeight;
    public Transform groundCheck;
    public LayerMask groundLayer;

    [Header("Climb")]
    public float climbSpeed;
    private bool canClimb = false;


    [Header("Move")]
    public float walkSpeed;
    private float horizontal, vertical; 
    private bool isLookingRight = true;

    [Header("Interact")]
    public LayerMask interactLayer;
    private bool interactOnce = false;


    [Header("Polish")]
    private bool canMove = true;
    // Start is called before the first frame update

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        source = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            moveSpeed = Mathf.Abs(horizontal);
            rb.velocity = new Vector2(horizontal * walkSpeed, rb.velocity.y);
           
            playerAnim.SetFloat("Speed", moveSpeed);

            if (IsGrounded() && rb.velocity.y > 2f && !canClimb)
            {
                playerAnim.SetBool("IsJumping", true);
            }
            else
            {
                playerAnim.SetBool("IsJumping", false);
            }

            if (!IsGrounded() && !canClimb)
            {
                playerAnim.SetBool("IsFalling", true);
            }
            else
            {
                playerAnim.SetBool("IsClimbing", false);
                playerAnim.SetBool("IsFalling", false);
            }
            if (canClimb && vertical != 0 && moveSpeed == 0)
            {
                playerAnim.SetBool("IsClimbing", true);
            }
        }

        if (!isLookingRight && horizontal > 0)
        {
            FlipPlayer();
        }
        else if (isLookingRight && horizontal < 0)
        {
            FlipPlayer();
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if(context.performed && IsGrounded())
        {
            source.PlayOneShot(clip[0]);
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.4f, groundLayer);
    }

    void FlipPlayer()
    {
        isLookingRight = !isLookingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    public void MovePlayer(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
    }

    public void Interact(InputAction.CallbackContext context)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.5f, interactLayer);
        foreach (Collider2D collider in colliders)
        {
            if (!interactOnce)
            {
                IInteractable interactable = collider.GetComponent<IInteractable>();
                if (interactable != null && context.performed)
                {
                    interactable.OnInteract();
                    break;
                }
            }
        }
    }
    public void Climb(InputAction.CallbackContext context)
    {
        if (context.performed && canClimb)
        {
            vertical = context.ReadValue<Vector2>().y;
            rb.velocity = new Vector2(rb.velocity.x, vertical * climbSpeed);
            rb.gravityScale = 0;
            Debug.Log("Climbing");
        }

        if (context.canceled && canClimb)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            Debug.Log("Stop");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ladder"))
        {
            canClimb = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ladder"))
        {
            canClimb = false;
            rb.gravityScale = 2;
        }
    }

    private void DamagePlayer()
    {
        canMove = false;
        rb.velocity = new Vector2(0, 6);
        playerAnim.SetTrigger("Knockback");
        Invoke(nameof(EnableMovement), 0.5f);
    }

    public void EnableMovement()
    {
        canMove = true;
    }

    public void DisablePlayer()
    {
        rb.velocity = Vector2.zero;
        PlayerInput playerInput = GetComponent<PlayerInput>();
        if (playerInput != null)
            playerInput.enabled = false;
        Debug.Log("Disable");
    }

    public void OnInteract()
    {
        DamagePlayer();
    }
}
