using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUpdate : MonoBehaviour
{
    public Image progressBar;
    private ShapeShifter shapeShifter;
    private float currentTime = 0.0f;
    private bool isDecreasing = false;
    private float fillAmount = 1.0f;
    private float cnt = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        progressBar.fillAmount = 1.0f;
    }

    // Update is called once per frame
    public void Update()
    {
        if (isDecreasing)
        {
            currentTime += Time.deltaTime;
            if (currentTime > cnt)
            {
                fillAmount = fillAmount - 0.05f;
                cnt += 0.5f;
            }
            // Calculate the fill amount as a ratio of remaining time to decrease duration
             
            // Ensure that fillAmount doesn't go below 0
            fillAmount = Mathf.Clamp01(fillAmount);

            // Update the fill amount of the time bar
            progressBar.fillAmount = fillAmount;

            // Check if the time bar is fully decreased
            if (fillAmount <= 0.0f)
            {
                // Time bar is fully decreased, stop decreasing
                isDecreasing = false;
                currentTime = 0.0f;
            }
        }
    }

    public float getTimeUsed()
    {
        return currentTime;
    }

    public void StartDecreasing(bool decreasing)
    {
        isDecreasing = decreasing;
    }

    public bool IsDecreasing()
    {
        if(isDecreasing == false ||  fillAmount <= 0.0f)
        {
            return false;
        }
        return true;
    }
}
