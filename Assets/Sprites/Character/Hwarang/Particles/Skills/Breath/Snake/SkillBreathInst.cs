using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBreathInst : MonoBehaviour
{
    public GameObject bp, instance;
    public DamageElement delem;
    public List<DamageType> damages = new List<DamageType>();
    public bool disappear = false;
    public CameraParent campar;
    public AudioClip breathSpawn, breathInstance;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = breathSpawn;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void TriggerBreath()
    {
        campar.StartCoroutine(campar.CamShake(new Vector2(0f, 0.2f), 0.1f));
        GameObject breathInst = Instantiate(instance, bp.transform.position, Quaternion.identity);
        breathInst.transform.localScale = new Vector2(this.transform.localScale.x * breathInst.transform.localScale.x, breathInst.transform.localScale.y);
        breathInst.GetComponent<BreathInstance>().parent = this;
        breathInst.GetComponent<BreathInstance>().delem = this.delem;
        breathInst.GetComponent<BreathInstance>().damages = this.damages;
        audioSource.clip = breathInstance;
        audioSource.loop = false;
        audioSource.Play();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (disappear)
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, GetComponent<SpriteRenderer>().color.a - 0.01f);
            if (GetComponent<SpriteRenderer>().color.a < 0.05f)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
