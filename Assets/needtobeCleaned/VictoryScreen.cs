using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using PlayFab.ClientModels;
using PlayFab;

public class VictoryScreen : MonoBehaviour
{
    public DamageManager dm;
    public PlayerControl pc;
    public ItemManager im;
    public GameManager gm;
    public Button button;
    public TextMeshProUGUI enemykilled, itemscollected, setsactivated, goldreserve, totalscore, timenumb, buttonText;
    public GameObject scorehigh, timehigh, scoreUpd, scoreFailed, scoreSuccess;
    public List<AudioClip> audios = new List<AudioClip>();
    private bool timescore, highscore = false;
    private AudioSource audioSource;
    private int totalScoreInt, timeInt;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void SetUp(float time)
    {
        enemykilled.text = "Enemies Killed : " + dm.killCount;
        totalScoreInt += dm.killCount;
        itemscollected.text = "Items Collected : " + im.itemCount + " X 2";
        totalScoreInt += (im.itemCount * 2);
        setsactivated.text = "Sets Activated : " + im.setCount + " X 3";
        totalScoreInt += im.setCount * 2;
        goldreserve.text = "Gold Reserve : " + pc.gold;
        totalScoreInt += pc.gold;
        totalscore.text = "Total Score : " + totalScoreInt;
        timenumb.text = time.ToString();
        if (PlayerPrefs.HasKey("Highscore"))
        {
            if (totalScoreInt > PlayerPrefs.GetInt("Highscore"))
            {
                UpdateLocalHighscore();
            }
        }
        else
        {
            UpdateLocalHighscore();
        }
        if (PlayerPrefs.HasKey("Timescore"))
        {
            if (time < PlayerPrefs.GetFloat("Timescore"))
            {
                UpdateLocalTimescore(time);
            }
            else
            {
                UpdateLocalTimescore(time);
            }
        }
    }
    private void UpdateLocalHighscore()
    {
        scorehigh.SetActive(true);
        highscore = true;
        PlayerPrefs.SetInt("Highscore", totalScoreInt);
    }
    private void UpdateLocalTimescore(float time)
    {
        timehigh.SetActive(true);
        timescore = true;
        PlayerPrefs.SetFloat("Timescore", time);
        timeInt = (int)time * 100;
    }
    public void AnimFinished()
    {
        if (gm.online)
        {
            if (timescore)
            {
                scoreUpd.SetActive(true);
                UpdateTimescore();
            }
            if (highscore)
            {
                scoreUpd.SetActive(true);
                UpdateHighscore();
            }
            if (!highscore && !timescore)
            {
                button.interactable = true;
                button.GetComponent<Image>().color = new Color(0.3862f, 0, 1, 1);
                buttonText.color = Color.white;
            }
        }
        else
        {
            button.interactable = true;
            button.GetComponent<Image>().color = new Color(0.3862f, 0, 1, 1);
            buttonText.color = Color.white;
        }
    }
    private void UpdateHighscore()
    {
        PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest()
        {
            FunctionName = "HighscoreUpdate",
            FunctionParameter = new { highscoreval = totalScoreInt },
            GeneratePlayStreamEvent = true,
        },
        OnHighUpdateSuccess, OnHighUpdateFail);
    }
    private void UpdateTimescore()
    {
        PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest()
        {
            FunctionName = "TimescoreUpdate",
            FunctionParameter = new { timescoreval = timeInt },
            GeneratePlayStreamEvent = true,
        },
        OnTimeUpdateSuccess, OnTimeUpdateFail);
    }
    private void OnHighUpdateSuccess(ExecuteCloudScriptResult result)
    {
        scoreUpd.SetActive(false);
        scoreSuccess.SetActive(true);
        button.interactable = true;
        button.GetComponent<Image>().color = new Color(0.3862f, 0, 1, 1);
        buttonText.color = Color.white;
    }
    private void OnHighUpdateFail(PlayFabError error)
    {
        scoreUpd.SetActive(false);
        scoreFailed.SetActive(true);
        button.interactable = true;
        button.GetComponent<Image>().color = new Color(0.3862f, 0, 1, 1);
        buttonText.color = Color.white;
    }
    
    private void OnTimeUpdateSuccess(ExecuteCloudScriptResult result)
    {
        scoreUpd.SetActive(false);
        scoreSuccess.SetActive(true);
        button.interactable = true;
        button.GetComponent<Image>().color = new Color(0.3862f, 0, 1, 1);
        buttonText.color = Color.white;
    }
    private void OnTimeUpdateFail(PlayFabError error)
    {
        scoreUpd.SetActive(false);
        scoreFailed.SetActive(true);
        button.interactable = true;
        button.GetComponent<Image>().color = new Color(0.3862f, 0, 1, 1);
        buttonText.color = Color.white;
    }
    public void OntoMain()
    {
        gm.GameReset();
    }
    public void PlaySound(int index)
    {
        audioSource.clip = audios[index];
        audioSource.Play();
    }
}
