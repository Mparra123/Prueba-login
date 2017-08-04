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

namespace Prueba_login
{
    [Activity(Label = "Profile")]
    public class TwtterActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            
            //Set our view from the "main" layout resource
            SetContentView(Resource.Layout.ProfileTwtt);

            TextView txt = FindViewById<TextView>(Resource.Id.txtProfileTwtt);
            ImageView image = FindViewById<ImageView>(Resource.Id.imagetwtt);
            
        }
    }
}

