namespace CombatLog
{
    using System;
    using System.Collections;
	using System.Diagnostics;
    
    
    /// <summary>
    /// Strongly typed collection of CombatLog.CombatLogCache.
    /// </summary>
	[Serializable()]
    public class CombatLogCacheCollection : System.Collections.CollectionBase
    {
		private WeaponDataDB.WeaponDataSourceCollection _WeaponData = null;

		public Hashtable FileNameIX			= null;
		private Hashtable UniqueLogDirs		= null;
		private Hashtable UniqueCharacters	= null;
		private Hashtable LogDirsIX			= null;
		private Hashtable CharactersIX		= null;
		private Hashtable UniqueWeapons		= null;
		private Hashtable UniqueTargets		= null;
		private Hashtable UniqueAttackers	= null;
		private Hashtable WeaponTypeIX		= null;
		private Hashtable WeaponClassIX		= null;
		private Hashtable WeaponIX			= null;
		private Hashtable TargetIX			= null;
		private Hashtable AttackerIX		= null;

		/// <summary>
		/// Default constructor.
		/// </summary>

		private void InitialiseHashtables()
		{
			#region Initialize hashtables
			if ( FileNameIX == null )
				FileNameIX = new Hashtable();

			if ( UniqueLogDirs == null )
				UniqueLogDirs = new Hashtable();

			if ( UniqueCharacters == null )
				UniqueCharacters = new Hashtable();

			if ( LogDirsIX == null )
				LogDirsIX = new Hashtable();

			if ( CharactersIX == null )
				CharactersIX = new Hashtable();

			if ( UniqueTargets == null )
				UniqueTargets = new Hashtable();

			if ( WeaponTypeIX == null )
				WeaponTypeIX = new Hashtable();

			if ( WeaponClassIX == null )
				WeaponClassIX = new Hashtable();

			if( UniqueWeapons == null )
				UniqueWeapons = new Hashtable();

			if ( UniqueAttackers == null )
				UniqueAttackers = new Hashtable();

			if ( WeaponIX == null )
				WeaponIX = new Hashtable();

			if ( TargetIX == null )
				TargetIX = new Hashtable();

			if ( AttackerIX == null )
				AttackerIX = new Hashtable();

			#endregion
		}
		
		public CombatLogCacheCollection() : base()
		{
			InitialiseHashtables();
		}

		public CombatLogCacheCollection(WeaponDataDB.WeaponDataSourceCollection WeaponData)
		{
			this._WeaponData = WeaponData;
			InitialiseHashtables();
		}

		private void ClearIndexes()
		{
			if ( FileNameIX != null )
				FileNameIX.Clear();

			if ( UniqueLogDirs != null )
				UniqueLogDirs.Clear();

			if ( UniqueCharacters != null )
				UniqueCharacters.Clear();

			if ( LogDirsIX != null )
				LogDirsIX.Clear();

			if ( CharactersIX != null )
				CharactersIX.Clear();

			if ( UniqueTargets != null )
				UniqueTargets.Clear();

			if( UniqueWeapons != null )
				UniqueWeapons.Clear();

			if ( WeaponTypeIX != null )
				WeaponTypeIX.Clear();

			if ( WeaponClassIX != null )
				WeaponClassIX.Clear();

			if ( WeaponIX != null )
				WeaponIX.Clear();

			if ( TargetIX != null )
				TargetIX.Clear();

			if ( UniqueAttackers != null )
				UniqueAttackers.Clear();

			if ( AttackerIX != null )
				AttackerIX.Clear();
		}
        

		public void SetWeaponData(WeaponDataDB.WeaponDataSourceCollection WeaponData)
		{
			this._WeaponData = WeaponData;
		}

