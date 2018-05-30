using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using StudyApp.Assets.Controllers;
using StudyApp.Assets.Models;

namespace StudyApp.Assets.Views {
    [Activity(Label = "NoteEditActivity")]
    public class NoteEditActivity : Activity
    {
        private NoteController noteController;
        private UserController userContoller;
        private Note note;
        private EditText titleText;
        private EditText contentText;
        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.EditNotesPage);
            Button cancelButton = FindViewById<Button>(Resource.Id.CancelNotesButton);
            Button saveButton = FindViewById<Button>(Resource.Id.SaveNotesButton);

            titleText = FindViewById<EditText>(Resource.Id.NoteTitle);
            contentText = FindViewById<EditText>(Resource.Id.NoteContent);

            cancelButton.Click += CancelClick;
            saveButton.Click += SaveClick;

            note = JsonConvert.DeserializeObject<Note>(Intent.GetStringExtra("SelectedNote"));
            noteController = JsonConvert.DeserializeObject<NoteController>(Intent.GetStringExtra("NoteController"));
            userContoller = JsonConvert.DeserializeObject<UserController>(Intent.GetStringExtra("UserController"));
        }

        public void CancelClick(object sender, EventArgs args) {
            GoBackToNotes();
        }

        public void SaveClick(object sender, EventArgs args)
        {
            note.Title = titleText.Text;
            note.Content = contentText.Text;
            noteController.UpdateNote(note);
            GoBackToNotes();
        }

        private void GoBackToNotes()
        {
            Intent intent = new Intent(this, typeof(NoteActivity));
            intent.PutExtra("NoteController", JsonConvert.SerializeObject(noteController));
            intent.PutExtra("UserController", JsonConvert.SerializeObject(userContoller));
            StartActivity(intent);
            Finish();
        }
    }
}