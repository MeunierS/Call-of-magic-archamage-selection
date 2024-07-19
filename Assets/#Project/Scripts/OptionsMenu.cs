using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer mainMixer;
    public Slider mainSlider;
    public Slider musicSlider;
    public Slider sfxSlider;
    // Start is called before the first frame update
    void Start()
    {
        SettingsData.Load();
        mainSlider.value = PlayerPrefs.GetFloat("MainVolume", 1f);
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1f);
        mainMixer.SetFloat("MainVolume", PlayerPrefs.GetFloat("MainVolume"));
        mainMixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("MusicVolume"));
        mainMixer.SetFloat("SFXVolume", PlayerPrefs.GetFloat("SFXVolume"));
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void ChangeMainVol(float volume){
        float audioValue =  Mathf.Log10(volume) * 20;
        PlayerPrefs.SetFloat("MainVolume", audioValue);
        mainMixer.SetFloat ("MainVolume", PlayerPrefs.GetFloat("MainVolume"));
        PlayerPrefs.Save();
    }
    public void ChangeMusicVol(float volume){
        float audioValue =  Mathf.Log10(volume) * 20;
        PlayerPrefs.SetFloat("MusicVolume", audioValue);
        mainMixer.SetFloat ("MusicVolume", PlayerPrefs.GetFloat("MusicVolume"));
        PlayerPrefs.Save();
    }
    public void ChangeSFXVol(float volume){
        float audioValue =  Mathf.Log10(volume) * 20;
        PlayerPrefs.SetFloat("SFXVolume", audioValue);
        mainMixer.SetFloat ("SFXVolume", PlayerPrefs.GetFloat("SFXVolume"));
        PlayerPrefs.Save();
    }
}
