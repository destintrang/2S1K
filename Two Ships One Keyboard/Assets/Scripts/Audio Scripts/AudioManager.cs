using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{


    [SerializeField] protected List<Sound> sounds;
    private Dictionary<string, Sound> soundLibrary = new Dictionary<string, Sound>();


    // Start is called before the first frame update
    void Awake()
    {
        foreach (Sound s in sounds)
        {

            AudioSource source = gameObject.AddComponent<AudioSource>();

            //Add to dictionary so that sound can be accessed by name
            soundLibrary.Add(s.GetName(), s);

            //Initialize the settings of the audio source
            s.SetSource(source);
            source.clip = s.GetSound();
            source.volume = s.GetVolume();
            source.pitch = s.GetPitch();
            source.playOnAwake = s.playOnAwake;
            source.loop = s.loop;

            if (s.playOnAwake)
            {
                source.Play();
            }

        }
    }

    public void Play (string s)
    {

        soundLibrary[s].GetSource().Play();

    }



}
