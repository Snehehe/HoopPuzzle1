using UnityEngine;

public class PlayerMovement2DPlatformer : MonoBehaviour
{
    public float moveSpeed = 5f;        // Speed of the player movement
    public float jumpForce = 10f;       // Force applied when jumping
    public Transform groundCheck;       // Reference to the ground check transform
    public float groundCheckRadius = 0.2f; // Radius for ground checking
    public LayerMask groundLayer;       // Layer of ground objects

    public AudioClip footstepSound;     // Assign footstep sound in the Inspector
    private AudioSource audioSource;    // Reference to the AudioSource component

    private Rigidbody2D rb;             // Reference to the Rigidbody2D component
    private bool isGrounded;            // Is the player on the ground?
    private bool canDoubleJump;         // Can the player double jump?
    private Animator animator;          // Reference to the Animator component

    void Start()
    {
        // Initialize components
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>(); // Get the AudioSource component
    }

    void FixedUpdate()
    {
        CheckGrounded(); // Check if the player is grounded
        MovePlayer();     // Call the movement method
    }

    void CheckGrounded()
    {
        // Check if the ground check position overlaps with the ground layer
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Allow double jump if grounded
        if (isGrounded)
        {
            canDoubleJump = true; // Reset double jump ability if grounded
        }
    }

    void MovePlayer()
    {
        float moveInput = Input.GetAxis("Horizontal"); // Get input for left/right movement
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y); // Set player velocity

        // Flip the character sprite based on movement direction
        if (moveInput > 0.1f) // Moving right
        {
            transform.localScale = new Vector3(1, 1, 1); // Face right
            animator.SetBool("Run", true); // Set run animation
        }
        else if (moveInput < -0.1f) // Moving left
        {
            transform.localScale = new Vector3(-1, 1, 1); // Face left
            animator.SetBool("Run", true); // Set run animation
        }
        else // Not moving
        {
            animator.SetBool("Run", false); // Set idle animation
        }

        // Jumping logic
        if (Input.GetButtonDown("Jump") && (isGrounded || canDoubleJump))
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse); // Apply jump force
            if (!isGrounded)
            {
                canDoubleJump = false; // Disable double jump if already jumped
            }
        }

        // Play footstep sounds
        if (moveInput != 0 && isGrounded && !audioSource.isPlaying)
        {
            audioSource.PlayOneShot(footstepSound); // Play footstep sound
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the player collides with an object tagged as "Enemy"
        if (collision.gameObject.CompareTag("Enemy"))
        {
            KillPlayer(); // Call the method to handle player death
        }
    }

    private void KillPlayer()
    {
        // Logic to handle player death, such as restarting the level, showing a death screen, etc.
        Debug.Log("Player has died!");
        // You could add code here to handle respawning or resetting the game state
    }

    private void OnDrawGizmosSelected()
    {
        // Draw a sphere in the editor to visualize the ground check area
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
