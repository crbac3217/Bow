using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Modifier : ScriptableObject
{
    public bool onAdd;
    public ModifierType modifierType;
    public AudioClip modifierAudio;

    public virtual void OnModifierAdd(PlayerControl pc)
    {
        if (modifierAudio)
        {
            pc.bodyAudio.clip = modifierAudio;
            pc.bodyAudio.Play();
        }
    }
    public virtual void OnModifierActive(PlayerControl pc)
    {
        if (modifierAudio)
        {
            pc.bodyAudio.clip = modifierAudio;
            pc.bodyAudio.Play();
        }
    }
    public virtual void OnEnemyModActive(EnemyArg da)
    {
        if (modifierAudio)
        {
            da.epc.bodyAudio.clip = modifierAudio;
            da.epc.bodyAudio.Play();
        }
    }
    public virtual float NumberMod(PlayerControl pc, float f)
    {
        return f;
    }
    public virtual void OnModifierRemove(PlayerControl pc)
    {

    }
    public virtual AttackArgs AttackArgMod(AttackArgs aa)
    {
        return aa;
    }
}
public enum ModifierType { OnAdd = 0, AnyAttack = 1, AttackHit = 2, Killed = 3, Hit = 4, Dead = 5, Move = 6}