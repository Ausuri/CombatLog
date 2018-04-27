using System;
using System.Collections;

namespace CombatLog.Sort
{
	/// <summary>
	/// Summary description for HitTypeInfoCollectionSorter.
	/// </summary>
	public class HitTypeInfoCollectionSorter : IComparer
	{
		private System.Windows.Forms.SortOrder _Direction = System.Windows.Forms.SortOrder.Ascending;

		public HitTypeInfoCollectionSorter()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public HitTypeInfoCollectionSorter(System.Windows.Forms.SortOrder Direction)
		{
			this._Direction = Direction;
		}

		#region IComparer Members

		public int Compare(object x, object y)
		{
			HitTypeInfo X = (HitTypeInfo)x;
			HitTypeInfo Y = (HitTypeInfo)y;

			if ( this._Direction == System.Windows.Forms.SortOrder.Ascending )
				return X.Rank - Y.Rank;
			else
				return Y.Rank - X.Rank;
		}

		#endregion
	}
}
