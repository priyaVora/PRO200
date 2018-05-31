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
            ActionBar.Hide();

            SetUpPage();
        }

        /// <summary>
        /// Sets up the page for the common layout. You do not need to call this, since it is called by OnCreate() method (unless you need to setup
        /// the page manually)
        /// </summary>
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

        /// <summary>
        /// Sets up the actual layout for the page that should be displayed
        /// </summary>
        /// <param name="pageId">The page's ID to display</param>
        /// <param name="layoutId">The topmost layout's ID of the page that includes every other element inside</param>
        protected void SetUpLayout(int pageId, int layoutId) {
            FrameLayout frame = FindViewById<FrameLayout>(Resource.Id.Common_FrameLayout);
            View calendarPage = LayoutInflater.Inflate(pageId, null);
            frame.AddView(calendarPage.FindViewById<LinearLayout>(layoutId));
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

        /// <summary>
        /// Hides the navbar from showing up on the page as well from being pressed
        /// </summary>
        protected void HideNavBar() {
            FindViewById<LinearLayout>(Resource.Id.Common_NavLayout).Visibility = ViewStates.Gone;
        }

        /// <summary>
        /// Goes to the specified activity
        /// </summary>
        /// <param name="activityType">The activity that you wish to go to (ex. typeof(HomeActivity) to go to HomeActivity)</param>
        /// <param name="finish">Whether or not to finish the current activity to prevent backtracking to the current activity</param>
        /// <param name="extras">Any extra parameters to inclulde in the intent, in the form of the key to the JSON serialized object</param>
        protected void GoToActivity(Type activityType, bool finish, params KeyValuePair<string, string>[] extras) {
            Intent intent = new Intent();
            intent.PutExtra("UserController", JsonConvert.SerializeObject(userController));
            intent.PutExtra("GoalController", JsonConvert.SerializeObject(goalController));
            intent.PutExtra("NoteController", JsonConvert.SerializeObject(noteController));
            intent.PutExtra("FileController", JsonConvert.SerializeObject(fileController));
            intent.PutExtra("CalendarController", JsonConvert.SerializeObject(calendarController));

            foreach (KeyValuePair<string, string> extraValue in extras) {
                intent.PutExtra(extraValue.Key, extraValue.Value);
            }

            StartActivity(intent);
            if (finish) {
                Finish();
            }
        }
    }
}