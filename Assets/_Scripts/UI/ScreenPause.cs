using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenPause : Screen
{
    [SerializeField] Button buttonHome;
    [SerializeField] Button buttonSave;
    [SerializeField] Button buttonContinue;

    protected override void InAwake()
    {
        buttonHome.onClick.AddListener(() => EventsManager.ChangeGameState(GameState.MainMenu));
        buttonSave.onClick.AddListener(() => SaveSystem.Save());
        buttonContinue.onClick.AddListener(() => EventsManager.ChangeGameState(GameState.PauseOff));
    }

    protected override void InOnDestroy()
    {
        return;
    }

    protected override void GameStateChanged(GameState state)
    {
        switch (state)
        {
            case GameState.MainMenu:
                gameObject.SetActive(false);
                break;

            case GameState.PauseOn:
                gameObject.SetActive(true);
                break;

            case GameState.PauseOff:
                gameObject.SetActive(false);
                break;
        }
    }
}
