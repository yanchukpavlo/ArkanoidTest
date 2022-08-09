using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class EnumExtensions
{
    public static T RandomEnum<T>()
    {
        System.Random _R = new System.Random();
        var v = Enum.GetValues(typeof(T));
        return (T)v.GetValue(_R.Next(v.Length));
    }

    public static T RandomEnum<T>(this Enum e)
    {
        System.Random _R = new System.Random();
        var v = Enum.GetValues(e.GetType());
        return (T)v.GetValue(_R.Next(v.Length));
    }
}
