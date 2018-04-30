using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Text;
using NSpring.Logging;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using SaveListView;
using CombatLog.Config;

namespace CombatLog
{
	/// <summary>
	/// Summary description for Form2.
	/// </summary>
	public class Form2 : System.Windows.Forms.Form
	{

		#region Properties
		private const string ItemDataURL = "http://www.eve-i.com/home/crowley/page/page_ptype.php?id=";

		public CombatLog.WeaponDataDB.WeaponDataSourceCollection EOWeaponData = null;

		private Timer SelectedIndexTimer = new Timer();
		private Timer AttackSelectedIndexTimer = new Timer();

		public CombatLog.Config.UserConfig MyConfig;

		private bool LastRedrawWasUserSelection = false;
		private bool LastAttackRedrawWasUserSelection = false;

		private GraphItemCollection GraphControls = new GraphItemCollection();
		private CombatLogLVSorter CombatSummarySorter = new CombatLogLVSorter();
		private AttackLogLVSorter AttackLogSorter = new AttackLogLVSorter();

		public NSpring.Logging.Logger logger;

		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label lblWeaponFilter;
		private System.Windows.Forms.Button btnWeaponFilter;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button btnHitTypeMenu;
		private System.Windows.Forms.Button btnTargetFilter;
		private System.Windows.Forms.Label lblHitType;
		private System.Windows.Forms.Label lblTargetFilter;
		private System.Windows.Forms.ListView listViewCombatSummary;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.ColumnHeader columnHeader7;
		private System.Windows.Forms.ColumnHeader columnHeader8;
		private System.Windows.Forms.ColumnHeader columnHeader9;
		private System.Windows.Forms.ColumnHeader columnHeader15;
		private System.Windows.Forms.ColumnHeader columnHeader16;
		private System.Windows.Forms.ColumnHeader columnHeader10;
		private System.Windows.Forms.ColumnHeader columnHeader11;
		private System.Windows.Forms.ColumnHeader columnHeader12;
		private System.Windows.Forms.ColumnHeader columnHeader13;
		private System.Windows.Forms.ColumnHeader columnHeader14;
		private System.ComponentModel.IContainer components;
		private TD.SandBar.SandBarManager sandBarManager1;
		private TD.SandBar.ToolBarContainer leftSandBarDock;
		private TD.SandBar.ToolBarContainer rightSandBarDock;
		private TD.SandBar.ToolBarContainer bottomSandBarDock;
		private TD.SandBar.ToolBarContainer topSandBarDock;
		private TD.SandBar.MenuBar menuBar1;
		private TD.SandBar.ContextMenuBarItem contextMenuCombatSummary;
		private TD.SandBar.MenuButtonItem menuItemFindWeapon;
		private TD.SandBar.MenuButtonItem menuItemFindTarget;
		private TD.SandBar.ContextMenuBarItem contextMenuBarItem1;
		private TD.SandBar.MenuButtonItem menuButtonItem1;
		private TD.SandBar.MenuButtonItem menuButtonItem2;
		private TD.SandBar.ContextMenuBarItem contextMenuWeapon;
		private TD.SandBar.MenuButtonItem menuButtonItem3;
		private TD.SandBar.ContextMenuBarItem contextMenuHitTypes;
		private TD.SandBar.ContextMenuBarItem contextMenuTarget;
		private TD.SandBar.MenuButtonItem menuButtonItem4;
		private TD.SandBar.MenuButtonItem menuButtonItem5;
		private System.Windows.Forms.Panel panelMainMiddle;

		public delegate void PlayerNoteUpdated(object Sender, GameLog GameLogObj);
		public event PlayerNoteUpdated PlayerNoteUpdatedHandler = null;

		public event GameLog.ProcessingStartedEvent CombatLogProcessStarted = null;
		public event GameLog.ProcessingUpdateEvent CombatLogProcessUpdate = null;
		public event GameLog.ProcessingCompleteEvent CombatLogProcessComplete = null;