		public void CreateWeaponTypeIndexes()
		{
			#region WeaponType Index and Unique list
			// Does this log cache contain a list of Weapons Used?

			foreach ( CombatLogCache g in this.List )
			{
				if ( g.WeaponsUsed != null && g.WeaponsUsed.Length > 0 )
				{
					// Iterate through each weapon name
					foreach ( string s in g.WeaponsUsed )
					{
						ArrayList WeaponsByType;

						//
						// Get weapon type information for this weapon name
						//
						WeaponDataDB.WeaponDataSource WeaponInfo = _WeaponData[s];

						if ( WeaponInfo != null )
						{
							// Does the Index for this type contain any weapon data entries?
							// If not, create a new arraylist for this type
							if ( !WeaponTypeIX.Contains(WeaponInfo.Type) )
							{
								WeaponTypeIX.Add(WeaponInfo.Type, new ArrayList());
							}

							// Retrieve the arraylist for this type
							WeaponsByType = (ArrayList)WeaponTypeIX[WeaponInfo.Type];

							// Add this combat log entry
							if ( !WeaponsByType.Contains(g) )
								WeaponsByType.Add(g);
						}
					}
				}
			}
			#endregion

			#region Weapon Class Index
			foreach ( CombatLogCache g in this.List )
			{
				if ( g.WeaponsUsed != null && g.WeaponsUsed.Length > 0 )
				{
					// Iterate through each weapon name
					foreach ( string s in g.WeaponsUsed )
					{
						ArrayList WeaponsByClass;

						//
						// Get weapon type information for this weapon name
						//
						WeaponDataDB.WeaponDataSource WeaponInfo = _WeaponData[s];

						if ( WeaponInfo != null )
						{
							// Does the Index for this type contain any weapon data entries?
							// If not, create a new arraylist for this type
							if ( !WeaponClassIX.Contains(WeaponInfo.Class) )
							{
								WeaponClassIX.Add(WeaponInfo.Class, new ArrayList());
							}

							// Retrieve the arraylist for this type
							WeaponsByClass = (ArrayList)WeaponClassIX[WeaponInfo.Class];

							// Add this combat log entry
							if ( !WeaponsByClass.Contains(g) )
							{
								WeaponsByClass.Add(g);
							}

						}
					}
				}
			}
			#endregion
		}
		private void IndexThis(string FileName, CombatLogCache g)
		{
			if ( g == null )
				return;

			#region Attackers Index and Unique list
			if ( g.Attackers != null && g.TargetsAttacked.Length > 0 )
			{
				foreach (string s in g.Attackers )
				{
					if ( !UniqueAttackers.ContainsKey(s) )
						UniqueAttackers.Add(s,"a");

					if ( !AttackerIX.ContainsKey(s) )
						AttackerIX.Add(s, new ArrayList());

					ArrayList Attackers = (ArrayList)AttackerIX[s];
					if ( !Attackers.Contains(g) )
						Attackers.Add(g);
				}
			}
			#endregion

			#region Targets Index and Unique list
			if ( g.TargetsAttacked != null && g.TargetsAttacked.Length > 0 )
			{
				foreach ( string s in g.TargetsAttacked )
				{
					if ( !UniqueTargets.ContainsKey(s) )
						UniqueTargets.Add(s, "t");

					if ( !TargetIX.ContainsKey(s) )
						TargetIX.Add(s, new ArrayList());

					ArrayList Targets = (ArrayList)TargetIX[s];
					Targets.Add(g);
				}
			}
			#endregion

			#region WeaponsUsed Index and Unique list
			if ( g.WeaponsUsed != null && g.WeaponsUsed.Length > 0 )
			{
				foreach (string s in g.WeaponsUsed )
				{
					if ( !UniqueWeapons.ContainsKey(s) )
						UniqueWeapons.Add(s, "w");

					if ( !WeaponIX.ContainsKey(s) )
						WeaponIX.Add(s, new ArrayList());

					ArrayList Weapons = (ArrayList)WeaponIX[s];
					Weapons.Add(g);
				}
			}
			#endregion

			#region Characters Index and Unique list
			if ( g.Character != null )
			{
				if ( !CharactersIX.ContainsKey(g.Character) )
				{
					ArrayList chars = new ArrayList();
					CharactersIX.Add(g.Character, chars);
				}

				ArrayList CharacterList = (ArrayList)CharactersIX[g.Character];
				CharacterList.Add(g);
			}
			#endregion

			#region Directory Alias Index and Unique list
			if ( g.DirAlias != null )
			{
				if ( !LogDirsIX.Contains(g.DirAlias) )
				{
					ArrayList logDirs = new ArrayList();
					LogDirsIX.Add(g.DirAlias, logDirs);
				}

				ArrayList LogDirList = (ArrayList)LogDirsIX[g.DirAlias];
				LogDirList.Add(g);
			}

			if ( g.Character != null && !UniqueCharacters.ContainsKey(g.Character) )
				UniqueCharacters.Add(g.Character, "bar");

			if ( g.DirAlias != null && !UniqueLogDirs.ContainsKey(g.DirAlias) )
				UniqueLogDirs.Add(g.DirAlias, "bas");

			#endregion

			try
			{
				if ( FileName.ToLower().IndexOf("eeyore") != -1 )
				{
					Debug.WriteLine("IFX: " + FileName.ToLower());
					Debug.WriteLine("IFX: g = " + g.FileName);
					Debug.WriteLine("IFX: char = " + g.Character);
					Debug.WriteLine("IFX: dir = " + g.DirAlias);


					if ( g == null )
						Debug.WriteLine("IFX: " + FileName.ToLower() + " is null");
				}

				FileNameIX.Add(FileName.ToLower(), g);
			}
			catch (Exception e)
			{
				Debug.WriteLine("INDEX: " + e.ToString());
				// If the entry already exists, no harm done, just exit quietly
			}
		}

