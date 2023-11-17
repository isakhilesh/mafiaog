using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimaryObjectScript : MonoBehaviour
{
    public float primaryObjectSpeed;
    private Rigidbody2D rb;
    private Vector2 primaryObjectDirection;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float directionX = Input.GetAxisRaw("Horizontal"); // Capture horizontal input
        float directionY = Input.GetAxisRaw("Vertical");

        primaryObjectDirection = new Vector2(directionX, directionY).normalized; // Combine horizontal and vertical input
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(primaryObjectDirection.x * primaryObjectSpeed, primaryObjectDirection.y * primaryObjectSpeed);
    }
}
