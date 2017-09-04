using System;
using System.Collections.Generic;
using System.Text;

namespace Nano.SssCore.Core
{
	public static class StringExtensions
	{
		public static string TrimStart(this string source, string trim, StringComparison stringComparison = StringComparison.Ordinal)
		{
			if (source == null)
				return null;
			var s = source;
			while (s.StartsWith(trim, stringComparison))
			{
				s = s.Substring(trim.Length);
			}
			return s;
		}
	}
}
