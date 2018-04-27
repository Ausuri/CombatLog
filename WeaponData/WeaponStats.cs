using System;

namespace CombatLog.WeaponSummaryData
{
	/// <summary>
	/// Summary description for WeaponStats.
	/// </summary>
	[Serializable()]
	public class WeaponStats
	{
		private string		_WeaponName = "";
		private double		_HigestDamage = 0;
		private string		_HigestDamageTarget = "";
		private long		_HighestDamagePositionInFile = 0;
		private string		_HighestDamageHitType = "";
		private DateTime	_HighestDamageDTM;
		private double		_TotalDamage = 0;
		private double		_TotalShots = 0;
		private double		_TotalHit = 0;
		private double		_TotalMissed = 0;

		public WeaponStats()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public DateTime HighestDamageDTM
		{
			get { return this._HighestDamageDTM; }
			set { this._HighestDamageDTM = value; }
		}

		public long HighestDamagePositionInFile
		{
			get { return _HighestDamagePositionInFile; }
			set { _HighestDamagePositionInFile = value; }
		}

		public string HighestDamageHitType
		{
			get { return _HighestDamageHitType; }
			set { _HighestDamageHitType = value; }
		}

		public string HighestDamageAgainstTarget
		{
			get {return _HigestDamageTarget; }
			set { _HigestDamageTarget = value; }
		}

		public double TotalDamage
		{
			get { return this._TotalDamage; }
			set { this._TotalDamage = value; }
		}

		public double TotalMissed
		{
			get { return this._TotalMissed; }
			set { this._TotalMissed = value; }
		}

		public double TotalShots
		{
			get { return this._TotalShots; }
			set { this._TotalShots = value; }
		}

		public double AverageDamage
		{
			get { return this._TotalDamage / this._TotalShots; }
		}

		public string WeaponName
		{
			get { return this._WeaponName; }
			set { this._WeaponName = value; }
		}

		public double HigestDamage
		{
			get { return this._HigestDamage; }
			set { this._HigestDamage = value; }
		}

		public double TotalHit
		{
			get { return this._TotalHit; }
			set { this._TotalHit = value; }
		}
	}
}
