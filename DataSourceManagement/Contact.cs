// **************************************************************
// * File: Contact.cs											*
// * Author: Kamil Wilkosz										*
// **************************************************************

using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DataSourceManagement
{
    public class Contact : IContact
	{
		#region Fields and properties

		private string _firstName = string.Empty;
		/// <summary>
		/// Contact first name property.
		/// </summary>
		public string FirstName
		{
			get { return _firstName; }
			set
			{
				_firstName = value;
				NotifyPropertyChanged(Constants.PropertyName.FirstName);
			}
		}

		private string _lastName = string.Empty;
		/// <summary>
		/// Contact last name property.
		/// </summary>
		public string LastName
		{
			get { return _lastName; }
			set
			{
				_lastName = value;
				NotifyPropertyChanged(Constants.PropertyName.LastName);
			}
		}

		private string _streetName = string.Empty;
		/// <summary>
		/// Contact street name property.
		/// </summary>
		public string StreetName
		{
			get { return _streetName; }
			set
			{
				_streetName = value;
				NotifyPropertyChanged(Constants.PropertyName.StreetName);
			}
		}

		private string _houseNumber = string.Empty;
		/// <summary>
		/// Contact house number property.
		/// </summary>
		public string HouseNumber
		{
			get { return _houseNumber; }
			set
			{
				_houseNumber = value;
				NotifyPropertyChanged(Constants.PropertyName.HouseNumber);
			}
		}

		private string _apartmentNumber = string.Empty;
		/// <summary>
		/// Contact apartment number property.
		/// </summary>
		public string ApartmentNumber
		{
			get { return _apartmentNumber; }
			set
			{
				_apartmentNumber = value;
				NotifyPropertyChanged(Constants.PropertyName.ApartmentNumber);
			}
		}

		private string _postalCode = string.Empty;
		/// <summary>
		/// Contact postal code property.
		/// </summary>
		public string PostalCode
		{
			get { return _postalCode; }
			set
			{
				_postalCode = value;
				NotifyPropertyChanged(Constants.PropertyName.PostalCode);
			}
		}

		private string _town = string.Empty;
		/// <summary>
		/// Contact town property.
		/// </summary>
		public string Town
		{
			get { return _town; }
			set
			{
				_town = value;
				NotifyPropertyChanged(Constants.PropertyName.Town);
			}
		}

		private string _phoneNumber = string.Empty;
		/// <summary>
		/// Contact phone number property.
		/// </summary>
		public string PhoneNumber
		{
			get { return _phoneNumber; }
			set
			{
				_phoneNumber = value;
				NotifyPropertyChanged(Constants.PropertyName.PhoneNumber);
			}
		}

		private DateTime _dateOfBirth;
		/// <summary>
		/// Contact date of birth property.
		/// </summary>
		public DateTime DateOfBirth
		{
			get { return _dateOfBirth; }
			set
			{
				_dateOfBirth = value;
				NotifyPropertyChanged(Constants.PropertyName.DateOfBirth);
			}
		}

		#endregion

		/// <summary>
		/// Construct a Contact with default date of birth.
		/// </summary>
		public Contact()
		{
			DateOfBirth = DateTime.Today;
		}

		#region INotifyPropertyChanged Members

		public event PropertyChangedEventHandler PropertyChanged;

		private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		#endregion

		#region IDataErrorInfo Members

		string IDataErrorInfo.Error
		{
			get
			{
				return null;
			}
		}

		string IDataErrorInfo.this[string propertyName]
		{
			get
			{
				return GetValidationError(propertyName);
			}
		}

		#endregion

		#region Validation

		/// <summary>
		/// Contains collection of property names which are validated.
		/// </summary>
		static readonly string[] ValidatedProperties =
		{
			Constants.PropertyName.FirstName,
			Constants.PropertyName.LastName,
			Constants.PropertyName.StreetName,
			Constants.PropertyName.HouseNumber,
			Constants.PropertyName.ApartmentNumber,
			Constants.PropertyName.PostalCode,
			Constants.PropertyName.Town,
			Constants.PropertyName.PhoneNumber
		};

		/// <summary>
		/// Validates contact. Returns True if contact properties are valid and False if they are not.
		/// </summary>
		public bool IsValid
		{
			get
			{
				foreach (var property in ValidatedProperties)
				{
					if (GetValidationError(property) != null)
					{
						return false;
					}
				}
				return true;
			}
		}

		/// <summary>
		/// Validates the Contact property indicated by propertyName argument. Returns error message when validation fails.
		/// </summary>
		/// <param name="propertyName">Contact's property name to be validated</param>
		/// <returns>Error message in case there is a validation error</returns>
		string GetValidationError(string propertyName)
		{
			var propertyValidator = ValidatorFactory.getValidator(propertyName);
			return propertyValidator != null ? propertyValidator.Validate(this) : null;
		}

		#endregion
	}
}
