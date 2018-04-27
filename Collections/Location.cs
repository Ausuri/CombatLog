using System;

namespace CombatLog.LocationInfo
{
	/// <summary>
	/// Summary description for Location.
	/// </summary>
	public class Location
	{
		private DateTime _LogDTM;
		private string _SourceFileName;
		private string _LocationStr;
		private string _Source;

		public Location()
		{
		}

		public string Source
		{
			get { return this._Source; }
			set { this._Source = value; }
		}

		public string SourceFileName
		{
			get { return this._SourceFileName; }
			set { this._SourceFileName = value; }
		}

		public DateTime LogDTM
		{
			get { return this._LogDTM; }
			set { this._LogDTM = value; }
		}

		public string LocationStr
		{
			get { return this._LocationStr; }
			set { this._LocationStr = value; }
		}
	}
}
