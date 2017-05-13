using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using FacebookLogin.iOS.View.Controls;

//[assembly: ExportRenderer(typeof(Entry), typeof(EntryRendererComment))]
[assembly: Xamarin.Forms.ExportRenderer(typeof(TestLayoutProblem.MyEntryComment), typeof(TestScrollViewProblem.iOS.MyEntryCommentRenderer))]

namespace TestScrollViewProblem.iOS
{
	public class MyEntryCommentRenderer : EntryRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
		{
			base.OnElementChanged(e);

			UITextField textField = (UITextField)Control;

			// Most commonly customized for font and no border

			//textField.BorderStyle = UITextBorderStyle.None;
			textField.BackgroundColor = UIKit.UIColor.FromRGB(46, 46, 46);
			textField.TextColor = UIKit.UIColor.FromRGB(200, 160, 124);
			textField.BorderStyle = UITextBorderStyle.None;
		}
	}
}
