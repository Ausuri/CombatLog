using System;
using System.Windows.Forms;
using System.Collections;

namespace CombatLog
{
	/// <summary>
	/// Summary description for CombatLogLVSorter.
	/// </summary>
	public class AttackLogLVSorter : IComparer
	{
		public System.Windows.Forms.SortOrder Direction;
		public int SortColumn = -1;

		const int DateTimeCol	= 0;
		const int AttackerCol	= 1;
		const int WeaponCol		= 2;
		const int HitTypeCol	= 3;
		const int DamageCol		= 4;

		public AttackLogLVSorter()
		{
		}
		#region IComparable Members

		public int Compare(object x, object y)
		{
			ListViewItem lx = (ListViewItem)x;
			ListViewItem ly = (ListViewItem)y;

			AttackEntry X = (AttackEntry)lx.Tag;
			AttackEntry Y = (AttackEntry)ly.Tag;

			switch (SortColumn)
			{
				case DateTimeCol:
				{
					if ( Direction == SortOrder.Ascending )
						return DateTime.Compare(X.LogDTM, Y.LogDTM);
					else
						return DateTime.Compare(Y.LogDTM, X.LogDTM);
				}
				case WeaponCol:
				{
					if ( Direction == SortOrder.Ascending )
						return string.Compare(X.WeaponName, Y.WeaponName);
					else
						return string.Compare(Y.WeaponName, X.WeaponName);
				}
				case HitTypeCol:
				{
					if ( Direction == SortOrder.Ascending )
						return HitTypeLib.GetRank(X.HitDescription) - HitTypeLib.GetRank(Y.HitDescription);
					else
						return HitTypeLib.GetRank(Y.HitDescription) - HitTypeLib.GetRank(X.HitDescription);
				}
				case AttackerCol:
				{
					if ( Direction == SortOrder.Ascending )
						return string.Compare(lx.SubItems[AttackerCol].Text, ly.SubItems[AttackerCol].Text);
					else
						return string.Compare(ly.SubItems[AttackerCol].Text, lx.SubItems[AttackerCol].Text);
				}
				case DamageCol:
				{
					if ( Direction == SortOrder.Ascending )
					{
						if ( X.DamageCaused > Y.DamageCaused )
							return 1;
						else if ( X.DamageCaused < Y.DamageCaused )
							return -1;
						else 
							return 0;
					}
					else
					{
						if ( X.DamageCaused > Y.DamageCaused )
							return -1;
						else if ( X.DamageCaused < Y.DamageCaused )
							return 1;
						else 
							return 0;
					}
				}
				default:
					return 0;
			}
		}

		#endregion
	}
}
