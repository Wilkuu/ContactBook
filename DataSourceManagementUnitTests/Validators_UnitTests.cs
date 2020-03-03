// **************************************************************
// * File: Validators_UnitTests.cs								*
// * Author: Kamil Wilkosz										*
// **************************************************************

using NUnit.Framework;
using DataSourceManagement;

namespace ValdatorFactoryUnitTests
{
	[TestFixture]
	class ValdatorFactory_UnitTests_should
	{
		private static object[] _validatorCases =
		{
			new object[] { "FirstName", new FirstNameValidator() },
			new object[] { "LastName", new LastNameValidator() },
			new object[] { "StreetName", new StreetNameValidator() },
			new object[] { "HouseNumber", new HouseNumberValidator() },
			new object[] { "ApartmentNumber", new ApartmentNumberValidator() },
			new object[] { "PostalCode", new PostalCodeValidator() },
			new object[] { "Town", new TownValidator() },
			new object[] { "PhoneNumber", new PhoneNumberValidator() }
		};
		
		[TestCaseSource("_validatorCases")]
		public void return_correct_validator_based_on_property_name(string propertyName, PropertyValidator validator)
		{
			// Arrange
			var expectedResult = validator.GetType();

			// Act
			var result = ValidatorFactory.getValidator(propertyName).GetType();
			
			// Assert
			Assert.AreEqual(expectedResult, result);
		}

		[Test]
		public void return_null_on_incorrect_property_name()
		{
			// Act
			var result = ValidatorFactory.getValidator("invalid property name");

			// Assert
			Assert.IsNull(result);
		}
	}
}