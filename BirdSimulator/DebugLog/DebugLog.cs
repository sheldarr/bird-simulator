using System;
using System.IO;
using System.Text;

namespace BirdSimulator.DebugLog
{
    public static class DebugLog
    {
        private static readonly StringBuilder log = new StringBuilder();
        public static bool AddDateTime { get; set; }

        public static void Write(string text)
        {
            text = AddDateTime ? DateTime.Now + " " + text : text;
            log.Append(text);
        }

        public static void WriteLine(string text)
        {
            text = AddDateTime ? DateTime.Now + " " + text : text;
            log.Append(text + "\n");
        }

        public static void SaveLog()
        {
            using (var fileStream = File.Open("debug.txt", FileMode.Create, FileAccess.Write))
            {
                using (var streamWriter = new StreamWriter(fileStream))
                {
                    streamWriter.Write(log);
                }
            }          
        }
    }
}
