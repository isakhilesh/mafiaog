using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Legend : MonoBehaviour
{
    public GameObject panel; // Reference to the UI Panel (should include the text as a child)

    private bool isInstructionsVisible = false;
    private int tabCount = 0;

    void Start()
    {
        panel.SetActive(false); // Start with the panel and text hidden
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            tabCount+=1;
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

    public int getTabCount()
    {
        return tabCount/2;
    }

    private void OnGUI(){
        if(isInstructionsVisible){

        Rect gameOverRect = new Rect(Screen.width / 2 - 100, Screen.height / 2 - 25, 400, 100);

        GUIStyle style = new GUIStyle(GUI.skin.label);
        style.fontSize = 16;
        style.alignment = TextAnchor.MiddleCenter;
        style.normal.textColor = Color.black;
        GUI.backgroundColor = Color.yellow;
        GUI.Label(gameOverRect,"Space Bar : Anti-Gravity \nDown Arrow : Shrink \nUp Arrow : Grow and Destroy \nM Key : Activate Magnet ", style);

        }
    }
}