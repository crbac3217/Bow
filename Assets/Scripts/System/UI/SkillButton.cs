using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{
    public Image coolDownImage;
    public GameObject outline;
    private Animator anim;
    public Skill thisSkill;

    private void Start()
    {
        if (!anim)
        {
            anim = outline.GetComponent<Animator>();
        }
    }
    public void UpdateCD()
    {
        if (thisSkill.cdType != CooldownType.time)
        {
            if (!thisSkill.isSkillAvail)
            {
                coolDownImage.fillAmount = 1 - (float)thisSkill.cdCurrentCount / (float)thisSkill.cdMaxCount;
            }
            else
            {
                coolDownImage.fillAmount = 0;
            }
        }
    }

    private void FixedUpdate()
    {
        if (thisSkill != null)
        {
            if (thisSkill.cdType == CooldownType.time)
            {
                if (!thisSkill.isSkillAvail)
                {
                    coolDownImage.fillAmount = 1 - (Time.time - thisSkill.lastTime) / thisSkill.cooldownTime;
                }
                else
                {
                    coolDownImage.fillAmount = 0;
                }
            }
        }
    }
    public void SkillSelected()
    {
        anim.ResetTrigger("Selected");
        anim.ResetTrigger("Deselected");
        anim.ResetTrigger("CDFinished");
        anim.SetTrigger("Selected");
    }
    public void SkillDeselected()
    {
        anim.ResetTrigger("Selected");
        anim.ResetTrigger("Deselected");
        anim.ResetTrigger("CDFinished");
        anim.SetTrigger("Deselected");
    }
    public void CDFinish()
    {
        anim.ResetTrigger("Selected");
        anim.ResetTrigger("Deselected");
        anim.ResetTrigger("CDFinished");
        anim.SetTrigger("CDFinished");
    }
}
