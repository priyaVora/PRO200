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

namespace StudyApp.Assets.Views {

    [Activity(Label = "GoalCreationActivity")]
    public class GoalCreationActivity : CommonActivity, SeekBar.IOnSeekBarChangeListener {

        private EditText taskNameField;
        private EditText taskDescriptionField;
        private Button datePickButton;
        private Button timePickButton;
        private SeekBar pointsSlider;
        private Switch typeSwitch;
        private LinearLayout frequencyLayout;

        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);

            FrameLayout frame = FindViewById<FrameLayout>(Resource.Id.Common_FrameLayout);
            View calendarPage = LayoutInflater.Inflate(Resource.Layout.GoalCreationPage, null);
            frame.AddView(calendarPage.FindViewById<LinearLayout>(Resource.Id.GoalCreation_Layout));

            LinearLayout navLayout = FindViewById<LinearLayout>(Resource.Id.Common_NavLayout);
            navLayout.Visibility = ViewStates.Gone;

            taskNameField = FindViewById<EditText>(Resource.Id.GoalCreation_TaskNameField);
            taskDescriptionField = FindViewById<EditText>(Resource.Id.GoalCreation_DescriptionField);
            datePickButton = FindViewById<Button>(Resource.Id.GoalCreation_DatePickButton);
            timePickButton = FindViewById<Button>(Resource.Id.GoalCreation_TimePickButton);
            pointsSlider = FindViewById<SeekBar>(Resource.Id.GoalCreation_PointsSlider);
            typeSwitch = FindViewById<Switch>(Resource.Id.GoalCreation_GoalSwitch);
            frequencyLayout = FindViewById<LinearLayout>(Resource.Id.GoalCreation_FrequencyLayout);
            Button cancelButton = FindViewById<Button>(Resource.Id.GoalCreation_CancelButton);
            Button saveButton = FindViewById<Button>(Resource.Id.GoalCreation_SaveButton);

            cancelButton.Click += Cancel_Click;
            saveButton.Click += Save_Click;
            // TODO: register events to previous views
            datePickButton.Click += delegate (object sender, EventArgs args) { };
            timePickButton.Click += delegate (object sender, EventArgs args) { };
            pointsSlider.SetOnSeekBarChangeListener(this);
        }

        private void Cancel_Click(object sender, EventArgs args) {
            GoBack();
        }

        private void GoBack() {
            OnBackPressed();
            Finish();
        }

        private void Save_Click(object sender, EventArgs args) {
            ParseInput();
            GoBack();
        }

        private void ParseInput() {
        }

        #region Seekbar change listener implementation
        public void OnProgressChanged(SeekBar seekBar, int progress, bool fromUser) {

        }

        public void OnStartTrackingTouch(SeekBar seekBar) {
            throw new NotImplementedException();
        }

        public void OnStopTrackingTouch(SeekBar seekBar) {
            throw new NotImplementedException();
        }
        #endregion
    }
}