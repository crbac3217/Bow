using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TripleShot", menuName = "Skill/Hwarang/TripleShot", order = 110)]
public class TripleShot : Skill
{
    public DamageElement delem;
    public TripleShotShoot aimableSkillforTriple;
    public Sprite[] icons = new Sprite[] { };

    public override void ReplaceShotPress(PlayerControl pc)
    {
        if (selected)
        {
            ShotSelectedToggleOff(pc);
            pc.ps.activeShoot = Instantiate(pc.ps.defaultShoot);
        }
        else
        {
            ShotSelectedToggleOn(pc);
            TripleShotShoot temp = Instantiate(this.aimableSkillforTriple);
            pc.ps.activeShoot = temp;
            temp.isSkill = true;
            temp.skill = this;
            temp.delem = delem;
            foreach (Skill skill in pc.skills)
            {
                if (skill)
                {
                    if (skill.skillType == SkillType.shotEnhance)
                    {
                        DisableSkill(skill);
                    }
                }
            }
        }
    }
}
