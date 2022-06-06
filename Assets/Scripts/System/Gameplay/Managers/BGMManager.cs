using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public AudioSource audioSource;
    public Animator anim;
    private void Update()
    {
        
    }
    public void ChangeMusic(AudioClip next)
    {
        StartCoroutine(BGMChange(next));
    }
    private IEnumerator BGMChange(AudioClip next)
    {
        anim.SetTrigger("Transition");
        yield return new WaitForSeconds(1f);
        audioSource.clip = next;
        audioSource.Play();
    }
}
