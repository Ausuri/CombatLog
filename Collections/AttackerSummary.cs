using System;

namespace CombatLog
{
	/// <summary>
	/// Summary description for AttackerSummary.
	/// </summary>
	public class AttackerSummary
	{
		string	_AttackerName;
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

		private HitTypeInfoCollection _HitSummary = new HitTypeInfoCollection();

		public AttackerSummary()
		{
		}

		public HitTypeInfoCollection HitSummary
		{
			get { return _HitSummary; }
			set { _HitSummary = value; }
		}

		public string AttackerName
		{
			get { return this._AttackerName; }
			set { this._AttackerName = value; }
		}

		public string WeaponName
		{
			get { return this._WeaponName; }
			set { this._WeaponName = value; }
		}

		public System.DateTime FiringEndedDTM
		{
			get { return this._FiringEndedDTM; }
			set { this._FiringEndedDTM = value; }
		}

		public double AverageDamage
		{
			get { return this._TotalDamage / this._ShotsFired; }
			set { this._AverageDamage = value; }
		}

		public System.DateTime FiringStartedDTM
		{
			get { return this._FiringStartedDTM; }
			set { this._FiringStartedDTM = value; }
		}

		public int ShotsHit
		{
			get { return this._ShotsHit; }
			set { this._ShotsHit = value; }
		}

		public int ShotsMissed
		{
			get { return this._ShotsMissed; }
			set { this._ShotsMissed = value; }
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

		public double TotalDamage
		{
			get { return this._TotalDamage; }
			set { this._TotalDamage = value; }
		}

		public int ShotsFired
		{
			get { return this._ShotsFired; }
			set { this._ShotsFired = value; }
		}

	}
}
