using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenMenu : Screen
{
    [SerializeField] Button buttonGameStart;
    [SerializeField] Button buttonGameLoad;
    [SerializeField] Button buttonExit;

    protected override void InAwake()
    {
        buttonGameStart.onClick.AddListener(() => EventsManager.ChangeGameState(GameState.GameStart));
        buttonGameLoad.onClick.AddListener(ClickLoadGame);
        buttonExit.onClick.AddListener(() => Application.Quit());
    }

    protected override void InOnDestroy()
    {
        return;
    }

    protected override void GameStateChanged(GameState state)
    {
        switch (state)
        {
            case GameState.GameStart:
                gameObject.SetActive(false);
                break;

            case GameState.GameLoad:
                gameObject.SetActive(false);
                break;

            case GameState.MainMenu:
                Setup();
                gameObject.SetActive(true);
                break;
        }
    }

    void Setup()
    {
        buttonGameLoad.interactable = SaveSystem.HasSaveFile();
    }

    void ClickLoadGame()
    {
        buttonGameLoad.interactable = false;
        SaveSystem.Load();
        EventsManager.ChangeGameState(GameState.GameLoad);
    }
}
