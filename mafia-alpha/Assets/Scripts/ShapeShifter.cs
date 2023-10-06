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
            StartCoroutine(ShiftShapeCoroutine(true));
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            StartCoroutine(ShiftShapeCoroutine(false));
        }
    }

    private IEnumerator ShiftShapeCoroutine(bool toRectangle)
    {
        if (toRectangle)
        {
            transform.localScale = new Vector3(originalScale.x * 0.5f, originalScale.y * 0.15f, originalScale.z);
            isRectangle = true;
        }
        else
        {
            float circleScale = 0.5f; 
            transform.localScale = new Vector3(originalScale.x * circleScale, originalScale.x * circleScale, originalScale.z);
            isCircle = true;
        }

        yield return new WaitForSeconds(5);

        transform.localScale = originalScale;
        isRectangle = false;
        isCircle = false;
    }
}