		private void RemoveFromIndexes(string FileName, CombatLogCache g)
		{
			if ( g == null )
				return;

			#region Attackers Index and Unique list
			if ( g.Attackers != null && g.TargetsAttacked.Length > 0 )
			{
				foreach (string s in g.Attackers )
				{
					if ( UniqueAttackers.ContainsKey(s) )
						UniqueAttackers.Remove(s);

					if ( AttackerIX.ContainsKey(s) )
					{
						ArrayList Attackers = (ArrayList)AttackerIX[s];
						if ( Attackers.Contains(g) )
							Attackers.Remove(g);
					}
				}
			}
			#endregion

			#region Targets Index and Unique list
			if ( g.TargetsAttacked != null && g.TargetsAttacked.Length > 0 )
			{
				foreach ( string s in g.TargetsAttacked )
				{
					if ( UniqueTargets.ContainsKey(s) )
						UniqueTargets.Remove(s);

					if ( TargetIX.ContainsKey(s) )
					{
						ArrayList Targets = (ArrayList)TargetIX[s];
						Targets.Remove(g);
					}
				}
			}
			#endregion

			#region WeaponsUsed Index and Unique list
			if ( g.WeaponsUsed != null && g.WeaponsUsed.Length > 0 )
			{
				foreach (string s in g.WeaponsUsed )
				{
					if ( UniqueWeapons.ContainsKey(s) )
						UniqueWeapons.Remove(s);

					if ( WeaponIX.ContainsKey(s) )
					{
						ArrayList Weapons = (ArrayList)WeaponIX[s];
						Weapons.Remove(g);
					}
				}
			}
			#endregion

			#region Characters Index and Unique list
			if ( g.Character != null )
			{
				if ( CharactersIX.ContainsKey(g.Character) )
				{
					ArrayList CharacterList = (ArrayList)CharactersIX[g.Character];
					CharacterList.Remove(g);
				}
			}
			#endregion

			#region Directory Alias Index and Unique list
			if ( g.DirAlias != null )
			{
				if ( LogDirsIX.Contains(g.DirAlias) )
				{
					ArrayList LogDirList = (ArrayList)LogDirsIX[g.DirAlias];
					LogDirList.Remove(g);
				}
			}

			if ( g.Character != null && UniqueCharacters.ContainsKey(g.Character) )
				UniqueCharacters.Remove(g.Character);

			if ( g.DirAlias != null && UniqueLogDirs.ContainsKey(g.DirAlias) )
				UniqueLogDirs.Remove(g.DirAlias);

			#endregion

			if ( FileNameIX.ContainsKey(FileName.ToLower()) )
				FileNameIX.Remove(FileName.ToLower());
		
		}

		
		public CombatLogCacheCollection FilterByAge(int Days)
		{
			CombatLogCacheCollection cc = new CombatLogCacheCollection(_WeaponData);

			foreach ( CombatLogCache c in this.List )
			{
				TimeSpan t = new TimeSpan(Days,0,0,0);

				if ( c.CreationTime >= DateTime.Now.Subtract(t) )
					cc.Add(c);
			}

			cc.CreateWeaponTypeIndexes();
			return cc;
		}

