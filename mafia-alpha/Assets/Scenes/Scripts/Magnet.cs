using UnityEngine;

public class Magnet : MonoBehaviour
{
    public float attractionForce = 100f;
    public float attractionRadius = 5f;
    private int magCount = 0;
    private bool magnetActive = false;
    public GameObject playerObject; // Assign the player object in the Inspector
    public GameObject surroundingObjectPrefab;
    private bool isMagnetActive = false;
    void Start()
    {
        if (surroundingObjectPrefab != null)
        {
            surroundingObjectPrefab.SetActive(false);
            if (playerObject != null)
            {
                // Set the surrounding object as a child of the player
                surroundingObjectPrefab.transform.parent = playerObject.transform;
            }
        }
    }

    private void FixedUpdate()
    {
        if (magnetActive)
        {
            Collider2D[] attractableObjects = Physics2D.OverlapCircleAll(transform.position, attractionRadius);

            foreach (Collider2D obj in attractableObjects)
            {
                if (obj.CompareTag("Player"))
                {
                    Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
                    if (rb != null)
                    {
                        Vector2 direction = transform.position - obj.transform.position;
                        rb.AddForce(direction.normalized * attractionForce);
                    }
                }
            }
        }
    }

    void OnDrawGizmos()
    {
        // Draw the gravity field radius in the Scene view
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attractionRadius);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            magCount += 1;
            magnetActive = !magnetActive; // Toggle the magnet state
            ToggleSurroundingObject();
        }
    }

    public int getMagCount()
    {
        return magCount/2;
    }

    void ToggleSurroundingObject()
    {
        isMagnetActive = !isMagnetActive;
        if (isMagnetActive)
        {
            surroundingObjectPrefab.SetActive(true);
        }
        else
        {
            surroundingObjectPrefab.SetActive(false);
        }
    }


}
