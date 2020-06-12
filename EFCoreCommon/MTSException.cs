using System;
using System.Collections.Generic;
using System.Text;

namespace EFCoreCommon
{
	public class MTSException : ApplicationException
	{
		public MTSException(string message) : base(message)
		{
		}
	}
}
