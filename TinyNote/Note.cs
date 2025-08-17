using System.IO;

namespace TinyNote
{
    public class Note(string name, string? content = null)
    {
        public string name = name;
        public string? content = content;
        public void Update(string? text2write = null)
        {
            if (text2write != null) 
            { 
                this.content = text2write;
            }
        }
    }
}

