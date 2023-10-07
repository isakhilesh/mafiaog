using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Adjust the speed as needed.
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        // Calculate the movement vector.
        Vector2 movement = new Vector2(horizontalInput, 0f) * moveSpeed;

        // Apply the movement to the Rigidbody.
        rb.velocity = new Vector2(movement.x, rb.velocity.y);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision is with an enemy object.
        if (collision.gameObject.CompareTag("enemy"))
        {
            // Destroy the enemy object when the player touches it.
            Destroy(collision.gameObject);
        }
    }
}
