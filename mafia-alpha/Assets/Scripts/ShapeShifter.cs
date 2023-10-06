using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeShifter : MonoBehaviour
{
    private Vector3 originalScale;
    private bool isRectangle = false;
    private bool isCircle = false;

    private void Start()
    {
        originalScale = transform.localScale;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            isRectangle = !isRectangle;
            if (isRectangle)
            {
                // Change the square to a rectangle by modifying the scale
                transform.localScale = new Vector3(originalScale.x * 0.5f, originalScale.y * 0.15f, originalScale.z);
            }
            else
            {
                // Change it back to a square
                transform.localScale = originalScale;
            }
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            // Turn the square into a circle by setting both X and Y scales to the same value
            isCircle = !isCircle;
            float circleScale = 0.5f; // Adjust this value as needed

           

            if (isRectangle)
            {
                // Change the square to a circle by modifying the scale
                transform.localScale = new Vector3(originalScale.x * circleScale, originalScale.x * circleScale, originalScale.z);
            }
            else
            {
                // Change it back to a square
                transform.localScale = originalScale;
            }
        }
    }
}
