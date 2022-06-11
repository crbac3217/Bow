using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MainOptionsPanel : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider bgmBar, sfxBar;
    public GameObject joySetting, controlSetting;
    public void SetUpVolume()
    {
        float bgmVal, sfxVal;
        mixer.GetFloat("bgmVol", out bgmVal);
        bgmBar.value = bgmVal;
        mixer.GetFloat("sfxVol", out sfxVal);
        sfxBar.value = sfxVal;
    }
    public void OnSFXChange()
    {
        mixer.SetFloat("sfxVol", sfxBar.GetComponent<Slider>().value);
        PlayerPrefs.SetFloat("sfxVol", sfxBar.GetComponent<Slider>().value);
    }
    public void OnBGMChange()
    {
        mixer.SetFloat("bgmVol", bgmBar.GetComponent<Slider>().value);
        PlayerPrefs.SetFloat("bgmVol", bgmBar.GetComponent<Slider>().value);
    }
    public void OnMouseSenseChange()
    {

    }
}
