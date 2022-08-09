using System;

public static class EventExtensions
{
    public static void ISafely(this Action action)
    {
        action?.Invoke();
    }

    public static void ISafely<T1>(this Action<T1> action, T1 arg1)
    {
        action?.Invoke(arg1);
    }
}
