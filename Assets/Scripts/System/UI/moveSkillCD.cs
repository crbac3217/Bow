using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class moveSkillCD : MonoBehaviour
{
    public float remaining;
    public float skillCD;
    private Image image;
    void Start()
    {
        image = GetComponent<Image>();
    }
    void FixedUpdate()
    {
        if (remaining > 0)
        {
            remaining -= Time.deltaTime;
            CDUpdate();
        }
    }
    public void CDUpdate()
    {
        image.fillAmount = (remaining / skillCD);
    }
}
