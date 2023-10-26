using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public Vector3 offset = new Vector3(0f, 5f, -50f); // Offset to control the camera's position

    void Update()
    {
        if (player != null) // Ensure the player reference is not null
        {
            // Set the camera's position to match the player's position with an offset
            transform.position = player.position + offset;
        }
    }
}
