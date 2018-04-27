using System;
using System.Windows.Forms;
using System.Text;
using System.Diagnostics;

namespace CombatLog.Summary
{
	/// <summary>
	/// Summary description for CombatSummary.
	/// </summary>
	public class CombatSummary
	{
		private GameLog				_ThisGameLog;
		private GameLogCollection	_AllGameLogs;
		private DateTime			_LogDTM;
		private TimeSpan			_CombatDuration;
		private string				_Location;
		private string				_Character;
		private string[]			_MyWeaponsUsed;
		private string[]			_EnemyWeaponsUsed;
		private string[]			_Enemies;
		
		#region Attack Data Variables
		private int		_MyTotalFired			= 0;
		private int		_MyTotalHit				= 0;
		private int		_MyTotalMissed			= 0;
		private float	_MyTotalDamage			= 0.0F;
		private float	_MyAverageDamage		= 0.0F;
		private float	_MyPercentHit			= 0.0F;
		private float	_MyPercentMissed		= 0.0F;
		private double	_MyDamagePerMinute		= 0.0F;
		private double	_MyShotsPerMinute		= 0.0F;
		private string	_MyShotsPerMinuteStr	= "";
		private string	_MyDamagePerMinuteStr	= "";

		private double _MyTotalAverageDamage	= 0.0F;
		private double _MyTotalShotsPerMinute	= 0.0F;
		private string _MyTotalShotsPerMinuteStr = "";

		private double _MyTotalDamagePerMinute = 0.0F;
		private string _MyTotalDurationStr;

		private TimeSpan _MyFireDuration;
		private TimeSpan _MyTotalDuration;

		private string _MyDuration;
		#endregion

		#region Attacker Data Variables
		int		_AttackerTotalFired				= 0;
		int		_AttackerTotalHit				= 0;
		int		_AttackerTotalMissed			= 0;
		float	_AttackerTotalDamage			= 0.0F;
		float	_AttackerAverageDamage			= 0.0F;
		float	_AttackerPercentHit				= 0.0F;
		float	_AttackerPercentMissed			= 0.0F;
		double	_AttackerDamagePerMinute		= 0.0F;
		double	_AttackerShotsPerMinute			= 0.0F;
		string	_AttackerShotsPerMinuteStr		= "";
		string	_AttackerDamagePerMinuteStr		= "";

		double _AttackerTotalAverageDamage		= 0.0F;
		double _AttackerTotalShotsPerMinute		= 0.0F;
		string _AttackerTotalShotsPerMinuteStr	= "";

		double _AttackerTotalDamagePerMinute	= 0.0F;
		string _AttackerTotalDurationStr;

		TimeSpan _AttackerFireDuration;
		string	_AttackerDuration;
		TimeSpan _AttackerTotalDuration;
		#endregion

		public CombatSummary()
		{
		}

		public string GetSummary()
		{
			StringBuilder s = new StringBuilder();
			string nl = Environment.NewLine;

			s.Append("One day this will be a nicely formatted battle summary report. Honest :)" + nl + nl);

			
			CombatLogCache c = AllGameLogs.LogCache.CacheData[ThisGameLog.FileName];

			
			CombatLogEntryCollection cl = _ThisGameLog.GetCombatEntries();

			if ( cl == null )
				return "Not enough data to generate combat summary.";

			Debug.WriteLine("CombatEntries: " + cl.Count);

			AttackEntryCollection AttackerData = _ThisGameLog.GetAttackEntries();
			AttackerData.GenerateAttackerStats();
			Debug.WriteLine("Attacker Entries: " + AttackerData.Count);

			cl.GenerateWeaponStats();
			GenerateMyAttackData(cl);
			GenerateAttackerData(AttackerData);

			this._LogDTM		= _ThisGameLog.SessionStartedDTM;
			this._Location		= GetLocation(cl);
			this._Character		= _ThisGameLog.Listener;
			this._MyWeaponsUsed = cl.GetUniqueWeaponsList();
			this._Enemies		= cl.GetUniqueTargets();

			TimeSpan duration = cl[cl.Count-1].LogDTM.Subtract(cl[0].LogDTM);

			s.Append("Time: " + this._LogDTM.ToString() + nl);
			s.Append("Character: " + this._Character + nl);
			s.Append("Location: " + this._Location + nl);
			s.Append("Duration: " + duration.ToString() + nl);

			s.Append(nl + "My Weapons Used" + nl + nl);
			foreach ( string w in this.MyWeaponsUsed )
			{
				s.Append(w + nl);
			}

			s.Append(nl + "Enemies Attacked" + nl + nl);
			foreach ( string e in this._Enemies )
				s.Append(e + nl);

			s.Append(nl + "Damage Done: " + this._MyTotalDamage.ToString("0.00") + nl);
			s.Append("Damage Received: " + this._AttackerTotalDamage.ToString("0.00") + nl);

			return s.ToString();
		}

