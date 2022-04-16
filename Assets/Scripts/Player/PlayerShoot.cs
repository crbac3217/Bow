using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public PlayerControl pc;
    public PlayerMove pm;
    public FixedJoystick fixedJoystick;
    public List<Modifier> shootMods = new List<Modifier>();
    public List<Modifier> killMods = new List<Modifier>();
    public LineRenderer projLine;
    public Shoot activeShoot, defaultShoot;
    public Melee activeMelee, defaultMelee;
    public Transform bowArm, firePoint;
    public Quaternion ArmInitialDeg;
    public float speedMultiplier, indicatorMultiplier, straightMultiplier, coolDown, ylimit, accuracyMult;
    public bool isVisualized = false, canShoot = true;
    public Material visualizerMaterial;
    public LayerMask groundMask;

    public void Begin()
    {
        fixedJoystick = GameObject.FindGameObjectWithTag("Joystick").GetComponent<FixedJoystick>();
        fixedJoystick.defaultCd = coolDown;
        ArmInitialDeg = bowArm.rotation;
        pc = GetComponent<PlayerControl>();
        fixedJoystick.campar = pc.campar;
        pm = GetComponent<PlayerMove>();
    }
    public void Attack(Vector2 joyDir, float joyHeld, int joyIndicator)
    {
        float angle = Mathf.Atan2(-joyDir.y, -joyDir.x) * Mathf.Rad2Deg;
        AttackArgs attackArg = new AttackArgs
        {
            multval = joyHeld / fixedJoystick.indicator[joyIndicator] + (joyIndicator * indicatorMultiplier),
            angle = angle,
            dir = -joyDir,
            damageMult = 0,
            heldPercent = joyHeld / fixedJoystick.indicator[joyIndicator],
            firePos = firePoint.transform.position,
            apc = pc,
            accuracyVal = pc.stats[4].value * accuracyMult,
            speedMult = speedMultiplier,
            projectile = Instantiate(activeShoot.projPrefab, firePoint.transform.position, Quaternion.AngleAxis(angle, Vector3.forward))
    };
        if (joyDir.normalized == Vector2.zero)
        {
            if (activeMelee == null)
            {
                activeMelee = Instantiate(defaultMelee);
            }
            activeMelee.InvokeMelee(attackArg);
            pc.pa.bodyAnim.ResetTrigger("meleeReleased");
            pc.pa.bodyAnim.SetTrigger("meleeReleased");
            pc.pa.DisableHead();
            pc.pa.DisableArm();
        }
        else
        {
            if (pc.pm.isRight == true)
            {
                if (-joyDir.normalized.x > 0)
                {
                    pc.pa.bodyAnim.ResetTrigger("shotReleased");
                    pc.pa.bodyAnim.SetTrigger("shotReleased");
                }
                else
                {
                    pc.pa.bodyAnim.ResetTrigger("revShotReleased");
                    pc.pa.bodyAnim.SetTrigger("revShotReleased");
                }
            }
            else
            {
                if (-joyDir.normalized.x < 0)
                {
                    pc.pa.bodyAnim.ResetTrigger("shotReleased");
                    pc.pa.bodyAnim.SetTrigger("shotReleased");
                }
                else
                {
                    pc.pa.bodyAnim.ResetTrigger("revShotReleased");
                    pc.pa.bodyAnim.SetTrigger("revShotReleased");
                }
            }
            if (activeShoot.isDefault)
            {
                for (int i = shootMods.Count-1; i >= 0; i--)
                {
                    shootMods[i].OnModifierActive(pc);
                    attackArg = shootMods[i].AttackArgMod(attackArg);
                }
            }
            pc.pa.DisableHead();
            pc.pa.DisableArm();
            activeShoot.InvokeShoot(attackArg);
        }
    }
    public void CancelShooting()
    {
        fixedJoystick.CancelShooting();
        pc.pa.DisableHead();
        pc.pa.DisableArm();
    }
    public void Projectory(Vector2 joyDir, int joyIndicator, float joyHeld)
    {
        float tempAngle = 0;
        bool tempIsgreater = false;
        if (joyDir.normalized.y < 0)
        {
            tempIsgreater = false;
            tempAngle = Vector3.Angle(new Vector3(-1.0f, 0.0f, 0.0f), new Vector3(joyDir.normalized.x, joyDir.normalized.y, 0.0f)) + 180f;
        }  
        else
        {
            tempIsgreater = true;
            tempAngle = Vector3.Angle(new Vector3(1.0f, 0.0f, 0.0f), new Vector3(joyDir.normalized.x, joyDir.normalized.y, 0.0f));
        }
        if (!isVisualized)
        {
            projLine.startColor = pc.playerType.tierColors[fixedJoystick.currentLevel];
            ShowVisualizer(true);
        }
        tempAngle *= Mathf.Deg2Rad;
        ProjArgs projargs = new ProjArgs
        {
            ylim = transform.position.y - 1,
            pmask = groundMask,
            pdir = joyDir,
            isgreater = tempIsgreater,
            angle = tempAngle,
            held = joyHeld,
            indicator = joyIndicator,
            firePos = firePoint.transform.position,
            apc = pc,
            speedMult = speedMultiplier,
            indiMult = indicatorMultiplier,
            joyIndi = fixedJoystick.indicator[joyIndicator],
            projLinei = projLine

        };
        activeShoot.projectory.InvokeProjectory(projargs);
    }
    public void FireLevelUp(int level)
    {
        projLine.startColor = pc.playerType.tierColors[level];
        visualizerMaterial.SetColor("MatColor", pc.playerType.tierColors[level]);
    }
    public void FireLevelReset()
    {
        projLine.startColor = pc.playerType.tierColors[1];
        visualizerMaterial.SetColor("MatColor", pc.playerType.tierColors[1]);
        ShowVisualizer(false);
    }
    public void ShowVisualizer(bool tf)
    {
        isVisualized = tf;
        projLine.gameObject.GetComponent<LineRenderer>().enabled = tf;
    }
}
public class AttackArgs : EventArgs
{
    public Vector2 dir;
    public float multval,heldPercent, angle, accuracyVal, damageMult;
    public Vector2 firePos;
    public PlayerControl apc;
    public float speedMult;
    public GameObject projectile;
}
public class ProjArgs : EventArgs
{
    public LayerMask pmask;
    public Vector2 pdir, firePos;
    public bool isgreater;
    public float angle, ylim, held, speedMult, indiMult, joyIndi;
    public int indicator;
    public PlayerControl apc;
    public LineRenderer projLinei;
}
