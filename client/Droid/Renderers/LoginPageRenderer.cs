using System;
using Xamarin.Forms.Platform.Android;
using Xamarin.Auth;
using System.Net.Http;
using System.Collections.Generic;
using Newtonsoft.Json;
using Android.App;
using SimpleOAuth.Helpers;

namespace SimpleOAuth.Droid.Renderers
{
	public class LoginPageRenderer :  PageRenderer
	{
		public LoginPageRenderer ()
		{
		}

		protected override void OnElementChanged (ElementChangedEventArgs<Xamarin.Forms.Page> e)
		{
			base.OnElementChanged (e);
			LoginToFacebook (false);
		}

		void LoginToFacebook (bool allowCancel)
		{
			var activity = this.Context as Activity;
			OAuth2Authenticator auth = null;

			//todo add to appsettings
			auth = new OAuth2Authenticator (
				clientId      : "933191510091844",
				scope: "email",
				authorizeUrl: new Uri ("https://www.facebook.com/dialog/oauth"),
				redirectUrl: new Uri ("http://yourValidEndpoint.com/login_success.html"));

			auth.AllowCancel = allowCancel;

			// If authorization succeeds or is canceled, .Completed will be fired.
			auth.Completed += async (s, e) => {
				if (!e.IsAuthenticated) {
					return;
				} else {

					var access = e.Account.Properties ["access_token"];

					using (var handler = new ModernHttpClient.NativeMessageHandler ()) {
						using (var client = new HttpClient (handler)) {
							var content = new FormUrlEncodedContent (new[] {
								new KeyValuePair<string, string> ("accesstoken", access),
								new KeyValuePair<string, string> ("grant_type", "facebook")
							});

							var authenticateResponse = await client.PostAsync (new Uri ("http://windows:8080/Token"), content);

							if (authenticateResponse.IsSuccessStatusCode) {

								var responseContent = await authenticateResponse.Content.ReadAsStringAsync ();
								var authenticationTicket = JsonConvert.DeserializeObject<AuthenticatedUser> (responseContent);

								if (authenticationTicket != null) {
									var apiAccessToken = authenticationTicket.Access_Token;
									Settings.ApiAccessToken = apiAccessToken;

									((App)App.Current).PresentMain ();
								}
							}
						}
					}
				}
			};

			var intent = auth.GetUI (activity);
			activity.StartActivity (intent);
		}
	}

	public class AuthenticatedUser
	{
		public string Access_Token {
			get;
			set;
		}
	}
}

