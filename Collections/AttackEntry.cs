using System;

namespace CombatLog
{
	/// <summary>
	/// Summary description for AttackEntry.
	/// </summary>
	public class AttackEntry
	{
		private DateTime	_LogDTM;
		private string		_EntryType;
		private string		_WeaponName;
		private string		_HitDescription;
		private string		_AttackerName;
		private double		_DamageCaused;
		private bool		_Missed;
		private int			_PositionInFile;
		private int			_MatchStringLength;

		public AttackEntry()
		{
		}

		public string HitType
		{
			get { return HitTypeLib.GetDisplayString(this._HitDescription); }
		}

		public int PositionInFile
		{
			get { return this._PositionInFile; }
			set { this._PositionInFile = value; }
		}

		public string AttackerName
		{
			get { return this._AttackerName; }
			set { this._AttackerName = value; }
		}

		public bool Missed
		{
			get { return this._Missed; }
			set { this._Missed = value; }
		}

		public string WeaponName
		{
			get { return this._WeaponName; }
			set { this._WeaponName = value; }
		}

		public double DamageCaused
		{
			get { return this._DamageCaused; }
			set { this._DamageCaused = value; }
		}

		public int MatchStringLength
		{
			get { return this._MatchStringLength; }
			set { this._MatchStringLength = value; }
		}

		public string EntryType
		{
			get { return this._EntryType; }
			set { this._EntryType = value; }
		}

		public string HitDescription
		{
			get { return this._HitDescription; }
			set { this._HitDescription = value; }
		}

		public System.DateTime LogDTM
		{
			get { return this._LogDTM; }
			set { this._LogDTM = value; }
		}
	}
}
