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
		/// ��λ��ӦDB �ر� ͨ������ CLOB ,BLOB , ���� date int Ӧ���Զ�ת��
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
