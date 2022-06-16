using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Audio;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;

public class MainOptionsPanel : MonoBehaviour
{
    public AudioMixer mixer;
    public AudioSource audioS;
    public Slider bgmBar, sfxBar;
    public GameObject joySetting, controlSetting, joySettingPanel, playFabSuccessful, playFabUnsuccessful, uIDChangeSuccess, uIDChangeFailure;
    public TMP_InputField playfabInputfield;
    private string uid;
    public void SetUp()
    {
        SetUpVolume();
        SetUpDeviceSetting();
        SetUpPlayfab();
    }
    #region volume
    public void SetUpVolume()
    {
        float bgmVal, sfxVal;
        if (PlayerPrefs.HasKey("bgmVol"))
        {
            mixer.SetFloat("bgmVol", PlayerPrefs.GetFloat("bgmVol"));
        }
        if (PlayerPrefs.HasKey("sfxVol"))
        {
            mixer.SetFloat("sfxVol", PlayerPrefs.GetFloat("sfxVol"));
        }
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
    #endregion volume
    private void SetUpDeviceSetting()
    {
#if UNITY_ANDROID
        joySetting.SetActive(true);
        controlSetting.SetActive(false);
#elif UNITY_STANDALONE_WIN
        joySetting.SetActive(false);
        controlSetting.SetActive(true);
#elif UNITY_EDITOR
        joySetting.SetActive(false);
        controlSetting.SetActive(true);
#endif
    }
    #region joystick
    public void OnJoyEditPress()
    {
        audioS.Play();
        joySettingPanel.SetActive(true);
        joySettingPanel.GetComponent<JoySettingPanel>().SetUp();
    }
    #endregion joystick
    #region pc
    #endregion pc
    #region playfab
    private void SetUpPlayfab()
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
        playFabSuccessful.SetActive(true);
        playFabUnsuccessful.SetActive(false);
    }
    private void OnLoginFailure(PlayFabError error)
    {
        Debug.Log(error.ErrorMessage.ToString());
        playFabUnsuccessful.SetActive(true);
        playFabSuccessful.SetActive(false);
    }
    public void OnInputChange()
    {
        uid = playfabInputfield.text;
    }
    public void ConfirmUsername()
    {
        audioS.Play();
        var request = new UpdateUserTitleDisplayNameRequest { DisplayName = uid };
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnUsernameSuccess, OnUsernameFailure);
    }
    private void OnUsernameSuccess(UpdateUserTitleDisplayNameResult result)
    {
        StartCoroutine(UIDChange(true));
        PlayerPrefs.SetString("username", result.DisplayName.ToString());
    }
    private void OnUsernameFailure(PlayFabError error)
    {
        StartCoroutine(UIDChange(false));
    }
    private IEnumerator UIDChange(bool state)
    {
        if (state)
        {
            uIDChangeSuccess.SetActive(true);
            uIDChangeFailure.SetActive(false);
            yield return new WaitForSeconds(2f);
            uIDChangeSuccess.SetActive(false);
        }
        else
        {
            uIDChangeSuccess.SetActive(false);
            uIDChangeFailure.SetActive(true);
            yield return new WaitForSeconds(2f);
            uIDChangeFailure.SetActive(false);
        }
    }
#endregion playfab


    public void CloseButton()
    {
        audioS.Play();
        this.gameObject.SetActive(false);
#if UNITY_STANDALONE_WIN
        foreach (ChangeControl cc in controlSetting.GetComponentsInChildren<ChangeControl>())
        {
            cc.ChangeOff();
        }
#endif
    }
}
