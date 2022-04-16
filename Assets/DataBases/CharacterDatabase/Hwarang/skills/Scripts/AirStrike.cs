using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

[CreateAssetMenu(fileName = "AirStrike", menuName = "Skill/Hwarang/AirStrike", order = 110)]
public class AirStrike : Skill
{
    public GameObject spinParticle, bird;
    private GameObject instance;
    public Skill birdshot;
    public float damageMultiplier;
    public override void ActiveRelease(PlayerControl pc)
    {
        //move Player to the bird's position
        bird.GetComponent<Collider2D>().isTrigger = true;
        pc.transform.position = bird.transform.position;
        Destroy(bird.gameObject);
        //make the spin particle
        instance = Instantiate(spinParticle, pc.transform.position, Quaternion.identity);
        instance.transform.localScale = new Vector2(instance.transform.localScale.x * pc.pm.body.transform.localScale.x, instance.transform.localScale.y * pc.gameObject.transform.localScale.y);
        FlurryInstance fi = instance.GetComponent<FlurryInstance>();
        foreach (DamageType damageType in pc.damageTypes)
        {
            float totDamage = damageType.value + (damageType.value * damageMultiplier);
            DamageType temp = new DamageType
            {
                damageElement = damageType.damageElement,
                value = (int)totDamage
            };
            fi.damages.Add(temp);
        }
        fi.fadein = true;
        //replace flurry back to scout
        Skill tempsk = Instantiate(birdshot);
        tempsk.isSkillAvail = false;
        cdCurrentCount = 0;
        pc.ReplaceSkill(this.skillName, tempsk);
        base.ActiveRelease(pc);
    }
}
