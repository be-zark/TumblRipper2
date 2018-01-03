using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;
using TumblRipper2.classes;

namespace TumblRipper2
{
	internal static class Program
	{
		private static Mutex s_Mutex;

		private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
		{
			MessageBox.Show(string.Concat("ERROR:", e.Exception.Message));
		}

		[STAThread]
		private static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler((object sender, ResolveEventArgs args) => {
				Assembly assembly;
				string str = string.Concat("TumblRipper2.", (new AssemblyName(args.Name)).Name, ".dll");
				using (Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(str))
				{
					if (manifestResourceStream != null)
					{
						byte[] numArray = new byte[checked(manifestResourceStream.Length)];
						manifestResourceStream.Read(numArray, 0, (int)numArray.Length);
						assembly = Assembly.Load(numArray);
					}
					else
					{
						assembly = null;
					}
				}
				return assembly;
			});
			Application.ThreadException += new ThreadExceptionEventHandler(Program.Application_ThreadException);
			Program.s_Mutex = new Mutex(true, "TumblRipper");
			if (!Program.s_Mutex.WaitOne(1000, false))
			{
				MessageBox.Show("Only one instance can run at a time");
				return;
			}
			if (!ImporterV1.HasPreviousConfig() || Settings.GetSettings().Sites.Count != 0 || MessageBox.Show("TumblRipper found settings for a previous version, import ?", "Previous Configuration", MessageBoxButtons.YesNo) != DialogResult.Yes)
			{
				Application.Run(new MainWindow());
				return;
			}
			ImporterV1.ImportSettings();
			MessageBox.Show("Settings imported. Please restart application");
		}
	}
}