using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using HGraph;
using CombatLog;

namespace HGraph
{
	// Chart control that displays three horizontal bars: Resolved,
	// Escalated and Open. Exposes public properties to set the three
	// values and specify chart colors.
	public class Chart : UserControl
	{

		public ChartItemCollection ChartItems = null;

		// const values
		private const int Border = 10;
		private const int LabelBorder = 1;
		private const int MessageBorder = 5;
		private const int MinBarWidth = 30;
		private const int LeftPos = 76;
		
		// bounding areas for chart and bars
		private Rectangle m_boundsChart;
		private RectangleF[] m_bounds = new RectangleF[4];
		
		private int m_barHeight;
		
		private int[] m_values = new int[4] {243, 123, 325, 11};
		
		private Color m_barColor = Color.FromArgb(250, 230, 165);
		private Color m_gridLineColor = Color.Gainsboro;
		private Color m_gridLineBaseColor = Color.SteelBlue;
		private Color m_labelColor = Color.DimGray;
		private Color m_valueColor = Color.Black;
		
		// text alignment
		private StringFormat m_formatRight;
		private StringFormat m_formatTop;
		private StringFormat m_formatLeft;
		private StringFormat m_formatCenter;
		
		// gdi objects
		private Brush m_brushLabel;
		private Brush m_brushValue;
		private Brush m_brushBar;
		private Pen m_penBaseGridLine;
		private Pen m_penGridLine;
		
		private IContainer components = null;

		[CategoryAttribute("Chart")]
		[DescriptionAttribute("Color of the chart bars.")]
		[DefaultValueAttribute(typeof(Color), "250, 230, 165")]
		public Color BarColor
		{
			get
			{
				return m_barColor;
			}

			set
			{
				if (value == Color.Empty)
				{
					value = Color.FromArgb(250, 230, 165);
				}
				m_barColor = value;
				m_brushBar = new SolidBrush(m_barColor);
				Invalidate();
			}
		}

		[DescriptionAttribute("Color of the gridlines.")]
		[DefaultValueAttribute(typeof(Color), "Gainsboro")]
		[CategoryAttribute("Chart")]
		public Color GridLineColor
		{
			get
			{
				return m_gridLineColor;
			}

			set
			{
				if (value == Color.Empty)
				{
					value = Color.Gainsboro;
				}
				m_gridLineColor = value;
				m_penGridLine = new Pen(m_gridLineColor);
				Invalidate();
			}
		}

		[DescriptionAttribute("Color of the base gridline.")]
		[CategoryAttribute("Chart")]
		[DefaultValueAttribute(typeof(Color), "SteelBlue")]
		public Color BaseGridLineColor
		{
			get
			{
				return m_gridLineBaseColor;
			}

			set
			{
				if (value == Color.Empty)
				{
					value = Color.SteelBlue;
				}
				m_gridLineBaseColor = value;
				m_penBaseGridLine = new Pen(m_gridLineBaseColor);
				Invalidate();
			}
		}

		[CategoryAttribute("Chart")]
		[DescriptionAttribute("Color of the labels and message.")]
		[DefaultValueAttribute(typeof(Color), "DimGray")]
		public Color LabelColor
		{
			get
			{
				return m_labelColor;
			}

			set
			{
				if (value == Color.Empty)
				{
					value = Color.DimGray;
				}
				m_labelColor = value;
				m_brushLabel = new SolidBrush(m_labelColor);
				Invalidate();
			}
		}

		[DefaultValueAttribute(typeof(Color), "Black")]
		[CategoryAttribute("Chart")]
		[DescriptionAttribute("Color of the bar values.")]
		public Color ValueColor
		{
			get
			{
				return m_valueColor;
			}

			set
			{
				if (value == Color.Empty)
				{
					value = Color.Black;
				}
				m_valueColor = value;
				m_brushValue = new SolidBrush(m_valueColor);
				Invalidate();
			}
		}

