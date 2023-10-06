using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeShifter : MonoBehaviour
{
    private Vector3 originalScale;
    private bool isRectangle = false;

    private void Start()
    {
        originalScale = transform.localScale;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
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
    }
}
