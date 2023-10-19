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
    private GameManager gameManager;

    private bool isLaserActive = false; // Flag to control the laser activation
    private float laserTimer = 5.0f; // Time interval to activate/deactivate the laser

    private bool isLaserTouched = false;
    public bool gameOver = false;
    private PlayerController playerController;
    private void Awake()
    {
        m_transform = transform;
    }
    private void Start()
    {
        Time.timeScale = 1;
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();

        // Find the PlayerController component in the scene and set the reference.
        playerController = GameObject.FindObjectOfType<PlayerController>();
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
        //Debug.Log("In shoot update");
        if (Physics2D.Raycast(m_transform.position, -transform.up))
        {
            RaycastHit2D _hit = Physics2D.Raycast(CCTVH.position, -transform.up);
            Draw2DRay(CCTVH.position, _hit.point);
            
            if (_hit.collider.CompareTag("Player"))
            {
                isLaserTouched = true;
                Time.timeScale = 0;
            }
        }
        else
        {
            Draw2DRay(CCTVH.position, CCTVH.position - transform.up * defDRay);
        }
    }

    void OnGUI(){
        if(isLaserTouched){
            gameOver = true;
            playerController.DisplayGameOverMessage("You were burned by the Laser! YOU LOSE!!");
        }
    }

    public bool isGameOver()
    {
        return gameOver;
    }
    

    void Draw2DRay(Vector2 startpos, Vector2 endpos)
    {
        m_LR.SetPosition(0, startpos);
        m_LR.SetPosition(1, endpos);
    }
}




