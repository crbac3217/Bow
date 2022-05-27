using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectroLine : MonoBehaviour
{
    private LineRenderer line;
    public GameObject from, to;
    public int segments;
    public float val, randomizerVal;
    private bool didDamage = false;
    private List<DamageType> damages = new List<DamageType>();
    private void Start()
    {
        Vector2 start = from.GetComponent<AiHandler>().visuals.transform.position;
        Vector2 end = to.GetComponent<AiHandler>().visuals.transform.position;
        float dist = Vector2.Distance(start, end);
        line = GetComponent<LineRenderer>();
        DamageType temp = new DamageType
        {
            damageElement = DamageElement.Thunder,
            value = (int)val
        };
        damages.Add(temp);
        line.positionCount = segments;
        for (int i = 0; i < segments; i++)
        {
            Vector2 posval = Vector2.Lerp(start, end, i * dist / (segments-1));
            if (i != 0 || i != segments-1)
            {
                posval = new Vector2(posval.x + Random.Range(-randomizerVal, randomizerVal), posval.y + Random.Range(-randomizerVal, randomizerVal));
            }
            Debug.Log(i);
            line.SetPosition(i, posval);
        }
    }
    private void FixedUpdate()
    {
        if (line.startColor.a < 0.9f && !didDamage)
        {
            line.startColor = new Color(line.startColor.r, line.startColor.g, line.startColor.b, line.startColor.a + 0.05f);
            line.endColor = new Color(line.startColor.r, line.startColor.g, line.startColor.b, line.startColor.a + 0.05f);
        }
        if (line.startColor.a > 0.95f && !didDamage)
        {
            DealDamage();
        }
        if (didDamage)
        {
            line.startColor = new Color(line.startColor.r, line.startColor.g, line.startColor.b, line.startColor.a - 0.01f);
            line.endColor = new Color(line.startColor.r, line.startColor.g, line.startColor.b, line.startColor.a - 0.01f);
            if (line.startColor.a < 0.1f)
            {
                Destroy(this.gameObject);
            }
        }
    }
    private void DealDamage()
    {
        to.GetComponent<EnemyController>().CalculateDamage(damages, false, 0);
        didDamage = true;
    }
}
