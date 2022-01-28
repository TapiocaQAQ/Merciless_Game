using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public Sounds[] sounds;

    private void Awake() {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(gameObject);
        }

        DontDestroyOnLoad(this);

        foreach(Sounds s in sounds){
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.outputAudioMixerGroup = s.output;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.playOnAwake = s.playOnAwake;
        }
    }

    void Start() 
    {
        Play("BGM");
    }

    public void Play(string _name)
    {
        Sounds s = Array.Find(sounds, sounds => sounds.name == _name);
        if(s == null){
            Debug.Log("Sound: " + _name + "not found!");
            return;
        }
        s.source.Play();
    }

    public void Play(int _index)
    {
        sounds[_index].source.Play();
    }

    public void Stop(string _name)
    {
        Sounds s = Array.Find(sounds, sounds => sounds.name == _name);
        if(s == null){
            Debug.Log("Sound: " + _name + "not found!");
            return;
        }
        s.source.Stop();
    }

    public void Stop(int _index)
    {
        sounds[_index].source.Stop();
    }

    public void Reset()
    {
        AudioManager.instance.Stop(10);
        AudioManager.instance.Play(7);
    }
}
