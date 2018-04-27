using System;
using System.Collections;

namespace CombatLog.Sort
{
	/// <summary>
	/// Summary description for CombatLogCacheCollectionSort.
	/// </summary>
	public class CombatLogCacheCollectionSort : IComparer
	{
		private System.Windows.Forms.SortOrder _Direction = System.Windows.Forms.SortOrder.Ascending;

		public CombatLogCacheCollectionSort()
		{
		}

		public CombatLogCacheCollectionSort(System.Windows.Forms.SortOrder Direction)
		{
			this._Direction = Direction;
		}

		#region IComparer Members

		public int Compare(object x, object y)
		{
			CombatLogCache X = (CombatLogCache)x;
			CombatLogCache Y = (CombatLogCache)y;

			if ( this._Direction == System.Windows.Forms.SortOrder.Ascending )
				return DateTime.Compare(X.CreationTime, Y.CreationTime);
			else
				return DateTime.Compare(Y.CreationTime, X.CreationTime);
		}

		#endregion

		public System.Windows.Forms.SortOrder Direction
		{
			get { return this._Direction; }
			set { this._Direction = value; }
		}
	}
}
