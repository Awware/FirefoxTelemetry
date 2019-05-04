using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirefoxTelemetry
{
    class Program
    {
        public static string ProfilesPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Mozilla\Firefox\Profiles";
        static void Main(string[] args)
        {
            Console.Title = "Firefox Telemetry Shutdown v1.1 | By Awware";
            string choosed = "";
            if (Utils.FirefoxChecker())
            {
                Utils.ColorWriteLine("[!] Закройте Firefox перед использованием программы!", ConsoleColor.Red);
                Console.Read();
                return;
            }
            try
            {
                string[] profiles = Directory.GetDirectories(ProfilesPath);

                for (int i = 0; i < profiles.Length; i++)
                    Utils.ColorWriteLine($"{i}. {Path.GetFileName(profiles[i])}");

                Utils.ColorWrite("\n[!] Выберите профиль -> ", ConsoleColor.Yellow);
                choosed = Console.ReadLine();

                string SProfile = profiles[int.Parse(choosed)];
                
                Utils.BackupPrefs(Utils.GetPrefs(SProfile));

                Utils.ColorWriteLine("[@] Произведён бэкап файла - pref.js! -> prefs.js.backup в директории профиля.", ConsoleColor.Yellow);
                Utils.ColorWriteLine("[@] Запуск процесса настройки", ConsoleColor.Yellow);

                ProcessProfile(new Profile()
                {
                    PrefsPath = Utils.GetPrefs(SProfile),
                    ProfilePath = SProfile
                });

                Utils.ColorWriteLine("[@] Выполнено.", ConsoleColor.Green);
            }
            catch (IndexOutOfRangeException) {  Utils.ColorWriteLine($"Выбран несуществующий профиль -> {choosed}", ConsoleColor.Red); }
            catch (FormatException) { Utils.ColorWriteLine($"Неизвестное ID профиля -> {choosed}", ConsoleColor.Red); }
            catch (Exception ex) { Utils.ColorWriteLine($"Неизвестная ошибка!\n{ex.Message}\n{ex.StackTrace}", ConsoleColor.Red); }
            Console.Read();
        }
        static void ProcessProfile(Profile prof)
        {
            StreamWriter sw = new StreamWriter(prof.PrefsPath, true);
            TelemetryOff(sw);
        }
        static void TelemetryOff(StreamWriter writer)
        {
            Utils.ColorWriteLine("[@] Отключение телеметрии", ConsoleColor.Yellow);
            //booleans
            Utils.ColorWriteLine("[@] Добавление булевых параметров", ConsoleColor.Yellow);

            writer.WriteLine(Utils.ConstructUserPref("datareporting.healthreport.uploadEnabled", false));
            writer.WriteLine(Utils.ConstructUserPref("toolkit.telemetry.enabled", false));
            writer.WriteLine(Utils.ConstructUserPref("browser.safebrowsing.malware.enabled", false));
            writer.WriteLine(Utils.ConstructUserPref("browser.safebrowsing.phishing.enabled", false));
            writer.WriteLine(Utils.ConstructUserPref("browser.safebrowsing.downloads.enabled", false));
            writer.WriteLine(Utils.ConstructUserPref("services.sync.prefs.sync.browser.safebrowsing.downloads.enabled", false));
            writer.WriteLine(Utils.ConstructUserPref("media.peerconnection.enabled", false));
            writer.WriteLine(Utils.ConstructUserPref("media.eme.enabled", false));
            writer.WriteLine(Utils.ConstructUserPref("services.sync.prefs.sync.media.eme.enabled", false));
            writer.WriteLine(Utils.ConstructUserPref("geo.enabled", false));
            writer.WriteLine(Utils.ConstructUserPref("extensions.pocket.enabled", false));
            writer.WriteLine(Utils.ConstructUserPref("privacy.trackingprotection.enabled", true));
            //strings
            Utils.ColorWriteLine("[@] Добавление строковых параметров", ConsoleColor.Yellow);

            writer.WriteLine(Utils.ConstructUserPref("geo.wifi.uri", ""));
            writer.WriteLine(Utils.ConstructUserPref("browser.aboutHomeSnippets.updateUrl", ""));

            Utils.ColorWriteLine("[@] Сохранение.");
            writer.Flush();
            writer.Dispose();
        }
    }
}
