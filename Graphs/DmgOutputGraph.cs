using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using ZedGraph;

namespace CombatLog.Graphs
{
	/// <summary>
	/// Summary description for DmgOutputGraph.
	/// </summary>
	public class DmgOutputGraph : System.Windows.Forms.Form
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private ZedGraph.ZedGraphControl zedGraphControl1;

		public GameLog thisGameLog;

		public DmgOutputGraph()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

//			if ( thisGameLog == null )
//			{
//				MessageBox.Show("GameLog is null, can't do anything");
//				return;
//			}
//
//			DrawGraph();
		}

		public void DrawGraph()
		{
			// ZedGraphControl control = new ZedGraphControl();

			GraphPane myPane = zedGraphControl1.GraphPane;

			// Set the title and axis labels
			myPane.Title = "Damage output report";
			myPane.XAxis.Title = "Time (sec)";
			myPane.YAxis.Title = "Shot Damage";
			
			myPane.AxisFill = new Fill( Color.White, Color.FromArgb( 230, 230, 255 ), 45.0f );

			// Make up some data arrays based on the Sine function
			PointPairList list1 = new PointPairList();
			PointPairList list2 = new PointPairList();

			CombatLogEntryCollection	combatLog = thisGameLog.GetCombatEntries();
			AttackEntryCollection		attackLog = thisGameLog.GetAttackEntries();


			foreach (CombatLogEntry c in combatLog )
			{
				if ( c.DamageCaused != 0 )
					list1.Add(c.LogDTM.TimeOfDay.TotalSeconds - combatLog.LogEntriesStartDTM.TimeOfDay.TotalSeconds , c.DamageCaused, Utils.LogFile.GetLineFromPosition(thisGameLog.FileName, c.PositionInFile));
			}

			

			foreach ( AttackEntry c in attackLog )
			{
				if ( c.DamageCaused != 0 )
					list2.Add(c.LogDTM.TimeOfDay.TotalSeconds - attackLog.LogEntriesStartDTM.TimeOfDay.TotalSeconds, c.DamageCaused, Utils.LogFile.GetLineFromPosition(thisGameLog.FileName, c.PositionInFile));
			}


//			for ( int i=0; i<36; i++ )
//			{
//				double x = (double) i + 5;
//				double y1 = 1.5 + Math.Sin( (double) i * 0.2 );
//				double y2 = 3.0 * ( 1.5 + Math.Sin( (double) i * 0.2 ) );
//				list1.Add( x, y1 );
//				list2.Add( x, y2 );
//			}

			// Generate a red curve with diamond
			// symbols, and "Porsche" in the legend
			LineItem myCurve = myPane.AddCurve( "Outgoing",
				list1, Color.Blue, SymbolType.Circle );

			// Generate a blue curve with circle
			// symbols, and "Piper" in the legend
			LineItem myCurve2 = myPane.AddCurve( "Incoming",
				list2, Color.Red, SymbolType.Triangle );

			zedGraphControl1.AxisChange();
			// base.ZedGraphControl.AxisChange();
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(DmgOutputGraph));
			this.zedGraphControl1 = new ZedGraph.ZedGraphControl();
			this.SuspendLayout();
			// 
			// zedGraphControl1
			// 
			this.zedGraphControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.zedGraphControl1.IsAutoScrollRange = false;
			this.zedGraphControl1.IsEnableHPan = true;
			this.zedGraphControl1.IsEnableHZoom = true;
			this.zedGraphControl1.IsEnableVPan = true;
			this.zedGraphControl1.IsEnableVZoom = true;
			this.zedGraphControl1.IsPrintFillPage = true;
			this.zedGraphControl1.IsPrintKeepAspectRatio = true;
			this.zedGraphControl1.IsScrollY2 = false;
			this.zedGraphControl1.IsShowContextMenu = false;
			this.zedGraphControl1.IsShowCopyMessage = true;
			this.zedGraphControl1.IsShowCursorValues = false;
			this.zedGraphControl1.IsShowHScrollBar = false;
			this.zedGraphControl1.IsShowPointValues = true;
			this.zedGraphControl1.IsShowVScrollBar = false;
			this.zedGraphControl1.IsZoomOnMouseCenter = false;
			this.zedGraphControl1.Location = new System.Drawing.Point(8, 8);
			this.zedGraphControl1.Name = "zedGraphControl1";
			this.zedGraphControl1.PanButtons = System.Windows.Forms.MouseButtons.Left;
			this.zedGraphControl1.PanButtons2 = System.Windows.Forms.MouseButtons.Middle;
			this.zedGraphControl1.PanModifierKeys2 = System.Windows.Forms.Keys.None;
			this.zedGraphControl1.PointDateFormat = "g";
			this.zedGraphControl1.PointValueFormat = "G";
			this.zedGraphControl1.ScrollMaxX = 0;
			this.zedGraphControl1.ScrollMaxY = 0;
			this.zedGraphControl1.ScrollMaxY2 = 0;
			this.zedGraphControl1.ScrollMinX = 0;
			this.zedGraphControl1.ScrollMinY = 0;
			this.zedGraphControl1.ScrollMinY2 = 0;
			this.zedGraphControl1.Size = new System.Drawing.Size(728, 360);
			this.zedGraphControl1.TabIndex = 0;
			this.zedGraphControl1.ZoomButtons = System.Windows.Forms.MouseButtons.Left;
			this.zedGraphControl1.ZoomButtons2 = System.Windows.Forms.MouseButtons.None;
			this.zedGraphControl1.ZoomModifierKeys = System.Windows.Forms.Keys.None;
			this.zedGraphControl1.ZoomModifierKeys2 = System.Windows.Forms.Keys.None;
			this.zedGraphControl1.ZoomStepFraction = 0.1;
			// 
			// DmgOutputGraph
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(744, 382);
			this.Controls.Add(this.zedGraphControl1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "DmgOutputGraph";
			this.Text = "Damage Output Graph";
			this.ResumeLayout(false);

		}
		#endregion
	}
}
