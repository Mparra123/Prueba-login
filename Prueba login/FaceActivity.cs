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
using Xamarin.Auth.Prueba_login;
using Android.Graphics;
using System.Net;
using Xamarin.Auth;
using Newtonsoft.Json;

namespace Prueba_login
{
    [Activity(Label = "Profile")]
    public class FaceActivity : Activity
    {


        public TextView txt;
        public ImageView image;

        public string name;
        public string id;
        public string pic;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //Set our view from the "main" layout resource
            SetContentView(Resource.Layout.ProfileFace);

            txt = FindViewById<TextView>(Resource.Id.txtProfileFace2);
            image = FindViewById<ImageView>(Resource.Id.imageFace2);


                       
            
        }

        


        private Bitmap GetImageBitFromUrl(string url)
        {
            Bitmap imageBit = null;

            using (var webClient = new WebClient())
            {
                var imageBytes = webClient.DownloadData(url);
                if (imageBytes != null && imageBytes.Length > 0)
                {
                    imageBit = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                }
            }

            return imageBit;

        }

    }
}