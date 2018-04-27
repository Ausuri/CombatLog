using System;

namespace ObjectExplorer
{
	/// <summary>
	/// Summary description for EVEItem.
	/// </summary>
	public class EVEItem
	{
		private int		_ItemID;
		private string	_Name;
		private string	_Description;
		private string	_Path;
		private string	_ImageURL;
		private DateTime _LastUpdated;

		private ItemAttributeCollection _Attributes;

		public EVEItem()
		{
		}

		public int ItemID
		{
			get { return this._ItemID; }
			set { this._ItemID = value; }
		}

		public ObjectExplorer.ItemAttributeCollection Attributes
		{
			get { return this._Attributes; }
			set { this._Attributes = value; }
		}

		public string Name
		{
			get { return this._Name; }
			set { this._Name = value; }
		}

		public string Description
		{
			get { return this._Description; }
			set { this._Description = value; }
		}

		public string ImageURL
		{
			get { return this._ImageURL; }
			set { this._ImageURL = value; }
		}

		public string Path
		{
			get { return this._Path; }
			set { this._Path = value; }
		}

		public System.DateTime LastUpdated
		{
			get { return this._LastUpdated; }
			set { this._LastUpdated = value; }
		}
	}
}
