﻿using System;
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
using StudyApp.Assets.Views;

namespace StudyApp.Assets.Views {

    [Activity]
    public class CommonActivity : Activity {

        protected UserController userController;
        protected NoteController noteController;
        protected FileController fileController;
        protected GoalController goalController;
        protected CalendarController calendarController;

        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);

            SetUpPage();
        }

        protected void SetUpPage() {
            SetContentView(Resource.Layout.CommonLayout);

            #region Controller instantiations

            /*
             * The UserController shouldn't have to be instantiated here, since that would mean that we would lose
             * the currently logged in user. Thus, it should always be in the intent coming into this activity.
             */
            userController = JsonConvert.DeserializeObject<UserController>(Intent.GetStringExtra("UserController"));

            try {
                goalController = JsonConvert.DeserializeObject<GoalController>(Intent.GetStringExtra("GoalController"));
            } catch (ArgumentNullException) {
                goalController = new GoalController();
            }
            try {
                noteController = JsonConvert.DeserializeObject<NoteController>(Intent.GetStringExtra("NoteController"));
            } catch (ArgumentNullException) {noteController = new NoteController();
            }
            try {
                fileController = JsonConvert.DeserializeObject<FileController>(Intent.GetStringExtra("FileController"));
            } catch (ArgumentNullException) {
                fileController = new FileController();
            }
            try {
                calendarController = JsonConvert.DeserializeObject<CalendarController>(Intent.GetStringExtra("CalendarController"));
            } catch (ArgumentNullException) {
                calendarController = new CalendarController();
            }
            #endregion

        }

        protected void SetUpNavBar() {
            Button homeButton = FindViewById<Button>(Resource.Id.NavBar_HomeButton);
            Button calendarButton = FindViewById<Button>(Resource.Id.NavBar_CalendarButton);
            Button filesButton = FindViewById<Button>(Resource.Id.NavBar_FilesButton);
            Button notesButton = FindViewById<Button>(Resource.Id.NavBar_NotesButton);

            homeButton.Click += (sender, args) => GoToActivity(typeof(HomeActivity), true);
            calendarButton.Click += (sender, args) => GoToActivity(typeof(CalendarActivity), false);
            filesButton.Click += (sender, args) => GoToActivity(typeof(FileViewActivity), false);
            notesButton.Click += (sender, args) => GoToActivity(typeof(NoteActivity), false);
        }

        protected void GoToActivity(Type activityType, bool finish, params KeyValuePair<string, string>[] extras) {
            Intent intent = new Intent(this, activityType);
            intent.PutExtra("UserController", JsonConvert.SerializeObject(userController));
            intent.PutExtra("GoalController", JsonConvert.SerializeObject(goalController));
            intent.PutExtra("NoteController", JsonConvert.SerializeObject(noteController));
            intent.PutExtra("FileController", JsonConvert.SerializeObject(fileController));
            intent.PutExtra("CalendarController", JsonConvert.SerializeObject(calendarController));
            StartActivity(intent);
            if (finish) {
                Finish();
            }
        }
    }
}