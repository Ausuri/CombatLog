using System;
using System.Collections;
using System.Windows.Forms;

namespace CombatLog.Sort
{
	/// <summary>
	/// Summary description for GameLogCollectionSorter.
	/// </summary>
	public class GameLogCollectionSorter : IComparer
	{
		private SortOrder _Direction;

		public GameLogCollectionSorter()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public GameLogCollectionSorter(SortOrder Direction)
		{
			this._Direction = Direction;
		}

		#region IComparer Members

		public int Compare(object x, object y)
		{
			GameLog X = (GameLog)x;
			GameLog Y = (GameLog)y;

			if ( _Direction == SortOrder.Ascending )
				return DateTime.Compare(X.SessionStartedDTM, Y.SessionStartedDTM);
			else
				return DateTime.Compare(Y.SessionStartedDTM, X.SessionStartedDTM);
		}

		#endregion

		public SortOrder Direction
		{
			get { return this._Direction; }
			set { this._Direction = value; }
		}
	}
}
