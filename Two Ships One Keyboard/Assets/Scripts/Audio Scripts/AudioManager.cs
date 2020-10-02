using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{

    
    //How long it takes for tracks to fade out
    [SerializeField] protected float trackTransitionTime;
    //The current BG song being played (only can be one at a time)
    private AudioSource currentTrack;


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

            //There can only be ONE track that plays on awake, and that will be set as the current track
            if (s.playOnAwake)
            {
                source.Play();
                currentTrack = source;
            }

        }
    }

    public void PlaySoundEffect (string s)
    {

        soundLibrary[s].GetSource().Play();

    }

    Coroutine transitionCoroutine = null;
    public void PlayTrack (string s)
    {
        if (transitionCoroutine != null) { StopCoroutine(transitionCoroutine); }
        else { transitionCoroutine = StartCoroutine(TrackTransition(soundLibrary[s].GetSource())); }
    }

    IEnumerator TrackTransition (AudioSource newTrack)
    {

        float counter = 0;
        float originalVolume = currentTrack.volume;

        while (counter < trackTransitionTime)
        {
            currentTrack.volume = Mathf.Lerp(originalVolume, 0, counter / trackTransitionTime);
            counter++;
            yield return new WaitForFixedUpdate();
        }

        currentTrack.Stop();
        currentTrack.volume = originalVolume;
        currentTrack = newTrack;
        currentTrack.Play();

    }



}
