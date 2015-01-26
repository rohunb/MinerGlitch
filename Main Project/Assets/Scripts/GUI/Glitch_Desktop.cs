using UnityEngine;
using System.Collections;

public class Glitch_Desktop : MonoBehaviour 
{
    public float waitTime = 2.0f;
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

        GameController.Instance.LevelComplete();

    }

    // Update is called once per frame
    void Update()
    {

    }
}