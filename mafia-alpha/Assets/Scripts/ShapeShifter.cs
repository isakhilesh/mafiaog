using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeShifter : MonoBehaviour
{
    private Vector3 originalScale;
    private bool isRectangle = false;


    private void Start()
    {
        Physics2D.gravity = new Vector2(0.0f, -9.81f);
        originalScale = transform.localScale;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(ShiftShapeCoroutine(true));
        }

<<<<<<< Updated upstream
=======
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
        
>>>>>>> Stashed changes

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
