using System.Collections.Generic;
using UnityEngine;

public enum Direction {
    UP,
    DOWN,
    LEFT,
    RIGHT
}

public enum LogType {
    INFO,
    ERROR,
    WARNING
}

public class Utils {
    private static readonly float EPSILON = .0001f;

    public static void DebugDictionary<T, V>(Dictionary<T, V> dictionary) {
        Utils.Log("\nSize: " + dictionary.Count);
        foreach (KeyValuePair<T, V> item in dictionary) {
            Utils.Log(string.Format("Key: {0}, Value: {1}", item.Key, item.Value));
        }
    }

    public static void Log(object msg, LogType logType = LogType.INFO) {
        Debug.Log(GetPrefix(logType) + msg.ToString() + GetPostFix());
    }

    private static string GetPrefix(LogType logType) {
        string format = "<color={0}>";
        string arg = "blue";
        switch (logType) {
            case LogType.ERROR:
                arg = "red";
                break;
            case LogType.WARNING:
                arg = "yellow";
                break;
            case LogType.INFO:
            default:
                break;
        }
        return string.Format(format, arg);
    }

    private static string GetPostFix() {
        return "</color>";
    }

    public static bool AreVectorsEqual(Vector3 one, Vector3 other) {
        return Approximately(one.x, other.x) && Approximately(one.y, other.y) && Approximately(one.z, other.z);
    }

    /// <summary>
    /// For when Mathf.Approximately is not enough.
    /// </summary>
    public static bool Approximately(float oneVlaue, float otherValue) {
        return Mathf.Abs(Mathf.Abs(oneVlaue) - Mathf.Abs(otherValue)) < EPSILON;
    }
}
