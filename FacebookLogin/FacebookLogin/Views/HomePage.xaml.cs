using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Plugin.LocalNotifications;
using Xamarin.Forms;

namespace FacebookLogin
{
	public class ButtonAndTraining
	{
		public Button TrainingButton;
		public Button TrainingButton2;
		public string TrainingID;
		public bool HasBeenEvaluated = false;
		public bool isTeamTraining = false;
	}
	public class ButtonAndDate
	{
		public Button DateButton;
		public int Month;
		public int Day;
	}
	public partial class HomePage : ContentPage
	{
		public static DateTime SelectedDate;
		public static HomePage instance;
		public List<ButtonAndTraining> ButtonAndTrainingList = new List<ButtonAndTraining>();
		public List<Button> allButtons = new List<Button>(); 

		public List<ButtonAndDate> currentDateButtons = new List<ButtonAndDate>(); 

		public HomePage()
		{
			InitializeComponent();
			//BackgroundColor = Color.Blue;
			//Title = "Home";
			Icon = "homeIcon.png";

			SelectedDate = DateTime.Today;

			LoadTrainingData();

			((StackLayout)DateButtonStackLayout).Children.Clear();
			currentDateButtons.Clear();
			//AddDateButton(SelectedDate.Day - 5);
			//AddDateButton(SelectedDate.Day - 4);

			AddDateButton(SelectedDate.Day-3,false,date1,SelectedDate.AddDays(-3).DayOfWeek.ToString());
			AddDateButton(SelectedDate.Day-2, false, date2,SelectedDate.AddDays(-2).DayOfWeek.ToString());
			AddDateButton(SelectedDate.Day-1, false, date3,SelectedDate.AddDays(-1).DayOfWeek.ToString());
			AddDateButton(SelectedDate.Day,true,date4,SelectedDate.DayOfWeek.ToString());
			AddDateButton(SelectedDate.Day+1, false, date5,SelectedDate.AddDays(1).DayOfWeek.ToString());
			AddDateButton(SelectedDate.Day+2, false, date6,SelectedDate.AddDays(2).DayOfWeek.ToString());
			AddDateButton(SelectedDate.Day+3, false, date7,SelectedDate.AddDays(3).DayOfWeek.ToString());
			//AddDateButton(SelectedDate.Day + 4);
			//AddDateButton(SelectedDate.Day + 5);
			instance = this;

			// Handle User-NickName
			Application.Current.Properties["AutoLoginValue"] = 1;
			Application.Current.SavePropertiesAsync();


		}
		protected override void OnAppearing()
		{
			base.OnAppearing();

			LoadTrainingData();
		}
		public void LoadTrainingData()
		{
			((StackLayout)ButtonStackLayout).Children.Clear();
			allButtons.Clear();
			List<Training> todaysTraining = Database.GetTrainingsForDate(SelectedDate);
			ButtonAndTrainingList.Clear();
			if (todaysTraining != null)
			{
				for (int i = 0; i < todaysTraining.Count; i++)
					AddButton(todaysTraining[i]);
			}

			AddButtonNewEventButton();


		}
		public void ShowDeleteButtons()
		{
			
			for (int i = 0; i < ButtonAndTrainingList.Count; i++)
			{
				if (ButtonAndTrainingList[i].HasBeenEvaluated)
				{
					((StackLayout)ButtonAndTrainingList[i].TrainingButton2.Parent).Children.RemoveAt(2);
				}
			}
			for (int i = 0; i < allButtons.Count; i++)
			{
				StackLayout sl = (StackLayout)allButtons[i].Parent;
				      

				//Button delBut = new Button
				//{
				//	BorderRadius = 0,
				//	WidthRequest = 100,
				//	HeightRequest = 50,
				//	TextColor = Color.White,
				//	FontFamily = "Lato-Light",
				//	BackgroundColor = Color.Red,
				//	FontSize = 13,
				//	Text = "Delete",
				//	HorizontalOptions= LayoutOptions.EndAndExpand

				//};
				Button delBut = new Button
				{
					Image = "delete.png",
					FontFamily = "Lato-Light",

					Text = "",
					WidthRequest = 32,
					HeightRequest = 32,
					MinimumHeightRequest = 32,
					MinimumWidthRequest = 32,
					BorderRadius = 0,
					Margin = 10,
					BackgroundColor = Color.White,
					HorizontalOptions = LayoutOptions.EndAndExpand,
					VerticalOptions = LayoutOptions.CenterAndExpand,

				};
				delBut.Clicked += OnDeleteTrainingClicked;
				sl.Children.Add(delBut);
			}
		}

