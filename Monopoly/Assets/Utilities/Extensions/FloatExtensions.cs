using UnityEngine;

namespace RH.Utilities.Extensions
{
    public static class FloatExtensions
    {
        public static bool Approximately(this float a, float b) => 
            Mathf.Approximately(a, b);
    }
}