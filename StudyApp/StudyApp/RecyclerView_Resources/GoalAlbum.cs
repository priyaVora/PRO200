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
        // Array of Goals that make up the album:
        private Goal[] Goals;



        // Create an instance copy of the built-in Goals list, later will grab the user's list of Goals

        public GoalAlbum()
        {
            Goals = null;
        }

        public GoalAlbum(Goal[] goals)
        {
            Goals = goals;
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