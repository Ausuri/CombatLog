using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Xml.Serialization;
using CombatLog.WeaponDataDB;
using System.Text;

using Cells = SourceGrid2.Cells.Real;

namespace CombatLog
{
	/// <summary>
	/// Summary description for WeaponForm.
	/// </summary>
	public class WeaponForm : System.Windows.Forms.Form
	{
		public Config.UserConfig MyConfig;
		public CombatLog.WeaponDataDB.WeaponDataSourceCollection WeaponData;
		
		public string SearchWeaponName = null;
		public WeaponDataDB.WeaponClass SearchClass;
		public WeaponDataDB.WeaponType SearchType;

		public bool doTypeFilter = false;
		public bool doClassFilter = false;
		public bool doNameFilter = false;

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button btnSearch;
		private System.Windows.Forms.Panel panelTop;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox tbSearchText;
		private System.Windows.Forms.ComboBox cbTypes;
		private System.Windows.Forms.ComboBox cbClass;
		private SourceGrid2.Grid grid1;
		private System.Windows.Forms.Button btnClear;
		private System.Windows.Forms.StatusBar statusBar1;
		private System.Windows.Forms.StatusBarPanel statusBarPanel1;
		private System.Windows.Forms.StatusBarPanel statusBarPanel2;
		private System.Windows.Forms.ContextMenu contextMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem6;
		private System.Windows.Forms.MenuItem menuItemSaveAs;
		private System.Windows.Forms.MenuItem menuItemClose;
		private System.Windows.Forms.MenuItem menuItem9;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
		private System.Windows.Forms.MenuItem menuItemView;
		private System.Windows.Forms.MenuItem menuItemViewAsHTML;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public WeaponForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(WeaponForm));
			this.panelTop = new System.Windows.Forms.Panel();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.btnClear = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.cbClass = new System.Windows.Forms.ComboBox();
			this.tbSearchText = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.cbTypes = new System.Windows.Forms.ComboBox();
			this.btnSearch = new System.Windows.Forms.Button();
			this.grid1 = new SourceGrid2.Grid();
			this.contextMenu1 = new System.Windows.Forms.ContextMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.menuItem5 = new System.Windows.Forms.MenuItem();
			this.statusBar1 = new System.Windows.Forms.StatusBar();
			this.statusBarPanel1 = new System.Windows.Forms.StatusBarPanel();
			this.statusBarPanel2 = new System.Windows.Forms.StatusBarPanel();
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.menuItem6 = new System.Windows.Forms.MenuItem();
			this.menuItemSaveAs = new System.Windows.Forms.MenuItem();
			this.menuItemClose = new System.Windows.Forms.MenuItem();
			this.menuItem9 = new System.Windows.Forms.MenuItem();
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			this.menuItemView = new System.Windows.Forms.MenuItem();
			this.menuItemViewAsHTML = new System.Windows.Forms.MenuItem();
			this.panelTop.SuspendLayout();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.statusBarPanel1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.statusBarPanel2)).BeginInit();
			this.SuspendLayout();
			// 
			// panelTop
			// 
			this.panelTop.Controls.Add(this.groupBox1);
			this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelTop.Location = new System.Drawing.Point(0, 0);
			this.panelTop.Name = "panelTop";
			this.panelTop.Size = new System.Drawing.Size(920, 136);
			this.panelTop.TabIndex = 9;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.btnClear);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.cbClass);
			this.groupBox1.Controls.Add(this.tbSearchText);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.cbTypes);
			this.groupBox1.Controls.Add(this.btnSearch);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox1.Location = new System.Drawing.Point(0, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(920, 136);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Weapon Filter";
			// 
			// btnClear
			// 
			this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnClear.Location = new System.Drawing.Point(128, 101);
			this.btnClear.Name = "btnClear";
			this.btnClear.TabIndex = 5;
			this.btnClear.Text = "Reset Filter";
			this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(56, 23);
			this.label1.TabIndex = 4;
			this.label1.Text = "Type";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(56, 23);
			this.label2.TabIndex = 4;
			this.label2.Text = "Class";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// cbClass
			// 
			this.cbClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbClass.Location = new System.Drawing.Point(67, 48);
			this.cbClass.Name = "cbClass";
			this.cbClass.Size = new System.Drawing.Size(216, 21);
			this.cbClass.Sorted = true;
			this.cbClass.TabIndex = 2;
			// 
			// tbSearchText
			// 
			this.tbSearchText.Location = new System.Drawing.Point(67, 72);
			this.tbSearchText.Name = "tbSearchText";
			this.tbSearchText.Size = new System.Drawing.Size(216, 20);
			this.tbSearchText.TabIndex = 3;
			this.tbSearchText.Text = "";
			this.tbSearchText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbSearchText_KeyPress);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 72);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(56, 23);
			this.label3.TabIndex = 4;
			this.label3.Text = "Search";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// cbTypes
			// 
			this.cbTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbTypes.Location = new System.Drawing.Point(67, 25);
			this.cbTypes.Name = "cbTypes";
			this.cbTypes.Size = new System.Drawing.Size(216, 21);
			this.cbTypes.Sorted = true;
			this.cbTypes.TabIndex = 1;
			this.cbTypes.SelectedIndexChanged += new System.EventHandler(this.cbTypes_SelectedIndexChanged);
			// 
			// btnSearch
			// 
			this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnSearch.Location = new System.Drawing.Point(208, 101);
			this.btnSearch.Name = "btnSearch";
			this.btnSearch.TabIndex = 4;
			this.btnSearch.Text = "Search";
			this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
			// 
			// grid1
			// 
			this.grid1.AutoSizeMinHeight = 10;
			this.grid1.AutoSizeMinWidth = 10;
			this.grid1.AutoStretchColumnsToFitWidth = false;
			this.grid1.AutoStretchRowsToFitHeight = false;
			this.grid1.BackColor = System.Drawing.SystemColors.ControlLight;
			this.grid1.ContextMenuStyle = SourceGrid2.ContextMenuStyle.None;
			this.grid1.CustomSort = false;
			this.grid1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grid1.GridToolTipActive = true;
			this.grid1.Location = new System.Drawing.Point(0, 136);
			this.grid1.Name = "grid1";
			this.grid1.Size = new System.Drawing.Size(920, 376);
			this.grid1.SpecialKeys = SourceGrid2.GridSpecialKeys.Default;
			this.grid1.TabIndex = 10;
			// 
			// contextMenu1
			// 
			this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						 this.menuItem1,
																						 this.menuItem2,
																						 this.menuItem3,
																						 this.menuItem4,
																						 this.menuItem5});
			this.contextMenu1.Popup += new System.EventHandler(this.contextMenu1_Popup);
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.Text = "Ctx 1";
			this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 1;
			this.menuItem2.Text = "Ctx 2";
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 2;
			this.menuItem3.Text = "Ctx 3";
			// 
			// menuItem4
			// 
			this.menuItem4.Index = 3;
			this.menuItem4.Text = "-";
			// 
			// menuItem5
			// 
			this.menuItem5.Index = 4;
			this.menuItem5.Text = "Foo";
			// 
			// statusBar1
			// 
			this.statusBar1.Location = new System.Drawing.Point(0, 512);
			this.statusBar1.Name = "statusBar1";
			this.statusBar1.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
																						  this.statusBarPanel1,
																						  this.statusBarPanel2});
			this.statusBar1.ShowPanels = true;
			this.statusBar1.Size = new System.Drawing.Size(920, 22);
			this.statusBar1.TabIndex = 11;
			// 
			// statusBarPanel1
			// 
			this.statusBarPanel1.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
			this.statusBarPanel1.Width = 894;
			// 
			// statusBarPanel2
			// 
			this.statusBarPanel2.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
			this.statusBarPanel2.Width = 10;
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem6,
																					  this.menuItemView});
			// 
			// menuItem6
			// 
			this.menuItem6.Index = 0;
			this.menuItem6.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItemSaveAs,
																					  this.menuItem9,
																					  this.menuItemClose});
			this.menuItem6.Text = "File";
			// 
			// menuItemSaveAs
			// 
			this.menuItemSaveAs.Index = 0;
			this.menuItemSaveAs.Text = "Save As...";
			this.menuItemSaveAs.Click += new System.EventHandler(this.menuItemSaveAs_Click);
			// 
			// menuItemClose
			// 
			this.menuItemClose.Index = 2;
			this.menuItemClose.Text = "Close";
			this.menuItemClose.Click += new System.EventHandler(this.menuItemClose_Click);
			// 
			// menuItem9
			// 
			this.menuItem9.Index = 1;
			this.menuItem9.Text = "-";
			// 
			// saveFileDialog1
			// 
			this.saveFileDialog1.DefaultExt = "htm";
			this.saveFileDialog1.Filter = "HTML File|*.htm*|CSV File|*.csv";
			this.saveFileDialog1.Title = "Save Grid As HTML";
			// 
			// menuItemView
			// 
			this.menuItemView.Index = 1;
			this.menuItemView.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						 this.menuItemViewAsHTML});
			this.menuItemView.Text = "View";
			// 
			// menuItemViewAsHTML
			// 
			this.menuItemViewAsHTML.Index = 0;
			this.menuItemViewAsHTML.Text = "HTML";
			this.menuItemViewAsHTML.Click += new System.EventHandler(this.menuItemViewAsHTML_Click);
			// 
			// WeaponForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(920, 534);
			this.Controls.Add(this.grid1);
			this.Controls.Add(this.statusBar1);
			this.Controls.Add(this.panelTop);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Menu = this.mainMenu1;
			this.Name = "WeaponForm";
			this.Text = "CLA Weapon Browser";
			this.Load += new System.EventHandler(this.WeaponForm_Load);
			this.panelTop.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.statusBarPanel1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.statusBarPanel2)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void LoadWeaponData()
		{
			if ( !File.Exists(Application.StartupPath + @"\Static\Weapons.xml") )
			{
				MessageBox.Show("Cannot find the Weapons.xml file, cannot do anything");
				return;
			}

			FileStream f = new FileStream(Application.StartupPath + @"\Static\Weapons.xml", FileMode.Open);

			XmlSerializer xs = new XmlSerializer(typeof(WeaponDataDB.WeaponDataSourceCollection));

			WeaponData = (WeaponDataSourceCollection)xs.Deserialize(f);

			f.Close();
		}


		private void PrepareComboBoxes(WeaponDataSourceCollection Weapons)
		{
			PrepareTypeCombo(Weapons);
			PrepareClassCombo(Weapons);
		}

		private void PrepareTypeCombo(WeaponDataSourceCollection FilteredWeapons)
		{
			bool ItemSelected = false;
			string PreviouslySelectedItem = "";

			if ( cbTypes.Items.Count > 0 && cbTypes.SelectedIndex != 0 )
			{
				ItemSelected = true;
				PreviouslySelectedItem = cbTypes.SelectedItem.ToString();
			}

			this.cbTypes.SelectedIndexChanged -= new EventHandler(cbTypes_SelectedIndexChanged);

			cbTypes.Items.Clear();
			cbTypes.Items.Add("--");

			WeaponType[] wTypes = FilteredWeapons.GetUniqueWeaponTypes();

			int newItemID;
			int selectThisItem = -1;

			foreach ( WeaponType wt in wTypes )
			{
				newItemID = cbTypes.Items.Add(WeaponDataDB.Utils.GetWeaponTypeObj(wt));

				if ( doTypeFilter )
				{
					if ( wt == SearchType )
						selectThisItem = newItemID;
				}
			}

			if ( ItemSelected )
			{
				for ( int i = 0; i < cbTypes.Items.Count; i++ )
				{
					if ( cbTypes.Items[i].ToString() == PreviouslySelectedItem )
					{
						cbTypes.SelectedIndex = i;
						break;
					}
				}
			}
			else
				cbTypes.SelectedIndex = 0;

			this.cbTypes.SelectedIndexChanged += new EventHandler(cbTypes_SelectedIndexChanged);

//			if ( doTypeFilter && selectThisItem != -1 )
//				cbTypes.SelectedIndex = selectThisItem;
		}

		private void PrepareTypeCombo()
		{
			PrepareTypeCombo(WeaponData);
		}


		private void PrepareClassCombo(WeaponDataSourceCollection FilteredWeapons)
		{
			bool ItemSelected = false;
			string PreviouslySelectedItem = "";

			if ( cbClass.Items.Count > 0 && cbClass.SelectedIndex != 0 )
			{
				ItemSelected = true;
				PreviouslySelectedItem = cbClass.SelectedItem.ToString();
			}

			this.cbClass.SelectedIndexChanged -= new EventHandler(cbTypes_SelectedIndexChanged);

			cbClass.Items.Clear();
			cbClass.Items.Add("--");

			WeaponClass[] Classes = FilteredWeapons.GetUniqueWeaponClasses();
			foreach ( WeaponClass wc in Classes )
				cbClass.Items.Add(WeaponDataDB.Utils.GetWeaponClassObj(wc));

			if ( ItemSelected )
			{
				for ( int i = 0; i < cbClass.Items.Count; i++ )
				{

					if ( cbClass.Items[i].ToString() == PreviouslySelectedItem )
					{
						cbClass.SelectedIndex = i;
						break;
					}
				}
			}
			else
				cbClass.SelectedIndex = 0;

			this.cbClass.SelectedIndexChanged += new EventHandler(cbTypes_SelectedIndexChanged);		
		}

		private void PrepareClassCombo()
		{
			PrepareClassCombo(WeaponData);
		}
		
		private void WeaponForm_Load(object sender, System.EventArgs e)
		{
			if ( WeaponData == null )
			{
				MessageBox.Show("No weapon data! I can't do anything.", "No weapon data", MessageBoxButtons.OK);
				return;
			}

			statusBarPanel2.Text = WeaponData.Count.ToString() + " items loaded...";

			PrepareComboBoxes(WeaponData);

			grid1.MouseUp += new MouseEventHandler(grid1_MouseUp);
			menuItemSaveAs.Enabled = false;
			menuItemViewAsHTML.Enabled = false;

			if ( doTypeFilter || doClassFilter || doNameFilter )
				DoCallerFilter();
		}

		private void SelectUIWeaponType(WeaponType wepType)
		{
			this.cbTypes.SelectedIndexChanged -= new EventHandler(cbTypes_SelectedIndexChanged);

			for (int i = 1; i < cbTypes.Items.Count; i++ )
			{
				object o = cbTypes.Items[i];

				Debug.WriteLine("Setting type: " + i.ToString() + " " + o.ToString());
				WeaponDataDB.WeaponTypeObj t = (WeaponDataDB.WeaponTypeObj)o;

				if ( t.Type == wepType )
				{
					cbTypes.SelectedIndex = i;
					break;
				}
			}

			this.cbTypes.SelectedIndexChanged += new EventHandler(cbTypes_SelectedIndexChanged);
		}

		private void SelectUIWeaponClass(WeaponClass wepClass)
		{
			this.cbClass.SelectedIndexChanged -= new EventHandler(cbTypes_SelectedIndexChanged);

			for (int i = 1; i < cbClass.Items.Count; i++ )
			{
				object o = cbClass.Items[i];

				WeaponDataDB.WeaponClassObj t = (WeaponDataDB.WeaponClassObj)o;

				if ( t.Class == wepClass )
				{
					cbClass.SelectedIndex = i;
					break;
				}
			}

			this.cbClass.SelectedIndexChanged += new EventHandler(cbTypes_SelectedIndexChanged);
		}

		private void DoCallerFilter()
		{
            WeaponDataSourceCollection Filtered = WeaponData;

			if ( doNameFilter )
			{
				Filtered = Filtered.FilterByNameExact(SearchWeaponName);
				tbSearchText.Text = SearchWeaponName;
			}

			Debug.WriteLine("DoCallerFilter: " + Filtered.Count + " weapon items");

			if ( doTypeFilter )
			{
				Filtered = Filtered.FilterByType(SearchType);
				SelectUIWeaponType(SearchType);
			}

			Debug.WriteLine("Type filter applied: (" + SearchType + ") " + Filtered.Count);

			if ( doClassFilter )
			{
				Filtered = Filtered.FilterByClass(SearchClass);
				SelectUIWeaponClass(SearchClass);
			}

			Debug.WriteLine("Class filter applied: (" + SearchClass.ToString() + ") " + Filtered.Count);

			Debug.WriteLine("Name filter applied: (" + SearchWeaponName + ") " + Filtered.Count);

			if ( Filtered.Count > 0 )
				DrawWeaponListGrid(Filtered);

			tbSearchText.Focus();
		}

		private void DrawItemInfo(WeaponDataSource w)
		{
			StringBuilder s = new StringBuilder();

			s.Append(w.Name + Environment.NewLine + Environment.NewLine);
			s.Append(w.Description + Environment.NewLine + Environment.NewLine);
			s.Append("Type: " + w.Type.ToString() + Environment.NewLine + Environment.NewLine);
			s.Append("Class: " + w.Class.ToString() + Environment.NewLine + Environment.NewLine);

			foreach ( ObjectExplorer.ItemAttribute a in w.Attributes )
				s.Append("[ " + a.GroupName + " ] " + a.Name + ": " + a.Value + Environment.NewLine);

			Debug.WriteLine(s.ToString());

		}

		private WeaponDataSourceCollection GetFilteredWeaponList()
		{
			WeaponDataSourceCollection FilteredWeapons = WeaponData;

			if ( cbTypes.SelectedIndex != 0 )
				FilteredWeapons = FilteredWeapons.FilterByType(((WeaponDataDB.WeaponTypeObj)cbTypes.SelectedItem).Type);

			if ( cbClass.SelectedIndex != 0 )
				FilteredWeapons = FilteredWeapons.FilterByClass(((WeaponDataDB.WeaponClassObj)cbClass.SelectedItem).Class);

			if ( tbSearchText.Text.Length != 0 )
				FilteredWeapons = FilteredWeapons.FilterByName(tbSearchText.Text.ToLower());

			if ( FilteredWeapons.Count != 0 )
				PrepareComboBoxes(FilteredWeapons);

			if ( FilteredWeapons.Count > 0 )
                statusBarPanel1.Text = "Found " + FilteredWeapons.Count.ToString() + " " + (string)(FilteredWeapons.Count==1?"entry":"entries");
			else
				statusBarPanel1.Text = "No matching entries found";

			return FilteredWeapons;
		}


		private void DrawWeaponListView(WeaponDataSourceCollection FilteredWeapons)
		{
			if ( FilteredWeapons == null || FilteredWeapons.Count == 0 )
				return;

			WeaponDataSource Weapon = FilteredWeapons[0]; // Just select the first entry

			// PrepareComboBoxes(FilteredWeapons);

			DrawWeaponListGrid(FilteredWeapons);
		}

		private void CreateGridHeaders(WeaponDataSource Weapon, int StartColumnNumber)
		{
			string colName = "";

			foreach ( ObjectExplorer.ItemAttribute ia in Weapon.Attributes )
			{
				try
				{
					colName = ia.Name;

					if ( MyConfig.WeaponBrowserColumnDisplayPreferences[ia.Name] == null )
					{
						Config.WBColumns wbc = new Config.WBColumns();
						wbc.AttributeName = ia.Name;
						wbc.ShowAttribute = true;

						MyConfig.WeaponBrowserColumnDisplayPreferences.Add(wbc);
					}

					if ( MyConfig.WeaponBrowserColumnDisplayPreferences[ia.Name].ShowAttribute )
					{
						
						grid1[0, StartColumnNumber] = new Cells.ColumnHeader(ia.Name.Replace(" ", Environment.NewLine));
						grid1.Columns[StartColumnNumber].AutoSizeMode = SourceGrid2.AutoSizeMode.EnableAutoSize;
						grid1.Columns[StartColumnNumber].Tag = ia;

						StartColumnNumber++;
					}
				}
				catch (Exception Ex)
				{
					Debug.WriteLine("Problem creating column: " + Ex.ToString());
				}
			}
		}

		private SourceGrid2.VisualModels.Common m_VisualProperties;
		private SourceGrid2.VisualModels.Common m_VisualPropertiesString;
		private SourceGrid2.VisualModels.Common m_VisualPropertiesPrice;
		private SourceGrid2.VisualModels.Common m_VisualPropertiesCheckBox;
		private SourceGrid2.VisualModels.Common m_VisualPropertiesLink;
		private SourceGrid2.VisualModels.Common m_VisualPropertiesNumber;

		private SourceGrid2.DataModels.IDataModel dm_Double = SourceGrid2.Utility.CreateDataModel(typeof(double));
		private SourceGrid2.DataModels.IDataModel dm_String = SourceGrid2.Utility.CreateDataModel(typeof(string));
		private SourceGrid2.DataModels.IDataModel dm_Int	= SourceGrid2.Utility.CreateDataModel(typeof(int));

		private int GetVisibleColumnCount(WeaponDataSource ws)
		{
			int count = 0;

			if ( MyConfig == null )
				Debug.WriteLine("MyConfig is null!!");

			foreach ( ObjectExplorer.ItemAttribute ia in ws.Attributes )
			{
				if ( MyConfig.WeaponBrowserColumnDisplayPreferences[ia.Name] == null )
				{
					Config.WBColumns wbc = new Config.WBColumns();
					wbc.AttributeName = ia.Name;
					wbc.ShowAttribute = true;
					MyConfig.WeaponBrowserColumnDisplayPreferences.Add(wbc);
				}

				if ( MyConfig.WeaponBrowserColumnDisplayPreferences[ia.Name].ShowAttribute )
					count++;
			}

			return count + 2;
		}

		private void HideGridColumn(string ColumnHeadingText)
		{
			for ( int i = 2; i < grid1.ColumnsCount; i++ )
			{
				if ( grid1[0,i].DisplayText == ColumnHeadingText )
				{
					Debug.WriteLine("Remove column index " + i.ToString() + " (" + grid1[0,i].DisplayText + ")");
					grid1.Columns.Remove(i);
				}
			}

			grid1.Refresh();
		}

		private void RedrawGrid()
		{
			this.Cursor = Cursors.WaitCursor;
			WeaponDataSourceCollection list = (WeaponDataSourceCollection)grid1.Tag;
			DrawWeaponListGrid(list);
			this.Cursor = Cursors.Default;
		}

		private void DrawWeaponListGrid(WeaponDataSourceCollection FilteredWeapons)
		{
			if ( grid1.RowsCount > 0 )
				grid1.Rows.RemoveRange(0,grid1.RowsCount);

			if ( grid1.ColumnsCount > 0 )
				grid1.Columns.RemoveRange(0,grid1.ColumnsCount);

			grid1.Tag = FilteredWeapons;

			grid1.RowsCount = 1;
			grid1.ColumnsCount = GetVisibleColumnCount(FilteredWeapons[0]); // FilteredWeapons[0].Attributes.Count + 2; // 1 Col per attribute + the Name column
			grid1.FixedRows = 1;
			grid1.FixedColumns = 2;
			grid1.Selection.SelectionMode = SourceGrid2.GridSelectionMode.Row;
			grid1.Columns[0].AutoSizeMode = SourceGrid2.AutoSizeMode.EnableAutoSize;

			#region Create Header Row and Editor
			Cells.Header l_00Header = new Cells.Header();
			grid1[0,0] = l_00Header;

			grid1[0,1] = new Cells.ColumnHeader("Name");
			grid1[0,1].VisualModel.TextAlignment = SourceLibrary.Drawing.ContentAlignment.BottomLeft;
			CreateGridHeaders(FilteredWeapons[0], 2);
			#endregion

			#region Visual Properties
			//set Cells style
			m_VisualProperties = new SourceGrid2.VisualModels.Common(false);

			m_VisualPropertiesString = (SourceGrid2.VisualModels.Common)m_VisualProperties.Clone(false);
			m_VisualPropertiesString.TextAlignment = SourceLibrary.Drawing.ContentAlignment.MiddleLeft;

			m_VisualPropertiesPrice = (SourceGrid2.VisualModels.Common)m_VisualProperties.Clone(false);
			m_VisualPropertiesPrice.TextAlignment = SourceLibrary.Drawing.ContentAlignment.MiddleRight;

			m_VisualPropertiesCheckBox = (SourceGrid2.VisualModels.CheckBox)SourceGrid2.VisualModels.CheckBox.Default.Clone(false);

			m_VisualPropertiesNumber = (SourceGrid2.VisualModels.Common)m_VisualProperties.Clone(false);
			m_VisualPropertiesNumber.TextAlignment = SourceLibrary.Drawing.ContentAlignment.MiddleRight;

			m_VisualPropertiesLink = (SourceGrid2.VisualModels.Common)SourceGrid2.VisualModels.Common.LinkStyle.Clone(false);
			#endregion

			grid1.RowsCount = FilteredWeapons.Count + 1;
			int l_RowsCount = 1;
			string ColName;
			ObjectExplorer.ItemAttribute Attr;
			string AttrValue;

			//
			// Custom formatter to convert -1 vals to "N/A"
			//
			SourceGrid2.DataModels.EditorTextBoxNumeric doubleValueFormatter = new SourceGrid2.DataModels.EditorTextBoxNumeric(typeof(double));
			doubleValueFormatter.ConvertingValueToDisplayString += new SourceLibrary.ComponentModel.ConvertingObjectEventHandler(doubleValue_ConvertingValueToDisplayString);

			//
			// Customer string formatter to convert nulls to N/A
			//
			SourceGrid2.DataModels.EditorTextBox stringValueFormatter = new SourceGrid2.DataModels.EditorTextBox(typeof(string));
			stringValueFormatter.ConvertingObjectToValue += new SourceLibrary.ComponentModel.ConvertingObjectEventHandler(stringValueFormatter_ConvertingObjectToValue);

			EVEAttributeTypeHelper.EVEItemType AttrType = new EVEAttributeTypeHelper.EVEItemType();

			foreach ( WeaponDataSource w in FilteredWeapons )
			{
				#region Populate RowsCount
				grid1[l_RowsCount,0] = new Cells.RowHeader();
				grid1[l_RowsCount,1] = new Cells.Cell(w.Name);
				grid1[l_RowsCount,1].VisualModel = m_VisualProperties;

				for ( int ColCount = 2; ColCount < grid1.ColumnsCount; ColCount++ )
				{
					ColName = grid1[0, ColCount].GetDisplayText(new SourceGrid2.Position(0,ColCount)).Replace(Environment.NewLine, " ");

					if ( MyConfig.WeaponBrowserColumnDisplayPreferences[ColName].ShowAttribute )
					{
						Attr = w.Attributes[ColName];

						if ( Attr != null )
						{
							// We have an attribute matching the column header name
							AttrValue = Attr.Value;
							AttrType = EVEAttributeTypeHelper.TypeHelper.GetTypeObject(Attr.Name, Attr.Value);

						}
						else
						{
							//
							// The object does not have an attribute corresponding to the column header name
							//
							AttrType = EVEAttributeTypeHelper.TypeHelper.EmptyAttribute(EVEAttributeTypeHelper.TypeHelper.GetAttrType(ColName));
						}

						grid1[l_RowsCount,ColCount] = new Cells.Cell();

						// Set Cell type
						switch ( AttrType.Type )
						{
							case EVEAttributeTypeHelper.ValueType.Double:
								grid1[l_RowsCount,ColCount] = new Cells.Cell();
								grid1[l_RowsCount,ColCount].VisualModel = m_VisualPropertiesNumber;
								//grid1[l_RowsCount,ColCount].DataModel = dm_Double;
								grid1[l_RowsCount,ColCount].Value = AttrType.DoubleValue;
								// grid1[l_RowsCount,ColCount].dis = AttrType.DisplayString;


								grid1[l_RowsCount, ColCount].DataModel = doubleValueFormatter;

								break;

							case EVEAttributeTypeHelper.ValueType.String:
								grid1[l_RowsCount,ColCount] = new Cells.Cell();
								grid1[l_RowsCount,ColCount].VisualModel = m_VisualPropertiesString;
								grid1[l_RowsCount,ColCount].DataModel = stringValueFormatter;
								grid1[l_RowsCount,ColCount].Value = AttrType.StringValue;
								break;

							case EVEAttributeTypeHelper.ValueType.URL:
								grid1[l_RowsCount,ColCount] = new Cells.Link(AttrType.DisplayString);
								grid1[l_RowsCount,ColCount].VisualModel = m_VisualPropertiesLink;
								grid1[l_RowsCount,ColCount].Tag = Attr;
								((Cells.Link)grid1[l_RowsCount,ColCount]).Click += new SourceGrid2.PositionEventHandler(CellLink_Click);

								break;

							default:
								grid1[l_RowsCount,ColCount].VisualModel = m_VisualProperties;
								grid1[l_RowsCount,ColCount].DataModel = stringValueFormatter;
								grid1[l_RowsCount,ColCount].Value = AttrType.StringValue;
								break;
						}
						grid1[l_RowsCount,ColCount].Tag = AttrType;

						if ( grid1[l_RowsCount, ColCount].DataModel != null )
							grid1[l_RowsCount,ColCount].DataModel.EnableEdit = false;

					}

					#endregion
				}
				grid1.Rows[l_RowsCount].Tag = w;
				l_RowsCount++;
			}

			grid1.AutoSize();
			grid1.DoubleClick += new EventHandler(grid1_DoubleClick);
		}

		private void CellLink_Click(object sender, SourceGrid2.PositionEventArgs e)
		{
			try
			{
				Cells.Link LinkCell = (Cells.Link)sender;
				EVEAttributeTypeHelper.EVEItemType Attr = (EVEAttributeTypeHelper.EVEItemType)LinkCell.Tag;
				Process.Start(Attr.URLHref);
			}
			catch(Exception)
			{
			}
		}

		private void DrawWeaponList(bool FromSearch)
		{
			WeaponDataSourceCollection FilteredWeapons = GetFilteredWeaponList();

			if ( FilteredWeapons.Count == 0 )
			{
				if ( FromSearch )
				{
					// statusBarPanel1.Text = "No results found";
					return;
				}
				else
				{
					ClearGrid();
					menuItemSaveAs.Enabled = false;
					menuItemViewAsHTML.Enabled = false;
					return;
				}
			}
			else
			{
//				if ( FromSearch )
//					statusBarPanel1.Text = "Found " + FilteredWeapons.Count.ToString() + " matching entries.";
				menuItemSaveAs.Enabled = true;
				menuItemViewAsHTML.Enabled = true;
			}

			DrawWeaponListView(FilteredWeapons);
			DrawColumnSelect();
		}

		private void DrawColumnSelect()
		{

		}

		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			if ( tbSearchText.Text.Length == 0 )
				return;

			DrawWeaponList(true);
			// DoTextSearch(tbSearchText.Text);
		}

		private void cbTypes_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			// sender is the appropriate combobox
			if ( cbTypes.SelectedIndex == 0 && cbClass.SelectedIndex == 0 && tbSearchText.Text.Length == 0 )
			{
				ClearGrid();
				return;
			}

			this.Cursor = Cursors.WaitCursor;
			DrawWeaponList(false);
			this.Cursor = Cursors.Default;

		}

		private void grid1_DoubleClick(object sender, EventArgs e)
		{
			if ( grid1.Selection.SelectedRows.Length == 1 )
			{
				Debug.WriteLine("Single row double clicked");
				WeaponDataDB.WeaponDataSource w = (WeaponDataSource)grid1.Selection.SelectedRows[0].Tag;

				Debug.WriteLine("Item: " + w.Name);

				Process.Start(@"http://www.eve-i.com/home/crowley/page/page_ptype.php?id=" + w.ItemID.ToString());
			}
		}

		private void ClearWeaponListGrid()
		{
			if ( grid1.RowsCount > 0 )
				grid1.Rows.RemoveRange(0,grid1.RowsCount);

			if ( grid1.ColumnsCount > 0 )
				grid1.Columns.RemoveRange(0,grid1.ColumnsCount);
		}

		private void ClearGrid()
		{
			this.cbClass.SelectedIndexChanged -= new EventHandler(cbTypes_SelectedIndexChanged);		
			this.cbTypes.SelectedIndexChanged -= new EventHandler(cbTypes_SelectedIndexChanged);

			cbClass.SelectedIndex = 0;
			cbTypes.SelectedIndex = 0;

			this.cbClass.SelectedIndexChanged += new EventHandler(cbTypes_SelectedIndexChanged);		
			this.cbTypes.SelectedIndexChanged += new EventHandler(cbTypes_SelectedIndexChanged);

			PrepareTypeCombo(WeaponData);
			PrepareClassCombo(WeaponData);

			tbSearchText.Text = "";
			menuItemSaveAs.Enabled = false;
			menuItemViewAsHTML.Enabled = false;
			ClearWeaponListGrid();
		}

		private void btnClear_Click(object sender, System.EventArgs e)
		{
			ClearGrid();
			statusBarPanel1.Text = "";
		}

		private void doubleValue_ConvertingValueToDisplayString(object sender, SourceLibrary.ComponentModel.ConvertingObjectEventArgs e)
		{
			if ( e.Value is double )
			{
				double val = (double)e.Value;

				// Debug.WriteLine("ConvertingValueToDisplayString invoked with value: " + val.ToString());

				if ( val == -1 )
				{
					e.Value = "N/A";
					e.ConvertingStatus = SourceLibrary.ComponentModel.ConvertingStatus.Completed;
				}
			}
		}

		private void stringValueFormatter_ConvertingObjectToValue(object sender, SourceLibrary.ComponentModel.ConvertingObjectEventArgs e)
		{
			if ( e.Value is string )
			{
				string s = e.Value.ToString();

				if ( s.Length == 0 )
				{
					e.Value = "N/A";
					e.ConvertingStatus = SourceLibrary.ComponentModel.ConvertingStatus.Completed;
				}
			}

		}

		private void tbSearchText_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if ( e.KeyChar == '\r' )
			{
				if ( tbSearchText.Text.Length > 0 )
				{
					this.Cursor = Cursors.WaitCursor;
					DrawWeaponList(true);
					this.Cursor = Cursors.Default;
				}
			}
		}

		private void SaveGridAsCSV(string FileName)
		{
			StringBuilder Row = new StringBuilder();
			StringBuilder FileText = new StringBuilder();

			// Get header

			for ( int RowCount = 0; RowCount < grid1.RowsCount; RowCount++ )
			{
				Row = new StringBuilder();

				for ( int ColCount = 1; ColCount < grid1.ColumnsCount; ColCount++ )
				{
					if ( Row.Length > 0 )
						Row.Append(",");

					if ( RowCount == 0 )
						Row.Append(grid1[RowCount, ColCount].DisplayText.Replace(Environment.NewLine, " "));
					else			
						Row.Append(grid1[RowCount,ColCount].Value.ToString());
				}

				Row.Append(Environment.NewLine);
				FileText.Append(Row.ToString());
			}
			
			WriteTextToFile(FileName, FileText.ToString());
		}

		private void WriteTextToFile(string FileName, string Text)
		{
			try
			{
				StreamWriter sr = File.CreateText(FileName);

				sr.Write(Text);
				sr.Close();
			}
			catch (Exception e)
			{
				MessageBox.Show("There was a problem saving the file to disk: " + e.Message, "Error saving file", MessageBoxButtons.OK);
				return;
			}
		}

		private void SaveGridAsHTML(string PathName)
		{
			try
			{
				using (System.IO.FileStream l_Stream = new System.IO.FileStream(PathName,System.IO.FileMode.Create,System.IO.FileAccess.Write))
				{
					grid1.ExportHTML(new SourceGrid2.HTMLExport(SourceGrid2.ExportHTMLMode.HTMLAndBody, System.IO.Path.GetFullPath(PathName), "", l_Stream));
					l_Stream.Close();
				}
			}
			catch(Exception err)
			{
				SourceLibrary.Windows.Forms.ErrorDialog.Show(this,err,"HTML Export Error");
			}
		}
