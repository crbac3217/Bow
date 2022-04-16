using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HwarangDefaultProjectile : Projectile
{
    public float yMax;
    public float accuracy, accuracyMult;
    public List<EnhanceObj> attachments = new List<EnhanceObj>();
    public Vector2 startPos;

    public override void SetUp()
    {
        base.SetUp();
        startPos = transform.position;
        yMax = startPos.y;
        if (attachments.Count > 0)
        {
            attachments[0].transform.SetParent(this.transform);
            attachments[0].transform.localPosition = new Vector2(-0.1f, 0.08f);
            attachments[0].transform.localRotation = Quaternion.Euler(0, 0, 60);
            spren.Add(attachments[0].GetComponent<SpriteRenderer>());
        }
        if (attachments.Count > 1)
        {
            attachments[1].transform.SetParent(this.transform);
            attachments[1].transform.localPosition = new Vector2(-0.1f, -0.08f);
            attachments[1].transform.localRotation = Quaternion.Euler(0, 0, -60);
            spren.Add(attachments[1].GetComponent<SpriteRenderer>());
        }
    }
    public override void Flying()
    {
        base.Flying();
        if (transform.position.y > yMax)
        {
            yMax = transform.position.y;
        }
    }
    public override void OnCollisionEnemy(EnemyController ec)
    {
        foreach (EnhanceObj enh in attachments)
        {
            enh.OnHit(ec, this);
        }
        base.OnCollisionEnemy(ec);
    }
    public override void DealDamage(EnemyController ec)
    {
        float yCurr = transform.position.y;
        if (Mathf.Abs(yCurr - startPos.y) > Mathf.Abs(yMax - startPos.y))
        {
            yMax = yCurr;
        }
        float rangeMult = accuracy * (Mathf.Abs(transform.position.x - startPos.x) * 0.3f + Mathf.Abs(yMax - startPos.y));
        foreach (DamageType dtype in damages)
        {
            float temp = dtype.value + (dtype.value * rangeMult * accuracyMult);
            dtype.value = (int)temp;
        }
        base.DealDamage(ec);
    }
}
