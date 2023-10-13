using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeShifter : MonoBehaviour
{
    private Vector3 originalScale;
    private Rigidbody2D rb;
    private Vector2 originalGravity;

    private bool isRectangle = false;

    private float gravityChangeTime = 0.0f; // Variable to track time of gravity change
    public float gravityChangeDuration = 2.0f;
    public float moveSpeed = 5.0f; // Adjust this speed as needed

    private PlayerController playerController;

    private void Start()
    {
        originalScale = transform.localScale;
        rb = GetComponent<Rigidbody2D>();
        originalGravity = Physics2D.gravity;
        playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(ShiftShapeCoroutine(true));
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Change the gravity direction to the opposite direction
            Physics2D.gravity = -Physics2D.gravity;
            gravityChangeTime = Time.time; // Record the time of gravity change
        }

        // Check if it's time to return gravity to normal
        if (Time.time - gravityChangeTime >= gravityChangeDuration)
        {
            Physics2D.gravity = originalGravity; // Restore the original gravity
        }
        float horizontalInput = playerController.getHorizontalInput();
        Vector2 moveDirection = new Vector2(horizontalInput, 0);

        rb.AddForce(moveDirection * moveSpeed);

    }

    private IEnumerator ShiftShapeCoroutine(bool toRectangle)
    {
        if (toRectangle)
        {
            transform.localScale = new Vector3(3f, 0.75f, originalScale.z);
            isRectangle = true;
        }
       

        yield return new WaitForSeconds(5);

        transform.localScale = originalScale;
        isRectangle = false;

    }
}
