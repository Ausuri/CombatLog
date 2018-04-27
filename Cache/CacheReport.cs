using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using System.Text;

namespace CombatLog.CacheReport
{
	/// <summary>
	/// Summary description for CacheReport.
	/// </summary>
	public class CacheReport : System.Windows.Forms.Form
	{
		public GameLogCollection GameLogs;
		public string CacheFileName;
		private System.Windows.Forms.RichTextBox rtbReportText;
		private System.Windows.Forms.Panel panelMain;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public CacheReport()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(CacheReport));
			this.rtbReportText = new System.Windows.Forms.RichTextBox();
			this.panelMain = new System.Windows.Forms.Panel();
			this.panelMain.SuspendLayout();
			this.SuspendLayout();
			// 
			// rtbReportText
			// 
			this.rtbReportText.Dock = System.Windows.Forms.DockStyle.Fill;
			this.rtbReportText.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.rtbReportText.Location = new System.Drawing.Point(0, 0);
			this.rtbReportText.Name = "rtbReportText";
			this.rtbReportText.ReadOnly = true;
			this.rtbReportText.Size = new System.Drawing.Size(384, 246);
			this.rtbReportText.TabIndex = 3;
			this.rtbReportText.Text = "rtbReportText";
			// 
			// panelMain
			// 
			this.panelMain.Controls.Add(this.rtbReportText);
			this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelMain.Location = new System.Drawing.Point(0, 0);
			this.panelMain.Name = "panelMain";
			this.panelMain.Size = new System.Drawing.Size(384, 246);
			this.panelMain.TabIndex = 8;
			// 
			// CacheReport
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(384, 246);
			this.Controls.Add(this.panelMain);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "CacheReport";
			this.ShowInTaskbar = false;
			this.Text = "Cache Diagnostics Report";
			this.Load += new System.EventHandler(this.CacheReport_Load);
			this.panelMain.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void CacheReport_Load(object sender, System.EventArgs e)
		{
			int CombatEntryCount = 0;
			StringBuilder ReportText = new StringBuilder();

			FileInfo f = new FileInfo(CacheFileName);

			foreach ( CombatLogCache c in GameLogs.LogCache.CacheData )
				if ( c.IsCombatLog )
					CombatEntryCount++;

			string FileSize;

			FileSize = (f.Length / 1024).ToString();

			ReportText.Append("Items in cache: " + GameLogs.LogCache.CacheData.Count.ToString());
			ReportText.Append(Environment.NewLine);
			ReportText.Append(Environment.NewLine);

			ReportText.Append("Combat Entries: " + CombatEntryCount.ToString());
			ReportText.Append(Environment.NewLine);
			ReportText.Append(Environment.NewLine);

			ReportText.Append("Non Combat Entries: " + (GameLogs.LogCache.CacheData.Count - CombatEntryCount).ToString());
			ReportText.Append(Environment.NewLine);
			ReportText.Append(Environment.NewLine);

			ReportText.Append("Last Updated: " + f.LastWriteTime.ToLongDateString());
			ReportText.Append(Environment.NewLine);
			ReportText.Append(Environment.NewLine);

			ReportText.Append("Cache size on disk: " + FileSize + "K");
			ReportText.Append(Environment.NewLine);
			ReportText.Append(Environment.NewLine);

			rtbReportText.Text = ReportText.ToString();
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}
	}
}
