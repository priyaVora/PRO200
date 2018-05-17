using System.Collections.Generic;

namespace StudyApp.Assets.Models
{
    public class Day
    {
        public List<Goal> Goals { get; set; }
        public int DayOfMonth { get; set; }
        public string DayOfWeek { get; set; }
    }
}