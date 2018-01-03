using System;
using System.ComponentModel;

namespace TumblRipper2
{
	public class SyncList<T> : BindingList<T>
	{
		private ISynchronizeInvoke _SyncObject;

		private Action<ListChangedEventArgs> _FireEventAction;

		public SyncList() : this(null)
		{
		}

		public SyncList(ISynchronizeInvoke syncObject)
		{
			this._SyncObject = syncObject;
			this._FireEventAction = new Action<ListChangedEventArgs>(this.FireEvent);
		}

		private void FireEvent(ListChangedEventArgs args)
		{
			base.OnListChanged(args);
		}

		protected override void OnListChanged(ListChangedEventArgs args)
		{
			if (this._SyncObject == null)
			{
				this.FireEvent(args);
				return;
			}
			this._SyncObject.Invoke(this._FireEventAction, new object[] { args });
		}

		public void SyncObject(ISynchronizeInvoke syncObject)
		{
			this._SyncObject = syncObject;
			this._FireEventAction = new Action<ListChangedEventArgs>(this.FireEvent);
		}
	}
}