using System;
using System.Windows.Forms;
using System.Collections;

namespace CombatLog.BestShotReport
{
	/// <summary>
	/// Summary description for BestShotListViewSorter.
	/// </summary>
	public class BestShotListViewSorter : IComparer
	{
		public System.Windows.Forms.SortOrder Direction = System.Windows.Forms.SortOrder.Ascending;
		public int SortColumn = 0;

		private const int CreatedCol	= 0;
		private const int WeaponCol		= 1;
		private const int HitTypeCol	= 2;
		private const int DamageCol		= 3;
		private const int TargetCol		= 4;
		private const int CharacterCol	= 5;
		private const int LogDirCol		= 6;

		public BestShotListViewSorter()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		#region IComparer Members

		public int Compare(object x, object y)
		{
			ListViewItem lx = (ListViewItem)x;
			ListViewItem ly = (ListViewItem)y;

			BestShotRecord X = (BestShotRecord)lx.Tag;
			BestShotRecord Y = (BestShotRecord)ly.Tag;

			switch ( SortColumn )
			{
				case CreatedCol:
					if ( Direction == SortOrder.Ascending )
						return DateTime.Compare(X.ShotDTM, Y.ShotDTM);
					else
						return DateTime.Compare(Y.ShotDTM, X.ShotDTM);

				case DamageCol:
				{
					if ( Direction == SortOrder.Ascending )
						return (int)Math.Round((X.Damage - Y.Damage), 0);
					else
						return (int)Math.Round((Y.Damage - X.Damage), 0);
				}
				default:
					if ( Direction == SortOrder.Ascending )
						return string.Compare(lx.SubItems[SortColumn].ToString(), ly.SubItems[SortColumn].ToString());
					else
						return string.Compare(ly.SubItems[SortColumn].ToString(), lx.SubItems[SortColumn].ToString());

			}
		}

		#endregion
	}
}
