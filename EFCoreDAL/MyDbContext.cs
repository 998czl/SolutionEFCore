using EFCoreDAL.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFCoreDAL
{
	public class MyDbContext : DbContext
	{
		public MyDbContext(DbContextOptions options) : base(options) { }

		public DbSet<Product> product { get; set; }
	}
}
