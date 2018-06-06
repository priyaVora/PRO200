using Android.App;
using Android.Widget;
using Android.OS;
using StudyApp.Assets.Views;

namespace StudyApp
{
    [Activity(Label = "StudyApp", MainLauncher = true, Icon ="@drawable/StudyAppLogo")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            ActionBar.Hide();

            // Set our view from the "main" layout resource
            //SetContentView(Resource.Layout.Main);
            StartActivity(typeof(LoginActivity));
            Finish();
        }
    }
}

