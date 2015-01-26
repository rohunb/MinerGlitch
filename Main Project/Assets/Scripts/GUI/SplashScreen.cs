using UnityEngine;
using System.Collections;

public class SplashScreen : MonoBehaviour 
{
    public float waitTime = 2.0f;
	// Use this for initialization
	void Start () 
    {
	
	}

    void Awake()
    {
        StartCoroutine(waitTillMainMenu(waitTime));
    }

    private IEnumerator waitTillMainMenu(float _secondsToWait)
    {
        yield return new WaitForSeconds(_secondsToWait);

        Application.LoadLevel("MainMenu");

    }
	
	// Update is called once per frame
	void Update () 
    {
	
	}
}
