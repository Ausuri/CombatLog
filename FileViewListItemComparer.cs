using System;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;

namespace CombatLog
{
	/// <summary>
	/// Summary description for ListViewItemComparer.
	/// </summary>
	public class FileViewListItemComparer : IComparer
	{
    	public System.Windows.Forms.SortOrder SortOrder;
		public ListView listViewItem;
		public int CurrentSortColumn = -1;

		public FileViewListItemComparer() 
		{
			// col=0;
		}

		public FileViewListItemComparer(int column) 
		{
			CurrentSortColumn = column;
		}

		public int Compare(object x, object y) 
		{
			ListViewItem lx = (ListViewItem)x;
			ListViewItem ly = (ListViewItem)y;

			switch ( CurrentSortColumn )
			{
				case 0:
					if ( SortOrder == System.Windows.Forms.SortOrder.Ascending )
						return DateTime.Compare( ((CombatLogCache)lx.Tag).CreationTime, ((CombatLogCache)ly.Tag).CreationTime); // Compare X with Y
					else
						return DateTime.Compare( ((CombatLogCache)ly.Tag).CreationTime, ((CombatLogCache)lx.Tag).CreationTime); // Compare Y with X

				case 2:
					if ( SortOrder == System.Windows.Forms.SortOrder.Ascending )
						return Convert.ToInt32(((CombatLogCache)lx.Tag).FileSize - ((CombatLogCache)ly.Tag).FileSize);
					else
						return Convert.ToInt32(((CombatLogCache)ly.Tag).FileSize - ((CombatLogCache)lx.Tag).FileSize);
				 
				default:
					if ( SortOrder == System.Windows.Forms.SortOrder.Ascending )
						return String.Compare(((ListViewItem)x).SubItems[CurrentSortColumn].Text, ((ListViewItem)y).SubItems[CurrentSortColumn].Text);
					else
						return String.Compare(((ListViewItem)y).SubItems[CurrentSortColumn].Text, ((ListViewItem)x).SubItems[CurrentSortColumn].Text);
			}
		}
	}
}
