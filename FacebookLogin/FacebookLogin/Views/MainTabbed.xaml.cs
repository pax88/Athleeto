using System;
using System.Collections.Generic;
using Xamarin.Forms;
using FacebookLogin.Views;
using System.Globalization;

namespace FacebookLogin
{
	public partial class MainTabbed : TabbedPage
	{
		private async void OpenQuestionerClicked(object sender, EventArgs e)
		{
			//QuestionPage qp = new QuestionPage("undefined becouse of calendar");
			//await QuestionPage.LoadQuestions(qp);
			//await Navigation.PushAsync(qp);
		}

		ContentPage calendarPage;
		//Label dateInfo;
		Button EvaluationButton;
		public MainTabbed()
		{
			InitializeComponent();


			PushNotificationsHandler.instance.UpdatePushNotifcaions();

			//dateInfo = new Label
			//{
			//	TextColor = Color.FromHex("827ca9"),
			//	Text = "info",

			//	HorizontalOptions = LayoutOptions.FillAndExpand,
			//	BackgroundColor = Color.FromHex("FFFFFF"),
			//	VerticalOptions = LayoutOptions.FillAndExpand,
			//};
			EvaluationButton = new Button
			{
				TextColor = Color.FromHex("827ca9"),
				Text = "Evaluate",

				HorizontalOptions = LayoutOptions.Center,
				//BackgroundColor = Color.FromHex("FFFFFF"),
				VerticalOptions = LayoutOptions.Center,
				HeightRequest = 100
			};
			EvaluationButton.Clicked += OpenQuestionerClicked;
			Label topBarLabel = new Label
			{
				TextColor = Color.FromHex("dabc2c"),
				Text = "ATHLEETO",
				HorizontalOptions = LayoutOptions.Center,
				BackgroundColor = Color.FromHex("3d3d4a"),
				FontSize=20,


			};
			Label EMPTY = new Label
			{
				TextColor = Color.FromHex("dabc2c"),
				Text = " ",
				HorizontalOptions = LayoutOptions.Center,
				BackgroundColor = Color.FromHex("3d3d4a"),
				FontSize = 20,


			};
			StackLayout s1 = new StackLayout
			{
				BackgroundColor = Color.FromHex("3d3d4a"),
				Spacing = 0,
				Padding=5,
				Children = {
					EMPTY,
						topBarLabel,

					}
			};
	


			calendarPage = new ContentPage
			{
				//Title = "Calendar",
				Icon = "calendar.png",
				BackgroundColor = Color.FromHex("3d3d4a"),
			};
			//this.NavigationController.NavigationBar.TintColor = UIColor.Magenta;

			TrainingDiaryPage tdp = new TrainingDiaryPage();
			this.Children.Add(tdp);
			//this.Children.Add(new DiarySummary());
			this.Children.Add(new HomePage());
			MyStatsPage msp = new MyStatsPage();

			this.Children.Add(msp);
			this.Children.Add(new SettingsPage());
			NavigationPage.SetHasNavigationBar(this, false);
	
			//MainPage = calendarPage;
			//MainPage = this.Children[this.Children.Count-3];
			var masterPage = this as TabbedPage;
			masterPage.CurrentPage = masterPage.Children[masterPage.Children.Count-3];
			masterPage.BarBackgroundColor = Color.FromHex("#2e2e2e");




		}

	}

