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
    /// Strongly typed collection of CombatLog.GraphItem.
    /// </summary>
    public class GraphItemCollection : System.Collections.CollectionBase
    {
        private int _LastSequenceID = 0;

		public int LastSequenceID
		{
			get { return _LastSequenceID; }
		}

        /// <summary>
        /// Default constructor.
        /// </summary>
        public GraphItemCollection() : 
                base()
        {
        }
        
        /// <summary>
        /// Gets or sets the value of the CombatLog.GraphItem at a specific position in the GraphItemCollection.
        /// </summary>
        public CombatLog.GraphItem this[int index]
        {
            get
            {
                return ((CombatLog.GraphItem)(this.List[index]));
            }
            set
            {
                this.List[index] = value;
            }
        }
        
        /// <summary>
        /// Append a CombatLog.GraphItem entry to this collection.
        /// </summary>
        /// <param name="value">CombatLog.GraphItem instance.</param>
        /// <returns>The position into which the new element was inserted.</returns>
        public int Add(CombatLog.GraphItem value)
        {
			_LastSequenceID++;
            return this.List.Add(value);
        }
        
        /// <summary>
        /// Determines whether a specified CombatLog.GraphItem instance is in this collection.
        /// </summary>
        /// <param name="value">CombatLog.GraphItem instance to search for.</param>
        /// <returns>True if the CombatLog.GraphItem instance is in the collection; otherwise false.</returns>
        public bool Contains(CombatLog.GraphItem value)
        {
            return this.List.Contains(value);
        }
        
        /// <summary>
        /// Retrieve the index a specified CombatLog.GraphItem instance is in this collection.
        /// </summary>
        /// <param name="value">CombatLog.GraphItem instance to find.</param>
        /// <returns>The zero-based index of the specified CombatLog.GraphItem instance. If the object is not found, the return value is -1.</returns>
        public int IndexOf(CombatLog.GraphItem value)
        {
            return this.List.IndexOf(value);
        }
        
        /// <summary>
        /// Removes a specified CombatLog.GraphItem instance from this collection.
        /// </summary>
        /// <param name="value">The CombatLog.GraphItem instance to remove.</param>
        public void Remove(CombatLog.GraphItem value)
        {
			_LastSequenceID--;
            this.List.Remove(value);
        }
        
        /// <summary>
        /// Returns an enumerator that can iterate through the CombatLog.GraphItem instance.
        /// </summary>
        /// <returns>An CombatLog.GraphItem's enumerator.</returns>
        public new GraphItemCollectionEnumerator GetEnumerator()
        {
            return new GraphItemCollectionEnumerator(this);
        }
        
        /// <summary>
        /// Insert a CombatLog.GraphItem instance into this collection at a specified index.
        /// </summary>
        /// <param name="index">Zero-based index.</param>
        /// <param name="value">The CombatLog.GraphItem instance to insert.</param>
        public void Insert(int index, CombatLog.GraphItem value)
        {
            this.List.Insert(index, value);
        }
        
        /// <summary>
        /// Strongly typed enumerator of CombatLog.GraphItem.
        /// </summary>
        public class GraphItemCollectionEnumerator : object, System.Collections.IEnumerator
        {
            
            /// <summary>
            /// Current index
            /// </summary>
            private int _index;
            
            /// <summary>
            /// Current element pointed to.
            /// </summary>
            private CombatLog.GraphItem _currentElement;
            
            /// <summary>
            /// Collection to enumerate.
            /// </summary>
            private GraphItemCollection _collection;
            
            /// <summary>
            /// Default constructor for enumerator.
            /// </summary>
            /// <param name="collection">Instance of the collection to enumerate.</param>
            internal GraphItemCollectionEnumerator(GraphItemCollection collection)
            {
                _index = -1;
                _collection = collection;
            }
            
            /// <summary>
            /// Gets the CombatLog.GraphItem object in the enumerated GraphItemCollection currently indexed by this instance.
            /// </summary>
            public CombatLog.GraphItem Current
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