using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleSprite : MonoBehaviour
{
    private Vector3 originalScale;
    private bool isSquare = true; // Start as a square
    private bool isCircle = false; // Start as a square

    private SpriteRenderer spriteRenderer;
    public Sprite squareSprite; // Assign the square sprite in the Unity Inspector
    public Sprite circleSprite;

    // Start is called before the first frame update
    void Start()
    {
        originalScale = transform.localScale;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = squareSprite;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            isCircle = true;
            ToggleShape();

        }
    }

    public bool checkCircle()
    {
        return !isSquare;
    }

    private void ToggleShape()
    {
        if (isCircle)
        {
            // Switch to a circle

            float circleScale = Mathf.Max(originalScale.x, originalScale.y);
            transform.localScale = new Vector3(circleScale, circleScale, originalScale.z);
            spriteRenderer.sprite = circleSprite;
            isSquare = false; 
        }
        else
        {
            // Switch back to a square
            transform.localScale = originalScale;
            spriteRenderer.sprite = squareSprite;
        }


        // Start a coroutine to revert the shape to square after 3 seconds
        StartCoroutine(RevertToSquareAfterDelay(3f));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            // Destroy the enemy object when the player touches it.
            if (!isSquare)
            {
                Destroy(collision.gameObject);
            }
        }
    }

    private IEnumerator RevertToSquareAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Ensure we are still in circle mode before reverting
        if (!isSquare)
        {
            transform.localScale = originalScale;
            spriteRenderer.sprite = squareSprite;
            isSquare = true;
        }
    }
}
