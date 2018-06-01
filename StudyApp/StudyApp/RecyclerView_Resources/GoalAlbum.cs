using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using StudyApp.Assets.Models;

namespace StudyApp.RecyclerView_Resources
{
    public class GoalAlbum
    {

        static Goal[] mBuiltInGoals = {
            new NonRecurringGoal{ TaskName="Task 1", Description="Finish PluralSight Course.", Points=100},
            new RecurringGoal{ TaskName="Task 2", Description="File all documents and organize them.", Points=300},
             new NonRecurringGoal{ TaskName="Task 1", Description="Finish PluralSight Course.", Points=100},
            new RecurringGoal{ TaskName="Task 2", Description="File all documents and organize them.", Points=300},
             new NonRecurringGoal{ TaskName="Task 1", Description="Finish PluralSight Course.", Points=100},
            new RecurringGoal{ TaskName="Task 2", Description="File all documents and organize them.", Points=300},
             new NonRecurringGoal{ TaskName="Task 1", Description="Finish PluralSight Course.", Points=100},
            new RecurringGoal{ TaskName="Task 2", Description="File all documents and organize them.", Points=300},
             new NonRecurringGoal{ TaskName="Task 1", Description="Finish PluralSight Course.", Points=100},
            new RecurringGoal{ TaskName="Task 2", Description="File all documents and organize them.", Points=300},
             new NonRecurringGoal{ TaskName="Task 1", Description="Finish PluralSight Course.", Points=100},
            new RecurringGoal{ TaskName="Task 2", Description="File all documents and organize them.", Points=300},
             new NonRecurringGoal{ TaskName="Task 1", Description="Finish PluralSight Course.", Points=100},
            new RecurringGoal{ TaskName="Task 2", Description="File all documents and organize them.", Points=300},
             new NonRecurringGoal{ TaskName="Task 1", Description="Finish PluralSight Course.", Points=100},
            new RecurringGoal{ TaskName="Task 2", Description="File all documents and organize them.", Points=300},
        };

        // Array of Goals that make up the album:
        private Goal[] Goals;

        // Create an instance copy of the built-in Goals list, later will grab the user's list of Goals

        public GoalAlbum(List<Goal> goalList)
        {
            Goals = goalList.ToArray();
        }

        // Return the number of Goal in the Goal collection:
        public int NumGoals
        {
            get { return Goals.Length; }
        }


        public Goal this[int i]
        {
            get { return Goals[i]; }
        }
    }
}