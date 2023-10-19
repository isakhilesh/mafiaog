using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject player; // Reference to the player GameObject.

    private bool gameIsOver = false;

    private Vector3 initialPlayerPosition; // Store the initial player position.

    private void Start()
    {
        initialPlayerPosition = player.transform.position;
    }

    private void Update()
    {
        if (gameIsOver)
        {
            // Check for user input to restart the game
            if (Input.GetKeyDown(KeyCode.R))
            {
                RestartGame();
            }
        }
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        gameIsOver = true;
    }

    public void RestartGame()
    {
        Time.timeScale = 1;

        // Reset the player's position
        player.transform.position = initialPlayerPosition;

        // Reload the current scene to restart the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
