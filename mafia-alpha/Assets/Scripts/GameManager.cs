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
            
        }
    }

    public void GameOver()
    {
        gameIsOver = true;
    }

    public bool isGameOver()
    {
        return gameIsOver;
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
