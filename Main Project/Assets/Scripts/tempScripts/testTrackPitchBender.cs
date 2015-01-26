using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class testTrackPitchBender : MonoBehaviour 
{
    public float interval = 1.0f;
    public float duration = 0.5f;
    public Vector2 pitchRange = new Vector2(0.4f, 1.4f);
    public Vector2 changeIntervalRange = new Vector2(0.1f, 0.3f);

    IEnumerator Start()
    {
        AudioManager.Instance.PlayMainTrack(Sound.SpaceWarTrack);
        //while(true)
        //{
        //    yield return new WaitForSeconds(interval);
        //    yield return StartCoroutine(GlitchSound());
        //}
        yield return null;
        
    }
    //private IEnumerator GlitchSound()
    //{
    //    //yield return new WaitForSeconds(2.0f);
    //    AudioSource source = AudioManager.Instance.MainTrack;
    //    float defaultPitch = source.pitch;

    //    float currentTime = 0.0f;

    //    do
    //    {
    //        source.pitch = Random.Range(pitchRange.x, pitchRange.y);
    //        float waitTime = Random.Range(changeIntervalRange.x, changeIntervalRange.y);
    //        currentTime += waitTime;
    //        yield return new WaitForSeconds(waitTime);

    //    } while (currentTime <= duration);

    //    source.pitch = defaultPitch;
    //}
}
