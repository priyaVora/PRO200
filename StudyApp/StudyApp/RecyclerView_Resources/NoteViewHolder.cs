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
    public class NoteViewHolder: RecyclerView.ViewHolder
    {
        public TextView NoteTitle { get; private set; }
        public NoteViewHolder(View itemView): base(itemView)
        {
            Color textColor = Color.White;
            NoteTitle = itemView.FindViewById<TextView>(Resource.Id.textView);
            NoteTitle.SetTextColor(textColor);

          
        }
    }
}