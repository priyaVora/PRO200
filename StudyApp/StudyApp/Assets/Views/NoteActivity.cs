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
using StudyApp.Assets.Models;

namespace StudyApp.Assets.Views {

    [Activity(Label = "NoteActivity")]
    public class NoteActivity : Activity {

        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.NotesPage);






        }
        //Todo: send "SelectedNote" to NoteEditActivity.cs --Empty note if new note.
        //Note note = new Note("owner");

        public void LongPress(object sender, EventArgs args) {

        }
    }
}