		void AddDateButton(int aDate, bool isToday, Label aLabel, string aDay)
		{
			aDay = aDay.Remove(3, aDay.Length - 3);
			aLabel.Text = aDay.ToUpper();
			int buttonDate = SelectedDate.Month;

			if (SelectedDate.Month == 1)
			{
				DateMonthLabel.Text = "January";
			}
			else if (SelectedDate.Month == 2)
			{
				DateMonthLabel.Text = "February";
			}
			else if (SelectedDate.Month == 3)
			{
				DateMonthLabel.Text = "March";
			}
			else if (SelectedDate.Month == 4)
			{
				DateMonthLabel.Text = "April";
			}
			else if (SelectedDate.Month == 5)
			{
				DateMonthLabel.Text = "May";
			}
			else if (SelectedDate.Month == 6)
			{
				DateMonthLabel.Text = "June";
			}
			else if (SelectedDate.Month == 7)
			{
				DateMonthLabel.Text = "July";
			}
			else if (SelectedDate.Month == 8)
			{
				DateMonthLabel.Text = "August";
			}
			else if (SelectedDate.Month == 9)
			{
				DateMonthLabel.Text = "September";
			}
			else if (SelectedDate.Month == 10)
			{
				DateMonthLabel.Text = "October";
			}
			else if (SelectedDate.Month == 11)
			{
				DateMonthLabel.Text = "November";
			}
			else if (SelectedDate.Month == 12)
			{
				DateMonthLabel.Text = "December";
			}

			DateMonthLabel.Text += " " +SelectedDate.Year;

			int dayOffset = aDate;
			Button button = null;
			if (isToday)
			{
				string dateAsString = "";
				dateAsString = aDate.ToString();
				if (aDate <= 9)
					dateAsString = "0" + aDate.ToString();
				 button = new Button
				{
					HorizontalOptions = LayoutOptions.FillAndExpand,
					TextColor = Color.FromHex("#b59272"),
					FontFamily = "Lato-Light",
					BackgroundColor = Color.Transparent,
					FontSize = 30,
					FontAttributes = FontAttributes.Italic,
					Text = dateAsString,

				};

			}
			else
			{
				if (aDate < 1)
				{
					buttonDate = SelectedDate.Month - 1;

					if (SelectedDate.Month == 1 || SelectedDate.Month == 2 || SelectedDate.Month == 4  ||  SelectedDate.Month == 6 || SelectedDate.Month == 8 || SelectedDate.Month == 9 || SelectedDate.Month == 11)
					{
						aDate = 31 + aDate;

					}
					else if (SelectedDate.Month == 3)
					{
						aDate = 28 + aDate;
					}
					else if (SelectedDate.Month == 12 || SelectedDate.Month == 10 || SelectedDate.Month == 5|| SelectedDate.Month == 7)
					{
						aDate = 30 + aDate;
					}
				}
				else
				{
					if (SelectedDate.Month == 10 || SelectedDate.Month == 12|| SelectedDate.Month == 7 ||SelectedDate.Month == 1   || SelectedDate.Month == 3 || SelectedDate.Month == 8 || SelectedDate.Month == 5 )
					{
						if (aDate > 31)
						{
							aDate -= 31;
							buttonDate = SelectedDate.Month + 1;
						}
					}
					else if (SelectedDate.Month == 2)
					{
						if (aDate > 28)
						{
							aDate -= 28;
							buttonDate = SelectedDate.Month + 1;
						}
					}
					else if (  SelectedDate.Month == 11|| SelectedDate.Month == 9|| SelectedDate.Month == 6|| SelectedDate.Month == 4)
					{
						if (aDate > 30)
						{
							aDate -= 30;
							buttonDate = SelectedDate.Month + 1;
					}
					}
					else if (SelectedDate.Month == 12 || SelectedDate.Month == 10 || SelectedDate.Month == 7 || SelectedDate.Month == 5)
					{
						if (aDate > 30)
						{
							aDate -= 30;
							buttonDate = SelectedDate.Month + 1;
						}
					}
				}


				string dateAsString = "";
				dateAsString = aDate.ToString();
				if (aDate <= 9)
					dateAsString = "0" + aDate.ToString();
				 button = new Button
				{
					HorizontalOptions = LayoutOptions.FillAndExpand,
					TextColor = Color.FromHex("#b59272"),
					FontFamily = "Lato-Light",
					BackgroundColor = Color.Transparent,
					FontSize = 20,
					FontAttributes =FontAttributes.Italic,
					Text = dateAsString,


				};
			}
			//Grid.SetColumn(button,dayOffset+4);
			button.Clicked += OnChangeDateClicked; 
			((StackLayout)DateButtonStackLayout).Children.Add(button);

			ButtonAndDate bnd = new ButtonAndDate();

			int buttonMonth = HomePage.SelectedDate.Month;

			if (HomePage.SelectedDate.Day - aDate < -10)
				buttonMonth -= 1;
			if (HomePage.SelectedDate.Day - aDate > 10)
				buttonMonth += 1;
			if (buttonMonth <= 0)
				buttonMonth = 12;
			if (buttonMonth > 12)
				buttonMonth = 1;
			bnd.Month = buttonMonth;
			bnd.Day = aDate;
			bnd.DateButton = button;

			currentDateButtons.Add(bnd);



		}
		void AddButton(Training aTraining)
		{
			DateTime timeForTraining = DateTime.ParseExact(aTraining.Date,"yyyyMMddHHmmss", CultureInfo.InvariantCulture);
			DateTime timeForTrainingEnd = DateTime.ParseExact(aTraining.EndDate, "yyyyMMddHHmmss", CultureInfo.InvariantCulture);
			string bgColor = "549fb3";
			if(aTraining.isTeamTraining != null)
				if ( bool.Parse(aTraining.isTeamTraining) == true )
				{
						bgColor = "619e60";
				}

			string h = timeForTraining.Hour.ToString();
			string m = timeForTraining.Minute.ToString();
			string h2 = timeForTrainingEnd.Hour.ToString();
			string m2 = timeForTrainingEnd.Minute.ToString();

			if(timeForTraining.Hour<10)
				h = "0"+timeForTraining.Hour.ToString();
			if (timeForTraining.Minute < 10)
				m = "0" + timeForTraining.Minute.ToString();
			
			if (timeForTraining.Hour < 10)
				h2 = "0" + timeForTrainingEnd.Hour.ToString();
			if (timeForTraining.Minute < 10)
				m2 = "0" + timeForTrainingEnd.Minute.ToString();
			
			Button button;
			Button button2;
			if (Device.OS == TargetPlatform.iOS)
			{
				string button2color = "2d2d2d";
				string evaluatedText = "";
				button2 = new TestLayoutProblem.MyButton
				{
					TextColor = Color.FromHex("b59272"),
					FontFamily = "Lato-Light",
					BackgroundColor = Color.FromHex(button2color),
					Text = aTraining.Name + evaluatedText,
					HeightRequest = 45,
					BorderRadius = 0,
					HorizontalOptions = LayoutOptions.FillAndExpand,
					FontSize = 22,
				};

				button = new TestLayoutProblem.MyButton
				{
					BorderRadius = 0,
					WidthRequest = 107,
					HeightRequest = 50,
					TextColor = Color.FromHex("FFFFFF"),
					FontFamily = "Lato-Light",
					BackgroundColor = Color.FromHex(bgColor),
					FontSize = 13,
					Text = h + ":" + m + " - " + h2 + ":" + m2,
				};
			}
			else
			{
				string button2color = "2d2d2d";
				string evaluatedText = "";
				 button2 = new TestLayoutProblem.MyButton
				{
					TextColor = Color.FromHex("b59272"),
					FontFamily = "Lato-Light",
					BackgroundColor = Color.FromHex(button2color),
					Text = aTraining.Name + evaluatedText,
					HeightRequest = 45,
					BorderRadius = 0,
					 HorizontalOptions = LayoutOptions.FillAndExpand,
					 FontSize = 22,
				};

				 button = new TestLayoutProblem.MyButton
				{
					BorderRadius = 0,
					WidthRequest = 107,
					HeightRequest = 50,
					TextColor = Color.FromHex("FFFFFF"),
					FontFamily = "Lato-Light",
					BackgroundColor = Color.FromHex(bgColor),
					FontSize = 13,
					Text = h + ":" + m + " - " + h2 + ":" + m2,

				};
			}
			if (aTraining.HasBeenEvaluated == false)
			{
				button.Clicked += OnDoQuestionairClicked;
				button2.Clicked += OnDoQuestionairClicked;
			}
			else
			{ 
				button.Clicked += OnAlreadyEvaluated;
				button2.Clicked += OnAlreadyEvaluated;
			}
			allButtons.Add(button);

			//allButtons.Add(button2);
			StackLayout s1 = new StackLayout
			{
				BackgroundColor = Color.FromHex("2d2d2d"),
				Orientation = StackOrientation.Horizontal,
				Children = {
					button,
					button2
					}
			};
			if (aTraining.HasBeenEvaluated == true)
			{
				Button buttonDone = new Button
				{
					Image="done.png",
					FontFamily = "Lato-Light",

					Text = "",
					WidthRequest = 32,
					HeightRequest = 32,
					MinimumHeightRequest = 32,
					MinimumWidthRequest = 32,
					BorderRadius = 0,
					Margin=10,
					BackgroundColor= Color.FromHex("2d2d2d"),
					HorizontalOptions = LayoutOptions.EndAndExpand,
					VerticalOptions = LayoutOptions.CenterAndExpand
					                                 
				};
				buttonDone.Clicked += OnAlreadyEvaluated;
				s1.Children.Add(buttonDone);
			}
			((StackLayout)ButtonStackLayout).Children.Add(s1);
			ButtonAndTraining bat = new ButtonAndTraining();
			bat.TrainingButton = button;
			bat.TrainingButton2 = button2;
			bat.TrainingID = aTraining.PraticeID;
			bat.isTeamTraining = bool.Parse(aTraining.isTeamTraining);
			bat.HasBeenEvaluated = aTraining.HasBeenEvaluated;
			ButtonAndTrainingList.Add(bat);



		}
		void AddButtonNewEventButton()
		{ 

			TestLayoutProblem.MyButtonEvent button = new TestLayoutProblem.MyButtonEvent
			{
				BorderRadius = 0,
				WidthRequest = 100,
				HeightRequest = 50,
				TextColor = Color.FromHex("#FFFFFF"),
				FontFamily = "Lato-Light",
				BackgroundColor = Color.FromHex("b59272"),
				FontSize = 26,
				Text = "+",
	

			};
			TestLayoutProblem.MyButtonEvent button2 = new TestLayoutProblem.MyButtonEvent
			{
				BorderRadius = 2,
				BorderColor = Color.FromHex("b59272"),
				TextColor = Color.FromHex("b59272"),
				FontFamily = "Lato-Light",
				BackgroundColor = Color.FromHex("4f514f"),
				Text = "New event",
				HeightRequest = 40,
				FontSize = 22,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				Margin = new Thickness(2,2,2,2),

			};

			//if (Device.OS == TargetPlatform.iOS)
			//{
			//	button.Clicked += OnAddNewEventClicked;
			//	button2.Clicked += OnAddNewEventClicked;
			//}
			StackLayout s1 = new StackLayout
			{
				BackgroundColor = Color.FromHex("b59272"),
				Orientation = StackOrientation.Horizontal,

				Children = {
					button,
					button2
					}
			};
			
			((StackLayout)ButtonStackLayout).Children.Add(s1);

		}
		public async void OnAddNewEventClicked(object sender, EventArgs e)
		{
			AddEventPage addevenpage = new AddEventPage { Title = "Add Event" };

			//var nav = new NavigationPage(addevenpage);
			//addevenpage.nav = nav;
			//addevenpage.NavigationUsed = Navigation;
			//nav.BarBackgroundColor = Color.FromHex("#4f504f");
			//nav.BarTextColor = Color.FromHex("#b59272");
			//NavigationPage.SetHasBackButton(nav, false);
			//nav.BarBackgroundColor = Color.FromHex("4f504f");
			//nav.BackgroundColor = Color.FromHex("4f504f");


			await Navigation.PushModalAsync(addevenpage);
			
		}


