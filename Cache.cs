using System;

namespace CombatLog
{
	/// <summary>
	/// Summary description for Cache.
	/// </summary>
	public class Cache
	{
		private string _CacheVersion;
		private DateTime _LastUpdated;
		private CombatLogCacheCollection _CacheData;

		public Cache()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public string CacheVersion
		{
			get { return _CacheVersion; }
			set { _CacheVersion = value; }
		}

		public DateTime LastUpdated
		{
			get { return _LastUpdated; }
			set { _LastUpdated = value; }
		}

		public CombatLogCacheCollection CacheData
		{
			get { return _CacheData; }
			set { _CacheData = value; }
		}
	}
}
