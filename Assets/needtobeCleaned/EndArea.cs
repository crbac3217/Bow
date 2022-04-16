using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndArea : Area
{
    public EndType endtype;
    public GameObject endingPortal, endSpawnPos, bossOrShopPos, bossPref, ShopPref, bossInst;
    public CameraParent campar;
    private Vector3 velocity = Vector2.zero;
    public Vector3 endAreaCam;
    public bool defeatedBoss, enteredBossfight, bossSpawned;
    private BossController bc;
    private BossAi bai;

    public override void SetUp()
    {
        bossSpawned = false;
        defeatedBoss = false;
        enteredBossfight = false;
        base.SetUp();
        endAreaCam = new Vector3((transform.position.x + end.transform.position.x) / 2, (transform.position.y + end.transform.position.y) / 2 + 1, -10);
    }
    public override void CheckForPlayerposMob()
    {
        base.CheckForPlayerposMob();
        if (endtype == EndType.Boss)
        {
            if (pc.transform.position.x > transform.position.x - 3 && !bossSpawned)
            {
                SpawnBoss();
            }
            if (pc.transform.position.x > transform.position.x + 1 && !defeatedBoss && !enteredBossfight)
            {
                campar.doFollowPlayer = false;
                Vector3 movePoint = Vector3.SmoothDamp(campar.transform.position, endAreaCam, ref velocity, 0.3f);
                campar.transform.position = movePoint;
                campar.cam.orthographicSize = Mathf.Clamp(campar.cam.orthographicSize + 0.01f, 1.5f, 2f);
                if (Vector3.Distance(campar.transform.position, endAreaCam) < 0.001f && campar.cam.orthographicSize > 1.99f)
                {
                    campar.transform.position = endAreaCam;
                    campar.cam.orthographicSize = 2;
                    enteredBossfight = true;
                    pc.guiManager.bossBar.SetActive(true);
                    bc.enabled = true;
                    bai.enabled = true;
                }
            }
        }
    }
    private void SpawnBoss()
    {
        bossInst = Instantiate(bossPref, bossOrShopPos.transform.position, Quaternion.identity);
        bc = bossInst.GetComponent<BossController>();
        bai = bossInst.GetComponent<BossAi>();
        bai.ea = this;
        bai.pc = pc;
        Debug.Log("setup");
        bai.campar = campar;
        bc.dm = dm;
        bc.lm = lm;
        bc.damageCrits = crits;
        bc.lvlm = lvlm;
        bc.ea = this;
        bc.pc = pc;
        pc.guiManager.BossHPBarSpawn(bc) ;
        pc.guiManager.BossHPBarUpdate(bc);
        pc.guiManager.bossBar.SetActive(false);
        bc.enabled = false;
        bai.enabled = false;
        bossSpawned = true;
    }
}
