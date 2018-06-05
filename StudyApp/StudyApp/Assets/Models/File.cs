using System.Collections.Generic;
using Java.Util;

namespace StudyApp.Assets.Models
{
    public class File
    {
        public string GUID { get; set; }
        public string Extension { get; set; }
        public string Name { get; set; }
        public Dictionary<string, Permission> Users { get; set; }
        public byte[] Content { get; set; }

        public static implicit operator FileMini(File f) => new FileMini { GUID = f.GUID, Name = f.Name, Extension = f.Extension, Size = f.Content.Length };
    }
}