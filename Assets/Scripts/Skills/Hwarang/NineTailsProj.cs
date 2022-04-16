using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class NineTailsProj : EnhanceObj
{
    public bool isNine;
    private Vector2 startpos;
    public GameObject projectile;
    public float multval;
    public override void SetUp()
    {
        startpos = transform.position;
        base.SetUp();
    }
    public override void OnHit(EnemyController ec, HwarangDefaultProjectile proj)
    {
        if (isNine)
        {
            for (int i = 0; i < 9; i++)
            {
                var temp = Instantiate(projectile, startpos, Quaternion.identity);
                var tempProj = temp.GetComponent<TargettedProjectile>();
                tempProj.speed = 1 + (i * 0.8f);
                tempProj.disp = i * 0.4f + 0.5f;
                tempProj.target = ec.gameObject;
                foreach (DamageType dt in proj.damages)
                {
                    float mult = dt.value * multval;
                    DamageType temps = new DamageType
                    {
                        damageElement = dt.damageElement,
                        value = (int)mult
                    };
                    tempProj.damages.Add(temps);
                }
            }
        }
        else
        {
            for (int i = 0; i < 3; i++)
            {
                var temp = Instantiate(projectile, startpos, Quaternion.identity);
                var tempProj = temp.GetComponent<TargettedProjectile>();
                tempProj.speed = 2 + (i * 0.8f);
                tempProj.disp = i * 0.8f + 0.5f;
                tempProj.target = ec.gameObject;
                foreach (DamageType dt in proj.damages)
                {
                    float mult = dt.value * multval;
                    DamageType temps = new DamageType
                    {
                        damageElement = dt.damageElement,
                        value = (int)mult
                    };
                    tempProj.damages.Add(temps);
                }
            }
        }
        base.OnHit(ec, proj);
    }
}
//    public GameObject projectile, partemp;
//    public bool isNine;
//    public bool attached;
//    public PlayerControl pc;
//    public float multiplier;
//    public Color nblue = new Color(0.16f, 0.58f, 1, 1), npurple = new Color(0.72f, 0.42f, 1, 1);
//    public List<DamageType> damages;
//    // Start is called before the first frame update
//    void Start()
//    {
//        foreach (DamageType dType in GetComponent<Projectile>().damageList)
//        {
//            DamageType temp = new DamageType
//            {
//                damageElement = dType.damageElement,
//                value = (int)(dType.value * multiplier)
//            };
//            damages.Add(temp);
//        }
//        if (isNine)
//        {
//            partemp.GetComponent<Light2D>().color = npurple;
//            pc.damageManager.Damaged += NineShot;
//            attached = true;
//        }
//        else
//        {
//            partemp.GetComponent<Light2D>().color = nblue;
//            pc.damageManager.Damaged += ThreeShot;
//            attached = true;
//        }
//        var temps = pc.ps.defaultShoot.projectile.GetComponent<NineTailsProj>();
//        Destroy(temps);
//    }
//    public void NineShot(object sender, DamageArg da)
//    {
//        if (da.projectile == this.gameObject)
//        {
//            for (int i = 0; i < 9; i++)
//            {
//                var temp = Instantiate(projectile, pc.transform.position, Quaternion.identity);
//                TargettedProjectile tp = temp.GetComponent<TargettedProjectile>();
//                tp.target = da.hitObj;
//                tp.damages = damages;
//                tp.disp = 1 + i * 0.1f;
//                tp.GetComponent<Light2D>().color = npurple;
//            }
//            pc.damageManager.Damaged -= NineShot;
//            attached = false;
//        }
//    }
//    public void ThreeShot(object sender, DamageArg da)
//    {
//        if (da.projectile == this.gameObject)
//        {
//            for (int i = 0; i < 3; i++)
//            {
//                var temp = Instantiate(projectile, pc.transform.position, Quaternion.identity);
//                TargettedProjectile tp = temp.GetComponent<TargettedProjectile>();
//                tp.target = da.hitObj;
//                tp.damages = damages;
//                tp.disp = 1 + i * 0.2f;
//                tp.GetComponent<Light2D>().color = nblue;
//            }
//            pc.damageManager.Damaged -= ThreeShot;
//            attached = false;
//        }
//    }
//    private void OnTriggerEnter2D(Collider2D collision)
//    {
//        if (!collision.CompareTag("Player") && !collision.CompareTag("Projectile") && !collision.CompareTag("Enemy"))
//        {
//            if (attached == true)
//            {
//                if (isNine)
//                {
//                    pc.damageManager.Damaged -= NineShot;
//                }
//                else
//                {
//                    pc.damageManager.Damaged -= ThreeShot;
//                }
//            }
//        }
//    }
//}
