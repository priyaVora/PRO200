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
using Newtonsoft.Json;
using StudyApp.Assets.Controllers;
using StudyApp.Assets.Models;
using StudyApp.Assets.Views;
using StudyApp.Assets.Views.PopUps;
using StudyApp.Interface;

namespace StudyApp.RecyclerView_Resources
{
    public class NoteAdapter : RecyclerView.Adapter, IItemClickListener
    {
        private Context currentContext;
        private UserAccount currentUser;
        public NoteAlbum mNoteAlbum;
        private ShareFileDialog currentDialog;

        public NoteAdapter(NoteAlbum notes, Context context, UserAccount currentUser)
        {
            mNoteAlbum = notes;
            currentContext = context;
            this.currentUser = currentUser;
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
            vh.SetItemClickListener(this);
        }

        public void OnClick(View itemView, int position, bool isLongClick)
        {
            try
            {
                if (isLongClick)
                {
                    Android.Widget.PopupMenu menu = new Android.Widget.PopupMenu((Activity)currentContext, itemView);
                    menu.MenuInflater.Inflate(Resource.Menu.notes_Options, menu.Menu);

                    menu.MenuItemClick += (s, arg) =>
                    {

                        if(arg.Item.ItemId.Equals(Resource.Id.editNote))
                        {
                            NoteController controller = new NoteController();
                            TextView NoteTitle = itemView.FindViewById<TextView>(Resource.Id.textView);

                            List<NoteMini> listOfNoteMini = controller.GetNotePreviews(currentUser.UserName);
                            string title = NoteTitle.Text;
                            string currentNoteId = null;
                            foreach(NoteMini eachMini in listOfNoteMini)
                            {
                                if(eachMini.Title == title)
                                {
                                    currentNoteId = eachMini.GUID;
                                    break;
                                }
                            }

                            Note getStoredNote = controller.GetNote(currentNoteId, currentUser.UserName);
                            CommonActivity activity = currentContext as CommonActivity;

                            activity.GoToActivity(typeof(NoteEditActivity), false, new KeyValuePair<string, string>("SelectedNote", JsonConvert.SerializeObject(getStoredNote)));

                            //FragmentTransaction transaction = ((Activity)currentContext).FragmentManager.BeginTransaction();


                            //currentDialog = new ShareFileDialog(currentContext);
                            //currentDialog.Show(transaction, "dialog fragment");
                        } else if(arg.Item.ItemId.Equals(Resource.Id.deleteNote))

                        {
                            Toast.MakeText(currentContext, "Delete" + itemView.FindViewById<TextView>(Resource.Id.textView).Text, ToastLength.Short).Show();
                            NoteController controller = new NoteController();
                            TextView NoteTitle = itemView.FindViewById<TextView>(Resource.Id.textView);

                            List<NoteMini> listOfNoteMini = controller.GetNotePreviews(currentUser.UserName);
                            string title = NoteTitle.Text;
                            string currentNoteId = null;
                            foreach (NoteMini eachMini in listOfNoteMini)
                            {
                                if (eachMini.Title == title)
                                {
                                    currentNoteId = eachMini.GUID;
                                    break;
                                }
                            }

                            Note deletingNote = controller.GetNote(currentNoteId, currentUser.UserName);
                            controller.DeleteNote(deletingNote.GUID);
                        }    
                    };

                    menu.Show();

                }
                else
                {
                    Toast.MakeText(currentContext, "Download Notes", ToastLength.Short).Show();
                    NoteController controller = new NoteController();
                    TextView NoteTitle = itemView.FindViewById<TextView>(Resource.Id.textView);
                    Toast.MakeText(currentContext, NoteTitle.Text.Trim(), ToastLength.Short).Show();

                    List<NoteMini> listOfNoteMini = controller.GetNotePreviews(currentUser.UserName);
                    string title = NoteTitle.Text;
                    string currentNoteId = null;
                    foreach (NoteMini eachMini in listOfNoteMini)
                    {
                        if (eachMini.Title.Equals(title))
                        {
                            currentNoteId = eachMini.GUID;
                            break;
                        }
                    }

                    Note deletingNote = controller.GetNote(currentNoteId, currentUser.UserName);
                    NoteController noteController = new NoteController();
                    //noteController.deleteNote();
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