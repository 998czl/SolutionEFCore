using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EFCoreCommon;

namespace EFCoreWeb.Controllers
{
	public class BaseController : Controller
	{
		protected string Success()
		{
			return new { code = 1 }.ToJson();
		}

		//成功执行返回
		protected string Success(string message)
		{
			return new { code = 1, msg = message }.ToJson();
		}

		//成功执行返回
		protected string Success(object data)
		{
			return new { code = 1, data = data }.ToJson();
		}

		//成功执行返回
		protected string Success(int total, object list)
		{
			return new { code = 1, total = total, rows = list }.ToJson();
		}

		//layui导出成功执行返回
		protected string Success(int total, object list, object data)
		{
			return new { total = total, rows = list, footer = data }.ToJson();
		}


		//layui 交互数据格式
		protected string Success(int count, object list, int code, string message)
		{
			return new { code = code, msg = message, count = count, data = list }.ToJson();

		}

		//layui 交互数据汇总格式
		protected string Success(int count, object list, int code, string message, object listCount)
		{
			return new { code = code, data = list, msg = message, count = count, listCount = listCount }.ToJson();
		}

		//失败执行返回
		protected string Error(string message)
		{
			return new { code = 0, msg = message }.ToJson();
		}

		protected string Error(int code, string msg)
		{
			return new { code = code, msg = msg }.ToJson();
		}
	}
}
