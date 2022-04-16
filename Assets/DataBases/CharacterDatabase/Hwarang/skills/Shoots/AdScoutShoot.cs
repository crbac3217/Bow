using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AdScoutShoot", menuName = "Shoot/Hwarang/AdScout", order = 110)]
public class AdScoutShoot : Shoot
{
    public SkyStrike typhoon;
    public override void InvokeShoot(AttackArgs Aa)
    {
        GameObject bird = Aa.projectile;
        ScoutBird birdcon = bird.GetComponent<ScoutBird>();
        birdcon.dir = Aa.dir;
        birdcon.birdshot = skill;
        birdcon.strike = typhoon;
        SkyStrike temp = Instantiate(typhoon);
        temp.bird = bird;
        Aa.apc.ReplaceSkill(this.skill.skillName, temp);
        skill.EndSkill(Aa.apc);
        skill.ShotSelectedToggleOff(Aa.apc);
    }
}
