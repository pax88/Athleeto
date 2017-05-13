//using System;
//using Xamarin.Forms.Platform.iOS;
//using UIKit;
//using TestLayoutProblem;
//using Foundation;

//[assembly: Xamarin.Forms.ExportRenderer(typeof(TestLayoutProblem.TimePicker24H), typeof(TestScrollViewProblem.iOS.TimePicker24HRenderer))]

//namespace TestScrollViewProblem.iOS
//	{
//		public class TimePicker24HRenderer : TimePickerRenderer
//		{

//		protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.TimePicker> e)
//			{
//				base.OnElementChanged(e);
//				var timePicker = (UIDatePicker)Control.InputView;
//				timePicker.Locale = new NSLocale("no_nb");
//			}
//		}
//	}

using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using FacebookLogin.iOS.View.Controls;

[assembly: ExportRenderer (typeof (TimePicker), typeof (TimePicker24HRenderer))]
namespace FacebookLogin.iOS.View.Controls
{
	public class TimePicker24HRenderer : TimePickerRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<TimePicker> e)
		{
			base.OnElementChanged(e);
			var timePicker = (UIDatePicker)Control.InputView;
			timePicker.Locale = new NSLocale("no_nb");

			UITextField textField = (UITextField)Control;


			textField.BorderStyle = UITextBorderStyle.None;
		}
	}
}



