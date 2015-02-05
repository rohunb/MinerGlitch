using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;


public enum Sound
{
    //tracks
    SpaceWarTrack,
    Level1BossTrack,
    BioMechTrack,
    LevelStartTrack,
    TitleScreenTrack,
    MissionCompleteTrack,

    //effects
    Shotgun,
    LaserBeam,
    Orb

}

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField]
    private int numSources = 32;

    [SerializeField]
    private List<SoundInfo> soundInfoList;

    private AudioSource[] sources;
    private Dictionary<Sound, AudioClip> sound_clip_table;

    public AudioSource MainTrack { get; private set; }

    //private bool soundsOn = true;
    //public bool SoundsOn
    //{
    //    get { return soundsOn; }
    //}
    [SerializeField]
    private string soundsOnName = "SoundOn";

    //public void TogglesSounds(bool on)
    //{
    //    soundsOn = on;
    //    if(on)
    //    {
    //        PlayerPrefs.SetInt(soundsOnName, 1);
    //    }
    //    else
    //    {
    //        PlayerPrefs.SetInt(soundsOnName, 0);
    //    }
    //    foreach (AudioSource source in sources)
    //    {
    //        source.mute = !on;
    //    }
    //}



    public void PlayMainTrack(Sound sound)
    {
        AudioClip audioClip = sound_clip_table[sound];
        if (audioClip == null)
        {
            Debug.LogError("No sound for found " + sound);
            return;
        }
        AudioSource source = null;

        if(MainTrack==null)
        {
            source = sources.FirstOrDefault(s => !s.isPlaying);
        }
        else
        {
            source = MainTrack;
        }
        if (source == null)
        {
            Debug.LogError("no room for track");
        }
        else
        {
            MainTrack = source;
            source.clip = audioClip;
            source.loop = true;
            source.Play();
        }
    }
    public void PlayOneShotSound(Sound sound)
    {
        PlaySound(sound, false);
    }
    private void PlaySound(Sound sound, bool loop)
    {
        AudioClip audioClip = sound_clip_table[sound];
        if (audioClip == null)
        {
            Debug.LogError("No sound for found " + sound);
            return;
        }
        AudioSource source = sources.FirstOrDefault(s => !s.isPlaying && s != MainTrack);
        if (source == null)
        {
            Debug.LogWarning("No Empty sources found");
        }
        else
        {
            source.clip = audioClip;
            source.loop = loop;
            source.Play();
        }
    }
    public void PlayOneShotSound(Sound sound, float vol)
    {
        AudioClip audioClip = sound_clip_table[sound];
        if (audioClip == null)
        {
            Debug.LogError("No sound for found " + sound);
            return;
        }
        AudioSource source = sources.FirstOrDefault(s => !s.isPlaying && s != MainTrack);
        if (source == null)
        {
            Debug.LogWarning("No Empty sources found");
        }
        else
        {
            source.clip = audioClip;
            source.loop = false;
            source.volume = vol;
            source.Play();
        }
    }
    public void StopSound(Sound sound)
    {
        AudioClip audioClip = sound_clip_table[sound];
        AudioSource source = sources.FirstOrDefault(s => s.clip == audioClip && s.isPlaying);
        if (source != null)
        {
            source.Stop();
        }

    }

    private void Awake()
    {
        sources = new AudioSource[numSources];
        //soundsOn = PlayerPrefs.GetInt(soundsOnName) == 1 ? true : false;
        //Debug.Log("Sounds on: " + soundsOn);
        for (int i = 0; i < numSources; i++)
        {
            sources[i] = gameObject.AddComponent<AudioSource>();
            //sources[i].mute = !soundsOn;
        }
        sound_clip_table = soundInfoList.ToDictionary(s => s.sound, s => s.audioClip);
    }
}
[Serializable]
public struct SoundInfo
{
    public Sound sound;
    public AudioClip audioClip;
}
