using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using FacebookLogin.iOS.View.Controls;


[assembly: ExportRenderer(typeof(DatePicker), typeof(DatePicker24HRenderer))]
namespace FacebookLogin.iOS.View.Controls
{
	public class DatePicker24HRenderer : DatePickerRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
		{
			base.OnElementChanged(e);


			UITextField textField = (UITextField)Control;


			textField.BorderStyle = UITextBorderStyle.None;
		}
	}
}