// **************************************************************
// * File: IContact.cs											*
// * Author: Kamil Wilkosz										*
// **************************************************************

using System;
using System.ComponentModel;

namespace DataSourceManagement
{
	/// <summary>
	/// The IContact interface is implemented by class that represents
	/// user contact.
	/// </summary>
	public interface IContact : IDataErrorInfo, INotifyPropertyChanged
	{
		// Contact specific properties
		string FirstName { get; set; }
		string LastName { get; set; }
		string StreetName { get; set; }
		string HouseNumber { get; set; }
		string ApartmentNumber { get; set; }
		string PostalCode { get; set; }
		string Town { get; set; }
		string PhoneNumber { get; set; }
		DateTime DateOfBirth { get; set; }

		// Validation flag
		bool IsValid { get; }
	}
}
