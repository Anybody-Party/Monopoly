using UnityEngine;
using System;
using System.Globalization;
using System.IO;

public class Utility : MonoBehaviour
{
    public static Color enabledColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    public static Color disabledColor = new Color(0.0f, 0.0f, 0.0f, 0.0f);
    public static Color halfAlpha = new Color(1.0f, 1.0f, 1.0f, 0.5f);
    public static Color exploredAlpha = new Color(1.0f, 1.0f, 1.0f, 0.75f); // 0.6f
    public static Color zeroAlpha = new Color(1.0f, 1.0f, 1.0f, 0.0f);

    public static Vector3 MousePointInWorld()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        return new Vector3(ray.origin.x, ray.origin.y, 0);
    }

    public static int GetNumberFromString(string str)
    {
        string resultString = System.Text.RegularExpressions.Regex.Match(str, @"\d+").Value;
        return System.Int32.Parse(resultString);
    }

    public static string ConvertToTime(float seconds)
    {
        TimeSpan t = TimeSpan.FromSeconds(seconds + 1);
        var h = t.TotalHours > 1 ? ((int)t.TotalHours).ToString() + ":" : "";
        return h + string.Format("{1:D2}:{2:D2}", (int)t.TotalHours, t.Minutes, t.Seconds);
    }

    public static double Truncate2(double value, int digits)
    {
        double mult = Math.Pow(10.0, digits);
        double result = Math.Truncate(mult * value) / mult;
        return (float)result;
    }

    public static double RoundToX(double num, int digits = 2)
    {
        var c = (int)Math.Floor(Math.Log10(num) + 1);
        var res = num;
        if (digits < c)
        {
            double d = Mathf.Pow(10, c - digits);
            res = Math.Round(res / d) * d;
        }
        return res;
    }

    static CultureInfo ci = new CultureInfo("en-us");
    public static string[] shortNotation = new string[23] { "", "k", "M", "B", "T", "Qa", "Qi", "Sx", "Sp", "Oc", "No", "Dc", "U", "D", "a", "j", "aa", "jj", "!", "!!", "ss", "dd", "nn", };

    public static string FormatEveryThirdPower(int target, string lowDecimalFormat = "N0", int maxValue = 1000, int minValue = 1000, bool isIgnoreLowDecWhenLessThanMinValue = false, bool auto = true)
    {
        double value = target;
        int baseValue = 0;
        string notationValue = "";
        string toStringValue;

        if (value >= maxValue)
        {
            value /= 1000;
            baseValue++;
            while (Mathf.Round((float)value) >= minValue)
            {
                value /= 1000;
                baseValue++;
            }
            string[] parts = value.ToString().Split('.');
            double part1 = double.Parse(parts[0]);
            if (auto)
            {
                if (part1.ToString().Length == 3)
                    toStringValue = "N0";
                else if (part1.ToString().Length == 2)
                    toStringValue = "N1";
                else if (part1.ToString().Length == 4)
                    toStringValue = "N0";
                else if (part1.ToString().Length == 5)
                    toStringValue = "N0";
                else
                    toStringValue = "N2";
            }
            else
                toStringValue = lowDecimalFormat;

            if (baseValue > shortNotation.Length) return null;
            else notationValue = shortNotation[baseValue];
            return value.ToString(toStringValue) + notationValue;
        }
        else if (isIgnoreLowDecWhenLessThanMinValue)
        {
            if (target < 0d && target > -minValue)
                toStringValue = "N0";
            else if (target > 0d && target < minValue)
                toStringValue = "N0";
            else
                toStringValue = lowDecimalFormat;
        }
        else
            toStringValue = lowDecimalFormat; // string formatting at low numbers
        return value.ToString(toStringValue) + notationValue;
    }

    public static long Fib(int n)
    {
        long a = 0;
        long b = 1;
        // In N steps compute Fibonacci sequence iteratively.
        for (int i = 0; i < n; i++)
        {
            long temp = a;
            a = b;
            b = temp + b;
        }
        return a;
    }

    public static long Fac(long x)
    {
        return (x == 0) ? 1 : x * Fac(x - 1);
    }

    public static void ShuffleArray<T>(T[] a)
    {
        System.Random rand = new System.Random();
        for (int i = a.Length - 1; i > 0; i--)
        {
            int j = rand.Next(0, i + 1);
            T tmp = a[i];
            a[i] = a[j];
            a[j] = tmp;
        }
    }

    public static void ResetAnimtor(Animator _animator)
    {
        _animator.Rebind();
        _animator.Update(0f);
        _animator.Play("Idle", -1, 0f);
        _animator.ResetTrigger("IsStop");
    }

    public static void ResetRigibodyVelocity(Rigidbody _rigidbody)
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
    }

    public static string GetDataPath()
    {
        return Path.Combine(Application.persistentDataPath, "SaveData");
    }
}
