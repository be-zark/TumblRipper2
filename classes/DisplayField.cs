using System;
using System.Runtime.CompilerServices;

namespace TumblRipper2.classes
{
	public class DisplayField
	{
		public string Column
		{
			get;
			set;
		}

		public string Field
		{
			get;
			set;
		}

		public string Name
		{
			get;
			set;
		}

		public int Size
		{
			get;
			set;
		}

		public DisplayField()
		{
		}

		public DisplayField(string f, string n) : this(f, n, "Textbox")
		{
		}

		public DisplayField(string f, string n, string c)
		{
			this.Field = f;
			this.Name = n;
			this.Column = c;
		}

		public bool Equals(DisplayField a)
		{
			if (a == null)
			{
				return false;
			}
			return this.Field.Equals(a);
		}

		public override int GetHashCode()
		{
			return this.Field.GetHashCode();
		}

		public override string ToString()
		{
			return this.Name.ToString();
		}
	}
}