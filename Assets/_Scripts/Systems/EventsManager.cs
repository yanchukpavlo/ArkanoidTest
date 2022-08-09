using System;

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
}
