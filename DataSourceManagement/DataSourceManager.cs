// **************************************************************
// * File: DataSourceManager.cs									*
// * Author: Kamil Wilkosz										*
// **************************************************************

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.IO.Abstractions;
using System.Xml.Linq;
using System.Xml.Schema;

namespace DataSourceManagement
{
	/// <summary>
	/// The DataSourceManager class is used to manage contacts stored
	/// in XML file. The class contains methods to:
	/// - convert XML text to/from contact list,
	/// - load contact list from XML file,
	/// - save contact list to XML file.
	/// </summary>
	public class DataSourceManager : IDataSourceManager
	{
		private IFileSystem _fileSystem;

		/// <summary>
		/// Construct a DataSourceManager, specifying the file system to be used 
		/// </summary>
		/// <param name="fileSystem">File system used by DataSourceManager</param>
		public DataSourceManager(IFileSystem fileSystem)
		{
			_fileSystem = fileSystem;
		}

		/// <summary>
		/// Construct a DataSourceManager using default file system
		/// </summary>
		public DataSourceManager()
			: this(new FileSystem())
		{ }

		#region XML text to/from contact list converters

		/// <summary>
		/// Method converts contact list to XML text.
		/// </summary>
		/// <param name="contacts">The list of contacts to be translated</param>
		/// <returns>The string containing contact list stored in XML format</returns>
		public string ConvertContactsToXMLText(List<Contact> contacts)
		{
			var xmlSerializer = new XmlSerializer(typeof(List<Contact>));
			using (var stringWriter = new StringWriter())
			{
				xmlSerializer.Serialize(stringWriter, contacts);
				return stringWriter.ToString();
			}
		}
		
		/// <summary>
		/// Method converts XML text to contact list.
		/// </summary>
		/// <param name="xmlText">The XML text with contact list</param>
		/// <returns>The list of contacts generated from XML text</returns>
		public List<Contact> ConvertXMLTextToContacts(string xmlText)
		{
			var xmlSerializer = new XmlSerializer(typeof(List<Contact>));

			try
			{
				if (!IsXmlValid(xmlText, Properties.Resources.Contacts))
				{
					return new List<Contact>();
				}

				using (var streamFromXmlText = new MemoryStream(Encoding.Unicode.GetBytes(xmlText)))
				{
					return xmlSerializer.Deserialize(streamFromXmlText) as List<Contact>;
				}
			}
			catch(Exception)
			{
				return new List<Contact>();
			}
		}
		
		/// <summary>
		/// Validates XML file against XSD file which contains validation schema.
		/// </summary>
		/// <param name="xmlText">The XML text to validate</param>
		/// <param name="xsdText">The XSD file content with validation rules</param>
		/// <returns>The boolean value. True if validation passes and false if it does not pass</returns>
		private bool IsXmlValid(string xmlText, string xsdText)
		{
			using (var streamFromXmlText = new MemoryStream(Encoding.Unicode.GetBytes(xmlText)))
			using (var streamFromXsdFile = new MemoryStream(Encoding.Unicode.GetBytes(xsdText)))
			{
				var schema = XmlSchema.Read(streamFromXsdFile, null);
				var schemaSet = new XmlSchemaSet();
				schemaSet.Add(schema);

				var xDocument = XDocument.Load(streamFromXmlText);
				var xmlIsValid = true;
				xDocument.Validate(schemaSet, (o, e) =>
				{
					xmlIsValid = false;
				});

				return xmlIsValid;
			}
		}

		#endregion

		#region Reading from file methods

		/// <summary>
		/// Reads content of text file.
		/// </summary>
		/// <param name="filePath">The path to the file which will be read</param>
		/// <returns>The string with a content of the file</returns>
		public string ReadDataFromFile(string filePath)
		{
			try
			{
				using (var fileStream = _fileSystem.File.Open(filePath, FileMode.Open))
				using (var streamReader = new StreamReader(fileStream))
				{
					return streamReader.ReadToEnd();
				}
			}
			catch(Exception)
			{
				return string.Empty;
			}
		}

		/// <summary>
		/// Loads contact list from xml file by reading it's content and converting it into list of contacts
		/// </summary>
		/// <param name="filePath">The path to the file which will be read</param>
		/// <returns>The list of contacts</returns>
		public List<Contact> LoadContactsFromFile(string filePath)
		{
			var xmlText = ReadDataFromFile(filePath);
			var contacts = ConvertXMLTextToContacts(xmlText);

			return contacts;
		}

		#endregion

		#region Writing to file methods

		/// <summary>
		/// Writes text to file
		/// </summary>
		/// <param name="filePath">The path to the file which will be written</param>
		/// <param name="textToWrite">The text to be written to the file</param>
		/// <returns>The boolean value. True if writing was successful and false if not</returns>
		public bool WriteDataToFile(string filePath, string textToWrite)
		{
			try
			{
				_fileSystem.File.WriteAllText(filePath, textToWrite);
				return true;
			}
			catch(Exception)
			{
				return false;
			}
		}

		/// <summary>
		/// Saves contact list to xml file by converting the list to xml text and writing it to the file.
		/// </summary>
		/// <param name="filePath">The path to the file which will be written</param>
		/// <param name="contacts">The list of contacts to save</param>
		/// <returns>The boolean value. True if saving was successful and false if not</returns>
		public bool SaveContactsToFile(string filePath, List<Contact> contacts)
		{
			var xmlText = ConvertContactsToXMLText(contacts);
			var savingResult = WriteDataToFile(filePath, xmlText);

			return savingResult;
		}

		#endregion
	}
}
