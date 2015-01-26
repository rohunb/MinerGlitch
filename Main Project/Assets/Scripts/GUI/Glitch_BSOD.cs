using UnityEngine;
using System.Collections;

public class Glitch_BSOD : MonoBehaviour {
    public float waitTime = 4.5f;
    public string sceneToLoad = "MovementTest";
    // Use this for initialization
    void Start()
    {

    }

    void Awake()
    {
        StartCoroutine(waitTillMainMenu(waitTime));
    }

    private IEnumerator waitTillMainMenu(float _secondsToWait)
    {
        yield return new WaitForSeconds(_secondsToWait);

        Application.LoadLevel(sceneToLoad);

    }

    // Update is called once per frame
    void Update()
    {

    }
}