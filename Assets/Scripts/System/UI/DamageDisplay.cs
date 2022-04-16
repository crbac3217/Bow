using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DamageDisplay : MonoBehaviour
{
    private TextMeshProUGUI tmpro;
    private bool disappear;
    void Start()
    {
        disappear = false;
        tmpro = GetComponent<TextMeshProUGUI>();
        StartCoroutine(OneSecThenDestroy());
    }

    // Update is called once per frame
    void Update()
    {
        if (disappear)
        {
            tmpro.color = new Color(tmpro.color.r, tmpro.color.g, tmpro.color.b, tmpro.color.a - 0.005f);
            if (tmpro.color.a < 0.01f)
            {
                Destroy(this.gameObject);
            }
            tmpro.characterSpacing += 0.1f;
        }
    }
    private IEnumerator OneSecThenDestroy()
    {
        yield return new WaitForSeconds(1);
        disappear = true;
    }
}
