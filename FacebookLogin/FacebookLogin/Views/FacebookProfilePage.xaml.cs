using FacebookLogin.ViewModels;
using Xamarin.Forms;

namespace FacebookLogin
{
	public partial class FacebookProfilePage : ContentPage
    {

        /// <summary>
        /// Make sure to get a new ClientId from:
        /// https://developers.facebook.com/apps/
        /// </summary>
        private string ClientId = "1315399838483895";

        public FacebookProfilePage()
        {


            InitializeComponent();

            var apiRequest =
                "https://www.facebook.com/dialog/oauth?client_id=" 
                + ClientId
                + "&display=popup&response_type=token&redirect_uri=http://www.facebook.com/connect/login_success.html/";
			apiRequest = "https://www.facebook.com/v2.8/dialog/oauth?client_id=1315399838483895&response_type=token&redirect_uri=https://www.facebook.com/connect/login_success.html";
            var webView = new WebView
            {
                Source = apiRequest,
                HeightRequest = 1
            };

            webView.Navigated += WebViewOnNavigated;

            Content = webView;

			NavigationPage.SetHasNavigationBar(this, false);



        }

        private async void WebViewOnNavigated(object sender, WebNavigatedEventArgs e)
        {
            
            var accessToken = ExtractAccessTokenFromUrl(e.Url);

            if (accessToken != "")
            {
                var vm = BindingContext as FacebookViewModel;

                await vm.SetFacebookUserProfileAsync(accessToken);

                Content = MainStackLayout;
				Database.SetFacebookProfile(vm.FacebookProfile);
				await Database.LoadTeamData();
				await Navigation.PushAsync(new Loading());
				// If user exists in database load and send user direktly to app
				if (await Database.CheckIfUserExist(vm.FacebookProfile.Id))
				{
					
					await Database.GetUserData(vm.FacebookProfile.Id);
					await Navigation.PopAsync();
					await Navigation.PushAsync(new MainTabbed());
					await HomePage.LoadPerformanceData();

				}
				else
				{
					await Navigation.PushAsync(new ChooseCoachOrPlayerPage());
				}
            }
			// Send to chose athlete or coach

        }

        private string ExtractAccessTokenFromUrl(string url)
        {
            if (url.Contains("access_token") && url.Contains("&expires_in="))
            {
                var at = url.Replace("https://www.facebook.com/connect/login_success.html#access_token=", "");

                if (Device.OS == TargetPlatform.WinPhone || Device.OS == TargetPlatform.Windows)
                {
                    at = url.Replace("http://www.facebook.com/connect/login_success.html#access_token=", "");
                }

                var accessToken = at.Remove(at.IndexOf("&expires_in="));

                return accessToken;
            }

            return string.Empty;
        }
    }
}
