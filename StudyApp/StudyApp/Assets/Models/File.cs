using System.Collections.Generic;
using Java.Util;

namespace StudyApp.Assets.Models
{
    public class File
    {
        public string GUID { get; set; }
        public string Extension { get; set; }
        public string Name { get; set; }
        public Dictionary<UserAccount, Permission> Users { get; set; }
        public byte[] Content { get; set; }
    }
}