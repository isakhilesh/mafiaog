using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Legend : MonoBehaviour
{
    public GameObject panel; // Reference to the UI Panel (should include the text as a child)

    private bool isInstructionsVisible = false;
    private int tabCount = 0;
    private GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        panel.SetActive(false); // Start with the panel and text hidden
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            tabCount += 1;
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

    private void OnGUI()
    {
        if (isInstructionsVisible)
        {
            float panelWidth = Screen.width * 0.4f;
            float panelHeight = Screen.height * 0.4f;

            Rect panelRect = new Rect(Screen.width / 2 - panelWidth / 2, Screen.height / 2 - panelHeight / 2, panelWidth, panelHeight);

            GUIStyle panelStyle = new GUIStyle(GUI.skin.box);
            panelStyle.normal.background = MakeTex(2, 2, new Color(1f, 1f, 1f, 0.8f)); // Transparent white background
            GUI.Box(panelRect, "", panelStyle);

            GUIStyle labelStyle = new GUIStyle(GUI.skin.label);
            labelStyle.fontSize = Mathf.RoundToInt(Screen.height * 0.035f); // Adjust font size based on screen height
            labelStyle.alignment = TextAnchor.UpperCenter;
            labelStyle.normal.textColor = Color.black;

            GUILayout.BeginArea(panelRect);
            GUILayout.Space(panelHeight * 0.05f);

            GUILayout.Label("Goal: Collect the Key and reach the door", labelStyle);
            GUILayout.Label("Space Bar: Anti-Gravity", labelStyle);
            GUILayout.Label("Down Arrow: Shrink", labelStyle);
            GUILayout.Label("Up Arrow: Grow and Destroy", labelStyle);
            GUILayout.Label("M Key: Activate Magnet", labelStyle);

            GUILayout.BeginHorizontal();

            // Main Menu Button
            if (GUILayout.Button("Main Menu", GUILayout.ExpandWidth(true)))
            {
                SceneManager.LoadScene("Menu");
                // Handle Main Menu button click
                // You can add code here to switch to the main menu scene or perform any desired action
            }

            // Play Again Button
            if (GUILayout.Button("Play Again", GUILayout.ExpandWidth(true)))
            {
                gameManager.RestartGame();
                // Handle Play Again button click
                // You can add code here to restart the game or perform any desired action
            }

            GUILayout.EndHorizontal();

            GUILayout.EndArea();
        }
    }
    private Texture2D MakeTex(int width, int height, Color color)
    {
        Color[] pix = new Color[width * height];
        for (int i = 0; i < pix.Length; ++i)
        {
            pix[i] = color;
        }

        Texture2D result = new Texture2D(width, height);
        result.SetPixels(pix);
        result.Apply();

        return result;
    }
}