using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Support.Design.Widget;
using SIGERSIV.Models;

namespace SIGERSIV
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme")]
    public class MainActivity : AppCompatActivity
    {

        public static Cliente Cliente {get; set;}

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource

            SetContentView(Resource.Layout.activity_main);

            var navigationBottom = FindViewById<BottomNavigationView>(Resource.Id.bottom_nav);
            navigationBottom.NavigationItemSelected += NavigationBottom_NavigationItemSelected;

            LoadFragment(Resource.Id.menu_inicio);
        }

        private void NavigationBottom_NavigationItemSelected(object sender, BottomNavigationView.NavigationItemSelectedEventArgs e)
        {
            LoadFragment(e.Item.ItemId);
        }

        private void LoadFragment(int id)
        {
            Fragment fragment = null;
            switch (id)
            {
                case Resource.Id.menu_inicio:
                    fragment = new Home();
                    break;
                case Resource.Id.menu_perfil:
                    fragment = new Perfil();
                    break;
                case Resource.Id.menu_reporte:
                    StartActivity(typeof(AddReport));
                    break;
            }

            if (fragment == null)
                return;

            FragmentManager.BeginTransaction()
                .Replace(Resource.Id.content_frame, fragment)
                .Commit();
        }
    }
}