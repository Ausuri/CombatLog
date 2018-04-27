//------------------------------------------------------------------------------
// <autogenerated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.4322.573
//
//     Changes to this file may cause incorrect behavior and will be lost if 
//     the code is regenerated.
// </autogenerated>
//------------------------------------------------------------------------------

namespace CombatLog.WeaponDataDB
{
    using System;
    using System.Collections;
	using System.Diagnostics;

	public class WeaponTypeObj
	{
		private WeaponType _Type;

		public WeaponTypeObj()
		{
		}

		public WeaponTypeObj(WeaponType value)
		{
			this._Type = value;
		}

		public override string ToString()
		{
			return this.GetWeaponTypeDisplayString();
		}

		private string GetWeaponTypeDisplayString()
		{
			switch (this._Type)
			{
				case WeaponType.CombatDrone:
					return "Combat Drone";
				case WeaponType.Hybrid:
					return "Hybrid Turret";
				case WeaponType.Laser:
					return "Laser Turret";
				case WeaponType.Missile:
					return "Missile";
				case WeaponType.Projectile:
					return "Projectile Turret";
				case WeaponType.SmartBomb:
					return "Smartbomb";
				default:
					return "Unknown";
			}
		}

		public WeaponType Type
		{
			get { return this._Type; }
			set { this._Type = value; }
		}
	}

	public class WeaponClassObj
	{
		private WeaponClass _Class;

		public override string ToString()
		{
			return GetWeaponTypeDisplayString();
		}

		private string GetWeaponTypeDisplayString()
		{
			switch ( this._Class )
			{
				case WeaponClass.CruiseMissile:
					return "Cruise Missile";

				case WeaponClass.DefenderMissile:
					return "Defender Missile";

				case WeaponClass.FOFCruiseMissile:
					return "FOF Cruise Missile";

				case WeaponClass.FOFHeavyMissile:
					return "FOF Heavy Missile";

				case WeaponClass.FOFLightMissile:
					return "FOF Light Missile";

				case WeaponClass.HeavyDrone:
					return "Heavy Drone";

				case WeaponClass.HeavyMissile:
					return "Heavy Missile";

				case WeaponClass.Large:
					return "Large";

				case WeaponClass.LightDrone:
					return "Light Drone";

				case WeaponClass.LightMissile:
					return "Light Missile";

				case WeaponClass.Medium:
					return "Medium";

				case WeaponClass.MediumDrone:
					return "Medium Drone";

				case WeaponClass.Micro:
					return "Micro";

				case WeaponClass.RocketMissile:
					return "Rocket";

				case WeaponClass.Small:
					return "Small";

				case WeaponClass.TorpedoMissile:
					return "Torpedo";

				default:
					return "Unknown";
			}
		}

		public WeaponClassObj()
		{
		}

		public WeaponClassObj(WeaponClass value)
		{
			this._Class = value;
		}

		public WeaponClass Class
		{
			get { return this._Class; }
			set { this._Class = value; }
		}

		public string DisplayName
		{
			get { return this.GetWeaponTypeDisplayString(); }
		}
	}

	public class Utils
	{
		public Utils()
		{
		}

		public static WeaponClassObj GetWeaponClassObj(WeaponClass value)
		{
			return new WeaponClassObj(value);
		}

		public static WeaponTypeObj GetWeaponTypeObj(WeaponType value)
		{
			return new WeaponTypeObj(value);
		}

		public static string GetWeaponTypeDisplayString(WeaponType value)
		{
			switch (value)
			{
				case WeaponType.CombatDrone:
					return "Combat Drone";

				case WeaponType.Hybrid:
					return "Hybrid Turret";

				case WeaponType.Laser:
					return "Laser Turret";

				case WeaponType.Missile:
					return "Missile";

				case WeaponType.Projectile:
					return "Projectile Turret";

				case WeaponType.SmartBomb:
					return "Smartbomb";
				default:
					return "Unknown";
			}
		}

		public static string GetWeaponClassDisplayString(WeaponClass value)
		{
			switch ( value )
			{
				case WeaponClass.CruiseMissile:
					return "Cruise Missile";

				case WeaponClass.DefenderMissile:
					return "Defender Missile";

				case WeaponClass.FOFCruiseMissile:
					return "FOF Cruise Missile";

				case WeaponClass.FOFHeavyMissile:
					return "FOF Heavy Missile";

				case WeaponClass.FOFLightMissile:
					return "FOF Light Missile";

				case WeaponClass.HeavyDrone:
					return "Heavy Drone";

				case WeaponClass.HeavyMissile:
					return "Heavy Missile";

				case WeaponClass.Large:
					return "Large Turret";

				case WeaponClass.LightDrone:
					return "Light Drone";

				case WeaponClass.LightMissile:
					return "Light Missile";

				case WeaponClass.Medium:
					return "Medium Turret";

				case WeaponClass.MediumDrone:
					return "Medium Drone";

				case WeaponClass.Micro:
					return "Micro Smartbomb";

				case WeaponClass.RocketMissile:
					return "Rocket";

				case WeaponClass.Small:
					return "Small Turret";

				case WeaponClass.TorpedoMissile:
					return "Torpedo";

				default:
					return "Unknown";
			}
		}
	}

    
    /// <summary>
    /// Strongly typed collection of WeaponDataDB.WeaponDataSource.
    /// </summary>
	[Serializable()]
	public class WeaponDataSourceCollection : System.Collections.CollectionBase
	{
        
