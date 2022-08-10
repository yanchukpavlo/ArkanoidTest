using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenMenu : Screen
{
    [SerializeField] Button buttonStart;
    [SerializeField] Button buttonContinue;
    [SerializeField] Button buttonExit;

    protected override void InAwake()
    {
        buttonStart.onClick.AddListener(() => EventsManager.ChangeGameState(GameState.Start));
        buttonContinue.onClick.AddListener(() => EventsManager.ChangeGameState(GameState.Start));
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
            case GameState.Start:
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
        buttonContinue.interactable = false;
    }
}
