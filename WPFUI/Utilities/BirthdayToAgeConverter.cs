// **************************************************************
// * File: BirthdayToAgeConverter.cs							*
// * Author: Kamil Wilkosz										*
// **************************************************************

using System;
using System.Globalization;
using System.Windows.Data;

namespace WPFUI
{
	/// <summary>
	/// A converter used to translate date of birth to age.
	/// </summary>
	[ValueConversion(typeof(DateTime), typeof(int))]
	public class BirthdayToAgeConverter : IValueConverter
	{
		/// <summary>
		/// Static instance of converter.
		/// </summary>
		public static BirthdayToAgeConverter Instance = new BirthdayToAgeConverter();

		/// <summary>
		/// Convert date of birth to age based on current date.
		/// </summary>
		/// <param name="value">Date of birth value</param>
		/// <param name="targetType">The type of the binding target property</param>
		/// <param name="parameter">The converter parameter to use</param>
		/// <param name="culture">The culture to use in the converter</param>
		/// <returns></returns>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var dateOfBirth = (DateTime)value;
			var currentDate = DateTime.Today;

			if(dateOfBirth == null)
			{
				return null;
			}

			var age = currentDate.Year - dateOfBirth.Year;
			if (dateOfBirth.Date > currentDate.AddYears(-age)) age--;

			return age >= 0 ? age : 0;
		}

		/// <summary>
		/// The backward convertion is not required at the moment. Throws NotImplementedException.
		/// </summary>
		/// <returns></returns>
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
