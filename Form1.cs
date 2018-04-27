using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using NSpring.Logging;
using System.Text;
using System.Net;
using CombatLog.VersionManager;
using SaveListView;
using EVEAttributeTypeHelper;
using CombatLog.Config;

namespace CombatLog
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private bool GameLogCacheUpdated = false;

		private CombatLog.WeaponDataDB.WeaponDataSourceCollection EOWeaponData = null;
		private VersionInfo CurrentVersion;
		private string[] CheckVersionURL = new string[] {"http://utter.chaos.org.uk/~dean/cla/VersionHistory.xml?v=1500"};

		private Logger logger;

		private ArrayList ActiveNotifyWindows = new ArrayList();

		private GraphItemCollection GraphControls = new GraphItemCollection();
		private System.ComponentModel.IContainer components;
		private UserConfig MyConfig = null;

		private GameLogCollection GameLogs;
		private System.Windows.Forms.RichTextBox richTextBox2;
		private System.Windows.Forms.MenuItem menuItem6;
		private System.Windows.Forms.MenuItem menuItem9;
		private System.Windows.Forms.MenuItem menuItem10;
		private TD.SandBar.ContextMenuBarItem contextMenuBarItem1;
		private TD.SandBar.MenuButtonItem menuButtonItem2;
		private TD.SandBar.MenuButtonItem menuButtonItem3;
		private TD.SandBar.MenuButtonItem menuButtonItem4;
		private System.Windows.Forms.Panel panelFileListMain;
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader Date;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.Panel panelFileListTop;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ComboBox comboBoxListener;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox comboBoxLogAge;
		private System.Windows.Forms.Button button1;
		private TD.SandDock.SandDockManager sandDockManager1;
		private TD.SandDock.DockContainer leftSandDock;
		private TD.SandDock.DockContainer rightSandDock;
		private TD.SandDock.DockContainer bottomSandDock;
		private TD.SandDock.DockContainer topSandDock;
		public FileViewListItemComparer FileBrowserComparer = new FileViewListItemComparer();

		private string ApplicationDataDirectory	= Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\EVECombatLog\";
		//private string ApplicationInstallDirectory = Environment.GetFolderPath(Enbironment.SpecialFolder.

		private ProgressStatus statusBar = new ProgressStatus();
		private System.Windows.Forms.StatusBarPanel pnl1;
		private System.Windows.Forms.MenuItem old_old_menuItem7;
		private System.Windows.Forms.ContextMenu contextMenuFile1;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem8;
		private System.Windows.Forms.MenuItem menuItemWindow;
		private System.Windows.Forms.MenuItem menuItemCascade;
		private System.Windows.Forms.MenuItem menuItemTileHoriz;
		private System.Windows.Forms.MenuItem menuItemTileVert;
		private System.Windows.Forms.MenuItem menuItemExport;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
		private System.Windows.Forms.MenuItem menuItem1About;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.MenuItem menuItem7;
		private System.Windows.Forms.MenuItem menuItem11;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox comboBoxLogDir;
		private System.Windows.Forms.MenuItem menuItem13;
		private System.Windows.Forms.MenuItem mbMerge;
		private System.Windows.Forms.MenuItem menuItemBestShotsReport;
		private System.Windows.Forms.MenuItem menuItem16;
		private System.Windows.Forms.MenuItem menuItemReleaseNotes;
		private System.Windows.Forms.MenuItem menuItemForum;
		private System.Windows.Forms.MenuItem menuItemHomePage;
		private System.Windows.Forms.MenuItem menuItem19;
		private System.Windows.Forms.MenuItem menuItem14;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ComboBox comboBoxWeapon;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.ComboBox comboBoxTarget;
		private System.Windows.Forms.NotifyIcon notifyIcon1;
		private System.Windows.Forms.Button btnClearFilters;
		private System.Windows.Forms.MenuItem menuItemCacheReport;
		private System.Windows.Forms.MenuItem menuItem12;
		private System.Windows.Forms.MenuItem menuItem15;
		private System.Windows.Forms.MenuItem menuItem17;
		private TD.SandDock.DockControl dockControlFileBrowser;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.ComboBox comboBoxAttackers;
		private System.Windows.Forms.StatusBarPanel pnlMain;
		private System.Windows.Forms.MenuItem menuItemWeaponInfo;
		private System.Windows.Forms.ComboBox cbWeaponType;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.ComboBox cbWeaponClass;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.StatusBarPanel pnlProgress;

		public Form1()
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
				// this.notifyIcon1.Dispose();

				if (components != null) 
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
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Form1));
			this.richTextBox2 = new System.Windows.Forms.RichTextBox();
			this.menuItem6 = new System.Windows.Forms.MenuItem();
			this.menuItem9 = new System.Windows.Forms.MenuItem();
			this.menuItem10 = new System.Windows.Forms.MenuItem();
			this.contextMenuBarItem1 = new TD.SandBar.ContextMenuBarItem();
			this.menuButtonItem2 = new TD.SandBar.MenuButtonItem();
			this.menuButtonItem3 = new TD.SandBar.MenuButtonItem();
			this.menuButtonItem4 = new TD.SandBar.MenuButtonItem();
			this.panelFileListMain = new System.Windows.Forms.Panel();
			this.listView1 = new System.Windows.Forms.ListView();
			this.Date = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.contextMenuFile1 = new System.Windows.Forms.ContextMenu();
			this.old_old_menuItem7 = new System.Windows.Forms.MenuItem();
			this.mbMerge = new System.Windows.Forms.MenuItem();
			this.panelFileListTop = new System.Windows.Forms.Panel();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.comboBoxListener = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.comboBoxLogAge = new System.Windows.Forms.ComboBox();
			this.button1 = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.comboBoxLogDir = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this.comboBoxWeapon = new System.Windows.Forms.ComboBox();
			this.label5 = new System.Windows.Forms.Label();
			this.comboBoxTarget = new System.Windows.Forms.ComboBox();
			this.btnClearFilters = new System.Windows.Forms.Button();
			this.label6 = new System.Windows.Forms.Label();
			this.comboBoxAttackers = new System.Windows.Forms.ComboBox();
			this.cbWeaponType = new System.Windows.Forms.ComboBox();
			this.label7 = new System.Windows.Forms.Label();
			this.cbWeaponClass = new System.Windows.Forms.ComboBox();
			this.label8 = new System.Windows.Forms.Label();
			this.sandDockManager1 = new TD.SandDock.SandDockManager();
			this.leftSandDock = new TD.SandDock.DockContainer();
			this.dockControlFileBrowser = new TD.SandDock.DockControl();
			this.rightSandDock = new TD.SandDock.DockContainer();
			this.bottomSandDock = new TD.SandDock.DockContainer();
			this.topSandDock = new TD.SandDock.DockContainer();
			this.statusBar = new CombatLog.ProgressStatus();
			this.pnl1 = new System.Windows.Forms.StatusBarPanel();
			this.pnlProgress = new System.Windows.Forms.StatusBarPanel();
			this.pnlMain = new System.Windows.Forms.StatusBarPanel();
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem5 = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.menuItemExport = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItem16 = new System.Windows.Forms.MenuItem();
			this.menuItemBestShotsReport = new System.Windows.Forms.MenuItem();
			this.menuItem14 = new System.Windows.Forms.MenuItem();
			this.menuItemCacheReport = new System.Windows.Forms.MenuItem();
			this.menuItem7 = new System.Windows.Forms.MenuItem();
			this.menuItemWeaponInfo = new System.Windows.Forms.MenuItem();
			this.menuItem12 = new System.Windows.Forms.MenuItem();
			this.menuItem15 = new System.Windows.Forms.MenuItem();
			this.menuItem11 = new System.Windows.Forms.MenuItem();
			this.menuItemWindow = new System.Windows.Forms.MenuItem();
			this.menuItemCascade = new System.Windows.Forms.MenuItem();
			this.menuItemTileHoriz = new System.Windows.Forms.MenuItem();
			this.menuItemTileVert = new System.Windows.Forms.MenuItem();
			this.menuItem8 = new System.Windows.Forms.MenuItem();
			this.menuItemHomePage = new System.Windows.Forms.MenuItem();
			this.menuItemForum = new System.Windows.Forms.MenuItem();
			this.menuItem19 = new System.Windows.Forms.MenuItem();
			this.menuItemReleaseNotes = new System.Windows.Forms.MenuItem();
			this.menuItem17 = new System.Windows.Forms.MenuItem();
			this.menuItem13 = new System.Windows.Forms.MenuItem();
			this.menuItem1About = new System.Windows.Forms.MenuItem();
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
			this.panelFileListMain.SuspendLayout();
			this.panelFileListTop.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.leftSandDock.SuspendLayout();
			this.dockControlFileBrowser.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pnl1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pnlProgress)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
			this.SuspendLayout();
			// 
			// richTextBox2
			// 
			this.richTextBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.richTextBox2.BackColor = System.Drawing.SystemColors.Control;
			this.richTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.richTextBox2.Cursor = System.Windows.Forms.Cursors.Default;
			this.richTextBox2.Location = new System.Drawing.Point(8, 16);
			this.richTextBox2.Name = "richTextBox2";
			this.richTextBox2.ReadOnly = true;
			this.richTextBox2.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
			this.richTextBox2.Size = new System.Drawing.Size(272, 76);
			this.richTextBox2.TabIndex = 0;
			this.richTextBox2.Text = "some text";
			this.richTextBox2.WordWrap = false;
			// 
			// menuItem6
			// 
			this.menuItem6.Index = -1;
			this.menuItem6.Text = "";
			// 
			// menuItem9
			// 
			this.menuItem9.Index = -1;
			this.menuItem9.Text = "Foo";
			// 
			// menuItem10
			// 
			this.menuItem10.Index = -1;
			this.menuItem10.Text = "Bar";
			// 
			// contextMenuBarItem1
			// 
			this.contextMenuBarItem1.MenuItems.AddRange(new TD.SandBar.MenuButtonItem[] {
																							this.menuButtonItem2,
																							this.menuButtonItem3,
																							this.menuButtonItem4});
			this.contextMenuBarItem1.Text = "(contextMenu1)";
			// 
			// menuButtonItem2
			// 
			this.menuButtonItem2.Checked = true;
			this.menuButtonItem2.Text = "425mm Proto Gauss Railgun";
			// 
			// menuButtonItem3
			// 
			this.menuButtonItem3.Text = "425 Compressed Coil Railgun";
			// 
			// menuButtonItem4
			// 
			this.menuButtonItem4.Text = "Modulated Heavy Neutron Blaster";
			// 
			// panelFileListMain
			// 
			this.panelFileListMain.BackColor = System.Drawing.SystemColors.Info;
			this.panelFileListMain.Controls.Add(this.listView1);
			this.panelFileListMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelFileListMain.Location = new System.Drawing.Point(0, 256);
			this.panelFileListMain.Name = "panelFileListMain";
			this.panelFileListMain.Size = new System.Drawing.Size(292, 378);
			this.panelFileListMain.TabIndex = 7;
			// 
			// listView1
			// 
			this.listView1.AllowColumnReorder = true;
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						this.Date,
																						this.columnHeader2,
																						this.columnHeader1,
																						this.columnHeader3});
			this.listView1.ContextMenu = this.contextMenuFile1;
			this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listView1.FullRowSelect = true;
			this.listView1.HideSelection = false;
			this.listView1.Location = new System.Drawing.Point(0, 0);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(292, 378);
			this.listView1.TabIndex = 1;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.ItemActivate += new System.EventHandler(this.listView1_ItemActivate);
			this.listView1.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView1_ColumnClick);
			// 
			// Date
			// 
			this.Date.Text = "Date";
			this.Date.Width = 102;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Char";
			this.columnHeader2.Width = 54;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Size";
			this.columnHeader1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Log Dir";
			// 
			// contextMenuFile1
			// 
			this.contextMenuFile1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																							 this.old_old_menuItem7,
																							 this.mbMerge});
			// 
			// old_old_menuItem7
			// 
			this.old_old_menuItem7.Index = 0;
			this.old_old_menuItem7.Text = "Open in text editor";
			this.old_old_menuItem7.Click += new System.EventHandler(this.menuItem7_Click_1);
			// 
			// mbMerge
			// 
			this.mbMerge.Enabled = false;
			this.mbMerge.Index = 1;
			this.mbMerge.Text = "Merge with active";
			this.mbMerge.Click += new System.EventHandler(this.mbMerge_Click);
			// 
			// panelFileListTop
			// 
			this.panelFileListTop.Controls.Add(this.groupBox1);
			this.panelFileListTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelFileListTop.Location = new System.Drawing.Point(0, 0);
			this.panelFileListTop.Name = "panelFileListTop";
			this.panelFileListTop.Size = new System.Drawing.Size(292, 256);
			this.panelFileListTop.TabIndex = 8;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.comboBoxListener);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.comboBoxLogAge);
			this.groupBox1.Controls.Add(this.button1);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.comboBoxLogDir);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.comboBoxWeapon);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.comboBoxTarget);
			this.groupBox1.Controls.Add(this.btnClearFilters);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.comboBoxAttackers);
			this.groupBox1.Controls.Add(this.cbWeaponType);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Controls.Add(this.cbWeaponClass);
			this.groupBox1.Controls.Add(this.label8);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(0, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(292, 256);
			this.groupBox1.TabIndex = 5;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Filter";
			// 
			// comboBoxListener
			// 
			this.comboBoxListener.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxListener.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxListener.Items.AddRange(new object[] {
																  "All",
																  "Last 30 Days",
																  "Last 7 Days",
																  "Today"});
			this.comboBoxListener.Location = new System.Drawing.Point(96, 39);
			this.comboBoxListener.Name = "comboBoxListener";
			this.comboBoxListener.Size = new System.Drawing.Size(184, 21);
			this.comboBoxListener.Sorted = true;
			this.comboBoxListener.TabIndex = 3;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 12);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(88, 23);
			this.label1.TabIndex = 4;
			this.label1.Text = "Show logs from";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 38);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(88, 23);
			this.label2.TabIndex = 4;
			this.label2.Text = "Character";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// comboBoxLogAge
			// 
			this.comboBoxLogAge.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxLogAge.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxLogAge.Items.AddRange(new object[] {
																"All",
																"Today",
																"Last 7 Days",
																"Last 30 Days"});
			this.comboBoxLogAge.Location = new System.Drawing.Point(96, 13);
			this.comboBoxLogAge.Name = "comboBoxLogAge";
			this.comboBoxLogAge.Size = new System.Drawing.Size(184, 21);
			this.comboBoxLogAge.TabIndex = 3;
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.button1.Location = new System.Drawing.Point(208, 225);
			this.button1.Name = "button1";
			this.button1.TabIndex = 0;
			this.button1.Text = "Rescan";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 64);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(88, 23);
			this.label3.TabIndex = 4;
			this.label3.Text = "Log Dir";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// comboBoxLogDir
			// 
			this.comboBoxLogDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxLogDir.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxLogDir.Items.AddRange(new object[] {
																"All",
																"Last 30 Days",
																"Last 7 Days",
																"Today"});
			this.comboBoxLogDir.Location = new System.Drawing.Point(96, 65);
			this.comboBoxLogDir.Name = "comboBoxLogDir";
			this.comboBoxLogDir.Size = new System.Drawing.Size(184, 21);
			this.comboBoxLogDir.Sorted = true;
			this.comboBoxLogDir.TabIndex = 3;
			this.comboBoxLogDir.SelectedIndexChanged += new System.EventHandler(this.comboBoxLogDir_SelectedIndexChanged);
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 142);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(88, 23);
			this.label4.TabIndex = 4;
			this.label4.Text = "Weapon Name";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// comboBoxWeapon
			// 
			this.comboBoxWeapon.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxWeapon.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxWeapon.DropDownWidth = 184;
			this.comboBoxWeapon.Location = new System.Drawing.Point(96, 143);
			this.comboBoxWeapon.MaxDropDownItems = 18;
			this.comboBoxWeapon.Name = "comboBoxWeapon";
			this.comboBoxWeapon.Size = new System.Drawing.Size(184, 21);
			this.comboBoxWeapon.Sorted = true;
			this.comboBoxWeapon.TabIndex = 3;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(8, 168);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(88, 23);
			this.label5.TabIndex = 4;
			this.label5.Text = "Target";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// comboBoxTarget
			// 
			this.comboBoxTarget.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxTarget.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxTarget.DropDownWidth = 184;
			this.comboBoxTarget.Location = new System.Drawing.Point(96, 169);
			this.comboBoxTarget.MaxDropDownItems = 18;
			this.comboBoxTarget.Name = "comboBoxTarget";
			this.comboBoxTarget.Size = new System.Drawing.Size(184, 21);
			this.comboBoxTarget.Sorted = true;
			this.comboBoxTarget.TabIndex = 3;
			// 
			// btnClearFilters
			// 
			this.btnClearFilters.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClearFilters.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnClearFilters.Location = new System.Drawing.Point(128, 225);
			this.btnClearFilters.Name = "btnClearFilters";
			this.btnClearFilters.TabIndex = 5;
			this.btnClearFilters.Text = "Reset Filters";
			this.btnClearFilters.Click += new System.EventHandler(this.btnClearFilters_Click);
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(8, 194);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(88, 23);
			this.label6.TabIndex = 4;
			this.label6.Text = "Attacked By";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// comboBoxAttackers
			// 
			this.comboBoxAttackers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxAttackers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxAttackers.DropDownWidth = 184;
			this.comboBoxAttackers.Location = new System.Drawing.Point(96, 195);
			this.comboBoxAttackers.MaxDropDownItems = 18;
			this.comboBoxAttackers.Name = "comboBoxAttackers";
			this.comboBoxAttackers.Size = new System.Drawing.Size(184, 21);
			this.comboBoxAttackers.Sorted = true;
			this.comboBoxAttackers.TabIndex = 3;
			// 
			// cbWeaponType
			// 
			this.cbWeaponType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.cbWeaponType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbWeaponType.DropDownWidth = 184;
			this.cbWeaponType.Location = new System.Drawing.Point(96, 91);
			this.cbWeaponType.MaxDropDownItems = 18;
			this.cbWeaponType.Name = "cbWeaponType";
			this.cbWeaponType.Size = new System.Drawing.Size(184, 21);
			this.cbWeaponType.Sorted = true;
			this.cbWeaponType.TabIndex = 3;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(8, 90);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(88, 23);
			this.label7.TabIndex = 4;
			this.label7.Text = "Weapon Type";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// cbWeaponClass
			// 
			this.cbWeaponClass.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.cbWeaponClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbWeaponClass.DropDownWidth = 184;
			this.cbWeaponClass.Location = new System.Drawing.Point(96, 117);
			this.cbWeaponClass.MaxDropDownItems = 18;
			this.cbWeaponClass.Name = "cbWeaponClass";
			this.cbWeaponClass.Size = new System.Drawing.Size(184, 21);
			this.cbWeaponClass.Sorted = true;
			this.cbWeaponClass.TabIndex = 3;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(8, 116);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(88, 23);
			this.label8.TabIndex = 4;
			this.label8.Text = "Weapon Class";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// sandDockManager1
			// 
			this.sandDockManager1.DockingManager = TD.SandDock.DockingManager.Whidbey;
			this.sandDockManager1.OwnerForm = this;
			// 
			// leftSandDock
			// 
			this.leftSandDock.Controls.Add(this.dockControlFileBrowser);
			this.leftSandDock.Dock = System.Windows.Forms.DockStyle.Left;
			this.leftSandDock.Guid = new System.Guid("e8c4835f-04b5-4d28-9941-e763ada515db");
			this.leftSandDock.LayoutSystem = new TD.SandDock.SplitLayoutSystem(250, 400, System.Windows.Forms.Orientation.Horizontal, new TD.SandDock.LayoutSystemBase[] {
																																											 new TD.SandDock.ControlLayoutSystem(292, 675, new TD.SandDock.DockControl[] {
																																																															 this.dockControlFileBrowser}, this.dockControlFileBrowser)});
			this.leftSandDock.Location = new System.Drawing.Point(0, 0);
			this.leftSandDock.Manager = this.sandDockManager1;
			this.leftSandDock.MaximumSize = 500;
			this.leftSandDock.Name = "leftSandDock";
			this.leftSandDock.Size = new System.Drawing.Size(296, 675);
			this.leftSandDock.TabIndex = 13;
			// 
			// dockControlFileBrowser
			// 
			this.dockControlFileBrowser.Closable = false;
			this.dockControlFileBrowser.Controls.Add(this.panelFileListMain);
			this.dockControlFileBrowser.Controls.Add(this.panelFileListTop);
			this.dockControlFileBrowser.Guid = new System.Guid("1958cef5-c709-41c5-8d14-930f85041f02");
			this.dockControlFileBrowser.Location = new System.Drawing.Point(0, 18);
			this.dockControlFileBrowser.Name = "dockControlFileBrowser";
			this.dockControlFileBrowser.Size = new System.Drawing.Size(292, 634);
			this.dockControlFileBrowser.TabIndex = 0;
			this.dockControlFileBrowser.Text = "Combat Log Browser";
			// 
			// rightSandDock
			// 
			this.rightSandDock.Dock = System.Windows.Forms.DockStyle.Right;
			this.rightSandDock.Guid = new System.Guid("d454a83d-9985-4536-ad97-bc50464253b6");
			this.rightSandDock.LayoutSystem = new TD.SandDock.SplitLayoutSystem(250, 400);
			this.rightSandDock.Location = new System.Drawing.Point(1112, 0);
			this.rightSandDock.Manager = this.sandDockManager1;
			this.rightSandDock.MaximumSize = 600;
			this.rightSandDock.Name = "rightSandDock";
			this.rightSandDock.Size = new System.Drawing.Size(0, 675);
			this.rightSandDock.TabIndex = 14;
			// 
			// bottomSandDock
			// 
			this.bottomSandDock.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.bottomSandDock.Guid = new System.Guid("bee2eff7-dcee-4a93-8b02-543ae8c129a4");
			this.bottomSandDock.LayoutSystem = new TD.SandDock.SplitLayoutSystem(250, 400);
			this.bottomSandDock.Location = new System.Drawing.Point(0, 675);
			this.bottomSandDock.Manager = this.sandDockManager1;
			this.bottomSandDock.Name = "bottomSandDock";
			this.bottomSandDock.Size = new System.Drawing.Size(1112, 0);
			this.bottomSandDock.TabIndex = 15;
			// 
			// topSandDock
			// 
			this.topSandDock.Dock = System.Windows.Forms.DockStyle.Top;
			this.topSandDock.Guid = new System.Guid("bf5d1e6d-4847-4734-b8ff-53842b9fbec2");
			this.topSandDock.LayoutSystem = new TD.SandDock.SplitLayoutSystem(250, 400);
			this.topSandDock.Location = new System.Drawing.Point(0, 0);
			this.topSandDock.Manager = this.sandDockManager1;
			this.topSandDock.Name = "topSandDock";
			this.topSandDock.Size = new System.Drawing.Size(1112, 0);
			this.topSandDock.TabIndex = 16;
			// 
			// statusBar
			// 
			this.statusBar.Location = new System.Drawing.Point(0, 675);
			this.statusBar.Name = "statusBar";
			this.statusBar.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
																						 this.pnl1,
																						 this.pnlProgress,
																						 this.pnlMain});
			this.statusBar.setProgressBarPanel = 1;
			this.statusBar.ShowPanels = true;
			this.statusBar.Size = new System.Drawing.Size(1112, 22);
			this.statusBar.TabIndex = 0;
			// 
			// pnl1
			// 
			this.pnl1.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
			this.pnl1.MinWidth = 130;
			this.pnl1.Width = 130;
			// 
			// pnlProgress
			// 
			this.pnlProgress.MinWidth = 0;
			this.pnlProgress.Style = System.Windows.Forms.StatusBarPanelStyle.OwnerDraw;
			// 
			// pnlMain
			// 
			this.pnlMain.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
			this.pnlMain.Width = 866;
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem1,
																					  this.menuItem16,
																					  this.menuItem7,
																					  this.menuItemWindow,
																					  this.menuItem8});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem5,
																					  this.menuItem4,
																					  this.menuItemExport,
																					  this.menuItem3,
																					  this.menuItem2});
			this.menuItem1.Text = "File";
			this.menuItem1.Popup += new System.EventHandler(this.menuItem1_Popup);
			// 
			// menuItem5
			// 
			this.menuItem5.Enabled = false;
			this.menuItem5.Index = 0;
			this.menuItem5.Text = "Open...";
			// 
			// menuItem4
			// 
			this.menuItem4.Enabled = false;
			this.menuItem4.Index = 1;
			this.menuItem4.MdiList = true;
			this.menuItem4.Text = "Close";
			this.menuItem4.Select += new System.EventHandler(this.menuItem4_Select);
			// 
			// menuItemExport
			// 
			this.menuItemExport.Index = 2;
			this.menuItemExport.Text = "Export";
			this.menuItemExport.Click += new System.EventHandler(this.menuItemExport_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 3;
			this.menuItem3.Text = "-";
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 4;
			this.menuItem2.ShowShortcut = false;
			this.menuItem2.Text = "Exit";
			this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
			// 
			// menuItem16
			// 
			this.menuItem16.Index = 1;
			this.menuItem16.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					   this.menuItemBestShotsReport,
																					   this.menuItem14,
																					   this.menuItemCacheReport});
			this.menuItem16.Text = "Reports";
			// 
			// menuItemBestShotsReport
			// 
			this.menuItemBestShotsReport.Index = 0;
			this.menuItemBestShotsReport.Text = "Best Shots...";
			this.menuItemBestShotsReport.Click += new System.EventHandler(this.menuItemBestShotsReport_Click);
			// 
			// menuItem14
			// 
			this.menuItem14.Index = 1;
			this.menuItem14.Text = "-";
			// 
			// menuItemCacheReport
			// 
			this.menuItemCacheReport.Index = 2;
			this.menuItemCacheReport.Text = "Cache Diagnostics...";
			this.menuItemCacheReport.Click += new System.EventHandler(this.menuItem12_Click_1);
			// 
			// menuItem7
			// 
			this.menuItem7.Index = 2;
			this.menuItem7.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItemWeaponInfo,
																					  this.menuItem12,
																					  this.menuItem15,
																					  this.menuItem11});
			this.menuItem7.Text = "Tools";
			// 
			// menuItemWeaponInfo
			// 
			this.menuItemWeaponInfo.Index = 0;
			this.menuItemWeaponInfo.Text = "Weapon Info...";
			this.menuItemWeaponInfo.Click += new System.EventHandler(this.menuItemWeaponInfo_Click);
			// 
			// menuItem12
			// 
			this.menuItem12.Index = 1;
			this.menuItem12.Text = "Clear Cache";
			this.menuItem12.Click += new System.EventHandler(this.menuItem12_Click_2);
			// 
			// menuItem15
			// 
			this.menuItem15.Index = 2;
			this.menuItem15.Text = "-";
			// 
			// menuItem11
			// 
			this.menuItem11.Index = 3;
			this.menuItem11.Text = "Options...";
			this.menuItem11.Click += new System.EventHandler(this.menuItem11_Click);
			// 
			// menuItemWindow
			// 
			this.menuItemWindow.Index = 3;
			this.menuItemWindow.MdiList = true;
			this.menuItemWindow.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						   this.menuItemCascade,
																						   this.menuItemTileHoriz,
																						   this.menuItemTileVert});
			this.menuItemWindow.Text = "Window";
			// 
			// menuItemCascade
			// 
			this.menuItemCascade.Index = 0;
			this.menuItemCascade.Text = "Cascade";
			this.menuItemCascade.Click += new System.EventHandler(this.menuItemCascade_Click);
			// 
			// menuItemTileHoriz
			// 
			this.menuItemTileHoriz.Index = 1;
			this.menuItemTileHoriz.Text = "Tile Horizontally";
			this.menuItemTileHoriz.Click += new System.EventHandler(this.menuItemTileHoriz_Click);
			// 
			// menuItemTileVert
			// 
			this.menuItemTileVert.Index = 2;
			this.menuItemTileVert.Text = "Tile Vertically";
			this.menuItemTileVert.Click += new System.EventHandler(this.menuItemTileVert_Click);
			// 
			// menuItem8
			// 
			this.menuItem8.Index = 4;
			this.menuItem8.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItemHomePage,
																					  this.menuItemForum,
																					  this.menuItem19,
																					  this.menuItemReleaseNotes,
																					  this.menuItem17,
																					  this.menuItem13,
																					  this.menuItem1About});
			this.menuItem8.Text = "Help";
			// 
			// menuItemHomePage
			// 
			this.menuItemHomePage.Index = 0;
			this.menuItemHomePage.Text = "Online Home Page";
			this.menuItemHomePage.Click += new System.EventHandler(this.menuItemHomePage_Click);
			// 
			// menuItemForum
			// 
			this.menuItemForum.Index = 1;
			this.menuItemForum.Text = "Online Forum";
			this.menuItemForum.Click += new System.EventHandler(this.menuItemForum_Click);
			// 
			// menuItem19
			// 
			this.menuItem19.Index = 2;
			this.menuItem19.Text = "-";
			// 
			// menuItemReleaseNotes
			// 
			this.menuItemReleaseNotes.Index = 3;
			this.menuItemReleaseNotes.Text = "Release Notes...";
			this.menuItemReleaseNotes.Click += new System.EventHandler(this.menuItem12_Click);
			// 
			// menuItem17
			// 
			this.menuItem17.Index = 4;
			this.menuItem17.Text = "Check for new version";
			this.menuItem17.Click += new System.EventHandler(this.menuItem17_Click);
			// 
			// menuItem13
			// 
			this.menuItem13.Index = 5;
			this.menuItem13.Text = "-";
			// 
			// menuItem1About
			// 
			this.menuItem1About.Index = 6;
			this.menuItem1About.Text = "About Combat Analyser...";
			this.menuItem1About.Click += new System.EventHandler(this.menuItem1About_Click);
			// 
			// saveFileDialog1
			// 
			this.saveFileDialog1.Filter = "Text File|*.txt|CSV|*.csv";
			// 
			// notifyIcon1
			// 
			this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
			this.notifyIcon1.Text = "EVE Combat Log Analyser";
			this.notifyIcon1.Click += new System.EventHandler(this.notifyIcon1_Click);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(1112, 697);
			this.Controls.Add(this.leftSandDock);
			this.Controls.Add(this.rightSandDock);
			this.Controls.Add(this.bottomSandDock);
			this.Controls.Add(this.topSandDock);
			this.Controls.Add(this.statusBar);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.IsMdiContainer = true;
			this.Menu = this.mainMenu1;
			this.Name = "Form1";
			this.Text = "EVE Combat Log Analyzer";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.Form1_Closing);
			this.Load += new System.EventHandler(this.Form1_Load);
			this.panelFileListMain.ResumeLayout(false);
			this.panelFileListTop.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.leftSandDock.ResumeLayout(false);
			this.dockControlFileBrowser.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pnl1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pnlProgress)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		[STAThread]

		static void Main() 
		{
			Application.EnableVisualStyles();
			Application.DoEvents();
			Application.Run(new Form1());
		}

		private Cache GetCombatLogCache()
		{

			logger.Log(Level.Info, "Loading combatlog cache...");

			Cache c = new Cache();
			c.CacheData = new CombatLogCacheCollection();

			FileStream fs;
			try
			{
				fs = new FileStream(ApplicationDataDirectory + "LogCache.xml", FileMode.Open);
			}
			catch (Exception e)
			{
				Debug.WriteLine("Problem opening log cache: " + e.ToString());
				logger.Log(Level.Exception, "Could not open the combatlog cache file: " + e.ToString());
				return c;
			}

			XmlSerializer xs;

			try
			{
				xs = new XmlSerializer(typeof(Cache));
			}
			catch (Exception e)
			{
				logger.Log(Level.Exception, "Possible invalid cache file format, purging the cache");
				logger.Log(Level.Exception, e.ToString());

				try
				{
					File.Delete(ApplicationDataDirectory + "LogCache.xml");
					return c;
				}
				catch
				{
					logger.Log(Level.Exception, "PANIC: Could not delete " + ApplicationDataDirectory + "LogCache.xml");
					return c;
				}
			}

			try
			{
				c = (Cache)xs.Deserialize(fs);
				logger.Log(Level.Verbose, "Combatlog cache loaded (" + c.CacheData.Count + " entries in cache)");
				logger.Log(Level.Verbose, "Combatlog cache version: " + c.CacheVersion);
			}
			catch (Exception e)
			{
				// Something went wrong loading the cache
				// return an empty log cache
				c  = new Cache();
				logger.Log(Level.Exception, "Could not deserialize the combatlog cache, cache will be reset: " + e.ToString());
			}

			fs.Close();

			return c;
		}


		private void LoadEOWeaponData()
		{
			if ( !File.Exists(Application.StartupPath + @"\Static\Weapons.xml") )
			{
				MessageBox.Show("Cannot find the Weapons.xml file, cannot do anything");
				return;
			}

			FileStream f = new FileStream(Application.StartupPath + @"\Static\Weapons.xml", FileMode.Open);

			XmlSerializer xs = new XmlSerializer(typeof(WeaponDataDB.WeaponDataSourceCollection));

			EOWeaponData = (WeaponDataDB.WeaponDataSourceCollection)xs.Deserialize(f);

			f.Close();
		}

		private void SaveGameLogCache(GameLogCollection gl)
		{
			if ( !Directory.Exists(ApplicationDataDirectory) )
				Directory.CreateDirectory(ApplicationDataDirectory);

			logger.Log(Level.Info, "Saving combatlog cache to disk");

			try
			{
				FileStream fs = new FileStream(ApplicationDataDirectory + "logCache.xml", FileMode.Create);

				XmlSerializer xs = new XmlSerializer(typeof(Cache));

				gl.LogCache.LastUpdated = DateTime.Now;
				xs.Serialize(fs, gl.LogCache);

				fs.Close();
			}
			catch (Exception e)
			{
				MessageBox.Show("There was a problem saving the cache file to disk. The error message reported was: " + e.ToString() + "\nPlease inform Hurg of the error using the official forum on the EVE-I web site.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				logger.Log(Level.Exception, "Error saving the combatlog cache to disk: " + e.ToString());
				return;
			}

			logger.Log(Level.Info, "Combatlog cache saved to disk.");
		}

		private void SaveMyConfig()
		{
			if ( !Directory.Exists(ApplicationDataDirectory) )
				Directory.CreateDirectory(ApplicationDataDirectory);

			FileStream fs;
			try
			{
				fs = new FileStream(ApplicationDataDirectory + "config.xml", FileMode.Create);

				XmlSerializer xs = new XmlSerializer(typeof(UserConfig));

				xs.Serialize(fs, MyConfig);

				fs.Close();
			}
			catch (Exception e)
			{
				MessageBox.Show("There was a problem saving the config.xml file to " + ApplicationDataDirectory +". The error message reported by the system is: " + e.ToString()+ "\nPlease inform Hurg of the error using the official forum on the EVE-I web site.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private UserConfig DeserializeUserConfig(string PathName)
		{
			UserConfig cfg = new UserConfig();

			FileStream fs;

			try
			{
				logger.Log(Level.Info, "Opening user config file");
				fs = new FileStream(PathName, FileMode.Open);
			}
			catch (Exception err)
			{
				logger.Log(Level.Exception, "Error opening user config file: " + err.ToString());

				MessageBox.Show("There was a problem opening the UserConfig file. Does some other application have it open for writing?", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

				return cfg;
			}

			XmlSerializer xs = new XmlSerializer(typeof(UserConfig));

			try
			{
				logger.Log(Level.Info, "Deserializing user config");
				cfg = (UserConfig)xs.Deserialize(fs);
				logger.Log(Level.Info, "User config deserialized");

				if ( cfg == null )
				{
					logger.Log(Level.Exception, "User config is null following deserialization!");
					MyConfig = new UserConfig();
					MyConfig.GameLogDirs = new ConfigGameLogDirCollection();
				}
			}
			catch (Exception err)
			{
				logger.Log(Level.Exception, "Error deserializing user configuration file: " + err.ToString());
				MessageBox.Show("There was a problem reading configuration data from your UserConfig file. Please delete this file and restart the application", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}

			fs.Close();

			return cfg;
		}

		private UserConfig LoadConfiguration()
		{
			logger.Log(Level.Info, "Loading user configuration data...");

			if ( !File.Exists(ApplicationDataDirectory + "config.xml") )
			{
				logger.Log(Level.Warning, "User configuration file not found in " + ApplicationDataDirectory);

				UserConfig cfg = new UserConfig();
				cfg.GameLogDirs = new ConfigGameLogDirCollection();

				logger.Log(Level.Info, "New instance of user configuration object created");
				return cfg;
			}

			return DeserializeUserConfig(ApplicationDataDirectory + "config.xml");
		}

		private void StopLogging()
		{
			if ( logger != null )
				if ( logger.IsOpen )
					logger.Close();
		}

		private void StartLogging()
		{

			if ( logger != null )
				if ( logger.IsOpen )
					logger.Close();

			string logPath = Application.StartupPath + @"\DEBUG_LOG.log";

			try
			{
				if ( File.Exists(logPath) )
					File.Delete(logPath);

				logger = Logger.CreateFileLogger(logPath, "{ts}{z}  [{ln:1w}]  {msg}");
				logger.IsBufferingEnabled = true;
				logger.BufferSize = 1000;
				logger.Open();
				logger.Log(Level.Info, "Log file starting @ " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
			}
			catch (Exception e)
			{
				MessageBox.Show("There was a problem creating the DEBUG_LOG.log file. Please report the following error to hurg@ntlworld.com: " + e.ToString(),"Warning");
				Application.Exit();
			}
		}

		private void TestHitTypes()
		{
			HitTypeLib.Initialise();

			Debug.WriteLine("HTL contains " + HitTypeLib.Count() + " entries");
			Debug.WriteLine("Descending");
			string[] types = HitTypeLib.SortedByPriority(SortOrder.Descending);
			foreach (string s in types)
			{
				Debug.WriteLine(s);
			}

			Debug.WriteLine("Ascending");
			types = HitTypeLib.SortedByPriority(SortOrder.Ascending);
			foreach (string s in types)
			{
				Debug.WriteLine(s);
			}

			Debug.WriteLine("Resolved lightly hits = " + HitTypeLib.GetDisplayString("lightly hits"));

			Debug.WriteLine("");

			string[] ht = new string[] { "Hits", "Glancing", "Well Aimed", "Excellent" };

			string[] htSorted = HitTypeLib.Sort(ht, SortOrder.Descending);

			Debug.WriteLine("Sorted list");
			int i = 0;
			foreach ( string s in htSorted )
			{
				Debug.WriteLine(i.ToString() +": " + s);
				i++;
			}

			Debug.WriteLine("=======================");

		}

		private void SetVersion()
		{
			CurrentVersion = new VersionInfo();
			CurrentVersion.VersionString	= "0.17";
			CurrentVersion.VersionNumber	= 1600;
			CurrentVersion.ReleaseType		= VersionType.Beta;
		}

		private void RestoreLayout()
		{
			if ( MyConfig.RememberColumnWidths )
			{
				if ( MyConfig.FileBrowserListViewSettings != null )
					MyConfig.FileBrowserListViewSettings.RestoreFormat(listView1);
			}

			if ( MyConfig.RememberWindowLayout )
			{
				if ( MyConfig.MainWindowLayout != null && MyConfig.MainWindowLayout.Length > 0 )
				{
					try
					{
						sandDockManager1.SetLayout(MyConfig.MainWindowLayout);
					}
					catch (Exception e)
					{
						logger.Log(Level.Exception, "Error restoring main window layout: " + e.ToString());
					}
				}
			}
		}

		private void Form1_Load(object sender, System.EventArgs e)
		{
			SetVersion();

			this.Resize += new System.EventHandler(this.Form1_Resize);

			StartLogging();

			LoadEOWeaponData();

			//notifyIcon1.Visible = false;
			pnl1.Text = "Starting up...";

			MyConfig = LoadConfiguration();

			RestoreLayout();

			PrepareFileBrowser();

			FileBrowserComparer.CurrentSortColumn = 0;
			FileBrowserComparer.SortOrder = SortOrder.Descending;
			listView1.ListViewItemSorter = FileBrowserComparer;

			SetupLogDirWatcher();

			VersionCheckTimer();
		}

		private void Form1_Resize(object sender, System.EventArgs e)
		{
			//
			// Have we just been minimized?
			//

			if ( MyConfig.MinimiseToSystemTray )
			{
				if ( this.WindowState == FormWindowState.Minimized )
				{
					this.ShowInTaskbar = false;
					this.notifyIcon1.Visible = true;
				}
				else
				{
					this.ShowInTaskbar = true;
					this.notifyIcon1.Visible = false;
				}
			}
			else
				this.ShowInTaskbar = true;
		}

		// Define the event handlers.
		private void WatcherLogFileChanged(object source, FileSystemEventArgs e)
		{
			// Specify what is done when a file is changed, created, or deleted.
			Debug.WriteLine("File: " +  e.FullPath + " " + e.ChangeType);

			FileInfo fi = new FileInfo(e.FullPath);

			try
			{
				FileStream f = new FileStream(e.FullPath, FileMode.Open);
				Debug.WriteLine("File is readable");
				f.Close();

				ProcessNewFile(fi);
			}
			catch (Exception err)
			{
				Debug.WriteLine("Cannot open file for reading, probably not ready to be parsed: " + err.ToString());
			}
			
		}

		private void RefreshFile(FileInfo fileInfo)
		{
			GameLog newFile = new GameLog(fileInfo.FullName);

			newFile.FileSize = fileInfo.Length;
			newFile.GetHeaders();
			newFile.PathAlias = MyConfig.AliasFromFileName(fileInfo.FullName);

			GameLogs.CacheThis(newFile);
			SaveGameLogCache(GameLogs);
			// CombatLog.CombatLogEntryCollection combatEntries = newFile.GetCombatEntries();
		}

		private void ProcessNewFile(FileInfo fileInfo)
		{
			bool NewFile = false;

			if ( GameLogs.LogCache.CacheData.Contains(fileInfo.FullName) )
			{
				// The details for a file that we already know about have changed.
				// Discard the existing cache entry and replace it with a newly
				// processed game log.
				CombatLogCache c = GameLogs.LogCache.CacheData[fileInfo.FullName];

				if ( c != null )
				{
					GameLogs.LogCache.CacheData.Remove(c);
				}
			}
			else
				NewFile = true;
			
			GameLog newFile = new GameLog(fileInfo.FullName);

			newFile.FileSize = fileInfo.Length;
			newFile.GetHeaders();
			newFile.PathAlias = MyConfig.AliasFromFileName(fileInfo.FullName);

			GameLogs.CacheThis(newFile);
			CombatLog.CombatLogEntryCollection combatEntries = newFile.GetCombatEntries();

			string[] Targets = combatEntries.GetUniqueTargets();
			StringBuilder TargetList = new StringBuilder();

			foreach (string s in Targets)
			{
				if ( TargetList.Length > 0 )
					TargetList.Append(", ");

				TargetList.Append(s);
			}

			Debug.WriteLine("Target List: " + TargetList.ToString());

			if ( MyConfig.NotifyNewLogFiles )
				ShowNotifyWindow(fileInfo.FullName, newFile.Listener, TargetList.ToString());

			if ( NewFile )
				AddEntryToFileBrowser(newFile.FileName);
			else
				UpdateListViewItem(GameLogs.LogCache.CacheData[fileInfo.FullName]);
		}

		private void AddEntryToFileBrowser(string FileName)
		{
			CombatLogCache c = GameLogs.LogCache.CacheData[FileName];

			ListViewItem l = new ListViewItem(new string[] { c.CreationTime.ToShortDateString() + " " + c.CreationTime.ToShortTimeString(), c.Character, c.FileSize.ToString(), c.DirAlias });

			Font f = new Font(l.Font.FontFamily.Name, l.Font.Size, l.Font.Style | FontStyle.Bold);
			l.Font = f;

			l.Tag = c;

			listView1.Items.Insert(0,l);
		}

		private FileSystemWatcher CreateDirWatcher(string PathName)
		{
			FileSystemWatcher watcher = new FileSystemWatcher();
			watcher.Path = PathName;

			watcher.NotifyFilter = NotifyFilters.Attributes;

			// Only watch text files.
			watcher.Filter = "*.txt";

			// Add event handlers.
			watcher.Changed += new FileSystemEventHandler(WatcherLogFileChanged);

			// Begin watching.
			watcher.EnableRaisingEvents = true;

			return watcher;
		}

		private void SetupLogDirWatcher()
		{
			if ( !MyConfig.DetectNewLogFiles )
				return;

			foreach ( ConfigGameLogDir gd in MyConfig.GameLogDirs )
			{
				gd.Watcher = CreateDirWatcher(gd.PathName);
				Debug.WriteLine("Setting a filesystem watcher on " + gd.PathName);
			}
		}

		private void PrepareFileBrowser()
		{
			listView1.SuspendLayout();
			listView1.Items.Clear();
			listView1.ResumeLayout();

			comboBoxListener.Items.Clear();
			comboBoxLogAge.Items.Clear();
			comboBoxLogDir.Items.Clear();

			this.statusBar.progressBar.Value = 0;
			
			if ( GameLogs != null )
			{
				GameLogs.Clear();

				if ( GameLogs.LogCache != null )
					GameLogs.LogCache.CacheData.Clear();
			}
			else
			{
				GameLogs = new GameLogCollection();

				try
				{
					GameLogs.GetDefaultInstallPath();
				}
				catch
				{
					if ( MyConfig.GameLogDirs.Count == 0 )
					{
						string msg = "Could not find a default EVE Installation. Please use the Tools->Options dialog to to manually add some GameLog directories";
						MessageBox.Show(msg, "Message from Combat Log Analyser", MessageBoxButtons.OK, MessageBoxIcon.Information);
						return;
					}
				}
			}

			Cache logCache = GetCombatLogCache();

			if ( logCache.CacheData == null )
			{
				logCache.CacheData = new CombatLogCacheCollection();
				logger.Log(Level.Warning, "Creating a new log cache, none available on disk");
			}

			if ( logCache.CacheVersion == null || logCache.CacheVersion == "" )
			{
				MessageBox.Show("The information that was previously extracted from your EVE GameLog files needs to be updated. This may take a couple of minutes if you have lots of log files. " + Environment.NewLine + Environment.NewLine + "Please be patient :)", "Message from EVE Combat Log Analyser", MessageBoxButtons.OK);
				logCache.CacheData.Clear();

				MyConfig.DetectNewLogFiles = true;
				MyConfig.MinimiseToSystemTray = true;
				MyConfig.NotifyNewLogFiles = true;
				MyConfig.CheckForNewVersion = true;
			}

			GameLogs.LogCache = logCache;
			logCache.CacheData.SetWeaponData(EOWeaponData);
			logCache.CacheData.CreateWeaponTypeIndexes();

			GameLogs.LogFileProcessed_Event		+= new GameLogCollection.LogFileProcessedEvent(LogFileLoaded_Event);
			GameLogs.ProcessingComplete_Event	+= new GameLogCollection.LogFileProcessingComplete(ProcessingComplete_Event);
			GameLogs.ProcessingStarted_Event	+= new GameLogCollection.LogFileProcessingStarting(ProcessingStarted_Event);

			logger.Log(Level.Info, "Enumeration event handlers bound");

			logCache.CacheVersion = "0.62";
			EnumerateDelegate DoEnumeration = new EnumerateDelegate(Enumerate);
			DoEnumeration.BeginInvoke(logger, null, null);

			logger.Log(Level.Info, "Enumeration thread invoked");
		}

		//private void LogFileLoaded_Event(object Sender, ameLogCollection EventArgs, int CurrentFile, int TotalFiles)
		private void LogFileLoaded_Event(object Sender, CombatLogCacheCollection EventArgs, int CurrentFile, int TotalFiles)
		{
			//Debug.WriteLine("Gamelog loaded: " + EventArgs.FileName);
			bool Abort;
			UpdateFileBrowser(EventArgs, CurrentFile, TotalFiles, out Abort);
		}

		private delegate void UpdateFileBrowserDelegate(CombatLogCacheCollection LogFiles, int CurrentFile, int TotalFiles, out bool UserAbortRequest);
		private void UpdateFileBrowser(CombatLogCacheCollection LogFiles, int CurrentFile, int TotalFiles, out bool UserAbortRequest)
		{
			if ( !listView1.InvokeRequired )
			{
				lock(listView1)
				{
					this.statusBar.progressBar.Maximum = TotalFiles;

					pnl1.Text = "Processed " + CurrentFile.ToString() + " / " + TotalFiles.ToString();
					this.statusBar.progressBar.Value = CurrentFile;
					
					UserAbortRequest = false;

					string DateStr = "";

					foreach (CombatLogCache c in LogFiles)
					{
						if ( c.IsCombatLog )
						{
							DateStr = c.CreationTime.ToShortDateString() + " " + c.CreationTime.ToShortTimeString();

							ListViewItem lvi = new ListViewItem(new string[] { DateStr, c.Character, c.FileSize.ToString(), c.DirAlias} );
							lvi.Tag = (object)c;

							listView1.Items.Add(lvi);
						}
					}
				}
			}
			else
			{
				object inOutAbortRequest = false;

				try
				{
					UpdateFileBrowserDelegate bc = new UpdateFileBrowserDelegate(UpdateFileBrowser);
					Invoke(bc, new object[] { LogFiles, CurrentFile, TotalFiles, inOutAbortRequest });
				}
				catch (Exception err)
				{
					Debug.WriteLine("ERROR: " + err.ToString());
				}

				UserAbortRequest = (bool)inOutAbortRequest;
			}
			UserAbortRequest = false;
		}

		private void ProcessingComplete_Event(object Sender, System.EventArgs e)
		{
			pnl1.Text = "Saving cache";
			SaveGameLogCache(GameLogs);
			pnl1.Text = "Preparing filters...";

			Win32.HiPerfTimer HTimer = new Win32.HiPerfTimer();

			HTimer.Start();

			PrepareAgeFilter();
			PrepareListenersFilter();
			PrepareLogDirFilter();
			PrepareWeaponTypeFilter();
			PrepareWeaponClassFilter();
			PrepareWeaponFilter();
			PrepareTargetFilter();
			PrepareAttackerFilter();

			HTimer.Stop();

			string[] Targets = GameLogs.LogCache.CacheData.GetUniqueTargetList();

			string[] Weps = GameLogs.LogCache.CacheData.GetUniqueWeaponList();

			Debug.WriteLine("Time to Prepare FileBrowser filters: " + HTimer.Duration.ToString());

			GameLogs.LogFileProcessed_Event		-= new GameLogCollection.LogFileProcessedEvent(LogFileLoaded_Event);
			GameLogs.ProcessingComplete_Event	-= new GameLogCollection.LogFileProcessingComplete(ProcessingComplete_Event);
			GameLogs.ProcessingStarted_Event	-= new GameLogCollection.LogFileProcessingStarting(ProcessingStarted_Event);

			if ( Sender.GetType() == typeof(Form2) )
				pnl1.Text = "Log file loaded";
			else
				pnl1.Text = "Ready";

			statusBar.progressBar.Value=0;
			statusBar.HideProgressBar();
		}

		private void ProcessingStarted_Event(object Sender, int FileCount)
		{
			this.statusBar.progressBar.Minimum = 0;
			this.statusBar.progressBar.Maximum = FileCount;
			this.statusBar.progressBar.Value = 0;
			this.statusBar.setProgressBarPanel = 1;
			this.statusBar.ShowProgressBar();
			pnl1.Text = "Processed 0/" + FileCount.ToString();
		}

		private delegate void EnumerateDelegate(Logger logger);
		private void Enumerate(Logger logger)
		{
			listView1.Items.Clear();
			Debug.WriteLine("About to enumerate log files");
			GameLogs.EnumerateGameLogDir(MyConfig.GameLogDirs, logger);
		}

		private void PrepareAgeFilter()
		{
			this.comboBoxLogAge.SelectedIndexChanged -= new System.EventHandler(this.comboBoxLogDir_SelectedIndexChanged);

			comboBoxLogAge.Items.Clear();
			comboBoxLogAge.Items.AddRange(new string[] { "--", "Today", "Last 7 Days", "Last 30 Days" });
			comboBoxLogAge.SelectedIndex = 0;

			this.comboBoxLogAge.SelectedIndexChanged += new System.EventHandler(this.comboBoxLogDir_SelectedIndexChanged);

		}


		private void PrepareLogDirFilter(CombatLogCacheCollection FilteredLogs)
		{
			bool ItemSelected = false;
			string PreviouslySelectedItem = "";

			if ( comboBoxLogDir.Items.Count > 0 && comboBoxLogDir.SelectedIndex != 0)
			{
				ItemSelected = true;
				PreviouslySelectedItem = comboBoxLogDir.SelectedItem.ToString();
			}
			
			this.comboBoxLogDir.SelectedIndexChanged -= new System.EventHandler(this.comboBoxLogDir_SelectedIndexChanged);

			comboBoxLogDir.Items.Clear();
			comboBoxLogDir.Items.Add("--");
			comboBoxLogDir.Items.AddRange(FilteredLogs.GetUniqueLogDirs());

			if ( ItemSelected )
			{
				for ( int i = 0; i < comboBoxLogDir.Items.Count; i++ )
				{

					if ( comboBoxLogDir.Items[i].ToString() == PreviouslySelectedItem )
					{
						comboBoxLogDir.SelectedIndex = i;
						break;
					}
				}
			}
			else
				comboBoxLogDir.SelectedIndex = 0;

			this.comboBoxLogDir.SelectedIndexChanged += new System.EventHandler(this.comboBoxLogDir_SelectedIndexChanged);
		}

		private void PrepareLogDirFilter()
		{
			PrepareLogDirFilter(GameLogs.LogCache.CacheData);
		}


		private void PrepareListenersFilter(CombatLogCacheCollection FilteredLogs)
		{
			bool ItemSelected = false;
			string PreviouslySelectedItem = "";

			if ( comboBoxListener.Items.Count > 0 && comboBoxListener.SelectedIndex != 0 )
			{
				ItemSelected = true;
				PreviouslySelectedItem = comboBoxListener.SelectedItem.ToString();
			}

			this.comboBoxListener.SelectedIndexChanged -= new System.EventHandler(this.comboBoxLogDir_SelectedIndexChanged);

			comboBoxListener.Items.Clear();
			comboBoxListener.Items.Add("--");
			comboBoxListener.Items.AddRange(FilteredLogs.GetUniqueCharacterList());

			if ( ItemSelected )
			{
				for ( int i = 0; i < comboBoxListener.Items.Count; i++ )
				{

					if ( comboBoxListener.Items[i].ToString() == PreviouslySelectedItem )
					{
						comboBoxListener.SelectedIndex = i;
						break;
					}
				}
			}
			else
				comboBoxListener.SelectedIndex = 0;

			this.comboBoxListener.SelectedIndexChanged += new System.EventHandler(this.comboBoxLogDir_SelectedIndexChanged);

		}
		private void PrepareListenersFilter()
		{
			PrepareListenersFilter(GameLogs.LogCache.CacheData);
		}


		private void PrepareWeaponClassFilter(CombatLogCacheCollection FilteredLogs)
		{
			bool ItemSelected = false;
			string PreviouslySelectedItem = "";

			if ( cbWeaponClass.Items.Count > 0 && cbWeaponClass.SelectedIndex != 0 )
			{
				ItemSelected = true;
				PreviouslySelectedItem = cbWeaponClass.SelectedItem.ToString();
			}

			this.cbWeaponClass.SelectedIndexChanged -= new System.EventHandler(this.comboBoxLogDir_SelectedIndexChanged);

			cbWeaponClass.Items.Clear();
			cbWeaponClass.Items.Add("--");

			WeaponDataDB.WeaponClassObj[] WeaponClasses = FilteredLogs.GetUniqueWeaponClassList(EOWeaponData);
			//
			cbWeaponClass.Items.AddRange(WeaponClasses);

			if ( ItemSelected )
			{
				for ( int i = 0; i < cbWeaponClass.Items.Count; i++ )
				{

					if ( cbWeaponClass.Items[i].ToString() == PreviouslySelectedItem )
					{
						cbWeaponClass.SelectedIndex = i;
						break;
					}
				}
			}
			else
				cbWeaponClass.SelectedIndex = 0;

			this.cbWeaponClass.SelectedIndexChanged += new System.EventHandler(this.comboBoxLogDir_SelectedIndexChanged);
		}
		private void PrepareWeaponClassFilter()
		{
			PrepareWeaponClassFilter(GameLogs.LogCache.CacheData);
		}


		private void PrepareWeaponTypeFilter(CombatLogCacheCollection FilteredLogs)
		{
			bool ItemSelected = false;
			string PreviouslySelectedItem = "";

			if ( cbWeaponType.Items.Count > 0 && cbWeaponType.SelectedIndex != 0 )
			{
				ItemSelected = true;
				PreviouslySelectedItem = cbWeaponType.SelectedItem.ToString();
			}

			this.cbWeaponType.SelectedIndexChanged -= new System.EventHandler(this.comboBoxLogDir_SelectedIndexChanged);

			cbWeaponType.Items.Clear();
			cbWeaponType.Items.Add("--");

			WeaponDataDB.WeaponTypeObj[] WeaponTypes = FilteredLogs.GetUniqueWeaponTypeList(EOWeaponData);
//
			cbWeaponType.Items.AddRange(WeaponTypes);

			if ( ItemSelected )
			{
				for ( int i = 0; i < cbWeaponType.Items.Count; i++ )
				{

					if ( cbWeaponType.Items[i].ToString() == PreviouslySelectedItem )
					{
						cbWeaponType.SelectedIndex = i;
						break;
					}
				}
			}
			else
				cbWeaponType.SelectedIndex = 0;

			this.cbWeaponType.SelectedIndexChanged += new System.EventHandler(this.comboBoxLogDir_SelectedIndexChanged);
		}

		private void PrepareWeaponTypeFilter()
		{
			PrepareWeaponTypeFilter(GameLogs.LogCache.CacheData);
		}


		private void PrepareWeaponFilter(CombatLogCacheCollection FilteredLogs)
		{
			bool ItemSelected = false;
			string PreviouslySelectedItem = "";

			if ( comboBoxWeapon.Items.Count > 0 && comboBoxWeapon.SelectedIndex != 0 )
			{
				ItemSelected = true;
				PreviouslySelectedItem = comboBoxWeapon.SelectedItem.ToString();
			}

			this.comboBoxWeapon.SelectedIndexChanged -= new System.EventHandler(this.comboBoxLogDir_SelectedIndexChanged);

			comboBoxWeapon.Items.Clear();
			comboBoxWeapon.Items.Add("--");
			comboBoxWeapon.Items.AddRange(FilteredLogs.GetUniqueWeaponList());

			if ( ItemSelected )
			{
				for ( int i = 0; i < comboBoxWeapon.Items.Count; i++ )
				{

					if ( comboBoxWeapon.Items[i].ToString() == PreviouslySelectedItem )
					{
						comboBoxWeapon.SelectedIndex = i;
						break;
					}
				}
			}
			else
				comboBoxWeapon.SelectedIndex = 0;

			this.comboBoxWeapon.SelectedIndexChanged += new System.EventHandler(this.comboBoxLogDir_SelectedIndexChanged);
		}

		private void PrepareWeaponFilter()
		{
			PrepareWeaponFilter(GameLogs.LogCache.CacheData);
		}


		private void PrepareTargetFilter(CombatLogCacheCollection FilteredLogs)
		{
			bool ItemSelected = false;
			string PreviouslySelectedItem = "";

			if ( comboBoxTarget.Items.Count > 0 && comboBoxTarget.SelectedIndex != 0 )
			{
				ItemSelected = true;
				PreviouslySelectedItem = comboBoxTarget.SelectedItem.ToString();
			}

			this.comboBoxTarget.SelectedIndexChanged -= new System.EventHandler(this.comboBoxLogDir_SelectedIndexChanged);

			comboBoxTarget.Items.Clear();
			comboBoxTarget.Items.Add("--");
			comboBoxTarget.Items.AddRange(FilteredLogs.GetUniqueTargetList());

			if ( ItemSelected )
			{
				for ( int i = 0; i < comboBoxTarget.Items.Count; i++ )
				{
					if ( comboBoxTarget.Items[i].ToString() == PreviouslySelectedItem )
					{
						comboBoxTarget.SelectedIndex = i;
						break;
					}
				}
			}
			else
				comboBoxTarget.SelectedIndex = 0;

			this.comboBoxTarget.SelectedIndexChanged += new System.EventHandler(this.comboBoxLogDir_SelectedIndexChanged);
		}

		private void PrepareTargetFilter()
		{
			PrepareTargetFilter(GameLogs.LogCache.CacheData);
		}
		

		private void PrepareAttackerFilter(CombatLogCacheCollection FilteredLogs)
		{
			bool ItemSelected = false;
			string PreviouslySelectedItem = "";

			if ( comboBoxAttackers.Items.Count > 0 && comboBoxAttackers.SelectedIndex != 0 )
			{
				ItemSelected = true;
				PreviouslySelectedItem = comboBoxAttackers.SelectedItem.ToString();
			}

			this.comboBoxAttackers.SelectedIndexChanged -= new System.EventHandler(this.comboBoxLogDir_SelectedIndexChanged);

			comboBoxAttackers.Items.Clear();
			comboBoxAttackers.Items.Add("--");
			comboBoxAttackers.Items.AddRange(FilteredLogs.GetUniqueAttackersList());

			if ( ItemSelected )
			{
				for ( int i = 0; i < comboBoxAttackers.Items.Count; i++ )
				{
					if ( comboBoxAttackers.Items[i].ToString() == PreviouslySelectedItem )
					{
						comboBoxAttackers.SelectedIndex = i;
						break;
					}
				}
			}
			else
				comboBoxAttackers.SelectedIndex = 0;

			this.comboBoxAttackers.SelectedIndexChanged += new System.EventHandler(this.comboBoxLogDir_SelectedIndexChanged);
		}

		private void PrepareAttackerFilter()
		{
			PrepareAttackerFilter(GameLogs.LogCache.CacheData);
		}
		private int GetDaysFromAgeCombo()
		{
			int days = -1;

			switch ( comboBoxLogAge.SelectedItem.ToString() )
			{
				case "All":
					days = -1;
					break;
				case "Today":
					days = 0;
					break;

				case "Last 7 Days":
					days = 7;
					break;

				case "Last 30 Days":
					days = 30;
					break;
			}

			return days;
		}


		private void PrepareFilters(CombatLogCacheCollection FilteredLogs)
		{
			Debug.WriteLine("FilteredLogs contains " + FilteredLogs.Count + " items");
			PrepareLogDirFilter(FilteredLogs);
			PrepareListenersFilter(FilteredLogs);
			PrepareWeaponTypeFilter(FilteredLogs);
			PrepareWeaponClassFilter(FilteredLogs);
			PrepareWeaponFilter(FilteredLogs);
			PrepareTargetFilter(FilteredLogs);
			PrepareAttackerFilter(FilteredLogs);
		}

	
		private void RedrawFileList()
		{
			listView1.Items.Clear();

			int Days = GetDaysFromAgeCombo();

			// GameLogCollection FilteredLogs = new GameLogCollection();
			CombatLogCacheCollection FilteredLogs = GameLogs.LogCache.CacheData;

			//
			// Apply the day filter
			//

			if ( Days != -1 )
				FilteredLogs = FilteredLogs.FilterByAge(Days);

			Debug.WriteLine("AGE Filter: " + FilteredLogs.Count + " items");
			//
			// Apply the Listener Filter
			//
			if ( comboBoxListener.SelectedItem != null )
			{
				if ( comboBoxListener.SelectedIndex != 0 )
					FilteredLogs = FilteredLogs.FilterByCharacter(comboBoxListener.SelectedItem.ToString());
			}

			Debug.WriteLine("LISTENER Filter: " + FilteredLogs.Count + " items");

			if ( comboBoxLogDir.SelectedItem != null )
			{
				if ( comboBoxLogDir.SelectedIndex != 0 )
					FilteredLogs = FilteredLogs.FilterByLogDir(comboBoxLogDir.SelectedItem.ToString());
			}

			Debug.WriteLine("LOGDIR Filter: " + FilteredLogs.Count + " items");

			if ( cbWeaponType.SelectedItem != null )
			{
				if ( cbWeaponType.SelectedIndex != 0 )
					FilteredLogs = FilteredLogs.FilterByWeaponType((WeaponDataDB.WeaponTypeObj)cbWeaponType.SelectedItem);
			}

			Debug.WriteLine("WEAPON TYPE Filter: " + FilteredLogs.Count + " items");

			if ( cbWeaponClass.SelectedItem != null )
			{
				if ( cbWeaponClass.SelectedIndex != 0 )
					FilteredLogs = FilteredLogs.FilterByWeaponClass((WeaponDataDB.WeaponClassObj)cbWeaponClass.SelectedItem);
			}

			Debug.WriteLine("WEAPON CLASS Filter: " + FilteredLogs.Count + " items");

			if ( comboBoxWeapon.SelectedItem != null )
			{
				if ( comboBoxWeapon.SelectedIndex != 0 )
					FilteredLogs = FilteredLogs.FilterByWeapon(comboBoxWeapon.SelectedItem.ToString());
			}

			Debug.WriteLine("WEAPON NAME Filter: " + FilteredLogs.Count + " items");

			if ( comboBoxTarget.SelectedItem != null )
			{
				if ( comboBoxTarget.SelectedIndex != 0 )
					FilteredLogs = FilteredLogs.FilterByTarget(comboBoxTarget.SelectedItem.ToString());
			}

			Debug.WriteLine("TARGET NAME Filter: " + FilteredLogs.Count + " items");

			if ( comboBoxAttackers.SelectedItem != null )
			{
				if ( comboBoxAttackers.SelectedIndex != 0 )
					FilteredLogs = FilteredLogs.FilterByAttacker(comboBoxAttackers.SelectedItem.ToString());
			}

			Debug.WriteLine("ATTACKER NAME Filter: " + FilteredLogs.Count + " items");

			//
			// Now we have a completely filtered GameLog Collection. We can use this to redraw the combobox lists
			//

			PrepareFilters(FilteredLogs);

			//
			// Update the listview
			//
			listView1.BeginUpdate();
			listView1.SuspendLayout();

			listView1.Items.AddRange(GetFileItems(FilteredLogs));

			listView1.ResumeLayout();
			listView1.EndUpdate();
		}

		private ListViewItem[] GetFileItems(CombatLogCacheCollection Logs)
		{
			int n = 0;
			foreach ( CombatLogCache c in Logs )
				if ( c.IsCombatLog )
					n++;

			ListViewItem[] lvis = new ListViewItem[n];

			int i = 0;
			foreach ( CombatLogCache gl in Logs )
			{
				if ( gl.IsCombatLog )
				{
					ListViewItem l = new ListViewItem(new string[] { gl.CreationTime.ToShortDateString() + " " + gl.CreationTime.ToShortTimeString(), gl.Character, gl.FileSize.ToString(), gl.DirAlias} );
					l.Tag = gl;

					lvis[i++] = l;
				}
			}

			return lvis;
		}

		private void DoFileBrowserSort(int ColumnID)
		{
			if ( FileBrowserComparer.CurrentSortColumn == ColumnID )
			{
				if ( FileBrowserComparer.SortOrder == System.Windows.Forms.SortOrder.Ascending )
					FileBrowserComparer.SortOrder = System.Windows.Forms.SortOrder.Descending;
				else
					FileBrowserComparer.SortOrder = System.Windows.Forms.SortOrder.Ascending;
			}
			else
				FileBrowserComparer.SortOrder = System.Windows.Forms.SortOrder.Ascending;

			FileBrowserComparer.CurrentSortColumn = ColumnID;

			listView1.ListViewItemSorter = FileBrowserComparer;
			listView1.Sort();
			
			if ( listView1.SelectedItems.Count == 1 )
				listView1.EnsureVisible(listView1.SelectedItems[0].Index);
		}

		private Color GetHitTypeColor(CombatLog.HitTypes HitType)
		{
			switch ( HitType )
			{
				case CombatLog.HitTypes.Wrecking:
					return Color.Red;

				case CombatLog.HitTypes.Excellent:
					return Color.Orange;

				case CombatLog.HitTypes.CloseMiss:
					return Color.Gray;

				case CombatLog.HitTypes.Miss:
					return Color.Gray;

				case CombatLog.HitTypes.Good:
					return Color.Green;

				default:
					return Color.Black;
			}
		}


		#region Event Handlers

		private void listView1_ColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
		{
			DoFileBrowserSort(e.Column);
		}

		private void CombatLogProcessingStarted(object Sender, GameLog Log)
		{
			statusBar.progressBar.Value = 0;
			statusBar.progressBar.Minimum = 0;
			statusBar.progressBar.Maximum = (int)Log.FileSize;
		}

		private void UpdateCombatLogProcessingStatus(object Sender, GameLog Log, int PositionInFile)
		{
			statusBar.progressBar.Value = PositionInFile;
		}

		//
		// Event handler: CombatLog has been loaded and parsed.
		//

		private void CombatLogProcessingComplete(object Sender, GameLog Log)
		{
			pnl1.Text = "Combat log loaded";
		}

		//
		// Find out if a given gamelog is already being viewed
		//
		private Form LogFileOpen(GameLog g)
		{
			foreach ( Form f in this.MdiChildren )
			{
				if ( f.Tag != null )
				{
					if ( f.Tag.GetType() == typeof(GameLog) )
					{
						if ( g.FileName == ((GameLog)f.Tag).FileName && g.PathAlias == ((GameLog)f.Tag).PathAlias )
							return f;
					}
				}
			}

			return null;
		}

		//
		// User has launched a combat log for analysis
		//
		private void listView1_ItemActivate(object sender, System.EventArgs e)
		{
			if ( listView1.SelectedItems.Count != 1 )
				return;

			this.statusBar.ShowProgressBar();

			ListViewItem l = listView1.SelectedItems[0];

			if ( l.Font.Bold )
			{
				Font f = new Font(l.Font.FontFamily, l.Font.Size, FontStyle.Regular);
				l.Font = f;
			}

			pnl1.Text = "Opening...";

			CombatLogCache c = (CombatLogCache)listView1.SelectedItems[0].Tag;

			Debug.WriteLine("Opening File: " + c.FileName);

			FileInfo fi = new FileInfo(c.FileName);

			if ( fi.Length != c.FileSize )
			{
				logger.Log(Level.Warning, "Filesize for '" + c.FileName + "' changed from " + c.FileSize + " to " + fi.Length );
				Debug.WriteLine("Filesize for '" + c.FileName + "' changed.");

				logger.Log(Level.Warning, "Removing cache data for '" + c.FileName + "'");
				Debug.WriteLine("Removing cache data for '" + c.FileName + "'");

				GameLogs.LogCache.CacheData.Remove(c);

				logger.Log(Level.Warning, "Re-caching '" + c.FileName + "'");
				Debug.WriteLine("Re-caching '" + c.FileName + "'");

				RefreshFile(fi);

				logger.Log(Level.Warning, "'" + c.FileName + "' re-cached, opening log file");
				Debug.WriteLine("'" + c.FileName + "' re-cached, opening log file");

				c = GameLogs.LogCache.CacheData[c.FileName];

				if ( c == null )
				{
					Debug.WriteLine("CacheData for " + fi.Name + " is null!");
				}

				Debug.WriteLine("Cached filesize now " + c.FileSize);

				UpdateListViewItem(c);
				// RedrawFileList();
			}

			GameLog g = new GameLog(c.FileName);

			g.SessionStartedDTM = c.CreationTime;
			g.CreationTime = c.CreationTime;
			g.PathAlias = c.DirAlias;
			g.Listener = c.Character;
			g.LeafName = Path.GetFileName(c.FileName);
			g.FileSize = c.FileSize;
			g.UserComment = c.UserComment;
			
            OpenCombatLog(g);
			this.statusBar.HideProgressBar();
		}

		private void UpdateListViewItem(CombatLogCache c)
		{
			foreach ( ListViewItem l in listView1.Items )
			{
				if ( ((CombatLogCache)l.Tag).FileName == c.FileName )
				{
					l.SubItems[0].Text = c.CreationTime.ToShortDateString() + " " + c.CreationTime.ToShortTimeString();
					l.SubItems[1].Text = c.Character;
					l.SubItems[2].Text = c.FileSize.ToString();
					l.SubItems[3].Text = c.Character;
					return;
				}
			}
		}

		private void OpenCombatLog(GameLog g)
		{
			Form logWindow = LogFileOpen(g);

			if ( logWindow != null )
			{
				logWindow.BringToFront();
				return;
			}

			g.GetFile();

			Form2 f = new Form2();

			f.MyConfig = MyConfig;
			f.logger = logger;
			f.Tag = g;
			f.MdiParent = this;
			f.Text = g.LeafName + " / " + g.Listener + "@" + g.PathAlias;
			f.AllGameLogs = GameLogs;
			f.ThisGameLog = g;
			f.EOWeaponData = EOWeaponData; // Pass a pointer to the WeaponData database
			// f.Dock = DockStyle.Fill;

			f.CombatLogProcessStarted	+= new GameLog.ProcessingStartedEvent(CombatLogProcessingStarted);
			f.CombatLogProcessUpdate	+= new GameLog.ProcessingUpdateEvent(UpdateCombatLogProcessingStatus);
			f.CombatLogProcessComplete	+= new GameLog.ProcessingCompleteEvent(CombatLogProcessingComplete);

			f.PlayerNoteUpdatedHandler += new CombatLog.Form2.PlayerNoteUpdated(f_PlayerNoteUpdatedHandler);
			if ( this.MdiChildren.Length > 0 )
				f.WindowState = this.MdiChildren[0].WindowState;
			else
				f.WindowState = FormWindowState.Maximized;

			f.Show();
		}

		private void comboBoxLogDir_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			RedrawFileList();
		}

		private void menuItem1_Click(object sender, System.EventArgs e)
		{
			if ( listView1.SelectedItems.Count != 1 )
				return;

			GameLog l = (GameLog)listView1.SelectedItems[0].Tag;
			Process.Start(l.FileName);

		}


		#endregion

		private void button1_Click(object sender, System.EventArgs e)
		{
			ResetFileList();
			PrepareFileBrowser();
		}

		private void menuItem7_Click_1(object sender, System.EventArgs e)
		{
			// File browser context menu

			if ( listView1.SelectedItems.Count != 1 )
				return;

			CombatLogCache c = (CombatLogCache)listView1.SelectedItems[0].Tag;

			Process.Start(c.FileName);
		}

		private void menuItemCascade_Click(object sender, System.EventArgs e)
		{
			this.LayoutMdi(MdiLayout.Cascade);
		}

		private void menuItemTileHoriz_Click(object sender, System.EventArgs e)
		{
			this.LayoutMdi(MdiLayout.TileHorizontal);
		}

		private void menuItemTileVert_Click(object sender, System.EventArgs e)
		{
			this.LayoutMdi(MdiLayout.TileVertical);
		}

		private void menuItemExport_Click(object sender, System.EventArgs e)
		{
			if ( this.ActiveMdiChild == null )
				return;

			Form2 f = (Form2)this.ActiveMdiChild;

			saveFileDialog1.Title = "Export " + f.Text;
			saveFileDialog1.FileName = Path.ChangeExtension(f.ThisGameLog.FileName, null);

			DialogResult r = saveFileDialog1.ShowDialog();

			Debug.WriteLine("Dialog result = " + r.ToString());

			if ( r == DialogResult.OK )
			{
				try
				{
					f.ExportFile(saveFileDialog1.FileName, saveFileDialog1.FilterIndex);
				}
				catch (Exception err)
				{
					MessageBox.Show("There was a problem saving the file. Please try again. " + err.Message, "Error", MessageBoxButtons.OK);
				}
			}
		}

		private void menuItem1_Popup(object sender, System.EventArgs e)
		{
			if ( this.ActiveMdiChild == null )
				menuItemExport.Enabled = false;
			else
				menuItemExport.Enabled = true;
		}

		private void menuItem1About_Click(object sender, System.EventArgs e)
		{
			AboutForm f = new AboutForm();
			f.StartPosition = FormStartPosition.CenterParent;
			f.ShowDialog();
		}

		private void menuItem2_Click(object sender, System.EventArgs e)
		{
			//Application.Exit();
            this.Close();
		}

		private void menuItem11_Click(object sender, System.EventArgs e)
		{
			Options opt = new Options();
			opt.MyConfig = MyConfig;

			DialogResult r = opt.ShowDialog();

			if ( r != DialogResult.OK )
				return;

			MyConfig = opt.MyConfig;
			MyConfig.GameLogDirs = opt.MyConfig.GameLogDirs;

			SaveMyConfig();
		}

		private void menuItem4_Select(object sender, System.EventArgs e)
		{

		}

		private void menuItem12_Click(object sender, System.EventArgs e)
		{
			// Process.Start("ReleaseNotes.txt");

			TextForm tf = new TextForm(Application.StartupPath + @"\ReleaseNotes.txt");
			tf.MdiParent = this;
			tf.Text = "ReleaseNotes.txt";
			tf.Show();
		}

		private void mbMerge_Click(object sender, System.EventArgs e)
		{
			if ( this.MdiChildren.Length == 0 )
				return;

			StartCombatLogMerge();
		}

		private void StartCombatLogMerge()
		{
			Form2 DestinationForm = (Form2)this.ActiveMdiChild;

			GameLog[] TheseLogs = new GameLog[listView1.SelectedItems.Count];

			int i=0;
			foreach (ListViewItem l in listView1.SelectedItems )
				TheseLogs[i++] = (GameLog)l.Tag;

			GameLog DestinationLog = (GameLog)DestinationForm.Tag;

			DestinationForm.Merge(TheseLogs);
		}

		private void Form1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			SaveLayout();
			SaveMyConfig();

			if (GameLogCacheUpdated)
			{
				SaveGameLogCache(GameLogs);
			}

			StopLogging();

		}

		private void notifyIcon1_Click(object sender, System.EventArgs e)
		{
			if ( MyConfig.MinimiseToSystemTray )
			{
				//
				// The Notify Icon has been clicked, open the main form and remove the NotifyIcon
				//
				if ( this.WindowState == FormWindowState.Minimized )
				{
					this.WindowState = FormWindowState.Normal;
					this.notifyIcon1.Visible = false;
				}
			}
		}

		private void SaveLayout()
		{
			if ( MyConfig.RememberColumnWidths )
				MyConfig.FileBrowserListViewSettings = new SaveListView.ListViewSettings( listView1 );
			else
				MyConfig.FileBrowserListViewSettings = null;

			if ( MyConfig.RememberWindowLayout )
				MyConfig.MainWindowLayout = sandDockManager1.GetLayout();
			else
				MyConfig.MainWindowLayout = null;
		}

		public static byte[] SerializeListViewToMemory(SaveListView.ListViewSettings Data)
		{			
			MemoryStream ms = new MemoryStream();

			XmlSerializer x = new XmlSerializer(typeof(ListViewSettings));

			x.Serialize(ms, Data);

			byte[] ResultsBuffer = (byte[])ms.ToArray();
			return ResultsBuffer;
		}

		private void menuItemBestShotsReport_Click(object sender, System.EventArgs e)
		{
			//
			// Is the best shots report window already open?
			//
			foreach (Form f in this.MdiChildren)
			{
				if ( f.Tag.ToString() == "BESTSHOTS")
				{
					f.BringToFront();
					return;
				}
			}

			BestShots bs = new BestShots();
			bs.CombatLogActivated += new BestShots.CombatLogActivatedEvent(OpenCombatLog);
			bs.AllGameLogs = GameLogs;
			bs.EOWeaponData = EOWeaponData;
			bs.Tag = "BESTSHOTS";
			bs.Text = "All Time Best Shot Records";
			bs.MdiParent = this;
			bs.Show();
		}

		private void OpenCombatLog(object sender, GameLog g)
		{
			Debug.WriteLine("Request to launch: " + g.FileName + " received");
			OpenCombatLog(g);
		}

		private void menuItemHomePage_Click(object sender, System.EventArgs e)
		{
			Process.Start(@"http://utter.chaos.org.uk/~dean/cla/");
		}

		private void menuItemForum_Click(object sender, System.EventArgs e)
		{
			Process.Start(@"http://utter.chaos.org.uk/~dean/cla/");
		}

		private void menuItem12_Click_1(object sender, System.EventArgs e)
		{
			foreach ( Form f in this.MdiChildren )
			{
				if ( f.Tag.ToString() == "CACHEDIAGNOSTICS" )
				{
					f.BringToFront();
					return;
				}
			}

			CacheReport.CacheReport cr = new CacheReport.CacheReport();
			cr.CacheFileName = ApplicationDataDirectory + "LogCache.xml";
			cr.GameLogs = GameLogs;
			cr.MdiParent = this;
			cr.Tag = "CACHEDIAGNOSTICS";
			cr.Text = "Cache File Diagnostics Report";
			cr.Show();
		}

		private string GetCacheDiagnosticsReport()
		{
			string nl = Environment.NewLine;
			StringBuilder rep = new StringBuilder();

			rep.Append("GameLog Collection has " + GameLogs.Count + " members");
			rep.Append(nl);

			if ( GameLogs.LogCache != null )
			{
				rep.Append("Cache active");
				rep.Append(nl);
				rep.Append("Cache contains " + GameLogs.LogCache.CacheData.Count + " members");

			}

			return rep.ToString();
		}

		private delegate void ShowNotifyWindowDelegate(string Character,string Location, string Targets);
		private void ShowNotifyWindow(string FileName, string Character, string Targets)
		{
			if ( !this.InvokeRequired )
			{
				lock(this)
				{
					CombatLog.Panes.Notify n = new CombatLog.Panes.Notify();
					n.ActiveNotifyWindows = ActiveNotifyWindows;
					n.EVELocation = Character;
					n.Targets = Targets;

					n.Show();

					ActiveNotifyWindows.Add(n);
				}
			}
			else
			{
				ShowNotifyWindowDelegate sn = new ShowNotifyWindowDelegate(ShowNotifyWindow);
				Invoke(sn,new object[] {FileName, Character, Targets});
			}
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			CombatLog.Panes.Notify n = new CombatLog.Panes.Notify();
			n.ActiveNotifyWindows = ActiveNotifyWindows;
			n.EVELocation = "Tash-Murkon Prime";
			n.Targets = "Domination General, Arch Angel Centurion";

			n.Show();

			ActiveNotifyWindows.Add(n);
		}

		private void menuItem12_Click_2(object sender, System.EventArgs e)
		{
			DialogResult r = MessageBox.Show("Clearing the cache will result in the cache being rebuilt when the application is next run. If you have a large number of Gamelog files, this could take a while." + Environment.NewLine + Environment.NewLine + "The application will automatically quit once the cache has been cleared." + Environment.NewLine + Environment.NewLine + "Do you want to proceed?", "Are you sure?", MessageBoxButtons.YesNo);

			if ( r == DialogResult.Yes )
			{
				File.Delete(ApplicationDataDirectory + "LogCache.xml");
				this.Close();
			}
		}

		private void ResetFileList()
		{
			// Disable combobox events
			// call redrawfilelist

			comboBoxLogAge.SelectedIndexChanged		-= new System.EventHandler(this.comboBoxLogDir_SelectedIndexChanged);
			comboBoxLogDir.SelectedIndexChanged		-= new System.EventHandler(this.comboBoxLogDir_SelectedIndexChanged);
			comboBoxListener.SelectedIndexChanged	-= new System.EventHandler(this.comboBoxLogDir_SelectedIndexChanged);
			comboBoxTarget.SelectedIndexChanged		-= new System.EventHandler(this.comboBoxLogDir_SelectedIndexChanged);
			cbWeaponType.SelectedIndexChanged		-= new System.EventHandler(this.comboBoxLogDir_SelectedIndexChanged);
			cbWeaponClass.SelectedIndexChanged		-= new System.EventHandler(this.comboBoxLogDir_SelectedIndexChanged);
			comboBoxWeapon.SelectedIndexChanged		-= new System.EventHandler(this.comboBoxLogDir_SelectedIndexChanged);
			comboBoxAttackers.SelectedIndexChanged	-= new System.EventHandler(this.comboBoxLogDir_SelectedIndexChanged);

			comboBoxLogAge.SelectedIndex = 0;
			comboBoxLogDir.SelectedIndex = 0;
			comboBoxListener.SelectedIndex = 0;
			comboBoxTarget.SelectedIndex = 0;
			cbWeaponType.SelectedIndex = 0;
			cbWeaponClass.SelectedIndex = 0;
			comboBoxWeapon.SelectedIndex = 0;
			comboBoxAttackers.SelectedIndex = 0;

			comboBoxLogAge.SelectedIndexChanged		+= new System.EventHandler(this.comboBoxLogDir_SelectedIndexChanged);
			comboBoxLogDir.SelectedIndexChanged		+= new System.EventHandler(this.comboBoxLogDir_SelectedIndexChanged);
			comboBoxListener.SelectedIndexChanged	+= new System.EventHandler(this.comboBoxLogDir_SelectedIndexChanged);
			comboBoxTarget.SelectedIndexChanged		+= new System.EventHandler(this.comboBoxLogDir_SelectedIndexChanged);
			cbWeaponType.SelectedIndexChanged		+= new System.EventHandler(this.comboBoxLogDir_SelectedIndexChanged);
			cbWeaponClass.SelectedIndexChanged		+= new System.EventHandler(this.comboBoxLogDir_SelectedIndexChanged);
			comboBoxWeapon.SelectedIndexChanged		+= new System.EventHandler(this.comboBoxLogDir_SelectedIndexChanged);
			comboBoxAttackers.SelectedIndexChanged	+= new System.EventHandler(this.comboBoxLogDir_SelectedIndexChanged);

			RedrawFileList();

		}
		private void btnClearFilters_Click(object sender, System.EventArgs e)
		{
			ResetFileList();
		}

		private void menuItem17_Click(object sender, System.EventArgs e)
		{
			CheckForNewVersion();
		}

		private VersionInfoCollection GetAvailableVersions()
		{
			return null;
		}

		//
		// Create a 5 second timer which will start the new version checking
		// procedure when it ticks
		//
		private void VersionCheckTimer()
		{
			if ( !MyConfig.CheckForNewVersion )
				return;

			Debug.WriteLine("Startup timer started");
			Timer VsnCheckTimer = new Timer();
			VsnCheckTimer.Interval = 2000; // Run in 5 seconds

			VsnCheckTimer.Tick += new EventHandler(VsnCheckTimer_Tick);

			VsnCheckTimer.Start();

		}

		//
		// This is the quiet version of the new version checker. The update dialog is only
		// shown if there is a new version available.
		//
		private void DoStartupVersionCheck()
		{
			Debug.WriteLine("DoStartupVersionCheck entered");

			try
			{
				App.VersionChecker.MyConfig = MyConfig;
				VersionInfoCollection Versions = App.VersionChecker.LoadFromWeb(CheckVersionURL);
			}
			catch (Exception e)
			{
				MessageBox.Show("There was a problem downloading the latest release information. If the problem persists, please contact the author." + Environment.NewLine + Environment.NewLine + e.ToString(), "Error getting release information", MessageBoxButtons.OK);
				logger.Log(Level.Exception, "Error getting version history data: " + e.ToString());
				return;
			}

			if ( App.VersionChecker.LatestVersion().VersionNumber > CurrentVersion.VersionNumber )
			{
				// There is a new version available

				CheckForNewVersion();
			}
		}

		//
		// User invoked version check
		//
		// Update dialog will be shown regardless of there being an update available
		//
		private void CheckForNewVersion()
		{
			App.VersionChecker.MyConfig = MyConfig;

			VersionInfoCollection Versions = null;

			try
			{
				Versions = App.VersionChecker.LoadFromWeb(CheckVersionURL);
				NewVersionForm nvf = new NewVersionForm();
				nvf.CurrentVersion = CurrentVersion;
				nvf.LatestVersion = App.VersionChecker.LatestVersion();
				nvf.ShowDialog();
			}
			catch (Exception e)
			{
				MessageBox.Show("There was a problem getting the latest release information. If the problem persists, please send the following error message to the author." + Environment.NewLine + Environment.NewLine + e.Message, "Error getting version information from web", MessageBoxButtons.OK);
				return;
			}
		}

		private void VsnCheckTimer_Tick(object sender, EventArgs e)
		{
			Debug.WriteLine("Startup timer ticked");

			Timer t = (Timer)sender;
			t.Stop();

			DoStartupVersionCheck();
		}

		private void ShowWeaponInfo(string WeaponName)
		{
			if ( EOWeaponData == null )
			{
				try
				{
					LoadEOWeaponData();
				}
				catch (Exception err)
				{
					MessageBox.Show("There was a problem loading the weapon data file. Unable to continue. Please try re-installing the EVE Combat Log Analyser ("+err.ToString()+")", "No Weapon Data", MessageBoxButtons.OK);
					return;
				}
			}

			WeaponForm f = new WeaponForm();
			f.MyConfig = MyConfig;
			f.WeaponData = EOWeaponData;

			if ( WeaponName != null )
				f.SearchWeaponName = WeaponName;

			f.Show();
		}

		private void menuItemWeaponInfo_Click(object sender, System.EventArgs e)
		{
			ShowWeaponInfo(null);
		}

		private void f_PlayerNoteUpdatedHandler(object Sender, GameLog GameLogObj)
		{
			// MessageBox.Show("Player note updated: '" + GameLogObj.UserComment + "'");
			GameLogCacheUpdated = true;
			Debug.WriteLine("Updated cache entry for " + GameLogs.LogCache.CacheData[GameLogObj.FileName].FileName);
			GameLogs.LogCache.CacheData[GameLogObj.FileName].UserComment = GameLogObj.UserComment;
			//GameLogs[GameLogObj.LeafName].UserComment = GameLogObj.UserComment;
		}
	}
}
