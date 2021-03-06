//------------------------------------------------------------------------------
// <autogenerated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.4322.573
//
//     Changes to this file may cause incorrect behavior and will be lost if 
//     the code is regenerated.
// </autogenerated>
//------------------------------------------------------------------------------

namespace CombatLog
{
    using System;
    using System.Collections;
    
    
    /// <summary>
    /// Strongly typed collection of CombatLog.HitTypeLookup.
    /// </summary>
    public class HitTypeCollection : System.Collections.CollectionBase
    {
        
        /// <summary>
        /// Default constructor.
        /// </summary>
        public HitTypeCollection() : 
                base()
        {
			Init();
        }

		public void Init()
		{
			this.Add(new HitTypeLookup(10, "misses", "Misses"));
			this.Add(new HitTypeLookup(20, "barely misses", "Barely Misses"));
			this.Add(new HitTypeLookup(30, "scratches", "Scratches"));
			this.Add(new HitTypeLookup(40, "glances off", "Glancing"));
			this.Add(new HitTypeLookup(50, "lightly hits", "Light hit"));
			this.Add(new HitTypeLookup(60, "hits", "Hits"));
			this.Add(new HitTypeLookup(70, "is well aimed at", "Well Aimed"));
			this.Add(new HitTypeLookup(80, "places an excellent hit on", "Excellent"));
			this.Add(new HitTypeLookup(90, "perfectly strikes", "Wrecking"));
		}
        
        /// <summary>
        /// Gets or sets the value of the CombatLog.HitTypeLookup at a specific position in the HitTypeCollection.
        /// </summary>
        public CombatLog.HitTypeLookup this[int index]
        {
            get
            {
                return ((CombatLog.HitTypeLookup)(this.List[index]));
            }
            set
            {
                this.List[index] = value;
            }
        }
        
        /// <summary>
        /// Append a CombatLog.HitTypeLookup entry to this collection.
        /// </summary>
        /// <param name="value">CombatLog.HitTypeLookup instance.</param>
        /// <returns>The position into which the new element was inserted.</returns>
        public int Add(CombatLog.HitTypeLookup value)
        {
            return this.List.Add(value);
        }
        
        /// <summary>
        /// Determines whether a specified CombatLog.HitTypeLookup instance is in this collection.
        /// </summary>
        /// <param name="value">CombatLog.HitTypeLookup instance to search for.</param>
        /// <returns>True if the CombatLog.HitTypeLookup instance is in the collection; otherwise false.</returns>
        public bool Contains(CombatLog.HitTypeLookup value)
        {
            return this.List.Contains(value);
        }
        
        /// <summary>
        /// Retrieve the index a specified CombatLog.HitTypeLookup instance is in this collection.
        /// </summary>
        /// <param name="value">CombatLog.HitTypeLookup instance to find.</param>
        /// <returns>The zero-based index of the specified CombatLog.HitTypeLookup instance. If the object is not found, the return value is -1.</returns>
        public int IndexOf(CombatLog.HitTypeLookup value)
        {
            return this.List.IndexOf(value);
        }
        
        /// <summary>
        /// Removes a specified CombatLog.HitTypeLookup instance from this collection.
        /// </summary>
        /// <param name="value">The CombatLog.HitTypeLookup instance to remove.</param>
        public void Remove(CombatLog.HitTypeLookup value)
        {
            this.List.Remove(value);
        }
        
        /// <summary>
        /// Returns an enumerator that can iterate through the CombatLog.HitTypeLookup instance.
        /// </summary>
        /// <returns>An CombatLog.HitTypeLookup's enumerator.</returns>
        public new HitTypeCollectionEnumerator GetEnumerator()
        {
            return new HitTypeCollectionEnumerator(this);
        }
        
        /// <summary>
        /// Insert a CombatLog.HitTypeLookup instance into this collection at a specified index.
        /// </summary>
        /// <param name="index">Zero-based index.</param>
        /// <param name="value">The CombatLog.HitTypeLookup instance to insert.</param>
        public void Insert(int index, CombatLog.HitTypeLookup value)
        {
            this.List.Insert(index, value);
        }
        
        /// <summary>
        /// Strongly typed enumerator of CombatLog.HitTypeLookup.
        /// </summary>
        public class HitTypeCollectionEnumerator : object, System.Collections.IEnumerator
        {
            
            /// <summary>
            /// Current index
            /// </summary>
            private int _index;
            
            /// <summary>
            /// Current element pointed to.
            /// </summary>
            private CombatLog.HitTypeLookup _currentElement;
            
            /// <summary>
            /// Collection to enumerate.
            /// </summary>
            private HitTypeCollection _collection;
            
            /// <summary>
            /// Default constructor for enumerator.
            /// </summary>
            /// <param name="collection">Instance of the collection to enumerate.</param>
            internal HitTypeCollectionEnumerator(HitTypeCollection collection)
            {
                _index = -1;
                _collection = collection;
            }
            
            /// <summary>
            /// Gets the CombatLog.HitTypeLookup object in the enumerated HitTypeCollection currently indexed by this instance.
            /// </summary>
            public CombatLog.HitTypeLookup Current
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
