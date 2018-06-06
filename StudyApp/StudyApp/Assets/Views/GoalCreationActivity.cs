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

using StudyApp.Assets.Views.PopUps;
using StudyApp.Assets.Models;

namespace StudyApp.Assets.Views {

    [Activity(Label = "GoalCreationActivity")]
    public class GoalCreationActivity : CommonActivity, DatePickerDialog.IOnDateSetListener, TimePickerDialog.IOnTimeSetListener, IOnSeekBarChangeListener {

        private EditText taskNameField;
        private EditText taskDescriptionField;
        private SeekBar pointsSlider;
        private TextView pointsLabel;
        private Switch typeSwitch;
        private LinearLayout frequencyLayout;
        private Button frequencyButton;
        private Button timePickButton;
        private Button datePickButton;
        private int points;
        private DateTime deadlineDate;
        private DateTime deadlineTime;

        private bool isRecurring;

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
            frequencyButton = FindViewById<Button>(Resource.Id.GoalCreation_FrequencyButton);
            timePickButton = FindViewById<Button>(Resource.Id.GoalCreation_TimePickButton);
            datePickButton = FindViewById<Button>(Resource.Id.GoalCreation_DatePickButton);

            Button cancelButton = FindViewById<Button>(Resource.Id.GoalCreation_CancelButton);
            Button saveButton = FindViewById<Button>(Resource.Id.GoalCreation_SaveButton);

            cancelButton.Click += Cancel_Click;
            saveButton.Click += Save_Click;

            frequencyButton.Click += delegate {
                PopupMenu menu = new PopupMenu(this, frequencyButton);
                menu.MenuInflater.Inflate(Resource.Menu.frequency_menu, menu.Menu);

                menu.MenuItemClick += (s, arg) => { frequencyButton.Text = arg.Item.TitleFormatted.ToString(); };

                menu.Show();

            };
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
                if (isRecurring = e.IsChecked) {
                    frequencyLayout.Visibility = ViewStates.Visible;
                } else {
                    frequencyLayout.Visibility = ViewStates.Invisible;
                }
            };

            pointsSlider.SetOnSeekBarChangeListener(this);
            pointsSlider.Max = 19;
            pointsLabel.Text = (pointsSlider.Progress + 1).ToString();
        }

        private void Cancel_Click(object sender, EventArgs args) {
            GoBack();
        }

        private void GoBack() {
            GoToActivity(typeof(CalendarActivity), true);
        }

        private void Save_Click(object sender, EventArgs args) {
            if (!IsNameValid() || !IsDescriptionValid() || !IsFrequencyValid() || !IsDateValid()) {
                string errorMessage = "Invalid input for one or more fields.";
                StringMessageDialogFragment dialog = StringMessageDialogFragment.CreateInstance(errorMessage);
                dialog.Show(FragmentManager, "Invalid information");
            } else {
                Goal goal;

                string name = taskNameField.Text;
                string description = taskDescriptionField.Text;
                DateTime deadline = new DateTime(deadlineDate.Year, deadlineDate.Month, deadlineDate.Day, deadlineTime.Hour, deadlineTime.Minute, deadlineTime.Second);

                if (isRecurring) {
                    TimeSpan frequency;
                    switch (frequencyButton.Text) {
                        case "Daily":
                            frequency = new TimeSpan(1, 0, 0, 0);
                            break;

                        case "Weekly":
                            frequency = new TimeSpan(7, 0, 0, 0);
                            break;

                        case "Monthly":
                            // I CAN'T DEAL WITH MONTHS HERE
                            frequency = new TimeSpan(30, 0, 0, 0);
                            break;

                        case "Yearly":
                            frequency = new TimeSpan(365, 0, 0, 0);
                            break;

                        default:
                            frequency = default(TimeSpan);
                            break;
                    }

                    goal = new RecurringGoal {
                        TaskName = name,
                        Deadline = deadline,
                        Description = description,
                        Frequency = frequency,
                        Points = points,
                        GUID = Guid.NewGuid().ToString()
                    };
                    goalController.CreateRecurringGoal(userController.CurrentUser.UserName, (RecurringGoal) goal);
                } else {
                    goal = new NonRecurringGoal {
                        TaskName = name,
                        Deadline = deadline,
                        Description = description,
                        Points = points,
                        GUID = Guid.NewGuid().ToString()
                    };

                    goalController.CreateNonRecurringGoal(userController.CurrentUser.UserName, (NonRecurringGoal) goal);
                }

                GoBack();
            }
        }

        #region Input Validation
        private bool IsNameValid() {
            return !String.IsNullOrWhiteSpace(taskNameField.Text);
        }

        private bool IsDescriptionValid() {
            return !String.IsNullOrWhiteSpace(taskDescriptionField.Text);
        }

        private bool IsFrequencyValid() {
            if (typeSwitch.Selected) {
                return !String.IsNullOrWhiteSpace(frequencyButton.Text);
            }

            return true;
        }

        private bool IsDateValid() {
            return !String.IsNullOrWhiteSpace(datePickButton.Text);
        }

        private bool IsTimeValid() {
            return !String.IsNullOrWhiteSpace(timePickButton.Text);
        }
        #endregion

        public void OnDateSet(DatePicker view, int year, int month, int dayOfMonth) {
            deadlineDate = new DateTime(year, month + 1, dayOfMonth);
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

        #region Unnecessary interface methods
        public void OnStartTrackingTouch(SeekBar seekBar) {
            //needed for interface but no code is needed
        }

        public void OnStopTrackingTouch(SeekBar seekBar) {
            //needed for interface but no code is needed
        }
        #endregion
    }
}