using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

enum TitleScreenOptions
{
    Start,
    Option,
    Quit
}

static class Constants {
    public static Vector2[] titleScreenOptionPositions = { new Vector3(-866, -335,0),
                                                    new Vector3(-267, -335,0),
                                                    new Vector3(336, -335,0)};
}

static class Extension 
{
    public static T Next<T>(this T option) where T : struct,Enum
    {
        int newValue = (int)(object)option + 1;
        if (newValue >= Enum.GetNames(typeof(T)).Length) { newValue = 0; }
        return (T)(object)newValue;
    }

    public static T Previous<T>(this T option) where T : struct, Enum
    {
        int newValue = (int)(object)option - 1;
        if (newValue < 0) { newValue = Enum.GetNames(typeof(T)).Length - 1; }
        return (T)(object)newValue;
    }
}
