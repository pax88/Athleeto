using System;

using Xamarin.Forms;

namespace FacebookLogin
{
	public class DiarySummary : ContentPage
	{
		public DiarySummary()
		{
			Content = new StackLayout
			{
				Children = {
					new Label { Text = "Hello ContentPage" }
				}
			};
		}
	}
}

