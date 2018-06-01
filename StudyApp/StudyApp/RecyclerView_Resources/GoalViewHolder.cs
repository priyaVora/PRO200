using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace StudyApp.RecyclerView_Resources
{
    public class GoalViewHolder : RecyclerView.ViewHolder
    {
        public TextView GoalTaskName { get; private set; }
        public TextView GoalDescription { get; private set; }

        public TextView GoalPoints { get; private set; }

        public GoalViewHolder(View itemView) : base(itemView)
        {
            Color textColor = Color.White;
            GoalTaskName = itemView.FindViewById<TextView>(Resource.Id.TaskName);
            GoalTaskName.SetTextColor(textColor);

            GoalDescription = itemView.FindViewById<TextView>(Resource.Id.Description);
            GoalDescription.SetTextColor(textColor);

            GoalPoints = itemView.FindViewById<TextView>(Resource.Id.Points);
            GoalPoints.SetTextColor(textColor);

        }
    }
}