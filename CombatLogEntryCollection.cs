using System;
using System.Collections;
using System.Diagnostics;
    
namespace CombatLog
{
    /// <summary>
    /// Strongly typed collection of CombatLog.CombatLogEntry.
    /// </summary>
    public class CombatLogEntryCollection : System.Collections.CollectionBase
    {
        public Hashtable WeaponData = new Hashtable();

		public DateTime LogEntriesStartDTM
		{
			get { return GetStartDTM(); }
		}

		public DateTime LogEntriesEndDTM
		{
			get { return GetEndDTM(); }
		}

		private DateTime GetStartDTM()
		{
			DateTime StartDTM = new DateTime(2038,12,30);

			foreach ( CombatLogEntry c in this.List )
				if ( c.LogDTM < StartDTM )
					StartDTM = c.LogDTM;

			return StartDTM;
		}

		private DateTime GetEndDTM()
		{
			DateTime EndDTM = new DateTime(1900,1,1);

			foreach ( CombatLogEntry c in this.List )
				if ( c.LogDTM > EndDTM )
					EndDTM = c.LogDTM;

			return EndDTM;

		}
        /// <summary>
        /// Default constructor.
        /// </summary>
        public CombatLogEntryCollection() : 
                base()
        {
        }
        
		public WeaponSummaryData.WeaponStatsCollection GetWeaponStatsSummary()
		{
			Hashtable Weapon = new Hashtable();

			WeaponSummaryData.WeaponStatsCollection WSCollection = new WeaponSummaryData.WeaponStatsCollection();

			foreach ( CombatLogEntry c in this.List )
			{
				if ( !Weapon.ContainsKey(c.WeaponName) )
				{
					WeaponSummaryData.WeaponStats w = new WeaponSummaryData.WeaponStats();
					w.WeaponName = c.WeaponName;
					Weapon.Add(c.WeaponName, w);
				}

				WeaponSummaryData.WeaponStats WS = (WeaponSummaryData.WeaponStats)Weapon[c.WeaponName];

				WS.TotalDamage	+= c.DamageCaused;
				WS.TotalShots	+= 1;
				
				if( c.Missed )
					WS.TotalMissed++;
				else
					WS.TotalHit++;

				if ( c.DamageCaused > WS.HigestDamage )
				{
					WS.HigestDamage = c.DamageCaused;
					WS.HighestDamageAgainstTarget = c.TargetName;
					WS.HighestDamagePositionInFile = c.PositionInFile;
					WS.HighestDamageHitType = c.HitDescription;
					WS.HighestDamageDTM = c.LogDTM;
				}
			}

			foreach ( object key in Weapon.Keys )
			{
				WeaponSummaryData.WeaponStats w = (WeaponSummaryData.WeaponStats)Weapon[key];
				WSCollection.Add(w);
			}

			return WSCollection;

		}

		/// <summary>
		/// Itterate through each CombatLogEntry creating a collection of WeaponData entries
		/// each containing a WeaponSummary object
		/// </summary>
		public void GenerateWeaponStats()
		{
			WeaponData.Clear();

			foreach ( CombatLogEntry c in this.List )
			{
				if ( !c.IsNotifyMessage )
				{
					WeaponSummary ws;

					//
					// Do we have a WeaponData entry for this weapon?
					//
					if ( !WeaponData.ContainsKey(c.WeaponName) )
					{
						ws = new WeaponSummary();
						ws.WeaponName = c.WeaponName;
						WeaponData.Add(ws.WeaponName, (object)ws);
					}

					//
					// Get a copy of the WeaponData entry for this weapon
					//
					ws = (WeaponSummary)WeaponData[c.WeaponName];
					ws.ShotsFired += 1;
					ws.TotalDamage += c.DamageCaused;

					//
					// Record the start and end times for this weapon being active
					//
					if ( c.LogDTM < ws.FiringStartedDTM )
						ws.FiringStartedDTM = c.LogDTM;

					if ( c.LogDTM > ws.FiringEndedDTM )
						ws.FiringEndedDTM = c.LogDTM;

					if ( c.DamageCaused == 0 )
						((WeaponSummary)WeaponData[c.WeaponName]).ShotsMissed +=1;
					else
						((WeaponSummary)WeaponData[c.WeaponName]).ShotsHit += 1;

					//
					// Do we have an entry for this HitType on this weapon?
					//
					if ( !ws.HitSummary.Contains(c.HitDescription) )
					{
						// Create a new instance of this hittype
						ws.HitSummary.Add(new HitTypeInfo(c.HitDescription, 0,0));
					}

					HitTypeInfo ht = ws.HitSummary[c.HitDescription];

					ht.DisplayName = HitTypeLib.GetDisplayString(c.HitDescription);
					ht.HitCount += 1;
					ht.DamageCaused += c.DamageCaused;
					ht.Rank = HitTypeLib.GetRank(c.HitDescription);
				}
			}
		}

