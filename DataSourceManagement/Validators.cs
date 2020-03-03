// **************************************************************
// * File: Validators.cs										*
// * Author: Kamil Wilkosz										*
// **************************************************************

using System.Text.RegularExpressions;

namespace DataSourceManagement
{
	/// <summary>
	/// The PropertyValidator abstract class is implemented by property specific
	/// validators.
	/// </summary>
	public abstract class PropertyValidator
	{
		/// <summary>
		/// Validation of prtoperty value based on regex rule return failure
		/// message in case of validation failure.
		/// </summary>
		/// <param name="propertyValue">Value of validated property</param>
		/// <param name="regexRule">Regex rule for validation process</param>
		/// <param name="failureMessage">Message returned when validation fails</param>
		/// <returns>Error message if validation fails</returns>
		protected string ValidateProperty(string propertyValue, string regexRule, string failureMessage)
		{
			if (propertyValue == null || new Regex(regexRule).Matches(propertyValue).Count != 1)
			{
				return failureMessage;
			}
			return null;
		}

		/// <summary>
		/// Validation method will be overwritten by property specific validators.
		/// </summary>
		/// <param name="contact">Contact to be validated</param>
		/// <returns>Error message if validation fails</returns>
		public abstract string Validate(Contact contact);
	}

	/// <summary>
	/// First name property validator.
	/// </summary>
	public class FirstNameValidator : PropertyValidator
	{
		/// <summary>
		/// Validates first name of given contact.
		/// </summary>
		/// <param name="contact">Contact whose first name will be validated</param>
		/// <returns>Error message if validation fails</returns>
		public override string Validate(Contact contact)
		{
			return ValidateProperty(contact.FirstName, Constants.ValidationRule.FirstName, Constants.ValidationFailureMessage.FirstName);
		}
	}

	/// <summary>
	/// Last name property validator.
	/// </summary>
	public class LastNameValidator : PropertyValidator
	{
		/// <summary>
		/// Validates last name of given contact.
		/// </summary>
		/// <param name="contact">Contact whose last name will be validated</param>
		/// <returns>Error message if validation fails</returns>
		public override string Validate(Contact contact)
		{
			return ValidateProperty(contact.LastName, Constants.ValidationRule.LastName, Constants.ValidationFailureMessage.LastName);
		}
	}

	/// <summary>
	/// Street name property validator.
	/// </summary>
	public class StreetNameValidator : PropertyValidator
	{
		/// <summary>
		/// Validates street name of given contact.
		/// </summary>
		/// <param name="contact">Contact whose street name will be validated</param>
		/// <returns>Error message if validation fails</returns>
		public override string Validate(Contact contact)
		{
			return ValidateProperty(contact.StreetName, Constants.ValidationRule.StreetName, Constants.ValidationFailureMessage.StreetName);
		}
	}

	/// <summary>
	/// House number property validator.
	/// </summary>
	public class HouseNumberValidator : PropertyValidator
	{
		/// <summary>
		/// Validates house number of given contact.
		/// </summary>
		/// <param name="contact">Contact whose house number will be validated</param>
		/// <returns>Error message if validation fails</returns>
		public override string Validate(Contact contact)
		{
			return ValidateProperty(contact.HouseNumber, Constants.ValidationRule.HouseNumber, Constants.ValidationFailureMessage.HouseNumber);
		}
	}

	/// <summary>
	/// Apartment number property validator.
	/// </summary>
	public class ApartmentNumberValidator : PropertyValidator
	{
		/// <summary>
		/// Validates apartment number of given contact.
		/// </summary>
		/// <param name="contact">Contact whose apartment number will be validated</param>
		/// <returns>Error message if validation fails</returns>
		public override string Validate(Contact contact)
		{
			return ValidateProperty(contact.ApartmentNumber, Constants.ValidationRule.ApartmentNumber, Constants.ValidationFailureMessage.ApartmentNumber);
		}
	}

	/// <summary>
	/// Postal code property validator.
	/// </summary>
	public class PostalCodeValidator : PropertyValidator
	{
		/// <summary>
		/// Validates postal code of given contact.
		/// </summary>
		/// <param name="contact">Contact whose postal code will be validated</param>
		/// <returns>Error message if validation fails</returns>
		public override string Validate(Contact contact)
		{
			return ValidateProperty(contact.PostalCode, Constants.ValidationRule.PostalCode, Constants.ValidationFailureMessage.PostalCode);
		}
	}

	/// <summary>
	/// Town property validator.
	/// </summary>
	public class TownValidator : PropertyValidator
	{
		/// <summary>
		/// Validates town of given contact.
		/// </summary>
		/// <param name="contact">Contact whose town will be validated</param>
		/// <returns>Error message if validation fails</returns>
		public override string Validate(Contact contact)
		{
			return ValidateProperty(contact.Town, Constants.ValidationRule.Town, Constants.ValidationFailureMessage.Town);
		}
	}

	/// <summary>
	/// Phone number property validator.
	/// </summary>
	public class PhoneNumberValidator : PropertyValidator
	{
		/// <summary>
		/// Validates phone number of given contact.
		/// </summary>
		/// <param name="contact">Contact whose phone number will be validated</param>
		/// <returns>Error message if validation fails</returns>
		public override string Validate(Contact contact)
		{
			return ValidateProperty(contact.PhoneNumber, Constants.ValidationRule.PhoneNumber, Constants.ValidationFailureMessage.PhoneNumber);
		}
	}

	/// <summary>
	/// Validator factory which returns a proper validator based on property name.
	/// </summary>
	public static class ValidatorFactory
	{
		/// <summary>
		/// Returns validator based on property name.
		/// </summary>
		/// <param name="propertyName">Property name to be validated by returned validator</param>
		/// <returns>Validator suitable to contact property</returns>
		public static PropertyValidator getValidator(string propertyName)
		{
			switch(propertyName)
			{
				case Constants.PropertyName.FirstName:
					{
						return new FirstNameValidator();
					}
				case Constants.PropertyName.LastName:
					{
						return new LastNameValidator();
					}
				case Constants.PropertyName.StreetName:
					{
						return new StreetNameValidator();
					}
				case Constants.PropertyName.HouseNumber:
					{
						return new HouseNumberValidator();
					}
				case Constants.PropertyName.ApartmentNumber:
					{
						return new ApartmentNumberValidator();
					}
				case Constants.PropertyName.PostalCode:
					{
						return new PostalCodeValidator();
					}
				case Constants.PropertyName.Town:
					{
						return new TownValidator();
					}
				case Constants.PropertyName.PhoneNumber:
					{
						return new PhoneNumberValidator();
					}
				default:
					{
						return null;
					}
			}
		}
	}

}
