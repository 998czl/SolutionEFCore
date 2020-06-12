using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreWeb.Models
{
	public class PgaeClass
	{

		public PgaeClass()
		{
			PageSize = 10;
			PageIndex = 1;
		}
		public int PageSize { get; set; }

		public int PageIndex { get; set; }
	}
}
