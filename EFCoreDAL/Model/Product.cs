using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EFCoreDAL.Model
{
	public class Product
	{
		[Key]
		public int ID { get; set; }

		[StringLength(50), Required]
		public string Name { get; set; }

		public int Category { get; set; }

		public decimal? Price { get; set; }

		public Guid Guid { get; set; }
	}
}
