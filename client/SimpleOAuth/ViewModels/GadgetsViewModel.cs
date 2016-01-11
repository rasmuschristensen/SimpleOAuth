using System;
using System.Collections.ObjectModel;
using ModernHttpClient;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime;
using SimpleOAuth.Helpers;
using Newtonsoft.Json;
using System.Collections.Generic;
using SimpleOAuth.Models;
using System.Xml.Serialization;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleOAuth.ViewModels
{
	public class GadgetsViewModel : BaseViewModel
	{
		ObservableCollection<Gadget> gadgets = new ObservableCollection<Gadget> ();

		public GadgetsViewModel ()
		{
			Initialize ();
		}

		public ObservableCollection<Gadget> Gadgets {
			get {
				return gadgets;
			}
		}

		public async void Initialize ()
		{
			var loadedGadgets = await LoadGadgets ();

			if (loadedGadgets != null && loadedGadgets.Any ()) {
				gadgets.Clear ();

				foreach (var item in loadedGadgets) {
					gadgets.Add (item);
				}
			}
		}

		public async Task<List<Gadget>> LoadGadgets ()
		{			
			using (var handler = new NativeMessageHandler ()) {
				using (var client = new HttpClient (handler)) {

					var requestMessage = new HttpRequestMessage {
						Method = HttpMethod.Get,
						RequestUri = new Uri ("http://windows:8080/api/gadgets")						
					};

					requestMessage.Headers.Add ("Accept", "application/json");
					requestMessage.Headers.Authorization = new AuthenticationHeaderValue ("Bearer", Settings.ApiAccessToken);


					var response = await client.SendAsync (requestMessage);
					var content = await response.Content.ReadAsStringAsync ();

					return JsonConvert.DeserializeObject<List<Gadget>> (content);
				}
			}
		}
	}
}

