// Helpers/Settings.cs
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace SimpleOAuth.Helpers
{
	/// <summary>
	/// This is the Settings static class that can be used in your Core solution or in any
	/// of your client applications. All settings are laid out the same exact way with getters
	/// and setters. 
	/// </summary>
	public static class Settings
	{
		private static ISettings AppSettings {
			get {
				return CrossSettings.Current;
			}
		}

		private const string ApiAccessTokenKey = "apiAccessToken_key";
		private static readonly string ApiAccessTokenDefault = string.Empty;

		public static string ApiAccessToken {
			get {
				return AppSettings.GetValueOrDefault<string> (ApiAccessTokenKey, ApiAccessTokenDefault);
			}
			set {
				AppSettings.AddOrUpdateValue<string> (ApiAccessTokenKey, value);
			}
		}

	}
}