		public CombatLogCacheCollection FilterByLogDir(string LogDirName)
		{
			if ( LogDirsIX != null )
			{
				if ( LogDirsIX.Contains(LogDirName) )
				{
					ArrayList LogDirList = (ArrayList)LogDirsIX[LogDirName];

					CombatLogCacheCollection logdirs = new CombatLogCacheCollection(_WeaponData);

					foreach ( object o in LogDirList )
					{
						logdirs.Add((CombatLogCache)o);
					}

					logdirs.CreateWeaponTypeIndexes();
					return logdirs;
				}
			}

			return new CombatLogCacheCollection(_WeaponData);
		}

		public CombatLogCacheCollection FilterByCharacter(string CharacterName)
		{
			if ( CharactersIX != null )
			{
				if ( CharactersIX.ContainsKey(CharacterName) )
				{
					ArrayList CharacterList = (ArrayList)CharactersIX[CharacterName];

					CombatLogCacheCollection Chars = new CombatLogCacheCollection(_WeaponData);

					foreach ( object o in CharacterList )
						Chars.Add((CombatLogCache)o);

					Chars.CreateWeaponTypeIndexes();
					return Chars;
				}
			}

			return new CombatLogCacheCollection(_WeaponData);
		}

		public CombatLogCacheCollection FilterByWeaponType(WeaponDataDB.WeaponTypeObj WeaponType)
		{
			if ( WeaponTypeIX != null )
			{
				// Does the index contain any entries for weapons of this type?
				if ( WeaponTypeIX.Contains(WeaponType.Type) )
				{
					ArrayList Weapons = (ArrayList)WeaponTypeIX[WeaponType.Type];

					CombatLogCacheCollection Results = new CombatLogCacheCollection(_WeaponData);

					foreach ( object o in Weapons )
					{
						Results.Add((CombatLogCache)o);
					}

					Results.CreateWeaponTypeIndexes();

					return Results;
				}
				else
					Debug.WriteLine("WeaponType Index has no entries for " + WeaponType.Type);
			}
			else
				Debug.WriteLine("WeaponTypeIX = null");

			return null;
		}


		public CombatLogCacheCollection FilterByWeaponClass(WeaponDataDB.WeaponClassObj WeaponClass)
		{
			if ( WeaponClassIX != null )
			{
				// Does the index contain any entries for weapons of this type?
				if ( WeaponClassIX.Contains(WeaponClass.Class) )
				{
					ArrayList Weapons = (ArrayList)WeaponClassIX[WeaponClass.Class];

					CombatLogCacheCollection Results = new CombatLogCacheCollection(_WeaponData);

					foreach ( object o in Weapons )
					{
						Results.Add((CombatLogCache)o);
					}

					Results.CreateWeaponTypeIndexes();
					return Results;
				}
			}
			return null;
		}


