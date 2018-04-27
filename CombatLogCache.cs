using System;
using System.Collections;

namespace CombatLog
{
	/// <summary>
	/// Summary description for CombatLogCache.
	/// </summary>
	[Serializable()]
	public class CombatLogCache
	{
		private DateTime _CreationTime;
		private long _FileSize;
		private string _FileName;
		private string _LeafName;
		private string _DirAlias;
		private bool _IsCombatLog;
		private bool _FileExists = false;
		private string _UserComment;

		private string _Character;
		private string[] _WeaponsUsed;
		private string[] _TargetsAttacked;
		private string[] _HitTypes;
		private string[] _Attackers;

		private CombatLog.WeaponSummaryData.WeaponStatsCollection _WeaponStats;

		public CombatLogCache()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public CombatLogCache(string FileName, long FileSize, DateTime CreationTime, bool CombatLogBoo)
		{
			_FileName = FileName;
			_FileSize = FileSize;
			_CreationTime = CreationTime;
			_IsCombatLog = CombatLogBoo;
		}

		[System.Xml.Serialization.XmlIgnoreAttribute]
		public bool FileExists
		{
			get { return this._FileExists; }
			set { this._FileExists = value; }
		}

		public string DirAlias
		{
			get { return _DirAlias; }
			set { _DirAlias = value; }
		}

		public WeaponSummaryData.WeaponStatsCollection WeaponStatsSummary
		{
			get { return _WeaponStats; }
			set { _WeaponStats = value; }
		}

		public System.DateTime CreationTime
		{
			get { return this._CreationTime; }
			set { this._CreationTime = value; }
		}

		public string[] Attackers
		{
			get { return this._Attackers; }
			set { this._Attackers = value; }
		}

		public string[] TargetsAttacked
		{
			get { return this._TargetsAttacked; }
			set { this._TargetsAttacked = value; }
		}

		public string[] HitTypes
		{
			get { return this._HitTypes; }
			set { this._HitTypes = value; }
		}

		public string FileName
		{
			get { return this._FileName; }
			set { this._FileName = value; }
		}

		public string[] WeaponsUsed
		{
			get { return this._WeaponsUsed; }
			set { this._WeaponsUsed = value; }
		}

		public string Character
		{
			get { return this._Character; }
			set { this._Character = value; }
		}

		public string LeafName
		{
			get { return this._LeafName; }
			set { this._LeafName = value; }
		}

		public long FileSize
		{
			get { return this._FileSize; }
			set { this._FileSize = value; }
		}

		public bool IsCombatLog
		{
			get { return _IsCombatLog; }
			set { _IsCombatLog = value; }
		}

		public string UserComment
		{
			get { return _UserComment; }
			set { _UserComment = value; }
		}
	}
}
