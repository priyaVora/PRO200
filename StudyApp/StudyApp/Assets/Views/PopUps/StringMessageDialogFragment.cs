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

    public class StringMessageDialogFragment : DialogFragment {

        public static StringMessageDialogFragment CreateInstance(string message) {
            StringMessageDialogFragment frag = new StringMessageDialogFragment();
            Bundle args = new Bundle();
            args.PutString("message", message);
            frag.Arguments = args;
            return frag;
        }

        public override Dialog OnCreateDialog(Bundle savedInstanceState) {
            DialogBuilder builder = new DialogBuilder(Activity);
            builder = builder.SetMessage(savedInstanceState.GetString("message"));
            return builder.Create();
        }
    }
}