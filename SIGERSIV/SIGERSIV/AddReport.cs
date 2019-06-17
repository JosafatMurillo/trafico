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

namespace SIGERSIV
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme")]
    public class AddReport : Android.Support.V7.App.AppCompatActivity
    {

        private LinearLayout galery;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.reporte);

            var hideImagesBtn = (Button)FindViewById(Resource.Id.hideImagesBtn);
            galery = (LinearLayout)FindViewById(Resource.Id.galeryPane);

            hideImagesBtn.Click += hideOrShowGalery;
        }

        public void hideOrShowGalery(object sender, EventArgs eventArgs)
        {
            if(galery.IsShown)
            {
                galery.Visibility = ViewStates.Invisible;
            }else
            {
                galery.Visibility = ViewStates.Visible;
            }
        }

    }
}