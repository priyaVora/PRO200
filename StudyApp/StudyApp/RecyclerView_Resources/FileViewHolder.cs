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
        public ImageView Image { get; private set; }
        public TextView FileName { get; private set; }

        public FileViewHolder(View itemView) : base(itemView)
        {
            Image = itemView.FindViewById<ImageView>(Resource.Id.imageView);
            FileName = itemView.FindViewById<TextView>(Resource.Id.textView);
        }
    }
}