	public partial class TrainingDiaryPage : ContentPage
	{
		public TrainingDiaryPage()
		{

			BackgroundColor = Color.White;
			//Title = "Diary";
			Icon = "workoutIcon.png";

			//	 < WebView x: Name = "websiteObject"

			//HeightRequest = "500"

			//WidthRequest = "200"

			//Source = "http://www.athleeto.com/social/wp-content/themes/farben-basic%202/stats/MobileStats.php"
			//	/>


			LoadTopBar();

		}
		public void LoadTopBar()
		{ 
			Label topBarLabel = new Label
			{
				TextColor = Color.FromHex("dabc2c"),
				Text = "ATHLEETO",
				HorizontalOptions = LayoutOptions.Center,
				BackgroundColor = Color.FromHex("3d3d4a"),
				FontSize = 20,


			};
			Label EMPTY = new Label
			{
				TextColor = Color.FromHex("dabc2c"),
				Text = " ",
				HorizontalOptions = LayoutOptions.Center,
				BackgroundColor = Color.FromHex("3d3d4a"),
				FontSize = 20,


			};

			StackLayout s1 = new StackLayout
			{
				BackgroundColor = Color.FromHex("3d3d4a"),
				Spacing = 0,
				Padding = 5,
				Children = {
					EMPTY,
						topBarLabel,

					}
			};

	

			Content = new StackLayout
			{
				Orientation = StackOrientation.Vertical,
				Padding = new Thickness(0, 0, 0, 5),
				Spacing = 0,
				Children =
				{
					s1,
				}
			};
		}
	}
	public partial class SettingsPage : ContentPage
	{
		public SettingsPage()
		{

			BackgroundColor = Color.FromHex("4f514f");
			//Title = "Settings";
			//Icon = "icecream.png";
			Icon = "settingsIcon.png";
			SetupFB();
		}
		private async void LogOut(object sender, EventArgs e)
		{
			Application.Current.Properties["AutoLoginValue"] = 0;
			Application.Current.SavePropertiesAsync();
			Navigation.PopToRootAsync();
		}
		private async void ChangeTeamClicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new ChoseTeamPage(false));
		}
		void SetupFB()
		{

			Label topBarLabel = new Label
			{
				TextColor = Color.FromHex("bc9877"),
				Text = "ATHLEETO",
				HorizontalOptions = LayoutOptions.Center,
				BackgroundColor = Color.FromHex("4f514f"),
				FontSize = 20,


			};
			Label EMPTY = new Label
			{
				TextColor = Color.FromHex("bc9877"),
				Text = " ",
				HorizontalOptions = LayoutOptions.Center,
				BackgroundColor = Color.FromHex("4f514f"),
				FontSize = 20,


			};
			Button ChangeTeamButton = new Button
			{
				//TextColor = Color.FromHex("827ca9"),
				TextColor = Color.FromHex("#c8a17c"),
				Text = "Change Team",

				VerticalOptions = LayoutOptions.Center,
				BackgroundColor =Color.FromHex("2e2e2e"),
				BorderRadius = 0,

			};
			ChangeTeamButton.Clicked += ChangeTeamClicked;
			Button LogoutButton = new Button
			{
				//TextColor = Color.FromHex("827ca9"),
				TextColor = Color.White,
				Text = "Log Out",

	
				//BackgroundColor = Color.FromHex("FFFFFF"),
				VerticalOptions = LayoutOptions.Center,

				BackgroundColor = Color.FromHex("f75758"),
				BorderRadius = 0,
			};
			LogoutButton.Clicked += LogOut;
	


	
			StackLayout logOutSl = new StackLayout
			{
				BackgroundColor = Color.FromHex("4f514f"),
				Spacing = 0,
				Padding = new Thickness(20, 0, 20, 20),

				Children = {
					EMPTY,
						LogoutButton,

					}
			};
			StackLayout ChangeTeamButtonStacklayout = new StackLayout
			{
				BackgroundColor = Color.FromHex("4f514f"),
				Spacing = 0,
				Padding = new Thickness(20, 20, 20, 20),

				Children = {
					EMPTY,
						ChangeTeamButton,

					}
			};
	
			StackLayout s1 = new StackLayout
			{
				BackgroundColor = Color.FromHex("4f514f"),
				Spacing = 0,
				Padding = 5,
				Children = {
					EMPTY,
						topBarLabel,

					}
			};

			if (Database.FacebookProfile != null)
			{

	

				Content = new StackLayout
				{
					Orientation = StackOrientation.Vertical,
					Padding = new Thickness(0, 0, 0, 5),
					Spacing = 0,
					BackgroundColor = Color.FromHex("2e2e2e"),
					Children =
				{
					s1,
					new Label
					{
						Text = " ",
						TextColor = Color.FromHex("c8a17c"),
						HorizontalTextAlignment=TextAlignment.Center,
						FontSize = 22,
					},
					new Image{
						Source=Database.FacebookProfile.Picture.Data.Url,
			

					},
					new Label
					{
						Text = Database.FacebookProfile.Name,
						TextColor = Color.FromHex("c8a17c"),
						HorizontalTextAlignment=TextAlignment.Center,
						FontSize = 22,
					},
					new Label
					{
						Text = Database.GetTeamNameFromId(Database.CurrentAthlete.TeamID) ,
							TextColor = Color.FromHex("c8a17c"),
						HorizontalTextAlignment=TextAlignment.Center,
						FontSize = 22,
					},
					new Label
					{
						Text = " ",
						TextColor = Color.FromHex("c8a17c"),
						HorizontalTextAlignment=TextAlignment.Center,
						FontSize = 22,
					},
					ChangeTeamButtonStacklayout,
					logOutSl
					
				}
				};
			}
		
		}
	}


	public partial class MyStatsPage : ContentPage
	{
		public MyStatsPage()
		{

			BackgroundColor = Color.FromHex("4f504f");
			//Title = "My Stats";
			//Icon = "icecream.png";
			Icon = "userInfo.png";


			SetupFB();

		}



		void SetupFB()
		{

			Label topBarLabel = new Label
			{
				TextColor = Color.FromHex("bc9877"),
				Text = "ATHLEETO",
				HorizontalOptions = LayoutOptions.Center,
				BackgroundColor = Color.FromHex("4f504f"),
				FontSize = 20,


			};
			Label EMPTY = new Label
			{
				TextColor = Color.FromHex("bc9877"),
				Text = " ",
				HorizontalOptions = LayoutOptions.Center,
				BackgroundColor = Color.FromHex("4f504f"),
				FontSize = 20,


			};
		
			StackLayout s1 = new StackLayout
			{
				BackgroundColor = Color.FromHex("4f504f"),
				Spacing = 0,
				Padding = 5,
				Children = {
					EMPTY,
						topBarLabel,
						
					}
			};

			if (Database.FacebookProfile != null)
			{
				WebView newWV = new WebView();
				//newWV.Source = "http://www.athleeto.com/social/wp-content/themes/farben-basic%202/stats/MobileStats2.php?athleteID=" + Database.CurrentAthlete.PlayerID + "&teamID=" + Database.CurrentAthlete.TeamID.ToString();
				//newWV.Source = "http://www.athleeto.com/social/wp-content/themes/farben-basic%202/stats/MobileStats2.php?athleteID=10211875891918667&teamID=13";


				//10210411355274507
				newWV.Source = "http://www.athleeto.com/social/mobile-athlete/?athleteID="+Database.FacebookProfile.Id+"&phone=true&period=10";
				newWV.WidthRequest = 200;
				newWV.HeightRequest = 1000;
				BackgroundColor = Color.FromHex("4f504f");
				//Content = new StackLayout
				//{
				//	Padding = new Thickness(0, 0, 0, 5),
				//	Spacing = 0,
				//	Children =
				//	{
				//		//new Label(){Text="Hej"},
				//		newWV
				//	}
				//};

				Content = new StackLayout
				{
					Orientation = StackOrientation.Vertical,
					Padding = new Thickness(0, 0, 0, 5),
					Spacing = 0,
					Children =
				{
					s1,
				//	ChangeTeamButton,
				//	LogoutButton,
					newWV
				}
				};
			}
			LoadData();
		}
		public void LoadData()
		{

		}
	}



}
