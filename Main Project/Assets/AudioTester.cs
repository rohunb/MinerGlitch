using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioTester : MonoBehaviour 
{

    IEnumerator Start()
    {
        AudioManager.Instance.PlayMainTrack(Sound.SpaceWarTrack);
        yield return new WaitForSeconds(5.0f);
        AudioManager.Instance.PlayMainTrack(Sound.Level1BossTrack);
    }
	
}
