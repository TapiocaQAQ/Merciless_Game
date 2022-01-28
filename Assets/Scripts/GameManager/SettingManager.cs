using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("Master", volume);
    }

    public void SetGamePlayVolume(float volume)
    {
        audioMixer.SetFloat("GamePlay", volume);
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("Music", volume);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        //Screen.fullScreen = isFullScreen;
        if(isFullScreen){
            Screen.SetResolution(1920, 1080, true);
        }else{
            Screen.SetResolution(1280, 720, false);
        }
    }

    public void SetMouseXSensity(float _value)
    {
        GameManager.instance.localPlayerController.cinema[0].m_XAxis.m_MaxSpeed = _value;
    }

    public void SetMouseYSensity(float _value)
    {
        GameManager.instance.localPlayerController.cinema[0].m_YAxis.m_MaxSpeed = _value;
    }
}
