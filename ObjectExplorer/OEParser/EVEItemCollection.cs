//------------------------------------------------------------------------------
// <autogenerated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.4322.573
//
//     Changes to this file may cause incorrect behavior and will be lost if 
//     the code is regenerated.
// </autogenerated>
//------------------------------------------------------------------------------

namespace ObjectExplorer
{
    using System;
    using System.Collections;
    
    
    /// <summary>
    /// Strongly typed collection of ObjectExplorer.EVEItem.
    /// </summary>
    public class EVEItemCollection : System.Collections.CollectionBase
    {
        
        /// <summary>
        /// Default constructor.
        /// </summary>
        public EVEItemCollection() : 
                base()
        {
        }
        
        /// <summary>
        /// Gets or sets the value of the ObjectExplorer.EVEItem at a specific position in the EVEItemCollection.
        /// </summary>
        public ObjectExplorer.EVEItem this[int index]
        {
            get
            {
                return ((ObjectExplorer.EVEItem)(this.List[index]));
            }
            set
            {
                this.List[index] = value;
            }
        }
        
        /// <summary>
        /// Append a ObjectExplorer.EVEItem entry to this collection.
        /// </summary>
        /// <param name="value">ObjectExplorer.EVEItem instance.</param>
        /// <returns>The position into which the new element was inserted.</returns>
        public int Add(ObjectExplorer.EVEItem value)
        {
            return this.List.Add(value);
        }
        
        /// <summary>
        /// Determines whether a specified ObjectExplorer.EVEItem instance is in this collection.
        /// </summary>
        /// <param name="value">ObjectExplorer.EVEItem instance to search for.</param>
        /// <returns>True if the ObjectExplorer.EVEItem instance is in the collection; otherwise false.</returns>
        public bool Contains(ObjectExplorer.EVEItem value)
        {
            return this.List.Contains(value);
        }
        
        /// <summary>
        /// Retrieve the index a specified ObjectExplorer.EVEItem instance is in this collection.
        /// </summary>
        /// <param name="value">ObjectExplorer.EVEItem instance to find.</param>
        /// <returns>The zero-based index of the specified ObjectExplorer.EVEItem instance. If the object is not found, the return value is -1.</returns>
        public int IndexOf(ObjectExplorer.EVEItem value)
        {
            return this.List.IndexOf(value);
        }
        
        /// <summary>
        /// Removes a specified ObjectExplorer.EVEItem instance from this collection.
        /// </summary>
        /// <param name="value">The ObjectExplorer.EVEItem instance to remove.</param>
        public void Remove(ObjectExplorer.EVEItem value)
        {
            this.List.Remove(value);
        }
        
        /// <summary>
        /// Returns an enumerator that can iterate through the ObjectExplorer.EVEItem instance.
        /// </summary>
        /// <returns>An ObjectExplorer.EVEItem's enumerator.</returns>
        public new EVEItemCollectionEnumerator GetEnumerator()
        {
            return new EVEItemCollectionEnumerator(this);
        }
        
        /// <summary>
        /// Insert a ObjectExplorer.EVEItem instance into this collection at a specified index.
        /// </summary>
        /// <param name="index">Zero-based index.</param>
        /// <param name="value">The ObjectExplorer.EVEItem instance to insert.</param>
        public void Insert(int index, ObjectExplorer.EVEItem value)
        {
            this.List.Insert(index, value);
        }
        
        /// <summary>
        /// Strongly typed enumerator of ObjectExplorer.EVEItem.
        /// </summary>
        public class EVEItemCollectionEnumerator : object, System.Collections.IEnumerator
        {
            
            /// <summary>
            /// Current index
            /// </summary>
            private int _index;
            
            /// <summary>
            /// Current element pointed to.
            /// </summary>
            private ObjectExplorer.EVEItem _currentElement;
            
            /// <summary>
            /// Collection to enumerate.
            /// </summary>
            private EVEItemCollection _collection;
            
            /// <summary>
            /// Default constructor for enumerator.
            /// </summary>
            /// <param name="collection">Instance of the collection to enumerate.</param>
            internal EVEItemCollectionEnumerator(EVEItemCollection collection)
            {
                _index = -1;
                _collection = collection;
            }
            
            /// <summary>
            /// Gets the ObjectExplorer.EVEItem object in the enumerated EVEItemCollection currently indexed by this instance.
            /// </summary>
            public ObjectExplorer.EVEItem Current
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
