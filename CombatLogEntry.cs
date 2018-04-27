using System;

namespace CombatLog
{
	public enum HitTypes { Miss, CloseMiss, Scratch, Glancing, Light, Hit, Good, Excellent, Wrecking, Unknown };

	/// <summary>
	/// Summary description for CombatLogEntry.
	/// </summary>
	public class CombatLogEntry
	{
		private DateTime _LogDTM;
		private string _EntryType;
		private string _WeaponName;
		private string _HitDescription;
		private string _TargetName;
		private double _DamageCaused;
		private bool _Missed;
		private int _PositionInFile;
		private int _MatchStringLength;
		private bool _IsNotifyMessage;

		public CombatLogEntry()
		{
		}

		public string HitType
		{
			get { return HitTypeLib.GetDisplayString(this._HitDescription); }
		}

		public bool IsNotifyMessage
		{
			get { return this._IsNotifyMessage; }
			set { this._IsNotifyMessage = value; }
		}

		public double DamageCaused
		{
			get { return this._DamageCaused; }
			set { this._DamageCaused = value; }
		}

		public string HitDescription
		{
			get { return this._HitDescription; }
			set { this._HitDescription = value; }
		}

		public string EntryType
		{
			get { return this._EntryType; }
			set { this._EntryType = value; }
		}

		public System.DateTime LogDTM
		{
			get { return this._LogDTM; }
			set { this._LogDTM = value; }
		}

		public string WeaponName
		{
			get { return this._WeaponName; }
			set { this._WeaponName = value; }
		}

		public string TargetName
		{
			get { return this._TargetName; }
			set { this._TargetName = value; }
		}

		public bool Missed
		{
			get { return this._Missed; }
			set { this._Missed = value; }
		}

		public int PositionInFile
		{
			get { return _PositionInFile; }
			set { _PositionInFile = value; }
		}

		public int MatchStringLength
		{
			get { return _MatchStringLength; }
			set { _MatchStringLength = value; }
		}
	}
}
