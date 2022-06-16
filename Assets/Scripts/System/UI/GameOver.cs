using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GameOver : MonoBehaviour
{
    public GameObject panel;
    public GameManager gm;
    public GUIManager guim;
    public Volume volume;
    private Vignette vignette;
    public bool gameOver = false;

    public void GameOverInit()
    {
        gameOver = true;
        volume.profile.TryGet<Vignette>(out vignette);
    }
    private void Update()
    {
        if (gameOver && volume.profile.Has<Vignette>())
        {
            vignette.intensity.value += 0.01f;
            if (vignette.intensity.value >= 0.85f)
            {
                gameOver = false;
                guim.GameOverPanelOn();
            }

        }
    }
    public void OnButtonPress()
    {
        gm.GameReset();
    }
}
