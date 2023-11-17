using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerController_3 : MonoBehaviour
{
    public float moveSpeed = 5f; // Adjust the speed as needed.
    private Rigidbody2D rb;
    private GameManager gameManager;

    private bool isGameWin = false;
    private bool isPlayerCrashPillar = false;
    private bool isPlayerKilledByEnemy = false;
    private bool hasKey = false;

    private GameObject keyObject;
    private bool isPullingKey = false;
    private float pullForce = 10f;
    private float pullDistance = 10f;
    private float horizontalInput;
    private CircleSprite circleSprite;

    private bool gameOver = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        keyObject = GameObject.FindGameObjectWithTag("Key");
        circleSprite = GetComponent<CircleSprite>(); // Assign the CircleSprite component here.
        Time.timeScale = 1; // Pause the game by setting the time scale to 0.

    }
    public bool getHasKey()
    {
        return hasKey;
    }

    public float getHorizontalInput()
    {
        return horizontalInput;
    }
    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        Vector2 movement = new Vector2(horizontalInput, 0f) * moveSpeed;

        rb.velocity = new Vector2(movement.x, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.P) && keyObject != null && Vector2.Distance(transform.position, keyObject.transform.position) <= pullDistance)
        {
            isPullingKey = true;
        }

        // If the player is pulling the key, apply the pulling force.
        if (isPullingKey)
        {
            PullKey();
        }
    }

    private void PullKey()
    {
        if (keyObject != null)
        {
            Vector2 direction = (transform.position - keyObject.transform.position).normalized;
            keyObject.GetComponent<Rigidbody2D>().AddForce(direction * pullForce);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision is with an enemy object.
        if (collision.gameObject.CompareTag("Pillar") && !isPlayerCrashPillar)
        {
            isPlayerCrashPillar = true;
            gameOver = true;
            gameManager.GameOver();
        }
        if (collision.gameObject.CompareTag("enemy") && !circleSprite.checkCircle())
        {
            isPlayerKilledByEnemy = true;
            gameOver = true;
            Time.timeScale = 0;
        }
        if (collision.gameObject.CompareTag("dog"))
        {
            isPlayerKilledByEnemy = true;
            gameOver = true;
            Time.timeScale = 0;
        }
        if (collision.gameObject.CompareTag("Key"))
        {
            Destroy(collision.gameObject);
            hasKey = true;
        }
        if (collision.gameObject.CompareTag("Door") && !isGameWin && hasKey)
        {
            isGameWin = true;
            gameOver = true;
            DisplayGameOverMessage("");
            gameManager.GameOver();

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Door") && hasKey)
        {
            isGameWin = true;
            Time.timeScale = 0; // Pause the game by setting the time scale to 0.
            DisplayGameOverMessage("");
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
            DisplayGameOverMessage("YOU WIN!!");
            gameOver = true;
        }

        if (isPlayerCrashPillar)
        {
            DisplayGameOverMessage("You crashed into the Pillar, YOU LOSE!!");
            gameOver = true;
        }

        if (isPlayerKilledByEnemy)
        {
            DisplayGameOverMessage("You were killed by the enemy, YOU LOSE!!");
            gameOver = true;
        }
    }

    public void DisplayGameOverMessage(string message)
    {
        /*Rect gameOverRect = new Rect(Screen.width / 2 - 100, Screen.height / 2 - 25, 400, 100);

        GUIStyle style = new GUIStyle(GUI.skin.label);
        style.fontSize = 24;
        style.alignment = TextAnchor.MiddleCenter;

        GUI.Label(gameOverRect, message, style);

        if (gameManager != null)
        {
            Rect buttonRect = new Rect(Screen.width / 2 - 100, Screen.height / 2 + 25, 200, 60);
            Rect mainMenuRect = new Rect(Screen.width / 2 + 5, Screen.height / 2 + 25, 200, 60);

            if (GUI.Button(buttonRect, "Play Again"))
            {
                gameManager.RestartGame();
            }
            if (GUI.Button(mainMenuRect, "Main Menu"))
            {
                SceneManager.LoadScene("Menu"); 
            }
        }
        else
        {
            // Handle the case where gameManager is null (e.g., display an error message).
            GUI.Label(new Rect(10, 10, 200, 30), "Error: GameManager not found.");
        }
        */
        if (isGameWin)
        {
            SceneManager.LoadScene("FourthLevel");
        }
        else
        {
            Rect gameOverRect = new Rect(Screen.width / 2 - 100, Screen.height / 2 - 25, 400, 100);
            GUIStyle style = new GUIStyle(GUI.skin.label);
            style.fontSize = 24;
            style.alignment = TextAnchor.MiddleCenter;

            GUI.Label(gameOverRect, message, style);

            if (gameManager != null)
            {
                Rect buttonRect = new Rect(Screen.width / 2 - 100, Screen.height / 2 + 25, 200, 60);
                Rect mainMenuRect = new Rect(Screen.width / 2 + 5, Screen.height / 2 + 25, 200, 60);

                if (GUI.Button(buttonRect, "Play Again"))
                {
                    gameManager.RestartGame();
                }
                if (GUI.Button(mainMenuRect, "Main Menu"))
                {
                    SceneManager.LoadScene("Menu");
                }
            }
        }
    }

    public bool isGameOver()
    {
        return gameOver;
    }
}
