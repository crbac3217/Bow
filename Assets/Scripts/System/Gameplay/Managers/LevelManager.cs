using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int level;
    public PlayerType playerType;
    public CameraParent cp;
    public bool shouldInitialize;
    public GameObject playerPrefab, spawnPoint, ItemManagerObject, GUIManagerObject, DamageManagerObject, LootManagerObject, boss;
    public PlayerControl pc;
    public ThemeDatabase theme;
    public EndType endtype;
    public GameManager gm;
    private List<Area> areas = new List<Area>();
    private ItemManager itemManager;
    private GUIManager guiManager;
    private DamageManager damageManager;
    private LootManager lootManager;
    public List<GameObject> enemies = new List<GameObject>();
    private void Start()
    {
        GetInfoFromGM();
        SpawnManagers();
        SpawnPlayer();
        GenerateLevel();
        CreateConnections();
    }
    #region SetUp
    private void GetInfoFromGM()
    {
        gm = FindObjectOfType<GameManager>();
        if (gm)
        {
            if (gm.isStarting)
            {
                shouldInitialize = true;
                gm.isStarting = false;
            }
            playerType = gm.pt;
            playerPrefab = gm.player;
            theme = gm.levels[0].theme;
            level = gm.levels[0].level;
            endtype = gm.levels[0].endType;
            boss = gm.levels[0].boss;
        }
        else
        {
            Debug.Log("A critical error happend :(");
        }
    }
    private void SpawnManagers()
    {
        var itemManagerObject = Instantiate(ItemManagerObject, this.transform);
        itemManager = itemManagerObject.GetComponent<ItemManager>();
        var guiManagerObject = Instantiate(GUIManagerObject, this.transform);
        guiManager = guiManagerObject.GetComponent<GUIManager>();
        var damageManagerObject = Instantiate(DamageManagerObject, this.transform);
        damageManager = damageManagerObject.GetComponent<DamageManager>();
        var lootManagerObject = Instantiate(LootManagerObject, this.transform);
        lootManager = lootManagerObject.GetComponent<LootManager>();
    }
    private void SpawnPlayer()
    {
        var player = Instantiate(playerPrefab, spawnPoint.transform.position, Quaternion.identity);
        gm.DestroyPrevPlayer();
        pc = player.GetComponent<PlayerControl>();
        pc.levelManager = this;
        damageManager.pc = pc;
        itemManager.pc = pc;
        guiManager.pc = pc;
        pc.playerType = Instantiate(playerType);
        pc.damageManager = damageManager;
        pc.guiManager = guiManager;
        pc.itemManager = itemManager;
        cp.player = player;
        pc.campar = cp;
    }
    #endregion SetUp
    #region MapMaking
    public void GenerateLevel()
    {
        CreateStartArea();
        //change to 5 and 2 later
        int areaAmount = 0 + (level * 0);
        for (int i = 0; i < areaAmount; i++)
        {
            CreateMidArea(areas[i].end.transform.position);
        }
        CreateEndArea(areas[areas.Count - 1].end.transform.position);
        foreach (Area area in areas)
        {
            foreach (Platform pf in area.pf)
            {
                pf.SetNode();
            }
        }
    }
    private void CreateStartArea()
    {
        int randIndex = Random.Range(0, theme.startAreas.Count);
        var startArea = Instantiate(theme.startAreas[randIndex], Vector2.zero, Quaternion.identity);
        spawnPoint = startArea.GetComponent<StartArea>().spawnPosition;
        areas.Add(startArea.GetComponent<Area>());
        AreaSetting(startArea.GetComponent<Area>());
        cp.xBoundaryL = startArea.transform.position.x + 2;
    }
    private void CreateMidArea(Vector2 startpos)
    {
        int randIndex = Random.Range(0, theme.middleAreas.Count);
        var midArea = Instantiate(theme.middleAreas[randIndex], startpos, Quaternion.identity);
        areas.Add(midArea.GetComponent<Area>());
        AreaSetting(midArea.GetComponent<Area>());
    }
    private void CreateEndArea(Vector2 startpos)
    {
        int randIndex = Random.Range(0, theme.endAreas.Count);
        var endArea = Instantiate(theme.endAreas[randIndex], startpos, Quaternion.identity);
        EndArea ea = endArea.GetComponent<EndArea>();
        ea.endtype = endtype;
        areas.Add(endArea.GetComponent<Area>());
        AreaSetting(endArea.GetComponent<Area>());
        ea.campar = cp;
        ea.bossPref = boss;
        cp.xBoundaryR = endArea.GetComponent<Area>().end.transform.position.x - 2;
        cp.yBoundaryU = endArea.GetComponent<Area>().end.transform.position.y + 3;
    }
    private void AreaSetting(Area area)
    {
        area.lvlm = this;
        area.level = level;
        area.dm = damageManager;
        area.lm = lootManager;
        area.crits = playerType.critList;
        area.loaded = true;
    }
    private void CreateConnections()
    {
        foreach (Area area in areas)
        {
            foreach (Platform pf in area.pf)
            {
                pf.SetUpConnection(1f);
            }
            area.pc = pc;
        }
    }
    #endregion MapMaking
}