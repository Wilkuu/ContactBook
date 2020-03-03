// **************************************************************
// * File: Contact_UnitTests.cs									*
// * Author: Kamil Wilkosz										*
// **************************************************************

using NUnit.Framework;
using System;
using DataSourceManagement;

namespace ContactModelUnitTests
{
	[TestFixture]
	public class ContactModel_UnitTests_should
	{
		private Contact _objectUnderTest;

		[SetUp]
		public void SetupObjectUnderTests()
		{
			_objectUnderTest = new Contact()
			{
				FirstName = "John",
				LastName = "Smith",
				StreetName = "Main Road",
				HouseNumber = "21",
				ApartmentNumber = "15",
				PostalCode = "12-345",
				Town = "London",
				PhoneNumber = "123456789",
				DateOfBirth = new DateTime(2020, 2, 25, 10, 15, 10)
			};
		}

		[Test]
		public void successfully_validate_proper_contact_values()
		{
			// Act
			var result = _objectUnderTest.IsValid;

			// Assert
			Assert.IsTrue(result);
		}

		[TestCase("Mark", true)]
		[TestCase("MaRk", true)]
		[TestCase("", false)]
		[TestCase(" Mark", false)]
		[TestCase("Mark Mark", false)]
		[TestCase("Mark!", false)]
		[TestCase(" ", false)]
		[TestCase("Mark1", false)]
		[TestCase("Mark 1", false)]
		[TestCase("123", false)]
		[TestCase("M@rk", false)]
		public void successfully_validate_FirstName(string firstName, bool expectedResult)
		{
			// Arrange
			_objectUnderTest.FirstName = firstName;

			// Act
			var result = _objectUnderTest.IsValid;

			// Assert
			Assert.AreEqual(expectedResult, result);
		}

		[TestCase("Smart", true)]
		[TestCase("SmArT", true)]
		[TestCase("", false)]
		[TestCase(" Smart", false)]
		[TestCase("Smart Smart", false)]
		[TestCase("Smart!", false)]
		[TestCase(" ", false)]
		[TestCase("Smart1", false)]
		[TestCase("Smart 1", false)]
		[TestCase("123", false)]
		[TestCase("Sm@rt", false)]
		public void successfully_validate_LastName(string lastName, bool expectedResult)
		{
			// Arrange
			_objectUnderTest.LastName = lastName;

			// Act
			var result = _objectUnderTest.IsValid;

			// Assert
			Assert.AreEqual(expectedResult, result);
		}

		[TestCase("Long Avenue", true)]
		[TestCase("LoNg AvEnUe", true)]
		[TestCase("1st Avenue", true)]
		[TestCase("Read 123rd", true)]
		[TestCase("", false)]
		[TestCase(" Long Avenue", false)]
		[TestCase(" ", false)]
		[TestCase("1", false)]
		[TestCase("123", false)]
		public void successfully_validate_StreetName(string streetName, bool expectedResult)
		{
			// Arrange
			_objectUnderTest.StreetName = streetName;

			// Act
			var result = _objectUnderTest.IsValid;

			// Assert
			Assert.AreEqual(expectedResult, result);
		}

		[TestCase("1", true)]
		[TestCase("123", true)]
		[TestCase("1Aa", true)]
		[TestCase("3b", true)]
		[TestCase("A", true)]
		[TestCase("315BC", true)]
		[TestCase("", false)]
		[TestCase(" ", false)]
		[TestCase(" 1", false)]
		[TestCase(" A", false)]
		[TestCase("1A ", false)]
		[TestCase("!@^", false)]
		public void successfully_validate_HouseNumber(string houseNumber, bool expectedResult)
		{
			// Arrange
			_objectUnderTest.HouseNumber = houseNumber;

			// Act
			var result = _objectUnderTest.IsValid;

			// Assert
			Assert.AreEqual(expectedResult, result);
		}

		[TestCase("1", true)]
		[TestCase("123", true)]
		[TestCase("1Aa", true)]
		[TestCase("3b", true)]
		[TestCase("A", true)]
		[TestCase("315BC", true)]
		[TestCase("", true)]
		[TestCase(" ", false)]
		[TestCase(" 1", false)]
		[TestCase(" A", false)]
		[TestCase("1A ", false)]
		[TestCase("!@^", false)]
		public void successfully_validate_ApartmentNumber(string apartmentNumber, bool expectedResult)
		{
			// Arrange
			_objectUnderTest.ApartmentNumber = apartmentNumber;

			// Act
			var result = _objectUnderTest.IsValid;

			// Assert
			Assert.AreEqual(expectedResult, result);
		}

		[TestCase("60-130", true)]
		[TestCase("858645", true)]
		[TestCase("", false)]
		[TestCase("60_130", false)]
		[TestCase(" 858645", false)]
		[TestCase("-60-130", false)]
		[TestCase("60-130-", false)]
		[TestCase("6o-13o", false)]
		[TestCase("word", false)]
		[TestCase("1a", false)]
		[TestCase("?&%", false)]
		public void successfully_validate_PostalCode(string postalCode, bool expectedResult)
		{
			// Arrange
			_objectUnderTest.PostalCode = postalCode;

			// Act
			var result = _objectUnderTest.IsValid;

			// Assert
			Assert.AreEqual(expectedResult, result);
		}

		[TestCase("New York", true)]
		[TestCase("NewYork", true)]
		[TestCase("NeWyOrK", true)]
		[TestCase("1st York", true)]
		[TestCase("York 123", true)]
		[TestCase(" New York", false)]
		[TestCase("9999", false)]
		[TestCase(" ", false)]
		[TestCase("?&%", false)]
		public void successfully_validate_Town(string town, bool expectedResult)
		{
			// Arrange
			_objectUnderTest.Town = town;

			// Act
			var result = _objectUnderTest.IsValid;

			// Assert
			Assert.AreEqual(expectedResult, result);
		}

		[TestCase("444555666", true)]
		[TestCase("123", true)]
		[TestCase("95648", true)]
		[TestCase("", false)]
		[TestCase(" ", false)]
		[TestCase("444-555-666", false)]
		[TestCase("444 555 666", false)]
		[TestCase("444aaa666", false)]
		[TestCase(" 444555666", false)]
		[TestCase("444555666 ", false)]
		[TestCase("a", false)]
		[TestCase("?&%", false)]
		public void successfully_validate_PhoneNumber(string phoneNumber, bool expectedResult)
		{
			// Arrange
			_objectUnderTest.PhoneNumber = phoneNumber;

			// Act
			var result = _objectUnderTest.IsValid;

			// Assert
			Assert.AreEqual(expectedResult, result);
		}
	}
}
