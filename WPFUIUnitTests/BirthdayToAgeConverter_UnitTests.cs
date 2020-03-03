// **************************************************************
// * File: BirthdayToAgeConverter_UnitTests.cs					*
// * Author: Kamil Wilkosz										*
// **************************************************************

using NUnit.Framework;
using System;
using WPFUI;

namespace BirthdayToAgeConverterUnitTests
{
	[TestFixture]
	public class BirthdayToAgeConverter_UnitTests_should
	{
		private static DateTime[] _sampleBirthdayDates =
		{
			new DateTime(2010, 02, 24),
			new DateTime(1970, 08, 26),
			new DateTime(1999, 01, 01),
			new DateTime(2100, 11, 12)
		};

		[TestCaseSource("_sampleBirthdayDates")]
		public void succesfully_convert_birthday_date_to_age(DateTime birthdayDate)
		{
			// Arrange
			var _objectUnderTest = new BirthdayToAgeConverter();
			var expectedResult = DateTime.Today.Year - birthdayDate.Year;
			if (birthdayDate.Date > DateTime.Today.AddYears(-expectedResult)) expectedResult--;
			expectedResult = expectedResult >= 0 ? expectedResult : 0;

			// Act
			var result = _objectUnderTest.Convert(birthdayDate, null, null, null);

			// Assert
			Assert.AreEqual(expectedResult, result);
		}

		[Test]
		public void throw_NotImplementedException_on_calling_ConvertBack()
		{
			// Arrange
			var _objectUnderTest = new BirthdayToAgeConverter();
			
			// Assert
			Assert.Throws<NotImplementedException>(() => { _objectUnderTest.ConvertBack(null, null, null, null); });
		}
	}
}
