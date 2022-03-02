using System.Collections.Generic;
using System.Data;
using System.Runtime.Serialization;

namespace MESModel
{
	public class SqlModel
	{
		public string Sql { get; set; }

		public object Model { get; set; }

		/// <summary>
		/// 栏位对应DB 特别 通常用于 CLOB ,BLOB , 其余 date int 应该自动转型
		/// </summary>
		public Dictionary<string, string> DbMapping { get; set; }
	}
	[DataContract]
	public class DALModel
	{
		[DataMember]
		public bool IsSuccess
		{
			get;
			set;
		}

		[DataMember]
		public string Message
		{
			get;
			set;
		}

		[DataMember]
		public DataSet DS
		{
			get;
			set;
		}
	}
}
