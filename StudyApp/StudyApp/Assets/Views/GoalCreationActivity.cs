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
using static Android.Widget.SeekBar;

namespace StudyApp.Assets.Views {

    [Activity(Label = "GoalCreationActivity")]
    public class GoalCreationActivity : CommonActivity, DatePickerDialog.IOnDateSetListener, TimePickerDialog.IOnTimeSetListener, IOnSeekBarChangeListener {

        private EditText taskNameField;
        private EditText taskDescriptionField;
        private SeekBar pointsSlider;
        private TextView pointsLabel;
        private Switch typeSwitch;
        private LinearLayout frequencyLayout;
        private Spinner frequencySpinner;
        private DateTime deadlineDate;
        private DateTime deadlineTime;
        private Button timePickButton;
        private Button datePickButton;
        private int points;

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
            pointsLabel = FindViewById<TextView>(Resource.Id.GoalCreation_PointsTextView);
            typeSwitch = FindViewById<Switch>(Resource.Id.GoalCreation_GoalSwitch);
            frequencyLayout = FindViewById<LinearLayout>(Resource.Id.GoalCreation_FrequencyLayout);
            frequencySpinner = FindViewById<Spinner>(Resource.Id.GoalCreation_FrequencySpinner);
            timePickButton = FindViewById<Button>(Resource.Id.GoalCreation_TimePickButton);
            datePickButton = FindViewById<Button>(Resource.Id.GoalCreation_DatePickButton);

            Button cancelButton = FindViewById<Button>(Resource.Id.GoalCreation_CancelButton);
            Button saveButton = FindViewById<Button>(Resource.Id.GoalCreation_SaveButton);


            cancelButton.Click += Cancel_Click;
            saveButton.Click += Save_Click;

            datePickButton.Click += delegate {
                DatePickerDialog datePickDialog = new DatePickerDialog(this);
                datePickDialog.SetOnDateSetListener(this);
                datePickDialog.Show();
            };
            timePickButton.Click += delegate {
                TimePickerDialog timePickDialog = new TimePickerDialog(this, this, DateTime.Now.Hour, DateTime.Now.Minute, false);
                timePickDialog.Show();
            };

            typeSwitch.CheckedChange += delegate (object sender, CompoundButton.CheckedChangeEventArgs e) {
                if (e.IsChecked) {
                    frequencyLayout.Visibility = ViewStates.Visible;
                } else {
                    frequencyLayout.Visibility = ViewStates.Invisible;
                }
            };

            pointsSlider.SetOnSeekBarChangeListener(this);
            pointsSlider.Max = 19;
            pointsLabel.Text = (pointsSlider.Progress + 1).ToString();

            
            List<string> frequencyItems = new List<string>() { "Daily", "Weekly", "Monthly", "Yearly" };
            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, frequencyItems);
            frequencySpinner.Prompt = "Select a frequency";

            
            frequencySpinner.Adapter = adapter;
            //frequencySpinner.SetSelection(adapter.GetPosition("Daily"));
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

        public void OnDateSet(DatePicker view, int year, int month, int dayOfMonth) {
            deadlineDate = new DateTime(year, month, dayOfMonth);
            datePickButton.Text = deadlineDate.ToShortDateString();
        }

        public void OnTimeSet(TimePicker view, int hourOfDay, int minute) {
            deadlineTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hourOfDay, minute, 0);
            timePickButton.Text = deadlineTime.ToShortTimeString();
        }

        public void OnProgressChanged(SeekBar seekBar, int progress, bool fromUser) {
            points = progress + 1;
            pointsLabel.Text = points.ToString();
        }

        public void OnStartTrackingTouch(SeekBar seekBar) {
            //needed for interface but no code is needed
        }

        public void OnStopTrackingTouch(SeekBar seekBar) {
            //needed for interface but no code is needed
        }
    }
}