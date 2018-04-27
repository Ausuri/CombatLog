using System;
using System.Collections;
using System.Diagnostics;

namespace CombatLog
{
	public class HitTypeSorter : IComparer
	{
		public System.Windows.Forms.SortOrder Direction = System.Windows.Forms.SortOrder.Ascending;

		#region IComparer Members

		public  int Compare(object x, object y)
		{
			HitTypeLookup srcX = (HitTypeLookup)x;
			HitTypeLookup srcY = (HitTypeLookup)y;

			if ( Direction == System.Windows.Forms.SortOrder.Descending )
				return srcY.Priority - srcX.Priority;
			else
				return srcX.Priority - srcY.Priority;
		}

		#endregion
	}

	/// <summary>
	/// Summary description for HitTypeLib.
	/// </summary>
	public class HitTypeLib
	{
		private static ArrayList _HitTypes = new ArrayList();

		public HitTypeLib()
		{
		}

		public static void Initialise()
		{
            //_HitTypes.Clear();
			
			//
			// Hit Types for damage caused by you
			//
            //_HitTypes.Add(new HitTypeLookup(10, "misses", "Miss"));
            //_HitTypes.Add(new HitTypeLookup(20, "barely misses", "Close Miss"));
            //_HitTypes.Add(new HitTypeLookup(30, "glances off", "Glancing"));
            //_HitTypes.Add(new HitTypeLookup(40, "barely scratches", "Scratch"));
            //_HitTypes.Add(new HitTypeLookup(50, "lightly hits", "Light Hit"));
            //_HitTypes.Add(new HitTypeLookup(60, "hits", "Hit"));
            //_HitTypes.Add(new HitTypeLookup(70, "is well aimed at", "Well Aimed"));
            //_HitTypes.Add(new HitTypeLookup(80, "places an excellent hit on", "Excellent"));
            //_HitTypes.Add(new HitTypeLookup(90, "perfectly strikes", "Wrecking"));

            ////
            //// Hit types for damage received 
            ////
            //_HitTypes.Add(new HitTypeLookup(30,"lands a hit on you which glances off, causing ", "Glancing"));
            //_HitTypes.Add(new HitTypeLookup(40,"barely scratches you, causing ", "Scratch"));
            //_HitTypes.Add(new HitTypeLookup(50,"lightly hits you, doing ", "Light Hit"));
            //_HitTypes.Add(new HitTypeLookup(60,"hits you, doing ", "Hit"));
            //_HitTypes.Add(new HitTypeLookup(70,"aims well at you, inflicting ","Well Aimed"));
            //_HitTypes.Add(new HitTypeLookup(75,"heavily hits you, inflicting ", "Heavy"));
            //_HitTypes.Add(new HitTypeLookup(80,"places an excellent hit on you, inflicting ", "Excellent"));
            //_HitTypes.Add(new HitTypeLookup(90,"strikes you  perfectly, wrecking for ", "Wrecking"));

            _HitTypes.Clear();
            _HitTypes.Add(new HitTypeLookup(10, "misses", "Miss"));
            _HitTypes.Add(new HitTypeLookup(20, "barely misses", "Close Miss"));
            _HitTypes.Add(new HitTypeLookup(30, "glances off", "Glancing"));
            _HitTypes.Add(new HitTypeLookup(40, "barely scratches", "Scratch"));
            _HitTypes.Add(new HitTypeLookup(50, "lightly hits", "Light Hit"));
            _HitTypes.Add(new HitTypeLookup(60, "hits", "Hit"));
            _HitTypes.Add(new HitTypeLookup(60, "hit", "Hit"));
            _HitTypes.Add(new HitTypeLookup(70, "is well aimed at", "Well Aimed"));
            _HitTypes.Add(new HitTypeLookup(80, "places an excellent hit on", "Excellent"));
            _HitTypes.Add(new HitTypeLookup(90, "perfectly strikes", "Wrecking"));
            _HitTypes.Add(new HitTypeLookup(90, "Wrecking!", "Wrecking"));
            _HitTypes.Add(new HitTypeLookup(30, "lands a hit on you which glances off, causing ", "Glancing"));
            _HitTypes.Add(new HitTypeLookup(40, "barely scratches you, causing ", "Scratch"));
            _HitTypes.Add(new HitTypeLookup(50, "lightly hits you, doing ", "Light Hit"));
            _HitTypes.Add(new HitTypeLookup(60, "hits you, doing ", "Hit"));
            _HitTypes.Add(new HitTypeLookup(60, "hits you for ", "Hit"));
            _HitTypes.Add(new HitTypeLookup(70, "aims well at you, inflicting ", "Well Aimed"));
            _HitTypes.Add(new HitTypeLookup(0x4b, "heavily hits you, inflicting ", "Heavy"));
            _HitTypes.Add(new HitTypeLookup(80, "places an excellent hit on you, inflicting ", "Excellent"));
            _HitTypes.Add(new HitTypeLookup(90, "strikes you  perfectly, wrecking for ", "Wrecking"));

		}

		public static int Count()
		{
			return _HitTypes.Count;
		}

		public static string GetDisplayString(string HitTypeText)
		{
			if ( _HitTypes.Count == 0 )
				Initialise();

			System.Collections.IEnumerator foo = _HitTypes.GetEnumerator();

			foreach ( object o in _HitTypes )
				if ( ((HitTypeLookup)o).LogName == HitTypeText )
					return ((HitTypeLookup)o).DisplayName;

			return "Unknown: '" + HitTypeText + "'";
		}

		public static int GetRank(string HitTypeText)
		{
			if ( _HitTypes.Count == 0 )
				Initialise();

			System.Collections.IEnumerator foo = _HitTypes.GetEnumerator();

			foreach ( object o in _HitTypes )
				if ( ((HitTypeLookup)o).LogName == HitTypeText )
					return ((HitTypeLookup)o).Priority;

			return 0;
		}


		private static object GetHitTypeObject(string HitTypeName)
		{
			System.Collections.IEnumerator foo = _HitTypes.GetEnumerator();

			foreach ( object o in _HitTypes )
				if ( ((HitTypeLookup)o).LogName == HitTypeName )
					return o;

			Debug.WriteLine("Could not find object for " + HitTypeName);
			return null;
		}
		
		public static string[] Sort(string[] ToSort, System.Windows.Forms.SortOrder Direction)
		{
			if ( _HitTypes.Count == 0 )
				Initialise();

			ArrayList tmpHitTypes = new ArrayList();

			foreach ( string s in ToSort )
				tmpHitTypes.Add(GetHitTypeObject(s));

			HitTypeSorter sort = new HitTypeSorter();
			sort.Direction = Direction;

			tmpHitTypes.Sort(sort);

			string[] sortedList = new string[tmpHitTypes.Count];

			int i = 0;

			foreach ( object o in tmpHitTypes )
			{
				sortedList[i++] = ((HitTypeLookup)o).DisplayName;
			}

			return sortedList;
		}

		public static string[] SortedByPriority(System.Windows.Forms.SortOrder Direction)
		{
			if ( _HitTypes.Count == 0 )
				Initialise();

			HitTypeSorter sort = new HitTypeSorter();
			sort.Direction = Direction;

			_HitTypes.Sort(new HitTypeSorter());

			string[] types = new string[_HitTypes.Count];

			int i = 0;
			foreach ( object o in _HitTypes )
				types[i++] = ((HitTypeLookup)o).DisplayName;

			return types;
		}
	}
}
