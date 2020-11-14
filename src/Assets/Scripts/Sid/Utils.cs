using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEngine;
using GameJam.Managers;

namespace GameJam
{
    public class Utils
    {
        public static List<T> ListSlice<T>(int from, int to, List<T> e)
        {
            return e.Take(to).Skip(from).ToList();
        }

        public static void AlterFileAtLineIndex(string newLine, string fileName, int lineIndex)
        {
            string[] lines = File.ReadAllLines(fileName, System.Text.Encoding.Unicode);
            lines[lineIndex - 1] = newLine;
            File.WriteAllLines(fileName, lines, System.Text.Encoding.Unicode);
        }

        public static int GetFileLineCount(string fileName)
        {
            string[] lines = File.ReadAllLines(fileName);
            return lines.Count();
        }

        public static bool IsNullOrEmpty<T>(T[] array)
        {
            return array == null || array.Length == 0;
        }

        public static void RunLater(System.Action method, float waitSeconds)
        {
            if (waitSeconds < 0 || method == null)
            {
                return;
            }

            GameManager.instance.StartCoroutine(RunLaterCoroutine(method, waitSeconds));
        }

        private static IEnumerator RunLaterCoroutine(System.Action method, float waitSeconds)
        {
            yield return new WaitForSeconds(waitSeconds);
            method();
        }

        public static float ClampFloat(float value, float min, float max)
        {
            value = value >= min ? value : min;
            value = value <= max ? value : max;
            return value;
        }
    }
}