using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class starttrack : MonoBehaviour 
{

	void Start()
    {
        AudioManager.Instance.PlayMainTrack(Sound.SpaceWarTrack);
    }
    void Update()
    {
        if( Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.J))
        {
            GameController.Instance.LevelComplete();
        }
    }
}
