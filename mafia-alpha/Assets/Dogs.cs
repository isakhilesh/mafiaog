using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dogs : MonoBehaviour
{
    public float speed = 2.0f; // Speed of the object's movement
    public float followRange = 10.0f; // Range to start following the player
    private Transform playerTransform;
    private bool isFollowing = false;

    void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").transform; // Assuming the player has a tag "Player"
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer <= followRange)
        {
            isFollowing = true;
        }

        if (isFollowing)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, step);

            // Rotate to face the player (optional)
            Vector3 direction = (playerTransform.position - transform.position).normalized;

        }
    }

}
