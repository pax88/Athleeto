using Xamarin.Forms;
using App37.Droid;
using System.ComponentModel;
using Xamarin.Forms.Platform.Android.AppCompat;
using Android.Support.V4.View;
using Android.Support.Design.Widget;
using Android.Content.Res;
using Android.OS;
using Android;
using Android.Support.V4.Content;

[assembly: ExportRenderer(typeof(TabbedPage), typeof(MyTabsRenderer))]
namespace App37.Droid
{
	public class MyTabsRenderer : TabbedPageRenderer
	{
		bool setup;
		ViewPager pager;
		TabLayout layout;
		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			if (setup)
				return;

			if (e.PropertyName == "Renderer")
			{
				pager = (ViewPager)ViewGroup.GetChildAt(0);
				layout = (TabLayout)ViewGroup.GetChildAt(1);
				setup = true;

				ColorStateList colors = null;
				if ((int)Build.VERSION.SdkInt >= 23)
				{
					colors = Resources.GetColorStateList(Resource.Color.White, Forms.Context.Theme);
				}
				else
				{
					//Resources.GetColorStateList();
					//colors = Resources.GetColorStateList(Android.Graphics.Color.Rgb(150, 150, 150));
					colors = Resources.GetColorStateList(Resource.Color.White);
				}
				for (int i = 0; i < layout.TabCount; i++)
				{
					var tab = layout.GetTabAt(i);
					var icon = tab.Icon;
					if (icon != null)
					{
						icon = Android.Support.V4.Graphics.Drawable.DrawableCompat.Wrap(icon);
						Android.Support.V4.Graphics.Drawable.DrawableCompat.SetTintList(icon, colors);
					}
				
				}

			}
		}
	}
}