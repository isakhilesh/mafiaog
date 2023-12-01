using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeShifter_2 : MonoBehaviour
{
    private Vector3 originalScale;
    private Rigidbody2D rb;
    private Vector2 originalGravity;

    private bool isRectangle = false;

    private int rectCount = 0;

    private float gravityChangeTime = 0.0f; // Variable to track time of gravity change
    public float gravityChangeDuration = 2.0f;
    public float moveSpeed = 5.0f; // Adjust this speed as needed


    private PlayerController_2 playerController;
    private ProgressBarUpdate progressBarUpdate;

    private void Start()
    {
        Physics2D.gravity = new Vector2(0.0f, -9.81f);
        originalScale = transform.localScale;
        rb = GetComponent<Rigidbody2D>();
        originalGravity = Physics2D.gravity;
        playerController = GetComponent<PlayerController_2>();
        progressBarUpdate = GetComponent<ProgressBarUpdate>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            rectCount += 1;
            StartCoroutine(ShiftShapeCoroutine(true));
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Change the gravity direction to the opposite direction
            Physics2D.gravity = -Physics2D.gravity;
            gravityChangeTime = Time.time; // Record the time of gravity change
            if (Physics2D.gravity.y < 0.0f)
            {

                progressBarUpdate.StartDecreasing(false);
            }
            else
            {
                progressBarUpdate.StartDecreasing(true);
            }
        }

        // Check if it's time to return gravity to normal
        if (progressBarUpdate.IsDecreasing() == false)
        {
            Physics2D.gravity = originalGravity; // Restore the original gravity
        }

        float horizontalInput = playerController.getHorizontalInput();
        Vector2 moveDirection = new Vector2(horizontalInput, 0);

        rb.AddForce(moveDirection * moveSpeed);


    }

    public int getRectcount()
    {
        return rectCount;
    }
    private IEnumerator ShiftShapeCoroutine(bool toRectangle)
    {
        if (toRectangle)
        {
            transform.localScale = new Vector3(0.3875977f, 0.1020664f, originalScale.z);
            isRectangle = true;
        }


        yield return new WaitForSeconds(3);

        transform.localScale = originalScale;
        isRectangle = false;

    }
}
