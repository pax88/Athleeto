using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Net;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace FacebookLogin
{


	public static class Database
	{



		public static List<User> DatabaseAthlete = new List<User>();
		public static List<Team> DatabaseTeam = new List<Team>();
		public static List<Training> DatabaseTraining = new List<Training>();

		public static List<string> InjuryLabelList = new List<string>();
		public static List<string> InjuryIdList = new List<string>();
		public static List<string> LatestInjuryIdList = new List<string>();

		public static Dictionary<string, string> InjuryDictonary = new Dictionary<string, string>();
		public static Dictionary<string, string> InjuryIDDictonary = new Dictionary<string, string>();


		public static User CurrentAthlete = null;
		public static Models.FacebookProfile FacebookProfile;
		public static System.Net.Http.HttpClient client = null;

		public static string APP_VERSION = "1.6";

		public static void LoadDatabase()
		{

			DatabaseTeam = new List<Team>();
			DatabaseAthlete = new List<User>();
			DatabaseTraining = new List<Training>();


			//DateTime startTime = new DateTime(2016, 10, 17, 20, 30, 0);
			//DateTime endTime = startTime;
			//endTime.AddHours(2);

			//Training s = new Training();
			//s.TeamID = null;
			//s.Name = "Styrketræning";
			//s.Date = startTime.ToString();
			//s.EndDate = endTime.ToString();
			//DatabaseTraining.Add(s);

			//int newWeek = 0;
			//for (int i = 0; i < 20; i++)
			//{
			//	newWeek++;
			//	if (newWeek > 3)
			//	{
			//		startTime=startTime.AddDays(1);
			//		endTime = startTime;
			//		endTime.AddHours(2);
			//		newWeek = 0;
			//	}
			//	Training t = new Training();
			//	t.TeamID = "1";
			//	t.Name = "Boldtræning";
			//	t.Date = startTime.ToString();
			//	t.EndDate = endTime.ToString();
			//	DatabaseTraining.Add(t);
			//	startTime=startTime.AddDays(2);

			//}
		}
		public static bool CheckIfTrainingIsTeam(string aPraticeId)
		{ 
			for (int i = 0; i < DatabaseTraining.Count; i++)
			{
				if (DatabaseTraining[i].PraticeID == aPraticeId)
				{
					if (DatabaseTraining[i].isTeamTraining == "True")
						return true;
					else
					{
						return false;
					}
				}
			}
			return false;
		}
		public async static Task<bool> LoadTeamData()
		{ 
			string teamData = await Backend_GetAllTeams();
			//"Gentofte,3,Lyngby,4,"
			DatabaseTeam.Clear();
			string[] splitData = teamData.Split(',');

			for (int i = 0; i < splitData.Length-1; i += 2)
			{
				Team t1 = new Team();
				t1.TeamID = int.Parse(splitData[i + 1]);
				t1.TeamName = splitData[i];
				DatabaseTeam.Add(t1);

			}
			return true;
		}
		public async static Task<bool> CheckIfUserExist(string PlayerID)
		{
			return await Backend_CheckIfUserExist(PlayerID);

			//for (int i = 0; i < Database.DatabaseAthlete.Count; i++)
			//{
			//	if (Database.DatabaseAthlete[i].PlayerID == PlayerID)
			//	{
			//		return true;
			//	}
			//}
			//return false;
		}
		public async static Task<bool> CheckIfDiaryInputCompletedForPraticeAndUserID(string PlayerID,string aTrainingID)
		{
			return await Backend_CheckIfDiaryInputCompletedForPraticeAndUserID(PlayerID,aTrainingID);
		}

		public async static Task<string> GetQuestionsForTeamID(string TeamID)
		{ 
			string userData = await Backend_GetQuestionsForTeamID(TeamID);
			return userData;
		}
		public async static Task<string> GetInjurys()
		{
			string injuryIDs = await Backend_GetInjurys();
			return injuryIDs;
		}
		public async static Task<string> GetLatestInjurys(string aUserId)
		{
			string injuryLatestIDs = await Backend_GetLatestInjurys(aUserId);
			return injuryLatestIDs;
		}


		public async static Task<float> GetPerformanceForTeamID(string TeamID)
		{
			try
			{
				string returnValue = await Backend_GetPerformanceForTeamID(TeamID);
				if (returnValue == null)
					return 0;
				if (returnValue.Length <= 0)
					return 0;
				if (returnValue == "")
					return 0;

				if (returnValue.Length > 2)
				{
					char a = returnValue[0];
					char b = returnValue[2];
					float first = (float)int.Parse(a + "");
					float second = (float)int.Parse(b + "");
					float tot = first + second;
					return tot;
				}

				returnValue = returnValue.Replace(",", ".");
				return float.Parse(returnValue);
			}
			catch
			{
				return 0;
			}
		}

		public async static Task<string> GetCoachForTeamID(string teamID)
		{ 
			string returnValue = await Backend_GetCoachForTeamID(teamID);
			return returnValue;
		}
		public async static Task<string> GetDiaryInputsForUser(string aUserID)
		{
			string returnValue = await Backend_GetDiaryInputsForUser(aUserID);

			return returnValue;
		}



		public async static Task<string> GetPerformanceSummary(string userID)
		{
			//"Training minutes this week;You,0,0;Team,0,0"
			string returnValue = await Backend_GetPerformanceSummary(userID);

			return returnValue;
		}
		public async static Task<float> GetPerformanceForUserID(string userID)
		{
			try
			{
				string returnValue = await Backend_GetPerformanceForUserID(userID);
				if (returnValue == null)
					return 0;
				if (returnValue.Length <= 0)
					return 0;
				if (returnValue == "")
					return 0;


				if (returnValue.Length > 2)
				{
					char a = returnValue[0];
					char b = returnValue[2];
					float first = (float)int.Parse(a+"");
					float second = (float)int.Parse(b+"");
					float tot = first + second;
					return tot;
				}

				returnValue = returnValue.Replace(",", ".");
				return float.Parse(returnValue);
			}
			catch
			{
				return 0;
			}
		}

		public async static Task<bool> GetUserData(string PlayerID)
		{
			string userData = await Backend_GetUserData(PlayerID);

			string[] splitData = userData.Split(',');
			//"ID:"
			//"14"
			//"faceID:"
			//"2147483647"
			//"age:"
			//"25"
			//"name:"
			//"Patrik"
			//"Bertilsson"
			//"teamID:"
			//"12"
			//"false"

			//"ID: 14 faceID: 2147483647 age: 25 name: Patrik Bertilsson teamID: 12 false"



			//"123123124,22,Patrik Bertilsson,5,true"

			//string[] splitData = userData.Split(',');

			//"boll,12/29/2016 1,12/29/2016 1,6,gym,12/30/2016 1,12/30/2016 1,7,bad,12/31/2016 1,12/31/2016 1,8,"

			Database.CurrentAthlete = new User();
			Database.CurrentAthlete.Age = int.Parse(splitData[2]);
			Database.CurrentAthlete.Name = splitData[3];
			Database.CurrentAthlete.PlayerID = splitData[0];
			Database.CurrentAthlete.TeamID = int.Parse(splitData[4]);
			Database.CurrentAthlete.Trainings = new List<Training>();

			bool isAdmin = bool.Parse(splitData[5]);
			string teamCoach = await GetCoachForTeamID(Database.CurrentAthlete.TeamID.ToString());
			if (teamCoach == PlayerID)
			{
				Database.CurrentAthlete.isCoach = true;
			}
			else
			{ 
				Database.CurrentAthlete.isCoach = false;
			}
			DatabaseTraining.Clear();
			string praticeData = await Backend_GetTrainingsForTeam(Database.CurrentAthlete.TeamID.ToString());
			string[] praticeSplitData = praticeData.Split(',');
			for (int i = 0; i < praticeSplitData.Length-1; i+=6)
			{

				string name = praticeSplitData[i];
				string date = praticeSplitData[i+1];
				string dateTo = praticeSplitData[i+2];
				string praticeId = praticeSplitData[i+3];
				string creatorIDTraining = praticeSplitData[i + 4];
				string isTeamEvent = praticeSplitData[i + 5];
				Training s = new Training();
				s.TeamID = Database.CurrentAthlete.TeamID.ToString();
				s.PraticeID = praticeId;
				s.Name = name;
				s.Date = date;
				s.EndDate = dateTo;
				s.CreatorID = creatorIDTraining;
				s.isTeamTraining = isTeamEvent;
				bool hasBeenEvaluated = await Backend_CheckIfDiaryInputCompletedForPraticeAndUserID(Database.FacebookProfile.Id,praticeId);
				s.HasBeenEvaluated = hasBeenEvaluated;

				DatabaseTraining.Add(s);


			}
			//string coachForCurrentTeam = await GetCoachForTeamID(Database.CurrentAthlete.TeamID.ToString());




			string injurysData = await Database.GetInjurys();
			InjuryIdList.Clear();
			InjuryIDDictonary.Clear();
			InjuryLabelList.Clear();
			InjuryDictonary.Clear();
			LatestInjuryIdList.Clear();
			string[] injury = injurysData.Split(',');
			int injuryListCount = 0;
			for (int i = 1; i < injury.Length - 1; i += 2)
			{
				//qp.LabelList[labelListCount].Text = questions[i];
				InjuryIdList.Add(injury[i-1]);
				InjuryLabelList.Add(injury[i]);
				InjuryDictonary.Add(injury[i],injury[i - 1]);
				InjuryIDDictonary.Add(injury[i-1], injury[i]);
				injuryListCount++;
			}

			string LatestInjurysData = await Database.GetLatestInjurys(Database.FacebookProfile.Id);

			string[] latestinjury = LatestInjurysData.Split(',');
			for (int i = 0; i < latestinjury.Length - 1; i++)
			{
				LatestInjuryIdList.Add(latestinjury[i]);
			}




			//await HomePage.LoadPerformanceData();
			return true;
			//for (int i = 0; i < Database.DatabaseAthlete.Count; i++)
			//{
			//	if (Database.DatabaseAthlete[i].PlayerID == PlayerID)
			//	{
			//		Database.CurrentAthlete = Database.DatabaseAthlete[i];
			//	}
			//}
		}

		public async static Task<bool> AddUser(string PlayerID, string Name,int Age,int TeamID,bool aIsCoach)
		{


			//Athlete a1 = new Athlete();
			//a1.PlayerID = PlayerID;
			//a1.Name = Name;
			//a1.Age = Age;
			//a1.TeamID = TeamID;
			//a1.isCoach = aIsCoach;
			//DatabaseAthlete.Add(a1);
			//CurrentAthlete = Database.DatabaseAthlete[DatabaseAthlete.Count - 1];

			//CurrentAthlete.Trainings = DatabaseTraining;

			await Backend_AddUser(PlayerID, Name, Age, TeamID, aIsCoach);
			return await GetUserData(PlayerID);

		}
		public async static Task<bool> RemoveTrainingWithID(string TrainingID)
		{

			await Backend_RemoveTrainingWithID(TrainingID);
			return true;

		}
		public async static Task<bool> RemoveDiaryInputForPratice(string TrainingID)
		{

			await Backend_RemoveDiaryInputForPratice(TrainingID);
			return true;

		}

		public async static Task<bool> AddDiaryInput(string userID, string teamID, string practiceID, string Questions, string QuestionIntesity, string Injury, string InjuryIntensity,string TrainingDuration,string comment)
		{

			return await Backend_AddDiaryInput(userID, teamID, practiceID, Questions, QuestionIntesity,Injury,InjuryIntensity,TrainingDuration,APP_VERSION,comment);
		}

		public async static Task<bool> AddTeam(string aTeamName,string aCoachId,string aQuestions)
		{
			//Team newTeam = new Team();
			//newTeam.TeamID = aTeamID;
			//newTeam.TeamName = aTeamName;
			//Database.DatabaseTeam.Add(newTeam);
			//return Database.DatabaseTeam[Database.DatabaseTeam.Count - 1];
			return await Backend_AddTeam(aTeamName,aCoachId,aQuestions);
		}
		public static List<Team> GetLocalDataTeams()
		{
			return Database.DatabaseTeam;
		}

		public async static Task<bool> AddTraining(string creatorID, string teamID,string aEventName, string aDate, string aEndDate, string aTime ,bool isTeamEvent)
		{
			aEventName = aEventName.Replace(',', ' ');

			await Backend_AddTraining(creatorID,teamID,aEventName, aDate, aEndDate,aTime,isTeamEvent);


			DatabaseTraining.Clear();
			string praticeData = await Backend_GetTrainingsForTeam(Database.CurrentAthlete.TeamID.ToString());
			string[] praticeSplitData = praticeData.Split(',');
			for (int i = 0; i < praticeSplitData.Length - 1; i += 6)
			{
				string name = praticeSplitData[i];
				string date = praticeSplitData[i + 1];
				string dateTo = praticeSplitData[i + 2];
				string praticeId = praticeSplitData[i + 3];
				string creatorIDTraining = praticeSplitData[i + 4];
				string isTeamEventBool = praticeSplitData[i + 5];

				Training s = new Training();
				s.TeamID = Database.CurrentAthlete.TeamID.ToString();
				s.Name = name;
				s.Date = date;
				s.PraticeID = praticeId;
				s.CreatorID = creatorIDTraining;
				s.EndDate = dateTo;
				s.isTeamTraining = isTeamEventBool;
				bool hasBeenEvaluated = await Backend_CheckIfDiaryInputCompletedForPraticeAndUserID(Database.FacebookProfile.Id, praticeId);
				s.HasBeenEvaluated = hasBeenEvaluated;
				DatabaseTraining.Add(s);
			}


			HomePage.instance.LoadTrainingData();
			//DiarySummary.instance.LoadTrainingData();
			//Training t = new Training();
			//t.Name = aEventName;
			//t.Date = aDate;
			//t.EndDate = aEndDate;
			//if (CurrentAthlete != null)
			//{
			//	t.CreatorID = CurrentAthlete.PlayerID;
			//	if (isTeamEvent)
			//		t.TeamID = CurrentAthlete.TeamID.ToString();
			//}
			return true;
			//DatabaseTraining.Add(t);
		}
		public static String GetTeamNameFromId(int aID)
		{
			for (int i = 0; i < Database.DatabaseTeam.Count; i++)
			{
				if (Database.DatabaseTeam[i].TeamID == aID)
				{
					return Database.DatabaseTeam[i].TeamName;
				}
			}
			return "No team found!";
		}
		public static List<Training> GetTrainingsForDate(DateTime aDate)
		{
			List<Training> trainings = new List<Training>();
			for (int i = 0; i < DatabaseTraining.Count; i++)
			{
				
				if (DateTime.ParseExact( DatabaseTraining[i].Date,"yyyyMMddHHmmss",CultureInfo.InvariantCulture ).Month == aDate.Month)
				if (DateTime.ParseExact(DatabaseTraining[i].Date , "yyyyMMddHHmmss",CultureInfo.InvariantCulture).Day == aDate.Day)
					{
					if (bool.Parse(DatabaseTraining[i].isTeamTraining) == true || DatabaseTraining[i].CreatorID == FacebookProfile.Id)
							trainings.Add( DatabaseTraining[i] );
					}
			}
			return trainings;
		}
		public static List<Training> GetAllTrainingsUser()
		{
			return DatabaseTraining;
		}

		public static void SetFacebookProfile(Models.FacebookProfile aFbProfile)
		{
			Database.FacebookProfile = aFbProfile;
		}


		static public async Task<bool> Backend_CheckIfUserExist(string PlayerID)
		{
			if(client == null)
			client = new System.Net.Http.HttpClient();
			client.BaseAddress = new Uri("http://athleeto.com/");
			var response = await client.GetAsync("checkIfUserExist.php?faceID=" + PlayerID);
			var placesJson = response.Content.ReadAsStringAsync().Result;

			if (placesJson.Contains("true"))
				return true;
			else
				return false;
		}

		static public async Task<bool> Backend_CheckIfDiaryInputCompletedForPraticeAndUserID(string PlayerID,string TrainingID)
		{
			if (TrainingID == null)
				TrainingID = "";
			if (PlayerID == null)
				PlayerID = "";

			if (client == null)
				client = new System.Net.Http.HttpClient();
			client.BaseAddress = new Uri("http://athleeto.com/");
			var response = await client.GetAsync("checkIfDiaryInputCompletedForPraticeAndUserID.php?faceID=" + PlayerID+"&praticeID="+TrainingID);
			var placesJson = response.Content.ReadAsStringAsync().Result;

			if (placesJson == null)
				return false;

			if (placesJson.Contains("true"))
				return true;
			else
				return false;
		}



		static public async Task<bool> Backend_AddUser(string PlayerID, string Name, int Age, int TeamID, bool aIsCoach)
		{
			if (client == null)
				client = new System.Net.Http.HttpClient();
			client.BaseAddress = new Uri("http://athleeto.com/");
			string call = "addUser.php?faceID=" + PlayerID +"&age="+Age.ToString()+"&nameFB="+Name+"&teamID="+TeamID.ToString()+"&isCouch="+aIsCoach.ToString();
			var response = await client.GetAsync(call);
			var placesJson = response.Content.ReadAsStringAsync().Result;
			return true;
		}
		static public async Task<bool> Backend_AddTeam(string aTeamName,string coachID,string aQuestions)
		{

			if (client == null)
				client = new System.Net.Http.HttpClient();
			client.BaseAddress = new Uri("http://athleeto.com/");
			string call = "addTeam.php?teamName=" + aTeamName+"&coachID="+coachID+"&Questions="+aQuestions;
			var response = await client.GetAsync(call);
			var placesJson = response.Content.ReadAsStringAsync().Result;
			return true;

		}
		static public async Task<bool> Backend_AddTraining(string creatorID,string teamID,string aEventName, string aDate, string aEndDate,string aTime,bool isTeamEvent)
		{

			if (client == null)
				client = new System.Net.Http.HttpClient();
			client.BaseAddress = new Uri("http://athleeto.com/");
			string call = "addPractice.php?creatorID=" + creatorID + "&teamID=" + teamID + "&practiceType=" + aEventName+ "&date=" + aDate + "&endDate=" + aEndDate + "&time="+aTime +"&isTeamEvent=" + isTeamEvent ;
			var response = await client.GetAsync(call);
			var placesJson = response.Content.ReadAsStringAsync().Result;
			return true;

		}
		static public async Task<bool> Backend_AddDiaryInput(string userID, string teamID, string practiceID, string Questions , string QuestionIntesity , string Injury, string InjuryIntensity,string TrainingDuration,string app_version,string comment)
		{
			if (client == null)
				client = new System.Net.Http.HttpClient();		
			client.BaseAddress = new Uri("http://athleeto.com/");
			string call = "addDiaryInput.php?userID=" + userID + "&teamID=" + teamID.ToString() + "&practiceID=" + practiceID + "&Questions=" + Questions.ToString() + "&QuestionIntensity=" + QuestionIntesity.ToString()+"&injuries="+Injury+" &injuryIntensity="+InjuryIntensity+" &TrainingDuration="+TrainingDuration+"&AppVersion="+app_version+"&comment="+comment;
			var response = await client.GetAsync(call);
			var placesJson = response.Content.ReadAsStringAsync().Result;
			return true;
		}
		static public async Task<string> Backend_GetUserData(string PlayerID)
		{

			if (client == null)
				client = new System.Net.Http.HttpClient();		
			client.BaseAddress = new Uri("http://athleeto.com/");
			var response = await client.GetAsync("phpGetUserData.php?faceID=" + PlayerID);
			var placesJson = response.Content.ReadAsStringAsync().Result;
			return placesJson;
		}

		static public async Task<string> Backend_GetAllTeams()
		{

			if (client == null)
				client = new System.Net.Http.HttpClient();		
			client.BaseAddress = new Uri("http://athleeto.com/");
			var response = await client.GetAsync("getAllTeams.php");
			var placesJson = response.Content.ReadAsStringAsync().Result;
			return placesJson;
		}
		static public async Task<string> Backend_GetTrainingsForTeam(string aTeamId)
		{

			if (client == null)
				client = new System.Net.Http.HttpClient();	
			client.BaseAddress = new Uri("http://athleeto.com/");
			var response = await client.GetAsync("getPracticesForTeam.php?teamID=" + aTeamId);
			var placesJson = response.Content.ReadAsStringAsync().Result;
			return placesJson;
		}
		static public async Task<string> Backend_GetDiaryInputsForUser(string aUserId)
		{

			if (client == null)
				client = new System.Net.Http.HttpClient();
			client.BaseAddress = new Uri("http://athleeto.com/");
			var response = await client.GetAsync("getDiaryInputsForUserID.php?userID=" + aUserId);
			var placesJson = response.Content.ReadAsStringAsync().Result;
			return placesJson;
		}
		static public async Task<string> Backend_GetQuestionsForTeamID(string TeamID)
		{

			if (client == null)
				client = new System.Net.Http.HttpClient();		
			client.BaseAddress = new Uri("http://athleeto.com/");
			var response = await client.GetAsync("questionsForTeamID.php?teamID=" + TeamID);
			var placesJson = response.Content.ReadAsStringAsync().Result;
			return placesJson;
		}
		static public async Task<string> Backend_GetInjurys()
		{
			if (client == null)
				client = new System.Net.Http.HttpClient();
			client.BaseAddress = new Uri("http://athleeto.com/");
			var response = await client.GetAsync("GetInjurys.php");
			var placesJson = response.Content.ReadAsStringAsync().Result;
			return placesJson;
		}
		static public async Task<string> Backend_GetLatestInjurys(string userID)
		{
			if (client == null)
				client = new System.Net.Http.HttpClient();
			client.BaseAddress = new Uri("http://athleeto.com/");
			var response = await client.GetAsync("getInjuryUserID.php?userID="+userID);
			var placesJson = response.Content.ReadAsStringAsync().Result;
			return placesJson;
		}

		static public async Task<string> Backend_GetPerformanceForTeamID(string TeamID)
		{
			if (client == null)
				client = new System.Net.Http.HttpClient();		
			client.BaseAddress = new Uri("http://athleeto.com/");
			var response = await client.GetAsync("getPerformanceForTeam.php?teamID=" + TeamID);
			var placesJson = response.Content.ReadAsStringAsync().Result;
			return placesJson;
		}
		static public async Task<string> Backend_GetPerformanceForUserID(string userID)
		{
			if (userID == null)
				userID = "";
			if (client == null)
				client = new System.Net.Http.HttpClient();
			System.Diagnostics.Debug.WriteLine(userID);
			client.BaseAddress = new Uri("http://athleeto.com/");
			var response = await client.GetAsync("getPerformanceForUser.php?faceID=" + userID);
			var placesJson = response.Content.ReadAsStringAsync().Result;
			return placesJson;
		}
		static public async Task<string> Backend_GetPerformanceSummary(string userID)
		{
			if (userID == null)
				userID = "";
			if (client == null)
				client = new System.Net.Http.HttpClient();
			System.Diagnostics.Debug.WriteLine(userID);
			client.BaseAddress = new Uri("http://athleeto.com/");
			var response = await client.GetAsync("getHomeStats.php?faceID=" + userID);
			var placesJson = response.Content.ReadAsStringAsync().Result;
			return placesJson;
		}
		static public async Task<bool> Backend_RemoveTrainingWithID(string aTrainingID)
		{

			if (client == null)
				client = new System.Net.Http.HttpClient();		
			client.BaseAddress = new Uri("http://athleeto.com/");
			var response = await client.GetAsync("removePractice.php?praticeId=" + aTrainingID);
			var placesJson = response.Content.ReadAsStringAsync().Result;
			return true;
		}
		static public async Task<bool> Backend_RemoveDiaryInputForPratice(string aTrainingID)
		{

			if (client == null)
				client = new System.Net.Http.HttpClient();
			client.BaseAddress = new Uri("http://athleeto.com/");
			var response = await client.GetAsync("removeDiaryInput.php?praticeId=" + aTrainingID);
			var placesJson = response.Content.ReadAsStringAsync().Result;
			return true;
		}
		static public async Task<string> Backend_GetCoachForTeamID(string teamID)
		{
			if (teamID == null)
				teamID = "";
			if (client == null)
				client = new System.Net.Http.HttpClient();
			client.BaseAddress = new Uri("http://athleeto.com/");
			var response = await client.GetAsync("getCoachForTeamID.php?teamID=" + teamID);
			var placesJson = response.Content.ReadAsStringAsync().Result;
			return placesJson;
		}
	}

	public class User
	{
		public string PlayerID;
		public string Name;
		public int Age;
		public int TeamID;
		public List<Training> Trainings;
		public bool isCoach;
	}
	public class Team
	{
		public int TeamID;
		public string TeamName;
	}
	public class Training
	{
		public string Name;
		public string TeamID;
		public string PraticeID;
		public string CreatorID;
		public string Date;
		public string EndDate;
		public string Presation;
		public string Preperation;
		public string Indsats;
		public string FysiskForm;
		public string Intensitet;
		public string isTeamTraining;
		public bool HasBeenEvaluated = false;
	}



}
