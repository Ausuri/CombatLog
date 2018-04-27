//------------------------------------------------------------------------------
// <autogenerated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.4322.573
//
//     Changes to this file may cause incorrect behavior and will be lost if 
//     the code is regenerated.
// </autogenerated>
//------------------------------------------------------------------------------

namespace CombatLog.LocationInfo
{
    using System;
    using System.Collections;
    
    
    /// <summary>
    /// Strongly typed collection of CombatLog.Collections.Location.
    /// </summary>
    public class LocationCollection : System.Collections.CollectionBase
    {
        
        /// <summary>
        /// Default constructor.
        /// </summary>
        public LocationCollection() : 
                base()
        {
        }
        
		public void SortByLogDTM(System.Windows.Forms.SortOrder Direction)
		{
			this.InnerList.Sort(new Sort.LocationCollectionSorter(Direction));
		}

        /// <summary>
        /// Gets or sets the value of the CombatLog.Collections.Location at a specific position in the LocationCollection.
        /// </summary>
        public CombatLog.LocationInfo.Location this[int index]
        {
            get
            {
                return ((CombatLog.LocationInfo.Location)(this.List[index]));
            }
            set
            {
                this.List[index] = value;
            }
        }
        
        /// <summary>
        /// Append a CombatLog.Collections.Location entry to this collection.
        /// </summary>
        /// <param name="value">CombatLog.Collections.Location instance.</param>
        /// <returns>The position into which the new element was inserted.</returns>
        public int Add(CombatLog.LocationInfo.Location value)
        {
            return this.List.Add(value);
        }
        
        /// <summary>
        /// Determines whether a specified CombatLog.Collections.Location instance is in this collection.
        /// </summary>
        /// <param name="value">CombatLog.Collections.Location instance to search for.</param>
        /// <returns>True if the CombatLog.Collections.Location instance is in the collection; otherwise false.</returns>
        public bool Contains(CombatLog.LocationInfo.Location value)
        {
            return this.List.Contains(value);
        }
        
        /// <summary>
        /// Retrieve the index a specified CombatLog.Collections.Location instance is in this collection.
        /// </summary>
        /// <param name="value">CombatLog.Collections.Location instance to find.</param>
        /// <returns>The zero-based index of the specified CombatLog.Collections.Location instance. If the object is not found, the return value is -1.</returns>
        public int IndexOf(CombatLog.LocationInfo.Location value)
        {
            return this.List.IndexOf(value);
        }
        
        /// <summary>
        /// Removes a specified CombatLog.Collections.Location instance from this collection.
        /// </summary>
        /// <param name="value">The CombatLog.Collections.Location instance to remove.</param>
        public void Remove(CombatLog.LocationInfo.Location value)
        {
            this.List.Remove(value);
        }
        
        /// <summary>
        /// Returns an enumerator that can iterate through the CombatLog.Collections.Location instance.
        /// </summary>
        /// <returns>An CombatLog.Collections.Location's enumerator.</returns>
        public new LocationCollectionEnumerator GetEnumerator()
        {
            return new LocationCollectionEnumerator(this);
        }
        
        /// <summary>
        /// Insert a CombatLog.Collections.Location instance into this collection at a specified index.
        /// </summary>
        /// <param name="index">Zero-based index.</param>
        /// <param name="value">The CombatLog.Collections.Location instance to insert.</param>
        public void Insert(int index, CombatLog.LocationInfo.Location value)
        {
            this.List.Insert(index, value);
        }
        
        /// <summary>
        /// Strongly typed enumerator of CombatLog.Collections.Location.
        /// </summary>
        public class LocationCollectionEnumerator : object, System.Collections.IEnumerator
        {
            
            /// <summary>
            /// Current index
            /// </summary>
            private int _index;
            
            /// <summary>
            /// Current element pointed to.
            /// </summary>
            private CombatLog.LocationInfo.Location _currentElement;
            
            /// <summary>
            /// Collection to enumerate.
            /// </summary>
            private LocationCollection _collection;
            
            /// <summary>
            /// Default constructor for enumerator.
            /// </summary>
            /// <param name="collection">Instance of the collection to enumerate.</param>
            internal LocationCollectionEnumerator(LocationCollection collection)
            {
                _index = -1;
                _collection = collection;
            }
            
            /// <summary>
            /// Gets the CombatLog.Collections.Location object in the enumerated LocationCollection currently indexed by this instance.
            /// </summary>
            public CombatLog.LocationInfo.Location Current
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
