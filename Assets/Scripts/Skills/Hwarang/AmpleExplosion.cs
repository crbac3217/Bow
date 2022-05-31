using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmpleExplosion : MonoBehaviour
{
    public AudioClip explodeaudio;
    private AudioSource audio;
    public GameObject ChildObject;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }
    public void Activate()
    {
        audio.clip = explodeaudio;
        audio.Play();
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
