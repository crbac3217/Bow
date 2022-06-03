using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OlgoiAi : BossAi
{
    public bool isDug;
    public GameObject digParticle;
    public void Dig()
    {
        GetComponent<Rigidbody2D>().gravityScale = 0;
        GetComponent<Collider2D>().enabled = false;
        ec.invincible = true;
        GetComponent<BossController>().UpdateHpUI();
        var dig = Instantiate(digParticle, transform.position, Quaternion.identity);
        campar.StartCoroutine(campar.CamShake(Vector2.up * 0.03f, 0.3f));
        isDug = true;
    }
    public void Emerge()
    {
        GetComponent<Rigidbody2D>().gravityScale = 1;
        GetComponent<Collider2D>().enabled = true;
        ec.invincible = false;
        GetComponent<BossController>().UpdateHpUI();
        var dig = Instantiate(digParticle, transform.position, Quaternion.identity);
        campar.StartCoroutine(campar.CamShake(Vector2.up * 0.03f, 0.3f));
        isDug = false;
    }
}
