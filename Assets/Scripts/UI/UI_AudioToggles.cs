// Copyright (C) 2018 Maarten Engels, thedreamweb.eu

// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.

// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.

// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class UI_AudioToggles : MonoBehaviour {

    public Text sfxToggleButtonText;
    public Text musicToggleButtonText;

    public AudioMixer mixer;

    bool sfxEnabled = true;
    bool musicEnabled = true;

    void Start()
    {
        if (PlayerPrefs.HasKey("SFXEnabled"))
        {
            if (PlayerPrefs.GetInt("SFXEnabled") == 1)
            {
                mixer.SetFloat("SFXVolume", -10f);
                sfxToggleButtonText.text = "SFX: On";
                sfxEnabled = true;
            } else
            {
                mixer.SetFloat("SFXVolume", -80f);
                sfxToggleButtonText.text = "SFX: Off";
                sfxEnabled = false;
            }
        }

        if (PlayerPrefs.HasKey("MusicEnabled"))
        {
            if (PlayerPrefs.GetInt("MusicEnabled") == 1)
            {
                mixer.SetFloat("MusicVolume", -5f);
                musicToggleButtonText.text = "Music: On";
                musicEnabled = true;
            }
            else
            {
                mixer.SetFloat("MusicVolume", -80f);
                musicToggleButtonText.text = "Music: Off";
                musicEnabled = false;
            }
        }
    }

    public void SFXToggleButtonClicked()
    {
        sfxEnabled = !sfxEnabled;

        if (sfxEnabled)
        {
            mixer.SetFloat("SFXVolume", -10f);
            sfxToggleButtonText.text = "SFX: On";
            PlayerPrefs.SetInt("SFXEnabled", 1);
        } else
        {
            mixer.SetFloat("SFXVolume", -80f);
            sfxToggleButtonText.text = "SFX: Off";
            PlayerPrefs.SetInt("SFXEnabled", 0);
        }
    }

    public void MusicToggleButtonClicked()
    {
        musicEnabled = !musicEnabled;

        if (musicEnabled)
        {
            mixer.SetFloat("MusicVolume", -5f);
            musicToggleButtonText.text = "Music: On";
            PlayerPrefs.SetInt("MusicEnabled", 1);
        }
        else
        {
            mixer.SetFloat("MusicVolume", -80f);
            musicToggleButtonText.text = "Music: Off";
            PlayerPrefs.SetInt("MusicEnabled", 0);
        }
    }

}
