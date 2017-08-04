using Android.App;
using Android.Widget;
using Android.OS;
using Xamarin.Auth;
using Newtonsoft.Json;
using Xamarin.Auth.Prueba_login;
using Android.Graphics;
using System;
using System.Net;

namespace Prueba_login
{
    [Activity(Label = "Prueba Redes Sociales", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {

        public TextView txt;
        public ImageView image;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

             //Set our view from the "main" layout resource
             SetContentView (Resource.Layout.Main);

            Button twiterButton = FindViewById<Button>(Resource.Id.twitterButton);
            Button facebookButton = FindViewById<Button>(Resource.Id.facebookButton);
            Button linkendInButton = FindViewById<Button>(Resource.Id.linkedInButton);
            txt = FindViewById<TextView>(Resource.Id.txtProfileFace);
            image= FindViewById<ImageView>(Resource.Id.imageFace);


            twiterButton.Click += TwiterButton_Click;
            facebookButton.Click += FacebookButton_Click;
            linkendInButton.Click += LinkendInButton_Click;
        }

        private void LinkendInButton_Click(object sender, System.EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void FacebookButton_Click(object sender, System.EventArgs e)
        {

            var auth = new OAuth2Authenticator(
                clientId: "1179568445482769",
                scope: "",
                authorizeUrl: new System.Uri("https://m.facebook.com/dialog/oauth/"),
                redirectUrl: new System.Uri("http://www.facebook.com/connect/login_success.html"));
            
            auth.Completed += FaceBookAuth_CompletedAsync;
            var ui= auth.GetUI(this);
            StartActivity(ui); 	
            

        }

        private async void FaceBookAuth_CompletedAsync(object sender, AuthenticatorCompletedEventArgs e)
        {
            if (e.IsAuthenticated)
            {
                var request = new OAuth2Request("GET", new System.Uri("https://graph.facebook.com/me?fields=name,picture,cover,birthday"),
                
                    null, e.Account);

                var response = await request.GetResponseAsync();
                var json = response.GetResponseText();

                //change and get the profile name
                var faceUser = JsonConvert.DeserializeObject<FaceUser>(json);

                var name = faceUser.name;
                var id = faceUser.id;
                var picture = faceUser.picture.data.url;
                var cover = faceUser.cover.source;


                txt.Text = name;
                image.SetImageBitmap(GetImageBitFromUrl(picture));
                
        
             
            }
        }

        private Bitmap GetImageBitFromUrl(string url)
        {
            Bitmap imageBit = null;

            using (var webClient= new WebClient())
            {
                var imageBytes = webClient.DownloadData(url);
                if (imageBytes != null && imageBytes.Length>0)
                {
                    imageBit = BitmapFactory.DecodeByteArray(imageBytes,0,imageBytes.Length);
                }
            }

            return imageBit;

        }

        void TwiterButton_Click(object sender, System.EventArgs e)
            {
                var auth = new OAuth1Authenticator(
                    consumerKey: "XJVrM50CAFG6BClyvKnFGut3u",
                    consumerSecret: "bAqZ2jskVoRRbwiamq4LqdgO0dQk1qIsCJ9KCxzpbDVzi0L0OI",
                    requestTokenUrl: new System.Uri("https://api.twitter.com/oauth/request_token"),
                    authorizeUrl: new System.Uri("https://api.twitter.com/oauth/authorize"),
                    accessTokenUrl: new System.Uri("https://api.twitter.com/oauth/access_token"),
                    callbackUrl: new System.Uri("http://mobile.twitter.com"));

                {

                };


                auth.Completed += TwitterAuth_CompletedAsync;
                var ui = auth.GetUI(this);
                StartActivity(ui);
            }

            async void TwitterAuth_CompletedAsync(object sender, AuthenticatorCompletedEventArgs e)
            {
                if (e.IsAuthenticated)
                {
                    var request = new OAuth1Request("GET", new System.Uri("http://mobile.twitter.com"),
                        null, e.Account);

                    var response = await request.GetResponseAsync();
                    var json = response.GetResponseText();
                    //var twitteruser = JsonConvert.DeserializeObject<TwitterUser>(json);
                }
            }
        }
    }