		private void GenerateAttackerData(AttackEntryCollection AttackData)
		{
			_AttackerTotalDuration  = AttackData.LogEntriesEndDTM.Subtract(AttackData.LogEntriesStartDTM);

			if ( _AttackerTotalDuration.TotalMinutes < 1 )
				_AttackerTotalDurationStr = _AttackerTotalDuration.TotalSeconds.ToString("0") + " secs";
			else
				_AttackerTotalDurationStr = _AttackerTotalDuration.TotalMinutes.ToString("0") + " mins";

			Debug.WriteLine("GenerateAttackerData: " + AttackData.AttackerData.Count + " keys in AttackerData table");

			foreach ( string AttackerName in AttackData.AttackerData.Keys )
			{
				AttackerSummary ws = (AttackerSummary)AttackData.AttackerData[AttackerName];
		
				try { _AttackerFireDuration = ws.FiringEndedDTM - ws.FiringStartedDTM;}
				catch { _AttackerFireDuration = new TimeSpan(0); }

				if ( _AttackerFireDuration.TotalMinutes > 1 )
				{
					_AttackerDuration			= _AttackerFireDuration.TotalMinutes.ToString("0") + " mins";
					_AttackerDamagePerMinute	= ws.TotalDamage / _AttackerFireDuration.TotalMinutes;
					_AttackerDamagePerMinuteStr	= _AttackerDamagePerMinute.ToString("0.00");
					_AttackerShotsPerMinute		= ws.ShotsFired / _AttackerFireDuration.TotalMinutes;
					_AttackerShotsPerMinuteStr	= _AttackerShotsPerMinute.ToString("0.00");
				}
				else
				{
					_AttackerDuration = _AttackerFireDuration.TotalSeconds.ToString("0") + " secs";
					_AttackerDamagePerMinuteStr = "-";
				}

				_AttackerTotalFired		+= ws.ShotsFired;
				_AttackerTotalHit		+= ws.ShotsHit;
				_AttackerTotalMissed	+= ws.ShotsMissed;
				_AttackerTotalDamage	+= (float)ws.TotalDamage;
				_AttackerAverageDamage	+= (float)ws.AverageDamage;
				_AttackerPercentHit		+= ws.PercentageShotsHit;
				_AttackerPercentMissed	+= ws.PercentageShotsMissed;

				Debug.WriteLine(ws.AttackerName + " - " + ws.TotalDamage);				
			}

			//
			// Calculate totals and create the final row in the list view
			//
			_AttackerPercentHit = (float)_AttackerTotalHit / (float)_AttackerTotalFired;
			_AttackerPercentMissed = (float)_AttackerTotalMissed / (float)_AttackerTotalFired;

			_AttackerTotalDamagePerMinute	= _AttackerTotalDamage / _AttackerTotalDuration.TotalMinutes;

			_AttackerTotalShotsPerMinute	= _AttackerTotalFired / _AttackerTotalDuration.TotalMinutes;
			_AttackerTotalShotsPerMinuteStr = _AttackerTotalShotsPerMinute.ToString("0.00");

			_AttackerTotalAverageDamage		= _AttackerTotalDamage /  _AttackerTotalFired;
		}

