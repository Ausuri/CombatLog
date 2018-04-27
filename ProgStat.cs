using System;
using System.Windows.Forms;

namespace CombatLog
{
	public class ProgressStatus : StatusBar
	{
		private int _progressBarPanel = -1;
		public ProgressBar progressBar = new ProgressBar();
		public bool _progressBarHidden;


		public ProgressStatus()
		{
			progressBar.Hide();
			this.Controls.Add(progressBar);
			this.DrawItem += new StatusBarDrawItemEventHandler(this.Reposition);
		}

		public int setProgressBarPanel
		{
			//
			// Property to tell the StatusBar which panel to use for the ProgressBar.
			//
			get { return _progressBarPanel; }
			set 
			{
				_progressBarPanel = value;
				this.Panels[_progressBarPanel].Style = StatusBarPanelStyle.OwnerDraw;
			}
		}

		public void HideProgressBar()
		{
			this._progressBarHidden = true;
			progressBar.Hide();
		}

		public void ShowProgressBar()
		{
			this._progressBarHidden = false;
			progressBar.Show();
		}

		private void Reposition(object sender, StatusBarDrawItemEventArgs sbdevent)
		{
			progressBar.Location = new System.Drawing.Point(sbdevent.Bounds.X, sbdevent.Bounds.Y);
			progressBar.Size = new System.Drawing.Size(sbdevent.Bounds.Width, sbdevent.Bounds.Height);

			if ( this._progressBarHidden )
			{
				progressBar.Hide();
				return;
			}

			progressBar.Show();
		}
	}
}
