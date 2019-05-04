using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirefoxTelemetry
{
    public static class Utils
    {
        public static string GetPrefs(string path) => Path.Combine(path, "prefs.js");
        public static string ConstructUserPref(string property, string parameter) => $"user_pref(\"{property}\", \"{parameter}\");";
        public static string ConstructUserPref(string property, int parameter) => $"user_pref(\"{property}\", {parameter});";
        public static string ConstructUserPref(string property, bool parameter) => $"user_pref(\"{property}\", {parameter.ToString().ToLower()});";
        public static void BackupPrefs(string path) => File.Copy(path, path + ".backup", true);
        public static void ColorWriteLine(string msg, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(msg);
            Console.ResetColor();
        }
        public static void ColorWrite(string msg, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.Write(msg);
            Console.ResetColor();
        }
        public static bool FirefoxChecker() => Process.GetProcessesByName("firefox").Length > 0;
        
    }
}
