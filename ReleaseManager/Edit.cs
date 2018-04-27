using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Diagnostics;
using CombatLog.VersionManager;

namespace CombatLog.ReleaseManager
{
	/// <summary>
	/// Summary description for Edit.
	/// </summary>
	public class Edit : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.DateTimePicker dateTimePickerReleaseDate;
		private System.Windows.Forms.TextBox tbDownloadURL;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.ComboBox cbReleaseType;
		private System.Windows.Forms.TextBox tbVersionStr;
		private System.Windows.Forms.TextBox tbReleaseNotesURL;

		public delegate void SaveUpdates(object sender, CombatLog.VersionManager.VersionInfo versionInfo);
		public event SaveUpdates Updated = null;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Button btnTestDownload;
		private System.Windows.Forms.Button btnTestReleaseNotes;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.TextBox tbIntegerVersion;
		private System.Windows.Forms.Label labelIntegerVersion;

		public VersionInfo ThisVersion;

		public Edit()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Edit));
			this.panel1 = new System.Windows.Forms.Panel();
			this.button4 = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.btnTestDownload = new System.Windows.Forms.Button();
			this.dateTimePickerReleaseDate = new System.Windows.Forms.DateTimePicker();
			this.tbDownloadURL = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.cbReleaseType = new System.Windows.Forms.ComboBox();
			this.tbVersionStr = new System.Windows.Forms.TextBox();
			this.tbReleaseNotesURL = new System.Windows.Forms.TextBox();
			this.btnTestReleaseNotes = new System.Windows.Forms.Button();
			this.labelIntegerVersion = new System.Windows.Forms.Label();
			this.tbIntegerVersion = new System.Windows.Forms.TextBox();
			this.panel1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.button4);
			this.panel1.Controls.Add(this.button1);
			this.panel1.Controls.Add(this.groupBox1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(536, 270);
			this.panel1.TabIndex = 5;
			// 
			// button4
			// 
			this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button4.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.button4.Location = new System.Drawing.Point(453, 240);
			this.button4.Name = "button4";
			this.button4.TabIndex = 10;
			this.button4.Text = "Cancel";
			this.button4.Click += new System.EventHandler(this.button4_Click);
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.button1.Location = new System.Drawing.Point(368, 240);
			this.button1.Name = "button1";
			this.button1.TabIndex = 9;
			this.button1.Text = "OK";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.btnTestDownload);
			this.groupBox1.Controls.Add(this.dateTimePickerReleaseDate);
			this.groupBox1.Controls.Add(this.tbDownloadURL);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.cbReleaseType);
			this.groupBox1.Controls.Add(this.tbVersionStr);
			this.groupBox1.Controls.Add(this.tbReleaseNotesURL);
			this.groupBox1.Controls.Add(this.btnTestReleaseNotes);
			this.groupBox1.Controls.Add(this.labelIntegerVersion);
			this.groupBox1.Controls.Add(this.tbIntegerVersion);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(8, 8);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(520, 224);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Edit";
			// 
			// btnTestDownload
			// 
			this.btnTestDownload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnTestDownload.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnTestDownload.Location = new System.Drawing.Point(424, 152);
			this.btnTestDownload.Name = "btnTestDownload";
			this.btnTestDownload.TabIndex = 6;
			this.btnTestDownload.Text = "Test";
			// 
			// dateTimePickerReleaseDate
			// 
			this.dateTimePickerReleaseDate.Location = new System.Drawing.Point(136, 89);
			this.dateTimePickerReleaseDate.Name = "dateTimePickerReleaseDate";
			this.dateTimePickerReleaseDate.TabIndex = 3;
			// 
			// tbDownloadURL
			// 
			this.tbDownloadURL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.tbDownloadURL.Location = new System.Drawing.Point(136, 153);
			this.tbDownloadURL.Name = "tbDownloadURL";
			this.tbDownloadURL.Size = new System.Drawing.Size(280, 20);
			this.tbDownloadURL.TabIndex = 5;
			this.tbDownloadURL.Text = "";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(112, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "Version";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(16, 88);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(112, 23);
			this.label2.TabIndex = 0;
			this.label2.Text = "Date";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(16, 120);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(112, 23);
			this.label3.TabIndex = 0;
			this.label3.Text = "Release Type";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(16, 152);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(112, 23);
			this.label4.TabIndex = 0;
			this.label4.Text = "Download URL";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(16, 184);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(112, 23);
			this.label5.TabIndex = 0;
			this.label5.Text = "Release Notes URL";
			// 
			// cbReleaseType
			// 
			this.cbReleaseType.Location = new System.Drawing.Point(136, 121);
			this.cbReleaseType.Name = "cbReleaseType";
			this.cbReleaseType.Size = new System.Drawing.Size(200, 21);
			this.cbReleaseType.TabIndex = 4;
			this.cbReleaseType.Text = "comboBox1";
			// 
			// tbVersionStr
			// 
			this.tbVersionStr.Location = new System.Drawing.Point(136, 25);
			this.tbVersionStr.Name = "tbVersionStr";
			this.tbVersionStr.Size = new System.Drawing.Size(200, 20);
			this.tbVersionStr.TabIndex = 1;
			this.tbVersionStr.Text = "";
			// 
			// tbReleaseNotesURL
			// 
			this.tbReleaseNotesURL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.tbReleaseNotesURL.Location = new System.Drawing.Point(136, 185);
			this.tbReleaseNotesURL.Name = "tbReleaseNotesURL";
			this.tbReleaseNotesURL.Size = new System.Drawing.Size(280, 20);
			this.tbReleaseNotesURL.TabIndex = 7;
			this.tbReleaseNotesURL.Text = "";
			// 
			// btnTestReleaseNotes
			// 
			this.btnTestReleaseNotes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnTestReleaseNotes.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnTestReleaseNotes.Location = new System.Drawing.Point(424, 184);
			this.btnTestReleaseNotes.Name = "btnTestReleaseNotes";
			this.btnTestReleaseNotes.TabIndex = 8;
			this.btnTestReleaseNotes.Text = "Test";
			// 
			// labelIntegerVersion
			// 
			this.labelIntegerVersion.Location = new System.Drawing.Point(16, 56);
			this.labelIntegerVersion.Name = "labelIntegerVersion";
			this.labelIntegerVersion.Size = new System.Drawing.Size(112, 23);
			this.labelIntegerVersion.TabIndex = 0;
			this.labelIntegerVersion.Text = "Integer Version No.";
			// 
			// tbIntegerVersion
			// 
			this.tbIntegerVersion.Location = new System.Drawing.Point(136, 57);
			this.tbIntegerVersion.Name = "tbIntegerVersion";
			this.tbIntegerVersion.Size = new System.Drawing.Size(200, 20);
			this.tbIntegerVersion.TabIndex = 2;
			this.tbIntegerVersion.Text = "";
			this.tbIntegerVersion.WordWrap = false;
			// 
			// Edit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(536, 270);
			this.Controls.Add(this.panel1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "Edit";
			this.Text = "Edit";
			this.Load += new System.EventHandler(this.Edit_Load);
			this.panel1.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		
		private void PrepareReleaseTypeCombo()
		{
			cbReleaseType.Items.Add("Public");
			cbReleaseType.Items.Add("Beta");
		}

		private void DrawVersionDetail(VersionInfo v)
		{
			tbVersionStr.Text					= v.VersionString;
			tbIntegerVersion.Text				= v.VersionNumber.ToString();
			dateTimePickerReleaseDate.Value		= v.ReleaseDate;
			tbDownloadURL.Text					= v.DownloadUrl;
			tbReleaseNotesURL.Text				= v.ReleaseNotesUrl;

			if ( v.ReleaseType == VersionType.Public )
				cbReleaseType.SelectedIndex = 0;
			else if ( v.ReleaseType == VersionType.Beta )
				cbReleaseType.SelectedIndex = 1;
		}

		private void Edit_Load(object sender, System.EventArgs e)
		{
			PrepareReleaseTypeCombo();

			if ( ThisVersion != null )
				DrawVersionDetail(ThisVersion);
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			// Do something to save the changes

			try
			{
				DoSave();
			}
			catch
			{
				return;
			}

			if ( Updated != null )
			{
				Updated(this, ThisVersion);
			}

			this.Close();
		}

		private bool ValidateForm()
		{
			try
			{
				if ( tbIntegerVersion.Text.Length == 0 )
					return false;

				long n = (long)Convert.ToInt64(tbIntegerVersion.Text);
				labelIntegerVersion.ForeColor = Color.Black;
			}
			catch (Exception e)
			{
				Debug.WriteLine("Int validation failed");
                Debug.WriteLine(e.ToString());
				labelIntegerVersion.ForeColor = Color.Red;
				return false;
			}

			return true;
		}

		private void GetUIData()
		{
			if ( !ValidateForm() )
				throw new Exception("Input validation failed");

			ThisVersion.VersionString	= tbVersionStr.Text;

			if ( tbIntegerVersion.Text.Length > 0 )
				ThisVersion.VersionNumber	= (long)Convert.ToInt64(tbIntegerVersion.Text);

			ThisVersion.ReleaseDate		= dateTimePickerReleaseDate.Value;
			ThisVersion.DownloadUrl		= tbDownloadURL.Text;
			ThisVersion.ReleaseNotesUrl	= tbReleaseNotesURL.Text;

			if ( cbReleaseType.SelectedIndex == 0 )
				ThisVersion.ReleaseType = VersionType.Public;
			else if (cbReleaseType.SelectedIndex == 1 )
				ThisVersion.ReleaseType = VersionType.Beta;

		}
		private void DoSave()
		{
			GetUIData();

			if ( Updated != null )
				Updated(this, ThisVersion);
		}

		private void button4_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}
	}
}
