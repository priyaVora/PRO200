using System;
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
using StudyApp.Assets.Controllers;
using StudyApp.Assets.Models;
using StudyApp.Assets.Views;
using StudyApp.Assets.Views.PopUps;
using StudyApp.RecyclerView_Resources;

namespace StudyApp
{
    public class GoalAdapter : RecyclerView.Adapter
    {
        public GoalAlbum mGoalAlbum;
        private Context _context;
        private UserAccount _currentAccount;
        public GoalAdapter(GoalAlbum Goal, Context context)
        {
            mGoalAlbum = Goal;
            _context = context;
            if (_context is CommonActivity activity)
            {
                _currentAccount = activity.userController.CurrentUser;
            }
        }

        public override RecyclerView.ViewHolder
            OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.Goal_CardView, parent, false);
            RecyclerView_Resources.GoalViewHolder vh = new RecyclerView_Resources.GoalViewHolder(itemView);
            return vh;
        }

        public override void
            OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            RecyclerView_Resources.GoalViewHolder vh = holder as RecyclerView_Resources.GoalViewHolder;
            vh.GoalTaskName.Text = mGoalAlbum[position].TaskName;
            vh.GoalDescription.Text = "This is a placeholder for the description.";
            vh.GoalPoints.Text = "100";
        }
        public void OnClick(View itemView, int position, bool isLongClick)
        {
            try
            {
                if (isLongClick)
                {
                    Toast.MakeText(_context, "Goal Options", ToastLength.Short).Show();

                    Android.Widget.PopupMenu menu = new Android.Widget.PopupMenu((Activity)_context, itemView);
                    menu.MenuInflater.Inflate(Resource.Menu.longPress_GoalMenu, menu.Menu);

                    menu.MenuItemClick += (s, arg) =>
                    {
                        if (_context is CommonActivity activity)
                        {   
                            if (arg.Item.ItemId.Equals(Resource.Id.CompleteGoal))
                            {
                                activity.goalController.CompleteGoal(mGoalAlbum[position].GUID, _currentAccount.UserName);
                                new ServerIOController().AddPoints(_currentAccount.UserName, mGoalAlbum[position].Points);
                            }
                            else if (arg.Item.ItemId.Equals(Resource.Id.DeleteGoal))
                            {
                                activity.goalController.CompleteGoal(mGoalAlbum[position].GUID, _currentAccount.UserName);
                            }
                        }
                    };

                    menu.Show();

                }
                //else
                //{
                //    Toast.MakeText(_context, "Downloaded File ", ToastLength.Short).Show();
                //}
            }
            catch (Exception e)
            {
                Toast.MakeText(_context, e.ToString(), ToastLength.Short).Show();

            }

        }


        public override int ItemCount
        {
            get { return mGoalAlbum.NumGoals; }
        }

    }
}