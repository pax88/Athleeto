using FacebookLogin.Views;
using Xamarin.Forms;
using XamForms.Controls;
using System;
using System.Collections.Generic;
using Com.OneSignal;

namespace FacebookLogin
{


    public partial class App : Application
    {


        public App()
        {
            InitializeComponent();
			Database.LoadDatabase();

			//MainPage = new NavigationPage(new Loading());

			//OLD working
			MainPage = new NavigationPage(new MainPage());

			//MainPage = new NavigationPage(new DiarySummary());
			//MainPage = new NavigationPage(new QuestionPage(""));
			//MainPage = new NavigationPage(new AddInjuryPage());



			//MainPage = new MDetailPage();


			//MainPage = new NavigationPage(new MainTabbed());

			//MainPage = new NavigationPage(new ChoseTeamPage(true));
			//MainPage = new CreateTeamPage();
            //MainPage = new NavigationPage(new MainCsPage())
            //{
            //    Title = "Facebook Login"
            //};

			  OneSignal.Current.StartInit("a6537203-18b2-4c90-9b2c-7102a2916c78")
				.EndInit();


			PushNotificationsHandler.instance.SheduleNotification();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
