using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace FacebookLogin
{
	public partial class Loading : ContentPage
	{
		public Loading()
		{
			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);
		}
	}
}
