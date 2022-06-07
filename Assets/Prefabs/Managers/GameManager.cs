using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public PlayerType pt;
    public GameObject playerPrf, instPlayer, managerPar, joystick, buttons, camPar;
    public float transitionTime;
    public List<Animator> transitionAnims = new List<Animator>();
    public List<Level> levels = new List<Level>();
    public List<GameObject> donotdestroy = new List<GameObject>();
    public LevelDataBase lvldata;

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

    public void StartGame()
    {
        DontDestroyOnLoad(this.gameObject);
        foreach (PerLevels lvl in lvldata.levels)
        {
            AddToLevels(lvl.levels[Random.Range(0, lvl.levels.Count)]);
        }
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
        levels.Remove(levels[0]);
        SceneManager.LoadSceneAsync(1);
    }
}