		[DefaultValueAttribute("")]
		[CategoryAttribute("Chart")]
		[DescriptionAttribute("Chart message at the bottom.")]
		public string Message
		{
			get
			{
				return base.Text;
			}

			set
			{
				base.Text = value;
			}
		}

		public Chart()
		{
			// init the gdi objects, do this by setting the
			// color properties (creates the associated gdi object)
			BarColor = m_barColor;
			GridLineColor = m_gridLineColor;
			BaseGridLineColor = m_gridLineBaseColor;
			LabelColor = m_labelColor;
			ValueColor = m_valueColor;
			
			// init the font
			Font = new Font("arial", 7.0F, FontStyle.Regular);
			
			// setup text alignments, used later when draw strings
			m_formatRight = new StringFormat();
			m_formatRight.Alignment = StringAlignment.Far;
			m_formatRight.LineAlignment = StringAlignment.Center;
			
			m_formatTop = new StringFormat();
			m_formatTop.Alignment = StringAlignment.Center;
			m_formatTop.LineAlignment = StringAlignment.Near;
			
			m_formatLeft = new StringFormat();
			m_formatLeft.LineAlignment = StringAlignment.Center;
			
			m_formatCenter = new StringFormat();
			m_formatCenter.Alignment = StringAlignment.Center;
			m_formatCenter.LineAlignment = StringAlignment.Center;
			
			// turn on double buffering
			SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, true);
			
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
			Name = "Chart";
			Size = new Size(120, 120);
		}

		// recalculate the position of the bars and labels
		protected override void OnSizeChanged(EventArgs e)
		{
			CalculateBounds();
			base.OnSizeChanged(e);
		}
		
