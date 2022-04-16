using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimConnector : MonoBehaviour
{
    public DokebiInst parent;
    
    public void Impact()
    {
        parent.OnImpact();
    }
    public void AnimFinished()
    {
        parent.disappear = true;
    }
}
