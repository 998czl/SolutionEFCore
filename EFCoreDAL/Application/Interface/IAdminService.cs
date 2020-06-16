using EFCoreDAL.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreDAL.Application.Interface
{
	public interface IAdminService
	{		
		Task<AdminDto> Login(string mobile, string password, DbContextOptions options);
	}
}
