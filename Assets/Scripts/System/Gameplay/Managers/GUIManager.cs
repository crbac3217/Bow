using System.Collections;
using TMPro;
using System.Reflection;
using System;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{
    public bool tester = false;
    public PlayerControl pc;
    public Volume volume;
    public GameObject joystickPref, movePref, canvasPref, canvas, moveButton, joyStick, eqPanelPref, eqConfirmPref, eqEachItem, chestPref, shopPref, shopEachPref, bossBarPref, bossBar;
    private GameObject eqPanel, eqConfirm, shopPanel, chestPanel;
    private ShopBase sb;
    public GameObject[] skillButtons = new GameObject[] { };
    public List<string> test = new List<string>();
    public Dictionary<string, GameObject> uiElements = new Dictionary<string, GameObject>();
    public Dictionary<string, EventTrigger.Entry> entries = new Dictionary<string, EventTrigger.Entry>();

    private void Start()
    {
        volume = FindObjectOfType<Volume>();
    }
    public void SpawnGUI()
    {
        canvas = Instantiate(canvasPref, pc.campar.cam.transform);
        moveButton = Instantiate(movePref, canvas.transform);
        RectTransform mtrans = moveButton.GetComponent<RectTransform>();
        mtrans.anchoredPosition = new Vector2(PlayerPrefs.GetFloat("MoveX"), PlayerPrefs.GetFloat("MoveY"));
        joyStick = Instantiate(joystickPref, canvas.transform);
        RectTransform jtrans = joyStick.GetComponent<RectTransform>();
        jtrans.anchoredPosition = new Vector2(PlayerPrefs.GetFloat("JoyX"), PlayerPrefs.GetFloat("JoyY"));
        joyStick = GameObject.FindGameObjectWithTag("Joystick");
        skillButtons = new GameObject[3] { GameObject.FindGameObjectWithTag("Skill1"), GameObject.FindGameObjectWithTag("Skill2"), GameObject.FindGameObjectWithTag("Skill3") };
        MoveButtonInitialize();
        JumpButtonInitialize();
        SkillButtonInitailize();
        SetUpEQPanels();
        SetUpShopPanels();
        SetUpChestPanel();
        //MoveButtonRead();
        //JoystickRead();
    }
    #region GUILoadSetUp
    //public void MoveButtonRead()
    //{
    //    //moveButton.GetComponent<HorizontalLayoutGroup>().spacing += PlayerPrefs.GetFloat("MoveSpacingDisplacement");

    //}
    //public void JoystickRead()
    //{

    //}
    #endregion GUILoadSetUp
    #region GUIButtonInitialize
    public void MoveButtonInitialize()
    {
        object[] parameters = new object[] { };
        PlayerMove pm = pc.GetComponent<PlayerMove>();
        ButtonAdd(pc.gameObject, pm.GetType(), pm.GetType().GetMethod("MoveRight"), parameters, "rButton", EventTriggerType.PointerDown);
        ButtonAdd(pc.gameObject, pm.GetType(), pm.GetType().GetMethod("MoveLeft"), parameters, "lButton", EventTriggerType.PointerDown);
        ButtonAdd(pc.gameObject, pm.GetType(), pm.GetType().GetMethod("LetGoRight"), parameters, "rButton", EventTriggerType.PointerUp);
        ButtonAdd(pc.gameObject, pm.GetType(), pm.GetType().GetMethod("LetGoLeft"), parameters, "lButton", EventTriggerType.PointerUp);
    }
    public void JumpButtonInitialize()
    {
        object[] parameters = new object[] { };
        PlayerJump pj = pc.GetComponent<PlayerJump>();
        ButtonAdd(pc.gameObject, pj.GetType(), pj.GetType().GetMethod("Jump"), parameters, "jumpButton", EventTriggerType.PointerDown);
        ButtonAdd(pc.gameObject, pj.GetType(), pj.GetType().GetMethod("OnLetGo"), parameters, "jumpButton", EventTriggerType.PointerUp);
    }
    public void SkillButtonInitailize()
    {
        for (int i = 0; i < pc.skills.Length; i++)
        {
            if (pc.skills[i])
            {
                AddSkill(i, pc.skills[i]);
            }
        }
    }
    #endregion GUIButtonInitialize
    #region ButtonAdd
    public void ButtonAdd(GameObject obj, Type type, MethodInfo method, object[] parameters, string buttonName, EventTriggerType triggerType)
    {
        EventTrigger triggerToEdit = GameObject.FindGameObjectWithTag(buttonName).GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = triggerType;
        entry.callback.AddListener((eventData) => { method.Invoke(obj.GetComponent(type), parameters); });
        triggerToEdit.triggers.Add(entry);
        string typeMethodButton = type.Name + method.Name + buttonName;
        entries.Add(typeMethodButton, entry);
        test.Add(typeMethodButton);
        //Debug.Log(typeMethodButton);
    }
    public void ButtonRemove(Type type, MethodInfo method, string buttonName)
    {
        EventTrigger eventTrigger = GameObject.FindGameObjectWithTag(buttonName).GetComponent<EventTrigger>();
        EventTrigger triggerToEdit = eventTrigger;
        string typeMethodButton = type.Name + method.Name + buttonName;
        for (int i = triggerToEdit.triggers.Count - 1; i >= 0; i--)
        {
            if (triggerToEdit.triggers[i].callback == entries[typeMethodButton].callback)
            {
                triggerToEdit.triggers.Remove(triggerToEdit.triggers[i]);
            }
        }
        entries.Remove(typeMethodButton);
    }
    #endregion ButtonAdd(discontinued)
    #region SetUpPanels
    private void SetUpEQPanels()
    {
        eqPanel = Instantiate(eqPanelPref, canvas.transform);
        eqPanel.SetActive(false);
        eqConfirm = Instantiate(eqConfirmPref, canvas.transform);
        eqConfirm.SetActive(false);
    }
    private void SetUpShopPanels()
    {
        shopPanel = Instantiate(shopPref, canvas.transform);
        shopPanel.SetActive(false);
        shopPanel.GetComponent<ShopBase>().close.onClick.AddListener(ShopClose);
        sb = shopPanel.GetComponent<ShopBase>();
    }
    private void SetUpChestPanel()
    {
        chestPanel = Instantiate(chestPref, canvas.transform);
        chestPanel.SetActive(false);
        chestPanel.GetComponent<ChestPanel>().closeButton.onClick.AddListener(ChestPopUpOff);
    }
    #endregion SetUpPanels
    #region equipments and skill
    public void EQPanelOn(int typeint)
    {
        StopGame();
        eqPanel.SetActive(true);
        List<Equipment> candidates = new List<Equipment>() { };
        foreach (Equipment eq in pc.itemManager.eqdb.equipments)
        {
            if ((int)eq.eType == typeint)
            {
                candidates.Add(eq);
            }
        }
        for (int i = 0; i < 3; i++)
        {
            int rand = UnityEngine.Random.Range(0, candidates.Count);
            Equipment eq = candidates[rand];
            GameObject eqButton = Instantiate(eqEachItem, eqPanel.transform);
            EquipmentButtonPrefab eqdata = eqButton.GetComponent<EquipmentButtonPrefab>();
            eqdata.image.sprite = eq.thumbnail;
            eqdata.bg.color = eq.color;
            eqdata.eName.text = eq.name;
            eqdata.description.text = eq.description;
            eqdata.skillName.text = eq.skill.skillName;
            eqdata.skillImage.sprite = eq.skill.thumbnail;
            //adding functions to the button
            EventTrigger trigger = eqButton.GetComponent<EventTrigger>();
            EventTrigger.Entry clickEntry = new EventTrigger.Entry();
            clickEntry.eventID = EventTriggerType.PointerClick;
            clickEntry.callback.AddListener((eventData) => { ConfirmEq(eq, eqButton.GetComponent<EquipmentButtonPrefab>()) ; });
            trigger.triggers.Add(clickEntry);
            //clickEntry.callback.AddListener((eventData) => { pc.itemManager.ReplaceEquipment(eq); });
            candidates.Remove(eq);
        }
    }
    public void ConfirmEq(Equipment eq, EquipmentButtonPrefab eqPref)
    {
        if (eqPref.isAvail)
        {
            eqConfirm.SetActive(true);
            foreach (EquipmentButtonPrefab epref in eqPanel.transform.GetComponentsInChildren<EquipmentButtonPrefab>())
            {
                epref.isAvail = false;
            }
            eqConfirm.GetComponent<EqConfirm>().bodyText.text = "Would you like to lock your " + eq.eType.ToString() + " as " + eq.name + "?";
            EventTrigger yesTrigger = eqConfirm.GetComponent<EqConfirm>().yes.GetComponent<EventTrigger>();
            yesTrigger.triggers.Clear();
            EventTrigger.Entry yesEntry = new EventTrigger.Entry();
            yesEntry.eventID = EventTriggerType.PointerClick;
            yesEntry.callback.AddListener((eventData) => { pc.itemManager.ReplaceEquipment(eq); CloseEQ(); });
            yesTrigger.triggers.Add(yesEntry);
            EventTrigger noTrigger = eqConfirm.GetComponent<EqConfirm>().no.GetComponent<EventTrigger>();
            noTrigger.triggers.Clear();
            EventTrigger.Entry noEntry = new EventTrigger.Entry();
            noEntry.eventID = EventTriggerType.PointerClick;
            noEntry.callback.AddListener((eventData) => {GoBackEq(); });
            noTrigger.triggers.Add(noEntry);
        }
    }
    public void CloseEQ()
    {
        ResumeGame();
        eqConfirm.SetActive(false);
        int cCount = eqPanel.transform.childCount;
        for (int i = cCount - 1; i >= 0; i--)
        {
            Destroy(eqPanel.transform.GetChild(i).gameObject);
        }
        eqPanel.SetActive(false);
    }
    public void GoBackEq()
    {
        foreach (EquipmentButtonPrefab epref in eqPanel.transform.GetComponentsInChildren<EquipmentButtonPrefab>())
        {
            epref.isAvail = true;
        }
        eqConfirm.SetActive(false);
    }
    public void AddSkill(int num, Skill skill)
    {
        if (skill.skillType != SkillType.Movement)
        {
            EventTrigger trigger = skillButtons[num].GetComponent<EventTrigger>();
            skillButtons[num].GetComponent<Image>().sprite = skill.thumbnail;
            skillButtons[num].GetComponent<Image>().color = new Color(1, 1, 1, 0.75f);
            skillButtons[num].GetComponent<SkillButton>().thisSkill = skill;
            skill.sb = skillButtons[num].GetComponent<SkillButton>();
            EventTrigger.Entry dEntry = new EventTrigger.Entry();
            EventTrigger.Entry uEntry = new EventTrigger.Entry();
            dEntry.eventID = EventTriggerType.PointerDown;
            uEntry.eventID = EventTriggerType.PointerUp;
            dEntry.callback.AddListener((eventData) => { skill.OnButtonPress(pc); });
            uEntry.callback.AddListener((eventData) => { skill.OnButtonRelease(pc); });
            trigger.triggers.Add(dEntry);
            trigger.triggers.Add(uEntry);
            skill.sb.UpdateCD();
        }
    }
    public void RemoveSkill(int num, Skill skill)
    {
        if (skill.skillType != SkillType.Movement)
        {
            EventTrigger trigger = skillButtons[num].GetComponent<EventTrigger>();
            skillButtons[num].transform.GetComponent<Image>().sprite = null;
            skillButtons[num].GetComponent<Image>().color = new Color(1, 1, 1, 0);
            trigger.triggers.Clear();
        }
    }
    #endregion equipments and skill
    #region chests
    public void ChestItem(int tier)
    {
        List<Item> candidates = new List<Item>();
        foreach (Item item in pc.itemManager.itemdb.items)
        {
            if (item.itemTier == tier)
            {
                candidates.Add(item);
            }
        }
        int indexrand = UnityEngine.Random.Range(0, candidates.Count - 1);
        ChestItemPopUp(candidates[indexrand]);
    }
    public void ChestItemPopUp(Item item)
    {
        StopGame();
        ChestPanel cp = chestPanel.GetComponent<ChestPanel>();
        cp.title.text = item.itemName;
        cp.image.sprite = item.itemThumbnail;
        cp.description.text = item.itemDescription;
        cp.outline.color = pc.playerType.tierColors[item.itemTier];
        cp.panelOutline.effectColor = pc.playerType.tierColors[item.itemTier];
        chestPanel.SetActive(true);
        pc.itemManager.AddItem(item.nameCode);
    }
    public void ChestPopUpOff()
    {
        chestPanel.SetActive(false);
        ResumeGame();
    }
    #endregion chests
    #region Shop
    public void ShopOn(int i)
    {
        StopGame();
        shopPanel.SetActive(true);
        sb.outline.effectColor = pc.playerType.tierColors[i];
        if (shopPanel.GetComponent<ShopBase>().items.Count <= 10)
        {
            List<Item> tempCandidates = new List<Item>();
            foreach (Item item in pc.itemManager.itemdb.items)
            {
                if (item.itemTier == i)
                {
                    tempCandidates.Add(item);
                    tempCandidates.Add(item);
                    tempCandidates.Add(item);
                    tempCandidates.Add(item);
                    tempCandidates.Add(item);
                }
                else if (Mathf.Abs(item.itemTier - i) == 1)
                {
                    tempCandidates.Add(item);
                }
            }
            sb.items = tempCandidates;
        }
        RefreshGold();
        if (sb.shopList.transform.childCount < 3)
        {
            RefreshShop();
        }
    }
    private void RefreshShop()
    {
        int j = sb.shopList.transform.childCount;
        if (j > 0)
        {
            j -= 1;
        }
        for (int i = j; i < 3; i++)
        {
            int rand = UnityEngine.Random.Range(0, sb.items.Count);
            Item it = sb.items[rand];
            GameObject itemButton = Instantiate(shopEachPref, sb.shopList.transform);
            ShopPanelEa sp = itemButton.GetComponent<ShopPanelEa>();
            sp.item = it;
            sp.image.sprite = it.itemThumbnail;
            sp.iName.text = it.itemName;
            sp.description.text = it.itemDescription;
            sp.price.text = it.cost + " Gold";
            sp.bg.color = pc.playerType.tierColors[it.itemTier];
            EventTrigger trigger = itemButton.GetComponent<EventTrigger>();
            EventTrigger.Entry buyEntry = new EventTrigger.Entry();
            buyEntry.eventID = EventTriggerType.PointerClick;
            buyEntry.callback.AddListener((eventData) => {BuyItem(sp.item, sp); });
            trigger.triggers.Add(buyEntry);
            sb.items.Remove(it);
        }
        RefreshGold();
    }
    private void RefreshGold()
    {
        sb.goldAmount.text = "Current Gold : " + pc.gold;
        foreach (ShopPanelEa sp in sb.shopList.transform.GetComponentsInChildren<ShopPanelEa>())
        {
            if (sp.item.cost > pc.gold)
            {
                sp.price.color = pc.playerType.utilColors[2];
            }
            else
            {
                sp.price.color = pc.playerType.utilColors[1];
            }
        }
    }
    private void BuyItem(Item item, ShopPanelEa sp)
    {
        if (item.cost <= pc.gold)
        {
            pc.itemManager.AddItem(item.nameCode);
            pc.gold -= item.cost;
            Destroy(sp.gameObject);
            RefreshShop();
        }
    }
    public void ShopClose()
    {
        shopPanel.SetActive(false);
        ResumeGame();
    }
    #endregion Shop
    #region Boss
    public void BossHPBarSpawn(BossController bc)
    {
        bossBar = Instantiate(bossBarPref, canvas.transform);
        bossBar.GetComponentInChildren<TextMeshProUGUI>().text = bc.BossName;
    }
    public void BossHPBarUpdate(BossController bc)
    {
        float amount = (float) bc.hp / (float)bc.maxHp;
        bossBar.GetComponent<BossHpBar>().redBar.fillAmount = amount;
        if (bossBar.GetComponent<BossHpBar>().redBar.fillAmount == 0)
        {
            Destroy(bossBar);
        }
    }
    #endregion Boss
    public void StopGame()
    {
        Time.timeScale = 0;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
    }
}