		public CombatLogCacheCollection FilterByWeapon(string WeaponName)
		{
			if ( WeaponIX != null )
			{
				if ( WeaponIX.ContainsKey(WeaponName) )
				{
					ArrayList WeaponList = (ArrayList)WeaponIX[WeaponName];

					CombatLogCacheCollection Weapons = new CombatLogCacheCollection(_WeaponData);

					foreach ( object o in WeaponList )
						Weapons.Add((CombatLogCache)o);

					Weapons.CreateWeaponTypeIndexes();
					return Weapons;
				}
			}

			return new CombatLogCacheCollection(_WeaponData);
		}

		public CombatLogCacheCollection FilterByTarget(string TargetName)
		{
			if ( TargetIX != null )
			{
				if ( TargetIX.ContainsKey(TargetName) )
				{
					ArrayList TargetList = (ArrayList)TargetIX[TargetName];

					CombatLogCacheCollection Targets = new CombatLogCacheCollection(_WeaponData);

					foreach ( object o in TargetList )
						Targets.Add((CombatLogCache)o);

					Targets.CreateWeaponTypeIndexes();
					return Targets;
				}
			}

			return new CombatLogCacheCollection(_WeaponData);
		}

		public CombatLogCacheCollection FilterByAttacker(string AttackerName)
		{
			if ( AttackerIX != null )
			{
				if ( AttackerIX.ContainsKey(AttackerName) )
				{
					ArrayList AttackerList = (ArrayList)AttackerIX[AttackerName];

					CombatLogCacheCollection Attackers = new CombatLogCacheCollection(_WeaponData);

					foreach ( object o in AttackerList )
						Attackers.Add((CombatLogCache)o);

					Attackers.CreateWeaponTypeIndexes();
					return Attackers;
				}
			}

			return new CombatLogCacheCollection(_WeaponData);
		}

		public bool FileCached(string FileName)
		{
			// Debug.WriteLine("Checking for existence of '" + FileName + "' in file index (" + this.FileNameIX.Count.ToString() + " index entries)");

			try
			{
				if ( this.FileNameIX.ContainsKey(FileName.ToLower()) )
					return true;
			}
			catch
			{
			}

			return false;
		}

		public bool IsCombatLog(string FileName)
		{
			CombatLogCache c  = (CombatLogCache)FileNameIX[FileName.ToLower()];
			return c.IsCombatLog;
		}

 
		public void SortBySessionDTM(System.Windows.Forms.SortOrder Direction)
		{
			this.InnerList.Sort(new Sort.CombatLogCacheCollectionSort(Direction));
		}

        /// <summary>
        /// Gets or sets the value of the CombatLog.CombatLogCache at a specific position in the CombatLogCacheCollection.
        /// </summary>
        public CombatLog.CombatLogCache this[int index]
        {
            get
            {
                return ((CombatLog.CombatLogCache)(this.List[index]));
            }
            set
            {
                this.List[index] = value;
            }
        }

		public CombatLog.CombatLogCache this[string FileName]
		{
			get { return (CombatLogCache)FileNameIX[FileName.ToLower()]; }
		}

		public string[] GetUniqueAttackersList()
		{
			if ( UniqueAttackers == null )
				return null;

			string[] Attackers = new string[UniqueAttackers.Count];

			int i = 0;
			foreach ( object o in UniqueAttackers.Keys )
				Attackers[i++] = o.ToString();

			return Attackers;
		}

		public string[] GetUniqueTargetList()
		{
			if ( UniqueTargets == null )
				return null;

			string[] Targets = new string[UniqueTargets.Count];

			int i = 0;
			foreach ( object o in UniqueTargets.Keys )
				Targets[i++] = o.ToString();

			return Targets;
		}

		public string[] GetUniqueWeaponList()
		{
			if ( UniqueWeapons == null )
				return null;

			string[] Weps = new string[UniqueWeapons.Count];

			int i = 0;
			foreach ( object o in UniqueWeapons.Keys )
				Weps[i++] = o.ToString();

			return Weps;
		}


