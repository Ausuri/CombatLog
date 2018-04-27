using System;

namespace CombatLog.WeaponDataDB
{
	public enum WeaponClass { Micro, Small, Medium, Large, RocketMissile, DefenderMissile, LightMissile, HeavyMissile, CruiseMissile, TorpedoMissile, FOFLightMissile, FOFHeavyMissile, FOFCruiseMissile, LightDrone, MediumDrone, HeavyDrone };
	public enum WeaponType { Hybrid, Laser, Projectile, CombatDrone, Missile, SmartBomb };

	/// <summary>
	/// Summary description for WeaponDataSource.
	/// </summary>
	[Serializable()]
	public class WeaponDataSource : ObjectExplorer.EVEItem
	{
		private WeaponClass _Class;
		private WeaponType _Type;
		private string _SourceUrl;

		public WeaponDataSource()
		{
		}

		public WeaponDataSource(WeaponClass Class, WeaponType Type, string SourceUrl)
		{
			this._Class		= Class;
			this._Type		= Type;
			this._SourceUrl	= SourceUrl;
		}

		public WeaponClass Class
		{
			get { return this._Class; }
			set { this._Class = value; }
		}

		public WeaponType Type
		{
			get { return this._Type; }
			set { this._Type = value; }
		}

		public string SourceUrl
		{
			get { return this._SourceUrl; }
			set { this._SourceUrl = value; }
		}
	}
}
