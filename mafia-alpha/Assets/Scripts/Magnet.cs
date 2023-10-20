using UnityEngine;

public class Magnet : MonoBehaviour
{
    public float attractionForce = 100f;
    public float attractionRadius = 5f;
    private int magCount = 0;
    private bool magnetActive = false;

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
        }
    }

    public int getMagCount()
    {
        return magCount/2;
    }


}