		private Hashtable _TypeIndex;
		private Hashtable _ClassIndex;
		private Hashtable _UniqueTypes;
		private Hashtable _UniqueClasses;
		private Hashtable _WeaponNameIX;

		/// <summary>
		/// Default constructor.
		/// </summary>
		public WeaponDataSourceCollection() : 
			base()
		{
			InitialiseIndexes();
		}

		private void IndexWeaponType(WeaponDataSource Weapon)
		{
			ArrayList Weapons = new ArrayList();

			if ( _TypeIndex.Contains(Weapon.Type) )
			{
				Weapons = (ArrayList)_TypeIndex[Weapon.Type];
				Weapons.Add(Weapon);
			}
			else
			{
				Weapons.Add(Weapon);
				_TypeIndex.Add(Weapon.Type, Weapons);
			}
		}

		private void IndexWeaponClass(WeaponDataSource Weapon)
		{
			ArrayList Weapons = new ArrayList();

			if ( _ClassIndex.Contains(Weapon.Class) )
			{
				Weapons = (ArrayList)_ClassIndex[Weapon.Class];
				Weapons.Add(Weapon);
			}
			else
			{
				Weapons.Add(Weapon);
				_ClassIndex.Add(Weapon.Class, Weapons);
			}
		}

		private void IndexWeaponName(WeaponDataSource Weapon)
		{
			if ( Weapon.Name == null || Weapon.Name.Length == 0 )
				return;

			if ( !_WeaponNameIX.Contains(Weapon.Name) )
				_WeaponNameIX.Add(Weapon.Name, Weapon);
		}

		private void CreateUniqueTypes(WeaponDataSource Weapon)
		{
			if ( !_UniqueTypes.Contains(Weapon.Type) )
				_UniqueTypes.Add(Weapon.Type, "foo");
		}

		private void CreateUniqueClasses(WeaponDataSource Weapon)
		{
			if ( !_UniqueClasses.Contains(Weapon.Class) )
				_UniqueClasses.Add(Weapon.Class, "foo");
		}

		private void IndexThis(WeaponDataSource Weapon)
		{
			CreateUniqueTypes(Weapon);
			CreateUniqueClasses(Weapon);

			IndexWeaponType(Weapon);
			IndexWeaponClass(Weapon);
			IndexWeaponName(Weapon);
		}

		private void InitialiseIndexes()
		{
			_TypeIndex = new Hashtable();
			_ClassIndex = new Hashtable();
			_UniqueClasses = new Hashtable();
			_UniqueTypes = new Hashtable();
			_WeaponNameIX = new Hashtable();
		}

		public WeaponType[] GetUniqueWeaponTypes()
		{
			if ( _UniqueTypes.Count != 0 )
			{

				WeaponType[] Types = new WeaponType[_UniqueTypes.Count];

				int i = 0;
				foreach ( object o in _UniqueTypes.Keys )
					Types[i++] = (WeaponType)o;

				return Types;
			}
			else
				return null;
		}

		public WeaponClass[] GetUniqueWeaponClasses()
		{
			if ( _UniqueClasses.Count > 0 )
			{
				WeaponClass[] Classes = new WeaponClass[_UniqueClasses.Count];

				int i = 0;
				foreach ( object o in _UniqueClasses.Keys )
					Classes[i++] = (WeaponClass)o;

				return Classes;
			}
			else
				return null;
		}

		public WeaponDataSourceCollection FilterByType(WeaponType Type)
		{
			if ( _TypeIndex.Contains(Type) )
			{
				ArrayList Weapons = (ArrayList)_TypeIndex[Type];
				WeaponDataSourceCollection WeaponCol = new WeaponDataSourceCollection();

				foreach (object o in Weapons)
					WeaponCol.Add((WeaponDataSource)o);

				return WeaponCol;
			}
			else
				return null;
		}

		public WeaponDataSourceCollection FilterByClass(WeaponClass Class)
		{
			if ( _ClassIndex.Contains(Class) )
			{
				ArrayList Weapons = (ArrayList)_ClassIndex[Class];
				WeaponDataSourceCollection WeaponCol = new WeaponDataSourceCollection();

				foreach (object o in Weapons)
					WeaponCol.Add((WeaponDataSource)o);

				return WeaponCol;
			}
			else
				return null;
		}

		public WeaponDataSourceCollection FilterByNameExact(string SearchWord)
		{
			WeaponDataSourceCollection Results = new WeaponDataSourceCollection();

			Results.Add(this[SearchWord]);

			return Results;
		}

