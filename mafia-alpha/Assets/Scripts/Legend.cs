using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Legend : MonoBehaviour
{
    public GameObject panel; // Reference to the UI Panel (should include the text as a child)

    private bool isInstructionsVisible = false;

    void Start()
    {
        panel.SetActive(false); // Start with the panel and text hidden
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isInstructionsVisible = !isInstructionsVisible;

            if (isInstructionsVisible)
            {
                panel.SetActive(true); // Show the UI panel and text
                Time.timeScale = 0.0f; // Pause the game
            }
            else
            {
                panel.SetActive(false); // Hide the UI panel and text
                Time.timeScale = 1.0f; // Unpause the game
            }
        }
    }
}
