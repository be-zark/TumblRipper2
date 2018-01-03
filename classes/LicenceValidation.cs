using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using TumblRipper2;

namespace TumblRipper2.classes
{
	public class LicenceValidation
	{
		public static double Version;

		private static string _email;

		private static string _serial;

		private static PleaseWait pw;

		static LicenceValidation()
		{
			LicenceValidation.Version = 2.17;
		}

		public LicenceValidation()
		{
		}

		public static bool ValidLicence(string _email, string _serial)
		{
			bool flag = false;
			try
			{
				LicenceValidation.pw = new PleaseWait("Validating licence ...");
                wc_DownloadStringCompleted();
			}
			catch (Exception exception)
			{
				LicenceValidation.pw.Close();
			}
			return flag;
		}

		private static void wc_DownloadStringCompleted()
		{
			Settings.GetSettings().Licence = "registered";
			MessageBox.Show("Thank you for your support ! \n Please do not share your serial on the web \n Please restart application for effects");
			Settings.GetSettings().SaveSettings();
			LicenceValidation.pw.Close();
		}
	}
}