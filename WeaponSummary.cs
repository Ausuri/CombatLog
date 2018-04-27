using System;
using System.Diagnostics;
using System.Collections;
using System.Xml.Serialization;

namespace CombatLog
{
	/// <summary>
	/// Summary description for WeaponSummary.
	/// </summary>
	[Serializable()]
	public class WeaponSummary
	{
		string	_WeaponName;
		int		_ShotsFired;
		int		_ShotsHit;
		int		_ShotsMissed;
		double	_TotalDamage;
		double	_AverageDamage;
		float	_PercentageShotsHit;
		float	_PercentageShotsMissed;
		DateTime _FiringStartedDTM	= new DateTime(9999,1,1);
		DateTime _FiringEndedDTM	= new DateTime(1,1,1);

		// private Hashtable _HitSummary = new Hashtable();
		private HitTypeInfoCollection _HitSummary = new HitTypeInfoCollection();

		public WeaponSummary()
		{
		}

		public HitTypeInfoCollection HitSummary
		{
			get { return _HitSummary; }
			set { _HitSummary = value; }
		}

		public DateTime FiringStartedDTM
		{
			get { return _FiringStartedDTM; }
			set { _FiringStartedDTM = value; }
		}

		public DateTime FiringEndedDTM
		{
			get { return _FiringEndedDTM; }
			set { _FiringEndedDTM = value; }
		}

		public double TotalDamage
		{
			get { return this._TotalDamage; }
			set { this._TotalDamage = value; }
		}

		public float PercentageShotsHit
		{
			get { return (float)(((float)_ShotsHit / (float)_ShotsFired)); }
			set { this._PercentageShotsHit = value; }
		}

		public float PercentageShotsMissed
		{
			get { return (float)(((float)_ShotsMissed / (float)_ShotsFired)); }
			set { this._PercentageShotsMissed = value; }
		}

		public double AverageDamage
		{
			get { return this._TotalDamage / this._ShotsFired; }
			set { this._AverageDamage = value; }
		}

		public string WeaponName
		{
			get { return this._WeaponName; }
			set { this._WeaponName = value; }
		}

		public int ShotsFired
		{
			get { return this._ShotsFired; }
			set { this._ShotsFired = value; }
		}

		public int ShotsHit
		{
			get { return _ShotsHit; }
			set { _ShotsHit = value; }
		}

		public int ShotsMissed
		{
			get { return _ShotsMissed; }
			set { _ShotsMissed = value; }
		}
	}
}
