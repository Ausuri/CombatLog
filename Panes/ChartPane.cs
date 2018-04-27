using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using HGraph;

namespace HGraph
{
	public class ChartPane : UserControl
	{
		public Chart Chart1;
		public PaneCaption PaneCaption2;
		//private IssueSubject m_subject;

		private IContainer components = null;

		public ChartPane()
		{
			InitializeComponent();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		[DebuggerStepThroughAttribute()]
		private void InitializeComponent()
		{
			PaneCaption2 = new PaneCaption();
			Chart1 = new Chart();
			SuspendLayout();
			PaneCaption2.AntiAlias = false;
			PaneCaption2.Caption = "Hit Types";
			PaneCaption2.Dock = DockStyle.Top;
			PaneCaption2.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
			PaneCaption2.InactiveGradientHighColor = Color.FromArgb(175, 200, 245);
			PaneCaption2.InactiveGradientLowColor = Color.FromArgb(205, 225, 250);
			PaneCaption2.InactiveTextColor = Color.Black;
			PaneCaption2.Location = new Point(0, 0);
			PaneCaption2.Name = "PaneCaption2";
			PaneCaption2.Size = new Size(272, 20);
			PaneCaption2.TabIndex = 22;
			Chart1.BackColor = SystemColors.MenuText;
			Chart1.Dock = DockStyle.Top;
			Chart1.Font = new Font("Arial", 7.0F);
			Chart1.ForeColor = SystemColors.ControlDarkDark;
			Chart1.LabelColor = Color.Gray;
			Chart1.Location = new Point(0, 20);
			Chart1.Message = "asdfasdfasdf";
			Chart1.Name = "Chart1";
			Chart1.Location = new Point(8, PaneCaption2.Font.Height + 8);
			Chart1.TabIndex = 23;
			Chart1.BackColor = Color.Plum;
			BackColor = SystemColors.Window;
			Controls.Add(Chart1);
			Controls.Add(PaneCaption2);
			Name = "ChartPane";
			Size = new Size(272, 216);
			ResumeLayout(false);
		}

		// m_subject.IssueDataChanged
		private void m_subject_IssueDataChanged(object sender, EventArgs e)
		{
//			Chart1.Open = m_subject.GetOpenCount();
//			Chart1.Escalated = m_subject.GetEscalatedCount();
//			Chart1.Resolved = m_subject.GetClosedCount();
		}
	}
}