		// recalculate the position of the bars and labels
		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
			CalculateBounds();
		}

		// update right away
		protected override void OnTextChanged(EventArgs e)
		{
			base.OnTextChanged(e);
			CalculateBounds();
			Invalidate();
		}

		// draw the chart
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			
			// clear and init the drawing surface
			e.Graphics.Clear(Parent.BackColor);
			e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
			
			// only draw if sized large enough to display the data
			if (Width > LeftPos && m_barHeight > 1)
			{
				DrawLabels(e.Graphics);
				DrawChart(e.Graphics);
			}
			
			// always draw the message
			DrawMessage(e.Graphics);
		}

		private ChartItemCollection GetChartItemsWithData()
		{
			return ChartItems;

//			ChartItemCollection ItemsWithData = new ChartItemCollection();
//			foreach ( ChartItem c in ChartItems )
//				if ( c.Value != 0 )
//					ItemsWithData.Add(c);
//
//			return ItemsWithData;
		}

		// calculate the bounding area, used later when drawing the chart
		public void CalculateBounds()
		{
			if ( ChartItems == null )
				return;

			// chart area
			// Height of the component - the height of 1 line of text (Font.Height) - the required border (top and bottom)
			//m_boundsChart = new Rectangle(LeftPos, Border, Width - Border - LeftPos, Height - Font.Height - (Border * 2));

			Font = new Font("Verdana", 8.5F);

			int ChartHeight;

			ChartItemCollection ItemsWithData = GetChartItemsWithData();

			ChartHeight = Font.Height * ItemsWithData.Count + Border * 2 + ( Font.Height );

			m_boundsChart = new Rectangle(LeftPos, Border, Width - Border - LeftPos, ChartHeight);

			m_boundsChart.Height -= (Font.Height);
		
			int RowCount = ItemsWithData.Count;

			if ( RowCount == 0 )
				m_barHeight = m_boundsChart.Height;
			else
				m_barHeight = m_boundsChart.Height / (ItemsWithData.Count * 2);

			int ItemPositionY;
			for ( int i = 0; i < ItemsWithData.Count ; i++ )
			{
				ItemPositionY = m_boundsChart.Top + ( Font.Height * i );
				ItemsWithData[i].Bounds = new RectangleF(0.0F, ItemPositionY, (LeftPos - LabelBorder), Font.Height + (Font.Height / 2));
			}
	
			Size = new Size(Size.Width, ChartHeight + Font.Height);
		}

		// labels on the left side
		private void DrawLabels(Graphics g)
		{
			if ( ChartItems == null )
				return;

			ChartItemCollection ItemsWithData = GetChartItemsWithData();
            
			foreach ( ChartItem ci in ItemsWithData )
			{
				g.DrawString(ci.Label, Font, m_brushLabel, ci.Bounds, m_formatRight);
			}
		}

		// message at the bottom left
		private void DrawMessage(Graphics g)
		{
			if (Text != null && Text.Length != 0)
			{
				// draw the message
				g.DrawString(Text, Font, m_brushLabel, MessageBorder, (Bottom - MessageBorder - Font.Height));
			}
		}

		// draw the chart, gridlines, bars and values
		private void DrawChart(Graphics g)
		{
			// calcualte the distance between the gridlines
			int space = m_boundsChart.Width / 5;
			
			ChartItemCollection ItemsWithData = GetChartItemsWithData();

			// draw the gridlines
			for (int i = 1; i <= 5; i++)
			{
				g.DrawLine(m_penGridLine, m_boundsChart.Left + (space * i), m_boundsChart.Top, m_boundsChart.Left + (space * i), m_boundsChart.Bottom);
			}
			
			// draw the gridline labels
			int tick = (int)Math.Round( Math.Ceiling( ItemsWithData.GetBiggestValue() / 5.0));

			for (int i = 0; i <= 5; i++)
			{
				g.DrawString((tick * i).ToString(), Font, m_brushLabel, (m_boundsChart.Left + space * i), m_boundsChart.Bottom, m_formatTop);
			}
			
			// calculate the real chart width (might be less then the control width)
			int chartWidth = space * 5;
			
			// ending tick mark
			int totalTicks = tick * 5;
			
			// loop through and draw each bar
			int barWidth = 0;
			string ValueStr = "";
			
			for (int i = 0; i < ItemsWithData.Count ; i++)
			{
				// bar
				if (totalTicks > 0)
				{
					barWidth = (chartWidth * (int)ItemsWithData[i].Value) / totalTicks;
				}
				
				g.FillRectangle(m_brushBar, m_boundsChart.Left, ItemsWithData[i].Bounds.Top + 4, barWidth, Font.Height-4);
				
				if ( ItemsWithData[i].Value == 0 )
					ValueStr = "0.0";
				else
					ValueStr = ItemsWithData[i].Value.ToString();
			
				// label
				if (barWidth > MinBarWidth)
				{
					// center label
					g.DrawString(	ValueStr, 
									Font, 
									m_brushValue, 
									(m_boundsChart.Left + (barWidth / 2)), 
									ItemsWithData[i].Bounds.Top + (int)Math.Round((double)(ItemsWithData[i].Bounds.Height  / 2.0F)) + 1.0F, 
									m_formatCenter);
				}
				else
				{
					// label outside of the bar
					g.DrawString(	ValueStr, 
									Font, 
									m_brushValue, 
									(m_boundsChart.Left + barWidth + 2), 
									ItemsWithData[i].Bounds.Top + (int)Math.Round((double)(ItemsWithData[i].Bounds.Height / 2.0F)) + 1.0F, 
									m_formatLeft);
				}
			}

			// draw the baseline last
			g.DrawLine(m_penBaseGridLine, m_boundsChart.Left, m_boundsChart.Top, m_boundsChart.Left, m_boundsChart.Bottom);

			//Debug.WriteLine("\tChart Dimensions");
			//Debug.WriteLine("\t\tTop = " + m_boundsChart.Top + " Bottom = " + m_boundsChart.Bottom);
			//Debug.WriteLine("\t\tHEIGHT = " + this.Height);
		}

		public Rectangle ChartBounds
		{
			get { return m_boundsChart; }
		}
	}
}