using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    public PlayerControl pc;
    public GameObject emptyArm, bowArm, body, head, firePoint;
    public Animator bodyAnim, headAnim;
    public SpriteRenderer emptyArmSprite, bowArmSprite, bodySprite, headSprite;
    public Vector3 IShootPos, IShootFp, IRShootPos, IRShootFp, RShootPos, RShootFp, RRShootPos, RRShootFp, JShootPos, JShootFp, JRShootPos, JRShootFp, EIPos, EIRPos, ERPos, ERRPos, EJPos, EJRPos;
    public Sprite IShoot, IRShoot, RShoot, RRShoot, JShoot, JRShoot, EIShoot, EIRShoot, ERShoot, ERRShoot, EJShoot, EJRShoot;

    public void Begin()
    {
        pc = GetComponent<PlayerControl>();
    }
    public void EnableHead()
    {
        headSprite.enabled = true;
    }
    public void DisableHead()
    {
        headSprite.enabled = false;
    }
    public void EnableArm()
    {
        bowArmSprite.enabled = true;
        emptyArmSprite.enabled = true;
    }
    public void DisableArm()
    {
        bowArmSprite.enabled = false;
        emptyArmSprite.enabled = false;
    }
    public void RotateArm(Vector2 dir)
    {
        if (pc.pm.isRight)
        {
            if (!pc.pj.isGrounded)
            {
                if (dir.x < 0)
                {
                    emptyArmSprite.sprite = EJShoot;
                    emptyArmSprite.sortingOrder = 0;
                    emptyArm.transform.localPosition = EJPos;
                    bowArmSprite.sprite = JShoot;
                    bowArmSprite.sortingOrder = 10;
                    bowArm.transform.localPosition = JShootPos;
                    firePoint.transform.localPosition = JShootFp;
                    float angle = Mathf.Atan2(dir.y * -1, dir.x * -1) * Mathf.Rad2Deg;
                    bowArm.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                    emptyArm.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                }
                else
                {
                    emptyArmSprite.sprite = EJRShoot;
                    emptyArmSprite.sortingOrder = 10;
                    emptyArm.transform.localPosition = EJRPos;
                    bowArmSprite.sprite = JRShoot;
                    bowArmSprite.sortingOrder = 0;
                    bowArm.transform.localPosition = JRShootPos;
                    firePoint.transform.localPosition = JRShootFp;
                    float angle = Mathf.Atan2(dir.y * -1, dir.x) * Mathf.Rad2Deg;
                    bowArm.transform.rotation = Quaternion.AngleAxis(angle, Vector3.back);
                    emptyArm.transform.rotation = Quaternion.AngleAxis(angle, Vector3.back);
                }
            }
            else
            {
                if (pc.pm.isMoving)
                {
                    if (dir.x < 0)
                    {
                        emptyArmSprite.sprite = ERShoot;
                        emptyArmSprite.sortingOrder = 0;
                        emptyArm.transform.localPosition = ERPos;
                        bowArmSprite.sprite = RShoot;
                        bowArmSprite.sortingOrder = 10;
                        bowArm.transform.localPosition = RShootPos;
                        firePoint.transform.localPosition = RShootFp;
                        float angle = Mathf.Atan2(dir.y * -1, dir.x * -1) * Mathf.Rad2Deg;
                        bowArm.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                        emptyArm.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                    }
                    else
                    {
                        emptyArmSprite.sprite = ERRShoot;
                        emptyArmSprite.sortingOrder = 10;
                        emptyArm.transform.localPosition = ERRPos;
                        bowArmSprite.sprite = RRShoot;
                        bowArmSprite.sortingOrder = 0;
                        bowArm.transform.localPosition = RRShootPos;
                        firePoint.transform.localPosition = RRShootFp;
                        float angle = Mathf.Atan2(dir.y * -1, dir.x) * Mathf.Rad2Deg;
                        bowArm.transform.rotation = Quaternion.AngleAxis(angle, Vector3.back);
                        emptyArm.transform.rotation = Quaternion.AngleAxis(angle, Vector3.back);
                    }
                }
                else
                {
                    if (dir.x < 0)
                    {
                        emptyArmSprite.sprite = EIShoot;
                        emptyArmSprite.sortingOrder = 0;
                        emptyArm.transform.localPosition = EIPos;
                        bowArmSprite.sprite = IShoot;
                        bowArmSprite.sortingOrder = 10;
                        bowArm.transform.localPosition = IShootPos;
                        firePoint.transform.localPosition = IShootFp;
                        float angle = Mathf.Atan2(dir.y * -1, dir.x * -1) * Mathf.Rad2Deg;
                        bowArm.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                        emptyArm.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                    }
                    else
                    {
                        emptyArmSprite.sprite = EIRShoot;
                        emptyArmSprite.sortingOrder = 10;
                        emptyArm.transform.localPosition = EIRPos;
                        bowArmSprite.sprite = IRShoot;
                        bowArmSprite.sortingOrder = 0;
                        bowArm.transform.localPosition = IRShootPos;
                        firePoint.transform.localPosition = IRShootFp;
                        float angle = Mathf.Atan2(dir.y * -1, dir.x) * Mathf.Rad2Deg;
                        bowArm.transform.rotation = Quaternion.AngleAxis(angle, Vector3.back);
                        emptyArm.transform.rotation = Quaternion.AngleAxis(angle, Vector3.back);
                    }
                }
            }
            
        }
        else
        {
            if (!pc.pj.isGrounded)
            {
                if (dir.x < 0)
                {
                    emptyArmSprite.sprite = EJRShoot;
                    emptyArmSprite.sortingOrder = 10;
                    emptyArm.transform.localPosition = EJRPos;
                    bowArmSprite.sprite = JRShoot;
                    bowArmSprite.sortingOrder = 0;
                    bowArm.transform.localPosition = JRShootPos;
                    firePoint.transform.localPosition = JRShootFp;
                    float angle = Mathf.Atan2(dir.y, -dir.x) * Mathf.Rad2Deg;
                    bowArm.transform.rotation = Quaternion.AngleAxis(angle, Vector3.back);
                    emptyArm.transform.rotation = Quaternion.AngleAxis(angle, Vector3.back);
                }
                else
                {
                    emptyArmSprite.sprite = EJShoot;
                    emptyArmSprite.sortingOrder = 0;
                    emptyArm.transform.localPosition = EJPos;
                    bowArmSprite.sprite = JShoot;
                    bowArmSprite.sortingOrder = 10;
                    bowArm.transform.localPosition = JShootPos;
                    firePoint.transform.localPosition = JShootFp;
                    float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                    bowArm.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                    emptyArm.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                }
            }
            else
            {
                if (pc.pm.isMoving)
                {
                    if (dir.x < 0)
                    {
                        emptyArmSprite.sprite = ERRShoot;
                        emptyArmSprite.sortingOrder = 10;
                        emptyArm.transform.localPosition = ERRPos;
                        bowArmSprite.sprite = RRShoot;
                        bowArmSprite.sortingOrder = 0;
                        bowArm.transform.localPosition = RRShootPos;
                        firePoint.transform.localPosition = RRShootFp;
                        float angle = Mathf.Atan2(dir.y, -dir.x) * Mathf.Rad2Deg;
                        bowArm.transform.rotation = Quaternion.AngleAxis(angle, Vector3.back);
                        emptyArm.transform.rotation = Quaternion.AngleAxis(angle, Vector3.back);
                    }
                    else
                    {
                        emptyArmSprite.sprite = ERShoot;
                        emptyArmSprite.sortingOrder = 0;
                        emptyArm.transform.localPosition = ERPos;
                        bowArmSprite.sprite = RShoot;
                        bowArmSprite.sortingOrder = 10;
                        bowArm.transform.localPosition = RShootPos;
                        firePoint.transform.localPosition = RShootFp;
                        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                        bowArm.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                        emptyArm.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                    }
                }
                else
                {
                    if (dir.x < 0)
                    {
                        emptyArmSprite.sprite = EIRShoot;
                        emptyArmSprite.sortingOrder = 10;
                        emptyArm.transform.localPosition = EIRPos;
                        bowArmSprite.sprite = IRShoot;
                        bowArmSprite.sortingOrder = 0;
                        bowArm.transform.localPosition = IRShootPos;
                        firePoint.transform.localPosition = IRShootFp;
                        float angle = Mathf.Atan2(dir.y, -dir.x) * Mathf.Rad2Deg;
                        bowArm.transform.rotation = Quaternion.AngleAxis(angle, Vector3.back);
                        emptyArm.transform.rotation = Quaternion.AngleAxis(angle, Vector3.back);
                    }
                    else
                    {
                        emptyArmSprite.sprite = EIShoot;
                        emptyArmSprite.sortingOrder = 0;
                        emptyArm.transform.localPosition = EIPos;
                        bowArmSprite.sprite = IShoot;
                        bowArmSprite.sortingOrder = 10;
                        bowArm.transform.localPosition = IShootPos;
                        firePoint.transform.localPosition = IShootFp;
                        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                        bowArm.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                        emptyArm.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                    }
                }
            }
        }
    }
}
