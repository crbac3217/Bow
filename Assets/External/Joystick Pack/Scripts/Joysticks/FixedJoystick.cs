using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FixedJoystick : Joystick
{
    protected Joystick joystick;
    public PlayerControl pc;
    public PlayerShoot ps;
    public CameraParent campar;
    public Camera camTemp;
    public bool isPressed = false, Avail = true;
    private Vector2 tempvec;
    public int currentLevel = 1, maxLevelsOfShooting;
    public Dictionary<int, float> indicator = new Dictionary<int, float>();
    private float holdtime, lastShot;
    public float defaultCd;
    public bool armRotate = false;
    public GameObject progressBar, currentBar, handleObj, handleCD;

    private void Awake()
    {
        joystick = this;
    }
    protected override void Start()
    {
        base.Start();
        handleObj = transform.GetChild(0).gameObject;
        handleCD = transform.GetChild(1).gameObject;
        progressBar = Resources.Load("Prefabs/ProgressBar") as GameObject;
    }
    //when handle is pressed
    public override void OnPointerDown(PointerEventData eventData)
    {
        //if there is no shoot active, we are going to fire our defaultShoot
        if (!ps.activeShoot)
        {
            ps.activeShoot = Instantiate(ps.defaultShoot);
        }
        InitializeShooting();
        base.OnPointerDown(eventData);
    }
    private void InitializeShooting()
    {
        pc.pa.bodyAnim.SetTrigger("joyPressed");
        if (pc.pm.isMoving || !pc.pj.isGrounded)
        {
            pc.pf.Freeze();
        }
        //initialize shooting sequence according to the active "shoot", setting up max levels and how long it will take to reach the max level
        indicator.Clear();
        maxLevelsOfShooting = ps.activeShoot.levelsOfShooting;
        for (int i = 0; i <= maxLevelsOfShooting; i++)
        {
            indicator.Add(i, i * 2.5f);
        }
        FireLevelUp(1);
        isPressed = true;
    }
    private void FixedUpdate()
    {
        //Cooldown Timer visualization
        if (!Avail)
        {
            handleCD.GetComponent<Image>().fillAmount = (Time.time - lastShot) / defaultCd;
            if (Time.time >= lastShot + defaultCd)
            {
                EnableShooting();
                handleCD.GetComponent<Image>().fillAmount = 0;
            }
        }
        //when held down
        if (isPressed)
        {
            if (Avail)
            {
                //holdtime increase according to the character status;
                if (pc.pf.isFrozen)
                {
                    holdtime = Mathf.Clamp(Time.deltaTime * 1.5f + holdtime, 0, indicator[currentLevel] - indicator[currentLevel-1]);
                    if (campar.doFollowPlayer)
                    {
                        camTemp.orthographicSize = Mathf.Clamp(camTemp.orthographicSize + 0.005f, 1.5f, 2);
                    }
                }
                else
                {
                    holdtime = Mathf.Clamp(Time.deltaTime + holdtime, 0, indicator[currentLevel] - indicator[currentLevel-1]);
                    if (campar.doFollowPlayer)
                    {
                        camTemp.orthographicSize = Mathf.Clamp(camTemp.orthographicSize + 0.005f, 1.5f, 2);
                    }
                }
                //arms and head rotation;
                tempvec = this.Direction;
                if (pc.pm.isRight == true)
                {
                    pc.pa.bodyAnim.SetFloat("joyXval", -tempvec.normalized.x);
                    pc.pa.bodyAnim.SetFloat("joyYval", -tempvec.normalized.y);
                    pc.pa.headAnim.SetFloat("joyXval", -tempvec.normalized.x);
                    pc.pa.headAnim.SetFloat("joyYval", -tempvec.normalized.y);
                }
                else
                {
                    pc.pa.bodyAnim.SetFloat("joyXval", tempvec.normalized.x);
                    pc.pa.bodyAnim.SetFloat("joyYval", tempvec.normalized.y);
                    pc.pa.headAnim.SetFloat("joyXval", tempvec.normalized.x);
                    pc.pa.headAnim.SetFloat("joyYval", -tempvec.normalized.y);
                }
                //level per holding, and it's visualization
                if (holdtime + indicator[currentLevel - 1] > indicator[currentLevel] && currentLevel < maxLevelsOfShooting)
                {
                    currentLevel++;
                    Mathf.Clamp(currentLevel, 0, maxLevelsOfShooting);
                    FireLevelUp(currentLevel);
                    ps.FireLevelUp(currentLevel);
                    holdtime = 0;
                }
                //visualization
                if (holdtime + indicator[currentLevel - 1] < indicator[currentLevel] && currentLevel <= maxLevelsOfShooting)
                {
                    float progressRate = holdtime / (indicator[currentLevel] - indicator[currentLevel - 1]);
                    currentBar.GetComponent<Image>().fillAmount = progressRate;
                }
                //head and arm appear and disappear according to the joystick angle
                //if the animator has entered the armRotate Mode
                if (armRotate)
                {
                    if (this.Direction != Vector2.zero)
                    {
                        ps.Projectory(-this.Direction, currentLevel, holdtime);
                        pc.pa.RotateArm(this.Direction);
                        if (!pc.pa.headSprite.enabled)
                        {
                            pc.pa.EnableHead();
                            pc.pa.EnableArm();
                        }
                    }
                    else if (this.Direction == Vector2.zero)
                    {
                        ps.ShowVisualizer(false);
                        if (pc.pa.headSprite.enabled)
                        {
                            pc.pa.DisableHead();
                            pc.pa.DisableArm();
                        }
                    }
                }
            }
        }
        else 
        {
            if (camTemp && campar)
            {
                if (camTemp.orthographicSize > 1.5f && campar.doFollowPlayer)
                {
                    camTemp.orthographicSize = Mathf.Clamp(camTemp.orthographicSize - 0.03f, 1.5f, 2);
                }
            }
        } 
    }
    public void DisableShooting()
    {
        Avail = false;
        ResetShootdata();
    }
    public void EnableShooting()
    {
        Avail = true;
    }
    public IEnumerator DisableShootingForSeconds(float time)
    {
        DisableShooting();
        yield return new WaitForSeconds(time);
        EnableShooting();
    }
    public void FireLevelUp(int level)
    {
        GameObject bar = Instantiate(progressBar, handleObj.transform);
        Vector3 scale = bar.transform.localScale;
        bar.transform.localScale = new Vector3(scale.x + (0.15f * level), scale.y + (0.15f * level), scale.z + (0.15f * level));
        bar.GetComponent<Image>().fillAmount = 0;
        bar.GetComponent<Image>().color = pc.playerType.tierColors[level];
        currentBar = bar;
    }
    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        if (isPressed)
        {
            isPressed = false;
            ps.Attack(tempvec, holdtime, currentLevel);
            campar.StartCoroutine(campar.CamShake(tempvec*holdtime*0.1f, 0.005f* holdtime));
            if (ps.activeShoot.isDefault == true)
            {
                lastShot = Time.time;
                DisableShooting();
            }
            else
            {
                ResetShootdata();
            }
            ps.activeShoot = null;
            defaultCd = ps.coolDown;
        }
    }
    public void CancelShooting()
    {
        isPressed = false;
        lastShot = Time.time;
        DisableShooting();
        foreach (Skill skill in ps.pc.skills)
        {
            if (skill)
            {
                skill.ShotSelectedToggleOff(ps.pc);
            }
        }
        ps.activeShoot = null;
        defaultCd = ps.coolDown;
    }
    public void ResetShootdata()
    {
        ps.bowArm.rotation = ps.ArmInitialDeg;
        armRotate = false;
        foreach (Transform child in handleObj.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        pc.pf.UnFreeze();
        ps.FireLevelReset();
        currentLevel = 1;
        holdtime = 0;
        tempvec = Vector2.zero;
    }
}