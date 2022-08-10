using System;

public enum GameState
{
    Start,
    Continue,
    PauseOn,
    PauseOff,
    BallLose,
    Win,
    MainMenu,
}

public static class EventsManager
{
    public static event Action<BlockType> OnBlockDestroy;
    public static void BlockDestroy(BlockType type)
    {
        OnBlockDestroy.ISafely(type);
    }
    
    public static event Action<PowerupType> OnPowerupPickup;
    public static void PowerupPickup(PowerupType type)
    {
        OnPowerupPickup.ISafely(type);
    }

    public static event Action<GameState> OnGameStateChange;
    public static void ChangeGameState(GameState type)
    {
        OnGameStateChange.ISafely(type);
    }  
}
