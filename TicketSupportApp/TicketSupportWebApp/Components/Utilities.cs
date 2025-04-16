﻿namespace Utilities
{
	public class Utils
	{
		public static Exception GetInnerException(Exception ex)
		{
			while (ex.InnerException != null)
			{
				ex = ex.InnerException;
			}
			return ex;
		}
	}
}
