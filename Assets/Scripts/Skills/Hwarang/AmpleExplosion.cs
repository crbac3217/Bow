using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmpleExplosion : MonoBehaviour
{
    public GameObject ChildObject;

    public void Activate()
    {
        Destroy(ChildObject);
        GetComponent<ParticleSystem>().Play();
        StartCoroutine(DestroyAfter1Sec());
    }
    public IEnumerator DestroyAfter1Sec()
    {
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
    }
}
