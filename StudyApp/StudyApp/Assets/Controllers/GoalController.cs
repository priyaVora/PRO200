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

namespace StudyApp.Assets.Controllers
{
    public class GoalController
    {
        public void CreateGoal(string username, Goal goal)
        {
            ServerIOController serverIo = new ServerIOController();
            serverIo.CreateGoal(goal, username);
        }

        public void CompleteGoal(string goalGuid, string username)
        {
            ServerIOController serverIo = new ServerIOController();
            serverIo.MarkGoalAsCompleted(goalGuid, username);
        }

        public Goal GetGoal(string guid, string username)
        {
            ServerIOController serverIo = new ServerIOController();
            return serverIo.GetGoal(guid, username);
        }

        public List<RecurringGoal> GetUpcomingRecurringGoals(string username)
        {
            ServerIOController serverIo = new ServerIOController();
            List<RecurringGoal> list = new List<RecurringGoal>();
            foreach (Goal g in serverIo.GetUpcomingGoals(username))
            {
                if (g.GetType() == typeof(RecurringGoal)) list.Add((RecurringGoal) g);
            }

            return list;
        }
        public List<NonRecurringGoal> GetUpcomingNonRecurringGoals(string username)
        {
            ServerIOController serverIo = new ServerIOController();
            List<NonRecurringGoal> list = new List<NonRecurringGoal>();
            foreach (Goal g in serverIo.GetUpcomingGoals(username))
            {
                if (g.GetType() == typeof(NonRecurringGoal)) list.Add((NonRecurringGoal) g);
            }

            return list;
        }

        public List<Goal> GetOverdueGoals(string username)
        {
            ServerIOController serverIo = new ServerIOController();
            return serverIo.GetOverdueGoals(username);
        }
    }
}