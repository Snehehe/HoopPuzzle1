using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 2f;          // Speed of the enemy movement
    public Transform groundCheck;         // Transform to detect the edge of the platform
    public LayerMask groundLayer;         // Layer for the ground objects
    public float groundCheckRadius = 0.2f; // Radius for checking if the ground is still ahead

    private Rigidbody2D rb;               // Reference to the Rigidbody2D component
    private bool isMovingRight = true;    // Determines the movement direction

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component attached to the enemy
    }

    void Update()
    {
        Move();
        CheckForEdgeOrObstacle();
    }

    void Move()
    {
        // Move the enemy in the current direction
        if (isMovingRight)
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y); // Move to the right
            transform.localScale = new Vector3(1, 1, 1); // Ensure the enemy faces right
        }
        else
        {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y); // Move to the left
            transform.localScale = new Vector3(-1, 1, 1); // Flip the enemy to face left
        }
    }

    void CheckForEdgeOrObstacle()
    {
        // Check if the ground check position overlaps with the ground layer
        bool isGroundAhead = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // If there is no ground ahead, reverse the direction
        if (!isGroundAhead)
        {
            isMovingRight = !isMovingRight; // Flip the movement direction
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Draw a sphere in the editor to visualize the ground check area
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
