using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using EFCoreDAL;
using EFCoreDAL.Application.Interface;
using EFCoreDAL.Model;
using EFCoreWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCoreWeb.Controllers
{
	public class ProductController : BaseController
	{
		private DbContextOptions _options { get; set; }
		public ProductController(DbContextOptions options)
		{
			_options = options;
		}
		public IActionResult Index()
		{
			return View();
		}
									
		public async Task<string> AddAsyns(ProductModel model)
		{
			try
			{
				var _service = Container.Instance.Resolve<IProductService>();
				Product product = new Product()
				{
					Name = model.Name,
					Category = model.Category,
					Price = model.Price,
					Guid = Guid.NewGuid()
				};
				var res = await _service.AddAsyns(product, _options);
				if (res)
				{
					return Success("添加成功");
				}
				return Error("添加失败");
			}
			catch (Exception ex)
			{
				return Error(ex.Message);
			}			
		}

		public async Task<string> DeleteAsync_Del(ProductModel model)
		{
			try
			{
				var _service = Container.Instance.Resolve<IProductService>();
				var res = await _service.DeleteAsync(model.ID, _options);
				if (res)
				{
					return Success("删除成功");
				}
				return Error("删除失败");
			}
			catch (Exception ex)
			{
				return Error(ex.Message);
			}
			
		}

		public async Task<string> UpdateAsync_Edit(ProductModel model)
		{
			var _service = Container.Instance.Resolve<IProductService>();
			Product product = new Product()
			{
				Name = model.Name,
				Category = model.Category,
				Price = model.Price,
				ID = model.ID
			};
			var res = await _service.UpdateAsync(product, _options);
			if (res)
			{
				return Success("修改成功");
			}
			return Error("修改失败");
		}

		public async Task<string> GetProductList(PgaeClass model)
		{
			var _service = Container.Instance.Resolve<IProductService>();		
			var list = await _service.GetProductList(model.PageIndex, model.PageSize, _options);
			return Success(list);
		}
	}
}
