using System;
using System.Collections;

namespace CombatLog.Sort
{
	/// <summary>
	/// Summary description for WSCByWeaponName.
	/// </summary>
	public class WSCByWeaponName : IComparer
	{
		private System.Windows.Forms.SortOrder direction;

		public WSCByWeaponName()
		{
			direction = System.Windows.Forms.SortOrder.Ascending;
		}

		public WSCByWeaponName(System.Windows.Forms.SortOrder Direction)
		{
			direction = Direction;
		}

		#region IComparer Members

		public int Compare(object x, object y)
		{
			WeaponSummary X = (WeaponSummary)x;
			WeaponSummary Y = (WeaponSummary)y;

			if ( direction == System.Windows.Forms.SortOrder.Ascending )
				return string.Compare(X.WeaponName, Y.WeaponName);
			else
				return string.Compare(Y.WeaponName, X.WeaponName);
		}

		#endregion
	}
}
