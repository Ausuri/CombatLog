using System;

namespace ObjectExplorer
{
	public enum AttrType {stringType, intType, floatType};

	/// <summary>
	/// Summary description for ItemAttribute.
	/// </summary>
	[Serializable()]
	public class ItemAttribute
	{
		private string _Name;
		private string _Value;
		private string _Description;
		private string _IconURL;
		private string _GroupName;

		private AttrType _AttrType;

		public ItemAttribute()
		{
		}

		public string Value
		{
			get { return this._Value; }
			set { this._Value = value; }
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

		public string GroupName
		{
			get { return this._GroupName; }
			set { this._GroupName = value; }
		}

		public string IconURL
		{
			get { return this._IconURL; }
			set { this._IconURL = value; }
		}

		public AttrType AttrType
		{
			get { return this._AttrType; }
			set { this._AttrType = value; }
		}

	}
}
