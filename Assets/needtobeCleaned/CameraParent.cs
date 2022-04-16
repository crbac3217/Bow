using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraParent : MonoBehaviour
{
    public float dampAmount, height, shakeIntensity;
    public GameObject backGround, player, lightObj;
    public Camera cam;
    public bool doFollowPlayer;
    public float xBoundaryL, xBoundaryR, yBoundaryD, yBoundaryU;
    private float lightStartAngle;
    private Vector3 velocity = Vector3.zero;
    private Vector3 camPos, lightPos;
    public Vector3 defcamPos;
    public Vector2 camShake = Vector2.zero;

    private void Start()
    {
        lightStartAngle = 120;
        lightPos = lightObj.transform.localPosition;
        doFollowPlayer = true;
        camPos = cam.transform.localPosition;
        defcamPos = camPos;
    }

    private void Update()
    {
        if (doFollowPlayer)
        {
            Vector3 target = player.transform.TransformPoint(new Vector3(0, height, -10));
            Vector3 movePoint = Vector3.SmoothDamp(transform.position, target, ref velocity, dampAmount);
            lightObj.transform.localPosition = new Vector3(lightPos.x - (transform.position.x / xBoundaryR * 6f), lightObj.transform.localPosition.y, lightObj.transform.localPosition.z);
            lightObj.transform.localRotation = Quaternion.Euler(0, 0, lightStartAngle + (transform.position.x / xBoundaryR * 120f));
            transform.position = new Vector3(Mathf.Clamp(movePoint.x, xBoundaryL, xBoundaryR), Mathf.Clamp(movePoint.y, yBoundaryD, yBoundaryU), movePoint.z);
        }
        if (camShake != Vector2.zero)
        {
            Vector3 mov = new Vector3(cam.transform.localPosition.x + Mathf.Sin(shakeIntensity * Time.time) * camShake.x, cam.transform.localPosition.y + Mathf.Sin(shakeIntensity * Time.time) * camShake.y, 0);
            cam.transform.localPosition = mov;
        }
        else
        {
            cam.transform.localPosition = defcamPos;
        }
    }
    public IEnumerator CamShake(Vector2 shakeMultitude, float time)
    {
        camShake += shakeMultitude;
        yield return new WaitForSeconds(time);
        camShake -= shakeMultitude;
    }
}
