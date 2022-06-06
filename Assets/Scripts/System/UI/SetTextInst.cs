using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SetTextInst : MonoBehaviour
{
    public SetEffect set;
    private TextMeshProUGUI text;
    private bool disappear = false, reached = false;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        text.text = set.effectName + " : " + set.effectDescription;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!disappear)
        {
            if (!reached)
            {
                if (text.color.a > 0.95f)
                {
                    StartCoroutine(UntilDisappear());
                    reached = true;
                }
                else
                {
                    text.color = new Color(1, 1, 1, text.color.a + 0.05f);
                }
            }
        }
        else
        {
            text.color = new Color(1, 1, 1, text.color.a - 0.05f);
            text.characterSpacing += 0.05f;
            if (text.color.a < 0.05f)
            {
                Destroy(this.gameObject);
            }
        }
    }
    private IEnumerator UntilDisappear()
    {
        yield return new WaitForSeconds(2.0f);
        disappear = true;
    }
}