		public WeaponDataSourceCollection FilterByName(string SearchWords)
		{
			string[] Words = SearchWords.Split(' ');
			WeaponDataSourceCollection Results = new WeaponDataSourceCollection();

			bool includeWeapon = false;
			foreach ( WeaponDataSource w in this.List )
			{
				foreach (string word in Words)
				{
					if ( w.Name.Trim().ToLower().IndexOf(word) != -1 )
						includeWeapon = true;
					else
					{
						includeWeapon = false;
						break;
					}
				}

				if ( includeWeapon )
					Results.Add(w);
			}

			return Results;
		}
        
        /// <summary>
        /// Gets or sets the value of the WeaponDataDB.WeaponDataSource at a specific position in the WeaponDataSourceCollection.
        /// </summary>
        public WeaponDataDB.WeaponDataSource this[int index]
        {
            get
            {
                return ((WeaponDataDB.WeaponDataSource)(this.List[index]));
            }
            set
            {
                this.List[index] = value;
            }
        }

		public WeaponDataDB.WeaponDataSource this[string WeaponName]
		{
			get
			{
				if ( _WeaponNameIX.Contains(WeaponName) )
					return (WeaponDataDB.WeaponDataSource)_WeaponNameIX[WeaponName]; 
				else
					return null;
			}
		}
        
        /// <summary>
        /// Append a WeaponDataDB.WeaponDataSource entry to this collection.
        /// </summary>
        /// <param name="value">WeaponDataDB.WeaponDataSource instance.</param>
        /// <returns>The position into which the new element was inserted.</returns>
        public int Add(WeaponDataDB.WeaponDataSource value)
        {
			IndexThis(value);
            return this.List.Add(value);
        }
        
        /// <summary>
        /// Determines whether a specified WeaponDataDB.WeaponDataSource instance is in this collection.
        /// </summary>
        /// <param name="value">WeaponDataDB.WeaponDataSource instance to search for.</param>
        /// <returns>True if the WeaponDataDB.WeaponDataSource instance is in the collection; otherwise false.</returns>
        public bool Contains(WeaponDataDB.WeaponDataSource value)
        {
            return this.List.Contains(value);
        }
        
        /// <summary>
        /// Retrieve the index a specified WeaponDataDB.WeaponDataSource instance is in this collection.
        /// </summary>
        /// <param name="value">WeaponDataDB.WeaponDataSource instance to find.</param>
        /// <returns>The zero-based index of the specified WeaponDataDB.WeaponDataSource instance. If the object is not found, the return value is -1.</returns>
        public int IndexOf(WeaponDataDB.WeaponDataSource value)
        {
            return this.List.IndexOf(value);
        }
        
        /// <summary>
        /// Removes a specified WeaponDataDB.WeaponDataSource instance from this collection.
        /// </summary>
        /// <param name="value">The WeaponDataDB.WeaponDataSource instance to remove.</param>
        public void Remove(WeaponDataDB.WeaponDataSource value)
        {
            this.List.Remove(value);
        }
        
        /// <summary>
        /// Returns an enumerator that can iterate through the WeaponDataDB.WeaponDataSource instance.
        /// </summary>
        /// <returns>An WeaponDataDB.WeaponDataSource's enumerator.</returns>
        public new WeaponDataSourceCollectionEnumerator GetEnumerator()
        {
            return new WeaponDataSourceCollectionEnumerator(this);
        }
        
        /// <summary>
        /// Insert a WeaponDataDB.WeaponDataSource instance into this collection at a specified index.
        /// </summary>
        /// <param name="index">Zero-based index.</param>
        /// <param name="value">The WeaponDataDB.WeaponDataSource instance to insert.</param>
        public void Insert(int index, WeaponDataDB.WeaponDataSource value)
        {
            this.List.Insert(index, value);
        }
        
        /// <summary>
        /// Strongly typed enumerator of WeaponDataDB.WeaponDataSource.
        /// </summary>
        public class WeaponDataSourceCollectionEnumerator : object, System.Collections.IEnumerator
        {
            
            /// <summary>
            /// Current index
            /// </summary>
            private int _index;
            
            /// <summary>
            /// Current element pointed to.
            /// </summary>
            private WeaponDataDB.WeaponDataSource _currentElement;
            
            /// <summary>
            /// Collection to enumerate.
            /// </summary>
            private WeaponDataSourceCollection _collection;
            
            /// <summary>
            /// Default constructor for enumerator.
            /// </summary>
            /// <param name="collection">Instance of the collection to enumerate.</param>
            internal WeaponDataSourceCollectionEnumerator(WeaponDataSourceCollection collection)
            {
                _index = -1;
                _collection = collection;
            }
            
            /// <summary>
            /// Gets the WeaponDataDB.WeaponDataSource object in the enumerated WeaponDataSourceCollection currently indexed by this instance.
            /// </summary>
            public WeaponDataDB.WeaponDataSource Current
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