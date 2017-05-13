using System;
using System.Collections.Generic;
using Xamarin.Forms;
using XamForms.Controls;
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
		protected override bool OnBackButtonPressed()
		{


			return true;
		}
		ContentPage calendarPage;
		//Label dateInfo;
		Button EvaluationButton;
		public MainTabbed()
		{
			InitializeComponent();


			List<SpecialDate> trainingDays = new List<SpecialDate>();
			//if (Database.CurrentAthlete == null)
			//	Database.AddUser("TestUser", "TestUser", 20, 0, false);

	
			for (int i = 0; i < Database.CurrentAthlete.Trainings.Count; i++)
			{

				DateTime trainingTime = DateTime.ParseExact(Database.CurrentAthlete.Trainings[i].Date, "yyyyMMddHHmmss", CultureInfo.InvariantCulture);
				trainingDays.Add(new SpecialDate(trainingTime) { BackgroundColor = Color.FromHex("d1d1d1"), TextColor = Color.FromHex("827ca9"), Selectable = true });

			}


			var calendar = new XamForms.Controls.Calendar
			{
				
				//MaxDate=DateTime.Now.AddDays(30), 
				MinDate = DateTime.Now.AddDays(-1),
				MultiSelectDates = false,
				SelectedTextColor = Color.White,
				DatesBackgroundColor = Color.FromHex("f0eeed"),
				SelectedBackgroundColor = Color.FromHex("444446"),
				DisabledBackgroundColor = Color.FromHex("f0eeed"),
				DatesBackgroundColorOutsideMonth = Color.FromHex("f0eeed"),
				BorderWidth = 0,
				OuterBorderWidth = 0,
				DisabledBorderWidth = 0,

				DatesTextColor = Color.FromHex("878585"),
				WeekdaysTextColor = Color.FromHex("878585"),
				DatesTextColorOutsideMonth = Color.FromHex("878585"),
				DisabledTextColor = Color.FromHex("878585"),

				SelectedBorderColor = Color.FromHex("827ca9"),


				SpecialDates = trainingDays,
			};



			calendar.DateClicked += (sender, e) =>
			{
				System.Diagnostics.Debug.WriteLine(calendar.SelectedDates);
				OnDateClicked(calendar.SelectedDates);
			};
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
			calendar.TitleLeftArrow.TextColor = Color.Black;
			calendar.TitleRightArrow.TextColor = Color.Black;

			// The root page of your application
			calendarPage = new ContentPage
			{
				//Title = "Calendar",
				Icon = "calendar.png",
				BackgroundColor = Color.FromHex("3d3d4a"),

				Content = new StackLayout
				{
					Padding = new Thickness(0, Device.OS == TargetPlatform.iOS ? 0 : 0, 0, 0),
					Spacing=0,
					Children = {
						s1,
						new StackLayout
						{
							BackgroundColor=Color.White,
							Children = {
							calendar,
							}
						},


						EvaluationButton

					}

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

		public void OnDateClicked(List<DateTime> aDate)
		{
			EvaluationButton.Text = "";
			//dateInfo.Text = "";
			if (aDate.Count > 0)
			{
				List<Training> t = Database.GetTrainingsForDate(aDate[0]);

				if (t.Count>0)
				{
					for (int i = 0; i < t.Count; i++)
					{
						EvaluationButton.Text += t[i].Name;
						//EvaluationButton.Text += aDate[0].Hour.ToString() + ":" + aDate[0].Minute.ToString() + " ";
					//	dateInfo.Text += t[i].Name;
					//	dateInfo.Text += aDate[0].Hour.ToString() + ":" + aDate[0].Minute.ToString()+" ";
					}

				}
			}
		}
	}



	// Red Page
	public partial class CalendarPage : ContentPage
	{
		public CalendarPage()
		{

			Icon = "calendar.png";
				//BackgroundColor = Color.FromHex("3d3d4a"),

			BackgroundColor = Color.White;
			//Title = "Calendar";
			//Icon = "icecream.png";
			LoadDiaryEntrys();


		}
		static List<Button> DiaryInputButtons = new List<Button>();
		async void LoadButtons()
		{
			//string[] splitData = userData.Split(',');
			string diaryInputs = await Database.GetDiaryInputsForUser(Database.CurrentAthlete.PlayerID);

		}
		public void LoadDiaryEntrys()
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
