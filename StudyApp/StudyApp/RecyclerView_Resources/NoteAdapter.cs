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
using StudyApp.Interface;

namespace StudyApp.RecyclerView_Resources
{
    public class NoteAdapter : RecyclerView.Adapter, IItemClickListener
    {
        private Context currentContext;
        public NoteAlbum mNoteAlbum;

        public NoteAdapter(NoteAlbum notes, Context context)
        {
            mNoteAlbum = notes;
            currentContext = context;
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

        public void OnClick(View itemView, int position, bool isLongClick)
        {
            try
            {
                if (isLongClick)
                {
                    Toast.MakeText(currentContext, "Notes Options", ToastLength.Short).Show();

                    Android.Widget.PopupMenu menu = new Android.Widget.PopupMenu((Activity)currentContext, itemView);
                    menu.MenuInflater.Inflate(Resource.Menu.notes_Options, menu.Menu);

                    menu.MenuItemClick += (s, arg) =>
                    {

                        if(arg.Item.ItemId.Equals(Resource.Id.deleteFile))
                        {
                            Toast.MakeText(currentContext, "Delete Notes", ToastLength.Short).Show();
                        }
                        
                        //    Toast.MakeText(context, "Delete File", ToastLength.Short).Show();
                        //    FileController fileController = new FileController();

                        
                    };

                    menu.Show();

                }
                else
                {
                    Toast.MakeText(currentContext, "Download Notes", ToastLength.Short).Show();
                }
            }
            catch (Exception e)
            {
                Toast.MakeText(currentContext, e.ToString(), ToastLength.Short).Show();

            }

        }

        public override int ItemCount
        {
            get { return mNoteAlbum.NumNotes; }
        }

    }
}