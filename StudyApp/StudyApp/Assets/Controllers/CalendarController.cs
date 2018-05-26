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
    public class CalendarController
    {
        public Month GetMonth(string username, int monthOfYear)
        {
            ServerIOController serverIo = new ServerIOController();
            return serverIo.GetMonth(username, monthOfYear);
        }
    }
}