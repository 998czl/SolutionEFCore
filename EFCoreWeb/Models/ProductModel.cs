using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreWeb.Models
{
	public class ProductModel
	{
		public int ID { get; set; }		
		public string Name { get; set; }

		public int Category { get; set; }

		public decimal? Price { get; set; }

		public Guid Guid { get; set; }
	}
}
