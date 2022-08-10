using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int Score { get; private set; } = 0;
    public static int Level { get; private set; } = 0;
    public static int HP { get; private set; } = 0;

    void Awake()
    {
        EventsManager.OnGameStateChange += GameStateChanged;
        EventsManager.OnBlockDestroy += BlockDestroyed;
    }

    void OnDestroy()
    {
        EventsManager.OnGameStateChange -= GameStateChanged;
        EventsManager.OnBlockDestroy -= BlockDestroyed;
    }

    private void Start()
    {
        EventsManager.ChangeGameState(GameState.MainMenu);
    }

    void GameStateChanged(GameState state)
    {
        switch (state)
        {
            case GameState.Start:
                Score = 0;
                Level = 0;
                HP = 3;
                break;

            case GameState.Continue:
                Score = 0;
                Level = 0;
                HP = 3;

                break;

            case GameState.PauseOn:
                SetPause(true);
                break;

            case GameState.PauseOff:
                SetPause(false);
                break;

            case GameState.BallLose:
                RemoveHP();
                break;

            case GameState.Win:
                AddLevel();
                AddScore(1000);
                break;

            case GameState.MainMenu:
                break;

            default:
                Debug.LogWarningFormat("Cath default state - {0}.", state);
                break;
        }
    }

    void SetPause(bool enable)
    {
        if (enable)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

    void AddScore(int amount)
    {
        Score += amount;
        Screen.OnScoreUpdate.ISafely(Score);
    }

    void AddLevel()
    {
        Level += 1;
        Screen.OnScoreUpdate.ISafely(Level);
    }

    void RemoveHP()
    {
        HP -= 1;
        Screen.OnHealthUpdate.ISafely(HP);
        if (HP == 0) EventsManager.ChangeGameState(GameState.MainMenu);
    }

    void BlockDestroyed(BlockType blockType)
    {
        switch (blockType)
        {
            case BlockType.Normal:
                AddScore(100);
                break;

            case BlockType.Shielded:
                AddScore(200);
                break;

            default:
                Debug.LogWarningFormat("Cath default state - {0}.", blockType);
                break;
        }
    }
}
