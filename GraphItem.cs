using System;

namespace CombatLog
{
	/// <summary>
	/// Summary description for GraphItem.
	/// </summary>
	public class GraphItem
	{
		private object _Control;
		private int _Sequence;

		public GraphItem()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public GraphItem(int PreviousSequenceID)
		{
			_Sequence = PreviousSequenceID + 1;
		}

		public GraphItem(int PreviousSequenceID, object GraphControl)
		{
			_Sequence = PreviousSequenceID + 1;
            _Control = GraphControl;
		}

		public object Control
		{
			get { return this._Control; }
			set { this._Control = value; }
		}

		public int Sequence
		{
			get { return this._Sequence; }
			set { this._Sequence = value; }
		}
	}
}
