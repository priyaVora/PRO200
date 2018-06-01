using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using StudyApp.Assets.Models;
using StudyApp.RecyclerView_Resources;

namespace StudyApp
{
    public class GoalAdapter : RecyclerView.Adapter
    {
        public GoalAlbum mGoalAlbum;
        public GoalAdapter(GoalAlbum Goal)
        {
            mGoalAlbum = Goal;
        }

        public override RecyclerView.ViewHolder
            OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.Goal_CardView, parent, false);
            RecyclerView_Resources.GoalViewHolder vh = new RecyclerView_Resources.GoalViewHolder(itemView);
            return vh;
        }

        public override void
            OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            RecyclerView_Resources.GoalViewHolder vh = holder as RecyclerView_Resources.GoalViewHolder;
            vh.GoalTaskName.Text = mGoalAlbum[position].TaskName;
            vh.GoalDescription.Text = "This is a placeholder for the description.";
            vh.GoalPoints.Text = "100";
        }


        public override int ItemCount
        {
            get { return mGoalAlbum.NumGoals; }
        }

    }
}