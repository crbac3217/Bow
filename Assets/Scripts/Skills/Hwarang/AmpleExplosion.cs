using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmpleExplosion : MonoBehaviour
{
    public AudioClip explodeaudio;
    private AudioSource ampleAudio;
    public GameObject ChildObject;

    private void Start()
    {
        ampleAudio = GetComponent<AudioSource>();
    }
    public void Activate()
    {
        ampleAudio.clip = explodeaudio;
        ampleAudio.Play();
        Destroy(ChildObject);
        GetComponent<ParticleSystem>().Play();
        StartCoroutine(DestroyAfter1Sec());
    }
    public IEnumerator DestroyAfter1Sec()
    {
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
    }
}
