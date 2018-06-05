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
    public class NoteViewHolder: RecyclerView.ViewHolder, View.IOnClickListener, View.IOnLongClickListener
        {

        public TextView NoteTitle { get; private set; }


        private Interface.IItemClickListener itemClickListener;
        public NoteViewHolder(View itemView): base(itemView)
        {
            Color textColor = Color.White;
            NoteTitle = itemView.FindViewById<TextView>(Resource.Id.textView);
            NoteTitle.SetTextColor(textColor);

            itemView.SetOnClickListener(this);
            itemView.SetOnLongClickListener(this);
        }

        public void SetItemClickListener(Interface.IItemClickListener itemClickListener)
        {
            this.itemClickListener = itemClickListener;
        }

        public void OnClick(View v)
        {
            itemClickListener.OnClick(v, AdapterPosition, false);
        }

        public bool OnLongClick(View v)
        {
            itemClickListener.OnClick(v, AdapterPosition, true);
            return true;
        }
    }
}