using System;
using UnityEngine;

namespace squishOS.Helpers
{
    public static class Logger
    {
        public static void LogText(string text)
        {
            var logText = $"{DateTime.Now}: SQUISHOS LOG: {text}";
            Debug.Log(logText);
        }

        public static void LogException(Exception ex)
        {
            var logText = $"{DateTime.Now}: SQUISHOS LOG ERROR: {ex}";
            Debug.Log(logText);
        }
    }
}