using UnityEngine;

public class GameManager : MonoBehaviour, ISaveable
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
            case GameState.GameStart:
                Score = 0;
                Level = 0;
                HP = 3;
                break;

            case GameState.GameLoad:
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
                DestroyBalls();
                AddLevel();
                AddScore(1000);
                break;

            case GameState.MainMenu:
                SetPause(false);
                DestroyBalls();
                break;

            default:
                Debug.LogWarningFormat("Cath default state - {0}.", state);
                break;
        }
    }

    void DestroyBalls()
    {
        foreach (var item in FindObjectsOfType<Ball>()) Destroy(item.gameObject);
        Ball.ResetBallAmount();
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
        Screen.OnLevelUpdate.ISafely(Level);
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

            case BlockType.Powerup:
                AddScore(150);
                break;

            default:
                Debug.LogWarningFormat("Cath default state - {0}.", blockType);
                break;
        }
    }

    public object CaptureState()
    {
        return new SaveData(Score, Level, HP);
    }

    public void RestoreState(object state)
    {
        var data = (SaveData)state;

        Score = data.score;
        Level = data.level;
        HP = data.hp;

        Screen.OnScoreUpdate.ISafely(data.score);
        Screen.OnLevelUpdate.ISafely(data.level);
        Screen.OnHealthUpdate.ISafely(data.hp);
    }

    [System.Serializable]
    struct SaveData
    {
        public readonly int score;
        public readonly int level;
        public readonly int hp;

        public SaveData(int score, int level, int hp)
        {
            this.score = score;
            this.level = level;
            this.hp = hp;
        }
    }
}
