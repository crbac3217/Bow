using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTriggerReceiver : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip movenoise;
    public AiHandler ai;
    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }
    public void playVoice(string voiceName)
    {
        ai.playVoice(voiceName);
    }
    public void playFoley(string foleyName)
    {
        ai.playFoley(foleyName);
    }
    public void AttackSound(string attackName)
    {
        ai.AttackSoundTriggered(attackName);
    }
    public void MoveNoise()
    {
        audioSource.clip = movenoise;
        audioSource.Play();
    }
    public void DeadAnimFinished()
    {
        ai.DeadAnimFinished();
    }
    public void AttackTriggered(string attackName)
    {
        ai.AttackTriggered(attackName);
    }
    public void AdditionalAttackTriggered(string attackName)
    {
        ai.AdditionalAttackTriggered(attackName);
    }
}
