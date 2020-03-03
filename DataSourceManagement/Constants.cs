// **************************************************************
// * File: Constants.cs											*
// * Author: Kamil Wilkosz										*
// **************************************************************

namespace DataSourceManagement
{
	/// <summary>
	/// Constants used for Contact class.
	/// </summary>
	static class Constants
	{
		internal static class PropertyName
		{
			internal const string FirstName			= "FirstName";
			internal const string LastName			= "LastName";
			internal const string StreetName		= "StreetName";
			internal const string HouseNumber		= "HouseNumber";
			internal const string ApartmentNumber	= "ApartmentNumber";
			internal const string PostalCode		= "PostalCode";
			internal const string Town				= "Town";
			internal const string PhoneNumber		= "PhoneNumber";
			internal const string DateOfBirth		= "DateOfBirth";
		}

		internal static class ValidationRule
		{
			internal const string FirstName			= @"^[a-zA-Z]+$";
			internal const string LastName			= @"^[a-zA-Z]+$";
			internal const string StreetName		= @"^\S[a-zA-Z\d ]*[a-zA-Z]+[a-zA-Z\d ]*$";
			internal const string HouseNumber		= @"^[a-zA-Z\d]+$";
			internal const string ApartmentNumber	= @"^[a-zA-Z\d]*$";
			internal const string PostalCode		= @"^\d+-*\d+$";
			internal const string Town				= @"^\S[a-zA-Z\d ]*[a-zA-Z]+[a-zA-Z\d ]*$";
			internal const string PhoneNumber		= @"^\d+$";
		}

		internal static class ValidationFailureMessage
		{
			internal const string FirstName			= "First name must contain only letters.";
			internal const string LastName			= "Last name must contain only letters.";
			internal const string StreetName		= "Street name must contain letters. It may contain digits and spaces. It can not start with space.";
			internal const string HouseNumber		= "House number must contain only digits and/or letters.";
			internal const string ApartmentNumber	= "Apartment number can be empty or contain only digits and/or letters.";
			internal const string PostalCode		= "Postal code must contains digits. It may contain \"-\" sign but cannot start or end with it.";
			internal const string Town				= "Town must contain letters. It may contain digits and spaces. It can not start with space.";
			internal const string PhoneNumber		= "Phone number must contain only digits.";
		}
	}
}
