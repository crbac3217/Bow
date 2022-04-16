using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrankOnAnimation : MonoBehaviour
{
    public void SkillHit()
    {
        GetComponentInParent<PrankInst>().SkillHit();
    }
}
