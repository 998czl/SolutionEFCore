using Autofac;
using Autofac.Extras.DynamicProxy;
using EFCoreCommon;
using EFCoreDAL.Application.Imp;
using EFCoreDAL.Application.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFCoreDAL
{
	public class Container
	{
		private static IContainer container;

		public static void RegisterAll()
		{
			var builder = new ContainerBuilder();

			#region Interceptor

			builder.Register(o => new ExeceptionInterceptor());

			#endregion

			#region Service
			builder.RegisterType<ProductService>()
			  .EnableInterfaceInterceptors()
			  .InterceptedBy(typeof(ExeceptionInterceptor))
			  .As<IProductService>();
			#endregion

			container = builder.Build();
		}

		public static IContainer Instance
		{
			get
			{
				return container;
			}
		}
	}
}
