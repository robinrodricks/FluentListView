using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Fluent {

	/// <summary>
	/// FluentListView is a C# wrapper around a .NET ListView, supporting model-bound lists,
	/// in-place item editing, drag and drop, icons, themes, trees &amp; data grids, and much more.
	/// This is a fork of ObjectListView with a focus on performance.
	/// </summary>
	public class FluentListView<T> : UserControl {

		private AdvancedListView advList;
		private FastListView fastList;
		private IEnumerable<T> items = new List<T>();

		/// <summary>
		/// The items that are bound to this list. You only need to set this once.
		/// When you change this collection, simply call Redraw() to update all the items.
		/// </summary>
		public IEnumerable<T> Items {
			get {
				return items;
			}
			set {
				items = value;
			}
		}

		/// <summary>
		/// Displays the items as a list. Items that are already created are re-used.
		/// </summary>
		public void Redraw() {

		}


	}
}