		public WeaponDataDB.WeaponClassObj[] GetUniqueWeaponClassList(WeaponDataDB.WeaponDataSourceCollection WeaponData)
		{
			string[] WeaponsUsed = this.GetUniqueWeaponList();
			Hashtable WeaponClasses = new Hashtable();

			foreach ( string weapon in WeaponsUsed )
			{
				WeaponDataDB.WeaponDataSource wds = _WeaponData[weapon];

				if ( wds != null )
				{
					WeaponDataDB.WeaponClassObj WepClass = WeaponDataDB.Utils.GetWeaponClassObj(wds.Class);

					if ( !WeaponClasses.ContainsKey(WepClass.Class) )
						WeaponClasses.Add(WepClass.Class, WepClass);
				}
			}

			WeaponDataDB.WeaponClassObj[] Results = new WeaponDataDB.WeaponClassObj[WeaponClasses.Count];

			int i = 0;
			foreach ( object o in WeaponClasses.Values )
			{
				try
				{
					Results[i++] = (WeaponDataDB.WeaponClassObj)o;
				}
				catch (Exception err)
				{
					Debug.WriteLine("ERROR: " + err.ToString());
				}
			}

			return Results;

		}
		
		public WeaponDataDB.WeaponTypeObj[] GetUniqueWeaponTypeList(WeaponDataDB.WeaponDataSourceCollection WeaponData)
		{
			string[] WeaponsUsed = this.GetUniqueWeaponList();
			Hashtable WeaponTypes = new Hashtable();

			if ( _WeaponData == null )
			{
				Debug.WriteLine("Weapondata is NULL!!!");
			}

			foreach ( string weapon in WeaponsUsed )
			{
				WeaponDataDB.WeaponDataSource wds = _WeaponData[weapon];

				if ( wds != null )
				{
					WeaponDataDB.WeaponTypeObj WepType = WeaponDataDB.Utils.GetWeaponTypeObj(wds.Type);

					if ( !WeaponTypes.ContainsKey(WepType.Type) )
						WeaponTypes.Add(WepType.Type, WepType);
				}
			}

			WeaponDataDB.WeaponTypeObj[] Results = new WeaponDataDB.WeaponTypeObj[WeaponTypes.Count];

			int i = 0;
			foreach ( object o in WeaponTypes.Values )
			{
				try
				{
					WeaponDataDB.WeaponTypeObj oTest = (WeaponDataDB.WeaponTypeObj)o;
					Results[i++] = (WeaponDataDB.WeaponTypeObj)o;
				}
				catch (Exception err)
				{
					Debug.WriteLine("ERROR: " + err.ToString());
				}
			}

			return Results;
		}

		public string[] GetUniqueCharacterList()
		{
			if ( UniqueCharacters == null )
				return null;

			string[] Chars = new string[UniqueCharacters.Count];

			int i = 0;
			foreach ( object o in UniqueCharacters.Keys )
				Chars[i++] = o.ToString();

			return Chars;
		}

		public string[] GetUniqueLogDirs()
		{
			if ( UniqueLogDirs == null )
				return null;

			string[] LogDirs = new string[UniqueLogDirs.Count];

			int i = 0;
			foreach ( object o in UniqueLogDirs.Keys )
				LogDirs[i++] = o.ToString();

			return LogDirs;
		}
		
		/// <summary>
        /// Append a CombatLog.CombatLogCache entry to this collection.
        /// </summary>
        /// <param name="value">CombatLog.CombatLogCache instance.</param>
        /// <returns>The position into which the new element was inserted.</returns>
        public int Add(CombatLog.CombatLogCache value)
        {
			// Debug.WriteLine("COMBATLOGCACHECOLLECTION: Adding " + value.FileName);
			IndexThis(value.FileName, value);

			// Debug.WriteLine("COMBATLOGCACHECOLLECTION: " + value.FileName + " indexs built");
            return this.List.Add(value);
        }
        
