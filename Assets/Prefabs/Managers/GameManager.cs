using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [System.NonSerialized]
    public PlayerType pt;
    public GameObject playerPrf, instPlayer, managerPar, joystick, buttons, camPar;
    public float transitionTime, startTime;
    public List<Animator> transitionAnims = new List<Animator>();
    public List<Level> levels = new List<Level>();
    public List<GameObject> donotdestroy = new List<GameObject>();
    public LevelDataBase lvldata;
    public LevelManager lvlM;
    public bool online = false;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("MoveX"))
        {
            SetUpGUIPref();
        }
    }

    private void SetUpGUIPref()
    {
        PlayerPrefs.SetFloat("MoveX", 200f);
        PlayerPrefs.SetFloat("MoveY", 250f);
        PlayerPrefs.SetFloat("JoyX", -280f);
        PlayerPrefs.SetFloat("JoyY", 230f);
    }

    public void StartGame(PlayerType type)
    {
        DontDestroyOnLoad(this.gameObject);
        pt = type;
        foreach (PerLevels lvl in lvldata.levels)
        {
            AddToLevels(lvl.levels[Random.Range(0, lvl.levels.Count)]);
        }
        startTime = Time.time;
        SceneManager.LoadSceneAsync(1);
    }
    private void AddToLevels(Level lvl)
    {
        for (int i = 0; i < 3; i++)
        {
            Level temp = new Level
            {
                theme = lvl.theme,
                endType = (EndType)i,
                boss = lvl.boss,
                level = lvl.level
            };
            levels.Add(temp);
        }
    }
    public void LoadNextLevel()
    {
        StartCoroutine(NextLevelAnim());
    }
    private IEnumerator NextLevelAnim()
    {
        foreach (Animator anim in transitionAnims)
        {
            anim.SetTrigger("Transition");
        }
        yield return new WaitForSeconds(transitionTime);
        if (levels.Count > 0)
        {
            levels.Remove(levels[0]);
        }
        else
        {
            Victory();
        }
        
        SceneManager.LoadSceneAsync(1);
    }
    public void GameReset()
    {
        for (int i = donotdestroy.Count-1; i >= 0; i--)
        {
            Destroy(donotdestroy[i]);
        }
        Destroy(this.gameObject);
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync(0);
    }
    public void Victory()
    {
        float score = Mathf.Round((Time.time - startTime) * 100) / 100f;
        lvlM.pc.guiManager.VictoryScreenInit(score);
    }
}

