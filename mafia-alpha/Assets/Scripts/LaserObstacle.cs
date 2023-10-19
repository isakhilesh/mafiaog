using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserObstacle : MonoBehaviour
{
    [SerializeField] private float defDRay = 100;
    public Transform CCTVH;
    public LineRenderer m_LR;
    Transform m_transform;

    public float rotationSpeed = 0.0f; // Rotation speed in degrees per second
    public float rotationInterval = 0.0f; // Interval to change rotation direction
    private bool rotateLeft = true;
    private float timeSinceLastRotation = 0.0f;

    private bool isLaserActive = false; // Flag to control the laser activation
    private float laserTimer = 5.0f; // Time interval to activate/deactivate the laser

    private void Awake()
    {
        m_transform = transform;
    }

    private void Update()
    {
        timeSinceLastRotation += Time.deltaTime;

        // Rotate the beam
        if (rotateLeft)
        {
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        }
        else
        {
            transform.Rotate(Vector3.forward, -rotationSpeed * Time.deltaTime);
        }

        // Check if it's time to change rotation direction
        if (timeSinceLastRotation >= rotationInterval)
        {
            rotateLeft = !rotateLeft;
            timeSinceLastRotation = 0.0f;
        }

        // Handle the laser activation and deactivation timer
        laserTimer -= Time.deltaTime;
        if (laserTimer <= 0)
        {
            Debug.Log("In if");
            isLaserActive = !isLaserActive; // Toggle the laser state
            laserTimer = 5.0f; // Reset the timer
        }

        // Shoot or stop the laser based on the flag
        if (isLaserActive)
        {
            ShootLaser();
        }
        else
        {
            // Deactivate the laser by not drawing the ray
            m_LR.SetPosition(0, CCTVH.position);
            m_LR.SetPosition(1, CCTVH.position);
        }
    }

    void ShootLaser()
    {
        Debug.Log("In shoot update");
        if (Physics2D.Raycast(m_transform.position, -transform.up))
        {
            RaycastHit2D _hit = Physics2D.Raycast(CCTVH.position, -transform.up);
            Draw2DRay(CCTVH.position, _hit.point);
        }
        else
        {
            Draw2DRay(CCTVH.position, CCTVH.position - transform.up * defDRay);
        }
    }

    void Draw2DRay(Vector2 startpos, Vector2 endpos)
    {
        m_LR.SetPosition(0, startpos);
        m_LR.SetPosition(1, endpos);
    }
}




/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserObstacle : MonoBehaviour
{
    [SerializeField] private float defDRay = 100;
    public Transform CCTVH;
    public LineRenderer m_LR;
    Transform m_transform;


    public float rotationSpeed = 10.0f; // Rotation speed in degrees per second
    public float rotationInterval = 15.0f; // Interval to change rotation direction
    private bool rotateLeft = true;
    private float timeSinceLastRotation = 0.0f;

    private void Awake()
    {
        m_transform = transform;
    }

    private void Update()
    {
        ShootLaser();
        timeSinceLastRotation += Time.deltaTime;

        // Rotate the beam
        if (rotateLeft)
        {
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        }
        else
        {
            transform.Rotate(Vector3.forward, -rotationSpeed * Time.deltaTime);
        }

        // Check if it's time to change rotation direction
        if (timeSinceLastRotation >= rotationInterval)
        {
            rotateLeft = !rotateLeft;
            timeSinceLastRotation = 0.0f;
        }

    }

    void ShootLaser()
    {
        if (Physics2D.Raycast(m_transform.position, -transform.up))
        {
            RaycastHit2D _hit = Physics2D.Raycast(CCTVH.position, -transform.up);
            Draw2DRay(CCTVH.position, _hit.point);
            // if (_hit.collider.CompareTag("Player"))
            //{
            // Game over logic here (e.g., display game over screen or restart the level)
            //  Debug.Log("Game Over");
            // You can add game over logic here, such as displaying a game over screen, resetting the level, or ending the game.
            // }
        }
        else
        {
            Draw2DRay(CCTVH.position, CCTVH.position - transform.up * defDRay);
        }
    }

    void Draw2DRay(Vector2 startpos, Vector2 endpos)
    {
        m_LR.SetPosition(0, startpos);
        m_LR.SetPosition(1, endpos);
    }
}


*/