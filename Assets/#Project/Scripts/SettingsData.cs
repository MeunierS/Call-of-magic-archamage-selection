using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsData
{
    public const string VOLUME_MAIN = "MainVolume";
    public const string VOLUME_MUSIC = "MusicVolume";
    public const string VOLUME_SFX = "SFXVolume";
    public const float VOLUME_DEFAULT = 1;
    public static float volumeMain = VOLUME_DEFAULT;
    public static float volumeMusic = VOLUME_DEFAULT;
    public static float volumeSFX = VOLUME_DEFAULT;

    public static void Load(){
        volumeMain = PlayerPrefs.GetFloat(VOLUME_MAIN, VOLUME_DEFAULT);
        volumeMusic = PlayerPrefs.GetFloat(VOLUME_MUSIC, VOLUME_DEFAULT);
        volumeSFX = PlayerPrefs.GetFloat(VOLUME_SFX, VOLUME_DEFAULT);
    }
}
