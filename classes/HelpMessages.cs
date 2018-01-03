using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TumblRipper2;

namespace TumblRipper2.classes
{
	public class HelpMessages
	{
		public static Dictionary<string, string> Messages;

		static HelpMessages()
		{
			HelpMessages.Messages = new Dictionary<string, string>()
			{
				{ "WelcomeMessage", "Welcome to TumblRipper ...." }
			};
		}

		public HelpMessages()
		{
		}

		public static void HelpMessage(string k)
		{
			if (HelpMessages.Hidden(k))
			{
				return;
			}
			HelpWindow helpWindow = new HelpWindow();
			helpWindow.SetText(k);
			helpWindow.Show();
			helpWindow.TopMost = true;
			helpWindow.BringToFront();
		}

		private static bool Hidden(string k)
		{
			return Settings.GetSettings().HiddenPopups.Contains(k);
		}

		public static void ResetHelp()
		{
			Settings.GetSettings().HiddenPopups.Clear();
			Settings.GetSettings().SaveSettings();
		}
	}
}