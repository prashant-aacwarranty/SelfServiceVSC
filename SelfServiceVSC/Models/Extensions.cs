using System.Text.RegularExpressions;

namespace AAC.SelfServiceVSC.Models
{
	public static class Extensions
	{
		/// <summary>
		/// Format a phone number.
		/// </summary>
		/// <param name="number">The original number.</param>
		/// <returns>Formatted string.</returns>
		public static String FormatPhone(
			this String number)
		{
			return Regex.Replace(number, @"(\d)?(\d{3})(\d{3})(\d{4})", @"($2) $3-$4");
		}

		/// <summary>
		/// Get substring of specified number of characters on the right.
		/// </summary>
		public static string Right(this String value, int length)
		{
			if (String.IsNullOrEmpty(value)) return String.Empty;

			return value.Length <= length ? value : value.Substring(value.Length - length);
		}
	}
}
