using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoySettingPanel : MonoBehaviour
{
    public RectTransform joyRect, moveRect;
    public Slider joySlider, moveSlider;
    public List<Image> joyImages = new List<Image>();
    public List<Image> moveImages = new List<Image>();
    public AudioSource audioS;
    public void SetUp()
    {
        if (PlayerPrefs.HasKey("joyX") && PlayerPrefs.HasKey("joyY"))
        {
            Debug.Log("sorta working");
            joyRect.anchoredPosition = new Vector2(PlayerPrefs.GetFloat("joyX"), PlayerPrefs.GetFloat("joyY"));
        }
        else
        {
            PlayerPrefs.SetFloat("joyX", joyRect.anchoredPosition.x);
            PlayerPrefs.SetFloat("joyY", joyRect.anchoredPosition.y);
        }
        if (PlayerPrefs.HasKey("moveX") && PlayerPrefs.HasKey("moveY"))
        {
            moveRect.anchoredPosition = new Vector2(PlayerPrefs.GetFloat("moveX"), PlayerPrefs.GetFloat("moveY"));
        }
        else
        {
            PlayerPrefs.SetFloat("moveX", moveRect.anchoredPosition.x);
            PlayerPrefs.SetFloat("moveY", moveRect.anchoredPosition.y);
        }
        //if (PlayerPrefs.HasKey("joyWidth") && PlayerPrefs.HasKey("joyHeight"))
        //{
        //    joyRect.sizeDelta = new Vector2(PlayerPrefs.GetFloat("joyWidth") * 280f, PlayerPrefs.GetFloat("joyHeight") * 280f);
        //}
        //else
        //{
        //    PlayerPrefs.SetFloat("joyWidth", joyRect.sizeDelta.x / 280f);
        //    PlayerPrefs.SetFloat("joyHeight", moveRect.sizeDelta.x / 100f);
        //}
        //if (PlayerPrefs.HasKey("moveWidth") && PlayerPrefs.HasKey("moveHeight"))
        //{
        //    moveRect.sizeDelta = new Vector2(PlayerPrefs.GetFloat("moveWidth") * 100f, PlayerPrefs.GetFloat("moveHeight") * 100f);
        //}
        //else
        //{
        //    PlayerPrefs.SetFloat("moveWidth", moveRect.sizeDelta.x / 100f);
        //    PlayerPrefs.SetFloat("moveHeight", moveRect.sizeDelta.y / 100f);
        //}
        //joySlider.value = moveRect.sizeDelta.x / 100f;
        //moveSlider.value = moveRect.sizeDelta.x / 100f;
    }

    public void StartMovingJoy()
    {
        audioS.Play();
        joyRect.GetComponent<MoveScript>().moving = true;
        joyRect.GetComponent<Image>().color = new Color(1f, 0.75f, 0f, 1f);
        foreach (Image im in joyImages)
        {
            im.color = new Color(1f, 0.75f, 0f, 1f);
        }
    }
    public void ConfirmMovingJoy()
    {
        joyRect.GetComponent<MoveScript>().moving = false;
        joyRect.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.75f);
        foreach (Image im in joyImages)
        {
            im.color = new Color(1f, 1f, 1f, 0.75f);
        }
        PlayerPrefs.SetFloat("joyX", joyRect.anchoredPosition.x);
        PlayerPrefs.SetFloat("joyY", joyRect.anchoredPosition.y);
    }
    public void StartMovingMove()
    {
        audioS.Play();
        moveRect.GetComponent<MoveScript>().moving = true;
        foreach (Image im in moveImages)
        {
            im.color = new Color(1f, 0.75f, 0f, 1f);
        }
    }
    public void ConfirmMovingMove()
    {
        moveRect.GetComponent<MoveScript>().moving = false;
        foreach (Image im in moveImages)
        {
            im.color = new Color(1f, 1f, 1f, 0.75f);
        }
        PlayerPrefs.SetFloat("moveX", moveRect.anchoredPosition.x);
        PlayerPrefs.SetFloat("moveY", moveRect.anchoredPosition.y);
    }
    public void ResetToDefault()
    {
        audioS.Play();
        PlayerPrefs.SetFloat("moveX", 200f);
        PlayerPrefs.SetFloat("moveY", 250f);
        PlayerPrefs.SetFloat("joyX", -250f);
        PlayerPrefs.SetFloat("joyY", 230f);
        SetUp();
    }
    //public void JoySizeChanged()
    //{
    //    joyRect.sizeDelta = new Vector2(joySlider.value * 280f, joySlider.value * 280f);
    //    PlayerPrefs.SetFloat("joyWidth", joyRect.sizeDelta.x/280f);
    //    PlayerPrefs.SetFloat("joyHeight", joyRect.sizeDelta.y/280f);
    //}
    //public void MoveSizeChanged()
    //{
    //    moveRect.sizeDelta = new Vector2(moveSlider.value * 280f, moveSlider.value * 100f);
    //    PlayerPrefs.SetFloat("moveWidth", moveRect.sizeDelta.x/100f);
    //    PlayerPrefs.SetFloat("moveHeight", moveRect.sizeDelta.y/100f);
    //}
    public void CloseButton()
    {
        audioS.Play();
        this.gameObject.SetActive(false);
    }
}
