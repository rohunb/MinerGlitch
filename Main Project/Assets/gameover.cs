using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class gameover : MonoBehaviour 
{
    public GameObject bsod;
    public GameObject bsodGlitch;

    void Start()
    {
        AudioManager.Instance.PlayMainTrack(Sound.MissionCompleteTrack);
        StartCoroutine(GameOver());
    }
    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(2.0f);
        bsod.SetActive(false);
        bsodGlitch.SetActive(true);
        while(!Input.anyKeyDown)
        {
            yield return null;
        }
        Application.LoadLevel(GameScene.MainMenu.ToString());

    }
   
	
}
