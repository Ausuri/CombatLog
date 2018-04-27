using System;

namespace CombatLog
{
	/// <summary>
	/// Summary description for HitType.
	/// </summary>
	public class HitTypeLookup
	{
		private int _Priority;
		private string _DisplayName;
		private string _LogText;

		public HitTypeLookup()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public HitTypeLookup(int Priority, string LogText, string DisplayName)
		{
			_Priority = Priority;
			_LogText = LogText;
			_DisplayName = DisplayName;
		}

		public int Priority
		{
			get { return this._Priority; }
			set { this._Priority = value; }
		}

		public string LogName
		{
			get { return this._LogText; }
			set { this._LogText = value; }
		}

		public string DisplayName
		{
			get { return this._DisplayName; }
			set { this._DisplayName = value; }
		}
	}
}
