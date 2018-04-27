using System;
using System.Drawing;
using System.Diagnostics;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace CombatLog.WeaponBrowser
{
	/// <summary>
	/// Summary description for WeaponDataColChooser.
	/// </summary>
	public class WeaponDataColChooser : System.Windows.Forms.Form
	{

		public string[] Columns;
		public CombatLog.Config.UserConfig MyConfig;
		public bool ColumnsChanged = false;

		private System.Windows.Forms.CheckedListBox cblCols;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnMoveUp;
		private System.Windows.Forms.Button btnMoveDown;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public WeaponDataColChooser()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(WeaponDataColChooser));
			this.cblCols = new System.Windows.Forms.CheckedListBox();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnMoveUp = new System.Windows.Forms.Button();
			this.btnMoveDown = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// cblCols
			// 
			this.cblCols.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.cblCols.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.cblCols.CheckOnClick = true;
			this.cblCols.Location = new System.Drawing.Point(8, 64);
			this.cblCols.Name = "cblCols";
			this.cblCols.Size = new System.Drawing.Size(176, 229);
			this.cblCols.TabIndex = 0;
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnCancel.Location = new System.Drawing.Point(192, 304);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "Cancel";
			// 
			// btnOK
			// 
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnOK.Location = new System.Drawing.Point(112, 304);
			this.btnOK.Name = "btnOK";
			this.btnOK.TabIndex = 0;
			this.btnOK.Text = "OK";
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnMoveUp
			// 
			this.btnMoveUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnMoveUp.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnMoveUp.Location = new System.Drawing.Point(192, 64);
			this.btnMoveUp.Name = "btnMoveUp";
			this.btnMoveUp.TabIndex = 2;
			this.btnMoveUp.Text = "Move Up";
			// 
			// btnMoveDown
			// 
			this.btnMoveDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnMoveDown.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnMoveDown.Location = new System.Drawing.Point(192, 96);
			this.btnMoveDown.Name = "btnMoveDown";
			this.btnMoveDown.TabIndex = 3;
			this.btnMoveDown.Text = "Move Down";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(272, 23);
			this.label1.TabIndex = 4;
			this.label1.Text = "Select the columns you want to display";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100, 16);
			this.label2.TabIndex = 5;
			this.label2.Text = "Details";
			// 
			// WeaponDataColChooser
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(272, 334);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnMoveDown);
			this.Controls.Add(this.btnMoveUp);
			this.Controls.Add(this.cblCols);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(280, 300);
			this.Name = "WeaponDataColChooser";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Column Chooser";
			this.Load += new System.EventHandler(this.WeaponDataColChooser_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void DrawColumns()
		{
			if ( Columns != null )
			{
				cblCols.Items.Clear();

				string colName = "";

				Debug.WriteLine("Found " + MyConfig.WeaponBrowserColumnDisplayPreferences.Count + " attribute settings in userconfig");

				foreach ( string s in Columns )
				{
					colName = s;

					if ( MyConfig.WeaponBrowserColumnDisplayPreferences == null )
						MyConfig.WeaponBrowserColumnDisplayPreferences = new Config.WBColumnsCollection();

					if ( MyConfig.WeaponBrowserColumnDisplayPreferences[colName] == null )
					{
						Debug.WriteLine("DisplayAttribute '" + colName + "' not found in userconfig, creating a new instance");

						Config.WBColumns wbc = new Config.WBColumns();
						wbc.AttributeName = colName;
						wbc.ShowAttribute = true;
						MyConfig.WeaponBrowserColumnDisplayPreferences.Add(wbc);
					}

					if ( MyConfig.WeaponBrowserColumnDisplayPreferences[colName].ShowAttribute )
						cblCols.Items.Add(colName, CheckState.Checked);
					else
						cblCols.Items.Add(colName, CheckState.Unchecked);
					
				}
			}
		}

		private void WeaponDataColChooser_Load(object sender, System.EventArgs e)
		{
			DrawColumns();
		}

		private void btnOK_Click(object sender, System.EventArgs e)
		{
			string colName = "";

			for ( int i = 0; i < cblCols.Items.Count; i++ )
			{
				colName = cblCols.Items[i].ToString();

				//
				// Does UserConfig know about this attribute? If not, add it and set it's display state later
				//
				if ( MyConfig.WeaponBrowserColumnDisplayPreferences[colName] == null )
				{
					Debug.WriteLine("Exit: DisplayAttribute: '" + colName + "' not found in userconfig!!");

					Config.WBColumns wbc = new Config.WBColumns();
					wbc.AttributeName = colName;
					MyConfig.WeaponBrowserColumnDisplayPreferences.Add(wbc);
				}

				if ( cblCols.GetItemChecked(i) )
				{
					Debug.WriteLine(colName + ": CHECKED");
					
					if ( MyConfig.WeaponBrowserColumnDisplayPreferences[colName].ShowAttribute )
						ColumnsChanged = true;

					MyConfig.WeaponBrowserColumnDisplayPreferences[colName].ShowAttribute = true;
				}
				else
				{
					if ( MyConfig.WeaponBrowserColumnDisplayPreferences[colName].ShowAttribute )
						ColumnsChanged = true;

					Debug.WriteLine(colName + ": NOT CHECKED");
					MyConfig.WeaponBrowserColumnDisplayPreferences[colName].ShowAttribute = false;
				}
			}

			this.DialogResult = DialogResult.OK;
			this.Close();
		}
	}
}
