using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;
using UnityEngine.Audio;

public class MainScreenManager : MonoBehaviour
{
    public GameManager gm;
    public List<PlayerType> playableChars = new List<PlayerType>();
    public GameObject CharSelectPanel, OptionsPanel, CharSelButtonPref;
    public AudioMixer mixer;
    public Light2D light;

    private void Start()
    {
        SetUpAudio();
    }
    private void SetUpAudio()
    {
        if (PlayerPrefs.HasKey("bgmVol"))
        {
            mixer.SetFloat("bgmVol", PlayerPrefs.GetFloat("bgmVol"));
        }
        if (PlayerPrefs.HasKey("sfxVol"))
        {
            mixer.SetFloat("sfxVol", PlayerPrefs.GetFloat("sfxVol"));
        }
    }
    private void Update()
    {
        light.intensity = Mathf.Clamp(light.intensity + 0.01f, 0f, 2.5f);
    }
    public void TurnOnOptionsPanel()
    {

    }
    public void TurnOnPlayerSelection()
    {
        CharSelectPanel.SetActive(true);
        GameObject panel = CharSelectPanel.transform.Find("CharSelectArea").gameObject;
        foreach (PlayerType pt in playableChars)
        {
            var tempButton = Instantiate(CharSelButtonPref, panel.transform);
            tempButton.transform.SetSiblingIndex(0);
            tempButton.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = pt.typeName;
            tempButton.transform.Find("Description").GetComponent<TextMeshProUGUI>().text = pt.description;
            tempButton.transform.Find("ImageOutline").Find("PlayerImage").GetComponent<Image>().sprite = pt.baseImage;
            tempButton.transform.Find("ImageOutline").Find("PlayerImage").GetComponent<Animator>().runtimeAnimatorController = pt.controller;
            Button button = tempButton.transform.Find("PlayButton").GetComponent<Button>();
            button.onClick.AddListener(() => gm.StartGame(pt));
        }

    }
    public void TurnOnRanking()
    {

    }
}
