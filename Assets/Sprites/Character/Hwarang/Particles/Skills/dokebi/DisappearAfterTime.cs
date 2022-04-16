using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearAfterTime : MonoBehaviour
{
    public float time;
    public GameObject objtoDestroy;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AfterTime(time));
        if (!objtoDestroy)
        {
            objtoDestroy = this.gameObject;
        }
    }
    IEnumerator AfterTime(float t)
    {
        yield return new WaitForSeconds(t);
        Destroy(objtoDestroy);
    }
}
