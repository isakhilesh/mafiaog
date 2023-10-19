using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCTV : MonoBehaviour
{
    [SerializeField] private float defDRay = 100;
    public Transform CCTVH;
    public LineRenderer m_LR;
    Transform m_transform;


    public float rotationSpeed = 10.0f; // Rotation speed in degrees per second
    public float rotationInterval = 3.0f; // Interval to change rotation direction
    private bool rotateLeft = true;
    private float timeSinceLastRotation = 0.0f;

    private void Awake()
    {
        m_transform = transform;
    }
<<<<<<< Updated upstream:mafia-alpha/Assets/CCTV.cs
=======
    private void Start()
    {
        Time.timeScale = 1;
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
>>>>>>> Stashed changes:mafia-alpha/Assets/Scripts/CCTV.cs

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
<<<<<<< Updated upstream:mafia-alpha/Assets/CCTV.cs
            // if (_hit.collider.CompareTag("Player"))
            //{
            // Game over logic here (e.g., display game over screen or restart the level)
            //  Debug.Log("Game Over");
            // You can add game over logic here, such as displaying a game over screen, resetting the level, or ending the game.
            // }
=======
            
            if (_hit.collider.CompareTag("Player"))
            {
                isLaserTouched = true;
                Time.timeScale = 0;
            }
>>>>>>> Stashed changes:mafia-alpha/Assets/Scripts/CCTV.cs
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
