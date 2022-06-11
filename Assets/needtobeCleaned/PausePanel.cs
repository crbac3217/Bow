using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.Audio;

public class PausePanel : MonoBehaviour
{
    public GameObject itemRows, itemIndi, itemPanel, bgmBar, sfxBar, closeButton, descPanel;
    public TextMeshProUGUI itemName, itemDesc;
    public List<StatIndi> stats = new List<StatIndi>();
    public GUIManager guiManager;

    private void Update()
    {
        if (descPanel.activeSelf)
        {
            descPanel.transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
    }
    public void SetUpPanel()
    {
        ResetPanel();
        SetUpItems();
        SetUpStats();
        SetUpVolume();
    }
    public void ResetPanel()
    {
        for (int i = itemPanel.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(itemPanel.transform.GetChild(i).gameObject);
        }
    }
    
    public void SetUpItems()
    {
        List<Item> candids = new List<Item>();
        for (int i = guiManager.pc.items.Count-1; i >= 0; i--)
        {
            candids.Add(guiManager.pc.items[i]);
            if (candids.Count >= 25)
            {
                break;
            }
        }
        for (int i = 0; i < 5; i++)
        {
            List<Item> temp = new List<Item>();
            for (int j = 0; j < 5; j++)
            {
                if (candids.Count > j + (i * 5))
                {
                    temp.Add(candids[j + (i*5)]);
                    Debug.Log("added " + candids[j + (i * 5)] + " to candids, candid Count = " + temp.Count);
                    if (temp.Count >= 5)
                    {
                        CreateRow(temp);
                        break;
                    }
                }
                else
                {
                    CreateRow(temp);
                    break;
                }
            }
        }
    }
    private void CreateRow(List<Item> temp)
    {
        if (temp.Count > 0)
        {
            var rowTemp = Instantiate(itemRows, itemPanel.transform.transform);
            foreach (Item item in temp)
            {
                var itemTemp = Instantiate(itemIndi, rowTemp.transform);
                ItemIndi id = itemTemp.GetComponent<ItemIndi>();
                id.image.sprite = item.itemThumbnail;
                id.itemName = item.itemName;
                id.description = item.itemDescription;
                EventTrigger trigger = itemTemp.GetComponent<EventTrigger>();
                EventTrigger.Entry onEntry = new EventTrigger.Entry();
                onEntry.eventID = EventTriggerType.PointerEnter;
                onEntry.callback.AddListener((eventData) => { DescOn(id.itemName, id.description); });
                EventTrigger.Entry offEntry = new EventTrigger.Entry();
                offEntry.eventID = EventTriggerType.PointerExit;
                offEntry.callback.AddListener((eventData) => { DescOff(); });
                trigger.triggers.Add(onEntry);
                trigger.triggers.Add(offEntry);
            }
        }
    }
    public void SetUpStats()
    {
        foreach (DamageType damageType in guiManager.pc.damageTypes)
        {
            foreach (StatIndi si in stats)
            {
                if (damageType.damageElement == si.delem && si.isDamage)
                {
                    si.text.text = ":" + damageType.value;
                }
            }
        }
        foreach (Stat stat in guiManager.pc.stats)
        {
            foreach (StatIndi si in stats)
            {
                if (stat.statType == si.statType && !si.isDamage)
                {
                    si.text.text = ":" + stat.value;
                }
            }
        }
    }
    public void SetUpVolume()
    {
        float bgmVal, sfxVal;
        guiManager.mixer.GetFloat("bgmVol", out bgmVal);
        bgmBar.GetComponent<Slider>().value = bgmVal;
        guiManager.mixer.GetFloat("sfxVol", out sfxVal);
        sfxBar.GetComponent<Slider>().value = sfxVal;
    }
    public void OnSFXChange()
    {
        guiManager.mixer.SetFloat("sfxVol", sfxBar.GetComponent<Slider>().value);
        PlayerPrefs.SetFloat("sfxVol", sfxBar.GetComponent<Slider>().value);
    }
    public void OnBGMChange()
    {
        guiManager.mixer.SetFloat("bgmVol", bgmBar.GetComponent<Slider>().value);
        PlayerPrefs.SetFloat("bgmVol", bgmBar.GetComponent<Slider>().value);
    }
    public void OnClose()
    {
        guiManager.PausePanelClose();
    }
    public void DescOn(string name, string desc)
    {
        descPanel.SetActive(true);
        itemName.text = name;
        itemDesc.text = desc;
        itemDesc.fontSize = itemName.fontSize - 2;
    }
    public void DescOff()
    {
        descPanel.SetActive(false);
    }
}
