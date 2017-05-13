using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using FacebookLogin.iOS.View.Controls;

[assembly: ExportRenderer(typeof(Entry), typeof(EntryRendererNoBorder))]
namespace FacebookLogin.iOS.View.Controls
{
	public class EntryRendererNoBorder : EntryRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
		{
			base.OnElementChanged(e);

			UITextField textField = (UITextField)Control;

			// Most commonly customized for font and no border

			//textField.BorderStyle = UITextBorderStyle.None;
			textField.BackgroundColor = UIKit.UIColor.FromRGB(79,81,79);
			textField.TextColor = UIKit.UIColor.FromRGB(200, 160, 124);
			textField.BorderStyle = UITextBorderStyle.None;
		}
	}
}
