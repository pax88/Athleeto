using System;
using Xamarin.Forms;
using System.Windows.Input;

namespace TestLayoutProblem
{
	public class MyButton : Button
	{
		// we have to hide the existing event since we cannot raise the event
		// (personal opinion - too much of the framework is internal. makes it very difficult to workaround bugs)
		public new event EventHandler Clicked;
		public bool longClicked = false;

		public void SendClicked()
		{
			if (longClicked)
			{
				//longClicked = false;
				return;
			}
			//if(Clicked == null)

				FacebookLogin.HomePage.instance.OnDoQuestionairClicked(this, null);

			ICommand command = this.Command;

			if (command != null)
				command.Execute(this.CommandParameter);

			EventHandler eventHandler = this.Clicked;

			if (eventHandler != null)
				eventHandler(this, EventArgs.Empty);
		}
		public void ShowDeleteButton()
		{
			FacebookLogin.HomePage.instance.ShowDeleteButtons();
			//ICommand command = this.Command;

			//if (command != null)
			//	command.Execute(this.CommandParameter);

			//EventHandler eventHandler = this.Clicked;

			//if (eventHandler != null)
			//	eventHandler(this, EventArgs.Empty);
			longClicked = true;
		}


	}
}