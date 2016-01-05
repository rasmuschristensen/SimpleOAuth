using System;
using SimpleOAuth.Views;
using Xamarin.Forms;
using SimpleOAuth.iOS.Renderers;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Auth;
using System.Net.Http;
using System.Collections.Generic;
using UIKit;

[assembly: ExportRenderer (typeof(LoginPage), typeof(LoginPageRenderer))]
namespace SimpleOAuth.iOS.Renderers
{
	public class LoginPageRenderer : PageRenderer
	{
		public LoginPageRenderer ()
		{
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
			OAuth2Authenticator auth = null;
					
			//todo add to appsettings
			auth = new OAuth2Authenticator (
				clientId      : "933191510091844",
				scope: "email",
				authorizeUrl: new Uri ("https://www.facebook.com/dialog/oauth"),
				redirectUrl: new Uri ("http://devlocal.com/NearbyAPI/login_success.html"));

			// we do this to be able to control the cancel flow outself...
			auth.AllowCancel = false;

			auth.Completed += async (sender, e) => {

				if (!e.IsAuthenticated)
					return;
				else {
					var access = e.Account.Properties ["access_token"];

					var client = new HttpClient (new ModernHttpClient.NativeMessageHandler ());

					var content = new FormUrlEncodedContent (new[] {
						new KeyValuePair<string, string> ("accesstoken", access),
						new KeyValuePair<string, string> ("provider", "facebook")
					});


					var authenticateResponse = await client.PostAsync (new Uri ("http://windows:8080/api/authentication/Login"), content);


					if (authenticateResponse.IsSuccessStatusCode) {

						// store user is logged in, request additional info...
					}


					((App)App.Current).PresentMain ();
				}
			};			
				
			UIViewController vc = auth.GetUI ();

			ViewController.AddChildViewController (vc);
			ViewController.View.Add (vc.View);

			// add out custom cancel button, to be able to navigate back
			vc.ChildViewControllers [0].NavigationItem.LeftBarButtonItem = new UIBarButtonItem (
				UIBarButtonSystemItem.Cancel, async (o, eargs) => await App.Current.MainPage.Navigation.PopModalAsync ()
			);
		}
	}
}

