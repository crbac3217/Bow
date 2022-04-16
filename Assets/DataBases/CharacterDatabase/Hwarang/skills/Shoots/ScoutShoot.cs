using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScoutShoot", menuName = "Shoot/Hwarang/Scout", order = 110)]
public class ScoutShoot : Shoot
{
    public AirStrike flurry;

    public override void InvokeShoot(AttackArgs aa)
    {
        base.InvokeShoot(aa);
        ScoutBird birdcon = aa.projectile.GetComponent<ScoutBird>();
        birdcon.dir = aa.dir;
        birdcon.birdshot = skill;
        birdcon.strike = flurry;
        AirStrike temp = Instantiate(flurry);
        temp.bird = aa.projectile;
        aa.apc.ReplaceSkill(this.skill.skillName, temp);
        skill.EndSkill(aa.apc);
        skill.ShotSelectedToggleOff(aa.apc);
    }
}