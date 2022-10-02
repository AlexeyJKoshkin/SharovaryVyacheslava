#region

using UnityEngine;

#endregion

namespace Core
{
    public class HLogger
    {
        public static void Log(string message)
        {
            Debug.Log(message);
        }

        public static void LogWarning(string message)
        {
            Debug.LogWarning(message);
        }

        public static void LogError(object message)
        {
            Debug.LogError(message);
        }

        public static void LogInfo(object message)
        {
            Debug.Log($"[INFO] {message}");
        }

        public static void TempLog(object message)
        {
            Debug.Log($"[TempLog] {message}");
        }

        public static void MobLog(object message)
        {
            Debug.Log($"[Mob] {message}");
        }
    }
}