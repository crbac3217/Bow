using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{
    public Image coolDownImage;
    public Skill thisSkill;

    public void UpdateCD()
    {
        Debug.Log("Updated cooldown for " + thisSkill.skillName+", which is " + thisSkill.cdCurrentCount +"/" + thisSkill.cdMaxCount);
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

    private void Update()
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
}