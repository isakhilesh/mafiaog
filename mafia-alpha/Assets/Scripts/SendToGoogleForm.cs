using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SendToGoogleForm : MonoBehaviour
{
    [SerializeField] private string URL;
    private double _sessionID;
    private double _testInt;
    private bool _testBool;
    private float _testFloat;
    public PlayerController playerController;
    public bool flag = true;
    private long _startTime;
    // Start is called before the first frame update
    void Start()
    {
        // Call the Send method when the script starts
        
        playerController = GetComponent<PlayerController>();
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
            Debug.Log("in if");
            
            _testInt = TimeSpan.FromTicks(DateTime.Now.Ticks - _startTime).TotalSeconds;

            Send();
            flag = false;
        }
    }

    public void Send()
    {
        // Assign variables
        _sessionID = DateTime.Now.Ticks;
        _testBool = true;
        _testFloat = UnityEngine.Random.Range(0.0f, 10.0f);
        // Create a dictionary to store the form fields and values
        Dictionary<string, string> formData = new Dictionary<string, string>();
        formData["entry.123182976"] = _sessionID.ToString(); // Replace 'entry.123456' with the actual form field name from your Google Form
        formData["entry.1350690521"] = _testInt.ToString();     // Replace 'entry.789012' with the actual form field name
        formData["entry.462554948"] = _testBool.ToString();     // Replace 'entry.345678' with the actual form field name
        formData["entry.475182497"] = _testFloat.ToString();    // Replace 'entry.901234' with the actual form field name
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
}
