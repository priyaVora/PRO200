using System;

namespace StudyApp.Assets.Models
{
    public abstract class Goal
    {
        public string GUID { get; set; }
        public string TaskName { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public int Points { get; set; }
        public bool Completed { get; set; } = false;
        public bool Hidden { get; set; } = false;
    }
}