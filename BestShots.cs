using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Text;
using System.IO;
using System.Diagnostics;
using CombatLog.BestShotReport;
using System.Globalization;

namespace CombatLog
{
	/// <summary>
	/// Summary description for BestShots.
	/// </summary>
	public class BestShots : System.Windows.Forms.Form
	{

		public delegate void CombatLogActivatedEvent(object Sender, GameLog GameLogObj);
		public event CombatLogActivatedEvent CombatLogActivated = null;

		public GameLogCollection AllGameLogs;
		public WeaponDataDB.WeaponDataSourceCollection EOWeaponData;

		private BestShotListViewSorter LVSorter = new BestShotListViewSorter();
		private ArrayList WeaponData = null;
		private System.Windows.Forms.ListView listViewBestShot;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.Panel panelTop;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox comboBoxCharacter;
		private System.Windows.Forms.ComboBox comboBoxLogDir;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ColumnHeader columnHeader7;
		private System.Windows.Forms.RichTextBox richTextBox1;
		private System.Windows.Forms.Panel panelBottom;
		private System.Windows.Forms.ContextMenu contextMenuMain;
		private System.Windows.Forms.MenuItem menuItemCopy;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Panel panelMid;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.ComboBox cbWeaponType;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.ComboBox cbWeaponClass;
		private System.Windows.Forms.Label label8;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		// TODO: Enable free text filtering on weapon name

