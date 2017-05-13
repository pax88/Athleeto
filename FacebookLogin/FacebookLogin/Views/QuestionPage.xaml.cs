using System;
using System.Collections.Generic;

using Xamarin.Forms;
using System.Threading.Tasks;
using System.Globalization;

namespace FacebookLogin
{
	public partial class QuestionPage : ContentPage
	{
		static List<string> LabelList = new List<string>();
		static List<string> DescriptionList = new List<string>();
		static List<string> IdList = new List<string>();


		static List<Slider> SliderListConnected = new List<Slider>();
		static List<Button> LabelListConnected = new List<Button>();
		static List<int> LabelListValue = new List<int>();


		static List<Slider> SliderListDurationConnected = new List<Slider>();
		static List<Button> LabelListDurationConnected = new List<Button>();


		static List<string> QuestionIdListConnected = new List<string>();
		static List<Button> DescriptionListConnected = new List<Button>();

		public static List<string> AddedInjurysList = new List<string>();
		public static List<int> AddedInjurysValueList = new List<int>();

		public static string Comment = "";
		public int Duration=0;

		StackLayout StackLayoutForInjuryButotns;

		public static string trainingID="";
		public QuestionPage(string trainingId)
		{
			InitializeComponent();
			QuestionPage.trainingID = trainingId;

			this.BackgroundColor = Color.FromHex("50514f");

			NavigationPage.SetHasNavigationBar(this, false);
			NavigationPage.SetHasBackButton(this, false);
	
		}


		protected override void OnAppearing()
		{
			base.OnAppearing();
			LabelListConnected.Clear();
			LabelListDurationConnected.Clear();
			DescriptionListConnected.Clear();
			SliderListConnected.Clear();
			SliderListDurationConnected.Clear();
			MainStackLayoutQuestions.Children.Clear();
			QuestionIdListConnected.Clear();

			AddTopBar();
			AddPraticeDuration2();
			AddEntry2("Vem", "2", "asd");
			AddEntry2("Vem", "2", "asd");
			//AddEntry2("Vem", "2", "asd");
			//AddEntry2("Vem", "2", "asd");
			for (int i = 0; i < LabelList.Count; i++)
			{
				AddEntry2(LabelList[i],IdList[i],DescriptionList[i]);
			}

			AddHeadline("INJURIES", "Input your injury");
			AddNewInjuryButton();
			UpdateInjuryButtonList();


	
			Button b1 = new Button
			{
				BorderRadius = 0,
				Text = "Back",
				TextColor = Color.White,
				FontAttributes = FontAttributes.Bold,
				FontFamily = "Lato-Light",
				FontSize = 26,
				BackgroundColor = Color.FromHex("#f75a54"),
				WidthRequest= 90
			};
			Button b2 = new Button
			{
				BorderRadius = 0,
				Text = "Done",
				TextColor = Color.White,
				FontAttributes = FontAttributes.Bold,
				FontFamily = "Lato-Light",
				FontSize = 26,
				BackgroundColor = Color.FromHex("#629e61"),
				WidthRequest = 90
			};
			b1.Clicked += BackButtonClicked;
			b2.Clicked += Answer1Clicked;
			Label label1 = new Label()
			{
				Text="Comments:",
				TextColor=Color.FromHex("c7a07c")
					
			};
			Label label2 = new Label()
			{
				Text = ""

			};
			TestLayoutProblem.MyEntryComment entry1 = new TestLayoutProblem.MyEntryComment()
			{
				Placeholder="Input..",
				Text = Comment,
				TextColor = Color.FromHex("c7a07c"),
				PlaceholderColor = Color.FromHex("c7a07c"),
				BackgroundColor = Color.FromHex("2e2e2e"),
				Margin= new Thickness(0,5,0,20)

			};
			entry1.TextChanged += OnTextChangedComment;
			StackLayout sl = new StackLayout
			{
				Orientation = StackOrientation.Vertical,

				Padding = new Thickness(20),
				BackgroundColor = Color.FromHex("2e2e2e")
					
					
			};
			sl.Children.Add(label1);
			sl.Children.Add(entry1);

			sl.Children.Add(b1);
			sl.Children.Add(b2);


			((StackLayout)MainStackLayoutQuestions).Children.Add(sl);


			for (int i = 0; i < Database.LatestInjuryIdList.Count; i++)
			{
				AddInjuryFromId(Database.LatestInjuryIdList[i]);
			}

			UpdateInjuryButtonList();
		}
		public StackLayout SLInjuryButtons = null;
		public StackLayout InjuryStackLayout = null;

