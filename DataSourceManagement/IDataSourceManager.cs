// **************************************************************
// * File: IDataSourceManager.cs								*
// * Author: Kamil Wilkosz										*
// **************************************************************

using System.Collections.Generic;

namespace DataSourceManagement
{
	/// <summary>
	/// The IDataSourceManager interface is implemented by a class
	/// that is able to read and write contacts from XML file
	/// </summary>
	public interface IDataSourceManager
	{
		#region XML text to/from contact list converters

		/// <summary>
		/// Method converts contact list to XML text.
		/// </summary>
		/// <param name="contacts">The list of contacts to be translated</param>
		/// <returns>The string containing contact list stored in XML format</returns>
		string ConvertContactsToXMLText(List<Contact> contacts);

		/// <summary>
		/// Method converts XML text to contact list.
		/// </summary>
		/// <param name="xmlText">The XML text with contact list</param>
		/// <returns>The list of contacts generated from XML text</returns>
		List<Contact> ConvertXMLTextToContacts(string xmlText);

		#endregion

		#region Reading from file methods

		/// <summary>
		/// Reads content of text file.
		/// </summary>
		/// <param name="filePath">The path to the file which will be read</param>
		/// <returns>The string with a content of the file</returns>
		string ReadDataFromFile(string filePath);

		/// <summary>
		/// Loads contact list from xml file by reading it's content and converting it into list of contacts
		/// </summary>
		/// <param name="filePath">The path to the file which will be read</param>
		/// <returns>The list of contacts</returns>
		List<Contact> LoadContactsFromFile(string filePath);

		#endregion

		#region Writing to file methods

		/// <summary>
		/// Writes text to file
		/// </summary>
		/// <param name="filePath">The path to the file which will be written</param>
		/// <param name="textToWrite">The text to be written to the file</param>
		/// <returns>The boolean value. True if writing was successful and false if not</returns>
		bool WriteDataToFile(string filePath, string textToWrite);

		/// <summary>
		/// Saves contact list to xml file by converting the list to xml text and writing it to the file.
		/// </summary>
		/// <param name="filePath">The path to the file which will be written</param>
		/// <param name="contacts">The list of contacts to save</param>
		/// <returns>The boolean value. True if saving was successful and false if not</returns>
		bool SaveContactsToFile(string filePath, List<Contact> contacts);

		#endregion
	}
}
