using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChangeControl : MonoBehaviour
{
    public string prefKey, curKey, defaultKey;
    public TextMeshProUGUI textbox;
    public Image image;
    public bool active = false;
    public AudioSource audioS;
    private void Start()
    {
        SetUp();
    }
    private void SetUp()
    {
        if (PlayerPrefs.HasKey(prefKey))
        {
            KeyChange(PlayerPrefs.GetString(prefKey));
        }
        else
        {
            PlayerPrefs.SetString(prefKey, defaultKey);
            SetUp();
        }
    }
    private void Update()
    {
        if (Input.anyKeyDown && active)
        {
            if (!Input.GetKeyDown(KeyCode.Mouse0))
            {
                KeyCheck();
                ChangeOff();
            }
        }
    }
    public void OnButtonPress()
    {
        if (!active)
        {
            active = true;
            image.color = new Color(1, 1, 1, 1);
            audioS.Play();
            foreach (ChangeControl cc in this.transform.parent.parent.GetComponent<ControlSettingInst>().ccs)
            {
                if (cc != this)
                {
                    cc.ChangeOff();
                }
            }
        }
        else
        {
            ChangeOff();
        }
    }
    private void KeyCheck()
    {
        foreach (KeyCode kcode in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(kcode))
            {
                KeyChange(kcode.ToString());
            }
        }
        audioS.Play();
    }
    private void KeyChange(string keycode)
    {
        if (!keycode.Contains("Alpha"))
        {
            textbox.text = keycode;
        }
        else
        {
            Debug.Log(keycode);
            string temp = keycode;
            temp = temp.Replace("Alpha", "");
            textbox.text = temp;
        }
        curKey = keycode;
        PlayerPrefs.SetString(prefKey, curKey);
    }
    public void ChangeOff()
    {
        active = false;
        image.color = new Color(1, 1, 1, 0.5f);
    }
    
}