		string currentSearchString = "";

	
		public void AddInjuryFromId(string aID)
		{ 
			string injuryName = GetInjuryNameFromId(aID);
			if (injuryName == "")
				return;


			//if (AddedInjurysList.Contains(injuryName) == false)
			//{
			//	AddedInjurysList.Add(injuryName);
			//	AddedInjurysValueList.Add(0);
			//}
			//else
			//{
			//	AddedInjurysList.Remove(injuryName);

			//	AddedInjurysValueList.RemoveAt(AddedInjurysList.IndexOf(injuryName));
			//}



			QuestionPage.AddedInjurysList.Add(injuryName);
			QuestionPage.AddedInjurysValueList.Add(0);



		}
		TimePicker pickerDuration;
		string minutesDuration="0";
		public void AddTopBar()
		{ 
			((StackLayout)MainStackLayoutQuestions).Children.Add(aa1);
			((StackLayout)MainStackLayoutQuestions).Children.Add(aa2);

		}
		public void AddPraticeDuration2()
		{
			int h = 0;
			int m = 0;
			for (int i = 0; i < Database.DatabaseTraining.Count; i++)
			{
				if (QuestionPage.trainingID == Database.DatabaseTraining[i].PraticeID)
				{
					DateTime d1 = DateTime.ParseExact(Database.DatabaseTraining[i].Date, "yyyyMMddHHmmss", CultureInfo.InvariantCulture);
					DateTime d2 = DateTime.ParseExact(Database.DatabaseTraining[i].EndDate, "yyyyMMddHHmmss", CultureInfo.InvariantCulture);

					h = d2.Hour - d1.Hour;
					m = d2.Minute - d1.Minute;
				}
			}
			if (Duration != 0)
			{
				minutesDuration = Duration.ToString();
			}
			else
			{
				minutesDuration = (((float)h * 60f) + (float)m).ToString();
				Duration = int.Parse(minutesDuration);
			}
			string bgColor = "2e2e2e";


			Button button;
			Button button2;
			if (Device.OS == TargetPlatform.iOS)
			{
				string button2color = "2e2e2e";
				string evaluatedText = "";
				button2 = new TestLayoutProblem.MyButton
				{
					
					BorderColor = Color.FromHex("2e2e2e"),
					TextColor = Color.FromHex("b59272"),
					FontFamily = "Lato-Light",
					BackgroundColor = Color.FromHex("2e2e2e"),
					Text = "Duration",
					HeightRequest = 40,
					FontSize = 22,
					HorizontalOptions = LayoutOptions.FillAndExpand,

				};


				button = new TestLayoutProblem.MyButton
				{
			
					BorderWidth = 1,
					BorderRadius = 1,
					BorderColor = Color.FromHex("b08f70"),
					TextColor = Color.FromHex("b08f70"),
					FontFamily = "Lato-Light",
					BackgroundColor = Color.FromHex("2e2e2e"),
					Text = minutesDuration,
					HeightRequest = 40,
					FontSize = 22,
					HorizontalOptions = LayoutOptions.FillAndExpand,
					//Margin = new Thickness(2, 2, 2, 2),
				};
			}
			else
			{
				string button2color = "2e2e2e";
				string evaluatedText = "";
				button2 = new TestLayoutProblem.MyButton
				{
					TextColor = Color.FromHex("b08f70"),
					FontFamily = "Lato-Light",
					BackgroundColor = Color.FromHex(button2color),
					Text = "Duration",
					HeightRequest = 45,
					BorderRadius = 0,

				};

				button = new TestLayoutProblem.MyButton
				{
					WidthRequest = 100,
					HeightRequest = 50,
					TextColor = Color.White,
					FontFamily = "Lato-Light",
					BackgroundColor = Color.FromHex(bgColor),
					FontSize = 13,
					Text = minutesDuration,
					BorderRadius = 2,
					BorderColor = Color.FromHex("b08f70"),
					                   			Margin=10

				};
			}

			//	button.Clicked += OnAlreadyEvaluated;
			//	button2.Clicked += OnAlreadyEvaluated;

			//	allButtons.Add(button);

			Slider slider1 = new Slider
			{
				Minimum = 0,
				Maximum = 10,
				BackgroundColor = Color.FromHex("4f514f"),
				Margin = new Thickness(40,0,40,0)
			};
			slider1.ValueChanged += HandleValueDuration;

			StackLayout s1 = new StackLayout
			{
				BackgroundColor = Color.FromHex("2e2e2e"),
				Orientation = StackOrientation.Horizontal,
				Margin = new Thickness(20,10,20,10),
				Children = {
					button,
					button2
					}
			};

			Button buttonDone = new Button
			{
				Image = "infoButton.png",
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
			buttonDone.Clicked += ClickedMoreInfo;
			s1.Children.Add(buttonDone);

			StackLayout sTotal = new StackLayout
			{
				BackgroundColor =Color.FromHex("4f514f"),
				Children = {
					s1,
					slider1
					}
			};


			((StackLayout)MainStackLayoutQuestions).Children.Add(sTotal);

			Label l3 = new Label
			{
			};
			Label l4 = new Label
			{
			};
			((StackLayout)MainStackLayoutQuestions).Children.Add(l3);
			((StackLayout)MainStackLayoutQuestions).Children.Add(l4);


			//ButtonAndTraining bat = new ButtonAndTraining();
			//bat.TrainingButton = button;
			//bat.TrainingButton2 = button2;
			//bat.TrainingID = aTraining.PraticeID;
			//bat.isTeamTraining = bool.Parse(aTraining.isTeamTraining);
			//bat.HasBeenEvaluated = aTraining.HasBeenEvaluated;
			//ButtonAndTrainingList.Add(bat);
			//LabelListDurationConnected.Add(l2);
			//SliderListDurationConnected.Add(s1);


			LabelListDurationConnected.Add(button);
			SliderListDurationConnected.Add(slider1);
			//DescriptionListConnected.Add(buttonDone);



		}
		public void AddHeadline(string aQuestion, string extraInfo)
		{
			Label l1 = new Label
			{
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.Center,
				TextColor = Color.FromHex("c8a07c"),
				FontAttributes = FontAttributes.None,
				FontSize = 30,
				Text = aQuestion
			};



	
			StackLayout stack = new StackLayout
			{
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.Center
			};
	
			stack.Children.Add(l1);

			((StackLayout)MainStackLayoutQuestions).Children.Add(stack);



		}
		void AddEntry2(string aQuestion, string questionID, string extraInfo)
		{

			string bgColor = "2e2e2e";


			Button button;
			Button button2;
			if (Device.OS == TargetPlatform.iOS)
			{
				string button2color = "2e2e2e";
				string evaluatedText = "";

				button2 = new TestLayoutProblem.MyButton
				{
					//TextColor = Color.Black,
					//FontFamily = "Lato-Light",
					//BackgroundColor = Color.FromHex(button2color),
					//Text = aQuestion,
					//HeightRequest = 45,
					//BorderRadius = 0,
					BorderColor = Color.FromHex("2e2e2e"),
					TextColor = Color.FromHex("b59272"),
					FontFamily = "Lato-Light",
					BackgroundColor = Color.FromHex("2e2e2e"),
					Text = aQuestion,
					HeightRequest = 40,
					FontSize = 22,
					HorizontalOptions = LayoutOptions.FillAndExpand,

				};


				button = new TestLayoutProblem.MyButton
				{
					//BorderRadius = 0,
					//WidthRequest = 100,
					//HeightRequest = 50,
					//TextColor = Color.White,
					//FontFamily = "Lato-Light",
					//BackgroundColor = Color.FromHex(bgColor),
					//FontSize = 13,
					//Text = "0",
					BorderWidth = 1,
					BorderRadius = 1,
					BorderColor = Color.FromHex("b08f70"),
					TextColor = Color.FromHex("b08f70"),
					FontFamily = "Lato-Light",
					BackgroundColor = Color.FromHex("2e2e2e"),
					Text = "0",
					HeightRequest = 40,
					FontSize = 22,
					HorizontalOptions = LayoutOptions.FillAndExpand,
				};
			}
			else
			{
				string button2color = "FFFFFF";
				string evaluatedText = "";
				button2 = new TestLayoutProblem.MyButton
				{
					TextColor = Color.Black,
					FontFamily = "Lato-Light",
					BackgroundColor = Color.FromHex(button2color),
					Text = aQuestion,
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
					Text = "0",

				};
			}

		//	button.Clicked += OnAlreadyEvaluated;
		//	button2.Clicked += OnAlreadyEvaluated;

		//	allButtons.Add(button);

			Slider slider1 = new Slider
			{
				Minimum = 0,
				Maximum = 10,
				BackgroundColor = Color.FromHex("4f514f"),
				Margin = new Thickness(40, 0, 40, 0)
			};
			slider1.ValueChanged += HandleValueChanged;

			StackLayout s1 = new StackLayout
			{
				BackgroundColor = Color.FromHex("2e2e2e"),
				Orientation = StackOrientation.Horizontal,
				Margin = new Thickness(20, 10, 20, 10),
				Children = {
					button,
					button2
					}
			};

			Button buttonDone = new Button
			{
				Image = "infoButton.png",
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
			buttonDone.Clicked += ClickedMoreInfo;
			s1.Children.Add(buttonDone);

			StackLayout sTotal = new StackLayout
			{
				BackgroundColor = Color.FromHex("4f514f"),
				Children = {
					s1,
					slider1
					}
			};


			((StackLayout)MainStackLayoutQuestions).Children.Add(sTotal);

			Label l3 = new Label
			{
			};
			Label l4 = new Label
			{
			};
			((StackLayout)MainStackLayoutQuestions).Children.Add(l3);
			((StackLayout)MainStackLayoutQuestions).Children.Add(l4);


			//ButtonAndTraining bat = new ButtonAndTraining();
			//bat.TrainingButton = button;
			//bat.TrainingButton2 = button2;
			//bat.TrainingID = aTraining.PraticeID;
			//bat.isTeamTraining = bool.Parse(aTraining.isTeamTraining);
			//bat.HasBeenEvaluated = aTraining.HasBeenEvaluated;
			//ButtonAndTrainingList.Add(bat);

			SliderListConnected.Add(slider1);
			LabelListConnected.Add(button);

			if (LabelListValue.Count < LabelListConnected.Count)
			{
				LabelListValue.Add(0);
			}
			else
			{
				button.Text = LabelListValue[ LabelListConnected.Count-1 ].ToString();
				SliderListConnected[LabelListConnected.Count-1].Value = (double)LabelListValue[LabelListConnected.Count-1];
			}


			QuestionIdListConnected.Add(questionID);
			DescriptionListConnected.Add(buttonDone);



		}
		public void UpdateInjuryButtonList()
		{
			StackLayoutForInjuryButotns.Children.Clear();
			for (int i = 0; i < QuestionPage.AddedInjurysList.Count; i++)
			{
				AddInjuryButton(QuestionPage.AddedInjurysList[i],"",QuestionPage.AddedInjurysValueList[i]);
			}
		}
		void AddInjuryButton(string aQuestion, string questionID , int aValue)
		{

			string bgColor = "2e2e2e";


			Button button;
			Button button2;
			if (Device.OS == TargetPlatform.iOS)
			{
				string button2color = "2e2e2e";
				string evaluatedText = "";

				button2 = new TestLayoutProblem.MyButton
				{
					//TextColor = Color.Black,
					//FontFamily = "Lato-Light",
					//BackgroundColor = Color.FromHex(button2color),
					//Text = aQuestion,
					//HeightRequest = 45,
					//BorderRadius = 0,
					BorderColor = Color.FromHex("2e2e2e"),
					TextColor = Color.FromHex("b59272"),
					FontFamily = "Lato-Light",
					BackgroundColor = Color.FromHex("2e2e2e"),
					Text = aQuestion,
					HeightRequest = 40,
					FontSize = 22,
					HorizontalOptions = LayoutOptions.FillAndExpand,

				};


				button = new TestLayoutProblem.MyButton
				{
					//BorderRadius = 0,
					//WidthRequest = 100,
					//HeightRequest = 50,
					//TextColor = Color.White,
					//FontFamily = "Lato-Light",
					//BackgroundColor = Color.FromHex(bgColor),
					//FontSize = 13,
					//Text = "0",
					BorderWidth = 1,
					BorderRadius = 1,
					BorderColor = Color.FromHex("b08f70"),
					TextColor = Color.FromHex("b08f70"),
					FontFamily = "Lato-Light",
					BackgroundColor = Color.FromHex("2e2e2e"),
					Text = aValue.ToString(),
					HeightRequest = 40,
					FontSize = 22,
					HorizontalOptions = LayoutOptions.FillAndExpand,
				};
			}
			else
			{
				string button2color = "FFFFFF";
				string evaluatedText = "";
				button2 = new TestLayoutProblem.MyButton
				{
					TextColor = Color.Black,
					FontFamily = "Lato-Light",
					BackgroundColor = Color.FromHex(button2color),
					Text = aQuestion,
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
					Text = "0",

				};
			}

			button2.Clicked += OnRemoveInjurClicked;
			button.Clicked += OnRemoveInjurClicked;

			Slider slider1 = new Slider
			{
				Minimum = 0,
				Maximum = 10,
				BackgroundColor = Color.FromHex("4f514f"),
				Margin = new Thickness(40, 0, 40, 0)
			};
			slider1.ValueChanged += HandleValueChangedInjurySlider;

			StackLayout s1 = new StackLayout
			{
				BackgroundColor = Color.FromHex("2e2e2e"),
				Orientation = StackOrientation.Horizontal,
				Margin = new Thickness(20, 10, 20, 10),
				Children = {
					button,
					button2
					}
			};



			StackLayout sTotal = new StackLayout
			{
				BackgroundColor = Color.FromHex("4f514f"),
				Children = {
					s1,
					slider1
					}
			};


			((StackLayout)StackLayoutForInjuryButotns).Children.Add(sTotal);

			Label l3 = new Label
			{
			};
			Label l4 = new Label
			{
			};
			((StackLayout)StackLayoutForInjuryButotns).Children.Add(l3);
			((StackLayout)StackLayoutForInjuryButotns).Children.Add(l4);





		}
		public void AddNewInjuryButton()
		{ 
			string bgColor = "2e2e2e";


			Button button;
			Button button2;
			if (Device.OS == TargetPlatform.iOS)
			{
				string button2color = "2e2e2e";
				string evaluatedText = "";

				button2 = new TestLayoutProblem.MyButton
				{
					//TextColor = Color.Black,
					//FontFamily = "Lato-Light",
					//BackgroundColor = Color.FromHex(button2color),
					//Text = aQuestion,
					//HeightRequest = 45,
					//BorderRadius = 0,
					BorderWidth = 1,
					BorderRadius = 1,
					BorderColor = Color.FromHex("b08f70"),
					TextColor = Color.FromHex("b08f70"),
					FontFamily = "Lato-Light",
					BackgroundColor = Color.FromHex("4f514f"),
					Text = "Add Injury",
					HeightRequest = 50,
					FontSize = 22,
					HorizontalOptions = LayoutOptions.FillAndExpand,

				};


				button = new TestLayoutProblem.MyButton
				{
					//BorderRadius = 0,
					//WidthRequest = 100,
					//HeightRequest = 50,
					//TextColor = Color.White,
					//FontFamily = "Lato-Light",
					//BackgroundColor = Color.FromHex(bgColor),
					//FontSize = 13,
					//Text = "0",
					WidthRequest=-35,
					BorderWidth = 0,
					BorderRadius = 0,
					BorderColor = Color.FromHex("b08f70"),
					TextColor = Color.FromHex("FFFFFF"),
					FontFamily = "Lato-Light",
					BackgroundColor = Color.FromHex("b08f70"),
					Text = "+",
					HeightRequest = 50,
					FontSize = 22,
					HorizontalOptions = LayoutOptions.FillAndExpand,
				};
			}
			else
			{
				string button2color = "FFFFFF";
				string evaluatedText = "";
				button2 = new TestLayoutProblem.MyButton
				{
					TextColor = Color.Black,
					FontFamily = "Lato-Light",
					BackgroundColor = Color.FromHex(button2color),
					Text = "Add Injury",
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
					Text = "+",

				};
			}

			button2.Clicked += OnAddInjurClicked;
			button.Clicked += OnAddInjurClicked;

			StackLayout s1 = new StackLayout
			{
				BackgroundColor = Color.FromHex("b08f70"),
				Orientation = StackOrientation.Horizontal,
				HeightRequest = 50,
				Margin = new Thickness(20, 0, 20, 0),
				Children = {
					button,
					button2
					}
			};


			StackLayoutForInjuryButotns = new StackLayout
			{
		
			};


			((StackLayout)MainStackLayoutQuestions).Children.Add(StackLayoutForInjuryButotns);

			((StackLayout)MainStackLayoutQuestions).Children.Add(s1);

			Label l3 = new Label
			{
			};
			Label l4 = new Label
			{
			};
			((StackLayout)MainStackLayoutQuestions).Children.Add(l3);
			((StackLayout)MainStackLayoutQuestions).Children.Add(l4);





	
		}
		static public async Task LoadQuestions(QuestionPage qp)
		{

			string questionData = await Database.GetQuestionsForTeamID(Database.CurrentAthlete.TeamID.ToString());
			LabelList.Clear();
			DescriptionList.Clear();
			IdList.Clear();
			string[] questions = questionData.Split(',');
			int labelListCount = 0;
			for (int i = 2; i < questions.Length-1; i+=3)
			{
				//qp.LabelList[labelListCount].Text = questions[i];
				LabelList.Add(questions[i]);
				DescriptionList.Add(questions[i-1]);
				IdList.Add(questions[i-2]);
				labelListCount++;
			}

	



		}
		private async void ClickedMoreInfo(object sender, EventArgs e)
		{
			for (int i = 0; i < DescriptionListConnected.Count; i++)
			{
				if (DescriptionListConnected[i] == (Button)sender)
				{ 
					DisplayAlert("Help",DescriptionList[i], "I Understand..");
				}
			}
		}
		bool pushedButtonInjury = false;
		private async void OnAddInjurClicked(object sender, EventArgs e)
		{
			if (pushedButtonInjury)
				return;
			pushedButtonInjury = true;

			await Navigation.PushModalAsync(new AddInjuryPage(this));
		}
		private async void OnRemoveInjurClicked(object sender, EventArgs e)
		{
			AddedInjurysValueList.RemoveAt(AddedInjurysList.IndexOf(((Button)sender).Text));
			AddedInjurysList.Remove(((Button)sender).Text);

			((StackLayout)(((Button)sender).Parent.Parent.Parent) ).Children.Remove( (StackLayout)((Button)sender).Parent.Parent );
		}

		private async void BackButtonClicked(object sender, EventArgs e)
		{
			if (pushedButton)
				return;
			pushedButton = true;

			await Navigation.PopModalAsync();

		}
		public bool pushedButton = false;
		private async void Answer1Clicked(object sender, EventArgs e)
		{
			if (pushedButton)
				return;
			pushedButton = true;


			Navigation.PushModalAsync(new Loading());


			string injury = GetInjuryString();
			string injuryIntensity = GetInjuryIntensityString();





			string questionIds = "";
			string questionValues = "";

			for (int i = 0; i < QuestionIdListConnected.Count; i++)
			{
				questionIds += QuestionIdListConnected[i];
				if (i < QuestionIdListConnected.Count - 1)
					questionIds += ",";

				questionValues += LabelListConnected[i].Text;
				if (i < QuestionIdListConnected.Count - 1)
					questionValues += ",";

			}

			string praticeId = QuestionPage.trainingID;
			string teamID = Database.CurrentAthlete.TeamID.ToString();
			if (Database.CheckIfTrainingIsTeam(praticeId) == false)
			{
				teamID = "-1";
			}
			await Database.AddDiaryInput(Database.FacebookProfile.Id,teamID,praticeId,questionIds,questionValues,injury,injuryIntensity,minutesDuration);

			await Database.GetUserData(Database.FacebookProfile.Id);
			HomePage.instance.LoadTrainingData();


			await HomePage.LoadPerformanceData();


			//await Navigation.PopToRootAsync();

			await Navigation.PopModalAsync();
			await Navigation.PopModalAsync();
		}

		void HandleValueChanged(object sender, EventArgs e)
		{   // display the value in a label
			for (int i = 0; i < SliderListConnected.Count; i++)
			{
				if ((Slider)sender == SliderListConnected[i])
				{ 
					LabelListConnected[i].Text = Math.Round(SliderListConnected[i].Value).ToString();
					LabelListValue[i] = (int)Math.Round(SliderListConnected[i].Value);
				}
			}

			//SliderValue.Text = SliderInput.Value.ToString();
			//SliderValue.Text = Math.Round(SliderInput.Value).ToString();
		}
		void HandleValueDuration(object sender, EventArgs e)
		{   // display the value in a label
			for (int i = 0; i < SliderListDurationConnected.Count; i++)
			{
				if ((Slider)sender == SliderListDurationConnected[i])
				{


					int h = 0;
					int m = 0;
					for (int j = 0; j < Database.DatabaseTraining.Count; j++)
					{
						if (QuestionPage.trainingID == Database.DatabaseTraining[j].PraticeID)
						{
							DateTime d1 = DateTime.ParseExact(Database.DatabaseTraining[j].Date, "yyyyMMddHHmmss", CultureInfo.InvariantCulture);
							DateTime d2 = DateTime.ParseExact(Database.DatabaseTraining[j].EndDate, "yyyyMMddHHmmss", CultureInfo.InvariantCulture);

							h = d2.Hour - d1.Hour;
							m = d2.Minute - d1.Minute;
						}
					}

					float totalMinutes = (float)m + (float)h * 60f;

					LabelListDurationConnected[i].Text = (Math.Round((SliderListDurationConnected[i].Value/10f) * totalMinutes)).ToString();
					minutesDuration = LabelListDurationConnected[i].Text;

					Duration = int.Parse(minutesDuration);
				}
			}

			//SliderValue.Text = SliderInput.Value.ToString();
			//SliderValue.Text = Math.Round(SliderInput.Value).ToString();
		}
		void HandleValueChangedInjurySlider(object sender, EventArgs e)
		{

			for (int i = 0; i < StackLayoutForInjuryButotns.Children.Count; i++)
			{
				if (((Slider)sender).Parent == StackLayoutForInjuryButotns.Children[i])
				{
					StackLayout s = (StackLayout)StackLayoutForInjuryButotns.Children[i];
					Button l = (Button)((StackLayout)s.Children[0]).Children[0];
					l.Text = Math.Round(((Slider)sender).Value).ToString();

					Button injuryName = (Button)((StackLayout)s.Children[0]).Children[1];
					AddedInjurysValueList[AddedInjurysList.IndexOf(injuryName.Text)] = (int)Math.Round( ((Slider)sender).Value);
					 
				}
			}
		}
		void OnTextChangedComment(object sender, EventArgs e)
		{
			Comment = ((TestLayoutProblem.MyEntryComment)sender).Text;
		}
		string GetInjuryString()
		{
			string allInjurys="";
			for (int i = 0; i < AddedInjurysList.Count; i++)
			{
				allInjurys += Database.InjuryDictonary[AddedInjurysList[i]] + ",";
			}
			if (allInjurys.Length > 1)
				allInjurys = allInjurys.TrimEnd(',');

			return allInjurys;
		}
		string GetInjuryIntensityString()
		{
			string allInjurysIntensitys = "";
			for (int i = 0; i < AddedInjurysValueList.Count; i++)
			{
				allInjurysIntensitys += AddedInjurysValueList[i].ToString() + ",";
			}
			if (allInjurysIntensitys.Length > 1)
				allInjurysIntensitys = allInjurysIntensitys.TrimEnd(',');
			return allInjurysIntensitys;
		}
		string GetInjuryNameFromId(string aID)
		{
			if (Database.InjuryIDDictonary.ContainsKey(aID) == false)
				return "";
			return Database.InjuryIDDictonary[aID];
		}
	}

}
