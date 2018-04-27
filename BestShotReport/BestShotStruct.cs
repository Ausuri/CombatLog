using System;

namespace CombatLog.BestShotReport
{
	/// <summary>
	/// Summary description for BestShotStruct.
	/// </summary>

	public struct BestShotRecord
	{
		public string WeaponName;
		public double Damage;
		public DateTime ShotDTM;
		public string HitType;
		public long PositionInFile;
		public string Against;
		public CombatLogCache CacheEntry;
	}
}
