using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EFCoreDAL.Model
{
	public class Administrator
	{
		[Key]
		public int UserId { get; set; }

		[StringLength(50), Required]
		public string UserName { get; set; }

		[StringLength(20), Required]
		public string Mobile { get; set; }

		[StringLength(50), Required]
		public string RealName { get; set; }

		public int UserState { get; set; }

		public int RoleId { get; set; }

		[StringLength(20), Required]
		public string UserPassword { get; set; }

		public DateTime CreateTime { get; set; }

		public int ShowMobile { get; set; }
	
		public string ConnectionStr { get; set; }
	}
}
