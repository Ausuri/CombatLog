using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using System.Text;
using System.Diagnostics;

namespace CombatLog
{
	/// <summary>
	/// Summary description for TextForm.
	/// </summary>
	public class TextForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.RichTextBox richTextBox1;
		private TD.SandBar.MenuBar menuBar1;
		private TD.SandBar.ContextMenuBarItem contextMenuBarItem1;
		private TD.SandBar.MenuButtonItem mbCopy;
		private TD.SandBar.MenuButtonItem mbSelectAll;
		private TD.SandBar.MenuButtonItem mbOpenInEditor;
		private TD.SandBar.MenuButtonItem mbWordWrapToggle;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public TextForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		public TextForm(string TextFileName)
		{
			InitializeComponent();
			ShowTextFile(TextFileName);
		}

		private void ShowTextFile(string _FileName)
		{
			StringBuilder _TextFile = new StringBuilder();

			if (!File.Exists(_FileName)) 
			{
				MessageBox.Show("The file " + _FileName + " does not exist.","Error");
				this.Close();
				return;
			}

			try
			{
				StreamReader sr = File.OpenText(_FileName);

				_TextFile.Append(sr.ReadToEnd());

				sr.Close();
			}
			catch
			{
				MessageBox.Show("There was a problem opening the file - " + _FileName, "Error");
				return;
			}

			richTextBox1.Text = _TextFile.ToString();
			richTextBox1.Tag = _FileName;
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(TextForm));
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.menuBar1 = new TD.SandBar.MenuBar();
			this.contextMenuBarItem1 = new TD.SandBar.ContextMenuBarItem();
			this.mbCopy = new TD.SandBar.MenuButtonItem();
			this.mbSelectAll = new TD.SandBar.MenuButtonItem();
			this.mbOpenInEditor = new TD.SandBar.MenuButtonItem();
			this.mbWordWrapToggle = new TD.SandBar.MenuButtonItem();
			this.SuspendLayout();
			// 
			// richTextBox1
			// 
			this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.richTextBox1.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.richTextBox1.Location = new System.Drawing.Point(0, 0);
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.ReadOnly = true;
			this.menuBar1.SetSandBarMenu(this.richTextBox1, this.contextMenuBarItem1);
			this.richTextBox1.Size = new System.Drawing.Size(504, 382);
			this.richTextBox1.TabIndex = 4;
			this.richTextBox1.Text = "";
			this.richTextBox1.WordWrap = false;
			// 
			// menuBar1
			// 
			this.menuBar1.Buttons.AddRange(new TD.SandBar.ToolbarItemBase[] {
																				this.contextMenuBarItem1});
			this.menuBar1.Guid = new System.Guid("0602e24e-6705-42cf-841b-bde961d62bc2");
			this.menuBar1.Location = new System.Drawing.Point(0, 0);
			this.menuBar1.Name = "menuBar1";
			this.menuBar1.Size = new System.Drawing.Size(504, 24);
			this.menuBar1.TabIndex = 5;
			this.menuBar1.Text = "menuBar1";
			this.menuBar1.Visible = false;
			// 
			// contextMenuBarItem1
			// 
			this.contextMenuBarItem1.MenuItems.AddRange(new TD.SandBar.MenuButtonItem[] {
																							this.mbOpenInEditor,
																							this.mbCopy,
																							this.mbWordWrapToggle,
																							this.mbSelectAll});
			this.contextMenuBarItem1.BeforePopup += new TD.SandBar.MenuItemBase.BeforePopupEventHandler(this.contextMenuBarItem1_BeforePopup);
			// 
			// mbCopy
			// 
			this.mbCopy.BeginGroup = true;
			this.mbCopy.Enabled = false;
			this.mbCopy.Shortcut = System.Windows.Forms.Shortcut.CtrlC;
			this.mbCopy.Text = "Copy";
			this.mbCopy.Activate += new System.EventHandler(this.mbCopy_Activate);
			// 
			// mbSelectAll
			// 
			this.mbSelectAll.Shortcut = System.Windows.Forms.Shortcut.CtrlA;
			this.mbSelectAll.Text = "Select All";
			this.mbSelectAll.Activate += new System.EventHandler(this.mbSelectAll_Activate);
			// 
			// mbOpenInEditor
			// 
			this.mbOpenInEditor.Text = "Open in editor";
			this.mbOpenInEditor.Activate += new System.EventHandler(this.mbOpenInEditor_Activate);
			// 
			// mbWordWrapToggle
			// 
			this.mbWordWrapToggle.BeginGroup = true;
			this.mbWordWrapToggle.Shortcut = System.Windows.Forms.Shortcut.CtrlW;
			this.mbWordWrapToggle.Text = "Word Wrap On";
			this.mbWordWrapToggle.Activate += new System.EventHandler(this.mbWordWrapToggle_Activate);
			// 
			// TextForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(504, 382);
			this.Controls.Add(this.menuBar1);
			this.Controls.Add(this.richTextBox1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "TextForm";
			this.ShowInTaskbar = false;
			this.Text = "Text File Viewer";
			this.ResumeLayout(false);

		}
		#endregion

		private void menuButtonItem4_Activate(object sender, System.EventArgs e)
		{
		
		}

		private void contextMenuBarItem1_BeforePopup(object sender, TD.SandBar.MenuPopupEventArgs e)
		{
			// Cut
			// Copy
			// Delete

			if ( richTextBox1.SelectedText.Length > 0 )
				mbCopy.Enabled = true;
			else
				mbCopy.Enabled = false;

			// Paste

			// Select All

			if ( richTextBox1.Text.Length > 0 && richTextBox1.SelectedText.Length < richTextBox1.Text.Length )
				mbSelectAll.Enabled = true;
			else
				mbSelectAll.Enabled = false;
		}

		private void mbCopy_Activate(object sender, System.EventArgs e)
		{
			richTextBox1.Copy();
		}

		private void mbSelectAll_Activate(object sender, System.EventArgs e)
		{
			richTextBox1.SelectAll();
		}

		private void mbWordWrapToggle_Activate(object sender, System.EventArgs e)
		{
			if ( richTextBox1.WordWrap )
			{
				richTextBox1.WordWrap = false;
				mbWordWrapToggle.Text = "Word Wrap On";
			}
			else
			{
				richTextBox1.WordWrap = true;
				mbWordWrapToggle.Text = "Word Wrap Off";
			}
		}

		private void mbOpenInEditor_Activate(object sender, System.EventArgs e)
		{
			Process.Start((string)richTextBox1.Tag);
		}
	}
}
