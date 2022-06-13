using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using PlayFab.ClientModels;
using PlayFab;
using UnityEngine.SceneManagement;

public class RankingScene : MonoBehaviour
{
    public GameObject rankPanel, updateFailed, rankPref, detailsPanel;
    public Image scorebuttonimg, timebuttonimg;
    public TextMeshProUGUI scoretext, timetext;
    public List<Skill> allSkills;
    public List<PlayerType> allTypes;
    public bool isScore = true, online = false;

    private void Start()
    {
        ClearRankScene();
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
        if (isScore)
        {
            ScoreOn();
        }
        else
        {
            TimeOn();
        }
    }
    public void ClearRankScene()
    {
        int j = rankPanel.transform.childCount;
        for (int i = j-1; i < 0; i--)
        {
            Destroy(rankPanel.transform.GetChild(i).gameObject);
        }
        detailsPanel.SetActive(false);
    }
    public void OnScorePress()
    {
        if (!isScore && online)
        {
            ScoreOn();
            ClearRankScene();
            ScoreScene();
        }
    }
    public void OnTimePress()
    {
        if (isScore && online)
        {
            TimeOn();
            ClearRankScene();
            TimeScene();
        }
    }
    private void ScoreScene()
    {

    }
    private void TimeScene()
    {

    }
    private void ScoreOn()
    {
        scorebuttonimg.color = Color.white;
        timebuttonimg.color = Color.black;
        scoretext.color = Color.white;
        timetext.color = Color.gray;
    }
    private void TimeOn()
    {
        timebuttonimg.color = Color.white;
        scorebuttonimg.color = Color.black;
        timetext.color = Color.white;
        scoretext.color = Color.gray;
    }
    private void OnLoginSuccess(LoginResult result)
    {
        if (isScore)
        {
            ScoreScene();
        }
        else
        {
            TimeScene();
        }
        online = true;
    }
    private void OnLoginFailure(PlayFabError error)
    {
        rankPanel.SetActive(false);
        updateFailed.SetActive(true);
    }
    public void DetailsUpdate(Sprite[] skillImages, string charName)
    {
        detailsPanel.SetActive(true);
        RankDetails rd = detailsPanel.GetComponent<RankDetails>();
        for (int i = 0; i < skillImages.Length - 1; i++)
        {
            rd.images[i].sprite = skillImages[i];
        }
        rd.character.text = "Character Played : " + charName;
    }
    public void ReturnToMainMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
