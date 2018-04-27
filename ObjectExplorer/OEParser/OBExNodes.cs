using System;

namespace ObjectExplorer
{
	/// <summary>
	/// Summary description for OBExNodes.
	/// </summary>
	[Serializable()]
	public class OBExNodes
	{
		private ItemAttributeCollection _AttributeLookup;
		private NodeCollection _Nodes;
		private string _VersionStr;
		private int _VersionID;
		private DateTime _LastUpdated;

		public OBExNodes()
		{
		}

		public ObjectExplorer.ItemAttributeCollection AttributeLookup
		{
			get { return this._AttributeLookup; }
			set { this._AttributeLookup = value; }
		}

		public ObjectExplorer.NodeCollection Nodes
		{
			get { return this._Nodes; }
			set { this._Nodes = value; }
		}

		public string VersionStr
		{
			get { return this._VersionStr; }
			set { this._VersionStr = value; }
		}

		public int VersionID
		{
			get { return this._VersionID; }
			set { this._VersionID = value; }
		}

		public System.DateTime LastUpdated
		{
			get { return this._LastUpdated; }
			set { this._LastUpdated = value; }
		}
	}
}
