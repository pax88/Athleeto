using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Xamarin.Forms;

using Xamarin.Forms;

namespace FacebookLogin
{
	public partial class DiarySummary : ContentPage
	{
		public List<ButtonAndTraining> ButtonAndTrainingList = new List<ButtonAndTraining>();
		public List<Button> allButtons = new List<Button>();
		public static DiarySummary instance;
		public DiarySummary()
		{
			InitializeComponent();
			Icon = "calendar.png";
			BackgroundColor = Color.White;

			//LoadDiaryEntrys();
			instance = this;

			LoadTrainingData();



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
			List<Training> todaysTraining = Database.GetAllTrainingsUser();

			//for (int i = 0; i < 10; i++)
			//{
			//	Training a = new Training();
			//	a.Date = "20170503040404";
			//	a.EndDate = "20170503040504";
			//	a.Name = "Training" + i.ToString();
			//	a.PraticeID = "";
			//	if(i%2 == 0)
			//		a.isTeamTraining = "true";
			//	else
			//		a.isTeamTraining = "false";

			//	if (i == 4 || i == 6 || i == 8)
			//		a.HasBeenEvaluated = true;
			//	todaysTraining.Add(a);
			//}

			ButtonAndTrainingList.Clear();





			Label txt2 = new Label
			{
				TextColor = Color.FromHex("c8a17d"),
				Text = "Diary Inputs",
				HorizontalOptions = LayoutOptions.Center,
				BackgroundColor = Color.FromHex("50514f"),
				FontSize = 20
			};

			StackLayout s1 = new StackLayout
			{
				BackgroundColor = Color.FromHex("50514f"),
				Spacing = 0,
				Padding = 5,
				Children = {
					txt2,
					}
			};
			((StackLayout)ButtonStackLayout).Children.Add(s1);


			if (todaysTraining != null)
			{
				for (int i = 0; i < todaysTraining.Count; i++)
					AddButton(todaysTraining[i]);
			}



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
		public async void OnDoQuestionairClickedDiarySummary(object sender, EventArgs e)
		{

			string praticeId = "";

			bool hasBeenEvaluated = false;
			for (int i = 0; i < ButtonAndTrainingList.Count; i++)
			{
				if (ButtonAndTrainingList[i].TrainingButton == (Button)sender || ButtonAndTrainingList[i].TrainingButton2 == (Button)sender)
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
				Navigation.PopModalAsync();
				//NavigationPage np = new NavigationPage(qp);
				//np.BarBackgroundColor = Color.FromHex("3d3d4a");
				//np.BackgroundColor = Color.FromHex("3d3d4a");

				await Navigation.PushModalAsync(qp);
			}
		}
		void AddButton(Training aTraining)
		{
			DateTime timeForTraining = DateTime.ParseExact(aTraining.Date, "yyyyMMddHHmmss", CultureInfo.InvariantCulture);
			DateTime timeForTrainingEnd = DateTime.ParseExact(aTraining.EndDate, "yyyyMMddHHmmss", CultureInfo.InvariantCulture);
			string bgColor = "549fb3";
			if (aTraining.isTeamTraining != null)
				if (bool.Parse(aTraining.isTeamTraining) == true)
				{
					bgColor = "619e60";
				}

			string h = timeForTraining.Hour.ToString();
			string m = timeForTraining.Minute.ToString();
			string h2 = timeForTrainingEnd.Hour.ToString();
			string m2 = timeForTrainingEnd.Minute.ToString();

			if (timeForTraining.Hour < 10)
				h = "0" + timeForTraining.Hour.ToString();
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
				string button2color = "2e2e2e";
				string evaluatedText = "";
				button2 = new TestLayoutProblem.MyButton
				{
					TextColor = Color.FromHex("c7a07c"),
					FontFamily = "Lato-Light",
					BackgroundColor = Color.FromHex(button2color),
					Text = timeForTraining.Month.ToString() + "/" + timeForTraining.Day.ToString() + " " + aTraining.Name + evaluatedText,
					HeightRequest = 45,
					BorderRadius = 0,

				};


				button = new TestLayoutProblem.MyButton
				{
					BorderRadius = 0,
					WidthRequest = 100,
					HeightRequest = 50,
					TextColor = Color.White,
					FontFamily = "Lato-Light",
					BackgroundColor = Color.FromHex(bgColor),
					FontSize = 13,
					Text = h + ":" + m + " - " + h2 + ":" + m2,
				};
			}
			else
			{
				string button2color = "2e2e2e";
				string evaluatedText = "";
				button2 = new TestLayoutProblem.MyButton2
				{
					TextColor = Color.FromHex("c7a07c"),
					FontFamily = "Lato-Light",
					BackgroundColor = Color.FromHex(button2color),
					Text = timeForTraining.Month.ToString()+"/"+timeForTraining.Day.ToString()+ " "+aTraining.Name + evaluatedText,
					HeightRequest = 45,
					BorderRadius = 0,

				};

				button = new TestLayoutProblem.MyButton2
				{
					BorderRadius = 0,
					WidthRequest = 100,
					HeightRequest = 50,
					TextColor = Color.White,
					FontFamily = "Lato-Light",
					BackgroundColor = Color.FromHex(bgColor),
					FontSize = 13,
					Text = h + ":" + m + " - " + h2 + ":" + m2,

				};
			}
			if (aTraining.HasBeenEvaluated == false)
			{
				button.Clicked += OnDoQuestionairClickedDiarySummary;
				button2.Clicked += OnDoQuestionairClickedDiarySummary;
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
				BackgroundColor = Color.FromHex("2e2e2e"),
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
					Image = "done.png",
					FontFamily = "Lato-Light",

					Text = "",
					WidthRequest = 32,
					HeightRequest = 32,
					MinimumHeightRequest = 32,
					MinimumWidthRequest = 32,
					BorderRadius = 0,
					Margin = 10,
					BackgroundColor = Color.FromHex("2e2e2e"),
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
	}
}
