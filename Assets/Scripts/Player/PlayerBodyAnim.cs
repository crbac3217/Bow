using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBodyAnim : MonoBehaviour
{
    public PlayerAnim pa;
    public PlayerControl pc;
    public AudioClip footstep;
    public void FootStep()
    {
        pc.bodyAudio.clip = footstep;
        pc.bodyAudio.Play();
    }
    public void EnableHead()
    {
        pa.EnableHead();
    }
    public void DisableHead()
    {
        pa.DisableHead();
    }
    public void EnableArm()
    {
        pa.EnableArm();
    }
    public void DisableArm()
    {
        pa.DisableArm();
    }
}
