using System;
using System.Collections.Generic;
using System.Text;

namespace EFCoreDAL.Dto
{
	public class AdminDto
	{
		public int UserId { get; set; }

		public string Mobile { get; set; }

		public string UserName { get; set; }

		public DateTime CreateTime { get; set; }

		public string ConnectionStr { get; set; }
	}
}
