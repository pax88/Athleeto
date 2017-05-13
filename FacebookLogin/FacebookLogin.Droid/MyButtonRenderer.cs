using System;
using Xamarin.Forms.Platform.Android;
using Android.Content.Res;
using Android.Graphics;
using Android.Widget;
using Android.Graphics.Drawables;
using System.ComponentModel;
using Android.Runtime;
using Android.Views;
using Android.Util;
using TestLayoutProblem;

[assembly: Xamarin.Forms.ExportRenderer(typeof(TestLayoutProblem.MyButton), typeof(TestScrollViewProblem.Android2.MyButtonRenderer))]

namespace TestScrollViewProblem.Android2
{
	public class MyButtonRenderer : ButtonRenderer
	{


		protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
		{
			base.OnElementChanged(e);

			if (this.Control != null)
			{
				Button button = this.Control;
				button.SetOnClickListener((IOnClickListener)MyButtonRenderer.ButtonClickListener.Instance.Value);
				button.SetOnLongClickListener((IOnLongClickListener)MyButtonRenderer.ButtonClickListener.Instance.Value);
		
			}
		}

		private void ForceUpdateText()
		{
			//this.Control.Text = this.Element.Text;
		}

		private void HandleClick(object sender, EventArgs e)
		{
		//	this.ForceUpdateText();
		}

		private class ButtonClickListener : Java.Lang.Object,IOnLongClickListener, IOnClickListener, IJavaObject, IDisposable
		{
			public static readonly Lazy<MyButtonRenderer.ButtonClickListener> Instance =
				new Lazy<MyButtonRenderer.ButtonClickListener>(() => new MyButtonRenderer.ButtonClickListener());

			public void OnClick(View v)
			{
				MyButtonRenderer buttonRenderer = v.Tag as MyButtonRenderer;

				if (buttonRenderer != null)
				{
					// have to cast to MyButton to access the new SendClicked method
					// which replaces that of the base class Button
					MyButton myButton = (MyButton)buttonRenderer.Element;

					myButton.SendClicked();
					//buttonRenderer.ForceUpdateText();
				}
			}
			public bool OnLongClick(View v)
			{

				MyButtonRenderer buttonRenderer = v.Tag as MyButtonRenderer;

				if (buttonRenderer != null)
				{
					// have to cast to MyButton to access the new SendClicked method
					// which replaces that of the base class Button
					MyButton myButton = (MyButton)buttonRenderer.Element;

					myButton.ShowDeleteButton();
					buttonRenderer.ForceUpdateText();
				}
				return false;
			}


		}
	}
}