using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EFCoreWeb.Controllers
{
	public class ImgUploadController : BaseController
	{
		private IWebHostEnvironment hostingEnv;
		public ImgUploadController(IWebHostEnvironment env)
		{
			hostingEnv = env;
		}

		public IActionResult Index()
		{
			return View();
		}


		/// <summary>
		/// 上传图片
		/// </summary>
		/// <returns></returns>
		public string UploadImg(List<IFormFile> files)
		{
			if (files.Count < 1)
			{
				return Error("文件为空");
			}
			//返回的文件地址
			List<string> filenames = new List<string>();
			var now = DateTime.Now;
			//文件存储路径
			var filePath = string.Format("/Uploads/{0}/{1}/{2}/", now.ToString("yyyy"), now.ToString("yyyyMM"), now.ToString("yyyyMMdd"));
			//获取当前web目录
			var webRootPath = hostingEnv.WebRootPath;
			if (!Directory.Exists(webRootPath + filePath))
			{
				Directory.CreateDirectory(webRootPath + filePath);
			}
			try
			{
				foreach (var item in files)
				{
					if (item != null)
					{
						#region  图片文件的条件判断
						//文件后缀
						var fileExtension = Path.GetExtension(item.FileName);

						//判断后缀是否是图片
						const string fileFilt = ".gif|.jpg|.jpeg|.png";
						if (fileExtension == null)
						{
							break;
						}
						if (fileFilt.IndexOf(fileExtension.ToLower(), StringComparison.Ordinal) <= -1)
						{
							break;
						}

						//判断文件大小    
						long length = item.Length;
						if (length > 1024 * 1024 * 5) //2M
						{
							break;
						}

						#endregion
						var strDateTime = DateTime.Now.ToString("yyMMddhhmmssfff"); //取得时间字符串
						var strRan = Convert.ToString(new Random().Next(100, 999)); //生成三位随机数
						var saveName = strDateTime + strRan + fileExtension;

						//插入图片数据                 
						using (FileStream fs = System.IO.File.Create(webRootPath + filePath + saveName))
						{
							item.CopyTo(fs);
							fs.Flush();
						}
						filenames.Add(filePath + saveName);
					}
				}
				return Success(filenames);
			}
			catch (Exception ex)
			{
				return Error(ex.Message);
			}
		}

	}
}
