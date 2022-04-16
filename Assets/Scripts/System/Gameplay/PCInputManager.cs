using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCInputManager : MonoBehaviour
{
    public PlayerControl pc;
    public KeyCode right, left, jump, skill1, skill2, skill3;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            pc.ph.HitPlayerNoDir(1);
        }
        if (Input.GetKeyDown(right))
        {
            pc.pm.MoveRight();
        }
        if (Input.GetKeyDown(left))
        {
            pc.pm.MoveLeft();
        }
        if (Input.GetKey(left) || Input.GetKey(right))
        {
            pc.pm.isMoving = true;
        }
        if (Input.GetKeyDown(jump))
        {
            pc.pj.Jump();
        }
        if (Input.GetKeyUp(right))
        {
            pc.pm.LetGoRight();
        }
        if (Input.GetKeyUp(left))
        {
            pc.pm.LetGoLeft();
        }
    }
}
