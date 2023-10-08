using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Adjust the speed as needed.
    private Rigidbody2D rb;
    private GameManager gameManager;

    private bool isGameWin = false;
    private bool isPlayerCrashPillar = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();

    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        // Calculate the movement vector.
        Vector2 movement = new Vector2(horizontalInput, 0f) * moveSpeed;

        // Apply the movement to the Rigidbody.
        rb.velocity = new Vector2(movement.x, rb.velocity.y);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision is with an enemy object.
        if (collision.gameObject.CompareTag("Pillar") && !isPlayerCrashPillar)
        {
            isPlayerCrashPillar = true;
            gameManager.GameOver();
        }
        if (collision.gameObject.CompareTag("enemy"))
        {
            // Destroy the enemy object when the player touches it.
            Destroy(collision.gameObject);
        }
        if(collision.gameObject.CompareTag("Door") && !isGameWin)
        {
            isGameWin = true;
            gameManager.GameOver();

        }
    }

	private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Door")) 
        {
            isGameWin = true;
            Time.timeScale = 0; // Pause the game by setting the time scale to 0.
        }
        if (collision.CompareTag("Pillar"))
        {
            isPlayerCrashPillar = true;
            Time.timeScale = 0; // Pause the game by setting the time scale to 0.
        }
    }


    private void OnGUI()
    {
        if (isGameWin)
        {
            // Define the position and size of the "Game Over" message
            Rect gameOverRect = new Rect(Screen.width / 2 - 100, Screen.height / 2 - 25, 200, 50);

            // Style for the message (font size, alignment, etc.)
            GUIStyle style = new GUIStyle(GUI.skin.label);
            style.fontSize = 24;
            style.alignment = TextAnchor.MiddleCenter;

            // Display the "Game Over" message
            GUI.Label(gameOverRect, "YOU WIN!!", style);

            if (gameManager != null)
            {
                // Define the position and size of the "Play Again" button
                Rect buttonRect = new Rect(Screen.width / 2 - 50, Screen.height / 2 + 25, 100, 30);

                // Check if the "Play Again" button is clicked
                if (GUI.Button(buttonRect, "Play Again"))
                {
                    gameManager.RestartGame();
                }
            }
            else
            {
                // Handle the case where gameManager is null (e.g., display an error message).
                GUI.Label(new Rect(10, 10, 200, 30), "Error: GameManager not found.");
            }
        }

        if (isPlayerCrashPillar)
        {
            // Define the position and size of the "Game Over" message
            Rect gameOverRect = new Rect(Screen.width / 2 - 100, Screen.height / 2 - 25, 200, 50);

            // Style for the message (font size, alignment, etc.)
            GUIStyle style = new GUIStyle(GUI.skin.label);
            style.fontSize = 14;
            style.alignment = TextAnchor.MiddleCenter;

            // Display the "Game Over" message
            GUI.Label(gameOverRect, "You crashed into the Pillar, YOU LOSE!!", style);

            if (gameManager != null)
            {
                // Define the position and size of the "Play Again" button
                Rect buttonRect = new Rect(Screen.width / 2 - 50, Screen.height / 2 + 25, 100, 30);

                // Check if the "Play Again" button is clicked
                if (GUI.Button(buttonRect, "Play Again"))
                {
                    gameManager.RestartGame();
                }
            }
            else
            {
                // Handle the case where gameManager is null (e.g., display an error message).
                GUI.Label(new Rect(10, 10, 200, 30), "Error: GameManager not found.");
            }
        }
    }



}
