using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace FacebookLogin
{
	public partial class AddInjuryPage : ContentPage
	{
		public List<string> InjuryList = new List<string>();
		QuestionPage CurrentaQuesitonPage = null;
		public AddInjuryPage(QuestionPage aQuesitonPage)
		{
			InitializeComponent();

			CurrentaQuesitonPage = aQuesitonPage;

			for (int i = 0; i < Database.InjuryLabelList.Count; i++)
			{
				InjuryList.Add(Database.InjuryLabelList[i]);
			}
			//InjuryList.Add("Leg");
			//InjuryList.Add("Arm");
			//InjuryList.Add("Left Arm");
			//InjuryList.Add("Right Arm");
			//InjuryList.Add("Knee");
			//InjuryList.Add("Shoulder");
			//InjuryList.Add("But");

			UpdateInjuryList(SearchTermEntry.Text);

			Entry e = (Entry)ButtonStackLayout.Children[0];
			e.TextChanged += OnInjuryTextFeildChanged;

		}
		private async void OnInjuryTextFeildChanged(object sender, EventArgs e)
		{

			string input = ((Entry)sender).Text;
			UpdateInjuryList(input);
		}
		bool pushed = false;
		private async void OnBackButtonClicked(object sender, EventArgs e)
		{
			if (pushed == false)
			{

				Navigation.PopModalAsync();
				pushed = true;
			}
		}

		private async void OnSelectInjury(object sender, EventArgs e)
		{
			if (pushed == false)
			{

				QuestionPage.AddedInjurysList.Add(((Button)sender).Text);
				QuestionPage.AddedInjurysValueList.Add(0);

				CurrentaQuesitonPage.UpdateInjuryButtonList();
				Navigation.PopModalAsync();
				pushed = true;
			}
		}

		void UpdateInjuryList(string searchTerm)
		{
			Entry e = (Entry)ButtonStackLayout.Children[0];
			for (int i = ButtonStackLayout.Children.Count-1; i>0 ;i--)
			{
				ButtonStackLayout.Children.RemoveAt(i);
			}
			ButtonStackLayout.Children.Add(e);
			for (int i = 0; i < InjuryList.Count; i++)
			{
				if (QuestionPage.AddedInjurysList.Contains(InjuryList[i]) == true)
					continue;
				Button button = new Button
				{
					HorizontalOptions = LayoutOptions.FillAndExpand,
					TextColor = Color.FromHex("#c8a07c"),
					FontFamily = "Lato-Light",
					BackgroundColor = Color.FromHex("#2e2e2e"),
					FontSize = 26,
					FontAttributes = FontAttributes.Italic,
					Text = InjuryList[i],
					WidthRequest = 90,
					BorderRadius=0,

				};
				button.Clicked += OnSelectInjury;
				bool found = false;

				if (searchTerm == null)
					searchTerm = "";
				if(searchTerm.Length > 0)
				if (InjuryList[i].ToUpper().Contains(searchTerm.ToUpper()))
				{
					found = true;
				}
				if(found || searchTerm.Length == 0)
					ButtonStackLayout.Children.Add(button);
			}
		}
	}
}
