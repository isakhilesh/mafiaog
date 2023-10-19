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
    private PlayerController playerController;
    private GameManager gameManager;
    private bool isLaserTouched = false;
    public bool gameOver = false;

    private void Awake()
    {
        m_transform = transform;
    }
    private void Start()
    {
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();

        // Find the PlayerController component in the scene and set the reference.
        playerController = GameObject.FindObjectOfType<PlayerController>();
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
            
            if (_hit.collider.CompareTag("Player"))
            {
                isLaserTouched = true;    
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
            playerController.DisplayGameOverMessage("You were caught by the CCTV! YOU LOSE!!");
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
