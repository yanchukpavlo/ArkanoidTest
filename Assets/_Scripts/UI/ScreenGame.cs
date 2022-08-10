using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScreenGame : Screen
{
    [SerializeField] Button buttonPause;
    [SerializeField] TextMeshProUGUI testScore;
    [SerializeField] TextMeshProUGUI testLevel;
    [SerializeField] Image[] imagesHP;

    protected override void InAwake()
    {
        OnLevelUpdate += SetLevel;
        OnScoreUpdate += SetScore;
        OnHealthUpdate += SetHP;

        buttonPause.onClick.AddListener(() => EventsManager.ChangeGameState(GameState.PauseOn));
    }

    protected override void InOnDestroy()
    {
        OnLevelUpdate -= SetLevel;
        OnScoreUpdate -= SetScore;
        OnHealthUpdate -= SetHP;
    }

    protected override void GameStateChanged(GameState state)
    {
        switch (state)
        {
            case GameState.Start:
                gameObject.SetActive(true);
                break;

            case GameState.Continue:
                gameObject.SetActive(true);
                break;

            case GameState.MainMenu:
                gameObject.SetActive(false);
                break;
        }
    }

    void SetHP(int amount)
    {
        for (int i = 0; i < imagesHP.Length; i++)
        {
            if (i < amount) 
                imagesHP[i].enabled = true;
            else 
                imagesHP[i].enabled = false;
        }
    }

    void SetLevel(int value) => testLevel.text = value.ToString();
    void SetScore(int value) => testScore.text = value.ToString();
}
