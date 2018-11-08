using UnityEngine;
using System;

public class Logger
{
    public static bool bDebug = true;

    public static void SetDebug(bool bValue)
    {
        bDebug = bValue;
    }

    public static void Log(string info)
    {
        if (bDebug == true)
        {
            Debug.Log(info);
        }
    }

    public static void Log(string info, UnityEngine.Object obj)
    {
        if (bDebug == true)
        {
            Debug.Log(info, obj);
        }
    }

    public static void LogWarning(string info, UnityEngine.Object obj)
    {
        if (bDebug == true)
        {
            Debug.LogWarning(info, obj);
        }
    }

    public static void LogWarning(string info)
    {
        if (bDebug == true)
        {
            Debug.LogWarning(info);
        }
    }

    public static void LogError(string info)
    {
        if (bDebug == true)
        {
            Debug.LogError(info);
        }
    }

    public static void LogError(string info, UnityEngine.Object obj)
    {
        if (bDebug == true)
        {
            Debug.LogError(info, obj);
        }
    }

    public static void LogException(Exception info)
    {
        if (bDebug == true)
        {
            Debug.LogException(info);
        }
    }

    public static void LogException(Exception info, UnityEngine.Object obj)
    {
        if (bDebug == true)
        {
            Debug.LogException(info, obj);
        }
    }
}
