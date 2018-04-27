using System;

namespace ObjectExplorer
{
	/// <summary>
	/// Primary Node object. Holds NodeName, NodeID and NodeURL
	/// </summary>
	[Serializable()]
	public class Node
	{
		private string				_NodeName;
		private string				_NodeID;
		private string				_NodeURL;
		private bool				_IsItemGroup;
		private EVEItemCollection	_ItemsInGroup;
		private DateTime			_LastUpdated;

		public Node()
		{
		}

		public Node(string NodeName, string NodeID, string NodeURL)
		{
			this._NodeName = NodeName;
			this._NodeID = NodeID;
			this._NodeURL = NodeURL;
		}

		public string NodeID
		{
			get { return this._NodeID; }
			set { this._NodeID = value; }
		}

		public string NodeName
		{
			get { return this._NodeName; }
			set { this._NodeName = value; }
		}

		public string NodeURL
		{
			get { return this._NodeURL; }
			set { this._NodeURL = value; }
		}

		public bool IsItemGroup
		{
			get { return this._IsItemGroup; }
			set { this._IsItemGroup = value; }
		}

		public bool HasItems
		{
			get
			{
				if ( this._NodeURL == null )
					return false;
				else
					return true;
			}
		}

		public int Level
		{
			get { return this._NodeID.Split('_').Length - 1; }
		}

		public ObjectExplorer.EVEItemCollection ItemsInGroup
		{
			get { return this._ItemsInGroup; }
			set { this._ItemsInGroup = value; }
		}

		public System.DateTime LastUpdated
		{
			get { return this._LastUpdated; }
			set { this._LastUpdated = value; }
		}
	}
}
