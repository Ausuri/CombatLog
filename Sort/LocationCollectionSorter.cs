using System;
using System.Collections;
using System.Windows.Forms;

namespace CombatLog.Sort
{
	/// <summary>
	/// Summary description for LocationCollectionSorter.
	/// </summary>
	public class LocationCollectionSorter : IComparer
	{
		private SortOrder _Direction = SortOrder.Ascending;

		public LocationCollectionSorter()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public LocationCollectionSorter(SortOrder Direction)
		{
			this._Direction = Direction;
		}
		#region IComparer Members

		public int Compare(object x, object y)
		{
			LocationInfo.Location X = (LocationInfo.Location)x;
			LocationInfo.Location Y = (LocationInfo.Location)y;

			if ( this._Direction == SortOrder.Ascending )
				return DateTime.Compare(X.LogDTM, Y.LogDTM);
			else
				return DateTime.Compare(Y.LogDTM, X.LogDTM);
		}

		#endregion
	}
}
