using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class CritRotator : MonoBehaviour
{
    public float rate, distance;
    public GameObject CritPanelPrefab;
    private void ClearCrits()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
    public void CreateCrits(List<Crit> crits)
    {
        ClearCrits();
        for (int i = 0; i < crits.Count; i++)
        {
            float radians = 2 * Mathf.PI / crits.Count * i;
            float ver = Mathf.Sin(radians);
            float hor = Mathf.Cos(radians);
            Vector3 spawnCircle = new Vector3(hor, 0, ver);
            Vector3 spawnPos = transform.position + spawnCircle * distance; // Radius is just the distance away from the point
            GameObject Panel = Instantiate(CritPanelPrefab, spawnPos, Quaternion.identity);
            Panel.GetComponent<SpriteRenderer>().sprite = crits[i].icon;
            Debug.Log(crits[i].critName);
            Panel.transform.SetParent(this.transform);
        }
    }
    private void Update()
    {
        if (transform.childCount != 0)
        {
            transform.Rotate(new Vector3(0, rate, 0) * Time.deltaTime);
            foreach (Transform tr in transform)
            {
                tr.Rotate(new Vector3(0, -rate, 0) * Time.deltaTime);
            }
        }   
    }
}