		private void GenerateMyAttackData(CombatLogEntryCollection cl)
		{

			_MyTotalDuration  = cl.LogEntriesEndDTM.Subtract(cl.LogEntriesStartDTM);

			if ( _MyTotalDuration.TotalMinutes < 1 )
				_MyTotalDurationStr = _MyTotalDuration.TotalSeconds.ToString("0") + " secs";
			else
				_MyTotalDurationStr = _MyTotalDuration.TotalMinutes.ToString("0") + " mins";

			foreach ( string WeaponName in cl.WeaponData.Keys )
			{
				WeaponSummary ws = (WeaponSummary)cl.WeaponData[WeaponName];
		
				try { _MyFireDuration = ws.FiringEndedDTM - ws.FiringStartedDTM;}
				catch { _MyFireDuration = new TimeSpan(0); }

				if ( _MyFireDuration.TotalMinutes > 1 )
				{
					_MyDuration				= _MyFireDuration.TotalMinutes.ToString("0") + " mins";
					_MyDamagePerMinute		= ws.TotalDamage / _MyFireDuration.TotalMinutes;
					_MyDamagePerMinuteStr	= _MyDamagePerMinute.ToString("0.00");
					_MyShotsPerMinute		= ws.ShotsFired / _MyFireDuration.TotalMinutes;
					_MyShotsPerMinuteStr	= _MyShotsPerMinute.ToString("0.00");
				}
				else
				{
					_MyDuration = _MyFireDuration.TotalSeconds.ToString("0") + " secs";
					_MyDamagePerMinuteStr = "-";
				}

				_MyTotalFired		+= ws.ShotsFired;
				_MyTotalHit			+= ws.ShotsHit;
				_MyTotalMissed		+= ws.ShotsMissed;
				_MyTotalDamage		+= (float)ws.TotalDamage;
				_MyAverageDamage	+= (float)ws.AverageDamage;
				_MyPercentHit		+= ws.PercentageShotsHit;
				_MyPercentMissed	+= ws.PercentageShotsMissed;
			}

			//
			// Calculate totals and create the final row in the list view
			//
			_MyPercentHit = (float)_MyTotalHit / (float)_MyTotalFired;
			_MyPercentMissed = (float)_MyTotalMissed / (float)_MyTotalFired;

			_MyTotalDamagePerMinute = _MyTotalDamage / _MyTotalDuration.TotalMinutes;

			_MyTotalShotsPerMinute	= _MyTotalFired / _MyTotalDuration.TotalMinutes;
			_MyTotalShotsPerMinuteStr = _MyTotalShotsPerMinute.ToString("0.00");

			_MyTotalAverageDamage = _MyTotalDamage /  _MyTotalFired;
		}

		private string GetLocation(CombatLogEntryCollection cl)
		{
			LocationInfo.LocationCollection locs = AllGameLogs.GetLocations(ThisGameLog, cl[0].LogDTM);

			if ( locs == null ) // No location data, bail out!
			{
				return "";
			}
			
			locs.SortByLogDTM(SortOrder.Ascending);

			// TODO: Fix issues with location data. See file : Size 12437 20040630_001219.txt
			string lastLocation = "";
			foreach ( LocationInfo.Location l in locs )
			{
				if ( l.LogDTM < cl[0].LogDTM )
					lastLocation = l.LocationStr;
			}

			return lastLocation;
		}

		public string Character
		{
			get { return this._Character; }
			set { this._Character = value; }
		}

		public System.DateTime LogDTM
		{
			get { return this._LogDTM; }
			set { this._LogDTM = value; }
		}

		public System.TimeSpan CombatDuration
		{
			get { return this._CombatDuration; }
			set { this._CombatDuration = value; }
		}

		public string[] EnemyWeaponsUsed
		{
			get { return this._EnemyWeaponsUsed; }
			set { this._EnemyWeaponsUsed = value; }
		}

		public string Location
		{
			get { return this._Location; }
			set { this._Location = value; }
		}

		public string[] MyWeaponsUsed
		{
			get { return this._MyWeaponsUsed; }
			set { this._MyWeaponsUsed = value; }
		}

		public string[] Enemies
		{
			get { return this._Enemies; }
			set { this._Enemies = value; }
		}

		public CombatLog.GameLog ThisGameLog
		{
			get { return this._ThisGameLog; }
			set { this._ThisGameLog = value; }
		}

		public CombatLog.GameLogCollection AllGameLogs
		{
			get { return this._AllGameLogs; }
			set { this._AllGameLogs = value; }
		}
	}
}
