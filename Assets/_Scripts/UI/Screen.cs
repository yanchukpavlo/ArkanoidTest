using UnityEngine;
using System;

public abstract class Screen : MonoBehaviour
{
    public static Action<int> OnScoreUpdate;
    public static Action<int> OnHealthUpdate;
    public static Action<int> OnLevelUpdate;

    void Awake()
    {
        gameObject.SetActive(false);
        EventsManager.OnGameStateChange += GameStateChanged;

        InAwake();
    }

    

    void OnDestroy()
    {
        EventsManager.OnGameStateChange -= GameStateChanged;

        InOnDestroy();
    }

    protected abstract void GameStateChanged(GameState state);
    protected abstract void InAwake();
    protected abstract void InOnDestroy();
}
