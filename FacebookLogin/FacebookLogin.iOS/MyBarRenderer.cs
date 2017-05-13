using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using FacebookLogin.iOS.View.Controls;


[assembly: ExportRenderer(typeof(ProgressBar), typeof(MyBarRenderer))]
namespace FacebookLogin.iOS.View.Controls
{
	public class MyBarRenderer : ProgressBarRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<ProgressBar> e)
		{
			base.OnElementChanged(e);


			//UITextField textField = (UITextField)Control;
			Control.ProgressTintColor = UIColor.FromRGB(200,160,124);
			Control.TrackTintColor = UIColor.FromRGB(79, 80, 79);
			//textField.BorderStyle = UITextBorderStyle.None;
		}
	}
}