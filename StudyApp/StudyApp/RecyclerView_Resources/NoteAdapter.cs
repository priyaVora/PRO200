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
    public class NoteAdapter : RecyclerView.Adapter
    {

        public NoteAlbum mNoteAlbum;

        public NoteAdapter(NoteAlbum notes)
        {
            mNoteAlbum = notes;
        }


        public override RecyclerView.ViewHolder
          OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.Note_CardView, parent, false);
            RecyclerView_Resources.NoteViewHolder vh = new RecyclerView_Resources.NoteViewHolder(itemView);
            return vh;
        }

        public override void
         OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            RecyclerView_Resources.NoteViewHolder vh = holder as RecyclerView_Resources.NoteViewHolder;
            vh.NoteTitle.Text = mNoteAlbum[position].Title;
        }

        public override int ItemCount
        {
            get { return mNoteAlbum.NumNotes; }
        }

    }
}