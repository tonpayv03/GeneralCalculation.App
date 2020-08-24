using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CalculationBytes.Utility
{
	public static class ValidateFormat
	{
		public static bool OnlyNumber(this string value)
		{
			var check = Regex.IsMatch(value, @"^[0-9.]*$");
			return check;
		}
	}
}
