using System;

namespace CombatLog
{
	/// <summary>
	/// Summary description for HitTypeInfo.
	/// </summary>
	[Serializable()]
	public class HitTypeInfo
	{
		private string	_DisplayName;
		private string	_HitTypeString;
		private int		_HitCount;
		private double	_DamageCaused;
		private int		_Rank;
		private float	_DamagePercentage;
		private float	_HitPercentage;

		public HitTypeInfo()
		{
		}

		public HitTypeInfo(string HitTypeString, int HitCount, double DamageCaused)
		{
			_HitTypeString = HitTypeString;
			_HitCount = HitCount;
			_DamageCaused = DamageCaused;
		}

		public float DamagePercentage
		{
			get { return this._DamagePercentage; }
			set { this._DamagePercentage = value; }
		}

		public float HitPercentage
		{
			get { return this._HitPercentage; }
			set { this._HitPercentage = value; }
		}

		public string DisplayName
		{
			get { return this._DisplayName; }
			set { this._DisplayName = value; }
		}

		public string HitTypeString
		{
			get { return this._HitTypeString; }
			set { this._HitTypeString = value; }
		}

		public int HitCount
		{
			get { return _HitCount; }
			set { _HitCount = value; }
		}

		public double DamageCaused
		{
			get { return _DamageCaused; }
			set { _DamageCaused = value; }
		}

		public int Rank
		{
			get { return this._Rank; }
			set { this._Rank = value; }
		}
	}
}
