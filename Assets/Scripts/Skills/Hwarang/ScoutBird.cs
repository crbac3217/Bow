using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class ScoutBird : MonoBehaviour
{
    public Vector2 dir;
    private SpriteRenderer spren;
    public float duration, speed;
    private Rigidbody2D rigid;
    public Skill birdshot, strike;
    
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spren = GetComponent<SpriteRenderer>();
        StartCoroutine(Activated());
        if (dir.x > 0)
        {
            spren.flipY = false;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else
        {
            spren.flipY = true;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
    public IEnumerator Activated()
    {
        yield return new WaitForSeconds(duration);
        Skill tempsk = Instantiate(birdshot);
        tempsk.isSkillAvail = false;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>().ReplaceSkill(strike.skillName, tempsk);
        tempsk.sb.UpdateCD();
        Destroy(this.gameObject);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        rigid.velocity = dir*speed;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GetComponent<Collider2D>().isTrigger = false;
        }
    }
}
