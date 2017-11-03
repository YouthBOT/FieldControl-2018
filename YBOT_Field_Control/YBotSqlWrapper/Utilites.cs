using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace HelpfulUtilites
{
    public class Description : Attribute
    {
        public string Text;

        public Description (string text) {
            Text = text;
        }
    }

    public static class Utils
    {
        public static string GetDescription (Enum en) {
            Type type = en.GetType ();
            MemberInfo[] memInfo = type.GetMember (en.ToString ());

            if (memInfo != null && memInfo.Length > 0) {
                object[] attrs = memInfo[0].GetCustomAttributes (typeof (Description), false);

                if (attrs != null && attrs.Length > 0)
                    return ((Description)attrs[0]).Text;
            }

            return en.ToString ();
        }

        public static bool IsEmpty (this string value) {
            return string.IsNullOrWhiteSpace (value);
        }

        public static bool IsNotEmpty (this string value) {
            return !value.IsEmpty ();
        }

        public static string RemoveWhitespace (this string value) {
            return new string (value.ToCharArray ()
                .Where (c => !Char.IsWhiteSpace (c))
                .ToArray ());
        }

        public static int LevenshteinDistance (string s, string t) {
            int n = s.Length;
            int m = t.Length;
            int[,] d = new int[n + 1, m + 1];
            if (n == 0) {
                return m;
            }
            if (m == 0) {
                return n;
            }
            for (int i = 0; i <= n; d[i, 0] = i++)
                ;
            for (int j = 0; j <= m; d[0, j] = j++)
                ;
            for (int i = 1; i <= n; i++) {
                for (int j = 1; j <= m; j++) {
                    int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;
                    d[i, j] = Math.Min (
                        Math.Min (d[i - 1, j] + 1, d[i, j - 1] + 1),
                        d[i - 1, j - 1] + cost);
                }
            }
            return d[n, m];
        }

        public static string ClosestMatchingString (this string value, IEnumerable<string> items) {
            var lowest = int.MaxValue;
            var closestString = string.Empty;

            foreach (var s in items) {
                var distance = LevenshteinDistance (value, s);
                if (distance < lowest) {
                    lowest = distance;
                    closestString = s;
                }
            }

            return closestString;
        }

        public static int Map (this int value, int from1, int from2, int to1, int to2) {
            return to1 + (value - from1) * (to2 - to1) / (from2 - from1);
        }

        public static int Constrain (this int value, int min, int max) {
            if (value < min)
                return min;
            else if (value > max)
                return max;
            else
                return value;
        }

        public static float Map (this float value, float from1, float from2, float to1, float to2) {
            return to1 + (value - from1) * (to2 - to1) / (from2 - from1);
        }

        public static float Constrain (this float value, float min, float max) {
            if (value < min)
                return min;
            else if (value > max)
                return max;
            else
                return value;
        }

        public static double Map (this double value, double from1, double from2, double to1, double to2) {
            return to1 + (value - from1) * (to2 - to1) / (from2 - from1);
        }

        public static double Constrain (this double value, double min, double max) {
            if (value < min)
                return min;
            else if (value > max)
                return max;
            else
                return value;
        }

        public static int ToInt (this double value) {
            return (int)Math.Floor (value + 0.5);
        }

        public static int ToInt (this float value) {
            return (int)Math.Floor (value + 0.5);
        }

        public static bool WithinRange (this double value, double setpoint, double deadband) {
            return ((value <= (setpoint + deadband)) && (value >= (setpoint - deadband)));
        }

        public static bool WithinRange (this int value, int setpoint, int deadband) {
            return ((value <= (setpoint + deadband)) && (value >= (setpoint - deadband)));
        }
    }
}
