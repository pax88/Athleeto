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
			this.Children.Add(new TrainingDiaryPage());
			this.Children.Add(calendarPage);
			this.Children.Add(new HomePage());
			this.Children.Add(new MyStatsPage());
			this.Children.Add(new SettingsPage());
			NavigationPage.SetHasNavigationBar(this, false);
	
			//MainPage = calendarPage;
			//MainPage = this.Children[this.Children.Count-3];
			var masterPage = this as TabbedPage;
			masterPage.CurrentPage = masterPage.Children[masterPage.Children.Count-3];
			masterPage.BarBackgroundColor = Color.FromHex("#dddcdc");

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

}
