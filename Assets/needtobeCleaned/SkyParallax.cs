using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyParallax : MonoBehaviour
{
    public CameraParent camPar;
    public float vertMoveRate;
    private Vector2 lastCampos;

    private void LateUpdate()
    {
        float xrate = (float)Mathf.Abs((float)camPar.transform.position.x - camPar.xBoundaryL) / (float)Mathf.Abs((float)camPar.xBoundaryR - camPar.xBoundaryL);
        float xpos = Mathf.Clamp(2.5f + (-5f * xrate), -2.5f, 2.5f);
        float ypos = Mathf.Clamp(-1f - Mathf.Clamp(((lastCampos.y - camPar.transform.position.y) * vertMoveRate), 0.05f, -0.05f), -0.5f, 0.5f);
        transform.localPosition = new Vector3(xpos, ypos);
        lastCampos = camPar.transform.position;
    }
}
