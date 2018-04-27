using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using CombatLog.Config;

namespace CombatLog
{
	/// <summary>
	/// Summary description for Options.
	/// </summary>
	public class Options : System.Windows.Forms.Form
	{
		private ArrayList ActiveNotifyWindows = new ArrayList();

		public UserConfig MyConfig;

		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.TreeView treeViewOptionsNav;
		private System.Windows.Forms.GroupBox groupBoxGameLogDirs;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.Button btnAdd;
		private System.Windows.Forms.Button btnDelete;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
		public System.Windows.Forms.ListView lvPaths;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Panel panelLeft;
		private System.Windows.Forms.CheckBox cbMinimiseToTray;
		private System.Windows.Forms.GroupBox groupBoxGeneralOptions;
		private System.Windows.Forms.CheckBox cbAutoNotify;
		private System.Windows.Forms.LinkLabel linkLabel1;
		private System.Windows.Forms.CheckBox cbDetectNewLogFiles;
		private System.Windows.Forms.CheckBox cbCheckForNewVersion;
		private System.Windows.Forms.CheckBox cbRememberWindowLayout;
		private System.Windows.Forms.CheckBox cbRememberColumnWidths;
		private System.Windows.Forms.GroupBox groupBoxProxy;
		private System.Windows.Forms.CheckBox cbUseWebProxy;
		private System.Windows.Forms.CheckBox cbUseBrowserProxySettings;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox tbProxyHostName;
		private System.Windows.Forms.TextBox tbProxyPortNum;
		private System.Windows.Forms.TextBox tbProxyUsername;
		private System.Windows.Forms.TextBox tbProxyPassword;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.CheckBox cbRequiresAuthentication;
		private System.Windows.Forms.Button btnTestProxy;
		private System.Windows.Forms.Label lblProxyUsernameError;
		private System.Windows.Forms.Label lblProxyPasswordError;
		private System.Windows.Forms.Label lblInvalidHostName;
		private System.Windows.Forms.Label lblInvalidPortNum;
		private System.ComponentModel.IContainer components;