		public string[] GetUniqueWeaponsList()
		{
			Hashtable weps = new Hashtable();

			foreach ( CombatLogEntry c in this.List )
			{
				if ( !c.IsNotifyMessage )
				{
					// Ignore exception thrown where the weapon already exists in the array
					try
					{
						weps.Add(c.WeaponName, "foo");
					}
					catch
					{
					}
				}
			}

			if ( weps.Count == 0 )
				return new string[] {"None"};

			string[] UniqueWeapons = new string[weps.Count];

			int i = 0;
			foreach ( object w in weps.Keys )
				UniqueWeapons[i++] = w.ToString();

			return UniqueWeapons;
		}

		public string[] GetUniqueHitTypes()
		{
			Hashtable hits = new Hashtable();

			foreach ( CombatLogEntry c in this.List )
			{
				try
				{
					hits.Add(c.HitDescription.ToString(), "foo");
				}
				catch
				{
				}
			}

			if ( hits.Count == 0 )
				return new string[] {"None"};

			string[] UniqueHitTypes = new string[hits.Count];

			int i = 0;
			foreach ( object h in hits.Keys )
				UniqueHitTypes[i++] = h.ToString();

			return UniqueHitTypes;
		}

		public string[] GetUniqueTargets()
		{
			Hashtable targets = new Hashtable();

			foreach ( CombatLogEntry c in this.List )
			{
				try
				{
					if ( c.TargetName.IndexOf("completely") != -1 )
						c.TargetName = c.TargetName.Substring(0, c.TargetName.IndexOf("completely")-1);

					targets.Add(c.TargetName, "foo");
				}
				catch
				{
				}
			}

			if ( targets.Count == 0 )
				return new string[] {"None"};

			string[] UniqueTargets = new string[targets.Count];

			int i = 0;
			foreach ( object h in targets.Keys )
				UniqueTargets[i++] = h.ToString();

			return UniqueTargets;
		}

        /// <summary>
        /// Gets or sets the value of the CombatLog.CombatLogEntry at a specific position in the CombatLogEntryCollection.
        /// </summary>
        public CombatLog.CombatLogEntry this[int index]
        {
            get
            {
                return ((CombatLog.CombatLogEntry)(this.List[index]));
            }
            set
            {
                this.List[index] = value;
            }
        }

		public CombatLogEntryCollection FilterByWeapon(string WeaponName, bool IncludeNotifyMessages)
		{
			string[] SelectedWeapons = WeaponName.Split(';');

			CombatLogEntryCollection cc = new CombatLogEntryCollection();

			foreach ( CombatLogEntry c in this.List )
			{
				if ( IncludeNotifyMessages && c.IsNotifyMessage )
					cc.Add(c);
				else
				{
					foreach ( string wn in SelectedWeapons )
					{
						if (c.WeaponName == wn.Trim() )
							cc.Add(c);
					}
				}
			}

			return cc;
		}

		public CombatLogEntryCollection FilterByHitType(string HitType, bool IncludeNotifyMessages)
		{
			string[] Entries = HitType.Split(';');

			CombatLogEntryCollection cc = new CombatLogEntryCollection();

			foreach ( CombatLogEntry c in this.List )
			{
				if ( IncludeNotifyMessages && c.IsNotifyMessage )
					cc.Add(c);
				else
				{
					foreach ( string s in Entries )
					{
						if ( c.HitType.ToString() == s.Trim() )
							cc.Add(c);
					}
				}
			}

			return cc;
		}

		public CombatLogEntryCollection FilterByTarget(string TargetName, bool IncludeNotifyMessages)
		{
			string[] Entries = TargetName.Split(';');

			CombatLogEntryCollection cc = new CombatLogEntryCollection();

			foreach ( CombatLogEntry c in this.List )
			{
				if ( IncludeNotifyMessages && c.IsNotifyMessage )
					cc.Add(c);
				else
				{
					foreach (string s in Entries )
						if (c.TargetName == s.Trim() )
							cc.Add(c);
				}
			}

			return cc;
		}
        
        /// <summary>
        /// Append a CombatLog.CombatLogEntry entry to this collection.
        /// </summary>
        /// <param name="value">CombatLog.CombatLogEntry instance.</param>
        /// <returns>The position into which the new element was inserted.</returns>
        public int Add(CombatLog.CombatLogEntry value)
        {
            return this.List.Add(value);
        }
        
