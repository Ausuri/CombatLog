using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Diagnostics;
using CombatLog.VersionManager;

namespace CombatLog
{
	/// <summary>
	/// Summary description for NewVersionForm.
	/// </summary>
	public class NewVersionForm : System.Windows.Forms.Form
	{
		public VersionInfo CurrentVersion;
		public VersionInfo LatestVersion;
		private System.Windows.Forms.Label lblMessage;
		private System.Windows.Forms.Button btnReleaseNotes;
		private System.Windows.Forms.Button btnDownload;
		private System.Windows.Forms.Button btnClose;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public NewVersionForm()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(NewVersionForm));
			this.lblMessage = new System.Windows.Forms.Label();
			this.btnReleaseNotes = new System.Windows.Forms.Button();
			this.btnDownload = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// lblMessage
			// 
			this.lblMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lblMessage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblMessage.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblMessage.Location = new System.Drawing.Point(8, 8);
			this.lblMessage.Name = "lblMessage";
			this.lblMessage.Size = new System.Drawing.Size(288, 112);
			this.lblMessage.TabIndex = 1;
			this.lblMessage.Text = @"There is a newer version of the Combat Log Analyser available for download. See below for details. There is a newer version of the Combat Log Analyser available for download. See below for details. There is a newer version of the Combat Log Analyser available for download. See below for details.";
			// 
			// btnReleaseNotes
			// 
			this.btnReleaseNotes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnReleaseNotes.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnReleaseNotes.Location = new System.Drawing.Point(319, 8);
			this.btnReleaseNotes.Name = "btnReleaseNotes";
			this.btnReleaseNotes.Size = new System.Drawing.Size(88, 23);
			this.btnReleaseNotes.TabIndex = 2;
			this.btnReleaseNotes.Text = "Release Notes";
			this.btnReleaseNotes.Click += new System.EventHandler(this.btnReleaseNotes_Click);
			// 
			// btnDownload
			// 
			this.btnDownload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnDownload.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnDownload.Location = new System.Drawing.Point(319, 37);
			this.btnDownload.Name = "btnDownload";
			this.btnDownload.Size = new System.Drawing.Size(88, 23);
			this.btnDownload.TabIndex = 3;
			this.btnDownload.Text = "Download";
			this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnClose.Location = new System.Drawing.Point(319, 97);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(88, 23);
			this.btnClose.TabIndex = 4;
			this.btnClose.Text = "Close";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// NewVersionForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(416, 128);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnDownload);
			this.Controls.Add(this.btnReleaseNotes);
			this.Controls.Add(this.lblMessage);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(376, 144);
			this.Name = "NewVersionForm";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "New Version Available";
			this.TransparencyKey = System.Drawing.Color.WhiteSmoke;
			this.Load += new System.EventHandler(this.NewVersionForm_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void NewVersionForm_Load(object sender, System.EventArgs e)
		{
			string msg;

			if ( LatestVersion == null || CurrentVersion == null )
				this.Close();

			if ( LatestVersion.VersionNumber > CurrentVersion.VersionNumber )
			{
				this.Text = "New version available";
				msg = "You are currently running version " + CurrentVersion.VersionString + ". The latest version available is " + LatestVersion.VersionString + " which was released on " + LatestVersion.ReleaseDate.ToShortDateString() + ".";


				btnDownload.Enabled = true;
				btnReleaseNotes.Enabled = true;
			}
			else
			{
				this.Text = "Latest version installed";
				msg = "You have the latest version of EVE Combat Log Analyser";
				btnDownload.Enabled = false;
				btnReleaseNotes.Enabled = false;
			}

			lblMessage.Text = msg;
		}

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void btnReleaseNotes_Click(object sender, System.EventArgs e)
		{
			Process.Start(LatestVersion.ReleaseNotesUrl);
		}

		private void btnDownload_Click(object sender, System.EventArgs e)
		{
			Process.Start(LatestVersion.DownloadUrl);
			this.Close();
		}
	}
}
