using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void levelOne()
    {
        SceneManager.LoadScene("FirstLevel"); 
    }

    public void levelTwo()
    {
        SceneManager.LoadScene("SecondLevel"); 
    }

    public void levelThree()
    {
        SceneManager.LoadScene("ThirdLevel"); 
    }

    public void levelFour()
    {
        SceneManager.LoadScene("FourthLevel"); 
    }

}