//		private void grid1_MouseUp(object sender, MouseEventArgs e)
//		{
//			Debug.WriteLine("Cell position = " + grid1.MouseCellPosition.Column + ", " + grid1.MouseCellPosition.Row);
//
//			if ( e.Button == MouseButtons.Right && grid1.MouseCellPosition.Row == 0 )
//				contextMenu1.Show(grid1, new Point(e.X, e.Y));
//		}

		private void contextMenu1_Popup(object sender, System.EventArgs e)
		{
			Debug.WriteLine("context menu popup");
			// ShowColumnChooser();
		}

		private void menuItem1_Click(object sender, System.EventArgs e)
		{
			Debug.WriteLine("Menu Item Click");

			MenuItem mi = (MenuItem)sender;

			if ( mi.Checked )
			{
				mi.Checked = false;
				MyConfig.WeaponBrowserColumnDisplayPreferences[mi.Text].ShowAttribute = false;
			}
			else
			{
				mi.Checked = true;
				MyConfig.WeaponBrowserColumnDisplayPreferences[mi.Text].ShowAttribute = true;
			}

			RedrawGrid();
		}

		private void CreateColumnChooserMenu()
		{
			WeaponDataSource wds = (WeaponDataSource)grid1.Rows[1].Tag;

			string[] cols = new string[wds.Attributes.Count];

			contextMenu1.MenuItems.Clear();

			MenuItem SelectAllMenuItem;
			MenuItem SelectNoneMenuItem;

			SelectAllMenuItem = new MenuItem("Select All");
			SelectAllMenuItem.Click += new EventHandler(SelectAllMenuItem_Click);

			SelectNoneMenuItem = new MenuItem("Select None");
			SelectNoneMenuItem.Click +=new EventHandler(SelectNoneMenuItem_Click);

			contextMenu1.MenuItems.Add(0, SelectAllMenuItem);
			contextMenu1.MenuItems.Add(1, SelectNoneMenuItem);
			contextMenu1.MenuItems.Add(2, new MenuItem("-"));

			MenuItem NameMenuItem = new MenuItem("Name");
			NameMenuItem.Enabled = false;
			contextMenu1.MenuItems.Add(3,NameMenuItem);

			for ( int i = 0; i < wds.Attributes.Count; i++)
			{
				MenuItem mi = new MenuItem(wds.Attributes[i].Name);
				mi.Click += new EventHandler(menuItem1_Click);

				contextMenu1.MenuItems.Add(i+4, mi);

				cols[i] = wds.Attributes[i].Name;
			}

			foreach ( MenuItem mi in contextMenu1.MenuItems )
				if ( MyConfig.WeaponBrowserColumnDisplayPreferences[mi.Text] != null )
					if ( MyConfig.WeaponBrowserColumnDisplayPreferences[mi.Text].ShowAttribute )
						mi.Checked = true;
					else
						mi.Checked = false;
		}

		private void grid1_MouseUp(object sender, MouseEventArgs e)
		{
			if ( e.Button == MouseButtons.Right )
			{
				if ( grid1.MouseCellPosition.Row == 0 )
				{
					CreateColumnChooserMenu();
					contextMenu1.Show(grid1, new Point(e.X, e.Y));
				}
			}
		}

		private void SelectAllMenuItem_Click(object sender, EventArgs e)
		{
			Debug.WriteLine("Select All");
			for ( int i = 4; i < contextMenu1.MenuItems.Count; i++ )
			{
				contextMenu1.MenuItems[i].Checked = true;
				if ( MyConfig.WeaponBrowserColumnDisplayPreferences[contextMenu1.MenuItems[i].Text] != null )
					MyConfig.WeaponBrowserColumnDisplayPreferences[contextMenu1.MenuItems[i].Text].ShowAttribute = true;
			}

			RedrawGrid();
		}

		private void SelectNoneMenuItem_Click(object sender, EventArgs e)
		{
			for ( int i = 4; i < contextMenu1.MenuItems.Count; i++ )
			{
				contextMenu1.MenuItems[i].Checked = false;
				MyConfig.WeaponBrowserColumnDisplayPreferences[contextMenu1.MenuItems[i].Text].ShowAttribute = false;
			}

			RedrawGrid();
		}

		private void menuItemClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void menuItemSaveAs_Click(object sender, System.EventArgs e)
		{
			DialogResult r = saveFileDialog1.ShowDialog();
            
			if ( r == DialogResult.OK )
			{
				Debug.WriteLine("Filesave index = " + saveFileDialog1.FilterIndex.ToString());

				try
				{
					switch ( saveFileDialog1.FilterIndex )
					{
						case 1:
							Debug.WriteLine("Saving as html");
							SaveGridAsHTML(saveFileDialog1.FileName);
							break;

						case 2:
							Debug.WriteLine("Saving as CSV");
							SaveGridAsCSV(saveFileDialog1.FileName);
							break;
					}
				}
				catch (Exception err)
				{
					MessageBox.Show("There was a problem saving the file. Please try again. " + err.Message, "Error", MessageBoxButtons.OK);
				}
			}

		}

		private void menuItemViewAsHTML_Click(object sender, System.EventArgs e)
		{
			string tmpPath;

			tmpPath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "gridExport.htm");
			Debug.WriteLine("tmpPath = " + tmpPath);

			SaveGridAsHTML(tmpPath);
			SourceLibrary.Utility.Shell.OpenFile(tmpPath);
		}
	}
}
