using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Animator animator;
    public float moveSpeed = 5f; // Adjust the speed as needed
    private bool isFacingRight = true; // Variable to track the character's facing direction

    void Start()
    {
        animator = GetComponent<Animator>();

        // Initialize boolean parameters
        animator.SetBool("walk", false); // Initially set to false
        animator.SetBool("idle", true); // Initially set to true
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        if (moveHorizontal != 0 || moveVertical != 0)
        {
            // If arrow key is pressed, set the "Walk" boolean parameter to true and "Idle" to false
            animator.SetBool("idle", true);
            animator.SetBool("walk", false);

            // Move the character left or right based on the arrow key pressed
            Vector3 movement = new Vector3(moveHorizontal, 0.0f, 0.0f);
            transform.position += movement * Time.deltaTime * moveSpeed;

            // Flip character when moving left
            if (moveHorizontal < 0 && isFacingRight)
            {
                FlipCharacter();
            }
            else if (moveHorizontal > 0 && !isFacingRight)
            {
                FlipCharacter();
            }
        }
        else
        {
            // If no arrow key is pressed, set the "Idle" boolean parameter to true and "Walk" to false
            animator.SetBool("walk", true);
            animator.SetBool("idle", false);
        }
    }

    // Function to flip the character
    void FlipCharacter()
    {
        isFacingRight = !isFacingRight; // Update facing direction
        Vector3 scale = transform.localScale;
        scale.x *= -1; // Flip the character by changing the X scale
        transform.localScale = scale;
    }
}
