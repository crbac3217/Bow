using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool isStarting;
    public PlayerType pt;
    public GameObject player;
    public List<Level> levels = new List<Level>();
    public LevelDataBase lvldata;

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
    public void LoadNextLevel(GameObject curplayer)
    {
        DontDestroyOnLoad(curplayer);
        player = curplayer;
        levels.Remove(levels[0]);
        SceneManager.LoadSceneAsync(1);
    }
    public void DestroyPrevPlayer()
    {
        if (player)
        {
            Destroy(player);
        }
    }
    
}

