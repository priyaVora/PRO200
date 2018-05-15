using System.Collections.Generic;

namespace StudyApp.Assets.Models
{
    public class Year
    {
        public List<Month> Months { get; set; }
        public int YearNum { get; set; }
    }
}