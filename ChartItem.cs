using System;
using System.Drawing;

namespace CombatLog
{
	/// <summary>
	/// Summary description for ChartItem.
	/// </summary>
	public class ChartItem
	{
		private string _Label;
		private double _Value;
		private RectangleF _Bounds;

		public ChartItem()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public ChartItem(string Label, double Value)
		{
			this._Label = Label;
			this._Value = Value;
		}

		public double Value
		{
			get { return this._Value; }
			set { this._Value = value; }
		}

		public string Label
		{
			get { return this._Label; }
			set { this._Label = value; }
		}

		public System.Drawing.RectangleF Bounds
		{
			get { return this._Bounds; }
			set { this._Bounds = value; }
		}
	}
}
