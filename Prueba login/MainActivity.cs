using Android.App;
using Android.Widget;
using Android.OS;
using Xamarin.Auth;

namespace Prueba_login
{
    [Activity(Label = "Prueba Redes Sociales", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

             //Set our view from the "main" layout resource
             SetContentView (Resource.Layout.Main);

            Button twiterButton = FindViewById<Button>(Resource.Id.twitterButton);
            Button facebookButton = FindViewById<Button>(Resource.Id.facebookButton);
            Button linkendInButton = FindViewById<Button>(Resource.Id.linkedInButton);


            twiterButton.Click += TwiterButton_Click;
            facebookButton.Click += FacebookButton_Click;
            linkendInButton.Click += LinkendInButton_Click;
        }

       private void LinkendInButton_Click(object sender, System.EventArgs e)
        {
            var auth = new OAuth2Authenticator
            (
                clientId: "788gtml82m0qbv",
                clientSecret: "loiTg5Rebyx6oa7v",
                scope: "r_basicprofile",
                authorizeURL: ("https://www.linkedin.com/uas/oauth2/authorization"),
                redirectURL: new System.Uri("https://www.youtube.com/"),
                accessTokenUrl: new System.Uri("htpps://www.linkedin.com/uas/bauth2/accessToken")
             );
            auth.Completed += LinkedinAuth_Completed;
            var ui = auth.GetUI(this);
            startActivity(ui);


        }

        private async void LinkedinAuth_Completed(object sender, AuthenticatorCompletedEventArgs e)
        {
            if (e.IsAuthenticated)
            {
                var request = new OAuth2Request("GET", new System.Uri("https://appi.linkedin.com/v1/people/~:(id,firstName,lastName,) ?" + "format=json" + "&oauth2_access_token=" + e.Account.Propierties["access_token"]),


                    null, e.Account);

                var linkedinresponse = await request.GetResponseAsync();
                var json = linkedinresponse.GetResponseText();
                var linkedinUser = JsonConvert.DeserializeObject<LinkedinUser>(json);
                var name = linkedinUser.FirstName + "" + linkedinUser.lastName;
                var id = linkedinUser.Id;

            }
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
                var request = new OAuth2Request("GET", new System.Uri("https://graph.facebook.com/me?fields=name,picture"),
                
                    null, e.Account);

                var response = await request.GetResponseAsync();
                var json = response.GetResponseText();
            }
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

