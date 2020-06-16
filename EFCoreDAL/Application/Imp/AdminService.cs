using EFCoreDAL.Application.Interface;
using EFCoreDAL.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreDAL.Application.Imp
{
	public class AdminService : IAdminService
	{
		public async Task<AdminDto> Login(string mobile, string password, DbContextOptions options)
		{
			using (MyDbContext db = new MyDbContext(options))
			{
				var res = await db.administrator.FirstOrDefaultAsync(s => s.Mobile == mobile && s.UserPassword == password);
				if (res == null)
				{
					throw new Exception("用户名或密码错误!");
				}
				return new AdminDto()
				{
					UserId = res.UserId,
					UserName = res.UserName,
					ConnectionStr = res.ConnectionStr,
					Mobile = res.Mobile,
					CreateTime = res.CreateTime
				};
			}
		}



	}
}
