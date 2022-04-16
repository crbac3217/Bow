using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour
{
    public List<GameObject> enemies;
    public List<Platform> pf = new List<Platform>();
    public LevelManager lvlm;
    public List<MobSpawnPoint> msps = new List<MobSpawnPoint>();
    public GameObject end;
    public int level;
    public bool loaded;
    public PlayerControl pc;
    public DamageManager dm;
    public LootManager lm;
    private int mobDelayCounter;
    public Crit[] crits = new Crit[] { };
    private void Start()
    {
        SetUp();
    }
    public virtual void SetUp()
    {

    }
    private void Update()
    {
        CheckForPlayerposMob();
        CheckForPlayerposChunkload();
    }
    public virtual void CheckForPlayerposMob()
    {
        if (pc.transform.position.x > transform.position.x - 2)
        {
            if (msps.Count > 0)
            {
                mobDelayCounter++;
                if (mobDelayCounter == 5)
                {
                    SpawnMobs();
                    mobDelayCounter = 0;
                }
            }
        }
    }
    public virtual void CheckForPlayerposChunkload()
    {
        if (Vector2.Distance(pc.transform.position, transform.position) < 15 || Vector2.Distance(pc.transform.position, end.transform.position) < 15)
        {
            if (!loaded)
            {
                foreach (Platform pf in pf)
                {
                    pf.gameObject.SetActive(true);
                }
                loaded = true;
            }
        }
        else
        {
            if (loaded)
            {
                foreach (Platform pf in pf)
                {
                    pf.gameObject.SetActive(false);
                }
                loaded = false;
            }
        }
    }
    public virtual void SpawnMobs()
    {
        Spawn(msps[0].transform.position);
        msps.Remove(msps[0]);
    }
    public void Spawn(Vector2 pos)
    {
        int randinDex = Random.Range(0, enemies.Count);
        GameObject temp = Instantiate(enemies[randinDex], pos, Quaternion.identity);
        EnemyController ec = temp.GetComponent<EnemyController>();
        lvlm.enemies.Add(temp);
        if (lvlm.enemies.Count > 10)
        {
            lvlm.enemies[0].GetComponent<EnemyController>().Dead(false);
        }
        SetDifficulty(ec);
    }
    private void SetDifficulty(EnemyController ec)
    {
        ec.dm = dm;
        ec.damageCrits = crits;
        ec.lm = lm;
        ec.GetComponent<AiHandler>().damage *= level;
        ec.GetComponent<AiHandler>().pc = pc;
        ec.lvlm = lvlm;
        ec.chestTier = level;
        if (level > 1)
        {
            ec.maxHp += Mathf.RoundToInt(ec.maxHp * 0.5f * level);
            ec.minGoldDrop += Mathf.RoundToInt(ec.minGoldDrop * 0.5f * level);
            ec.maxGoldDrop += Mathf.RoundToInt(ec.maxGoldDrop * 0.5f * level);
            foreach (DamageType dt in ec.strength)
            {
                float val = dt.value + (dt.value * level * 0.5f);
                dt.value = Mathf.RoundToInt(val);
            }
        }
    }
}
