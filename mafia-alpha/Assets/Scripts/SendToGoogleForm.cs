using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SendToGoogleForm : MonoBehaviour
{
    [SerializeField] private string URL;
    private double _sessionID;
    private double _keyTime;

    public PlayerController playerController;
    public ShapeShifter shapeShifter;
    public CircleSprite circleSprite;
    public ProgressBarUpdate progressBarUpdate;
    public Magnet magnet;

    public bool flag = true;
    public bool flagGameOver = true;
    private long _startTime;
    public LaserObstacle laserObstacle;
    public CCTV cctv;

    private double _totalIdleTime = 0;
    private double _totalTime = 0;

    private const string SessionIDKey = "SessionID";
    private long _sessionCounter = 0;


    // Start is called before the first frame update
    void Start()
    {
        // Call the Send method when the script starts
        cctv = GameObject.FindObjectOfType<CCTV>();
        laserObstacle = GameObject.FindObjectOfType<LaserObstacle>();
        playerController = GetComponent<PlayerController>();
        shapeShifter = GetComponent<ShapeShifter>();
        circleSprite = GetComponent<CircleSprite>();
        progressBarUpdate = GetComponent<ProgressBarUpdate>();
        magnet = GameObject.FindObjectOfType<Magnet>();

        _totalIdleTime = 0;
        _totalTime = 0;
    }

    private void Awake()
    {
        // Assign sessionID to identify playtests
        URL = "https://docs.google.com/forms/u/1/d/e/1FAIpQLScsFVmridEbsmLLd_ZThAOEhVnJxYfSt-QE35YSc5p58MIAHg/formResponse";
        _startTime = DateTime.Now.Ticks;

    }
    private void Update()
    {
        if (playerController.getHasKey() && flag)
        {
            _keyTime = TimeSpan.FromTicks(DateTime.Now.Ticks - _startTime).TotalSeconds;

            flag = false;
        }

        if ((playerController.isGameOver() == true || laserObstacle.isGameOver() == true || cctv.isGameOver() == true)  && flagGameOver)
        {
            _totalTime = TimeSpan.FromTicks(DateTime.Now.Ticks - _startTime).TotalSeconds;
            Send();
            flagGameOver = false;
        }

        if (!Input.anyKey && playerController.isGameOver() == false)
        {
            Debug.Log("idle!");
            _totalIdleTime += Time.deltaTime;
        }
    }

    public void Send()
    {
        // Assign variables
        _sessionID = DateTime.Now.Ticks;

        if (_keyTime == 0)
        {
            _keyTime = -20;
        }

        // Create a dictionary to store the form fields and values
        Dictionary<string, string> formData = new Dictionary<string, string>();
        //sessionID
        //formData["entry.123182976"] = _sessionID.ToString();
        formData["entry.123182976"] = GenerateOrderedSessionID().ToString();

        //TimetoKey
        formData["entry.1350690521"] = _keyTime.ToString();
        
        //totalTimeSurvived
        formData["entry.16580158"] = _totalTime.ToString();

        //idleTime
        formData["entry.1809734840"] = _totalIdleTime.ToString();

        //AntiGravity analysis
        formData["entry.462554948"] = progressBarUpdate.getTimeUsed().ToString();

        //Rectangle analytics
        formData["entry.475182497"] = shapeShifter.getRectcount().ToString();    

        //Circle analytics
        formData["entry.266303659"] = circleSprite.getCircount().ToString();     

        //Magnet analytics
        formData["entry.432883543"] = magnet.getMagCount().ToString();

        StartCoroutine(Post(URL, formData));
    }

    private IEnumerator Post(string url, Dictionary<string, string> formData)
    {
        // Create a new form and add the form fields and values
        WWWForm form = new WWWForm();
        foreach (var field in formData)
        {
            form.AddField(field.Key, field.Value);
        }

        // Send the form and verify the result
        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
            }
        }
    }

    public long GenerateOrderedSessionID()
    {
        long currentTicks = DateTime.Now.Ticks;

        // Retrieve the last session ID from PlayerPrefs
        long lastSessionID = PlayerPrefs.GetInt(SessionIDKey, -1);

        // If the current ticks are greater than or equal to the last session ID, increment the counter
        if (currentTicks >= lastSessionID)
        {
            _sessionCounter++;
        }
        else
        {
            _sessionCounter = 0;
        }

        // Combine current ticks with the counter to ensure uniqueness
        long orderedSessionID = currentTicks + _sessionCounter;

        // Store the new session ID in PlayerPrefs
        PlayerPrefs.SetInt(SessionIDKey, (int)orderedSessionID);

        return orderedSessionID;
    }
}
