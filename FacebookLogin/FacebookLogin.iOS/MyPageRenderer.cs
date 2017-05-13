using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using UIKit;
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;


using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using FacebookLogin.iOS.View.Controls;




[assembly: ExportRenderer(typeof(ContentPage), typeof(FacebookLogin.iOS.ClusterSurveyiOSCustomPageRenderer))]
namespace FacebookLogin.iOS
	{
		public class ClusterSurveyiOSCustomPageRenderer : PageRenderer
		{
			public override void ViewDidAppear(bool animated)
			{
				//base.ViewDidAppear(animated);
				//var navctrl = this.ViewController.NavigationController;
				//navctrl.InteractivePopGestureRecognizer.Enabled = false;
			}
		}
	}

//[assembly: ExportRenderer(typeof(TimePicker), typeof(TimePicker24HRenderer))]
//namespace FacebookLogin.iOS.View.Controls