using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class UI_AudioToggles : MonoBehaviour {

    public Text sfxToggleButtonText;
    public Text musicToggleButtonText;

    public AudioMixer mixer;

    float baseSFXVolume;
    float baseMusicVolume;

    bool sfxEnabled = true;
    bool musicEnabled = true;

    void Start()
    {
        mixer.GetFloat("SFXVolume", out baseSFXVolume);
        mixer.GetFloat("MusicVolume", out baseMusicVolume);
    }

    public void SFXToggleButtonClicked()
    {
        sfxEnabled = !sfxEnabled;

        if (sfxEnabled)
        {
            mixer.SetFloat("SFXVolume", baseSFXVolume);
            sfxToggleButtonText.text = "SFX: On";
        } else
        {
            mixer.SetFloat("SFXVolume", -80f);
            sfxToggleButtonText.text = "SFX: Off";
        }
    }

    public void MusicToggleButtonClicked()
    {
        musicEnabled = !musicEnabled;

        if (musicEnabled)
        {
            mixer.SetFloat("MusicVolume", baseMusicVolume);
            musicToggleButtonText.text = "Music: On";
        }
        else
        {
            mixer.SetFloat("MusicVolume", -80f);
            musicToggleButtonText.text = "Music: Off";
        }
    }

}
