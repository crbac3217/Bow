using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "Nine Tails", menuName = "Skill/Hwarang/NineTail", order = 110)]

public class NineTailsBlessing : Skill
{
    public Modifier temp;
    public Sprite[] iconCycle;

    public override void OnSkillAdd(PlayerControl pc)
    {
        base.OnSkillAdd(pc);
        Ninetails newNine = ScriptableObject.CreateInstance<Ninetails>();
        newNine.counter = 0;
        newNine.iconCycle = this.iconCycle;
        newNine.ShotAttachement = enhanceObj;
        temp = newNine;
        pc.ps.shootMods.Add(temp);
        pc.guiManager.skillButtons[2].GetComponent<Image>().sprite = iconCycle[0];
    }
    public override void OnSkillRemove(PlayerControl pc)
    {
        base.OnSkillRemove(pc);
        pc.ps.shootMods.Remove(temp);
    }
}
[CreateAssetMenu(fileName = "Ninetails", menuName = "Modifier/Hwarang/Skills/Ninetails", order = 110)]
public class Ninetails : Modifier
{
    public int counter;
    public Sprite[] iconCycle;
    public GameObject ShotAttachement;


    public override AttackArgs AttackArgMod(AttackArgs aa)
    {
        if (counter == 2 || counter == 5)
        {
            GameObject temp = Instantiate(ShotAttachement, aa.projectile.transform.position, Quaternion.identity);
            aa.projectile.GetComponent<HwarangDefaultProjectile>().attachments.Add(temp.GetComponent<EnhanceObj>());
            temp.GetComponent<NineTailsProj>().isNine = false;
        }
        else if (counter == 8)
        {
            GameObject temp = Instantiate(ShotAttachement, aa.projectile.transform.position, Quaternion.identity);
            aa.projectile.GetComponent<HwarangDefaultProjectile>().attachments.Add(temp.GetComponent<EnhanceObj>());
            temp.GetComponent<NineTailsProj>().isNine = true;
        }
        if (aa.apc.ps.activeShoot.isDefault)
        {
            if (counter == 8)
            {
                counter = 0;
            }
            else
            {
                counter++;
            }
            aa.apc.guiManager.skillButtons[2].GetComponent<Image>().sprite = iconCycle[counter];
        }
        return aa;
    }
}