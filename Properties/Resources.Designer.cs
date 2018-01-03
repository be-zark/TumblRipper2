using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace TumblRipper2.Properties
{
	[CompilerGenerated]
	[DebuggerNonUserCode]
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	internal class Resources
	{
		private static System.Resources.ResourceManager resourceMan;

		private static CultureInfo resourceCulture;

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return Resources.resourceCulture;
			}
			set
			{
				Resources.resourceCulture = value;
			}
		}

		internal static string DebugSuccess
		{
			get
			{
				return Resources.ResourceManager.GetString("DebugSuccess", Resources.resourceCulture);
			}
		}

		internal static string DebugThisWillSend
		{
			get
			{
				return Resources.ResourceManager.GetString("DebugThisWillSend", Resources.resourceCulture);
			}
		}

		internal static string DebugThisWillSendTITLE
		{
			get
			{
				return Resources.ResourceManager.GetString("DebugThisWillSendTITLE", Resources.resourceCulture);
			}
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static System.Resources.ResourceManager ResourceManager
		{
			get
			{
				if (Resources.resourceMan == null)
				{
					Resources.resourceMan = new System.Resources.ResourceManager("TumblRipper2.Properties.Resources", typeof(Resources).Assembly);
				}
				return Resources.resourceMan;
			}
		}

		internal Resources()
		{
		}
	}
}