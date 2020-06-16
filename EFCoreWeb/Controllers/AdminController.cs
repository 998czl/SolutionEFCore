using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using EFCoreDAL;
using EFCoreDAL.Application.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCoreWeb.Controllers
{
	public class AdminController : BaseController
	{
		private DbContextOptions _options { get; set; }
		public AdminController(DbContextOptions options)
		{
			_options = options;
		}

		public IActionResult Index()
		{
			return View();
		}
		
		public async Task<string> Login(string mobile, string password)
		{
			try
			{
				var _service = Container.Instance.Resolve<IAdminService>();
				var res = await _service.Login(mobile, password, _options);
				//CurrentUser = res;
				return Success(res);
			}
			catch (Exception ex)
			{
				return Error(ex.Message);
			}
			
		}

	}
}