		private async void OnDeleteTrainingClicked(object sender, EventArgs e)
		{
			string praticeId = "";
			bool isTeamTraining = false;
			for (int i = 0; i < ButtonAndTrainingList.Count; i++)
			{
				
				if (ButtonAndTrainingList[i].TrainingButton == (Button)(((StackLayout)(((Button)sender).Parent)).Children[0]) || ButtonAndTrainingList[i].TrainingButton2 == (Button)(((StackLayout)(((Button)sender).Parent)).Children[0]))
				{
					praticeId = ButtonAndTrainingList[i].TrainingID;
					isTeamTraining = ButtonAndTrainingList[i].isTeamTraining;
				}
			}

			if (isTeamTraining)
			{
				if (Database.CurrentAthlete.isCoach == false)
				{ 
					DisplayAlert("Not possible!", "Only the coach can delete team practices", "I Understand..");
					return;
				}
			}

			await Database.RemoveTrainingWithID(praticeId);
			await Database.GetUserData(Database.FacebookProfile.Id);
			LoadTrainingData();
			await HomePage.LoadPerformanceData();
		}

		public async void OnCalendarOpen(object sender, EventArgs e)
		{ 
			await Navigation.PushAsync(new DiarySummary());
		}
		public async void OnDoQuestionairClicked(object sender, EventArgs e)
		{

			string praticeId = "";

			bool hasBeenEvaluated = false;
			for (int i = 0; i < ButtonAndTrainingList.Count; i++)
			{
				if (ButtonAndTrainingList[i].TrainingButton == (Button)sender|| ButtonAndTrainingList[i].TrainingButton2 ==(Button)sender)
				{
					praticeId = ButtonAndTrainingList[i].TrainingID;
					hasBeenEvaluated = ButtonAndTrainingList[i].HasBeenEvaluated;
				}
			}
			if (hasBeenEvaluated == false)
			{
				await Navigation.PushModalAsync(new Loading());
				QuestionPage qp = new QuestionPage(praticeId);
				await QuestionPage.LoadQuestions(qp);
				await Navigation.PopModalAsync();
				NavigationPage np = new NavigationPage(qp);
				//np.BarBackgroundColor = Color.FromHex("3d3d4a");
				//np.BackgroundColor = Color.FromHex("3d3d4a");
				await Navigation.PushModalAsync(np);
			}
		}
		public async void OnAlreadyEvaluated(object sender, EventArgs e)
		{ 
			bool redoEvaluation = await DisplayAlert("Already evaluated", "Would you like to delete the evaluation and do it again?", "Yes", "No");

			if (redoEvaluation)
			{
				string praticeId = "";

				for (int i = 0; i < ButtonAndTrainingList.Count; i++)
				{

					if (ButtonAndTrainingList[i].TrainingButton == (Button)(((StackLayout)(((Button)sender).Parent)).Children[0]) || ButtonAndTrainingList[i].TrainingButton2 == (Button)(((StackLayout)(((Button)sender).Parent)).Children[0]))
					{
						praticeId = ButtonAndTrainingList[i].TrainingID;
					}
				}

				await Database.RemoveDiaryInputForPratice(praticeId);
				await Database.GetUserData(Database.FacebookProfile.Id);
				LoadTrainingData();
				await HomePage.LoadPerformanceData();


			}
			//await Navigation.PushAsync(new AlreadyEvaluated());
		}

