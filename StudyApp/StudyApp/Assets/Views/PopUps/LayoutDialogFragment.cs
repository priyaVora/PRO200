using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace StudyApp.Assets.Views.PopUps {

    public class LayoutDialogFragment : DialogFragment {

        public int LayoutID => Arguments.GetInt("LayoutID");

        public static LayoutDialogFragment CreateInstance(int layoutID) {
            LayoutDialogFragment frag = new LayoutDialogFragment();
            Bundle args = new Bundle();
            args.PutInt("LayoutID", layoutID);
            frag.Arguments = args;
            return frag;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
            View v = inflater.Inflate(LayoutID, container, false);
            return v;
        }
    }
}