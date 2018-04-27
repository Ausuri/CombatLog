using System;
using System.Xml.Serialization;
using System.Windows.Forms;
using System.Collections;
using System.Runtime.InteropServices;

namespace SaveListView
{
	/// <summary>
	/// This class is used to save and restore the column width and order settings
	/// for a ListView control.  Unfortunately, there is no easy way to obtain the
	/// order so a Windows message must be sent to the control.  This class
	/// encapsulates that functionality as well as the serialization/deserialization
	/// of the settings.
	/// </summary>
	[Serializable]
	public class ListViewSettings
	{

		public ListViewSettings()
		{
		}

		[DllImport("user32.dll")]
		static extern bool SendMessage(IntPtr hWnd, Int32 msg, Int32 wParam, ref
			LV_COLUMN lParam);

		[StructLayoutAttribute(LayoutKind.Sequential)]
			struct LV_COLUMN
		{
			public UInt32 mask;
			public Int32 fmt;
			public Int32 cx;
			public String pszText;
			public Int32 cchTextMax;
			public Int32 iSubItem;
			public Int32 iImage;
			public Int32 iOrder;
		}  

		const Int32 LVM_FIRST = 0x1000;
		const Int32 LVM_GETCOLUMN = LVM_FIRST + 95;
		const Int32 LVM_SETCOLUMN = LVM_FIRST + 96;
		const Int32 LVCF_ORDER = 0x0020;

		[XmlElement("ListViewColumns", typeof(ListViewColumn))]
		public ArrayList listViewCols = new ArrayList();


		public ListViewSettings( ListView listView )
		{
			try
			{
				foreach( ColumnHeader column in listView.Columns )
				{
					LV_COLUMN pcol = new LV_COLUMN();
					pcol.mask = LVCF_ORDER;
					bool ret = SendMessage(listView.Handle, LVM_GETCOLUMN, column.Index, ref pcol);
					listViewCols.Add( new ListViewColumn( column.Text, column.Width, pcol.iOrder ));
				}
			}
			catch {}
		}

		public void RestoreFormat( ListView listView )
		{
			try
			{
				listView.Hide();
				for( int i=0; i<listViewCols.Count; i++ )
				{
					foreach( ColumnHeader column in listView.Columns )
					{
						if( column.Text == ((ListViewColumn)listViewCols[i]).header )
						{
							LV_COLUMN pcol = new LV_COLUMN();
							pcol.mask = LVCF_ORDER;
							pcol.iOrder = ((ListViewColumn)listViewCols[i]).order;
							bool ret = SendMessage(listView.Handle, LVM_SETCOLUMN, column.Index, ref pcol);
							column.Width = ((ListViewColumn)listViewCols[i]).width;
							break;
						}
					}
				}
				listView.Show();
			}
			catch {}
		}
	}

	[Serializable]
	public class ListViewColumn
	{
		public string header;
		public int width;
		public int order;

		public ListViewColumn()
		{
		}

		public ListViewColumn( string colHeader, int colWidth, int colOrder )
		{
			header = colHeader;
			width = colWidth;
			order = colOrder;
		}
	}
}