		private async void OnChangeDateClicked(object sender, EventArgs e)
		{
			int dateClicked = int.Parse(((Button)sender).Text);

			int currentMonth = HomePage.SelectedDate.Month;
			//if ( DateTime.Today.Day - dateClicked  < -10)
			//	currentMonth -= 1;
			//if (DateTime.Today.Day - dateClicked  > 10)
			//	currentMonth += 1;

			for (int i = 0; i < currentDateButtons.Count; i++)
			{
				if (currentDateButtons[i].DateButton == (Button)sender)
				{
					currentMonth = currentDateButtons[i].Month;
				}
			}


			DateTime temp = new DateTime(HomePage.SelectedDate.Year,currentMonth,dateClicked); 
			HomePage.SelectedDate = temp;
			await Database.GetUserData(Database.FacebookProfile.Id);
			LoadTrainingData();

			currentDateButtons.Clear();
			((StackLayout)DateButtonStackLayout).Children.Clear();
			//AddDateButton(SelectedDate.Day - 5);
			//AddDateButton(SelectedDate.Day - 4);
			AddDateButton(SelectedDate.Day - 3,false,date1,SelectedDate.AddDays(-3).DayOfWeek.ToString());
			AddDateButton(SelectedDate.Day - 2, false, date2,SelectedDate.AddDays(-2).DayOfWeek.ToString());
			AddDateButton(SelectedDate.Day - 1, false, date3,SelectedDate.AddDays(-1).DayOfWeek.ToString());
			AddDateButton(SelectedDate.Day, true, date4,SelectedDate.DayOfWeek.ToString());
			AddDateButton(SelectedDate.Day + 1, false, date5,SelectedDate.AddDays(1).DayOfWeek.ToString());
			AddDateButton(SelectedDate.Day + 2, false, date6,SelectedDate.AddDays(2).DayOfWeek.ToString());
			AddDateButton(SelectedDate.Day + 3, false, date7,SelectedDate.AddDays(3).DayOfWeek.ToString());
			//AddDateButton(SelectedDate.Day + 4);
			//AddDateButton(SelectedDate.Day + 5);
			//await Navigation.PushAsync(new QuestionPage());

			await HomePage.LoadPerformanceData();
		}
		public void SetPerformanceData(float user, float team)
		{ 
			UserPerformanceSlider.Progress = (user / 10f);

			TeamPerformanceSlider.Progress = (team / 10f);
		}
		public async static Task<bool> LoadPerformanceData()
		{
			//float userValue = await Database.GetPerformanceForUserID(Database.FacebookProfile.Id);
			//float teamValue = await Database.GetPerformanceForTeamID(Database.CurrentAthlete.TeamID.ToString());
			//HomePage.instance.SetPerformanceData(userValue, teamValue);

			string data = await Database.GetPerformanceSummary(Database.FacebookProfile.Id);
			string[] obj = data.Split(';');

			string[] titleData = obj[0].Split(',');
			string[] youData = obj[1].Split(',');
			string[] teamData = obj[2].Split(',');

			HomePage.instance.TitlePerformanceLabel.Text = titleData[0];
			HomePage.instance.YouPerformanceLabel.Text = youData[0];
			HomePage.instance.TeamPerformanceLabel.Text = teamData[0];


			HomePage.instance.SetPerformanceData(float.Parse(youData[2]), float.Parse(teamData[2]));

			//float userValue = await Database.GetPerformanceForUserID(Database.FacebookProfile.Id);
			//float teamValue = await Database.GetPerformanceForTeamID(Database.CurrentAthlete.TeamID.ToString() );
			//HomePage.instance.SetPerformanceData(userValue,teamValue);
			return true;
		}


	}
}

