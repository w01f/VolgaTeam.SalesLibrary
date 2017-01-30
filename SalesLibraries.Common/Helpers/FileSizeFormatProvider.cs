using System;

namespace SalesLibraries.Common.Helpers
{
	public class FileSizeFormatProvider : IFormatProvider, ICustomFormatter
	{
		public object GetFormat(Type formatType)
		{
			return formatType == typeof(ICustomFormatter) ? this : null;
		}

		private const string FileSizeFormat = "fs";
		private const decimal OneKiloByte = 1024M;
		private const decimal OneMegaByte = OneKiloByte * 1024M;
		private const decimal OneGigaByte = OneMegaByte * 1024M;

		public string Format(string format, object arg, IFormatProvider formatProvider)
		{
			if (format == null || !format.StartsWith(FileSizeFormat))
			{
				return DefaultFormat(format, arg, formatProvider);
			}

			if (arg is string)
			{
				return DefaultFormat(format, arg, formatProvider);
			}

			decimal size;

			try
			{
				size = Convert.ToDecimal(arg);
			}
			catch (InvalidCastException)
			{
				return DefaultFormat(format, arg, formatProvider);
			}

			string suffix;
			if (size > OneGigaByte)
			{
				size /= OneGigaByte;
				suffix = "Gb";
			}
			else if (size > OneMegaByte)
			{
				size /= OneMegaByte;
				suffix = "Mb";
			}
			else if (size > OneKiloByte)
			{
				size /= OneKiloByte;
				suffix = "Kb";
			}
			else
			{
				suffix = "b";
			}

			var precision = format.Substring(2);
			if (String.IsNullOrEmpty(precision)) precision = "2";
			return String.Format("{0:N" + precision + "}{1}", size, suffix);
		}

		private static string DefaultFormat(string format, object arg, IFormatProvider formatProvider)
		{
			var formattableArg = arg as IFormattable;
			return formattableArg?.ToString(format, formatProvider) ?? arg.ToString();
		}
	}
}
