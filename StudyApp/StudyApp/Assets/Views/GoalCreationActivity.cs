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

namespace StudyApp.Assets.Views {

    [Activity(Label = "GoalCreationActivity")]
    public class GoalCreationActivity : CommonActivity {

        private EditText taskNameField;
        private EditText taskDescriptionField;
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
            pointsSlider = FindViewById<SeekBar>(Resource.Id.GoalCreation_PointsSlider);
            typeSwitch = FindViewById<Switch>(Resource.Id.GoalCreation_GoalSwitch);
            frequencyLayout = FindViewById<LinearLayout>(Resource.Id.GoalCreation_FrequencyLayout);

            Button timePickButton = FindViewById<Button>(Resource.Id.GoalCreation_TimePickButton);
            Button datePickButton = FindViewById<Button>(Resource.Id.GoalCreation_DatePickButton);
            Button cancelButton = FindViewById<Button>(Resource.Id.GoalCreation_CancelButton);
            Button saveButton = FindViewById<Button>(Resource.Id.GoalCreation_SaveButton);

            cancelButton.Click += Cancel_Click;
            saveButton.Click += Save_Click;

            datePickButton.Click += delegate {
                DatePickerDialog datePickDialog = new DatePickerDialog(this, Date, DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                datePickDialog.Show();
            };
        }

        private void Date(object sender, DatePickerDialog.DateSetEventArgs args) {
            Toast.MakeText(Application.Context, args.Date.ToShortDateString(), ToastLength.Short);
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

        public void SliderListen(object sender, EventArgs args) {

        }
    }
}