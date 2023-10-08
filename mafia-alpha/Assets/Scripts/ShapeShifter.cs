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
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(ShiftShapeCoroutine(true));
        }


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
