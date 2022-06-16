using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ChestPanel : MonoBehaviour
{
    public TextMeshProUGUI title, description;
    public Image outline, image;
    public Outline panelOutline;
    public Button closeButton;
    public AudioSource audioS;

    public void OnObtain(AudioClip audioclip)
    {
        audioS = GetComponent<AudioSource>();
        audioS.clip = audioclip;
        audioS.Play();
    }
}
