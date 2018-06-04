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
    public class ShareViewHolder: RecyclerView.ViewHolder
    {
        public TextView SharedUserName { get; private set; }
        public TextView FilePermissionType { get; private set; }
        public ShareViewHolder(View itemView) : base(itemView)
        {
            Color textColor = Color.White;
           SharedUserName = itemView.FindViewById<TextView>(Resource.Id.sharedUsername);
           FilePermissionType = itemView.FindViewById<Button>(Resource.Id.deleteSharedUser);
            SharedUserName.SetTextColor(textColor);
        }



    }
}