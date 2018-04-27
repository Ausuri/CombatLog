using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Diagnostics;

namespace CombatLog.Panes
{
	/// <summary>
	/// Summary description for Notify.
	/// </summary>
	public class Notify : System.Windows.Forms.Form
	{
		private enum FormStatus
		{
			Open = 1,
			Opening = 2,
			Closing = 4,
			Closed = 8
		}

		private FormStatus formStatus = FormStatus.Open;
		
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label4;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private Timer fadeTimer = new Timer();						
		private int showFadeInterval = 3000;
		private int fadeInterval = 30;		
		private bool doFadeClose = false;

		public string EVELocation;
		public string Targets;

		private System.Windows.Forms.Label lblLocation;
		private System.Windows.Forms.Label lblTargets;
		public ArrayList ActiveNotifyWindows;

		public Notify()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Notify));
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.label2 = new System.Windows.Forms.Label();
			this.lblLocation = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.lblTargets = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(8, 4);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(16, 16);
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label2.Location = new System.Drawing.Point(32, -1);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(64, 23);
			this.label2.TabIndex = 1;
			this.label2.Text = "Character:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.label2.Click += new System.EventHandler(this.pictureBox1_Click);
			// 
			// lblLocation
			// 
			this.lblLocation.Location = new System.Drawing.Point(96, -1);
			this.lblLocation.Name = "lblLocation";
			this.lblLocation.Size = new System.Drawing.Size(208, 23);
			this.lblLocation.TabIndex = 2;
			this.lblLocation.Text = "A charactername";
			this.lblLocation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.lblLocation.Click += new System.EventHandler(this.pictureBox1_Click);
			// 
			// label4
			// 
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label4.Location = new System.Drawing.Point(32, 23);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(64, 23);
			this.label4.TabIndex = 1;
			this.label4.Text = "Targets:";
			this.label4.Click += new System.EventHandler(this.pictureBox1_Click);
			// 
			// lblTargets
			// 
			this.lblTargets.Location = new System.Drawing.Point(96, 23);
			this.lblTargets.Name = "lblTargets";
			this.lblTargets.Size = new System.Drawing.Size(208, 32);
			this.lblTargets.TabIndex = 2;
			this.lblTargets.Text = "Domination Warlord, Arch Angel General";
			this.lblTargets.Click += new System.EventHandler(this.pictureBox1_Click);
			// 
			// Notify
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.SystemColors.Info;
			this.ClientSize = new System.Drawing.Size(312, 56);
			this.Controls.Add(this.lblLocation);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.lblTargets);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Notify";
			this.Opacity = 0;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "New Combat Log File...";
			this.TopMost = true;
			this.Click += new System.EventHandler(this.Notify_Click);
			this.Closing += new System.ComponentModel.CancelEventHandler(this.Notify_Closing);
			this.Load += new System.EventHandler(this.Notify_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void Notify_Load(object sender, System.EventArgs e)
		{
			this.lblLocation.Text = this.EVELocation;

			this.lblTargets.Text = this.Targets;
				
			// Look at other active notify windows to determine screen position
			//
			// Open window in bottom right hand side of desktop
			//
			// Start tick timer to control opacity fade in / out
			//
			// If click event received, cancel fade operation and persist the form on-screen until
			// closed by the user.

			System.Drawing.Point FormPos = GetLocation();

			this.SetDesktopLocation(FormPos.X, FormPos.Y);

			this.formStatus = FormStatus.Opening;

			fadeTimer.Tick += new EventHandler(DoFade);
			fadeTimer.Interval = fadeInterval;
			fadeTimer.Enabled = true;
		}
	
		private void DoFade(object sender, EventArgs e)
		{						
			if (formStatus == FormStatus.Opening)
			{
				//if the splash is opening, the opacity will increase in increments of 5 until it is fully
				//shown.  Once it is fully opacic, it sets the status flag to open.
				if (this.Opacity < 0.9)
					this.Opacity += .05;
				else								
					formStatus = FormStatus.Open;
			}
			else if (formStatus == FormStatus.Closing)
			{
				//if the splash is closing, the opacity will decrease in increments of 5 until it is fully
				//hidden.  Once it is fully transparent, it sets the status flag to closed.
				if (this.Opacity > .10)
				{
					this.Opacity -= .05;
					fadeTimer.Interval = fadeInterval;
				}
				else
				{
					formStatus = FormStatus.Closed;					
				}
			}
			else if (formStatus == FormStatus.Open)
			{
				//Once the splash is open and fully shown, the timer interval is set to the splash delay setting,
				//which is defaulted to 2 seconds, and then sets the status flag depending on if the user
				//wants to just close the splash or fade it out.
				fadeTimer.Interval = showFadeInterval;				
				doFadeClose = true;

				if (doFadeClose)
					formStatus = FormStatus.Closing;
				else
					formStatus = FormStatus.Closed;				
			}			
			else if(formStatus == FormStatus.Closed)
			{
				//This is called if the splash is ready to be closed.  In order to do this
				//we first dispose of the timer (we dont need any leaks do we?) and then
				//call the base.MainForm.Close function which will triger the MainFormClosed event
				//that we overrode so we can set the Application Context's main form as the 
				//main form the user passed into the constructor.
				fadeTimer.Enabled = false;
				fadeTimer.Dispose();
				this.Close();
			}
		}

		private System.Drawing.Point GetLocation()
		{
			int ActiveCount;
			int X, Y;

			if ( ActiveNotifyWindows != null )
				ActiveCount = ActiveNotifyWindows.Count + 1;
			else
				ActiveCount = 1;

			X = SystemInformation.WorkingArea.Size.Width - this.Size.Width;
			Y = SystemInformation.WorkingArea.Size.Height - this.Size.Height * (ActiveCount);

			return new System.Drawing.Point(X,Y);
		}

		private void Notify_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			this.ActiveNotifyWindows.Remove(this);
		}

		private void Notify_Click(object sender, System.EventArgs e)
		{
			Debug.WriteLine("Form clicked...");
		}

		private void pictureBox1_Click(object sender, System.EventArgs e)
		{
			this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
			this.fadeTimer.Enabled = false;
			this.fadeTimer.Dispose();
			this.Opacity = 1;
		}

	}
}
