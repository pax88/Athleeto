using System;
using Xamarin.Forms;
using System.Net;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace FacebookLogin.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);
			Title = "Main";
			//string queryStr = string.Format("http://{0}/testing.php?usr=test", host);

			//var request = System.Net.HttpWebRequest.Create(queryStr);

			//request.Method = System.Net.WebRequestMethods.Http.Get;

			//var response = request.GetResponse();

			//System.IO.StreamReader str = new System.IO.StreamReader(response.GetResponseStream());

			//UIAlertView _response = new UIAlertView("Response", str.ReadToEnd(), null, "Ok", null);
			//_response.Show();

			if (Application.Current.Properties.ContainsKey("AutoLoginValue"))
			{
				int id = (int)Application.Current.Properties["AutoLoginValue"];

				if (id == 1)
				{
					LoginWithFacebook_Clicked(this, null);
				}
			}


			//websiteObject.Source = "http://www.athleeto.com/social/wp-content/themes/farben-basic%202/stats/MobileStats.php";


		}
		public static System.Net.Http.HttpClient client = null;

		static public async Task<string> GetStatsHTML()
		{
			if (client == null)
				client = new System.Net.Http.HttpClient();
			client.BaseAddress = new Uri("http://www.athleeto.com/");
			//var response = await client.GetAsync("social/player/?teamID=13&athleteID=10211875891918667");
			var response = await client.GetAsync("social/wp-content/themes/farben-basic%202/stats/MobileStats.php");
			//var response = await client.GetAsync("social/mobile-athlete/?athleteID=10210411355274507&phone=true");

			var placesJson = response.Content.ReadAsStringAsync().Result;

			return placesJson;

			int indexFrom = placesJson.IndexOf("<header class=\"navbar-default\">");
			int indexTo = placesJson.IndexOf("<h3>Injury</h3>")-100;
			string placesJson2 = placesJson;


			string begining = placesJson.Substring(0, indexFrom);
			string ending = placesJson2.Substring(indexTo, placesJson2.Length-indexTo);

			return begining + ending;




			//string new1 = placesJson.Substring(placesJson.IndexOf("<div class=\"col-md-6\">\n\t\t\t\t<div class=\"box\">\n\t\t\t\t\t\n\n<h3>Accumulated training load</h3>"));
			//return new1;
			////

			//int indexEnding = new1.IndexOf("The accumulated training load is sum of the TRIMP for the previous\n4 days with a half-life of\n 1 day(s).\n\t\t\t\t</div>\n\t\t\t</div>");
			//string result = new1.Remove(indexEnding, new1.Length - indexEnding);

		}
		protected override void OnAppearing()
		{
			base.OnAppearing();
			clicked = false;
			FacebookLoginButton.Opacity = 1.0f;


		}
		static public async Task<bool> GetPlacesAsync()
		{
			var client = new System.Net.Http.HttpClient();
			client.BaseAddress = new Uri("http://athleeto.com/");
			var response = await client.GetAsync("phpGetUsers.php");
			var placesJson = response.Content.ReadAsStringAsync().Result;
			//Placeobject placeobject = new Placeobject();
			//if (placesJson != "")
			//{
			//	placeobject = JsonConvert.DeserializeObject<Placeobject>(placesJson);
			//}
			return true;
		}


		public static bool clicked = false;
		private async void LoginWithFacebook_Clicked(object sender, EventArgs e)
		{
			string test = await GetStatsHTML();

			//var htmlSource = new HtmlWebViewSource();
			//htmlSource.BaseUrl = "http://www.athleeto.com/social/wp-content/themes/farben-basic%202/stats/MobileStats.php";
			//htmlSource.Html = test;
			//websiteObject.Source = htmlSource;
			//return;

			//websiteObject.Source =


			if (clicked == false)
			{

				try
				{
					var client = new System.Net.Http.HttpClient();
					client.BaseAddress = new Uri("http://athleeto.com/");
					var response = await client.GetAsync("phpGetUsers.php");
				}
				catch
				{
					DisplayAlert("No Internet", "You need internet to use this app, when fixed restart the app!", "I Understand..");
					return;
				}
					

				clicked = true;
				FacebookLoginButton.Opacity = 0.3f;
				await GetPlacesAsync();
				await Navigation.PushAsync(new FacebookProfilePage());
			}
			//await Navigation.PushAsync(new Loading());
        }
    }
}
