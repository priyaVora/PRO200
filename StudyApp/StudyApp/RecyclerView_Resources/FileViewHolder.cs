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

namespace StudyApp.RecyclerView_Resources
{
    public class FileViewHolder : RecyclerView.ViewHolder
    {
        public TextView Caption { get; private set; }

        public FileViewHolder(View itemView) : base(itemView)
        {
            Caption = itemView.FindViewById<TextView>(Resource.Id.textView);
        }
    }
}