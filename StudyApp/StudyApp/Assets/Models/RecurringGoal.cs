using System;

namespace StudyApp.Assets.Models
{
    public class RecurringGoal : Goal
    {
        public TimeSpan Frequency { get; set; }
    }
}