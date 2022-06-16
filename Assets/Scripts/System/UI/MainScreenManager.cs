using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;
using UnityEngine.Audio;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.SceneManagement;

public class MainScreenManager : MonoBehaviour
{
    public GameManager gm;
    public List<PlayerType> playableChars = new List<PlayerType>();
    public GameObject CharSelectPanel, OptionsPanel, CharSelButtonPref, username, onlineCheckScreen, userIDScreen, loginFailureScreen, userIDSuccess, userIDFailure, startButton;
    public AudioMixer mixer;
    public Light2D light2d;
    public TMP_InputField usernameInput;
    private string usernameString;
    private PlayerType pt;

    private void Start()
    {
        SetUpAudio();
        if (PlayerPrefs.HasKey("username"))
        {
            username.SetActive(true);
            username.GetComponent<TextMeshProUGUI>().text = "Welcome Back! " + PlayerPrefs.GetString("username");
        }
        else
        {
            username.SetActive(false);
        }
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
        light2d.intensity = Mathf.Clamp(light2d.intensity + 0.01f, 0f, 2.5f);
    }
    public void TurnOnOptionsPanel()
    {
        OptionsPanel.SetActive(true);
        OptionsPanel.GetComponent<MainOptionsPanel>().SetUp();
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
            button.onClick.AddListener(() => TurnOnPlayfabSignin(pt));
        }

    }
    public void TurnOnRanking()
    {
        SceneManager.LoadSceneAsync(2);
    }
    #region Playfab
    public void TurnOnPlayfabSignin(PlayerType tpt)
    {
        pt = tpt;
        onlineCheckScreen.SetActive(true);
    }
    public void RequestOnline()
    {
#if UNITY_ANDROID
        var request = new LoginWithAndroidDeviceIDRequest { CreateAccount = true };
        PlayFabClientAPI.LoginWithAndroidDeviceID(request, OnLoginSuccess, OnLoginFailure);
#elif UNITY_STANDALONE_WIN
        var request = new LoginWithCustomIDRequest { CustomId = SystemInfo.deviceUniqueIdentifier, CreateAccount = true };
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
#elif UNITY_EDITOR
        var request = new LoginWithCustomIDRequest { CustomId = SystemInfo.deviceUniqueIdentifier, CreateAccount = true };
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
#endif
    }
    private void OnLoginSuccess(LoginResult result)
    {
        if (PlayerPrefs.HasKey("username"))
        {
            PlayOnline();
        }
        else
        {
            userIDScreen.SetActive(true);
        }
    }
    private void OnLoginFailure(PlayFabError error)
    {
        Debug.Log(error.ErrorMessage.ToString());
        loginFailureScreen.SetActive(true);
    }
    public void ConfirmUsername()
    {
        var request = new UpdateUserTitleDisplayNameRequest { DisplayName = usernameString };
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnUsernameSuccess, OnUsernameFailure);
    }
    public void OnUsernameType()
    {
        usernameString = usernameInput.text;
    }
    private void OnUsernameSuccess(UpdateUserTitleDisplayNameResult result)
    {
        StartCoroutine(UIDChange(true));
        PlayerPrefs.SetString("username", result.DisplayName.ToString());
        startButton.SetActive(true);
    }
    private void OnUsernameFailure(PlayFabError error)
    {
        StartCoroutine(UIDChange(false));
    }
    private IEnumerator UIDChange(bool state)
    {
        if (state)
        {
            userIDSuccess.SetActive(true);
            userIDFailure.SetActive(false);
            yield return new WaitForSeconds(2f);
            userIDSuccess.SetActive(false);
        }
        else
        {
            userIDSuccess.SetActive(false);
            userIDFailure.SetActive(true);
            yield return new WaitForSeconds(2f);
            userIDFailure.SetActive(false);
        }
    }
    public void PlayOnline()
    {
        gm.online = true;
        gm.StartGame(pt);
    }
    public void PlayOffline()
    {
        gm.online = false;
        gm.StartGame(pt);
    }
    #endregion Playfab
    public void ResetScene()
    {
        SceneManager.LoadSceneAsync(0);
    }
    public void EndGame()
    {
        Application.Quit();
    }
}