		public BestShots()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(BestShots));
			this.listViewBestShot = new System.Windows.Forms.ListView();
			this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
			this.contextMenuMain = new System.Windows.Forms.ContextMenu();
			this.menuItemCopy = new System.Windows.Forms.MenuItem();
			this.panelTop = new System.Windows.Forms.Panel();
			this.label1 = new System.Windows.Forms.Label();
			this.comboBoxCharacter = new System.Windows.Forms.ComboBox();
			this.comboBoxLogDir = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.panelBottom = new System.Windows.Forms.Panel();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.panelMid = new System.Windows.Forms.Panel();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.cbWeaponType = new System.Windows.Forms.ComboBox();
			this.label7 = new System.Windows.Forms.Label();
			this.cbWeaponClass = new System.Windows.Forms.ComboBox();
			this.label8 = new System.Windows.Forms.Label();
			this.panelTop.SuspendLayout();
			this.panelBottom.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.panelMid.SuspendLayout();
			this.SuspendLayout();
			// 
			// listViewBestShot
			// 
			this.listViewBestShot.AllowColumnReorder = true;
			this.listViewBestShot.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																							   this.columnHeader6,
																							   this.columnHeader1,
																							   this.columnHeader7,
																							   this.columnHeader2,
																							   this.columnHeader3,
																							   this.columnHeader4,
																							   this.columnHeader5});
			this.listViewBestShot.ContextMenu = this.contextMenuMain;
			this.listViewBestShot.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listViewBestShot.FullRowSelect = true;
			this.listViewBestShot.HideSelection = false;
			this.listViewBestShot.Location = new System.Drawing.Point(0, 0);
			this.listViewBestShot.Name = "listViewBestShot";
			this.listViewBestShot.Size = new System.Drawing.Size(760, 246);
			this.listViewBestShot.TabIndex = 1;
			this.listViewBestShot.View = System.Windows.Forms.View.Details;
			this.listViewBestShot.ItemActivate += new System.EventHandler(this.listViewBestShot_ItemActivate);
			this.listViewBestShot.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listViewBestShot_ColumnClick);
			this.listViewBestShot.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.listViewBestShot_ItemDrag);
			this.listViewBestShot.SelectedIndexChanged += new System.EventHandler(this.listViewBestShot_SelectedIndexChanged);
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "When";
			this.columnHeader6.Width = 117;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Weapon";
			this.columnHeader1.Width = 195;
			// 
			// columnHeader7
			// 
			this.columnHeader7.Text = "Hit Type";
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Damage";
			this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.columnHeader2.Width = 74;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Target";
			this.columnHeader3.Width = 141;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Character";
			this.columnHeader4.Width = 71;
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "Log Dir";
			this.columnHeader5.Width = 96;
			// 
			// contextMenuMain
			// 
			this.contextMenuMain.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																							this.menuItemCopy});
			// 
			// menuItemCopy
			// 
			this.menuItemCopy.Index = 0;
			this.menuItemCopy.Text = "Copy Log Text";
			this.menuItemCopy.Click += new System.EventHandler(this.menuItemCopy_Click);
			// 
			// panelTop
			// 
			this.panelTop.Controls.Add(this.label1);
			this.panelTop.Controls.Add(this.comboBoxCharacter);
			this.panelTop.Controls.Add(this.comboBoxLogDir);
			this.panelTop.Controls.Add(this.label2);
			this.panelTop.Controls.Add(this.cbWeaponType);
			this.panelTop.Controls.Add(this.label7);
			this.panelTop.Controls.Add(this.cbWeaponClass);
			this.panelTop.Controls.Add(this.label8);
			this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelTop.Location = new System.Drawing.Point(0, 0);
			this.panelTop.Name = "panelTop";
			this.panelTop.Size = new System.Drawing.Size(760, 72);
			this.panelTop.TabIndex = 3;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(72, 23);
			this.label1.TabIndex = 2;
			this.label1.Text = "Character";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// comboBoxCharacter
			// 
			this.comboBoxCharacter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxCharacter.Items.AddRange(new object[] {
																   "All",
																   "Hurg"});
			this.comboBoxCharacter.Location = new System.Drawing.Point(80, 9);
			this.comboBoxCharacter.Name = "comboBoxCharacter";
			this.comboBoxCharacter.Size = new System.Drawing.Size(152, 21);
			this.comboBoxCharacter.Sorted = true;
			this.comboBoxCharacter.TabIndex = 1;
			// 
			// comboBoxLogDir
			// 
			this.comboBoxLogDir.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxLogDir.Items.AddRange(new object[] {
																"All",
																"Bas"});
			this.comboBoxLogDir.Location = new System.Drawing.Point(80, 41);
			this.comboBoxLogDir.Name = "comboBoxLogDir";
			this.comboBoxLogDir.Size = new System.Drawing.Size(152, 21);
			this.comboBoxLogDir.Sorted = true;
			this.comboBoxLogDir.TabIndex = 1;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 40);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(72, 23);
			this.label2.TabIndex = 2;
			this.label2.Text = "Log Dir";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// richTextBox1
			// 
			this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.richTextBox1.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.richTextBox1.Location = new System.Drawing.Point(3, 16);
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.ReadOnly = true;
			this.richTextBox1.Size = new System.Drawing.Size(750, 89);
			this.richTextBox1.TabIndex = 3;
			this.richTextBox1.Text = "";
			// 
			// panelBottom
			// 
			this.panelBottom.Controls.Add(this.groupBox1);
			this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panelBottom.DockPadding.All = 2;
			this.panelBottom.Location = new System.Drawing.Point(0, 318);
			this.panelBottom.Name = "panelBottom";
			this.panelBottom.Size = new System.Drawing.Size(760, 112);
			this.panelBottom.TabIndex = 6;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.richTextBox1);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox1.Location = new System.Drawing.Point(2, 2);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(756, 108);
			this.groupBox1.TabIndex = 4;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Log File Entry";
			// 
			// panelMid
			// 
			this.panelMid.Controls.Add(this.listViewBestShot);
			this.panelMid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelMid.Location = new System.Drawing.Point(0, 72);
			this.panelMid.Name = "panelMid";
			this.panelMid.Size = new System.Drawing.Size(760, 246);
			this.panelMid.TabIndex = 7;
			// 
			// splitter1
			// 
			this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.splitter1.Location = new System.Drawing.Point(0, 315);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(760, 3);
			this.splitter1.TabIndex = 8;
			this.splitter1.TabStop = false;
			// 
			// cbWeaponType
			// 
			this.cbWeaponType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbWeaponType.DropDownWidth = 184;
			this.cbWeaponType.Location = new System.Drawing.Point(328, 9);
			this.cbWeaponType.MaxDropDownItems = 18;
			this.cbWeaponType.Name = "cbWeaponType";
			this.cbWeaponType.Size = new System.Drawing.Size(184, 21);
			this.cbWeaponType.Sorted = true;
			this.cbWeaponType.TabIndex = 10;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(240, 8);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(88, 23);
			this.label7.TabIndex = 12;
			this.label7.Text = "Weapon Type";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// cbWeaponClass
			// 
			this.cbWeaponClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbWeaponClass.DropDownWidth = 184;
			this.cbWeaponClass.Location = new System.Drawing.Point(328, 41);
			this.cbWeaponClass.MaxDropDownItems = 18;
			this.cbWeaponClass.Name = "cbWeaponClass";
			this.cbWeaponClass.Size = new System.Drawing.Size(184, 21);
			this.cbWeaponClass.Sorted = true;
			this.cbWeaponClass.TabIndex = 9;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(240, 40);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(88, 23);
			this.label8.TabIndex = 11;
			this.label8.Text = "Weapon Class";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// BestShots
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(760, 430);
			this.Controls.Add(this.splitter1);
			this.Controls.Add(this.panelMid);
			this.Controls.Add(this.panelBottom);
			this.Controls.Add(this.panelTop);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "BestShots";
			this.ShowInTaskbar = false;
			this.Text = "BestShots";
			this.Load += new System.EventHandler(this.BestShots_Load);
			this.panelTop.ResumeLayout(false);
			this.panelBottom.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.panelMid.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void DoReport()
		{
			PrepareFilters();
			GenerateReportData();
			DrawBestShotList();
		}

		private void BestShots_Load(object sender, System.EventArgs e)
		{
			LVSorter.SortColumn = 3;
			LVSorter.Direction = SortOrder.Descending;
			listViewBestShot.ListViewItemSorter = LVSorter;

			DoReport();

			listViewBestShot.Sort();
		}

		private CombatLogCacheCollection GetFilteredLogs()
		{
			CombatLogCacheCollection FilteredLogs = AllGameLogs.LogCache.CacheData;

			if ( comboBoxCharacter.SelectedIndex != 0 )
			{
				Debug.WriteLine("Filtering by Char: " + comboBoxCharacter.SelectedItem.ToString());
				FilteredLogs = FilteredLogs.FilterByCharacter(comboBoxCharacter.SelectedItem.ToString());
			}

			if ( comboBoxLogDir.SelectedIndex != 0 )
			{
				Debug.WriteLine("Filtering by LogDir: " + comboBoxLogDir.SelectedItem.ToString());
				FilteredLogs = FilteredLogs.FilterByLogDir(comboBoxLogDir.SelectedItem.ToString());
			}

			if ( cbWeaponType.SelectedIndex != 0 )
			{
				Debug.WriteLine("Filtering by weapon type: " + cbWeaponType.SelectedItem.ToString());
				FilteredLogs = FilteredLogs.FilterByWeaponType((WeaponDataDB.WeaponTypeObj)cbWeaponType.SelectedItem);
			}

			if ( cbWeaponClass.SelectedIndex != 0 )
			{
				Debug.WriteLine("Filtering by weapon class: " + cbWeaponClass.SelectedItem.ToString());
				FilteredLogs = FilteredLogs.FilterByWeaponClass((WeaponDataDB.WeaponClassObj)cbWeaponClass.SelectedItem);
			}

			return FilteredLogs;
		}

		private void GenerateReportData()
		{
			GenerateReportData(AllGameLogs.LogCache.CacheData);
		}

		private void GenerateReportData(CombatLogCacheCollection FilteredLogs)
		{
			if ( AllGameLogs == null )
				return;

			Debug.WriteLine("GenerateReportData: " + FilteredLogs.Count + " items");

			try
			{
				// CombatLogCacheCollection Cache = AllGameLogs.LogCache.CacheData;

				// GameLog f = AllGameLogs.con

				if ( FilteredLogs == null )
					return;

				Hashtable Weapons = new Hashtable();

				string Key;
				foreach ( CombatLogCache c in FilteredLogs )
				{
					// Fix for best shots report blowing up
					//
					// Cause: Combat Logs in which the player was attacked but did not return fire
					// WeaponStatsSummary is therefore undefined.
					//
					// Date: 20/12/2004
					//
					if ( c.IsCombatLog && c.WeaponStatsSummary != null )
					{
						foreach ( CombatLog.WeaponSummaryData.WeaponStats ws in c.WeaponStatsSummary )
						{
							Key = ws.WeaponName + "/" + c.Character;

							if ( ws.HigestDamage > 0 )
							{
								if ( !Weapons.ContainsKey(Key) )
								{
									BestShotRecord bsr_ = new BestShotRecord();

									bsr_.Damage = 0;
									bsr_.CacheEntry = new CombatLogCache();

									Weapons.Add(Key, bsr_);
								}

								BestShotRecord bsr = (BestShotRecord)Weapons[Key];

								if ( ws.HigestDamage > bsr.Damage )
								{
									BestShotRecord b	= new BestShotRecord();

									b.ShotDTM			= ws.HighestDamageDTM;
									b.WeaponName		= ws.WeaponName;
									b.Damage			= ws.HigestDamage;
									b.Against			= ws.HighestDamageAgainstTarget;
									b.CacheEntry		= c;
									b.HitType			= ws.HighestDamageHitType;
									b.PositionInFile	= ws.HighestDamagePositionInFile;

									Weapons[Key] = b;
								}
							}
						}
					}
				}

				// Now copy the data into an array list
				WeaponData = new ArrayList();
				foreach ( object w in Weapons.Keys )
					WeaponData.Add(Weapons[w]);
			}
			catch (Exception e)
			{
				MessageBox.Show("Error generating report: " + e.ToString());
				return;
			}
		}

		private void PrepareFilters()
		{
			CombatLogCacheCollection FilteredLogs = AllGameLogs.LogCache.CacheData;

			comboBoxCharacter.SelectedIndexChanged -= new System.EventHandler(this.comboBoxCharacter_SelectedIndexChanged);
			comboBoxLogDir.SelectedIndexChanged -= new System.EventHandler(this.comboBoxLogDir_SelectedIndexChanged);
			cbWeaponType.SelectedIndexChanged -= new EventHandler(cbWeaponType_SelectedIndexChanged);
			cbWeaponClass.SelectedIndexChanged -= new EventHandler(cbWeaponClass_SelectedIndexChanged);

			comboBoxCharacter.Items.Clear();
			comboBoxLogDir.Items.Clear();
			cbWeaponType.Items.Clear();
			cbWeaponClass.Items.Clear();

			comboBoxCharacter.Items.Add("--");
			comboBoxLogDir.Items.Add("--");
			cbWeaponType.Items.Add("--");
			cbWeaponClass.Items.Add("--");

			comboBoxCharacter.Items.AddRange(FilteredLogs.GetUniqueCharacterList());
			comboBoxLogDir.Items.AddRange(FilteredLogs.GetUniqueLogDirs());
			cbWeaponType.Items.AddRange(FilteredLogs.GetUniqueWeaponTypeList(EOWeaponData));
			cbWeaponClass.Items.AddRange(FilteredLogs.GetUniqueWeaponClassList(EOWeaponData));

			Debug.WriteLine(comboBoxCharacter.Items.Count + " items in Character list");
			Debug.WriteLine(comboBoxLogDir.Items.Count + " items in LogDir list");

			comboBoxCharacter.SelectedIndex = 0;
			comboBoxLogDir.SelectedIndex = 0;
			cbWeaponType.SelectedIndex = 0;
			cbWeaponClass.SelectedIndex = 0;

			this.comboBoxCharacter.SelectedIndexChanged += new System.EventHandler(this.comboBoxCharacter_SelectedIndexChanged);
			this.comboBoxLogDir.SelectedIndexChanged += new System.EventHandler(this.comboBoxLogDir_SelectedIndexChanged);
			cbWeaponType.SelectedIndexChanged += new EventHandler(cbWeaponType_SelectedIndexChanged);
			cbWeaponClass.SelectedIndexChanged += new EventHandler(cbWeaponClass_SelectedIndexChanged);
		}

		private void DrawBestShotList()
		{
			listViewBestShot.Items.Clear();

			try
			{
				ListViewItem[] newLines = GetLineItems();

				Debug.WriteLine("GetLineItems returned: "  + newLines.Length + " items");

				listViewBestShot.Items.AddRange(newLines);

				LVSorter.Direction = System.Windows.Forms.SortOrder.Descending;
				LVSorter.SortColumn = 3;
				listViewBestShot.Sort();
			}
			catch (Exception e)
			{
				Debug.WriteLine(e.ToString());
				return;
			}
		}

		private ArrayList GetFilteredList()
		{
			ArrayList Filtered = new ArrayList();
			Filtered.Clear();

			string CharName	= comboBoxCharacter.SelectedItem.ToString();
			string LogDir	= comboBoxLogDir.SelectedItem.ToString();

			WeaponDataDB.WeaponTypeObj WeaponType = null;
			WeaponDataDB.WeaponClassObj WeaponClass = null;

			if ( cbWeaponType.SelectedIndex != 0 )
				WeaponType = (WeaponDataDB.WeaponTypeObj)cbWeaponType.SelectedItem;

			if ( cbWeaponClass.SelectedIndex != 0 )
				WeaponClass = (WeaponDataDB.WeaponClassObj)cbWeaponClass.SelectedItem;

//			Debug.WriteLine("Charname = " + CharName);
//			Debug.WriteLine("LogDir = " + LogDir);

			CombatLogCacheCollection FilteredLogs = GetFilteredLogs();
			GenerateReportData(FilteredLogs);

			bool CharOK = false;
			bool AliasOK = false;
			bool WeaponTypeOK = false;
			bool WeaponClassOK = false;

			foreach ( object w in WeaponData )
			{
				BestShotRecord b = (BestShotRecord)w;

				if ( CharName.Length > 0 && comboBoxCharacter.SelectedIndex != 0 )
				{
					if ( b.CacheEntry.Character == CharName )
						CharOK = true;
				}
				else
					CharOK = true;

				if ( LogDir.Length > 0 && comboBoxLogDir.SelectedIndex != 0 )
				{
					if ( b.CacheEntry.DirAlias == LogDir )
						AliasOK = true;
				}
				else
					AliasOK = true;

				if ( WeaponType != null )
				{
					if ( EOWeaponData[b.WeaponName] != null )
					{
						if ( EOWeaponData[b.WeaponName].Type == WeaponType.Type )
							WeaponTypeOK = true;
					}
				}
				else
					WeaponTypeOK = true;

				if ( WeaponClass != null )
				{
					if ( EOWeaponData[b.WeaponName] != null )
						if ( EOWeaponData[b.WeaponName].Class == WeaponClass.Class )
							WeaponClassOK = true;
						else
							WeaponClassOK = false;
				}
				else
					WeaponClassOK = true;

				if ( CharOK && AliasOK && WeaponTypeOK && WeaponClassOK )
					Filtered.Add(b);

				CharOK = false;
				AliasOK = false;
				WeaponTypeOK = false;
				WeaponClassOK = false;
			}

			return Filtered;
		}

		private ListViewItem[] GetLineItems()
		{
			ArrayList Weapons = GetFilteredList();
			ListViewItem[] items = new ListViewItem[Weapons.Count];

			int count = 0;
			foreach ( object w in Weapons )
			{
				BestShotRecord b = (BestShotRecord)w;

				ListViewItem l;

				try
				{

					l = new ListViewItem(new string[] {b.ShotDTM.ToString(), b.WeaponName, HitTypeLib.GetDisplayString(b.HitType), b.Damage.ToString("0.00"), b.Against, b.CacheEntry.Character, b.CacheEntry.DirAlias});
					l.Tag = b;
				}
				catch (Exception e)
				{
					Debug.WriteLine("BestShotError: " +e.ToString());
					throw e;
				}

				items[count++] = l;
			}

			return items;
		}

		private void listViewBestShot_ColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
		{
			DoSort(e.Column);
		}

		//
		// ListView sorter
		//
		private void DoSort(int ColumnID)
		{
			if ( LVSorter.SortColumn == ColumnID )
			{
				if ( LVSorter.Direction == System.Windows.Forms.SortOrder.Ascending )
					LVSorter.Direction = System.Windows.Forms.SortOrder.Descending;
				else
					LVSorter.Direction = System.Windows.Forms.SortOrder.Ascending;
			}
			else
				LVSorter.Direction = System.Windows.Forms.SortOrder.Ascending;

			LVSorter.SortColumn = ColumnID;

			listViewBestShot.ListViewItemSorter = LVSorter;
			listViewBestShot.Sort();
			
			if ( listViewBestShot.SelectedItems.Count == 1 )
				listViewBestShot.EnsureVisible(listViewBestShot.SelectedItems[0].Index);
		}

		private void listViewBestShot_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if ( listViewBestShot.SelectedItems.Count == 0 )
			{
				richTextBox1.Text = "";
				return;
			}

			richTextBox1.Text = GetLogTextForSelectedItems();
		}

		private void listViewBestShot_ItemActivate(object sender, System.EventArgs e)
		{
			if ( CombatLogActivated != null )
			{
				BestShotRecord bsr = (BestShotRecord)listViewBestShot.SelectedItems[0].Tag;

				if ( AllGameLogs.LogCache.CacheData.Contains(bsr.CacheEntry.FileName) )
				{
					GameLog g = new GameLog();
					g.FileName = bsr.CacheEntry.FileName;
					g.LeafName = Path.GetFileName(g.FileName);
					g.FileSize = bsr.CacheEntry.FileSize;
					g.Listener = bsr.CacheEntry.Character;
					g.PathAlias = bsr.CacheEntry.DirAlias;
					CombatLogActivated(this, g);
				}
			}
		}

		private void listViewBestShot_ItemDrag(object sender, System.Windows.Forms.ItemDragEventArgs e)
		{
			((ListView)sender).DoDragDrop(GetLogTextForSelectedItems(), DragDropEffects.Copy);
		}


		private string GetLogTextForSelectedItems()
		{
			StringBuilder exportText = new StringBuilder();

			foreach ( ListViewItem l in listViewBestShot.SelectedItems )
			{
				BestShotRecord bsr = (BestShotRecord)l.Tag;
				exportText.Append(Utils.LogFile.GetLineFromPosition(bsr.CacheEntry.FileName, bsr.PositionInFile));
			}

			return exportText.ToString();
		}

		private void menuItemCopy_Click(object sender, System.EventArgs e)
		{
			Clipboard.SetDataObject(GetLogTextForSelectedItems(), true);
		}

		private void comboBoxCharacter_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			DrawBestShotList();
		}

		private void comboBoxLogDir_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			DrawBestShotList();
		}

		private void cbWeaponType_SelectedIndexChanged(object sender, EventArgs e)
		{
			DrawBestShotList();
		}

		private void cbWeaponClass_SelectedIndexChanged(object sender, EventArgs e)
		{
			DrawBestShotList();
		}
	}
}
