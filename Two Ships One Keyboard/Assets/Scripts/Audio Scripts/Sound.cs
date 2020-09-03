using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound 
{


    [SerializeField] protected string name;
    public string GetName () { return name; }

    [SerializeField] protected AudioClip sound;
    public AudioClip GetSound () { return sound; }

    [Range(0f, 1f)]
    [SerializeField] protected float volume;
    public float GetVolume () { return volume; }

    [Range(0.1f, 3f)]
    [SerializeField] protected float pitch;
    public float GetPitch () { return pitch; }

    public bool playOnAwake;
    public bool loop;

    private AudioSource source;
    public void SetSource(AudioSource s) { source = s; }
    public AudioSource GetSource() { return source; }

}
