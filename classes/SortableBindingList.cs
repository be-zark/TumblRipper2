using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;

namespace TumblRipper2.classes
{
	public class SortableBindingList<T> : BindingList<T>, INotifyPropertyChanged
	where T : class
	{
		private List<T> _originalList;

		private ListSortDirection _sortDirection;

		private PropertyDescriptor _sortProperty;

		private Action<SortableBindingList<T>, List<T>> populateBaseList;

		protected override ListSortDirection SortDirectionCore
		{
			get
			{
				return this._sortDirection;
			}
		}

		protected override PropertyDescriptor SortPropertyCore
		{
			get
			{
				return this._sortProperty;
			}
		}

		protected override bool SupportsSortingCore
		{
			get
			{
				return true;
			}
		}

		public SortableBindingList()
		{
			this.populateBaseList = (SortableBindingList<T> a, List<T> b) => a.ResetItems(b);
			//base();
			this._originalList = new List<T>();
		}

		public SortableBindingList(IEnumerable<T> enumerable)
		{
			this.populateBaseList = (SortableBindingList<T> a, List<T> b) => a.ResetItems(b);
			//base();
			this._originalList = new List<T>(enumerable);
			this.populateBaseList(this, this._originalList);
		}

		public SortableBindingList(List<T> list)
		{
			this.populateBaseList = (SortableBindingList<T> a, List<T> b) => a.ResetItems(b);
			//base();
			this._originalList = list;
			this.populateBaseList(this, this._originalList);
		}

		public void Add(INotifyPropertyChanged item)
		{
			item.PropertyChanged += new PropertyChangedEventHandler(this.item_PropertyChanged);
			base.Add((T)item);
		}

		protected override void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction)
		{
			this._sortProperty = prop;
			this._sortDirection = direction;
			SortableBindingList<T>.PropertyCompare propertyCompare = new SortableBindingList<T>.PropertyCompare(prop, this._sortDirection);
			List<T> ts = new List<T>(this);
			ts.Sort(propertyCompare);
			this.ResetItems(ts);
			base.ResetBindings();
			this._sortDirection = (this._sortDirection == ListSortDirection.Ascending ? ListSortDirection.Descending : ListSortDirection.Ascending);
		}

		private void item_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			this.PropertyChanged(sender, new PropertyChangedEventArgs(string.Format("{0}:{1}", sender, e)));
		}

		protected override void OnListChanged(ListChangedEventArgs e)
		{
			this._originalList = base.Items.ToList<T>();
		}

		protected override void RemoveSortCore()
		{
			this.ResetItems(this._originalList);
		}

		private void ResetItems(List<T> items)
		{
			base.ClearItems();
			for (int i = 0; i < items.Count; i++)
			{
				base.InsertItem(i, items[i]);
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		private class PropertyCompare : IComparer<T>
		{
			private PropertyDescriptor _property;

			private ListSortDirection _direction;

			public PropertyCompare(PropertyDescriptor property, ListSortDirection direction)
			{
				this._property = property;
				this._direction = direction;
			}

			public int Compare(T comp1, T comp2)
			{
				IComparable value = this._property.GetValue(comp1) as IComparable;
				IComparable comparable = this._property.GetValue(comp2) as IComparable;
				if (value == comparable)
				{
					return 0;
				}
				if (this._direction == ListSortDirection.Ascending)
				{
					if (value == null)
					{
						return -1;
					}
					return value.CompareTo(comparable);
				}
				if (comparable == null)
				{
					return 1;
				}
				return comparable.CompareTo(value);
			}
		}
	}
}