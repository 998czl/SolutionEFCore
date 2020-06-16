using EFCoreCommon;
using EFCoreDAL.Application.Interface;
using EFCoreDAL.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreDAL.Application.Imp
{
	public class ProductService : IProductService
	{		
		public async Task<bool> AddAsyns(Product product, DbContextOptions options)
		{
			using (MyDbContext db = new MyDbContext(options))
			{
				using (var transaction = db.Database.BeginTransaction())
				{
					try
					{						
						await db.Products.AddAsync(product);
						var res = await db.SaveChangesAsync();
						transaction.Commit();
						return res > 0 ? true : false;
					}
					catch (Exception ex)
					{
						LogHelper.Error("Product=>AddAsyns添加失败", ex);
						throw ex;
					}
				}
			}
		}

		public async Task<bool> DeleteAsync(int ID, DbContextOptions options)
		{
			using (MyDbContext db = new MyDbContext(options))
			{
				using (var transaction = db.Database.BeginTransaction())
				{
					try
					{
						var model = await db.Products.FindAsync(ID);
						if (model == null)
						{
							throw new Exception("数据有误!");
						}
						db.Products.Remove(model);
						var result = await db.SaveChangesAsync() > 0 ? true : false;
						transaction.Commit();
						return result;
					}
					catch (Exception ex)
					{
						LogHelper.Error("Product=>DeleteAsync", ex);
						return false;
					}
				}
			}
		}

		public async Task<List<Product>> GetProductList(int pageIndex, int pageSize, DbContextOptions options)
		{
			using (MyDbContext db = new MyDbContext(options))
			{
				var list = await db.Products.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
				return list;
			}
		}

		public async Task<bool> UpdateAsync(Product product, DbContextOptions options)
		{
			using (MyDbContext db = new MyDbContext(options))
			{
				using (var transaction = db.Database.BeginTransaction())
				{
					try
					{
						var model = await db.Products.FindAsync(product.ID);
						if (model != null)
						{
							model.Name = product.Name;
							model.Category = product.Category;
							model.Price = product.Price;
						}						
						var result =  await db.SaveChangesAsync() > 0 ? true : false;
						transaction.Commit();
						return result;
					}
					catch (Exception ex)
					{
						LogHelper.Error("Product=>UpdateAsync", ex);
						return false;
					}
				}
			}
		}
	}
}