        /// <summary>
        /// Determines whether a specified CombatLog.CombatLogEntry instance is in this collection.
        /// </summary>
        /// <param name="value">CombatLog.CombatLogEntry instance to search for.</param>
        /// <returns>True if the CombatLog.CombatLogEntry instance is in the collection; otherwise false.</returns>
        public bool Contains(CombatLog.CombatLogEntry value)
        {
            return this.List.Contains(value);
        }
        
        /// <summary>
        /// Retrieve the index a specified CombatLog.CombatLogEntry instance is in this collection.
        /// </summary>
        /// <param name="value">CombatLog.CombatLogEntry instance to find.</param>
        /// <returns>The zero-based index of the specified CombatLog.CombatLogEntry instance. If the object is not found, the return value is -1.</returns>
        public int IndexOf(CombatLog.CombatLogEntry value)
        {
            return this.List.IndexOf(value);
        }
        
        /// <summary>
        /// Removes a specified CombatLog.CombatLogEntry instance from this collection.
        /// </summary>
        /// <param name="value">The CombatLog.CombatLogEntry instance to remove.</param>
        public void Remove(CombatLog.CombatLogEntry value)
        {
            this.List.Remove(value);
        }
        
        /// <summary>
        /// Returns an enumerator that can iterate through the CombatLog.CombatLogEntry instance.
        /// </summary>
        /// <returns>An CombatLog.CombatLogEntry's enumerator.</returns>
        public new CombatLogEntryCollectionEnumerator GetEnumerator()
        {
            return new CombatLogEntryCollectionEnumerator(this);
        }
        
        /// <summary>
        /// Insert a CombatLog.CombatLogEntry instance into this collection at a specified index.
        /// </summary>
        /// <param name="index">Zero-based index.</param>
        /// <param name="value">The CombatLog.CombatLogEntry instance to insert.</param>
        public void Insert(int index, CombatLog.CombatLogEntry value)
        {
            this.List.Insert(index, value);
        }
        
        /// <summary>
        /// Strongly typed enumerator of CombatLog.CombatLogEntry.
        /// </summary>
        public class CombatLogEntryCollectionEnumerator : object, System.Collections.IEnumerator
        {
            
            /// <summary>
            /// Current index
            /// </summary>
            private int _index;
            
            /// <summary>
            /// Current element pointed to.
            /// </summary>
            private CombatLog.CombatLogEntry _currentElement;
            
            /// <summary>
            /// Collection to enumerate.
            /// </summary>
            private CombatLogEntryCollection _collection;
            
            /// <summary>
            /// Default constructor for enumerator.
            /// </summary>
            /// <param name="collection">Instance of the collection to enumerate.</param>
            internal CombatLogEntryCollectionEnumerator(CombatLogEntryCollection collection)
            {
                _index = -1;
                _collection = collection;
            }
            
            /// <summary>
            /// Gets the CombatLog.CombatLogEntry object in the enumerated CombatLogEntryCollection currently indexed by this instance.
            /// </summary>
            public CombatLog.CombatLogEntry Current
            {
                get
                {
                    if (((_index == -1) 
                                || (_index >= _collection.Count)))
                    {
                        throw new System.IndexOutOfRangeException("Enumerator not started.");
                    }
                    else
                    {
                        return _currentElement;
                    }
                }
            }
            
            /// <summary>
            /// Gets the current element in the collection.
            /// </summary>
            object IEnumerator.Current
            {
                get
                {
                    if (((_index == -1) 
                                || (_index >= _collection.Count)))
                    {
                        throw new System.IndexOutOfRangeException("Enumerator not started.");
                    }
                    else
                    {
                        return _currentElement;
                    }
                }
            }
            
            /// <summary>
            /// Reset the cursor, so it points to the beginning of the enumerator.
            /// </summary>
            public void Reset()
            {
                _index = -1;
                _currentElement = null;
            }
            
            /// <summary>
            /// Advances the enumerator to the next queue of the enumeration, if one is currently available.
            /// </summary>
            /// <returns>true, if the enumerator was succesfully advanced to the next queue; false, if the enumerator has reached the end of the enumeration.</returns>
            public bool MoveNext()
            {
                if ((_index 
                            < (_collection.Count - 1)))
                {
                    _index = (_index + 1);
                    _currentElement = this._collection[_index];
                    return true;
                }
                _index = _collection.Count;
                return false;
            }
        }
    }
}
