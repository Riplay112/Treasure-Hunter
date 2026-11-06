using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_menu : MonoBehaviour
{
    [Header("Animasi Settings")]
    public RectTransform[] buttons;
    public CanvasGroup canvasGroup;

    void Start()
    {
        // Fade in canvas
        canvasGroup.alpha = 0;
        canvasGroup.DOFade(1f, 0.8f);

        // Slide in tombol dari bawah, bertahap
        for (int i = 0; i < buttons.Length; i++)
        {
            Vector2 originalPos = buttons[i].anchoredPosition;
            buttons[i].anchoredPosition = originalPos + Vector2.down * 80f;

            buttons[i].DOAnchorPos(originalPos, 0.5f)
                      .SetEase(Ease.OutCubic)
                      .SetDelay(0.1f * i);
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("level_menu");
    }

    public void credits()
    {
        SceneManager.LoadScene("credits");
    }

    public void back()
    {
        SceneManager.LoadScene("main_menu");
    }
}
