using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Diagnostics;
using System.Xml.Serialization;
using CombatLog.VersionManager;
using CombatLog.ReleaseManager;

namespace ReleaseManager
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private TD.SandDock.SandDockManager sandDockManager1;
		private TD.SandDock.DockContainer leftSandDock;
		private TD.SandDock.DockContainer rightSandDock;
		private TD.SandDock.DockContainer bottomSandDock;
		private TD.SandDock.DockContainer topSandDock;
		private TD.SandDock.DockControl dockControl1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem3;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private string VersionHistoryFileName = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\EVECombatLog\VersionInfo.xml";
		private System.Windows.Forms.ListView listViewVersions;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.MenuItem menuItem6;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
		private System.Windows.Forms.MenuItem menuItem9;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private VersionInfoCollection Versions = new VersionInfoCollection();
		private System.Windows.Forms.MenuItem menuItemSave;
		private System.Windows.Forms.MenuItem menuItemOpen;
		private System.Windows.Forms.MenuItem menuItemSaveAs;

		private bool DataChanged = false;

		public Form1()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Form1));
			this.sandDockManager1 = new TD.SandDock.SandDockManager();
			this.leftSandDock = new TD.SandDock.DockContainer();
			this.dockControl1 = new TD.SandDock.DockControl();
			this.listViewVersions = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.rightSandDock = new TD.SandDock.DockContainer();
			this.bottomSandDock = new TD.SandDock.DockContainer();
			this.topSandDock = new TD.SandDock.DockContainer();
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItemOpen = new System.Windows.Forms.MenuItem();
			this.menuItemSave = new System.Windows.Forms.MenuItem();
			this.menuItemSaveAs = new System.Windows.Forms.MenuItem();
			this.menuItem9 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.menuItem5 = new System.Windows.Forms.MenuItem();
			this.menuItem6 = new System.Windows.Forms.MenuItem();
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.leftSandDock.SuspendLayout();
			this.dockControl1.SuspendLayout();
			this.SuspendLayout();
			// 
			// sandDockManager1
			// 
			this.sandDockManager1.OwnerForm = this;
			// 
			// leftSandDock
			// 
			this.leftSandDock.Controls.Add(this.dockControl1);
			this.leftSandDock.Dock = System.Windows.Forms.DockStyle.Left;
			this.leftSandDock.Guid = new System.Guid("44196b20-ad65-4265-92c0-6b667c2ed504");
			this.leftSandDock.LayoutSystem = new TD.SandDock.SplitLayoutSystem(250, 400, System.Windows.Forms.Orientation.Horizontal, new TD.SandDock.LayoutSystemBase[] {
																																											 new TD.SandDock.ControlLayoutSystem(244, 465, new TD.SandDock.DockControl[] {
																																																															 this.dockControl1}, this.dockControl1)});
			this.leftSandDock.Location = new System.Drawing.Point(0, 0);
			this.leftSandDock.Manager = this.sandDockManager1;
			this.leftSandDock.Name = "leftSandDock";
			this.leftSandDock.Size = new System.Drawing.Size(248, 465);
			this.leftSandDock.TabIndex = 0;
			// 
			// dockControl1
			// 
			this.dockControl1.Controls.Add(this.listViewVersions);
			this.dockControl1.Guid = new System.Guid("ea0fd747-6103-4576-9e15-289f231002ad");
			this.dockControl1.Location = new System.Drawing.Point(0, 18);
			this.dockControl1.Name = "dockControl1";
			this.dockControl1.Size = new System.Drawing.Size(244, 424);
			this.dockControl1.TabIndex = 0;
			this.dockControl1.Text = "Versions";
			// 
			// listViewVersions
			// 
			this.listViewVersions.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																							   this.columnHeader1,
																							   this.columnHeader2,
																							   this.columnHeader3});
			this.listViewVersions.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listViewVersions.FullRowSelect = true;
			this.listViewVersions.Location = new System.Drawing.Point(0, 0);
			this.listViewVersions.Name = "listViewVersions";
			this.listViewVersions.Size = new System.Drawing.Size(244, 424);
			this.listViewVersions.TabIndex = 4;
			this.listViewVersions.View = System.Windows.Forms.View.Details;
			this.listViewVersions.ItemActivate += new System.EventHandler(this.listViewVersions_ItemActivate);
			this.listViewVersions.SelectedIndexChanged += new System.EventHandler(this.listViewVersions_SelectedIndexChanged);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Version";
			this.columnHeader1.Width = 69;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Date";
			this.columnHeader2.Width = 79;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Type";
			this.columnHeader3.Width = 87;
			// 
			// rightSandDock
			// 
			this.rightSandDock.Dock = System.Windows.Forms.DockStyle.Right;
			this.rightSandDock.Guid = new System.Guid("1645b352-28d6-4977-a716-0e8f45ff548b");
			this.rightSandDock.LayoutSystem = new TD.SandDock.SplitLayoutSystem(250, 400);
			this.rightSandDock.Location = new System.Drawing.Point(912, 0);
			this.rightSandDock.Manager = this.sandDockManager1;
			this.rightSandDock.Name = "rightSandDock";
			this.rightSandDock.Size = new System.Drawing.Size(0, 465);
			this.rightSandDock.TabIndex = 1;
			// 
			// bottomSandDock
			// 
			this.bottomSandDock.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.bottomSandDock.Guid = new System.Guid("e4f9b431-f5c8-4439-8795-e720bec47654");
			this.bottomSandDock.LayoutSystem = new TD.SandDock.SplitLayoutSystem(250, 400);
			this.bottomSandDock.Location = new System.Drawing.Point(0, 465);
			this.bottomSandDock.Manager = this.sandDockManager1;
			this.bottomSandDock.Name = "bottomSandDock";
			this.bottomSandDock.Size = new System.Drawing.Size(912, 0);
			this.bottomSandDock.TabIndex = 2;
			// 
			// topSandDock
			// 
			this.topSandDock.Dock = System.Windows.Forms.DockStyle.Top;
			this.topSandDock.Guid = new System.Guid("2cd1d860-77c1-482f-83b2-04f1c3ad1c3e");
			this.topSandDock.LayoutSystem = new TD.SandDock.SplitLayoutSystem(250, 400);
			this.topSandDock.Location = new System.Drawing.Point(0, 0);
			this.topSandDock.Manager = this.sandDockManager1;
			this.topSandDock.Name = "topSandDock";
			this.topSandDock.Size = new System.Drawing.Size(912, 0);
			this.topSandDock.TabIndex = 3;
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem1,
																					  this.menuItem4,
																					  this.menuItem6});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItemOpen,
																					  this.menuItemSave,
																					  this.menuItemSaveAs,
																					  this.menuItem9,
																					  this.menuItem3});
			this.menuItem1.Text = "File";
			// 
			// menuItemOpen
			// 
			this.menuItemOpen.Index = 0;
			this.menuItemOpen.Text = "Open...";
			this.menuItemOpen.Click += new System.EventHandler(this.menuItem8_Click);
			// 
			// menuItemSave
			// 
			this.menuItemSave.Index = 1;
			this.menuItemSave.Text = "Save";
			this.menuItemSave.Click += new System.EventHandler(this.menuItem2_Click);
			// 
			// menuItemSaveAs
			// 
			this.menuItemSaveAs.Index = 2;
			this.menuItemSaveAs.Text = "Save As...";
			this.menuItemSaveAs.Click += new System.EventHandler(this.menuItem7_Click);
			// 
			// menuItem9
			// 
			this.menuItem9.Index = 3;
			this.menuItem9.Text = "-";
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 4;
			this.menuItem3.Text = "Exit";
			// 
			// menuItem4
			// 
			this.menuItem4.Index = 1;
			this.menuItem4.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem5});
			this.menuItem4.Text = "Edit";
			// 
			// menuItem5
			// 
			this.menuItem5.Index = 0;
			this.menuItem5.Text = "Create version";
			this.menuItem5.Click += new System.EventHandler(this.menuItem5_Click);
			// 
			// menuItem6
			// 
			this.menuItem6.Index = 2;
			this.menuItem6.MdiList = true;
			this.menuItem6.Text = "Window";
			// 
			// saveFileDialog1
			// 
			this.saveFileDialog1.Filter = "XML|*.xml";
			this.saveFileDialog1.Title = "Export Version History File";
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.Filter = "XML|*.xml";
			this.openFileDialog1.Title = "Load Version History File";
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(912, 465);
			this.Controls.Add(this.leftSandDock);
			this.Controls.Add(this.rightSandDock);
			this.Controls.Add(this.bottomSandDock);
			this.Controls.Add(this.topSandDock);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.IsMdiContainer = true;
			this.Menu = this.mainMenu1;
			this.Name = "Form1";
			this.Text = "CLA Version Manager";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.Form1_Closing);
			this.Load += new System.EventHandler(this.Form1_Load);
			this.leftSandDock.ResumeLayout(false);
			this.dockControl1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.EnableVisualStyles();
			Application.DoEvents();
			Application.Run(new Form1());
		}

		private CombatLog.VersionManager.VersionInfoCollection LoadVersionHistory()
		{
			return LoadVersionHistory(VersionHistoryFileName);
		}

		private CombatLog.VersionManager.VersionInfoCollection LoadVersionHistory(string FileName)
		{
			VersionInfoCollection LoadVersions = new VersionInfoCollection();

			FileStream fs;
			try
			{
				fs = new FileStream(FileName, FileMode.Open);
			}
			catch (Exception e)
			{
				Debug.WriteLine("Problem opening version history file: " + e.ToString());
				return LoadVersions;
			}

			XmlSerializer xs;

			try
			{
				xs = new XmlSerializer(typeof(VersionInfoCollection));
			}
			catch (Exception e)
			{
				Debug.WriteLine("Problem creating deserializer for the version history file: " + e.ToString());
				return LoadVersions;
			}

			try
			{
				LoadVersions = (VersionInfoCollection)xs.Deserialize(fs);
			}
			catch (Exception e)
			{
				Debug.WriteLine("Problem deserializing the version history file: " + e.ToString());
			}

			fs.Close();

			return LoadVersions;
		}

		private void DrawVersionList()
		{
			listViewVersions.Items.Clear();

			foreach ( VersionInfo v in Versions )
			{
				ListViewItem l = new ListViewItem(new string[] { v.VersionString, v.ReleaseDate.ToShortDateString(), v.ReleaseType.ToString()});
				l.Tag = v;

				listViewVersions.Items.Add(l);
			}
		}

		private void Form1_Load(object sender, System.EventArgs e)
		{
			//Versions = LoadVersionHistory();
			DrawVersionList();
		}


		private void menuItem5_Click(object sender, System.EventArgs e)
		{
			VersionInfo v = new VersionInfo();
			v.VersionString = "New";
			v.ReleaseDate = DateTime.Now;
			v.VersionNumber = 0;
			v.DownloadUrl = "http://";
			v.ReleaseNotesUrl = "http://";

			Versions.Add(v);

			DrawVersionList();
		}

		private void OpenEditor(VersionInfo v)
		{
			foreach ( Form f in this.MdiChildren )
			{
				VersionInfo ver = (VersionInfo)f.Tag;

				if ( ver == v )
				{
					f.BringToFront();
					return;
				}
			}

			Edit EditForm = new Edit();
			EditForm.ThisVersion = v;
			EditForm.MdiParent = this;
			EditForm.Text = v.VersionString + " " + v.ReleaseType.ToString() + " " + v.ReleaseDate.ToShortDateString();
			EditForm.Tag = v;
			EditForm.Updated += new Edit.SaveUpdates(EditForm_Updated);
			EditForm.Show();
		}

		private void listViewVersions_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		}

		private void listViewVersions_ItemActivate(object sender, System.EventArgs e)
		{
			if ( listViewVersions.SelectedItems.Count != 1 )
				return;

			VersionInfo v = (VersionInfo)listViewVersions.SelectedItems[0].Tag;

			OpenEditor(v);
		}

		private void EditForm_Updated(object sender, VersionInfo versionInfo)
		{
			DataChanged = true;
			DrawVersionList();
		}

		private void menuItem2_Click(object sender, System.EventArgs e)
		{
			DoExport();
			DataChanged = false;
		}

		private void SaveVersionHistory(string FileName)
		{
			FileStream fs;
			try
			{
				fs = new FileStream(FileName, FileMode.Create);

				XmlSerializer xs = new XmlSerializer(typeof(VersionInfoCollection));

				xs.Serialize(fs, Versions);

				fs.Close();
			}
			catch (Exception e)
			{
				MessageBox.Show("There was a problem saving the version history file to " + FileName +". The error message reported by the system is: " + e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void DoExport()
		{
			SaveVersionHistory(VersionHistoryFileName);
			DataChanged = false;
		}

		private DialogResult DoExportAs()
		{
			DialogResult dr = saveFileDialog1.ShowDialog();

			if ( dr == DialogResult.OK )
			{
				Debug.WriteLine("Save file to :" + saveFileDialog1.FileName);
				SaveVersionHistory(saveFileDialog1.FileName);
			}

			return dr;
		}

		private void CloseOpenWindows()
		{
			foreach ( Form f in this.MdiChildren )
			{
				f.Close();
			}
		}

		private void LoadFile(string FileName)
		{
			VersionHistoryFileName = FileName;
			CloseOpenWindows();
			Versions = LoadVersionHistory(openFileDialog1.FileName);
			DrawVersionList();
			menuItemSave.Text = "Save " + Path.GetFileName(FileName);
		}

		private void menuItem8_Click(object sender, System.EventArgs e)
		{
			DialogResult dr = openFileDialog1.ShowDialog();

			if ( dr == DialogResult.OK )
			{
				LoadFile(openFileDialog1.FileName);
				DataChanged = false;
			}
		}

		private void Form1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if ( DataChanged )
			{
				DialogResult dr = MessageBox.Show("There are unsaved changes. Do you wish to save these changes?", "Warning", MessageBoxButtons.YesNoCancel);

				if ( dr == DialogResult.Cancel )
					e.Cancel = true;
				else if ( dr == DialogResult.Yes )
					SaveVersionHistory(VersionHistoryFileName);
				else if ( dr == DialogResult.No )
					return;
			}
		}

		private void menuItem7_Click(object sender, System.EventArgs e)
		{
			DoExportAs();
			DataChanged = false;
		}

	}
}
