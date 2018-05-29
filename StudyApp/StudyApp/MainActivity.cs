using Android.App;
using Android.Widget;
using Android.OS;

namespace StudyApp
{
    [Activity(Label = "StudyApp", MainLauncher = true, Icon ="@drawable/Envision")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
        }
    }
}

