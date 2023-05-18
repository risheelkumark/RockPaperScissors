using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public RectTransform homeScreen;
    public RectTransform gamePanel;

    public Button playBtn;

    public GamePlayUI gamePlayUI;

    public enum Screen
    {
        HomeScreen,
        GamePanel
    }

    private void Awake()
    {
        //Init();
    }

    public void Init(Action onPlay)
    {
        ChangeScreen(Screen.HomeScreen);
        playBtn.onClick.AddListener(() =>
        {
            ChangeScreen(Screen.GamePanel);
            onPlay?.Invoke();
        });
    }

    public void ChangeScreen(Screen screen)
    {
        switch (screen)
        {
            case Screen.HomeScreen:
                homeScreen.gameObject.SetActive(true);
                gamePanel.gameObject.SetActive(false);
                break;
            case Screen.GamePanel:
                homeScreen.gameObject.SetActive(false);
                gamePanel.gameObject.SetActive(true);
                break;
        }
    }

    public void UpdateScore(string score)
    {
        gamePlayUI.UpdateScore(score);
    }
}
