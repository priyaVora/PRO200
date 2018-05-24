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

using DialogBuilder = Android.App.AlertDialog.Builder;

namespace StudyApp.Assets.Views.PopUps {

    public class LoginFailedDialogFragment : DialogFragment {

        public override Dialog OnCreateDialog(Bundle savedInstanceState) {
            DialogBuilder builder = new DialogBuilder(Activity);
            builder = builder.SetMessage("Invalid username or password.");
            return builder.Create();
        }
    }
}