using System;
using Xamarin.Forms.Platform.iOS;
using UIKit;
using TestLayoutProblem;

[assembly: Xamarin.Forms.ExportRenderer(typeof(TestLayoutProblem.MyButton), typeof(TestScrollViewProblem.iOS.MyButtonRenderer))]

namespace TestScrollViewProblem.iOS
	{
		public class MyButtonRenderer : ButtonRenderer
		{
		
		UILongPressGestureRecognizer pressGestureRecognizer;
			protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
			{

				base.OnElementChanged(e);


			pressGestureRecognizer = new UILongPressGestureRecognizer(() =>
			{

				// Notify element
				if (pressGestureRecognizer.State == UIGestureRecognizerState.Began)
				{
					//circularProgressButton.OnPressStarted();

					//((TestLayoutProblem.MyButton)e).ShowDeleteButton();
					((TestLayoutProblem.MyButton)Element).ShowDeleteButton();

					//buttonRenderer.ForceUpdateText();

				}
				else if (pressGestureRecognizer.State == UIGestureRecognizerState.Ended)
				{
					//circularProgressButton.OnPressEnded();
				}
			});
			pressGestureRecognizer.MinimumPressDuration = 0.8f;
			//pressGestureRecognizer.Delegate = gestureDelegate;

			AddGestureRecognizer(pressGestureRecognizer);


				if (this.Control != null)
				{
					//UIButton button = this.Control;

					//button.SetOnClickListener((IOnClickListener)MyButtonRenderer.ButtonClickListener.Instance.Value);
					//button.SetOnLongClickListener((IOnLongClickListener)MyButtonRenderer.ButtonClickListener.Instance.Value);

				}
			}

		//	private void ForceUpdateText()
		//	{
		//		//this.Control.Text = this.Element.Text;
		//	}

		//	private void HandleClick(object sender, EventArgs e)
		//	{
		//		//	this.ForceUpdateText();
		//	}

		//	//private class ButtonClickListener : Java.Lang.Object, IOnLongClickListener, IOnClickListener, IJavaObject, IDisposable
		//	//{
		//	//	public static readonly Lazy<MyButtonRenderer.ButtonClickListener> Instance =
		//	//		new Lazy<MyButtonRenderer.ButtonClickListener>(() => new MyButtonRenderer.ButtonClickListener());

		//	//	public void OnClick(View v)
		//	//	{
		//	//		MyButtonRenderer buttonRenderer = v.Tag as MyButtonRenderer;

		//	//		if (buttonRenderer != null)
		//	//		{
		//	//			// have to cast to MyButton to access the new SendClicked method
		//	//			// which replaces that of the base class Button
		//	//			MyButton myButton = (MyButton)buttonRenderer.Element;

		//	//			myButton.SendClicked();
		//	//			//buttonRenderer.ForceUpdateText();
		//	//		}
		//	//	}
		//	//	public bool OnLongClick(View v)
		//	//	{

		//	//		MyButtonRenderer buttonRenderer = v.Tag as MyButtonRenderer;

		//	//		if (buttonRenderer != null)
		//	//		{
		//	//			// have to cast to MyButton to access the new SendClicked method
		//	//			// which replaces that of the base class Button
		//	//			MyButton myButton = (MyButton)buttonRenderer.Element;

		//	//			myButton.ShowDeleteButton();
		//	//			buttonRenderer.ForceUpdateText();
		//	//		}
		//	//		return false;
		//	//	}


		//	//}
		}
	}

