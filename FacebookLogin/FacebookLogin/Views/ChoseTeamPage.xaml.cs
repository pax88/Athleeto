using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace FacebookLogin
{
	public partial class ChoseTeamPage : ContentPage
	{
		bool isCoach = false;
		StackLayout ButtonList;
		public ChoseTeamPage(bool aIsCoach)
		{
			isCoach = aIsCoach;
			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);



			//Content = new StackLayout
			//{
			//	Orientation = StackOrientation.Vertical,
			//	VerticalOptions = LayoutOptions.Start,
			//	Padding = new Thickness(20, 20),
			//	Spacing = 20
			//};

			//StackLayout stack;
			//var scrollview = new ScrollView
			//{
			//	Content = new StackLayout
			//	{
			//		Padding = new Thickness(20),
			//	},
			//};
			//Content = scrollview.Content;

			//Content = new ScrollView()
			//{

			//		Content = new StackLayout()
			//		{
			//			Orientation = StackOrientation.Vertical,
			//			VerticalOptions = LayoutOptions.Start,
			//			Padding = new Thickness(20, 20),
			//			Spacing = 20,
			//			Children = {

			//				}
			//		}

			//};
			ButtonList = ((StackLayout)(((ScrollView)((Grid)Content).Children[1]).Content));

	

			//this.BackgroundImage = "bg.png";
	
			List<Team> allTeams = Database.GetLocalDataTeams();

			ReloadData();

		}
		void ReloadData()
		{
			ButtonList.Children.Clear();
			List<Team> allTeams = Database.GetLocalDataTeams();

				Button b2 = new Button();
				b2.Text = "Solo";
				b2.TextColor = Color.White;
				b2.Clicked += OnClikedGoSolo;

				b2.BackgroundColor = Color.FromHex("033358");
				((StackLayout)(((ScrollView)((Grid)Content).Children[1]).Content)).Children.Add(b2);

			for (int i = 0; i < allTeams.Count; i++)
			{
				Button b = new Button();
				b.Text = allTeams[i].TeamName;
				b.TextColor = Color.White;
				b.BackgroundColor = Color.FromHex("033358");
				b.Clicked += OnClikedTeam;
				b.AutomationId = allTeams[i].TeamID.ToString();
				((StackLayout)(((ScrollView)((Grid)Content).Children[1]).Content)).Children.Add(b);
				//((StackLayout)(((ScrollView)(Content)).Content)).Children.Add(b);
			}
			if (isCoach)
			{
				Button b = new Button();
				b.Text = "Add Team";
				b.TextColor = Color.White;
				b.Clicked += OnClikedCreateTeam;

				b.BackgroundColor = Color.FromHex("033358");
				((StackLayout)(((ScrollView)((Grid)Content).Children[1]).Content)).Children.Add(b);
			}
		}
		private async void OnClikedTeam(object sender, EventArgs e)
		{
			await Database.AddUser(Database.FacebookProfile.Id, Database.FacebookProfile.Name, Database.FacebookProfile.AgeRange.Min, int.Parse(((Button)sender).AutomationId),isCoach);

			await Navigation.PushAsync(new MainTabbed());

		}
		private async void OnClikedCreateTeam(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new CreateTeamPage());

		}
		private async void OnClikedGoSolo(object sender, EventArgs e)
		{
			await Database.AddUser(Database.FacebookProfile.Id, Database.FacebookProfile.Name, Database.FacebookProfile.AgeRange.Min, -1, true);

			await Navigation.PushAsync(new MainTabbed());
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			ReloadData();
		}
	}
}
