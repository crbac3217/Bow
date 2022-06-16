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
    public int maxRankings;
    private bool isScore = true, online = false;

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
        for (int i = j-1; i >= 0; i--)
        {
            Destroy(rankPanel.transform.GetChild(i).gameObject);
        }
        rankPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(rankPanel.GetComponent<RectTransform>().sizeDelta.x, 0);
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
        var request = new GetLeaderboardRequest();
        request.StatisticName = "Highscore";
        request.StartPosition = 0;
        request.MaxResultsCount = maxRankings;
        request.ProfileConstraints = new PlayerProfileViewConstraints() { ShowStatistics = true, ShowDisplayName = true };
        PlayFabClientAPI.GetLeaderboard(request, LeaderboardSuccess, LeaderboardError);
    }
    private void TimeScene()
    {
        var request = new GetLeaderboardRequest();
        request.StatisticName = "Timescore";
        request.StartPosition = 0;
        request.MaxResultsCount = maxRankings;
        request.ProfileConstraints = new PlayerProfileViewConstraints() { ShowStatistics = true, ShowDisplayName = true };
        PlayFabClientAPI.GetLeaderboard(request, TimeLeaderboardSuccess, LeaderboardError);
    }
    private void LeaderboardSuccess(GetLeaderboardResult result)
    {
        foreach (PlayerLeaderboardEntry entry in result.Leaderboard)
        {
            var inst = Instantiate(rankPref, rankPanel.transform);
            var rect = rankPanel.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(rect.sizeDelta.x, rect.sizeDelta.y + 200f);
            RankPanel rp = inst.GetComponent<RankPanel>();
            rp.rank.text = "#"+(entry.Position + 1)+".";
            rp.username.text = entry.Profile.DisplayName;
            rp.score.text = entry.StatValue.ToString();
            foreach (var stat in entry.Profile.Statistics)
            {
                if (stat.Name == "HighscoreChar")
                {
                    foreach (PlayerType pt in allTypes)
                    {
                        if (stat.Value == pt.charNum)
                        {
                            rp.charName = pt.typeName;
                        }
                    }
                }
                for (int i = 1; i <= 4; i++)
                {
                    if (stat.Name == "HighscoreSkill" + i.ToString())
                    {
                        foreach (Skill sk in allSkills)
                        {
                            if (sk.skillNum == stat.Value)
                            {
                                rp.skillImages[i - 1] = sk.thumbnail;
                            }
                        }
                    }
                }
            }
            EventTrigger trigger = rp.GetComponent<EventTrigger>();
            EventTrigger.Entry upEntry = new EventTrigger.Entry();
            upEntry.eventID = EventTriggerType.PointerEnter;
            upEntry.callback.AddListener((eventdata) => DetailsUpdate(rp.skillImages, rp.charName));
            trigger.triggers.Add(upEntry);
            EventTrigger.Entry downEntry = new EventTrigger.Entry();
            downEntry.eventID = EventTriggerType.PointerExit;
            downEntry.callback.AddListener((eventdata) => DetailsOff());
            trigger.triggers.Add(downEntry);
        }
    }
    private void TimeLeaderboardSuccess(GetLeaderboardResult result)
    {
        foreach (PlayerLeaderboardEntry entry in result.Leaderboard)
        {
            var inst = Instantiate(rankPref, rankPanel.transform);
            var rect = rankPanel.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(rect.sizeDelta.x, rect.sizeDelta.y + 200f);
            RankPanel rp = inst.GetComponent<RankPanel>();
            rp.rank.text = "#" + (entry.Position + 1) + ".";
            rp.username.text = entry.DisplayName;
            float temp = (float)entry.StatValue;
            rp.score.text = (temp / -100f).ToString();
            foreach (var stat in entry.Profile.Statistics)
            {
                if (stat.Name == "HighscoreChar")
                {
                    foreach (PlayerType pt in allTypes)
                    {
                        if (stat.Value == pt.charNum)
                        {
                            rp.charName = pt.typeName;
                        }
                    }
                }
                for (int i = 1; i <= 4; i++)
                {
                    if (stat.Name == "TimescoreSkill" + i.ToString())
                    {
                        foreach (Skill sk in allSkills)
                        {
                            if (sk.skillNum == stat.Value)
                            {
                                rp.skillImages[i - 1] = sk.thumbnail;
                            }
                        }
                    }
                }
            }
            EventTrigger trigger = rp.GetComponent<EventTrigger>();
            EventTrigger.Entry upEntry = new EventTrigger.Entry();
            upEntry.eventID = EventTriggerType.PointerEnter;
            upEntry.callback.AddListener((eventdata) => DetailsUpdate(rp.skillImages, rp.charName));
            trigger.triggers.Add(upEntry);
            EventTrigger.Entry downEntry = new EventTrigger.Entry();
            downEntry.eventID = EventTriggerType.PointerExit;
            downEntry.callback.AddListener((eventdata) => DetailsOff());
            trigger.triggers.Add(downEntry);
        }
    }
    private void LeaderboardError(PlayFabError error)
    {
        Debug.LogError(error);
        rankPanel.SetActive(false);
        updateFailed.SetActive(true);
    }
    private void ScoreOn()
    {
        isScore = true;
        scorebuttonimg.color = Color.white;
        timebuttonimg.color = Color.black;
        scoretext.color = Color.white;
        timetext.color = Color.gray;
    }
    private void TimeOn()
    {
        isScore = false;
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
        for (int i = 0; i <= skillImages.Length - 1; i++)
        {
            rd.images[i].sprite = skillImages[i];
        }
        rd.character.text = "Character Played : " + charName;
    }
    public void DetailsOff()
    {
        detailsPanel.SetActive(false);
    }
    public void ReturnToMainMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
