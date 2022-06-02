using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering.Universal;


public class LevelManager : MonoBehaviour
{
    public int level;
    public PlayerType playerType;
    public CameraParent cp;
    public GameObject managerPar, playerPrefab, camParPref, spawnPoint, ItemManagerObject, GUIManagerObject, DamageManagerObject, LootManagerObject, bgmManagerObject, boss;
    public PlayerControl pc;
    public ThemeDatabase theme;
    public EndType endtype;
    public GameManager gm;
    private List<Area> areas = new List<Area>();
    private ItemManager itemManager;
    private GUIManager guiManager;
    private DamageManager damageManager;
    private LootManager lootManager;
    public BGMManager bgmManager;
    public List<GameObject> enemies = new List<GameObject>();
    private void Start()
    {
        GetInfoFromGM();
        GenerateLevel();
        CreateConnections();
        OnSceneReset();
    }
    #region SetUp
    private void GetInfoFromGM()
    {
        gm = FindObjectOfType<GameManager>();
        if (gm)
        {
            playerType = gm.pt;
            playerPrefab = gm.playerPrf;
            theme = gm.levels[0].theme;
            level = gm.levels[0].level;
            endtype = gm.levels[0].endType;
            boss = gm.levels[0].boss;
            if (!gm.instPlayer)
            {
                SpawnCampar();
                SpawnManagers();
                SpawnPlayer();
                guiManager.SpawnGUI();
            }
            else
            {
                Destroy(managerPar);
                cp = gm.camPar.GetComponent<CameraParent>();
                managerPar = gm.managerPar;
                gm.instPlayer.transform.position = spawnPoint.transform.position;
                itemManager = managerPar.GetComponentInChildren<ItemManager>();
                guiManager = managerPar.GetComponentInChildren<GUIManager>();
                damageManager = managerPar.GetComponentInChildren<DamageManager>();
                lootManager = managerPar.GetComponentInChildren<LootManager>();
                bgmManager = managerPar.GetComponentInChildren<BGMManager>();
                pc = gm.instPlayer.GetComponent<PlayerControl>();
                pc.pm.LetGoLeft();
                pc.pm.LetGoRight();
                cp.player = pc.gameObject;
                ShopReset();
            }
            pc.levelManager = this;
            foreach (GameObject go in cp.lightsColorChange)
            {
                go.GetComponent<Light2D>().color = theme.themeColor[0];
            }
            cp.lightsColorChange[0].GetComponent<Light2D>().intensity = theme.backgLightIntensity;
            cp.backGround.GetComponent<SpriteRenderer>().sprite = theme.backGround[Random.Range(0, theme.backGround.Count)];
        }
        else
        {
            Debug.Log("A critical error happend :(");
        }
    }
    private void ShopReset()
    {
        int j = guiManager.sb.shopList.transform.childCount;
        for (int i = j-1; i >= 0; i--)
        {
            Destroy(guiManager.sb.shopList.transform.GetChild(i).gameObject);
        }
        guiManager.sb.items.Clear();
        
    }
    private void SpawnCampar()
    {
        var camparobj = Instantiate(camParPref, Vector2.zero, Quaternion.identity);
        cp = camparobj.GetComponent<CameraParent>();
        gm.camPar = camparobj;
        DontDestroyOnLoad(camparobj);
    }
    private void SpawnManagers()
    {
        var itemManagerObject = Instantiate(ItemManagerObject, managerPar.transform);
        itemManager = itemManagerObject.GetComponent<ItemManager>();
        var guiManagerObject = Instantiate(GUIManagerObject, managerPar.transform);
        guiManager = guiManagerObject.GetComponent<GUIManager>();
        var damageManagerObject = Instantiate(DamageManagerObject, managerPar.transform);
        damageManager = damageManagerObject.GetComponent<DamageManager>();
        var lootManagerObject = Instantiate(LootManagerObject, managerPar.transform);
        lootManager = lootManagerObject.GetComponent<LootManager>();
        var BgmManagerObject = Instantiate(bgmManagerObject, managerPar.transform);
        bgmManager = BgmManagerObject.GetComponent<BGMManager>();
        gm.transitionAnims.Add(bgmManager.GetComponent<Animator>());
        DontDestroyOnLoad(managerPar);
        gm.managerPar = managerPar;
    }
    private void SpawnPlayer()
    {
        var player = Instantiate(playerPrefab, spawnPoint.transform.position, Quaternion.identity);
        pc = player.GetComponent<PlayerControl>();
        pc.campar = cp;
        damageManager.pc = pc;
        itemManager.pc = pc;
        guiManager.pc = pc;
        pc.playerType = Instantiate(playerType);
        pc.damageManager = damageManager;
        pc.guiManager = guiManager;
        pc.itemManager = itemManager;
        cp.player = player;
    }
    private void OnSceneReset()
    {
        cp.doFollowPlayer = true;
        cp.cam.orthographicSize = 1.5f;
        float healAmount = (float)pc.stats[2].value / 10f;
        pc.Heal((int)healAmount);
        pc.ps.fixedJoystick.CancelShooting();
    }
    #endregion SetUp
    #region MapMaking
    public void GenerateLevel()
    {
        AudioClip rand = theme.audios[Random.Range(0, theme.audios.Count)];
        bgmManager.audioSource.clip = rand;
        bgmManager.audioSource.Play();
        CreateStartArea();
        //change this
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