		public Options()
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
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Options));
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOk = new System.Windows.Forms.Button();
			this.panelLeft = new System.Windows.Forms.Panel();
			this.treeViewOptionsNav = new System.Windows.Forms.TreeView();
			this.groupBoxGameLogDirs = new System.Windows.Forms.GroupBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.btnDelete = new System.Windows.Forms.Button();
			this.btnAdd = new System.Windows.Forms.Button();
			this.lvPaths = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
			this.groupBoxGeneralOptions = new System.Windows.Forms.GroupBox();
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this.cbMinimiseToTray = new System.Windows.Forms.CheckBox();
			this.cbDetectNewLogFiles = new System.Windows.Forms.CheckBox();
			this.cbAutoNotify = new System.Windows.Forms.CheckBox();
			this.cbCheckForNewVersion = new System.Windows.Forms.CheckBox();
			this.cbRememberWindowLayout = new System.Windows.Forms.CheckBox();
			this.cbRememberColumnWidths = new System.Windows.Forms.CheckBox();
			this.groupBoxProxy = new System.Windows.Forms.GroupBox();
			this.lblProxyUsernameError = new System.Windows.Forms.Label();
			this.lblInvalidPortNum = new System.Windows.Forms.Label();
			this.cbRequiresAuthentication = new System.Windows.Forms.CheckBox();
			this.tbProxyHostName = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.cbUseWebProxy = new System.Windows.Forms.CheckBox();
			this.cbUseBrowserProxySettings = new System.Windows.Forms.CheckBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.tbProxyPortNum = new System.Windows.Forms.TextBox();
			this.tbProxyUsername = new System.Windows.Forms.TextBox();
			this.tbProxyPassword = new System.Windows.Forms.TextBox();
			this.lblProxyPasswordError = new System.Windows.Forms.Label();
			this.lblInvalidHostName = new System.Windows.Forms.Label();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.btnTestProxy = new System.Windows.Forms.Button();
			this.panelLeft.SuspendLayout();
			this.groupBoxGameLogDirs.SuspendLayout();
			this.groupBoxGeneralOptions.SuspendLayout();
			this.groupBoxProxy.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnCancel.Location = new System.Drawing.Point(488, 256);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.TabIndex = 8;
			this.btnCancel.Text = "Cancel";
			// 
			// btnOk
			// 
			this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnOk.Location = new System.Drawing.Point(404, 256);
			this.btnOk.Name = "btnOk";
			this.btnOk.TabIndex = 7;
			this.btnOk.Text = "OK";
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// panelLeft
			// 
			this.panelLeft.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left)));
			this.panelLeft.Controls.Add(this.treeViewOptionsNav);
			this.panelLeft.Location = new System.Drawing.Point(8, 8);
			this.panelLeft.Name = "panelLeft";
			this.panelLeft.Size = new System.Drawing.Size(128, 272);
			this.panelLeft.TabIndex = 2;
			// 
			// treeViewOptionsNav
			// 
			this.treeViewOptionsNav.ImageIndex = -1;
			this.treeViewOptionsNav.Indent = 16;
			this.treeViewOptionsNav.Location = new System.Drawing.Point(0, 0);
			this.treeViewOptionsNav.Name = "treeViewOptionsNav";
			this.treeViewOptionsNav.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
																						   new System.Windows.Forms.TreeNode("Application", new System.Windows.Forms.TreeNode[] {
																																													new System.Windows.Forms.TreeNode("General"),
																																													new System.Windows.Forms.TreeNode("Web Proxy")}),
																						   new System.Windows.Forms.TreeNode("Log Files", new System.Windows.Forms.TreeNode[] {
																																												  new System.Windows.Forms.TreeNode("Game Logs")})});
			this.treeViewOptionsNav.Scrollable = false;
			this.treeViewOptionsNav.SelectedImageIndex = -1;
			this.treeViewOptionsNav.ShowLines = false;
			this.treeViewOptionsNav.Size = new System.Drawing.Size(128, 272);
			this.treeViewOptionsNav.TabIndex = 3;
			this.treeViewOptionsNav.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeViewOptionsNav_BeforeSelect);
			// 
			// groupBoxGameLogDirs
			// 
			this.groupBoxGameLogDirs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBoxGameLogDirs.Controls.Add(this.label2);
			this.groupBoxGameLogDirs.Controls.Add(this.label1);
			this.groupBoxGameLogDirs.Controls.Add(this.btnDelete);
			this.groupBoxGameLogDirs.Controls.Add(this.btnAdd);
			this.groupBoxGameLogDirs.Controls.Add(this.lvPaths);
			this.groupBoxGameLogDirs.Location = new System.Drawing.Point(144, 8);
			this.groupBoxGameLogDirs.Name = "groupBoxGameLogDirs";
			this.groupBoxGameLogDirs.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.groupBoxGameLogDirs.Size = new System.Drawing.Size(424, 240);
			this.groupBoxGameLogDirs.TabIndex = 3;
			this.groupBoxGameLogDirs.TabStop = false;
			this.groupBoxGameLogDirs.Text = "Gamelog Directories";
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label2.Location = new System.Drawing.Point(8, 73);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(400, 23);
			this.label2.TabIndex = 5;
			this.label2.Text = "Select the Install_Dir\\CCP\\EVE\\Capture\\Gamelogs directory";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.label1.Location = new System.Drawing.Point(8, 21);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(400, 48);
			this.label1.TabIndex = 4;
			this.label1.Text = "Use this dialog to add additional Gamelog directories i.e. if you have more than " +
				"one copy of EVE installed or you have moved EVE from it\'s original installation " +
				"directory.";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnDelete
			// 
			this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnDelete.Enabled = false;
			this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnDelete.Location = new System.Drawing.Point(336, 136);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.TabIndex = 2;
			this.btnDelete.Text = "Delete";
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			// 
			// btnAdd
			// 
			this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnAdd.Location = new System.Drawing.Point(336, 104);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.TabIndex = 1;
			this.btnAdd.Text = "Add";
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			// 
			// lvPaths
			// 
			this.lvPaths.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lvPaths.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																					  this.columnHeader1,
																					  this.columnHeader2});
			this.lvPaths.FullRowSelect = true;
			this.lvPaths.HideSelection = false;
			this.lvPaths.Location = new System.Drawing.Point(8, 104);
			this.lvPaths.MultiSelect = false;
			this.lvPaths.Name = "lvPaths";
			this.lvPaths.Size = new System.Drawing.Size(320, 128);
			this.lvPaths.TabIndex = 0;
			this.lvPaths.View = System.Windows.Forms.View.Details;
			this.lvPaths.SelectedIndexChanged += new System.EventHandler(this.lvPaths_SelectedIndexChanged);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Alias";
			this.columnHeader1.Width = 57;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Path";
			this.columnHeader2.Width = 251;
			// 
			// folderBrowserDialog1
			// 
			this.folderBrowserDialog1.ShowNewFolderButton = false;
			// 
			// groupBoxGeneralOptions
			// 
			this.groupBoxGeneralOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBoxGeneralOptions.Controls.Add(this.linkLabel1);
			this.groupBoxGeneralOptions.Controls.Add(this.cbMinimiseToTray);
			this.groupBoxGeneralOptions.Controls.Add(this.cbDetectNewLogFiles);
			this.groupBoxGeneralOptions.Controls.Add(this.cbAutoNotify);
			this.groupBoxGeneralOptions.Controls.Add(this.cbCheckForNewVersion);
			this.groupBoxGeneralOptions.Controls.Add(this.cbRememberWindowLayout);
			this.groupBoxGeneralOptions.Controls.Add(this.cbRememberColumnWidths);
			this.groupBoxGeneralOptions.Location = new System.Drawing.Point(144, 8);
			this.groupBoxGeneralOptions.Name = "groupBoxGeneralOptions";
			this.groupBoxGeneralOptions.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.groupBoxGeneralOptions.Size = new System.Drawing.Size(424, 240);
			this.groupBoxGeneralOptions.TabIndex = 6;
			this.groupBoxGeneralOptions.TabStop = false;
			this.groupBoxGeneralOptions.Text = "General Options";
			// 
			// linkLabel1
			// 
			this.linkLabel1.Location = new System.Drawing.Point(344, 96);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new System.Drawing.Size(56, 23);
			this.linkLabel1.TabIndex = 1;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "Show me";
			this.linkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
			// 
			// cbMinimiseToTray
			// 
			this.cbMinimiseToTray.Location = new System.Drawing.Point(24, 32);
			this.cbMinimiseToTray.Name = "cbMinimiseToTray";
			this.cbMinimiseToTray.Size = new System.Drawing.Size(360, 24);
			this.cbMinimiseToTray.TabIndex = 0;
			this.cbMinimiseToTray.Text = "Use tray icon when main window is minimised";
			// 
			// cbDetectNewLogFiles
			// 
			this.cbDetectNewLogFiles.Location = new System.Drawing.Point(24, 64);
			this.cbDetectNewLogFiles.Name = "cbDetectNewLogFiles";
			this.cbDetectNewLogFiles.Size = new System.Drawing.Size(360, 24);
			this.cbDetectNewLogFiles.TabIndex = 0;
			this.cbDetectNewLogFiles.Text = "Automatically detect new log files";
			// 
			// cbAutoNotify
			// 
			this.cbAutoNotify.Location = new System.Drawing.Point(24, 96);
			this.cbAutoNotify.Name = "cbAutoNotify";
			this.cbAutoNotify.Size = new System.Drawing.Size(320, 24);
			this.cbAutoNotify.TabIndex = 0;
			this.cbAutoNotify.Text = "Display new log file notification when new log files detected";
			// 
			// cbCheckForNewVersion
			// 
			this.cbCheckForNewVersion.Location = new System.Drawing.Point(24, 128);
			this.cbCheckForNewVersion.Name = "cbCheckForNewVersion";
			this.cbCheckForNewVersion.Size = new System.Drawing.Size(320, 24);
			this.cbCheckForNewVersion.TabIndex = 0;
			this.cbCheckForNewVersion.Text = "Check for new version when application starts";
			// 
			// cbRememberWindowLayout
			// 
			this.cbRememberWindowLayout.Location = new System.Drawing.Point(24, 160);
			this.cbRememberWindowLayout.Name = "cbRememberWindowLayout";
			this.cbRememberWindowLayout.Size = new System.Drawing.Size(320, 24);
			this.cbRememberWindowLayout.TabIndex = 0;
			this.cbRememberWindowLayout.Text = "Remember and restore window layouts";
			// 
			// cbRememberColumnWidths
			// 
			this.cbRememberColumnWidths.Location = new System.Drawing.Point(24, 192);
			this.cbRememberColumnWidths.Name = "cbRememberColumnWidths";
			this.cbRememberColumnWidths.Size = new System.Drawing.Size(320, 24);
			this.cbRememberColumnWidths.TabIndex = 0;
			this.cbRememberColumnWidths.Text = "Remember and restore column widths of list views";
			// 
			// groupBoxProxy
			// 
			this.groupBoxProxy.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBoxProxy.Controls.Add(this.lblProxyUsernameError);
			this.groupBoxProxy.Controls.Add(this.lblInvalidPortNum);
			this.groupBoxProxy.Controls.Add(this.cbRequiresAuthentication);
			this.groupBoxProxy.Controls.Add(this.tbProxyHostName);
			this.groupBoxProxy.Controls.Add(this.label5);
			this.groupBoxProxy.Controls.Add(this.label3);
			this.groupBoxProxy.Controls.Add(this.cbUseWebProxy);
			this.groupBoxProxy.Controls.Add(this.cbUseBrowserProxySettings);
			this.groupBoxProxy.Controls.Add(this.label4);
			this.groupBoxProxy.Controls.Add(this.label6);
			this.groupBoxProxy.Controls.Add(this.tbProxyPortNum);
			this.groupBoxProxy.Controls.Add(this.tbProxyUsername);
			this.groupBoxProxy.Controls.Add(this.tbProxyPassword);
			this.groupBoxProxy.Controls.Add(this.lblProxyPasswordError);
			this.groupBoxProxy.Controls.Add(this.lblInvalidHostName);
			this.groupBoxProxy.Location = new System.Drawing.Point(144, 8);
			this.groupBoxProxy.Name = "groupBoxProxy";
			this.groupBoxProxy.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.groupBoxProxy.Size = new System.Drawing.Size(424, 240);
			this.groupBoxProxy.TabIndex = 7;
			this.groupBoxProxy.TabStop = false;
			this.groupBoxProxy.Text = "Web Proxy";
			// 
			// lblProxyUsernameError
			// 
			this.lblProxyUsernameError.ForeColor = System.Drawing.Color.Red;
			this.lblProxyUsernameError.Location = new System.Drawing.Point(272, 176);
			this.lblProxyUsernameError.Name = "lblProxyUsernameError";
			this.lblProxyUsernameError.TabIndex = 7;
			this.lblProxyUsernameError.Text = "Required";
			this.lblProxyUsernameError.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.lblProxyUsernameError.Visible = false;
			// 
			// lblInvalidPortNum
			// 
			this.lblInvalidPortNum.ForeColor = System.Drawing.Color.Red;
			this.lblInvalidPortNum.Location = new System.Drawing.Point(192, 108);
			this.lblInvalidPortNum.Name = "lblInvalidPortNum";
			this.lblInvalidPortNum.Size = new System.Drawing.Size(216, 23);
			this.lblInvalidPortNum.TabIndex = 6;
			this.lblInvalidPortNum.Text = "Integers only e.g. 8080";
			this.lblInvalidPortNum.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.lblInvalidPortNum.Visible = false;
			// 
			// cbRequiresAuthentication
			// 
			this.cbRequiresAuthentication.Location = new System.Drawing.Point(16, 149);
			this.cbRequiresAuthentication.Name = "cbRequiresAuthentication";
			this.cbRequiresAuthentication.Size = new System.Drawing.Size(184, 24);
			this.cbRequiresAuthentication.TabIndex = 4;
			this.cbRequiresAuthentication.Text = "Proxy Server Authentication";
			this.toolTip1.SetToolTip(this.cbRequiresAuthentication, "Enable this option if your proxy server requires a username and password");
			this.cbRequiresAuthentication.CheckedChanged += new System.EventHandler(this.cbRequiresAuthentication_CheckedChanged);
			// 
			// tbProxyHostName
			// 
			this.tbProxyHostName.Location = new System.Drawing.Point(90, 84);
			this.tbProxyHostName.Name = "tbProxyHostName";
			this.tbProxyHostName.Size = new System.Drawing.Size(254, 20);
			this.tbProxyHostName.TabIndex = 2;
			this.tbProxyHostName.Text = "";
			this.toolTip1.SetToolTip(this.tbProxyHostName, "Enter the host name of your proxy e.g. http://myproxyserver/");
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(16, 176);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(88, 23);
			this.label5.TabIndex = 2;
			this.label5.Text = "Proxy Username";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(16, 83);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(72, 23);
			this.label3.TabIndex = 1;
			this.label3.Text = "Host Name";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// cbUseWebProxy
			// 
			this.cbUseWebProxy.Location = new System.Drawing.Point(16, 21);
			this.cbUseWebProxy.Name = "cbUseWebProxy";
			this.cbUseWebProxy.Size = new System.Drawing.Size(120, 24);
			this.cbUseWebProxy.TabIndex = 0;
			this.cbUseWebProxy.Text = "Use Web Proxy";
			this.toolTip1.SetToolTip(this.cbUseWebProxy, "If you use a proxy to connect to the web, enable this option.");
			this.cbUseWebProxy.CheckStateChanged += new System.EventHandler(this.cbUseWebProxy_CheckStateChanged);
			// 
			// cbUseBrowserProxySettings
			// 
			this.cbUseBrowserProxySettings.Location = new System.Drawing.Point(16, 57);
			this.cbUseBrowserProxySettings.Name = "cbUseBrowserProxySettings";
			this.cbUseBrowserProxySettings.Size = new System.Drawing.Size(208, 24);
			this.cbUseBrowserProxySettings.TabIndex = 1;
			this.cbUseBrowserProxySettings.Text = "Use Web Browsers Proxy Settings";
			this.toolTip1.SetToolTip(this.cbUseBrowserProxySettings, "Enable this option if you have configured your web browser to use a proxy");
			this.cbUseBrowserProxySettings.CheckedChanged += new System.EventHandler(this.cbUseBrowserProxySettings_CheckedChanged);
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(16, 108);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(72, 23);
			this.label4.TabIndex = 1;
			this.label4.Text = "Port Number";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(16, 201);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(88, 23);
			this.label6.TabIndex = 2;
			this.label6.Text = "Proxy Password";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// tbProxyPortNum
			// 
			this.tbProxyPortNum.Location = new System.Drawing.Point(90, 109);
			this.tbProxyPortNum.Name = "tbProxyPortNum";
			this.tbProxyPortNum.Size = new System.Drawing.Size(86, 20);
			this.tbProxyPortNum.TabIndex = 3;
			this.tbProxyPortNum.Text = "";
			this.toolTip1.SetToolTip(this.tbProxyPortNum, "Enter the port number that you connect to your proxy server on e.g. 8080");
			// 
			// tbProxyUsername
			// 
			this.tbProxyUsername.Location = new System.Drawing.Point(112, 177);
			this.tbProxyUsername.Name = "tbProxyUsername";
			this.tbProxyUsername.Size = new System.Drawing.Size(152, 20);
			this.tbProxyUsername.TabIndex = 5;
			this.tbProxyUsername.Text = "";
			this.toolTip1.SetToolTip(this.tbProxyUsername, "Enter your proxy server user name");
			// 
			// tbProxyPassword
			// 
			this.tbProxyPassword.Location = new System.Drawing.Point(112, 202);
			this.tbProxyPassword.Name = "tbProxyPassword";
			this.tbProxyPassword.PasswordChar = '*';
			this.tbProxyPassword.Size = new System.Drawing.Size(152, 20);
			this.tbProxyPassword.TabIndex = 6;
			this.tbProxyPassword.Text = "";
			this.toolTip1.SetToolTip(this.tbProxyPassword, "Enter your proxy server password");
			// 
			// lblProxyPasswordError
			// 
			this.lblProxyPasswordError.ForeColor = System.Drawing.Color.Red;
			this.lblProxyPasswordError.Location = new System.Drawing.Point(272, 201);
			this.lblProxyPasswordError.Name = "lblProxyPasswordError";
			this.lblProxyPasswordError.TabIndex = 7;
			this.lblProxyPasswordError.Text = "Required";
			this.lblProxyPasswordError.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.lblProxyPasswordError.Visible = false;
			// 
			// lblInvalidHostName
			// 
			this.lblInvalidHostName.ForeColor = System.Drawing.Color.Red;
			this.lblInvalidHostName.Location = new System.Drawing.Point(352, 83);
			this.lblInvalidHostName.Name = "lblInvalidHostName";
			this.lblInvalidHostName.Size = new System.Drawing.Size(64, 23);
			this.lblInvalidHostName.TabIndex = 6;
			this.lblInvalidHostName.Text = "Required";
			this.lblInvalidHostName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.lblInvalidHostName.Visible = false;
			// 
			// btnTestProxy
			// 
			this.btnTestProxy.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnTestProxy.Location = new System.Drawing.Point(320, 256);
			this.btnTestProxy.Name = "btnTestProxy";
			this.btnTestProxy.TabIndex = 6;
			this.btnTestProxy.Text = "Test";
			this.toolTip1.SetToolTip(this.btnTestProxy, "Click here to test your proxy server configuration options.");
			this.btnTestProxy.Visible = false;
			// 
			// Options
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(576, 286);
			this.Controls.Add(this.btnTestProxy);
			this.Controls.Add(this.groupBoxProxy);
			this.Controls.Add(this.groupBoxGeneralOptions);
			this.Controls.Add(this.groupBoxGameLogDirs);
			this.Controls.Add(this.panelLeft);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.btnCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(584, 320);
			this.Name = "Options";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Options";
			this.Load += new System.EventHandler(this.Options_Load);
			this.panelLeft.ResumeLayout(false);
			this.groupBoxGameLogDirs.ResumeLayout(false);
			this.groupBoxGeneralOptions.ResumeLayout(false);
			this.groupBoxProxy.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void btnAdd_Click(object sender, System.EventArgs e)
		{

			DialogResult folderResult = folderBrowserDialog1.ShowDialog();

			if ( folderResult != DialogResult.OK )
				return;

			AddFolder af = new AddFolder();
			af.tbAlias.Focus();
			DialogResult r = af.ShowDialog();

			if ( r != DialogResult.OK )
				return;

			Debug.WriteLine("Folder = " + folderBrowserDialog1.SelectedPath);
			Debug.WriteLine("Alias  = " + af.tbAlias.Text);

			ListViewItem l = new ListViewItem(new string[] { af.tbAlias.Text, folderBrowserDialog1.SelectedPath });
			lvPaths.Items.Add(l);
		}

		private void lvPaths_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if ( lvPaths.SelectedItems.Count == 0 )
				btnDelete.Enabled = false;
			else
				btnDelete.Enabled = true;
		}

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			lvPaths.SelectedItems[0].Remove();
		}

		private void PopulatePathDialog()
		{
			lvPaths.Items.Clear();

			foreach ( ConfigGameLogDir d in MyConfig.GameLogDirs )
			{
				ListViewItem l = new ListViewItem(new string[] { d.Alias, d.PathName });
				lvPaths.Items.Add(l);
			}
		}

		private void PrepareCategoryTree()
		{
			treeViewOptionsNav.ExpandAll();

			foreach ( TreeNode t in treeViewOptionsNav.Nodes )
			{
				if ( t.Nodes != null )
				{
					foreach (TreeNode innerNode in t.Nodes )
					{
						Debug.WriteLine("testing node '" + t.Text + "'");

						if ( innerNode.Text.Trim() == "General" )
							treeViewOptionsNav.SelectedNode = innerNode;
					}
				}
			}
		}

		private void Options_Load(object sender, System.EventArgs e)
		{
			PrepareCategoryTree();
			if ( MyConfig != null )
			{
				PopulatePathDialog();
				InitProxySettings();
			}

			cbMinimiseToTray.Checked		= MyConfig.MinimiseToSystemTray;
			cbDetectNewLogFiles.Checked		= MyConfig.DetectNewLogFiles;
			cbAutoNotify.Checked			= MyConfig.NotifyNewLogFiles;
			cbCheckForNewVersion.Checked	= MyConfig.CheckForNewVersion;
			cbRememberWindowLayout.Checked	= MyConfig.RememberWindowLayout;
			cbRememberColumnWidths.Checked	= MyConfig.RememberColumnWidths;
		}

		private void PopulateProxyConfig()
		{
			cbUseWebProxy.Checked				= MyConfig.UseProxy;
			cbUseWebProxy.Checked				= MyConfig.UseBrowserSettings;
			tbProxyHostName.Text				= MyConfig.ProxyHost;
			tbProxyPortNum.Text					= MyConfig.ProxyPort.ToString();
			cbRequiresAuthentication.Checked	= MyConfig.ProxyRequiresAuthentication;
			tbProxyUsername.Text				= MyConfig.ProxyUserName;
			tbProxyPassword.Text				= MyConfig.ProxyPassword;
		}

		private void GetProxySettingsFromUI()
		{
			MyConfig.UseProxy = cbUseWebProxy.Checked;
			MyConfig.UseBrowserSettings = cbUseWebProxy.Checked;
			MyConfig.ProxyHost = tbProxyHostName.Text;
			MyConfig.ProxyPort = int.Parse(tbProxyPortNum.Text);
			MyConfig.ProxyRequiresAuthentication = cbRequiresAuthentication.Checked;
			MyConfig.ProxyUserName = tbProxyUsername.Text;
			MyConfig.ProxyPassword = tbProxyPassword.Text;
		}

		private void InitProxySettings()
		{
			PopulateProxyConfig();

			if ( !MyConfig.UseProxy )
				DisableProxyConfigControls();
			else
				EnableProxyConfigControls();
		}

		private bool ValidInt(string val)
		{
			try
			{
				int i = int.Parse(val);
				return true;
			}
			catch
			{
				return false;
			}
		}
		
		private bool ValidateProxySettings()
		{
			bool valid = true;

			if ( cbUseWebProxy.Checked )
			{
				if ( !cbUseBrowserProxySettings.Checked )
				{
					if ( tbProxyHostName.Text.Length == 0 )
					{
						lblInvalidHostName.Visible = true;
						valid = false;
					}
					else
						lblInvalidHostName.Visible = false;

					if ( !ValidInt(tbProxyPortNum.Text) )
					{
						lblInvalidPortNum.Visible = true;
						valid = false;
					}
					else
						lblInvalidPortNum.Visible = false;
				}
				else
				{
					lblInvalidHostName.Visible = false;
					lblInvalidPortNum.Visible = false;
				}

				if ( cbRequiresAuthentication.Checked )
				{
					if ( tbProxyUsername.Text.Length == 0 )
					{
						groupBoxProxy.BringToFront();
						lblProxyUsernameError.Visible = true;
						valid = false;
					}
					else
						lblProxyUsernameError.Visible = false;

					if ( tbProxyPassword.Text.Length == 0 )
					{
						groupBoxProxy.BringToFront();
						lblProxyPasswordError.Visible = true;
						valid = false;
					}
					else
						lblProxyPasswordError.Visible = false;
				}
				else
				{
					lblProxyPasswordError.Visible = false;
					lblProxyUsernameError.Visible = false;
				}
			}

			return valid;
		}

		private void btnOk_Click(object sender, System.EventArgs e)
		{
			UpdatePathConfig();

			MyConfig.MinimiseToSystemTray	= cbMinimiseToTray.Checked;
			MyConfig.DetectNewLogFiles		= cbDetectNewLogFiles.Checked;
			MyConfig.NotifyNewLogFiles		= cbAutoNotify.Checked;
			MyConfig.CheckForNewVersion		= cbCheckForNewVersion.Checked;
			MyConfig.RememberWindowLayout	= cbRememberWindowLayout.Checked;
			MyConfig.RememberColumnWidths	= cbRememberColumnWidths.Checked;

			if ( ValidateProxySettings() )
			{
				GetProxySettingsFromUI();
				this.DialogResult = DialogResult.OK;
				this.Close();
			}
			else
				ShowWebProxyOptions();
		}

		private void UpdatePathConfig()
		{
			if ( MyConfig == null )
				MyConfig = new UserConfig();

			if ( MyConfig.GameLogDirs == null )
				MyConfig.GameLogDirs = new ConfigGameLogDirCollection();
			else
				MyConfig.GameLogDirs.Clear();

			foreach ( ListViewItem l in lvPaths.Items )
			{
				ConfigGameLogDir g = new ConfigGameLogDir(l.SubItems[1].Text, l.SubItems[0].Text);
				MyConfig.GameLogDirs.Add(g);
			}
		}

		private void PrepareProxyDialog()
		{
			if ( MyConfig == null )
				MyConfig = new UserConfig();

			if ( MyConfig.GameLogDirs == null )
				MyConfig.GameLogDirs = new ConfigGameLogDirCollection();

			cbUseWebProxy.Checked = MyConfig.UseProxy;
			
		}

		private void ShowGeneralOptions()
		{
			groupBoxGameLogDirs.Visible = false;
			groupBoxProxy.Visible = false;
			groupBoxGeneralOptions.Visible = true;
			groupBoxGeneralOptions.BringToFront();
		}

		private void ShowLogFileOptions()
		{
			groupBoxGeneralOptions.Visible = false;
			groupBoxProxy.Visible = false;
			groupBoxGameLogDirs.Visible = true;

			groupBoxGameLogDirs.BringToFront();
		}

		private void ShowWebProxyOptions()
		{
			groupBoxGeneralOptions.Visible = false;
			groupBoxGameLogDirs.Visible = false;
			groupBoxProxy.Visible = true;
			groupBoxProxy.BringToFront();
		}

		private void treeViewOptionsNav_BeforeSelect(object sender, System.Windows.Forms.TreeViewCancelEventArgs e)
		{
			switch ( e.Node.Text )
			{
				case "Application":
					Debug.WriteLine("Appliction clicked");
					ShowGeneralOptions();
					break;
				case "General":
					Debug.WriteLine("General clicked");
					ShowGeneralOptions();
					break;
				case "Log Files":
					Debug.WriteLine("Log Files Clicked");
					ShowLogFileOptions();
					break;
				case "Game Logs":
					Debug.WriteLine("Game logs clicked");
					ShowLogFileOptions();
					break;

				case "Web Proxy":
					Debug.WriteLine("Web Proxy clicked");
					ShowWebProxyOptions();
					// PrepareProxyDialog();
					break;
			}
		}

		private void linkLabel1_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			CombatLog.Panes.Notify n = new CombatLog.Panes.Notify();
			n.ActiveNotifyWindows = ActiveNotifyWindows;
			n.EVELocation = "Tash-Murkon Prime";
			n.Targets = "Domination General, Arch Angel Centurion";

			n.EVELocation = "Test Location #" + (ActiveNotifyWindows.Count + 1).ToString();
			n.Targets = "Test target list.. test target list... # " + (ActiveNotifyWindows.Count + 1).ToString();

			n.Show();

			ActiveNotifyWindows.Add(n);
		}

		private void cbUseWebProxy_CheckStateChanged(object sender, System.EventArgs e)
		{
			if ( cbUseWebProxy.Checked )
				EnableProxyConfigControls();
			else
				DisableProxyConfigControls();
		}

		private void EnableProxyConfigControls()
		{
			cbUseBrowserProxySettings.Enabled = true;

			if ( cbUseBrowserProxySettings.Checked )
				DisableProxyHostOptions();
			else
				EnableProxyHostOptions();

			cbRequiresAuthentication.Enabled = true;
			if ( cbRequiresAuthentication.Checked )
				EnableProxyAuthenticationOptions();
			else
				DisableProxyAuthenticationOptions();
		}

		private void DisableProxyConfigControls()
		{
			cbUseBrowserProxySettings.Enabled = false;
			DisableProxyHostOptions();
			cbRequiresAuthentication.Enabled = false;
			DisableProxyAuthenticationOptions();
		}

		private void DisableProxyHostOptions()
		{
			tbProxyHostName.Enabled = false;
			tbProxyPortNum.Enabled = false;
		}

		private void EnableProxyHostOptions()
		{
			tbProxyHostName.Enabled = true;
			tbProxyPortNum.Enabled = true;
		}

		private void DisableProxyAuthenticationOptions()
		{
			tbProxyUsername.Enabled = false;
			tbProxyPassword.Enabled = false;
		}

		private void EnableProxyAuthenticationOptions()
		{
			tbProxyUsername.Enabled = true;
			tbProxyPassword.Enabled = true;
		}

		private void cbUseBrowserProxySettings_CheckedChanged(object sender, System.EventArgs e)
		{
			if ( cbUseBrowserProxySettings.Checked )
				DisableProxyHostOptions();
			else
				EnableProxyHostOptions();
		}

		private void cbRequiresAuthentication_CheckedChanged(object sender, System.EventArgs e)
		{
			if ( cbRequiresAuthentication.Checked )
				EnableProxyAuthenticationOptions();
			else
				DisableProxyAuthenticationOptions();
		}

	}
}
