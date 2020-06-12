using EFCoreDAL.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreDAL.Application.Interface
{
	public interface IProductService
	{		
		Task<bool> AddAsyns(Product product, DbContextOptions options);
		Task<bool> UpdateAsync(Product product, DbContextOptions options);
		Task<bool> DeleteAsync(int ID, DbContextOptions options);
		Task<List<Product>> GetProductList(int pageIndex, int pageSize, DbContextOptions options);
	}
}
