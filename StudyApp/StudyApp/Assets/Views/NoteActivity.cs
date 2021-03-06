﻿using System;
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
using StudyApp.RecyclerView_Resources;

namespace StudyApp.Assets.Views
{
    
    [Activity(Label = "NoteActivity")]
    public class NoteActivity : CommonActivity
    {
        RecyclerView mRecyclerView;
        RecyclerView.LayoutManager mLayoutManager;
        NoteAdapter mAdapter;
        NoteAlbum mNoteAlbum;

        Note currentNote = null;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            ActionBar.Hide();
            mNoteAlbum = new NoteAlbum();
            SetNoteData();

            FrameLayout frame = FindViewById<FrameLayout>(Resource.Id.Common_FrameLayout);
            View note = LayoutInflater.Inflate(Resource.Layout.NotesPage, null); // Replace the inside of this method call with your desired layout
            frame.AddView(note.FindViewById<LinearLayout>(Resource.Id.Note_Layout));
            SetUpNavBar();

            //Note testNote = new Note();
            ////owner title content guid
            //testNote.Owner = this.userController.CurrentUser.UserName;
            //testNote.GUID = "1";
            //testNote.Title = "Note One";
            //testNote.Content = "Hey Priya!";


            //NoteController noteController = new NoteController();
            //noteController.CreateNote(testNote, this.userController.CurrentUser.UserName);

            
            mRecyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView);

            //Plug in the linear layout manager:
            mLayoutManager = new LinearLayoutManager(this);
            mRecyclerView.SetLayoutManager(mLayoutManager);


            // Plug in my adapter:
            mAdapter = new NoteAdapter(mNoteAlbum, this, this.userController.CurrentUser);
            mRecyclerView.SetAdapter(mAdapter);

            Button addNoteButton = FindViewById<Button>(Resource.Id.NotePage_AddNoteButton);
            addNoteButton.Click += AddNoteClick;



        }

        private void SetNoteData()
        {
            List<Note> notes = this.userController.GetUser(userController.CurrentUser.UserName).ListOfNotes;//CurrentUser.ListOfNotes;
         
            mNoteAlbum = new NoteAlbum(notes.Select(f => (NoteMini)f).ToList());

        }
        //Todo: send "SelectedNote" to NoteEditActivity.cs --Empty note if new note.
        //Note note = new Note("owner");
        

        public void AddNoteClick(object sender, EventArgs args)
        {
            Note note = new Note(userController.CurrentUser.UserName, "", "");
            noteController.CreateNote(note, userController.CurrentUser.UserName);
            GoToActivity(typeof(NoteEditActivity), true, new KeyValuePair<string, string>("SelectedNote", JsonConvert.SerializeObject(note)));
        }


    }
}