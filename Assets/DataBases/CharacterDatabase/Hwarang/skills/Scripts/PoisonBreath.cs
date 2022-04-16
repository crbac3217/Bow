using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PoisonBreath", menuName = "Skill/Hwarang/PoisonBreath", order = 110)]
public class PoisonBreath : Skill
{
    public GameObject poisonBreathPrefab;
    private GameObject instance;
    public float damageMultiplier;

    public override void ActivePress(PlayerControl pc)
    {
        base.ActivePress(pc);
        instance = Instantiate(poisonBreathPrefab, new Vector2(pc.transform.position.x, pc.transform.position.y), Quaternion.identity);
        instance.transform.localScale = new Vector2(instance.transform.localScale.x * pc.pm.body.transform.localScale.x, instance.transform.localScale.y * pc.gameObject.transform.localScale.y);
        foreach (DamageType dtype in pc.damageTypes)
        {
            float multval = dtype.value + (dtype.value * damageMultiplier);
            DamageType temp = new DamageType
            {
                damageElement = dtype.damageElement,
                value = (int)multval
            };
            instance.GetComponent<SkillBreathInst>().damages.Add(temp);
        }
        instance.GetComponent<SkillBreathInst>().campar = pc.campar;
    }
    public override void ActiveRelease(PlayerControl pc)
    {
        base.ActiveRelease(pc);
        instance.GetComponent<Animator>().SetTrigger("skillReleased");
    }
}
