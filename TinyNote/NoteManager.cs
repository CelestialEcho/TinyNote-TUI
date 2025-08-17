using System.Collections.Generic;
using System.IO;
using System;

namespace TinyNote
{
    public class NoteManager
    {
        public static readonly string basePath = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\TinyNote";
        public static void Init()
        {
            if (!Directory.Exists(basePath))
            {
                Directory.CreateDirectory(basePath);
            }
        }
        public static void UpdateInfo(out List<Note> buffer, out List<string> pathBuffer)
        {
            buffer = [];
            pathBuffer = [];
            foreach (string file in Directory.EnumerateFiles(basePath, "*.tn"))
            {
                pathBuffer.Add(file);
                buffer.Add(new Note(Path.GetFileName(file), File.ReadAllText(file)));
            }
        }
    }
}


