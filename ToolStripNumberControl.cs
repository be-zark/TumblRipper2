using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace TumblRipper2
{
	[ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.ToolStrip)]
	public class ToolStripNumberControl : ToolStripControlHost
	{
		public System.Windows.Forms.Control NumericUpDownControl
		{
			get
			{
				return base.Control as NumericUpDown;
			}
		}

		public ToolStripNumberControl() : base(new NumericUpDown())
		{
		}

		protected override void OnSubscribeControlEvents(System.Windows.Forms.Control control)
		{
			base.OnSubscribeControlEvents(control);
			((NumericUpDown)control).ValueChanged += new EventHandler(this.OnValueChanged);
		}

		protected override void OnUnsubscribeControlEvents(System.Windows.Forms.Control control)
		{
			base.OnUnsubscribeControlEvents(control);
			((NumericUpDown)control).ValueChanged -= new EventHandler(this.OnValueChanged);
		}

		public void OnValueChanged(object sender, EventArgs e)
		{
			if (this.ValueChanged != null)
			{
				this.ValueChanged(sender, e);
			}
		}

		public event EventHandler ValueChanged;
	}
}