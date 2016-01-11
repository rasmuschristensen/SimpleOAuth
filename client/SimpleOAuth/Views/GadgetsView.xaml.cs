using System;
using System.Collections.Generic;
using Xamarin.Forms;
using SimpleOAuth.ViewModels;

namespace SimpleOAuth.Views
{
	public partial class GadgetsView : ContentPage
	{
		public GadgetsView ()
		{
			this.BindingContext = new GadgetsViewModel ();
			InitializeComponent ();
		}
	}
}