        /// <summary>
        /// Determines whether a specified CombatLog.CombatLogCache instance is in this collection.
        /// </summary>
        /// <param name="value">CombatLog.CombatLogCache instance to search for.</param>
        /// <returns>True if the CombatLog.CombatLogCache instance is in the collection; otherwise false.</returns>
        public bool Contains(CombatLog.CombatLogCache value)
        {
            return this.List.Contains(value);
        }

		public bool Contains(string FileName)
		{
			if ( FileNameIX == null )
			{
				Debug.WriteLine("Filename index is empty!");
				return false;
			}

			if ( this.FileNameIX.ContainsKey(FileName.ToLower()) )
				return true;

//			foreach ( CombatLogCache c in this.List )
//				if ( c.FileName.ToLower() == FileName.ToLower() )
//					return true;

			return false;
		}
        
        /// <summary>
        /// Retrieve the index a specified CombatLog.CombatLogCache instance is in this collection.
        /// </summary>
        /// <param name="value">CombatLog.CombatLogCache instance to find.</param>
        /// <returns>The zero-based index of the specified CombatLog.CombatLogCache instance. If the object is not found, the return value is -1.</returns>
        public int IndexOf(CombatLog.CombatLogCache value)
        {
            return this.List.IndexOf(value);
        }
        
        /// <summary>
        /// Removes a specified CombatLog.CombatLogCache instance from this collection.
        /// </summary>
        /// <param name="value">The CombatLog.CombatLogCache instance to remove.</param>
        public void Remove(CombatLog.CombatLogCache value)
        {
			Debug.WriteLine("In CombatLogCacheCollection.Remove");
			Debug.WriteLine("Removing: " + value.FileName);

			if ( this.List.Contains(value) )
			{
				Debug.WriteLine("List contains value, removing it from indexes and collection");
				RemoveFromIndexes(value.FileName, value);
				this.List.Remove(value);
			}
        }
        
        /// <summary>
        /// Returns an enumerator that can iterate through the CombatLog.CombatLogCache instance.
        /// </summary>
        /// <returns>An CombatLog.CombatLogCache's enumerator.</returns>
        public new CombatLogCacheCollectionEnumerator GetEnumerator()
        {
            return new CombatLogCacheCollectionEnumerator(this);
        }
        
        /// <summary>
        /// Insert a CombatLog.CombatLogCache instance into this collection at a specified index.
        /// </summary>
        /// <param name="index">Zero-based index.</param>
        /// <param name="value">The CombatLog.CombatLogCache instance to insert.</param>
        public void Insert(int index, CombatLog.CombatLogCache value)
        {
            this.List.Insert(index, value);
        }
        
        /// <summary>
        /// Strongly typed enumerator of CombatLog.CombatLogCache.
        /// </summary>
        public class CombatLogCacheCollectionEnumerator : object, System.Collections.IEnumerator
        {
            
            /// <summary>
            /// Current index
            /// </summary>
            private int _index;
            
            /// <summary>
            /// Current element pointed to.
            /// </summary>
            private CombatLog.CombatLogCache _currentElement;
            
            /// <summary>
            /// Collection to enumerate.
            /// </summary>
            private CombatLogCacheCollection _collection;
            
            /// <summary>
            /// Default constructor for enumerator.
            /// </summary>
            /// <param name="collection">Instance of the collection to enumerate.</param>
            internal CombatLogCacheCollectionEnumerator(CombatLogCacheCollection collection)
            {
                _index = -1;
                _collection = collection;
            }
            
            /// <summary>
            /// Gets the CombatLog.CombatLogCache object in the enumerated CombatLogCacheCollection currently indexed by this instance.
            /// </summary>
            public CombatLog.CombatLogCache Current
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
    
		public new void Clear()
		{
			ClearIndexes();
			base.Clear ();
		}
	}
}