		public GameLog ThisGameLog;
		private System.Windows.Forms.ContextMenu contextMenuItemInfo;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private TD.SandDock.SandDockManager sandDockManager1;
		private TD.SandDock.DockContainer leftSandDock;
		private TD.SandDock.DockContainer rightSandDock;
		private TD.SandDock.DockContainer bottomSandDock;
		private TD.SandDock.DockContainer topSandDock;
		private TD.SandDock.DockControl dockControl2;
		private TD.SandDock.DockControl dockControl3;
		private AxSHDocVw.AxWebBrowser axWebBrowser1;
		private System.Windows.Forms.Panel panelSummaryBottom;
		private System.Windows.Forms.MenuItem menuItem6;
		private System.Windows.Forms.MenuItem menuItem7;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.Panel panelMainTop;
		private System.Windows.Forms.Panel panelBottom;
		private TD.SandDock.DocumentContainer documentContainer1;
		private TD.SandDock.DockControl dockControlDamage;
		private TD.SandDock.DockControl dockControlAttackers;
		private System.Windows.Forms.ColumnHeader columnHeader19;
		private System.Windows.Forms.ColumnHeader columnHeader20;
		private System.Windows.Forms.ColumnHeader columnHeader21;
		private System.Windows.Forms.ColumnHeader columnHeader22;
		private System.Windows.Forms.ColumnHeader columnHeader23;
		private System.Windows.Forms.ListView listViewAttackData;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.ColumnHeader columnHeader24;
		private System.Windows.Forms.ColumnHeader columnHeader25;
		private System.Windows.Forms.ColumnHeader columnHeader26;
		private System.Windows.Forms.ColumnHeader columnHeader27;
		private System.Windows.Forms.ColumnHeader columnHeader28;
		private System.Windows.Forms.ColumnHeader columnHeader29;
		private System.Windows.Forms.ColumnHeader columnHeader30;
		private System.Windows.Forms.ColumnHeader columnHeader31;
		private System.Windows.Forms.ColumnHeader columnHeader32;
		private System.Windows.Forms.ColumnHeader columnHeader33;
		private System.Windows.Forms.ColumnHeader columnHeader34;
		private TD.SandBar.ContextMenuBarItem contextMenuAttackWeapon;
		private TD.SandBar.ContextMenuBarItem contextMenuAttackHitType;
		private TD.SandBar.ContextMenuBarItem contextMenuAttackAttacker;
		private System.Windows.Forms.Label lblAttackWeapon;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label lblAttacker;
		private System.Windows.Forms.Label lblAttackHitType;
		private System.Windows.Forms.Label lblAttackAttacker;
		private System.Windows.Forms.Button btnAttackWeapon;
		private System.Windows.Forms.Button btnAttackHitType;
		private System.Windows.Forms.Button btnAttackAttacker;
		private System.Windows.Forms.ListView listViewAttackSummary;
		private System.Windows.Forms.ContextMenu contextMenuAttackItemInfo;
		private System.Windows.Forms.MenuItem menuItemAttackCopyText;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItemAttackWeaponInfo;
		private System.Windows.Forms.MenuItem menuItemAttackAttackerInfo;
		private System.Windows.Forms.ListView listViewCombatSummaryStats;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.Splitter splitterAttacker;
		private System.Windows.Forms.MenuItem menuItemWeaponInfo;
		private System.Windows.Forms.MenuItem menuItemTargetInfo;
		private System.Windows.Forms.Button button1;
		private TD.SandDock.DockControl dockControl1;
		private System.Windows.Forms.RichTextBox rtbSummary;
		private System.Windows.Forms.ColumnHeader columnHeader17;
		private System.Windows.Forms.ColumnHeader columnHeader18;
		private TD.SandDock.DockControl dockControl4;
		private System.Windows.Forms.RichTextBox tbPlayerNotes;
		private System.Windows.Forms.CheckBox cbShowNotifyMessages;
		public GameLogCollection AllGameLogs;
		#endregion
		public Form2()
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
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Form2));
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.button1 = new System.Windows.Forms.Button();
			this.lblWeaponFilter = new System.Windows.Forms.Label();
			this.btnWeaponFilter = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.btnHitTypeMenu = new System.Windows.Forms.Button();
			this.btnTargetFilter = new System.Windows.Forms.Button();
			this.lblHitType = new System.Windows.Forms.Label();
			this.lblTargetFilter = new System.Windows.Forms.Label();
			this.listViewCombatSummary = new System.Windows.Forms.ListView();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
			this.contextMenuItemInfo = new System.Windows.Forms.ContextMenu();
			this.menuItem6 = new System.Windows.Forms.MenuItem();
			this.menuItem7 = new System.Windows.Forms.MenuItem();
			this.menuItemWeaponInfo = new System.Windows.Forms.MenuItem();
			this.menuItemTargetInfo = new System.Windows.Forms.MenuItem();
			this.listViewCombatSummaryStats = new System.Windows.Forms.ListView();
			this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader9 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader15 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader16 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader10 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader11 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader12 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader13 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader14 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader17 = new System.Windows.Forms.ColumnHeader();
			this.sandBarManager1 = new TD.SandBar.SandBarManager();
			this.bottomSandBarDock = new TD.SandBar.ToolBarContainer();
			this.leftSandBarDock = new TD.SandBar.ToolBarContainer();
			this.rightSandBarDock = new TD.SandBar.ToolBarContainer();
			this.topSandBarDock = new TD.SandBar.ToolBarContainer();
			this.menuBar1 = new TD.SandBar.MenuBar();
			this.contextMenuWeapon = new TD.SandBar.ContextMenuBarItem();
			this.menuButtonItem3 = new TD.SandBar.MenuButtonItem();
			this.contextMenuHitTypes = new TD.SandBar.ContextMenuBarItem();
			this.menuButtonItem4 = new TD.SandBar.MenuButtonItem();
			this.contextMenuTarget = new TD.SandBar.ContextMenuBarItem();
			this.menuButtonItem5 = new TD.SandBar.MenuButtonItem();
			this.contextMenuAttackWeapon = new TD.SandBar.ContextMenuBarItem();
			this.contextMenuAttackHitType = new TD.SandBar.ContextMenuBarItem();
			this.contextMenuAttackAttacker = new TD.SandBar.ContextMenuBarItem();
			this.contextMenuCombatSummary = new TD.SandBar.ContextMenuBarItem();
			this.menuItemFindWeapon = new TD.SandBar.MenuButtonItem();
			this.menuItemFindTarget = new TD.SandBar.MenuButtonItem();
			this.contextMenuBarItem1 = new TD.SandBar.ContextMenuBarItem();
			this.menuButtonItem1 = new TD.SandBar.MenuButtonItem();
			this.menuButtonItem2 = new TD.SandBar.MenuButtonItem();
			this.panelMainTop = new System.Windows.Forms.Panel();
			this.panelBottom = new System.Windows.Forms.Panel();
			this.panelMainMiddle = new System.Windows.Forms.Panel();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.btnAttackWeapon = new System.Windows.Forms.Button();
			this.btnAttackHitType = new System.Windows.Forms.Button();
			this.btnAttackAttacker = new System.Windows.Forms.Button();
			this.sandDockManager1 = new TD.SandDock.SandDockManager();
			this.leftSandDock = new TD.SandDock.DockContainer();
			this.rightSandDock = new TD.SandDock.DockContainer();
			this.dockControl3 = new TD.SandDock.DockControl();
			this.panelSummaryBottom = new System.Windows.Forms.Panel();
			this.axWebBrowser1 = new AxSHDocVw.AxWebBrowser();
			this.dockControl2 = new TD.SandDock.DockControl();
			this.dockControl4 = new TD.SandDock.DockControl();
			this.tbPlayerNotes = new System.Windows.Forms.RichTextBox();
			this.bottomSandDock = new TD.SandDock.DockContainer();
			this.topSandDock = new TD.SandDock.DockContainer();
			this.documentContainer1 = new TD.SandDock.DocumentContainer();
			this.dockControlDamage = new TD.SandDock.DockControl();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.dockControlAttackers = new TD.SandDock.DockControl();
			this.splitterAttacker = new System.Windows.Forms.Splitter();
			this.listViewAttackData = new System.Windows.Forms.ListView();
			this.columnHeader19 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader22 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader20 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader21 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader23 = new System.Windows.Forms.ColumnHeader();
			this.contextMenuAttackItemInfo = new System.Windows.Forms.ContextMenu();
			this.menuItemAttackCopyText = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItemAttackWeaponInfo = new System.Windows.Forms.MenuItem();
			this.menuItemAttackAttackerInfo = new System.Windows.Forms.MenuItem();
			this.listViewAttackSummary = new System.Windows.Forms.ListView();
			this.columnHeader24 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader25 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader26 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader27 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader28 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader29 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader30 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader31 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader32 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader33 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader34 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader18 = new System.Windows.Forms.ColumnHeader();
			this.panel1 = new System.Windows.Forms.Panel();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.lblAttackWeapon = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.lblAttacker = new System.Windows.Forms.Label();
			this.lblAttackHitType = new System.Windows.Forms.Label();
			this.lblAttackAttacker = new System.Windows.Forms.Label();
			this.dockControl1 = new TD.SandDock.DockControl();
			this.rtbSummary = new System.Windows.Forms.RichTextBox();
			this.cbShowNotifyMessages = new System.Windows.Forms.CheckBox();
			this.groupBox2.SuspendLayout();
			this.topSandBarDock.SuspendLayout();
			this.panelMainTop.SuspendLayout();
			this.panelBottom.SuspendLayout();
			this.panelMainMiddle.SuspendLayout();
			this.rightSandDock.SuspendLayout();
			this.dockControl3.SuspendLayout();
			this.panelSummaryBottom.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.axWebBrowser1)).BeginInit();
			this.dockControl4.SuspendLayout();
			this.documentContainer1.SuspendLayout();
			this.dockControlDamage.SuspendLayout();
			this.dockControlAttackers.SuspendLayout();
			this.panel1.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.dockControl1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox2
			// 
			this.groupBox2.BackColor = System.Drawing.SystemColors.ControlLight;
			this.groupBox2.Controls.Add(this.cbShowNotifyMessages);
			this.groupBox2.Controls.Add(this.button1);
			this.groupBox2.Controls.Add(this.lblWeaponFilter);
			this.groupBox2.Controls.Add(this.btnWeaponFilter);
			this.groupBox2.Controls.Add(this.label3);
			this.groupBox2.Controls.Add(this.label4);
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.Controls.Add(this.btnHitTypeMenu);
			this.groupBox2.Controls.Add(this.btnTargetFilter);
			this.groupBox2.Controls.Add(this.lblHitType);
			this.groupBox2.Controls.Add(this.lblTargetFilter);
			this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
			this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox2.Location = new System.Drawing.Point(0, 0);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(746, 96);
			this.groupBox2.TabIndex = 3;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Display Filters";
			// 
			// button1
			// 
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.button1.Location = new System.Drawing.Point(368, 16);
			this.button1.Name = "button1";
			this.button1.TabIndex = 5;
			this.button1.Text = "Graph Test";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// lblWeaponFilter
			// 
			this.lblWeaponFilter.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.lblWeaponFilter.Location = new System.Drawing.Point(72, 16);
			this.lblWeaponFilter.Name = "lblWeaponFilter";
			this.lblWeaponFilter.Size = new System.Drawing.Size(248, 23);
			this.lblWeaponFilter.TabIndex = 4;
			this.lblWeaponFilter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnWeaponFilter
			// 
			this.btnWeaponFilter.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnWeaponFilter.Location = new System.Drawing.Point(328, 16);
			this.btnWeaponFilter.Name = "btnWeaponFilter";
			this.btnWeaponFilter.Size = new System.Drawing.Size(24, 23);
			this.btnWeaponFilter.TabIndex = 3;
			this.btnWeaponFilter.Text = "...";
			this.toolTip1.SetToolTip(this.btnWeaponFilter, "Hold down the CTRL key when selecting an item to add it to the filter");
			this.btnWeaponFilter.Click += new System.EventHandler(this.btnWeaponFilter_Click);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 16);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(56, 23);
			this.label3.TabIndex = 1;
			this.label3.Text = "Weapon";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 40);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(56, 23);
			this.label4.TabIndex = 1;
			this.label4.Text = "Hit Type";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(8, 64);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(56, 23);
			this.label5.TabIndex = 1;
			this.label5.Text = "Target";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnHitTypeMenu
			// 
			this.btnHitTypeMenu.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnHitTypeMenu.Location = new System.Drawing.Point(328, 40);
			this.btnHitTypeMenu.Name = "btnHitTypeMenu";
			this.btnHitTypeMenu.Size = new System.Drawing.Size(24, 23);
			this.btnHitTypeMenu.TabIndex = 3;
			this.btnHitTypeMenu.Text = "...";
			this.toolTip1.SetToolTip(this.btnHitTypeMenu, "Hold down the CTRL key when selecting an item to add it to the filter");
			this.btnHitTypeMenu.Click += new System.EventHandler(this.btnHitTypeMenu_Click);
			// 
			// btnTargetFilter
			// 
			this.btnTargetFilter.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnTargetFilter.Location = new System.Drawing.Point(328, 64);
			this.btnTargetFilter.Name = "btnTargetFilter";
			this.btnTargetFilter.Size = new System.Drawing.Size(24, 23);
			this.btnTargetFilter.TabIndex = 3;
			this.btnTargetFilter.Text = "...";
			this.toolTip1.SetToolTip(this.btnTargetFilter, "Hold down the CTRL key when selecting an item to add it to the filter");
			this.btnTargetFilter.Click += new System.EventHandler(this.btnTargetFilter_Click);
			// 
			// lblHitType
			// 
			this.lblHitType.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.lblHitType.Location = new System.Drawing.Point(72, 40);
			this.lblHitType.Name = "lblHitType";
			this.lblHitType.Size = new System.Drawing.Size(248, 23);
			this.lblHitType.TabIndex = 4;
			this.lblHitType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblTargetFilter
			// 
			this.lblTargetFilter.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.lblTargetFilter.Location = new System.Drawing.Point(72, 64);
			this.lblTargetFilter.Name = "lblTargetFilter";
			this.lblTargetFilter.Size = new System.Drawing.Size(248, 23);
			this.lblTargetFilter.TabIndex = 4;
			this.lblTargetFilter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// listViewCombatSummary
			// 
			this.listViewCombatSummary.AllowColumnReorder = true;
			this.listViewCombatSummary.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																									this.columnHeader3,
																									this.columnHeader4,
																									this.columnHeader5,
																									this.columnHeader6,
																									this.columnHeader7});
			this.listViewCombatSummary.ContextMenu = this.contextMenuItemInfo;
			this.listViewCombatSummary.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listViewCombatSummary.FullRowSelect = true;
			this.listViewCombatSummary.HideSelection = false;
			this.listViewCombatSummary.Location = new System.Drawing.Point(0, 0);
			this.listViewCombatSummary.Name = "listViewCombatSummary";
			this.listViewCombatSummary.Size = new System.Drawing.Size(746, 323);
			this.listViewCombatSummary.TabIndex = 4;
			this.listViewCombatSummary.View = System.Windows.Forms.View.Details;
			this.listViewCombatSummary.DragOver += new System.Windows.Forms.DragEventHandler(this.listViewCombatSummary_DragOver);
			this.listViewCombatSummary.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listViewCombatSummary_ColumnClick);
			this.listViewCombatSummary.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.listViewCombatSummary_ItemDrag);
			this.listViewCombatSummary.SelectedIndexChanged += new System.EventHandler(this.listViewCombatSummary_SelectedIndexChanged);
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Date/Time";
			this.columnHeader3.Width = 116;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Weapon";
			this.columnHeader4.Width = 185;
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "Hit Type";
			this.columnHeader5.Width = 93;
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "Target";
			this.columnHeader6.Width = 138;
			// 
			// columnHeader7
			// 
			this.columnHeader7.Text = "Damage";
			this.columnHeader7.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// contextMenuItemInfo
			// 
			this.contextMenuItemInfo.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																								this.menuItem6,
																								this.menuItem7,
																								this.menuItemWeaponInfo,
																								this.menuItemTargetInfo});
			// 
			// menuItem6
			// 
			this.menuItem6.Index = 0;
			this.menuItem6.Text = "Copy log text";
			this.menuItem6.Click += new System.EventHandler(this.menuItem6_Click);
			// 
			// menuItem7
			// 
			this.menuItem7.Index = 1;
			this.menuItem7.Text = "-";
			// 
			// menuItemWeaponInfo
			// 
			this.menuItemWeaponInfo.Index = 2;
			this.menuItemWeaponInfo.Text = "Weapon info...";
			this.menuItemWeaponInfo.Click += new System.EventHandler(this.menuItemWeaponInfo_Click);
			// 
			// menuItemTargetInfo
			// 
			this.menuItemTargetInfo.Index = 3;
			this.menuItemTargetInfo.Text = "Target info...";
			this.menuItemTargetInfo.Click += new System.EventHandler(this.menuItemTargetInfo_Click);
			// 
			// listViewCombatSummaryStats
			// 
			this.listViewCombatSummaryStats.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																										 this.columnHeader8,
																										 this.columnHeader9,
																										 this.columnHeader15,
																										 this.columnHeader16,
																										 this.columnHeader10,
																										 this.columnHeader11,
																										 this.columnHeader12,
																										 this.columnHeader13,
																										 this.columnHeader14,
																										 this.columnHeader2,
																										 this.columnHeader1,
																										 this.columnHeader17});
			this.listViewCombatSummaryStats.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listViewCombatSummaryStats.Enabled = false;
			this.listViewCombatSummaryStats.FullRowSelect = true;
			this.listViewCombatSummaryStats.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.listViewCombatSummaryStats.Location = new System.Drawing.Point(0, 0);
			this.listViewCombatSummaryStats.Name = "listViewCombatSummaryStats";
			this.listViewCombatSummaryStats.Size = new System.Drawing.Size(746, 128);
			this.listViewCombatSummaryStats.TabIndex = 5;
			this.listViewCombatSummaryStats.View = System.Windows.Forms.View.Details;
			this.listViewCombatSummaryStats.ItemActivate += new System.EventHandler(this.listView2_ItemActivate);
			// 
			// columnHeader8
			// 
			this.columnHeader8.Text = "Weapon Name";
			this.columnHeader8.Width = 145;
			// 
			// columnHeader9
			// 
			this.columnHeader9.Text = "Fired";
			this.columnHeader9.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.columnHeader9.Width = 43;
			// 
			// columnHeader15
			// 
			this.columnHeader15.Text = "Hit";
			this.columnHeader15.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.columnHeader15.Width = 41;
			// 
			// columnHeader16
			// 
			this.columnHeader16.Text = "Missed";
			this.columnHeader16.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.columnHeader16.Width = 50;
			// 
			// columnHeader10
			// 
			this.columnHeader10.Text = "Tot. Dmg";
			this.columnHeader10.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// columnHeader11
			// 
			this.columnHeader11.Text = "Avg. Dmg";
			this.columnHeader11.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.columnHeader11.Width = 66;
			// 
			// columnHeader12
			// 
			this.columnHeader12.Text = "% Hit";
			this.columnHeader12.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.columnHeader12.Width = 49;
			// 
			// columnHeader13
			// 
			this.columnHeader13.Text = "% Missed";
			this.columnHeader13.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.columnHeader13.Width = 66;
			// 
			// columnHeader14
			// 
			this.columnHeader14.Text = "Duration";
			this.columnHeader14.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Shots/Min";
			this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.columnHeader2.Width = 66;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Dmg/Min";
			this.columnHeader1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// columnHeader17
			// 
			this.columnHeader17.Text = "DPS";
			this.columnHeader17.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// sandBarManager1
			// 
			this.sandBarManager1.BottomContainer = this.bottomSandBarDock;
			this.sandBarManager1.LeftContainer = this.leftSandBarDock;
			this.sandBarManager1.OwnerForm = this;
			this.sandBarManager1.Renderer = new TD.SandBar.WhidbeyRenderer();
			this.sandBarManager1.RightContainer = this.rightSandBarDock;
			this.sandBarManager1.TopContainer = this.topSandBarDock;
			// 
			// bottomSandBarDock
			// 
			this.bottomSandBarDock.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.bottomSandBarDock.Location = new System.Drawing.Point(0, 598);
			this.bottomSandBarDock.Manager = this.sandBarManager1;
			this.bottomSandBarDock.Name = "bottomSandBarDock";
			this.bottomSandBarDock.Size = new System.Drawing.Size(984, 0);
			this.bottomSandBarDock.TabIndex = 12;
			// 
			// leftSandBarDock
			// 
			this.leftSandBarDock.Dock = System.Windows.Forms.DockStyle.Left;
			this.leftSandBarDock.Location = new System.Drawing.Point(0, 24);
			this.leftSandBarDock.Manager = this.sandBarManager1;
			this.leftSandBarDock.Name = "leftSandBarDock";
			this.leftSandBarDock.Size = new System.Drawing.Size(0, 574);
			this.leftSandBarDock.TabIndex = 10;
			// 
			// rightSandBarDock
			// 
			this.rightSandBarDock.Dock = System.Windows.Forms.DockStyle.Right;
			this.rightSandBarDock.Location = new System.Drawing.Point(984, 24);
			this.rightSandBarDock.Manager = this.sandBarManager1;
			this.rightSandBarDock.Name = "rightSandBarDock";
			this.rightSandBarDock.Size = new System.Drawing.Size(0, 574);
			this.rightSandBarDock.TabIndex = 11;
			// 
			// topSandBarDock
			// 
			this.topSandBarDock.Controls.Add(this.menuBar1);
			this.topSandBarDock.Dock = System.Windows.Forms.DockStyle.Top;
			this.topSandBarDock.Location = new System.Drawing.Point(0, 0);
			this.topSandBarDock.Manager = this.sandBarManager1;
			this.topSandBarDock.Name = "topSandBarDock";
			this.topSandBarDock.Size = new System.Drawing.Size(984, 24);
			this.topSandBarDock.TabIndex = 13;
			// 
			// menuBar1
			// 
			this.menuBar1.Buttons.AddRange(new TD.SandBar.ToolbarItemBase[] {
																				this.contextMenuWeapon,
																				this.contextMenuHitTypes,
																				this.contextMenuTarget,
																				this.contextMenuAttackWeapon,
																				this.contextMenuAttackHitType,
																				this.contextMenuAttackAttacker});
			this.menuBar1.Guid = new System.Guid("ceb8862f-b368-4931-9bde-7bf71b610201");
			this.menuBar1.Location = new System.Drawing.Point(2, 0);
			this.menuBar1.Name = "menuBar1";
			this.menuBar1.Size = new System.Drawing.Size(982, 24);
			this.menuBar1.TabIndex = 0;
			this.menuBar1.Visible = false;
			// 
			// contextMenuWeapon
			// 
			this.contextMenuWeapon.MenuItems.AddRange(new TD.SandBar.MenuButtonItem[] {
																						  this.menuButtonItem3});
			this.contextMenuWeapon.Text = "contextMenuWeapon";
			// 
			// menuButtonItem3
			// 
			this.menuButtonItem3.Text = "All";
			// 
			// contextMenuHitTypes
			// 
			this.contextMenuHitTypes.MenuItems.AddRange(new TD.SandBar.MenuButtonItem[] {
																							this.menuButtonItem4});
			this.contextMenuHitTypes.Text = "contextMenuHitTypes";
			// 
			// menuButtonItem4
			// 
			this.menuButtonItem4.Text = "All";
			// 
			// contextMenuTarget
			// 
			this.contextMenuTarget.MenuItems.AddRange(new TD.SandBar.MenuButtonItem[] {
																						  this.menuButtonItem5});
			this.contextMenuTarget.Text = "contextMenuTarget";
			// 
			// menuButtonItem5
			// 
			this.menuButtonItem5.Text = "All";
			// 
			// contextMenuCombatSummary
			// 
			this.contextMenuCombatSummary.MenuItems.AddRange(new TD.SandBar.MenuButtonItem[] {
																								 this.menuItemFindWeapon,
																								 this.menuItemFindTarget});
			this.contextMenuCombatSummary.Text = "(contextMenuCombatSummary)";
			// 
			// menuItemFindWeapon
			// 
			this.menuItemFindWeapon.Text = "Find %0 on EVE-I OE";
			// 
			// menuItemFindTarget
			// 
			this.menuItemFindTarget.Text = "Find %0 on EVE-I OE";
			// 
			// contextMenuBarItem1
			// 
			this.contextMenuBarItem1.MenuItems.AddRange(new TD.SandBar.MenuButtonItem[] {
																							this.menuButtonItem1,
																							this.menuButtonItem2});
			this.contextMenuBarItem1.Text = "(contextMenuCombatSummary)";
			// 
			// menuButtonItem1
			// 
			this.menuButtonItem1.Text = "Find %0 on EVE-I OE";
			// 
			// menuButtonItem2
			// 
			this.menuButtonItem2.Text = "Find %0 on EVE-I OE";
			// 
			// panelMainTop
			// 
			this.panelMainTop.Controls.Add(this.groupBox2);
			this.panelMainTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelMainTop.Location = new System.Drawing.Point(0, 0);
			this.panelMainTop.Name = "panelMainTop";
			this.panelMainTop.Size = new System.Drawing.Size(746, 96);
			this.panelMainTop.TabIndex = 14;
			// 
			// panelBottom
			// 
			this.panelBottom.Controls.Add(this.listViewCombatSummaryStats);
			this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panelBottom.Location = new System.Drawing.Point(0, 419);
			this.panelBottom.Name = "panelBottom";
			this.panelBottom.Size = new System.Drawing.Size(746, 128);
			this.panelBottom.TabIndex = 15;
			// 
			// panelMainMiddle
			// 
			this.panelMainMiddle.Controls.Add(this.listViewCombatSummary);
			this.panelMainMiddle.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelMainMiddle.Location = new System.Drawing.Point(0, 96);
			this.panelMainMiddle.Name = "panelMainMiddle";
			this.panelMainMiddle.Size = new System.Drawing.Size(746, 323);
			this.panelMainMiddle.TabIndex = 16;
			// 
			// btnAttackWeapon
			// 
			this.btnAttackWeapon.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnAttackWeapon.Location = new System.Drawing.Point(328, 16);
			this.btnAttackWeapon.Name = "btnAttackWeapon";
			this.btnAttackWeapon.Size = new System.Drawing.Size(24, 23);
			this.btnAttackWeapon.TabIndex = 3;
			this.btnAttackWeapon.Text = "...";
			this.toolTip1.SetToolTip(this.btnAttackWeapon, "Hold down the CTRL key when selecting an item to add it to the filter");
			this.btnAttackWeapon.Click += new System.EventHandler(this.btnAttackWeapon_Click);
			// 
			// btnAttackHitType
			// 
			this.btnAttackHitType.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnAttackHitType.Location = new System.Drawing.Point(328, 40);
			this.btnAttackHitType.Name = "btnAttackHitType";
			this.btnAttackHitType.Size = new System.Drawing.Size(24, 23);
			this.btnAttackHitType.TabIndex = 3;
			this.btnAttackHitType.Text = "...";
			this.toolTip1.SetToolTip(this.btnAttackHitType, "Hold down the CTRL key when selecting an item to add it to the filter");
			this.btnAttackHitType.Click += new System.EventHandler(this.btnAttackHitType_Click);
			// 
			// btnAttackAttacker
			// 
			this.btnAttackAttacker.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnAttackAttacker.Location = new System.Drawing.Point(328, 64);
			this.btnAttackAttacker.Name = "btnAttackAttacker";
			this.btnAttackAttacker.Size = new System.Drawing.Size(24, 23);
			this.btnAttackAttacker.TabIndex = 3;
			this.btnAttackAttacker.Text = "...";
			this.toolTip1.SetToolTip(this.btnAttackAttacker, "Hold down the CTRL key when selecting an item to add it to the filter");
			this.btnAttackAttacker.Click += new System.EventHandler(this.btnAttackAttacker_Click);
			// 
			// sandDockManager1
			// 
			this.sandDockManager1.OwnerForm = this;
			this.sandDockManager1.Renderer = new TD.SandDock.Rendering.EverettRenderer();
			// 
			// leftSandDock
			// 
			this.leftSandDock.Dock = System.Windows.Forms.DockStyle.Left;
			this.leftSandDock.Guid = new System.Guid("605a565c-ad14-483d-bf9a-dea23ab2075f");
			this.leftSandDock.LayoutSystem = new TD.SandDock.SplitLayoutSystem(250, 400);
			this.leftSandDock.Location = new System.Drawing.Point(0, 24);
			this.leftSandDock.Manager = this.sandDockManager1;
			this.leftSandDock.Name = "leftSandDock";
			this.leftSandDock.Size = new System.Drawing.Size(0, 574);
			this.leftSandDock.TabIndex = 18;
			// 
			// rightSandDock
			// 
			this.rightSandDock.Controls.Add(this.dockControl3);
			this.rightSandDock.Controls.Add(this.dockControl2);
			this.rightSandDock.Controls.Add(this.dockControl4);
			this.rightSandDock.Dock = System.Windows.Forms.DockStyle.Right;
			this.rightSandDock.Guid = new System.Guid("4ae549f1-d900-406e-a32e-dae75b40ed0f");
			this.rightSandDock.LayoutSystem = new TD.SandDock.SplitLayoutSystem(250, 400, System.Windows.Forms.Orientation.Horizontal, new TD.SandDock.LayoutSystemBase[] {
																																											  new TD.SandDock.ControlLayoutSystem(228, 285, new TD.SandDock.DockControl[] {
																																																															  this.dockControl3,
																																																															  this.dockControl4}, this.dockControl3),
																																											  new TD.SandDock.ControlLayoutSystem(228, 285, new TD.SandDock.DockControl[] {
																																																															  this.dockControl2}, this.dockControl2)});
			this.rightSandDock.Location = new System.Drawing.Point(752, 24);
			this.rightSandDock.Manager = this.sandDockManager1;
			this.rightSandDock.MaximumSize = 400;
			this.rightSandDock.Name = "rightSandDock";
			this.rightSandDock.Size = new System.Drawing.Size(232, 574);
			this.rightSandDock.TabIndex = 19;
			// 
			// dockControl3
			// 
			this.dockControl3.Closable = false;
			this.dockControl3.Controls.Add(this.panelSummaryBottom);
			this.dockControl3.Guid = new System.Guid("faf5015a-6d81-4247-8807-01d993b8354a");
			this.dockControl3.Location = new System.Drawing.Point(4, 20);
			this.dockControl3.Name = "dockControl3";
			this.dockControl3.Size = new System.Drawing.Size(228, 243);
			this.dockControl3.TabIndex = 1;
			this.dockControl3.Text = "Hit Type Info";
			// 
			// panelSummaryBottom
			// 
			this.panelSummaryBottom.Controls.Add(this.axWebBrowser1);
			this.panelSummaryBottom.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelSummaryBottom.Location = new System.Drawing.Point(0, 0);
			this.panelSummaryBottom.Name = "panelSummaryBottom";
			this.panelSummaryBottom.Size = new System.Drawing.Size(228, 243);
			this.panelSummaryBottom.TabIndex = 3;
			// 
			// axWebBrowser1
			// 
			this.axWebBrowser1.ContainingControl = this;
			this.axWebBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.axWebBrowser1.Enabled = true;
			this.axWebBrowser1.Location = new System.Drawing.Point(0, 0);
			this.axWebBrowser1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axWebBrowser1.OcxState")));
			this.axWebBrowser1.Size = new System.Drawing.Size(228, 243);
			this.axWebBrowser1.TabIndex = 2;
			// 
			// dockControl2
			// 
			this.dockControl2.AutoScroll = true;
			this.dockControl2.Closable = false;
			this.dockControl2.Guid = new System.Guid("55baf4b4-541a-4780-8342-338616bec53e");
			this.dockControl2.Location = new System.Drawing.Point(4, 309);
			this.dockControl2.Name = "dockControl2";
			this.dockControl2.Size = new System.Drawing.Size(228, 243);
			this.dockControl2.TabIndex = 0;
			this.dockControl2.Text = "Damage Graphs";
			// 
			// dockControl4
			// 
			this.dockControl4.Closable = false;
			this.dockControl4.Controls.Add(this.tbPlayerNotes);
			this.dockControl4.Guid = new System.Guid("f5c998cf-ee9d-416a-a8bb-3c4420a10747");
			this.dockControl4.Location = new System.Drawing.Point(4, 20);
			this.dockControl4.Name = "dockControl4";
			this.dockControl4.Size = new System.Drawing.Size(228, 243);
			this.dockControl4.TabIndex = 2;
			this.dockControl4.Text = "Player Notes";
			// 
			// tbPlayerNotes
			// 
			this.tbPlayerNotes.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbPlayerNotes.Location = new System.Drawing.Point(0, 0);
			this.tbPlayerNotes.Name = "tbPlayerNotes";
			this.tbPlayerNotes.Size = new System.Drawing.Size(228, 243);
			this.tbPlayerNotes.TabIndex = 0;
			this.tbPlayerNotes.Text = "";
			// 
			// bottomSandDock
			// 
			this.bottomSandDock.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.bottomSandDock.Guid = new System.Guid("0acbfb51-aae4-4c38-9bf6-d4ec72a65ab6");
			this.bottomSandDock.LayoutSystem = new TD.SandDock.SplitLayoutSystem(250, 400);
			this.bottomSandDock.Location = new System.Drawing.Point(0, 598);
			this.bottomSandDock.Manager = this.sandDockManager1;
			this.bottomSandDock.Name = "bottomSandDock";
			this.bottomSandDock.Size = new System.Drawing.Size(984, 0);
			this.bottomSandDock.TabIndex = 20;
			// 
			// topSandDock
			// 
			this.topSandDock.Dock = System.Windows.Forms.DockStyle.Top;
			this.topSandDock.Guid = new System.Guid("a11b4b37-491b-4687-98e7-08a973f2c204");
			this.topSandDock.LayoutSystem = new TD.SandDock.SplitLayoutSystem(250, 400);
			this.topSandDock.Location = new System.Drawing.Point(0, 24);
			this.topSandDock.Manager = this.sandDockManager1;
			this.topSandDock.Name = "topSandDock";
			this.topSandDock.Size = new System.Drawing.Size(984, 0);
			this.topSandDock.TabIndex = 21;
			// 
			// documentContainer1
			// 
			this.documentContainer1.Controls.Add(this.dockControlDamage);
			this.documentContainer1.Controls.Add(this.dockControlAttackers);
			this.documentContainer1.Controls.Add(this.dockControl1);
			this.documentContainer1.Cursor = System.Windows.Forms.Cursors.Default;
			this.documentContainer1.Guid = new System.Guid("f676f239-44fc-44d2-8255-3d0da5e1c53f");
			this.documentContainer1.LayoutSystem = new TD.SandDock.SplitLayoutSystem(250, 400, System.Windows.Forms.Orientation.Horizontal, new TD.SandDock.LayoutSystemBase[] {
																																												   new TD.SandDock.DocumentLayoutSystem(750, 572, new TD.SandDock.DockControl[] {
																																																																	this.dockControlDamage,
																																																																	this.dockControlAttackers,
																																																																	this.dockControl1}, this.dockControlDamage)});
			this.documentContainer1.Location = new System.Drawing.Point(0, 24);
			this.documentContainer1.Manager = null;
			this.documentContainer1.Name = "documentContainer1";
			this.documentContainer1.Renderer = new TD.SandDock.Rendering.EverettRenderer();
			this.documentContainer1.Sizable = false;
			this.documentContainer1.Size = new System.Drawing.Size(752, 574);
			this.documentContainer1.TabIndex = 22;
			// 
			// dockControlDamage
			// 
			this.dockControlDamage.Closable = false;
			this.dockControlDamage.Collapsible = false;
			this.dockControlDamage.Controls.Add(this.splitter1);
			this.dockControlDamage.Controls.Add(this.panelMainMiddle);
			this.dockControlDamage.Controls.Add(this.panelMainTop);
			this.dockControlDamage.Controls.Add(this.panelBottom);
			this.dockControlDamage.Guid = new System.Guid("a914ea20-d229-447e-9f49-916136a9e40c");
			this.dockControlDamage.Location = new System.Drawing.Point(3, 24);
			this.dockControlDamage.Name = "dockControlDamage";
			this.dockControlDamage.Size = new System.Drawing.Size(746, 547);
			this.dockControlDamage.TabIndex = 0;
			this.dockControlDamage.Text = "My Victims";
			// 
			// splitter1
			// 
			this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.splitter1.Location = new System.Drawing.Point(0, 416);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(746, 3);
			this.splitter1.TabIndex = 17;
			this.splitter1.TabStop = false;
			// 
			// dockControlAttackers
			// 
			this.dockControlAttackers.Closable = false;
			this.dockControlAttackers.Collapsible = false;
			this.dockControlAttackers.Controls.Add(this.splitterAttacker);
			this.dockControlAttackers.Controls.Add(this.listViewAttackData);
			this.dockControlAttackers.Controls.Add(this.listViewAttackSummary);
			this.dockControlAttackers.Controls.Add(this.panel1);
			this.dockControlAttackers.Guid = new System.Guid("69df739b-4242-47a4-9a18-f24508073ce7");
			this.dockControlAttackers.Location = new System.Drawing.Point(3, 24);
			this.dockControlAttackers.Name = "dockControlAttackers";
			this.dockControlAttackers.Size = new System.Drawing.Size(706, 547);
			this.dockControlAttackers.TabIndex = 1;
			this.dockControlAttackers.Text = "My Attackers";
			// 
			// splitterAttacker
			// 
			this.splitterAttacker.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.splitterAttacker.Location = new System.Drawing.Point(0, 416);
			this.splitterAttacker.Name = "splitterAttacker";
			this.splitterAttacker.Size = new System.Drawing.Size(706, 3);
			this.splitterAttacker.TabIndex = 17;
			this.splitterAttacker.TabStop = false;
			// 
			// listViewAttackData
			// 
			this.listViewAttackData.AllowColumnReorder = true;
			this.listViewAttackData.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																								 this.columnHeader19,
																								 this.columnHeader22,
																								 this.columnHeader20,
																								 this.columnHeader21,
																								 this.columnHeader23});
			this.listViewAttackData.ContextMenu = this.contextMenuAttackItemInfo;
			this.listViewAttackData.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listViewAttackData.FullRowSelect = true;
			this.listViewAttackData.HideSelection = false;
			this.listViewAttackData.Location = new System.Drawing.Point(0, 96);
			this.listViewAttackData.Name = "listViewAttackData";
			this.listViewAttackData.Size = new System.Drawing.Size(706, 323);
			this.listViewAttackData.TabIndex = 5;
			this.listViewAttackData.View = System.Windows.Forms.View.Details;
			this.listViewAttackData.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listViewAttackData_ColumnClick);
			this.listViewAttackData.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.listViewAttackData_ItemDrag);
			this.listViewAttackData.SelectedIndexChanged += new System.EventHandler(this.listViewAttackData_SelectedIndexChanged);
			// 
			// columnHeader19
			// 
			this.columnHeader19.Text = "Date/Time";
			this.columnHeader19.Width = 116;
			// 
			// columnHeader22
			// 
			this.columnHeader22.Text = "Attacker";
			this.columnHeader22.Width = 154;
			// 
			// columnHeader20
			// 
			this.columnHeader20.Text = "Weapon";
			this.columnHeader20.Width = 135;
			// 
			// columnHeader21
			// 
			this.columnHeader21.Text = "Hit Type";
			this.columnHeader21.Width = 83;
			// 
			// columnHeader23
			// 
			this.columnHeader23.Text = "Damage";
			this.columnHeader23.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// contextMenuAttackItemInfo
			// 
			this.contextMenuAttackItemInfo.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																									  this.menuItemAttackCopyText,
																									  this.menuItem2,
																									  this.menuItemAttackWeaponInfo,
																									  this.menuItemAttackAttackerInfo});
			this.contextMenuAttackItemInfo.Popup += new System.EventHandler(this.contextMenuAttackItemInfo_Popup);
			// 
			// menuItemAttackCopyText
			// 
			this.menuItemAttackCopyText.Index = 0;
			this.menuItemAttackCopyText.Text = "Copy log text";
			this.menuItemAttackCopyText.Click += new System.EventHandler(this.menuItemAttackCopyText_Click);
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 1;
			this.menuItem2.Text = "-";
			// 
			// menuItemAttackWeaponInfo
			// 
			this.menuItemAttackWeaponInfo.Index = 2;
			this.menuItemAttackWeaponInfo.Text = "Weapon info...";
			this.menuItemAttackWeaponInfo.Click += new System.EventHandler(this.menuItemAttackWeaponInfo_Click);
			// 
			// menuItemAttackAttackerInfo
			// 
			this.menuItemAttackAttackerInfo.Index = 3;
			this.menuItemAttackAttackerInfo.Text = "Attacker info...";
			this.menuItemAttackAttackerInfo.Click += new System.EventHandler(this.menuItemAttackAttackerInfo_Click);
			// 
			// listViewAttackSummary
			// 
			this.listViewAttackSummary.AllowColumnReorder = true;
			this.listViewAttackSummary.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																									this.columnHeader24,
																									this.columnHeader25,
																									this.columnHeader26,
																									this.columnHeader27,
																									this.columnHeader28,
																									this.columnHeader29,
																									this.columnHeader30,
																									this.columnHeader31,
																									this.columnHeader32,
																									this.columnHeader33,
																									this.columnHeader34,
																									this.columnHeader18});
			this.listViewAttackSummary.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.listViewAttackSummary.FullRowSelect = true;
			this.listViewAttackSummary.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.listViewAttackSummary.Location = new System.Drawing.Point(0, 419);
			this.listViewAttackSummary.MultiSelect = false;
			this.listViewAttackSummary.Name = "listViewAttackSummary";
			this.listViewAttackSummary.Size = new System.Drawing.Size(706, 128);
			this.listViewAttackSummary.TabIndex = 16;
			this.listViewAttackSummary.View = System.Windows.Forms.View.Details;
			this.listViewAttackSummary.ItemActivate += new System.EventHandler(this.listViewAttackSummary_ItemActivate);
			// 
			// columnHeader24
			// 
			this.columnHeader24.Text = "Attacker";
			this.columnHeader24.Width = 145;
			// 
			// columnHeader25
			// 
			this.columnHeader25.Text = "Fired";
			this.columnHeader25.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.columnHeader25.Width = 43;
			// 
			// columnHeader26
			// 
			this.columnHeader26.Text = "Hit";
			this.columnHeader26.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.columnHeader26.Width = 41;
			// 
			// columnHeader27
			// 
			this.columnHeader27.Text = "Missed";
			this.columnHeader27.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.columnHeader27.Width = 50;
			// 
			// columnHeader28
			// 
			this.columnHeader28.Text = "Tot. Dmg";
			this.columnHeader28.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// columnHeader29
			// 
			this.columnHeader29.Text = "Avg. Dmg";
			this.columnHeader29.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.columnHeader29.Width = 66;
			// 
			// columnHeader30
			// 
			this.columnHeader30.Text = "% Hit";
			this.columnHeader30.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.columnHeader30.Width = 49;
			// 
			// columnHeader31
			// 
			this.columnHeader31.Text = "% Missed";
			this.columnHeader31.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.columnHeader31.Width = 66;
			// 
			// columnHeader32
			// 
			this.columnHeader32.Text = "Duration";
			this.columnHeader32.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// columnHeader33
			// 
			this.columnHeader33.Text = "Shots/Min";
			this.columnHeader33.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.columnHeader33.Width = 66;
			// 
			// columnHeader34
			// 
			this.columnHeader34.Text = "Dmg/Min";
			this.columnHeader34.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// columnHeader18
			// 
			this.columnHeader18.Text = "DPS";
			this.columnHeader18.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.groupBox3);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(706, 96);
			this.panel1.TabIndex = 15;
			// 
			// groupBox3
			// 
			this.groupBox3.BackColor = System.Drawing.SystemColors.ControlLight;
			this.groupBox3.Controls.Add(this.lblAttackWeapon);
			this.groupBox3.Controls.Add(this.btnAttackWeapon);
			this.groupBox3.Controls.Add(this.label6);
			this.groupBox3.Controls.Add(this.label8);
			this.groupBox3.Controls.Add(this.lblAttacker);
			this.groupBox3.Controls.Add(this.btnAttackHitType);
			this.groupBox3.Controls.Add(this.btnAttackAttacker);
			this.groupBox3.Controls.Add(this.lblAttackHitType);
			this.groupBox3.Controls.Add(this.lblAttackAttacker);
			this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
			this.groupBox3.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox3.Location = new System.Drawing.Point(0, 0);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(706, 96);
			this.groupBox3.TabIndex = 3;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Display Filters";
			// 
			// lblAttackWeapon
			// 
			this.lblAttackWeapon.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.lblAttackWeapon.Location = new System.Drawing.Point(72, 16);
			this.lblAttackWeapon.Name = "lblAttackWeapon";
			this.lblAttackWeapon.Size = new System.Drawing.Size(248, 23);
			this.lblAttackWeapon.TabIndex = 4;
			this.lblAttackWeapon.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(8, 16);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(56, 23);
			this.label6.TabIndex = 1;
			this.label6.Text = "Weapon";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(8, 40);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(56, 23);
			this.label8.TabIndex = 1;
			this.label8.Text = "Hit Type";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblAttacker
			// 
			this.lblAttacker.Location = new System.Drawing.Point(8, 64);
			this.lblAttacker.Name = "lblAttacker";
			this.lblAttacker.Size = new System.Drawing.Size(56, 23);
			this.lblAttacker.TabIndex = 1;
			this.lblAttacker.Text = "Attacker";
			this.lblAttacker.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblAttackHitType
			// 
			this.lblAttackHitType.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.lblAttackHitType.Location = new System.Drawing.Point(72, 40);
			this.lblAttackHitType.Name = "lblAttackHitType";
			this.lblAttackHitType.Size = new System.Drawing.Size(248, 23);
			this.lblAttackHitType.TabIndex = 4;
			this.lblAttackHitType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblAttackAttacker
			// 
			this.lblAttackAttacker.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.lblAttackAttacker.Location = new System.Drawing.Point(72, 64);
			this.lblAttackAttacker.Name = "lblAttackAttacker";
			this.lblAttackAttacker.Size = new System.Drawing.Size(248, 23);
			this.lblAttackAttacker.TabIndex = 4;
			this.lblAttackAttacker.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// dockControl1
			// 
			this.dockControl1.Controls.Add(this.rtbSummary);
			this.dockControl1.Guid = new System.Guid("02d133f8-3b05-482a-b837-d72e5bef8079");
			this.dockControl1.Location = new System.Drawing.Point(3, 24);
			this.dockControl1.Name = "dockControl1";
			this.dockControl1.Size = new System.Drawing.Size(706, 547);
			this.dockControl1.TabIndex = 2;
			this.dockControl1.Text = "Summary";
			// 
			// rtbSummary
			// 
			this.rtbSummary.Dock = System.Windows.Forms.DockStyle.Fill;
			this.rtbSummary.Location = new System.Drawing.Point(0, 0);
			this.rtbSummary.Name = "rtbSummary";
			this.rtbSummary.Size = new System.Drawing.Size(706, 547);
			this.rtbSummary.TabIndex = 0;
			this.rtbSummary.Text = "richTextBox1";
			// 
			// cbShowNotifyMessages
			// 
			this.cbShowNotifyMessages.Checked = true;
			this.cbShowNotifyMessages.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbShowNotifyMessages.Location = new System.Drawing.Point(368, 64);
			this.cbShowNotifyMessages.Name = "cbShowNotifyMessages";
			this.cbShowNotifyMessages.Size = new System.Drawing.Size(160, 24);
			this.cbShowNotifyMessages.TabIndex = 6;
			this.cbShowNotifyMessages.Text = "Show Notify Messages";
			this.cbShowNotifyMessages.CheckedChanged += new System.EventHandler(this.cbShowNotifyMessages_CheckedChanged);
			// 
			// Form2
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(984, 598);
			this.Controls.Add(this.documentContainer1);
			this.Controls.Add(this.leftSandDock);
			this.Controls.Add(this.rightSandDock);
			this.Controls.Add(this.bottomSandDock);
			this.Controls.Add(this.topSandDock);
			this.Controls.Add(this.leftSandBarDock);
			this.Controls.Add(this.rightSandBarDock);
			this.Controls.Add(this.bottomSandBarDock);
			this.Controls.Add(this.topSandBarDock);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "Form2";
			this.Text = "Form2";
			this.Resize += new System.EventHandler(this.Form2_Resize);
			this.Closing += new System.ComponentModel.CancelEventHandler(this.Form2_Closing);
			this.Load += new System.EventHandler(this.Form2_Load);
			this.groupBox2.ResumeLayout(false);
			this.topSandBarDock.ResumeLayout(false);
			this.panelMainTop.ResumeLayout(false);
			this.panelBottom.ResumeLayout(false);
			this.panelMainMiddle.ResumeLayout(false);
			this.rightSandDock.ResumeLayout(false);
			this.dockControl3.ResumeLayout(false);
			this.panelSummaryBottom.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.axWebBrowser1)).EndInit();
			this.dockControl4.ResumeLayout(false);
			this.documentContainer1.ResumeLayout(false);
			this.dockControlDamage.ResumeLayout(false);
			this.dockControlAttackers.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.dockControl1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void PrepareAttackSummaryListView()
		{
			AttackLogSorter.SortColumn = 0;
			AttackLogSorter.Direction = SortOrder.Ascending;
			listViewAttackData.ListViewItemSorter = AttackLogSorter;
		}

		private void PrepareCombatSummaryListView()
		{
			CombatSummarySorter.SortColumn = 0;
			CombatSummarySorter.Direction = SortOrder.Ascending;
			listViewCombatSummary.ListViewItemSorter = CombatSummarySorter;
		}


		private void PrepareLayout()
		{
			if ( MyConfig == null )
				return;

			if ( MyConfig.RememberColumnWidths )
			{
				if ( MyConfig.CombatDataMainLVS != null )
					MyConfig.CombatDataMainLVS.RestoreFormat(listViewCombatSummary);

				if ( MyConfig.CombatDataSummaryLVS != null )
					MyConfig.CombatDataSummaryLVS.RestoreFormat(listViewCombatSummaryStats);

				if ( MyConfig.AttackerDataLVS != null)
					MyConfig.AttackerDataLVS.RestoreFormat(listViewAttackData);

				if ( MyConfig.AttackerDataSummaryLVS != null )
					MyConfig.AttackerDataSummaryLVS.RestoreFormat(listViewAttackSummary);
			}

			if ( MyConfig.RememberWindowLayout )
			{
				if ( MyConfig.AnalysisWindowLayout != null && MyConfig.AnalysisWindowLayout.Length > 0 )
				{
					Debug.WriteLine("About to restore analysis window layout");
					try
					{
						sandDockManager1.SetLayout(MyConfig.AnalysisWindowLayout);
					}
					catch (Exception e)
					{
						Debug.WriteLine(e.ToString());
						logger.Log(Level.Exception, "Error restoring analysis window layout: " + e.ToString());
					}
				}

				this.WindowState = MyConfig.AnalysisWindowState;
			}
		}

		private void Form2_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			Debug.WriteLine("Usercomment = '" + ThisGameLog.UserComment + "'");
			Debug.WriteLine("PlayerNotes text box = '" + tbPlayerNotes.Text + "'");

			if ( ThisGameLog.UserComment == null )
				ThisGameLog.UserComment = "";

			if ( tbPlayerNotes.Text == null )
				tbPlayerNotes.Text = "";

			if ( ThisGameLog.UserComment != tbPlayerNotes.Text )
			{
				ThisGameLog.UserComment = tbPlayerNotes.Text;

				if ( PlayerNoteUpdatedHandler != null )
					PlayerNoteUpdatedHandler(this, ThisGameLog);
			}

			if ( MyConfig.RememberColumnWidths )
			{
				MyConfig.CombatDataMainLVS		= new ListViewSettings(listViewCombatSummary);
				MyConfig.CombatDataSummaryLVS	= new ListViewSettings(listViewCombatSummaryStats);
				MyConfig.AttackerDataLVS		= new ListViewSettings(listViewAttackData);
				MyConfig.AttackerDataSummaryLVS	= new ListViewSettings(listViewAttackSummary);
			}
			else
			{
				MyConfig.CombatDataMainLVS = null;
				MyConfig.CombatDataSummaryLVS = null;
				MyConfig.AttackerDataLVS = null;
				MyConfig.AttackerDataSummaryLVS = null;
			}

			if ( MyConfig.RememberWindowLayout )
			{
				MyConfig.AnalysisWindowLayout	= sandDockManager1.GetLayout();
				MyConfig.AnalysisWindowState	= this.WindowState;
			}
			else
			{
				MyConfig.AnalysisWindowLayout	= null;
				MyConfig.AnalysisWindowState	= FormWindowState.Normal;
			}

			try
			{
				File.Delete(Application.StartupPath + @"\tmprep.htm");
			}
			catch
			{}
		}

		private void Form2_Load(object sender, System.EventArgs e)
		{
			if ( ThisGameLog == null )
			{
				MessageBox.Show("Serious problem, no gamelog details supplied!");
				return;
			}

			if ( MyConfig == null )
			{
				MessageBox.Show("No user configuration data supplied. This should not happen. Please contact deanpm@gmail.com.");
				MyConfig = new UserConfig();
				MyConfig.GameLogDirs = new ConfigGameLogDirCollection();
			}

			PrepareLayout();
			GameLog g = ThisGameLog;

			g.GetFile();

			EnableComponents();

			Win32.HiPerfTimer HTimer = new Win32.HiPerfTimer();

			HTimer.Start();

			PrepareCombatSummaryListView();
			PrepareAttackSummaryListView();
			
			HTimer.Stop();
			logger.Log(Level.Verbose, "Time to Prepare CombatSummaryListView(): " + HTimer.Duration.ToString());
            
			HTimer.Start();

			DoCombatLog(g);
			
			HTimer.Stop();
			logger.Log(Level.Verbose, "Time to execute DoCombatLog(): " + HTimer.Duration.ToString());

			HTimer.Start();

			DoAttackAnalysis(g);

			HTimer.Stop();
			logger.Log(Level.Verbose, "Time to analyse attack data: " + HTimer.Duration.ToString());

			//
			// Setup the Tick handler for the SelectedIndexTimer
			//
			SelectedIndexTimer.Tick += new EventHandler(SelectedIndexTimer_Tick);
			AttackSelectedIndexTimer.Tick += new EventHandler(AttackSelectedIndexTimer_Tick);

            //TODO: Fix This.
            //DoAnalysisSummary();
            tbPlayerNotes.Text = ThisGameLog.UserComment;
		}

		private void DoAnalysisSummary()
		{
			Summary.CombatSummary cs = new Summary.CombatSummary();
			cs.AllGameLogs = AllGameLogs;
			cs.ThisGameLog = ThisGameLog;

			rtbSummary.Text = cs.GetSummary();
		}

		private void SaveFile(string text, string FileName)
		{
			System.IO.FileStream fs;

			try
			{
				File.Delete(FileName);
			}
			catch
			{
			}

			try
			{
				fs = new FileStream(FileName, FileMode.CreateNew);
			}
			catch (Exception err)
			{
				logger.Log(Level.Exception, "Error creating temporary html file!! " + FileName);
				MessageBox.Show("There was a problem creating a temporary file on your system: " + err.ToString());
				return;
			}

			System.IO.StreamWriter sw = new System.IO.StreamWriter(fs);

			//
			// FilterIndex is the type of file format the user chose. Pass this through to the export method.
			//

			sw.Write(text);

			sw.Close();
			fs.Close();
		}

		public void ShowHTML(string html)
		{
			string url;
			url = Application.StartupPath + @"\tmprep.htm";

			// Save the doc to a file in the app.startup dir
			SaveFile(html, url);

			// Open the saved file using the embedded browser control

			object nullObject = null;
			object nullString = "";

			string f = url.Replace(@"\", @"/");

			axWebBrowser1.Navigate(f, ref nullObject, ref nullString, ref nullString, ref nullString);
		}

		public static byte[] WriteResultsToMemory(WeaponSummaryCollection data)
		{			
			MemoryStream ms = new MemoryStream();

			// XmlSerializer x = new XmlSerializer(typeof(AuditResults));
			XmlSerializer x = new XmlSerializer(typeof(WeaponSummaryCollection));

			x.Serialize(ms, data);

			byte[] ResultsBuffer = (byte[])ms.ToArray();
			return ResultsBuffer;
		}

		private byte[] getXmlDocumentFromResults(WeaponSummaryCollection WeaponData)
		{
			byte[] ResBuff = WriteResultsToMemory(WeaponData);

			return (ResBuff);
		}

		private string GetRenderedHTML(WeaponSummaryCollection WeaponData)
		{
			byte[] XmlTags = getXmlDocumentFromResults(WeaponData);

			// Debug.WriteLine(System.Text.Encoding.UTF8.GetString(XmlTags));

			return System.Text.Encoding.UTF8.GetString(transformResultsToHTML(XmlTags));
		}

		/// <summary>
		/// Performs an in memory transformation of an XML document to HTML using the specified style sheet.
		/// </summary>
		/// <param name="ResultsStr">A byte[] array containing an XML document</param>
		/// <returns>A byte[] array containing a transformed HTML document</returns>
		private byte[] transformResultsToHTML(byte[] ResultsStr)
		{
			string stylesheet = Application.StartupPath + @"\" + @"xsl\HitTypeSummary.xsl";
			//Create the XslTransform.
			XslTransform xslt = new XslTransform();
            // XslCompiledTransform xslt = new XslCompiledTransform();

			//Load the stylesheet.
			xslt.Load(stylesheet);

			//Load the XML byte array into a MemoryStream object
			MemoryStream ms = new MemoryStream(ResultsStr, 0, ResultsStr.Length);

			// Load the document from the memory stream containing the byte[] which contains the XML tags
			XPathDocument doc = new XPathDocument(ms);

			//Create the XmlTextWriter to output to the console.            

			// Create an output memory stream. This will be returned to the caller containing the rendered HTML
			MemoryStream WriteStream = new MemoryStream();
			WriteStream.Position = 0;

			//Transform the XML.
			xslt.Transform(doc, null, WriteStream, null);

			WriteStream.Close();

			return WriteStream.GetBuffer();
		}


		public void ExportFile(string FileName, int FilterIndex)
		{
			Debug.WriteLine("Request to export file: " + FileName + " received.");

			if ( FilterIndex == 1 )
			{
				WriteTextToFile(FileName, ThisGameLog.GetFile());
			}
			else
			{
				WriteCSVFile(FileName);
			}
		}

		private void WriteCSVFile(string FileName)
		{
			StringBuilder FileText = new StringBuilder();
			StringBuilder LineText = new StringBuilder();

			int x = 1;
 
			Debug.WriteLine("dockControlDamage: " + dockControlDamage.IsOpen.ToString());
			Debug.WriteLine("dockControlAttackers: " + dockControlAttackers.IsOpen.ToString());

			if ( dockControlDamage.IsOpen )
			{
				foreach ( ListViewItem l in this.listViewCombatSummary.Items )
				{
					LineText.Append(l.SubItems[0].Text); // Time
					LineText.Append(",");
					LineText.Append(l.SubItems[1].Text); // Weapon
					LineText.Append(",");
					LineText.Append(l.SubItems[2].Text); // HitType
					LineText.Append(",");
					LineText.Append(l.SubItems[3].Text); // Target
					LineText.Append(",");
					LineText.Append(l.SubItems[4].Text); // Damage
					LineText.Append('\n');

					Debug.WriteLine(x.ToString() +":" + LineText.ToString());

					FileText.Append(LineText.ToString());
					LineText.Remove(0, LineText.Length);
				}
			}
			else
			{
				foreach ( ListViewItem l in this.listViewAttackData.Items )
				{
					LineText.Append(l.SubItems[0].Text); // Time
					LineText.Append(",");
					LineText.Append(l.SubItems[1].Text); // Weapon
					LineText.Append(",");
					LineText.Append(l.SubItems[2].Text); // HitType
					LineText.Append(",");
					LineText.Append(l.SubItems[3].Text); // Target
					LineText.Append(",");
					LineText.Append(l.SubItems[4].Text); // Damage
					LineText.Append('\n');

					FileText.Append(LineText.ToString());
					LineText.Remove(0,LineText.Length);
				}
			}

			WriteTextToFile(FileName, FileText.ToString());
		}

		private void WriteTextToFile(string FileName, string Text)
		{
			StreamWriter sr = File.CreateText(FileName);

			sr.Write(Text);
			sr.Close();
		}

		private void EnableComponents()
		{
			listViewCombatSummary.Enabled = true;
			btnHitTypeMenu.Enabled = true;
			btnWeaponFilter.Enabled = true;
			btnTargetFilter.Enabled = true;
			listViewCombatSummaryStats.Enabled = true;
		}


		private void PrepareAttackAttackerContextMenu(AttackEntryCollection AttackData, GameLog g)
		{
			int newItem;
			contextMenuAttackAttacker.MenuItems.Clear();
			contextMenuAttackAttacker.Tag = lblAttackAttacker;
		
			newItem = contextMenuAttackAttacker.MenuItems.Add(new TD.SandBar.MenuButtonItem("All", new System.EventHandler(this.contextMenuAttack_Activate)));
			contextMenuAttackAttacker.MenuItems[newItem].Checked = true;
		
			string[] Entries;

			//			if ( AllGameLogs.LogCache.CacheData.Contains(g.FileName) )
			//			{
			//				CombatLogCache cache = AllGameLogs.LogCache.CacheData[g.FileName];
			//				Entries = cache.TargetsAttacked;
			//			}
			//			else
			//			{
			Entries = AttackData.GetUniqueAttackers();
			//			}
		
			int itemCount = 0;
			foreach (string s in Entries)
			{
				newItem = contextMenuAttackAttacker.MenuItems.Add(new TD.SandBar.MenuButtonItem(s, new System.EventHandler(this.contextMenuAttack_Activate)));
		
				if ( itemCount == 0)
					contextMenuAttackAttacker.MenuItems[newItem].BeginGroup = true;
						
				itemCount++;
			}
		}

		private void PrepareAttackHitTypeContextMenu(AttackEntryCollection AttackData, GameLog g)
		{
			int newItem;
			contextMenuAttackHitType.MenuItems.Clear();
			contextMenuAttackHitType.Tag = lblAttackHitType;
		
			newItem = contextMenuAttackHitType.MenuItems.Add(new TD.SandBar.MenuButtonItem("All", new System.EventHandler(this.contextMenuAttack_Activate)));
			contextMenuAttackHitType.MenuItems[newItem].Checked = true;
		
			string[] Entries;

			//			if ( AllGameLogs.LogCache.CacheData.Contains(g.FileName) )
			//			{
			//				CombatLogCache cache = AllGameLogs.LogCache.CacheData[g.FileName];
			//				Entries = cache.TargetsAttacked;
			//			}
			//			else
			//			{
			Entries = AttackData.GetUniqueHitTypes();
			//			}
		
			string[] sortedEntries = HitTypeLib.Sort(Entries, SortOrder.Descending);

			int itemCount = 0;
			foreach ( string s in sortedEntries )
			{
				newItem = contextMenuAttackHitType.MenuItems.Add(new TD.SandBar.MenuButtonItem(s, new System.EventHandler(this.contextMenuAttack_Activate)));
		
				if ( itemCount == 0 )
					contextMenuAttackHitType.MenuItems[newItem].BeginGroup = true;
		
				itemCount++;
			}

		}

		private void PrepareAttackWeaponContextMenu(AttackEntryCollection AttackData, GameLog g)
		{
			int newItem;
			contextMenuAttackWeapon.MenuItems.Clear();
			contextMenuAttackWeapon.Tag = lblAttackWeapon;
		
			newItem = contextMenuAttackWeapon.MenuItems.Add(new TD.SandBar.MenuButtonItem("All", new System.EventHandler(this.contextMenuAttack_Activate)));
			contextMenuAttackWeapon.MenuItems[newItem].Checked = true;
		
			string[] Entries;

			//			if ( AllGameLogs.LogCache.CacheData.Contains(g.FileName) )
			//			{
			//				CombatLogCache cache = AllGameLogs.LogCache.CacheData[g.FileName];
			//				Entries = cache.TargetsAttacked;
			//			}
			//			else
			//			{
			Entries = AttackData.GetUniqueWeapons();
			//			}
		
			int itemCount = 0;
			foreach (string s in Entries)
			{
				newItem = contextMenuAttackWeapon.MenuItems.Add(new TD.SandBar.MenuButtonItem(s, new System.EventHandler(this.contextMenuAttack_Activate)));
		
				if ( itemCount == 0)
					contextMenuAttackWeapon.MenuItems[newItem].BeginGroup = true;
						
				itemCount++;
			}
		}

		private void PrepareAttackLogFilters(AttackEntryCollection AttackData, GameLog g)
		{
			PrepareAttackWeaponContextMenu(AttackData, g);
			PrepareAttackHitTypeContextMenu(AttackData, g);
			PrepareAttackAttackerContextMenu(AttackData, g);
		}
		

		private void DrawAttackSummary(AttackEntryCollection AttackData)
		{
			listViewAttackData.Tag = AttackData;
			listViewAttackData.Items.Clear();

			AttackEntryCollection FilteredLog = GetFilteredAttackLog(AttackData);

			ListViewItem[] AttackItems = new ListViewItem[FilteredLog.Count];

			int i = 0;
			foreach ( AttackEntry ae in FilteredLog )
			{
				AttackItems[i] = new ListViewItem(new string[] { ae.LogDTM.ToShortDateString() + " " + ae.LogDTM.ToLongTimeString(), ae.AttackerName, ae.WeaponName, ae.HitType, ae.DamageCaused.ToString("0.0")});
				AttackItems[i].Tag = ae;
				AttackItems[i].ForeColor = GetHitTypeColor(ae.HitDescription);

				i++;
			}

			listViewAttackData.Items.AddRange(AttackItems);

			FilteredLog.GenerateAttackerStats();
			DrawAttackerSummary(FilteredLog);

		}
		
		private void DoAttackAnalysis(GameLog g)
		{
			AttackEntryCollection AttackData = g.GetAttackEntries();

			if ( AttackData.Count == 0 )
			{
				dockControlAttackers.Enabled = false;
				dockControlDamage.Activate();
				return;
			}

			if ( AttackData != null )
			{
				PrepareAttackLogFilters(AttackData, g);
				DrawAttackSummary(AttackData);
			}
		}

		private void PrepareCombatSummaryFilters(CombatLogEntryCollection cc, GameLog g)
		{
			PrepareWeaponContextMenu(cc, g);
			PrepareHitTypeContextMenu(cc, g);
			PrepareTargetContextMenu(cc, g);
		}

		private void PrepareTargetContextMenu(CombatLogEntryCollection cc, GameLog g)
		{
			int newItem;
			contextMenuTarget.MenuItems.Clear();
			contextMenuTarget.Tag = (object)lblTargetFilter;
		
			newItem = contextMenuTarget.MenuItems.Add(new TD.SandBar.MenuButtonItem("All", new System.EventHandler(this.contextMenu_Activate)));
			contextMenuTarget.MenuItems[newItem].Checked = true;
		
			string[] Entries;

			if ( AllGameLogs.LogCache.CacheData.Contains(g.FileName) )
			{
				CombatLogCache cache = AllGameLogs.LogCache.CacheData[g.FileName];
				Entries = cache.TargetsAttacked;
			}
			else
			{
				Entries = cc.GetUniqueTargets();
			}

            //TODO: Fix this
            int itemCount = 0;
            foreach (string s in Entries)
            {
                newItem = contextMenuTarget.MenuItems.Add(new TD.SandBar.MenuButtonItem(s, new System.EventHandler(this.contextMenu_Activate)));

                if (itemCount == 0)
                    contextMenuTarget.MenuItems[newItem].BeginGroup = true;

                itemCount++;
            }
        }
		
		private void PrepareWeaponContextMenu(CombatLogEntryCollection cc, GameLog g)
		{
			int newItem;
			contextMenuWeapon.MenuItems.Clear();
			contextMenuWeapon.Tag = (object)lblWeaponFilter;
		
			newItem = contextMenuWeapon.MenuItems.Add(new TD.SandBar.MenuButtonItem("All", new System.EventHandler(this.contextMenu_Activate)));
			contextMenuWeapon.MenuItems[newItem].Checked = true;

			string[] Entries;

			logger.Log(Level.Verbose, "Checking to see if file is in cache: " + g.FileName);

			if ( AllGameLogs.LogCache.CacheData.Contains(g.FileName.ToLower()) )
			{
				logger.Log(Level.Verbose, "PrepareWeaponContextMenu: File in cache");
				CombatLogCache cache = AllGameLogs.LogCache.CacheData[g.FileName];
				Entries = cache.WeaponsUsed;
			}
			else
			{
				logger.Log(Level.Verbose, "PrepareWeaponContextMenu: File not in cache");
				Entries = cc.GetUniqueWeaponsList();
			}
		
			int itemCount = 0;
			foreach (string s in Entries)
			{
				newItem = contextMenuWeapon.MenuItems.Add(new TD.SandBar.MenuButtonItem(s, new System.EventHandler(this.contextMenu_Activate)));
		
				if ( itemCount == 0)
					contextMenuWeapon.MenuItems[newItem].BeginGroup = true;
						
				itemCount++;
			}
		
		}
		
		private void PrepareHitTypeContextMenu(CombatLogEntryCollection cc, GameLog g)
		{
			int newItem;
			contextMenuHitTypes.MenuItems.Clear();
			contextMenuHitTypes.Tag = (object)lblHitType;
		
			newItem = contextMenuHitTypes.MenuItems.Add(new TD.SandBar.MenuButtonItem("All", new System.EventHandler(this.contextMenu_Activate)));
			contextMenuHitTypes.MenuItems[newItem].Checked = true;

			string[] Entries;

			if ( AllGameLogs.LogCache.CacheData.Contains(g.FileName) )
			{
				Debug.WriteLine("Getting hit type entries from cache");
				CombatLogCache cache = AllGameLogs.LogCache.CacheData[g.FileName];
				Entries = cache.HitTypes;
			}
			else
			{
				Debug.WriteLine("Getting hit type menu entries from source file");
				Entries = cc.GetUniqueHitTypes();
			}

            //TODO: Fix this.
            string[] sortedEntries = HitTypeLib.Sort(Entries, SortOrder.Descending);

            int itemCount = 0;
            foreach (string s in sortedEntries)
            {
                newItem = contextMenuHitTypes.MenuItems.Add(new TD.SandBar.MenuButtonItem(s, new System.EventHandler(this.contextMenu_Activate)));

                if (itemCount == 0)
                    contextMenuHitTypes.MenuItems[newItem].BeginGroup = true;

                itemCount++;
            }
        }

		private void DoCombatLog(GameLog g)
		{
			#region Events
			// Wire up progress events
			if ( CombatLogProcessStarted != null )
				g.ProcessingStarted += CombatLogProcessStarted;

			if ( CombatLogProcessUpdate != null )
				g.ProcessingUpdate += CombatLogProcessUpdate;

			if ( CombatLogProcessComplete != null )
				g.ProcessingComplete += CombatLogProcessComplete;
			#endregion

			CombatLogEntryCollection cl = g.GetCombatEntries();

			if ( cl != null )
			{
				PrepareCombatSummaryFilters(cl, g);
				DrawCombatSummary(cl);
				DoLocationTest(cl);
			}
		}

		private AttackEntryCollection GetFilteredAttackLog(AttackEntryCollection AttackData)
		{
			AttackEntryCollection FilteredList = AttackData;
		
			if ( lblAttackWeapon.Text != "All" && lblAttackWeapon.Text.Length != 0 ) // 'All' is not selected
				FilteredList = FilteredList.FilterByWeapon(lblAttackWeapon.Text.Trim());
		
			if ( lblAttackHitType.Text != "All" && lblAttackHitType.Text.Length != 0 )
				FilteredList = FilteredList.FilterByHitType(lblAttackHitType.Text.Trim());
		
			if ( lblAttackAttacker.Text != "All" && lblAttackAttacker.Text.Length != 0 )
				FilteredList = FilteredList.FilterByAttacker(lblAttackAttacker.Text.Trim());
		
			return FilteredList;
		}

		private void DrawCombatSummary(CombatLogEntryCollection cc)
		{
			listViewCombatSummary.Tag = cc;
			listViewCombatSummary.Items.Clear();
		
			CombatLogEntryCollection FilteredLog = GetFilteredCombatLog(cc, cbShowNotifyMessages.Checked);
		
			ListViewItem[] l = new ListViewItem[FilteredLog.Count];

			int i = 0;
			foreach ( CombatLogEntry c in FilteredLog )
			{
				if ( !c.IsNotifyMessage )
				{
					l[i] = new ListViewItem(new string[] { c.LogDTM.ToShortDateString() + " " + c.LogDTM.ToShortTimeString() + ":" + c.LogDTM.Second.ToString("00"), c.WeaponName, c.HitType, c.TargetName, c.DamageCaused.ToString("0.0")} );
		
					l[i].ForeColor = GetHitTypeColor(c.HitDescription);
					l[i].Tag = c;

					i++;
				}
				else if ( cbShowNotifyMessages.Checked )
				{
					l[i] = new ListViewItem(new string[] { c.LogDTM.ToShortDateString() + " " + c.LogDTM.ToShortTimeString() + ":" + c.LogDTM.Second.ToString("00"), c.WeaponName, "", "", ""} );
					l[i].Tag = c;
					l[i].ForeColor = Color.Blue;

					i++;
				}
			}
		
			listViewCombatSummary.Items.AddRange(l);

			FilteredLog.GenerateWeaponStats();
			DrawWeaponSummary(FilteredLog);
			DrawOverallSummary(FilteredLog);
		}

		private void DoLocationTest(CombatLogEntryCollection cc)
		{
			LocationInfo.LocationCollection locs = AllGameLogs.GetLocations(ThisGameLog, cc[0].LogDTM);

			if ( locs == null ) // No location data, bail out!
			{
				return;
			}
			
			locs.SortByLogDTM(SortOrder.Ascending);

			// TODO: Fix issues with location data. See file : Size 12437 20040630_001219.txt
			string lastLocation = "";
			foreach ( LocationInfo.Location l in locs )
			{
				if ( l.LogDTM < cc[0].LogDTM )
					lastLocation = l.LocationStr;
			}

			if ( lastLocation.Length > 0 )
				this.Text = this.Text + " / " + lastLocation;
		}

		private void DrawWeaponGraph(WeaponSummary w, int Count)
		{
			// DeletePreviousGraphs();
			HGraph.ChartPane c = new HGraph.ChartPane();
			c.Chart1.ChartItems = new ChartItemCollection();

			c.PaneCaption2.Caption = w.WeaponName;

			foreach ( HitTypeInfo ht in w.HitSummary )
			{
				c.Chart1.ChartItems.Add(new ChartItem(HitTypeLib.GetDisplayString(ht.HitTypeString), ht.DamageCaused));
			}

			int VerticalPosition;

			if ( GraphControls.Count == 0 )
				VerticalPosition = 8;
			else
			{
				HGraph.ChartPane cp = (HGraph.ChartPane)GraphControls[GraphControls.Count - 1].Control;
				VerticalPosition = cp.Location.Y + cp.Height + 16;
			}

			c.Location = new Point(8,VerticalPosition);

			c.Chart1.CalculateBounds();
			c.Height = c.PaneCaption2.Size.Height + c.Chart1.Size.Height;
			c.Width = dockControl2.Width - 16;
			c.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

			dockControl2.Controls.Add(c);
			GraphControls.Add(new GraphItem(GraphControls.LastSequenceID, (object)c));
		}


		private void DrawAttackerSummary(AttackEntryCollection AttackData)
		{
			listViewAttackSummary.Items.Clear();

			int		TotalFired			= 0;
			int		TotalHit			= 0;
			int		TotalMissed			= 0;
			float	TotalDamage			= 0.0F;
			float	AverageDamage		= 0.0F;
			float	PercentHit			= 0.0F;
			float	PercentMissed		= 0.0F;
			double	DamagePerMinute		= 0.0F;
			double	ShotsPerMinute		= 0.0F;
			string	ShotsPerMinuteStr	= "";
			string	DamagePerMinuteStr	= "";

			double TotalAverageDamage	= 0.0F;
			double TotalShotsPerMinute	= 0.0F;
			string TotalShotsPerMinuteStr = "";

			double TotalDamagePerMinute = 0.0F;
			double TotalDamagePerSecond = 0.0F;
			string TotalDurationStr;

			TimeSpan FireDuration;
			string duration;
		
			TimeSpan TotalDuration  = AttackData.LogEntriesEndDTM.Subtract(AttackData.LogEntriesStartDTM);

			if ( TotalDuration.TotalMinutes < 1 )
				TotalDurationStr = TotalDuration.TotalSeconds.ToString("0") + " secs";
			else
				TotalDurationStr = TotalDuration.TotalMinutes.ToString("0") + " mins";

			foreach ( string AttackerName in AttackData.AttackerData.Keys )
			{
				AttackerSummary ws = (AttackerSummary)AttackData.AttackerData[AttackerName];
		
				try { FireDuration = ws.FiringEndedDTM - ws.FiringStartedDTM;}
				catch { FireDuration = new TimeSpan(0); }

				if ( FireDuration.TotalMinutes > 1 )
				{
					duration			= FireDuration.TotalMinutes.ToString("0") + " mins";
					DamagePerMinute		= ws.TotalDamage / FireDuration.TotalMinutes;
					DamagePerMinuteStr	= DamagePerMinute.ToString("0.00");
					ShotsPerMinute		= ws.ShotsFired / FireDuration.TotalMinutes;
					ShotsPerMinuteStr	= ShotsPerMinute.ToString("0.00");
				}
				else
				{
					duration = FireDuration.TotalSeconds.ToString("0") + " secs";
					DamagePerMinuteStr = "-";
				}

				ListViewItem l = new ListViewItem(new string[] {ws.AttackerName, ws.ShotsFired.ToString(), ws.ShotsHit.ToString(), ws.ShotsMissed.ToString(), ws.TotalDamage.ToString("0.00"), ws.AverageDamage.ToString("0.00"), ws.PercentageShotsHit.ToString("0.0%"),ws.PercentageShotsMissed.ToString("0.0%"), duration, ShotsPerMinuteStr, DamagePerMinuteStr});

				l.Tag = ws.AttackerName;
				
				listViewAttackSummary.Items.Add(l);
		
				TotalFired		+= ws.ShotsFired;
				TotalHit		+= ws.ShotsHit;
				TotalMissed		+= ws.ShotsMissed;
				TotalDamage		+= (float)ws.TotalDamage;
				AverageDamage	+= (float)ws.AverageDamage;
				PercentHit		+= ws.PercentageShotsHit;
				PercentMissed	+= ws.PercentageShotsMissed;
			}
		
			//
			// Add a blank line to the list view to seperate entries from the totals line
			//
			ListViewItem ll = new ListViewItem(new string[] {""});
			ll.Tag = "BLANK";
			listViewAttackSummary.Items.Add(ll);

			//
			// Calculate totals and create the final row in the list view
			//
			PercentHit = (float)TotalHit / (float)TotalFired;
			PercentMissed = (float)TotalMissed / (float)TotalFired;

			TotalDamagePerMinute = TotalDamage / TotalDuration.TotalMinutes;
			TotalDamagePerSecond = TotalDamagePerMinute / 60;

			TotalShotsPerMinute	= TotalFired / TotalDuration.TotalMinutes;
			TotalShotsPerMinuteStr = TotalShotsPerMinute.ToString("0.00");

			TotalAverageDamage = TotalDamage /  TotalFired;

			ll = new ListViewItem(new string[] {"Total",TotalFired.ToString(),TotalHit.ToString(), TotalMissed.ToString(), TotalDamage.ToString("0.00"), TotalAverageDamage.ToString("0.00"), PercentHit.ToString("0.0%"),PercentMissed.ToString("0.0%"), TotalDurationStr, TotalShotsPerMinuteStr, TotalDamagePerMinute.ToString("0.00"), TotalDamagePerSecond.ToString("0.00") });
			ll.Tag = "TOTAL";
		
			listViewAttackSummary.Items.Add(ll);

			//
			// Set the position of the splitter so that all of the summary listview is on-screen
			//
			splitterAttacker.SplitPosition = ll.Bounds.Height * (listViewAttackSummary.Items.Count + 3);
		}

		private void DrawWeaponSummary(CombatLogEntryCollection cl)
		{
			listViewCombatSummaryStats.Items.Clear();
			int		TotalFired			= 0;
			int		TotalHit			= 0;
			int		TotalMissed			= 0;
			float	TotalDamage			= 0.0F;
			float	AverageDamage		= 0.0F;
			float	PercentHit			= 0.0F;
			float	PercentMissed		= 0.0F;
			double	DamagePerMinute		= 0.0F;
			double	ShotsPerMinute		= 0.0F;
			string	ShotsPerMinuteStr	= "";
			string	DamagePerMinuteStr	= "";

			double TotalAverageDamage	= 0.0F;
			double TotalShotsPerMinute	= 0.0F;
			string TotalShotsPerMinuteStr = "";

			double TotalDamagePerMinute = 0.0F;
			double TotalDamagePerSecond = 0.0F;
			string TotalDurationStr;

			TimeSpan FireDuration;
			string duration;
		
			TimeSpan TotalDuration  = cl.LogEntriesEndDTM.Subtract(cl.LogEntriesStartDTM);

			if ( TotalDuration.TotalMinutes < 1 )
				TotalDurationStr = TotalDuration.TotalSeconds.ToString("0") + " secs";
			else
				TotalDurationStr = TotalDuration.TotalMinutes.ToString("0") + " mins";

			foreach ( string WeaponName in cl.WeaponData.Keys )
			{
				WeaponSummary ws = (WeaponSummary)cl.WeaponData[WeaponName];
		
				try { FireDuration = ws.FiringEndedDTM - ws.FiringStartedDTM;}
				catch { FireDuration = new TimeSpan(0); }

				if ( FireDuration.TotalMinutes > 1 )
				{
					duration			= FireDuration.TotalMinutes.ToString("0") + " mins";
					DamagePerMinute		= ws.TotalDamage / FireDuration.TotalMinutes;
					DamagePerMinuteStr	= DamagePerMinute.ToString("0.00");
					ShotsPerMinute		= ws.ShotsFired / FireDuration.TotalMinutes;
					ShotsPerMinuteStr	= ShotsPerMinute.ToString("0.00");
				}
				else
				{
					duration = FireDuration.TotalSeconds.ToString("0") + " secs";
					DamagePerMinuteStr = "-";
				}

				ListViewItem l = new ListViewItem(new string[] {ws.WeaponName, ws.ShotsFired.ToString(), ws.ShotsHit.ToString(), ws.ShotsMissed.ToString(), ws.TotalDamage.ToString("0.00"), ws.AverageDamage.ToString("0.00"), ws.PercentageShotsHit.ToString("0.0%"),ws.PercentageShotsMissed.ToString("0.0%"), duration, ShotsPerMinuteStr, DamagePerMinuteStr});

				l.Tag = ws.WeaponName;
				
				listViewCombatSummaryStats.Items.Add(l);
		
				TotalFired		+= ws.ShotsFired;
				TotalHit		+= ws.ShotsHit;
				TotalMissed		+= ws.ShotsMissed;
				TotalDamage		+= (float)ws.TotalDamage;
				AverageDamage	+= (float)ws.AverageDamage;
				PercentHit		+= ws.PercentageShotsHit;
				PercentMissed	+= ws.PercentageShotsMissed;
			}
		
			//
			// Add a blank line to the list view to seperate entries from the totals line
			//
			ListViewItem ll = new ListViewItem(new string[] {""});
			ll.Tag = "BLANK";
			listViewCombatSummaryStats.Items.Add(ll);

			//
			// Calculate totals and create the final row in the list view
			//
			PercentHit = (float)TotalHit / (float)TotalFired;
			PercentMissed = (float)TotalMissed / (float)TotalFired;

			TotalDamagePerMinute = TotalDamage / TotalDuration.TotalMinutes;
			TotalDamagePerSecond = TotalDamagePerMinute / 60;

			TotalShotsPerMinute	= TotalFired / TotalDuration.TotalMinutes;
			TotalShotsPerMinuteStr = TotalShotsPerMinute.ToString("0.00");

			TotalAverageDamage = TotalDamage /  TotalFired;

			ll = new ListViewItem(new string[] {"Total",TotalFired.ToString(),TotalHit.ToString(), TotalMissed.ToString(), TotalDamage.ToString("0.00"), TotalAverageDamage.ToString("0.00"), PercentHit.ToString("0.0%"),PercentMissed.ToString("0.0%"), TotalDurationStr, TotalShotsPerMinuteStr, TotalDamagePerMinute.ToString("0.00"), TotalDamagePerSecond.ToString("0.00") });
			ll.Tag = "TOTAL";
		
			listViewCombatSummaryStats.Items.Add(ll);

			int oldHeight = listViewCombatSummaryStats.Size.Height;
			int oldSplitterPosition = splitter1.SplitPosition;
			int newSplitterPosition = listViewCombatSummaryStats.Items[0].Bounds.Height * ( listViewCombatSummaryStats.Items.Count + 3);

//			Debug.WriteLine("Weapon Summary: Items: " + listViewCombatSummaryStats.Items.Count);
//			Debug.WriteLine("Weapon Summary: Old Height: " + oldHeight);
//			Debug.WriteLine("Weapon Summary: Old Splitter Position: " + oldSplitterPosition);

			splitter1.SplitPosition = newSplitterPosition;

//			Debug.WriteLine("Weapon Summary: New Height: " + listViewCombatSummaryStats.Size.Height);
//			Debug.WriteLine("Weapon Summary: New Splitter Position: " + newSplitterPosition);
		}

		private void DeletePreviousGraphs()
		{
			foreach ( GraphItem g in GraphControls )
			{
				dockControl2.Controls.Remove((Control)g.Control);
			}

			GraphControls.Clear();
		}

		private void DrawOverallSummary(CombatLogEntryCollection cl)
		{
			// For each Weapon
			// Draw proportional damage types
				
			DeletePreviousGraphs();
		
			int WeaponCount = 0;
			StringBuilder TextReport = new StringBuilder();
			
			WeaponSummaryCollection WeaponsData = new WeaponSummaryCollection();

			foreach ( Object w in cl.WeaponData.Keys )
			{
				TextReport.Append("\n" + w.ToString() + "\n\n");
		
				WeaponSummary ws = (WeaponSummary)cl.WeaponData[w];
		
				ws.HitSummary.SortByHitType(System.Windows.Forms.SortOrder.Descending);

				foreach ( HitTypeInfo h in ws.HitSummary )
				{
					h.DamagePercentage = (float)(h.DamageCaused / (ws.TotalDamage / 100F));
					h.HitPercentage = (float)(h.HitCount / (ws.ShotsFired / 100F));
					TextReport.Append("\t" + h.HitTypeString + "\t" + h.HitCount + "\t" + h.DamageCaused + "\n");
				}
		
				WeaponsData.Add(ws);

				DrawWeaponGraph(ws, WeaponCount++);
			}

			WeaponsData.SortByWeaponName(SortOrder.Ascending);

			StringBuilder html = new StringBuilder(GetRenderedHTML(WeaponsData));
			ShowHTML(html.ToString());
		}

		private void SaveWeaponsData(WeaponSummaryCollection wd)
		{
			logger.Log(Level.Info, "Saving weapon summary to disk");

			try
			{
				FileStream fs = new FileStream(Application.StartupPath + @"\ws.xml", FileMode.Create);

				XmlSerializer xs = new XmlSerializer(typeof(WeaponSummaryCollection));

				xs.Serialize(fs, wd);

				fs.Close();
			}
			catch (Exception e)
			{
				MessageBox.Show("Problem saving weapon summary data!: " + e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				logger.Log(Level.Exception, "Error saving weapon summary to disk: " + e.ToString());
				return;
			}

			logger.Log(Level.Info, "Weapon summary saved to disk.");
		}

		private Color GetHitTypeColor(string HitTypeDescription)
		{
			int HitTypeRank = HitTypeLib.GetRank(HitTypeDescription);

			switch ( HitTypeRank )
			{
				case 90:
					return Color.Red;

				case 80:
					return Color.Orange;

				case 30:
					return Color.Gray;

				case 20:
					return Color.Gray;

				case 10:
					return Color.Gray;

				case 70:
					return Color.Green;

				default:
					return Color.Black;
			}
		}

		private CombatLogEntryCollection GetFilteredCombatLog(CombatLogEntryCollection cc, bool IncludeNotifyMessages)
		{
			CombatLogEntryCollection FilteredList = cc;
		
			if ( lblWeaponFilter.Text != "All" && lblWeaponFilter.Text.Length != 0 ) // 'All' is not selected
				FilteredList = FilteredList.FilterByWeapon(lblWeaponFilter.Text.Trim(), IncludeNotifyMessages);
		
			if ( lblHitType.Text != "All" && lblHitType.Text.Length != 0 )
				FilteredList = FilteredList.FilterByHitType(lblHitType.Text.Trim(), IncludeNotifyMessages);
		
			if ( lblTargetFilter.Text != "All" && lblTargetFilter.Text.Length != 0 )
				FilteredList = FilteredList.FilterByTarget(lblTargetFilter.Text.Trim(), IncludeNotifyMessages);
		
			CombatLogEntryCollection NotifyFilteredList = new CombatLogEntryCollection();

			if ( !IncludeNotifyMessages )
			{
				foreach ( CombatLogEntry c in FilteredList )
				{
					if ( !c.IsNotifyMessage )
						NotifyFilteredList.Add(c);
				}
			}
			else
				NotifyFilteredList = FilteredList;

			Debug.WriteLine("Filtered list linecount = " + NotifyFilteredList.Count);
			return NotifyFilteredList;
		}

		private void DrawFilterText(TD.SandBar.ContextMenuBarItem Menu)
		{
			Label tb = (Label)Menu.Tag;
			tb.Text = "";

			int i = 0;
			foreach ( TD.SandBar.MenuButtonItem m in Menu.MenuItems )
			{
				if ( m.Checked )
				{
					if ( i > 0 )
						tb.Text += "; ";

					tb.Text += m.Text;

					i++;
				}
			}
		}

		private bool TDMenuItemsChecked(TD.SandBar.ContextMenuBarItem menu)
		{
			bool itemsChecked = false;

			for (int i = 0; i < menu.MenuItems.Count; i++ )
			{
				if ( i > 0 )
				{
					if ( menu.MenuItems[i].Checked )
					{
						itemsChecked = true;
						break;
					}
				}
			}

			return itemsChecked;
		}

		private void TDClearSelectedItems(TD.SandBar.ContextMenuBarItem menu)
		{
			for (int i = 0; i < menu.MenuItems.Count; i++ )
				menu.MenuItems[i].Checked = false;
		}

		private void HandleAttackContextMenuClick(TD.SandBar.ContextMenuBarItem Menu, TD.SandBar.MenuButtonItem Button)
		{
			Debug.WriteLine("Menu title = " + Menu.Text);

			Debug.WriteLine("Button = " + Button.Text);

			if ( Button.Text == "All" )
			{
				if ( Button.Checked )
					return;
				else
				{
					foreach ( TD.SandBar.MenuButtonItem mi in Menu.MenuItems )
						mi.Checked = false;

					Button.Checked = true;
				}
			}
			else
			{
				bool ControlPressed = (Control.ModifierKeys & Keys.Control) == Keys.Control;

				if ( Button.Checked )
				{
					Button.Checked = false;

					// Is anything else checked? If not, ensure that the All option is

					if ( !TDMenuItemsChecked(Menu) )
						Menu.MenuItems[0].Checked = true;
				}
				else
				{
					// Tick this item

					if ( !ControlPressed )
						TDClearSelectedItems(Menu);

					Button.Checked = true;
					Menu.MenuItems[0].Checked = false; // Clear the 'All' option
				}
			}

			DrawFilterText(Menu);

			AttackEntryCollection AttackData = (AttackEntryCollection)listViewAttackData.Tag;
			DrawAttackSummary(AttackData);
		}

		private void HandleContextMenuClick(TD.SandBar.ContextMenuBarItem Menu, TD.SandBar.MenuButtonItem Button)
		{
			Debug.WriteLine("Menu title = " + Menu.Text);

			Debug.WriteLine("Button = " + Button.Text);

			if ( Button.Text == "All" )
			{
				if ( Button.Checked )
					return;
				else
				{
					foreach ( TD.SandBar.MenuButtonItem mi in Menu.MenuItems )
						mi.Checked = false;

					Button.Checked = true;
				}
			}
			else
			{
				bool ControlPressed = (Control.ModifierKeys & Keys.Control) == Keys.Control;

				if ( Button.Checked )
				{
					Button.Checked = false;

					// Is anything else checked? If not, ensure that the All option is

					if ( !TDMenuItemsChecked(Menu) )
						Menu.MenuItems[0].Checked = true;
				}
				else
				{
					// Tick this item

					if ( !ControlPressed )
						TDClearSelectedItems(Menu);

					Button.Checked = true;
					Menu.MenuItems[0].Checked = false; // Clear the 'All' option
				}
			}

			DrawFilterText(Menu);

			CombatLogEntryCollection cc = (CombatLogEntryCollection)listViewCombatSummary.Tag;
			DrawCombatSummary(cc);
		}

		private void contextMenuAttack_Activate(object sender, System.EventArgs e)
		{
			TD.SandBar.MenuButtonItem m = (TD.SandBar.MenuButtonItem)sender;
			TD.SandBar.ContextMenuBarItem Menu = (TD.SandBar.ContextMenuBarItem)m.Parent;

			HandleAttackContextMenuClick(Menu, m);
		}

		private void contextMenu_Activate(object sender, System.EventArgs e)
		{
			TD.SandBar.MenuButtonItem m = (TD.SandBar.MenuButtonItem)sender;
			TD.SandBar.ContextMenuBarItem Menu = (TD.SandBar.ContextMenuBarItem)m.Parent;

			HandleContextMenuClick(Menu, m);
		}

		//
		// Open the Weapon Filter Context Menu
		//
		private void btnWeaponFilter_Click(object sender, System.EventArgs e)
		{
			contextMenuWeapon.Show(btnWeaponFilter, new Point(btnWeaponFilter.Width, 0));
		}

		//
		// Open the Hit Type Filter Context Menu
		//
		private void btnHitTypeMenu_Click(object sender, System.EventArgs e)
		{
            //try
            //{
            //    contextMenuHitTypes.Show(btnHitTypeMenu, new Point(btnHitTypeMenu.Width, 0));
            //}
            //catch (Exception ex)
            //{

            //}
		}

		//
		// Open the Target Filter Context Menu
		//
		private void btnTargetFilter_Click(object sender, System.EventArgs e)
		{
			contextMenuTarget.Show(btnTargetFilter, new Point(btnTargetFilter.Width, 0));
		}

		public void Merge(GameLog[] MergeLogs)
		{
			MessageBox.Show("Request to merge " + MergeLogs.Length + " with " + ((GameLog)this.Tag).FileName, "Message");
		}

		//
		// Handle dragging of items from the main combat list view to a text editor
		//
		private void listViewCombatSummary_ItemDrag(object sender, System.Windows.Forms.ItemDragEventArgs e)
		{
			StringBuilder exportText = new StringBuilder();

			long[] Positions = new long[((ListView)sender).SelectedItems.Count];

			int i = 0;
			foreach ( ListViewItem l in ((ListView)sender).SelectedItems )
			{
				CombatLogEntry c = (CombatLogEntry)l.Tag;
				Positions[i++] = c.PositionInFile;
			}

			exportText.Append(Utils.LogFile.GetLineFromPosition(ThisGameLog.FileName, Positions));
			listViewCombatSummary.DoDragDrop(exportText.ToString(), DragDropEffects.Copy);
		}

		//
		// Ensure we indicate that we will not accept drops on our main list view
		//
		private void listViewCombatSummary_DragOver(object sender, System.Windows.Forms.DragEventArgs e)
		{
			e.Effect = DragDropEffects.None;
		}

		//
		// ListView sorter
		//
		private void DoCombatSummarySort(int ColumnID)
		{
			if ( CombatSummarySorter.SortColumn == ColumnID )
			{
				if ( CombatSummarySorter.Direction == System.Windows.Forms.SortOrder.Ascending )
					CombatSummarySorter.Direction = System.Windows.Forms.SortOrder.Descending;
				else
					CombatSummarySorter.Direction = System.Windows.Forms.SortOrder.Ascending;
			}
			else
				CombatSummarySorter.Direction = System.Windows.Forms.SortOrder.Ascending;

			CombatSummarySorter.SortColumn = ColumnID;

			listViewCombatSummary.ListViewItemSorter = CombatSummarySorter;
			listViewCombatSummary.Sort();
			
			if ( listViewCombatSummary.SelectedItems.Count == 1 )
				listViewCombatSummary.EnsureVisible(listViewCombatSummary.SelectedItems[0].Index);
		}

		private void DoAttackSummarySort(int ColumnID)
		{
			if ( AttackLogSorter.SortColumn == ColumnID )
			{
				if ( AttackLogSorter.Direction == System.Windows.Forms.SortOrder.Ascending )
					AttackLogSorter.Direction = System.Windows.Forms.SortOrder.Descending;
				else
					AttackLogSorter.Direction = System.Windows.Forms.SortOrder.Ascending;
			}
			else
				AttackLogSorter.Direction = System.Windows.Forms.SortOrder.Ascending;

			AttackLogSorter.SortColumn = ColumnID;

			listViewAttackData.Sort();
			
			if ( listViewAttackData.SelectedItems.Count == 1 )
				listViewAttackData.EnsureVisible(listViewAttackData.SelectedItems[0].Index);
		}

		private void listViewCombatSummary_ColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
		{
			DoCombatSummarySort(e.Column);
		}

		private void listViewAttackData_ColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
		{
			DoAttackSummarySort(e.Column);		
		}


		//
		// Usability short-cut here. If the user activates an entry in the summary list view,
		// take this as an indication that the user wants to see more information on this
		// entry.
		//
		// To this end, simulate a click on the Weapon filter context menu to invoke filtering by
		// weapon in the main view.
		//
		private void listView2_ItemActivate(object sender, System.EventArgs e)
		{
			string s = ((ListView)sender).SelectedItems[0].Tag.ToString();

			// The Blank and Total lines in the summary list view should be ignored.
			if ( s == "BLANK" || s == "TOTAL" )
				return;

			int i = 0;
			foreach ( TD.SandBar.MenuButtonItem mb in contextMenuWeapon.MenuItems )
			{
				if ( mb.Text == s )
					contextMenu_Activate(contextMenuWeapon.MenuItems[i], new System.EventArgs());

				i++;
			}
		}

		//
		// Copy the source log text for the selected items into the Windows clipboard
		//
		private void menuItem6_Click(object sender, System.EventArgs e)
		{
			if ( listViewCombatSummary.SelectedItems.Count == 0 )
				return;

			StringBuilder exportText = new StringBuilder();
			foreach ( ListViewItem l in listViewCombatSummary.SelectedItems )
			{
				CombatLogEntry c = (CombatLogEntry)l.Tag;
				exportText.Append(Utils.LogFile.GetLineFromPosition(ThisGameLog.FileName, c.PositionInFile));
			}

			Clipboard.SetDataObject(exportText.ToString(), true);
		}


		private void DrawAttackSummaryForSelectedItems()
		{
			AttackEntryCollection AttackData = new AttackEntryCollection();

			if ( listViewAttackData.SelectedItems.Count <= 1) // 0 or 1 items selected, draw the entire list
			{
				if ( LastAttackRedrawWasUserSelection )
				{
					AttackData = (AttackEntryCollection)listViewAttackData.Tag;
					DrawAttackSummary(AttackData);
					LastAttackRedrawWasUserSelection = false;
				}

				return;
			}

			foreach ( ListViewItem l in listViewAttackData.SelectedItems )
			{
				AttackEntry c = (AttackEntry)l.Tag;
				AttackData.Add(c);
			}

			LastAttackRedrawWasUserSelection = true;

			AttackData.GenerateAttackerStats();
			DrawAttackerSummary(AttackData);
			// DrawOverallSummary(cc);
		}

		private void DrawCombatSummaryForSelectedItems()
		{
			CombatLogEntryCollection cc = new CombatLogEntryCollection();

			if ( listViewCombatSummary.SelectedItems.Count <= 1) // 0 or 1 items selected, draw the entire list
			{
				if ( LastRedrawWasUserSelection )
				{
					cc = (CombatLogEntryCollection)listViewCombatSummary.Tag;
					DrawCombatSummary(cc);
					//					cc.GenerateWeaponStats();
					//					DrawWeaponSummary(cc);
					//					DrawOverallSummary(cc);
					LastRedrawWasUserSelection = false;
				}

				return;
			}

			foreach ( ListViewItem l in listViewCombatSummary.SelectedItems )
			{
				CombatLogEntry c = (CombatLogEntry)l.Tag;
				cc.Add(c);
			}

			LastRedrawWasUserSelection = true;

			cc.GenerateWeaponStats();
			DrawWeaponSummary(cc);
			DrawOverallSummary(cc);
		}

		private void listViewCombatSummary_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			SelectedIndexTimer.Stop();
			SelectedIndexTimer.Interval = 800;
			SelectedIndexTimer.Start();
		}

		private void SelectedIndexTimer_Tick(object sender, System.EventArgs e)
		{
			SelectedIndexTimer.Stop();
			DrawCombatSummaryForSelectedItems();
		}

		private void dockControl1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
		
		}

		private void btnAttackWeapon_Click(object sender, System.EventArgs e)
		{
			contextMenuAttackWeapon.Show(btnAttackWeapon, new Point(btnAttackWeapon.Width, 0));
		}

		private void btnAttackAttacker_Click(object sender, System.EventArgs e)
		{
			contextMenuAttackAttacker.Show(btnAttackAttacker, new Point(btnAttackAttacker.Width,0));
		}

		private void btnAttackHitType_Click(object sender, System.EventArgs e)
		{
			contextMenuAttackHitType.Show(btnAttackHitType, new Point(btnAttackHitType.Width, 0));
		}

		private void listViewAttackData_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			AttackSelectedIndexTimer.Stop();
			AttackSelectedIndexTimer.Interval = 800;
			AttackSelectedIndexTimer.Start();
		}

		private void AttackSelectedIndexTimer_Tick(object sender, EventArgs e)
		{
			AttackSelectedIndexTimer.Stop();
			DrawAttackSummaryForSelectedItems();
		}

		private void listViewAttackSummary_ItemActivate(object sender, System.EventArgs e)
		{
			string s = ((ListView)sender).SelectedItems[0].Tag.ToString();

			// The Blank and Total lines in the summary list view should be ignored.
			if ( s == "BLANK" || s == "TOTAL" )
				return;

			int i = 0;
			foreach ( TD.SandBar.MenuButtonItem mb in contextMenuAttackAttacker.MenuItems )
			{
				if ( mb.Text == s )
					contextMenuAttack_Activate(contextMenuAttackAttacker.MenuItems[i], new System.EventArgs());

				i++;
			}
		}

		public int GetActiveTab()
		{
			if ( dockControlAttackers.Focused )
				return 2;

			return 1;
		}

		private void menuItemAttackCopyText_Click(object sender, System.EventArgs e)
		{
			if ( listViewAttackData.SelectedItems.Count == 0 )
				return;

			StringBuilder exportText = new StringBuilder();
			foreach ( ListViewItem l in listViewAttackData.SelectedItems )
			{
				AttackEntry ae = (AttackEntry)l.Tag;
				exportText.Append(Utils.LogFile.GetLineFromPosition(ThisGameLog.FileName, ae.PositionInFile));
			}

			Clipboard.SetDataObject(exportText.ToString(), true);		
		}

		private void contextMenuAttackItemInfo_Popup(object sender, System.EventArgs e)
		{
			if ( listViewAttackData.SelectedItems.Count == 0 )
			{
				menuItemAttackCopyText.Enabled = false;
				menuItemAttackWeaponInfo.Enabled = false;
				menuItemAttackAttackerInfo.Enabled = false;
				return;
			}

			if ( listViewAttackData.SelectedItems.Count > 1 )
			{
				menuItemAttackWeaponInfo.Enabled = false;
				menuItemAttackAttackerInfo.Enabled = false;
				return;
			}

			menuItemAttackCopyText.Enabled = true;
			menuItemAttackWeaponInfo.Enabled = true;
			menuItemAttackAttackerInfo.Enabled = true;

			AttackEntry ae = (AttackEntry)listViewAttackData.SelectedItems[0].Tag;

			if ( ae.WeaponName != "Turret" )
				menuItemAttackWeaponInfo.Enabled = true;
			else
				menuItemAttackWeaponInfo.Enabled = false;
		}

		private void Form2_Resize(object sender, System.EventArgs e)
		{
			// TODO: Form2 code does not check that MyConfig is not null. Catch this in Form2_Load. 
			// create a new instance of the Config object.
			try
			{
				if ( MyConfig.RememberWindowLayout )
					MyConfig.AnalysisWindowState = this.WindowState;
			}
			catch (Exception err)
			{
				logger.Log(Level.Exception, "Error occurred in form2_resize: " + err.ToString());
			}
		}

		private void DoObjectExplorerSearch(string SearchText)
		{
			ObjectExplorer.OEParser Parser = new ObjectExplorer.OEParser();
			Parser.MyConfig = MyConfig;

			ObjectExplorer.SearchResult[] Results = new ObjectExplorer.SearchResult[1];

			try
			{
				Results = Parser.GetSearchResults(SearchText);
			}
			catch (Exception err)
			{
				Debug.WriteLine(err.ToString());
				MessageBox.Show("There was a problem getting information on this item: " + err.Message, "Problem getting information", MessageBoxButtons.OK);
				return;
			}

			Debug.WriteLine("Found " + Results.Length + " results");

			foreach ( ObjectExplorer.SearchResult r in Results )
				Debug.WriteLine("Item: " + r.ItemName + " (" + r.ItemID.ToString() + ")");

			if ( Results.Length > 1 )
			{
				foreach ( ObjectExplorer.SearchResult r in Results )
				{
					if ( SearchText.ToLower().Trim() == r.ItemName.ToLower().Trim() )
					{
						Debug.WriteLine("FOUND EXACT MATCH: " + r.ItemName);
						Process.Start(ItemDataURL + r.ItemID);
					}
				}
			}

			if ( Results.Length == 1 )
			{
				Process.Start(ItemDataURL + Results[0].ItemID);
			}
		}

		private void ShowWeaponInWeaponBrowser(string WeaponName)
		{
			if ( EOWeaponData == null )
				return;

			WeaponDataDB.WeaponDataSource wep = EOWeaponData[WeaponName];

			if ( wep != null )
			{
				WeaponForm wf = new WeaponForm();
				wf.WeaponData = EOWeaponData;
				wf.SearchWeaponName = WeaponName;
				wf.SearchType = wep.Type;
				wf.SearchClass = wep.Class;

				wf.doTypeFilter = true;
				wf.doClassFilter = true;
				wf.doNameFilter = true;

				wf.MyConfig = MyConfig;

				wf.Show();
			}
		}

		private void menuItemWeaponInfo_Click(object sender, System.EventArgs e)
		{

			if ( listViewCombatSummary.SelectedItems.Count != 1 )
				return;

			CombatLogEntry c = (CombatLogEntry)listViewCombatSummary.SelectedItems[0].Tag;

			ShowWeaponInWeaponBrowser(c.WeaponName);

			// DoObjectExplorerSearch(c.WeaponName);
		}

		private void menuItemTargetInfo_Click(object sender, System.EventArgs e)
		{
			if ( listViewCombatSummary.SelectedItems.Count != 1 )
				return;

			CombatLogEntry c = (CombatLogEntry)listViewCombatSummary.SelectedItems[0].Tag;
			DoObjectExplorerSearch(c.TargetName);

//			OEResults sf = new OEResults();
//			OEResults.MyConfig = MyConfig;
//			sf.SearchText = c.TargetName;
//			sf.MdiParent = this.MdiParent;
//			sf.Show();
		}

		private void menuItemAttackWeaponInfo_Click(object sender, System.EventArgs e)
		{
			if ( listViewAttackData.SelectedItems.Count != 1 )
				return;

			AttackEntry ae = (AttackEntry)listViewAttackData.SelectedItems[0].Tag;
			WeaponDataDB.WeaponDataSource wep = EOWeaponData[ae.WeaponName];

			Debug.WriteLine("IN HERE");

			if ( wep != null )
			{
				Debug.WriteLine("IN THERE");

				WeaponForm wf = new WeaponForm();
				wf.WeaponData = EOWeaponData;
				wf.SearchWeaponName = ae.WeaponName;
				wf.SearchType = wep.Type;
				wf.SearchClass = wep.Class;

				wf.doTypeFilter = true;
				wf.doClassFilter = true;
				wf.doNameFilter = true;

				if ( MyConfig == null )
					Debug.WriteLine("FORM2: Myconfig is null!!");
				else
					Debug.WriteLine("FORM2: MyConfig is not null");

				wf.MyConfig = MyConfig;

				wf.Show();
			}

			// DoObjectExplorerSearch(ae.WeaponName);
		}

		private void menuItemAttackAttackerInfo_Click(object sender, System.EventArgs e)
		{
			if ( listViewAttackData.SelectedItems.Count != 1 )
				return;

			AttackEntry ae = (AttackEntry)listViewAttackData.SelectedItems[0].Tag;
			DoObjectExplorerSearch(ae.AttackerName);
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			if ( ThisGameLog == null )
			{
				MessageBox.Show("Source ThisGameLog is null!");
				return;
			}

			Debug.WriteLine("Creating graph window instance");
			Graphs.DmgOutputGraph gt = new Graphs.DmgOutputGraph();
			Debug.WriteLine("Graphwindow instance created");
			gt.thisGameLog = ThisGameLog;
			gt.DrawGraph();
			gt.Show();
		}

		private void listViewAttackData_ItemDrag(object sender, System.Windows.Forms.ItemDragEventArgs e)
		{
			StringBuilder exportText = new StringBuilder();

			long[] Positions = new long[((ListView)sender).SelectedItems.Count];

			int i = 0;
			foreach ( ListViewItem l in ((ListView)sender).SelectedItems )
			{
				AttackEntry ae = (AttackEntry)l.Tag;
				Positions[i++] = ae.PositionInFile;
			}

			exportText.Append(Utils.LogFile.GetLineFromPosition(ThisGameLog.FileName, Positions));
			((ListView)sender).DoDragDrop(exportText.ToString(), DragDropEffects.Copy);
		}

		private void cbShowNotifyMessages_CheckedChanged(object sender, System.EventArgs e)
		{
			Debug.WriteLine("NOTIFY checkbox state changed: " + Convert.ToString(cbShowNotifyMessages.Checked?"CHECKED":"NOT CHECKED"));
			DrawCombatSummary((CombatLog.CombatLogEntryCollection)listViewCombatSummary.Tag);
		}
	}
}
