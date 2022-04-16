using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OlgoiTriggerReceiver : AnimationTriggerReceiver
{
    public OlgoiAi oai;

    public void Dig()
    {
        oai.Dig();
    }
    public void Emerge()
    {
        oai.Emerge();
    }
}
