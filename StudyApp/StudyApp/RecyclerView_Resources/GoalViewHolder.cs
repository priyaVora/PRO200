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
        //public ImageView Image { get; private set; }
        public TextView GoalName { get; private set; }
        public TextView GoalSize { get; private set; }

        public GoalViewHolder(View itemView) : base(itemView)
        {
            Color textColor = Color.White;
            // Image = itemView.FindViewById<ImageView>(Resource.Id.imageView);
            GoalName = itemView.FindViewById<TextView>(Resource.Id.textView);
            GoalName.SetTextColor(textColor);

            GoalSize = itemView.FindViewById<TextView>(Resource.Id.GoalSize);
            GoalSize.SetTextColor(textColor);

        }
    }
}