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
    public class NoteEditActivity : CommonActivity
    {
        private Note note;
        private EditText titleText;
        private EditText contentText;
        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.EditNotesPage);
            Button cancelButton = FindViewById<Button>(Resource.Id.ClNotesButton);
            Button saveButton = FindViewById<Button>(Resource.Id.SaveNotesButton);

            note = JsonConvert.DeserializeObject<Note>(Intent.GetStringExtra("SelectedNote"));

            titleText = FindViewById<EditText>(Resource.Id.NoteTitle);
            titleText.Text = note.Title;

            contentText = FindViewById<EditText>(Resource.Id.NoteContent);
            contentText.Text = note.Content;

            cancelButton.Click += CancelClick;
            saveButton.Click += SaveClick;
        }

        public void CancelClick(object sender, EventArgs args) {
            Toast.MakeText(this, "Cancel notes", ToastLength.Short).Show();
            GoToActivity(typeof(NoteActivity), true);
        }

        public void SaveClick(object sender, EventArgs args)
        {
            Toast.MakeText(this, "Save notes", ToastLength.Short).Show();
            note.GUID = Guid.NewGuid().ToString();
            note.Title = titleText.Text;
            note.Content = contentText.Text;
            NoteController noteController = new NoteController();
            noteController.UpdateNote(note);
            GoToActivity(typeof(NoteActivity), true);
        }

        public void UpdateClick(object sender, EventArgs args)
        {

        }
    }
}