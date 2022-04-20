using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowTriggerReciever : AnimationTriggerReceiver
{
    public ShadowAi sai;

    private void Start()
    {
        sai = ai.GetComponent<ShadowAi>();
    }
    public void Dig()
    {
        sai.Dig();
    }
    public void Emerge()
    {
        sai.Emerge();
    }
}
