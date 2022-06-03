using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowController : BossController
{
    public int phase = 1;
    public GameObject canvas;

    public override void OnDamageTaken()
    {
        base.OnDamageTaken();
        if (hp > 0)
        {
            float threshHold = maxHp * 0.5f;
            if (hp < threshHold && phase == 1)
            {
                ChangePhase();
            }
        }
    }
    private void ChangePhase()
    {
        GetComponent<ShadowAi>().ChangePhase();
        phase = 2;
        foreach (DamageType dt in strength)
        {
            dt.value += 50;
            if (dt.damageElement == DamageElement.None)
            {
                dt.value += 50;
            }
        }
        critRotate.transform.localPosition = new Vector2(critRotate.transform.localPosition.x, critRotate.transform.localPosition.y + 0.2f);
        canvas.transform.localPosition = new Vector2(canvas.transform.localPosition.x, canvas.transform.localPosition.y + 0.2f);
        GetComponent<CapsuleCollider2D>().offset = new Vector2(0, 0.5f);
        GetComponent<CapsuleCollider2D>().size = new Vector2(0.16f, 0.57f);
    }
}
