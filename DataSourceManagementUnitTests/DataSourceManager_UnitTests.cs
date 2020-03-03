// **************************************************************
// * File: DataSourceManager_UnitTests.cs						*
// * Author: Kamil Wilkosz										*
// **************************************************************

using System.Collections.Generic;
using NUnit.Framework;
using System;
using Moq;
using System.IO;
using System.IO.Abstractions;
using System.Text;
using DataSourceManagement;

namespace DataSourceManagerUnitTests
{
	[TestFixture]
	public class DataSourceManager_UnitTests_should
	{
		private readonly Contact _sampleContact = new Contact()
		{
			FirstName = "John",
			LastName = "Smith",
			StreetName = "Main Road",
			HouseNumber = "21",
			ApartmentNumber = "15",
			PostalCode = "12-345",
			Town = "London",
			PhoneNumber = "123456789",
			DateOfBirth = new DateTime(2020, 2, 28, 10, 15, 10)
		};

		private readonly string _sampleContactXml = "<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n" +
													 "<ArrayOfContact xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">\r\n" +
													 "  <Contact>\r\n" +
													 "    <FirstName>John</FirstName>\r\n" +
													 "    <LastName>Smith</LastName>\r\n" +
													 "    <StreetName>Main Road</StreetName>\r\n" +
													 "    <HouseNumber>21</HouseNumber>\r\n" +
													 "    <ApartmentNumber>15</ApartmentNumber>\r\n" +
													 "    <PostalCode>12-345</PostalCode>\r\n" +
													 "    <Town>London</Town>\r\n" +
													 "    <PhoneNumber>123456789</PhoneNumber>\r\n" +
													 "    <DateOfBirth>2020-02-28T10:15:10</DateOfBirth>\r\n" +
													 "  </Contact>\r\n" +
													 "</ArrayOfContact>";

		private readonly string _invalidContactXml = "<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n" +
													 "<ArrayOfContact xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">\r\n" +
													 "  <Contact>\r\n" +
													 "  </Contact>\r\n" +
													 "</ArrayOfContact>";

		private DataSourceManager _objectUnderTests;
		private Mock<IFileSystem> _fileSystem;

		[SetUp]
		public void SetupObjectUnderTests()
		{
			_fileSystem = new Mock<IFileSystem>();
			_objectUnderTests = new DataSourceManager(_fileSystem.Object);
		}

		#region ConvertContactsToXMLText unit tests

		[Test]
		public void successfully_convert_contacts_to_XML_text()
		{
			// Arrange
			var expectedResult = _sampleContactXml;

			// Act
			var result = _objectUnderTests.ConvertContactsToXMLText(new List<Contact>() { _sampleContact });

			// Assert
			Assert.AreEqual(expectedResult, result);
		}

		#endregion

		#region ConvertXMLTextToContacts unit tests

		[Test]
		public void successfully_convert_XML_text_to_contacts()
		{
			// Arrange
			var expectedResult = new List<Contact>() { _sampleContact };

			// Act
			var result = _objectUnderTests.ConvertXMLTextToContacts(_sampleContactXml);

			// Assert
			Assert.AreEqual(expectedResult.Count, result.Count);
			Assert.AreEqual(expectedResult[0].FirstName,		result[0].FirstName);
			Assert.AreEqual(expectedResult[0].LastName,			result[0].LastName);
			Assert.AreEqual(expectedResult[0].StreetName,		result[0].StreetName);
			Assert.AreEqual(expectedResult[0].HouseNumber,		result[0].HouseNumber);
			Assert.AreEqual(expectedResult[0].ApartmentNumber,	result[0].ApartmentNumber);
			Assert.AreEqual(expectedResult[0].PostalCode,		result[0].PostalCode);
			Assert.AreEqual(expectedResult[0].Town,				result[0].Town);
			Assert.AreEqual(expectedResult[0].PhoneNumber,		result[0].PhoneNumber);
			Assert.AreEqual(expectedResult[0].DateOfBirth,		result[0].DateOfBirth);
		}

		[Test]
		public void fail_on_converting_invalid_XML_text_to_contacts_by_XML_validation()
		{
			// Arrange
			var expectedResult = new List<Contact>();

			// Act
			var result = _objectUnderTests.ConvertXMLTextToContacts(_invalidContactXml);

			// Assert
			Assert.AreEqual(expectedResult, result);
		}

		[Test]
		public void fail_on_converting_invalid_XML_text_to_contacts_by_invalid_data()
		{
			// Arrange
			var expectedResult = new List<Contact>();

			// Act
			var result = _objectUnderTests.ConvertXMLTextToContacts("invalid data");

			// Assert
			Assert.AreEqual(expectedResult, result);
		}

		#endregion

		#region ReadDataFromFile unit tests

		[Test]
		public void successfully_read_data_from_XML_file()
		{
			// Arrange
			var expectedResult = "sampleContent";
			using (var sampleContactXmlStream = new MemoryStream(Encoding.ASCII.GetBytes(expectedResult)))
			{
				_fileSystem.Setup(x => x.File.Open(It.IsAny<string>(), FileMode.Open)).Returns(sampleContactXmlStream);

				// Act
				var result = _objectUnderTests.ReadDataFromFile("sample path");

				// Assert
				Assert.AreEqual(expectedResult, result);
				_fileSystem.Verify(x => x.File.Open(It.IsAny<string>(), It.IsAny<FileMode>()), Times.Once);
			}
		}

		[Test]
		public void fail_on_reading_data_from_not_existing_XML_file()
		{
			// Arrange
			var expectedResult = string.Empty;
			_fileSystem.Setup(x => x.File.Open(It.IsAny<string>(), FileMode.Open)).Throws(new FileNotFoundException());

			// Act
			var result = _objectUnderTests.ReadDataFromFile("sample path");

			// Assert
			Assert.AreEqual(expectedResult, result);
			_fileSystem.Verify(x => x.File.Open(It.IsAny<string>(), It.IsAny<FileMode>()), Times.Once);
		}

		#endregion

		#region WriteDataToFile unit tests

		[Test]
		public void successfully_write_data_to_XML_file()
		{
			// Arrange
			_fileSystem.Setup(x => x.File.WriteAllText(It.IsAny<string>(), It.IsAny<string>()));

			// Act
			var result = _objectUnderTests.WriteDataToFile("samplePath", "sampleContent");

			// Assert
			Assert.IsTrue(result);
			_fileSystem.Verify(x => x.File.WriteAllText(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
		}

		[Test]
		public void fail_on_writing_data_to_XML_file_in_not_existing_directory()
		{
			// Arrange
			_fileSystem.Setup(x => x.File.WriteAllText(It.IsAny<string>(), It.IsAny<string>())).Throws(new DirectoryNotFoundException());

			// Act
			var result = _objectUnderTests.WriteDataToFile("samplePath", "sampleContent");

			// Assert
			Assert.IsFalse(result);
			_fileSystem.Verify(x => x.File.WriteAllText(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
		}

		#endregion

		#region LoadContactsFromFile unit tests

		[Test]
		public void successfully_load_contacts_from_XML_file()
		{
			// Arrange
			var expectedResult = new List<Contact>() { _sampleContact };
			using (var sampleContactXmlStream = new MemoryStream(Encoding.ASCII.GetBytes(_sampleContactXml)))
			{
				_fileSystem.Setup(x => x.File.Open(It.IsAny<string>(), FileMode.Open)).Returns(sampleContactXmlStream);

				// Act
				var result = _objectUnderTests.LoadContactsFromFile("samplePath");

				// Assert
				Assert.AreEqual(expectedResult.Count, result.Count);
				Assert.AreEqual(expectedResult[0].FirstName, result[0].FirstName);
				Assert.AreEqual(expectedResult[0].LastName, result[0].LastName);
				Assert.AreEqual(expectedResult[0].StreetName, result[0].StreetName);
				Assert.AreEqual(expectedResult[0].HouseNumber, result[0].HouseNumber);
				Assert.AreEqual(expectedResult[0].ApartmentNumber, result[0].ApartmentNumber);
				Assert.AreEqual(expectedResult[0].PostalCode, result[0].PostalCode);
				Assert.AreEqual(expectedResult[0].Town, result[0].Town);
				Assert.AreEqual(expectedResult[0].PhoneNumber, result[0].PhoneNumber);
				Assert.AreEqual(expectedResult[0].DateOfBirth, result[0].DateOfBirth);
				_fileSystem.Verify(x => x.File.Open(It.IsAny<string>(), It.IsAny<FileMode>()), Times.Once);
			}
		}

		[Test]
		public void successfully_return_empty_contact_list_when_XML_file_is_invalid()
		{
			// Arrange
			var expectedResult = new List<Contact>() { };
			using (var sampleContactXmlStream = new MemoryStream(Encoding.ASCII.GetBytes("invalid XML content")))
			{
				_fileSystem.Setup(x => x.File.Open(It.IsAny<string>(), FileMode.Open)).Returns(sampleContactXmlStream);

				// Act
				var result = _objectUnderTests.LoadContactsFromFile("samplePath");

				// Assert
				Assert.AreEqual(expectedResult, result);
				_fileSystem.Verify(x => x.File.Open(It.IsAny<string>(), It.IsAny<FileMode>()), Times.Once);
			}
		}

		#endregion

		#region SaveContactsToFile unit tests

		[Test]
		public void successfully_save_contacts_to_XML_file()
		{
			// Arrange
			var listToWriteIntoFile = new List<Contact>() { _sampleContact };
			_fileSystem.Setup(x => x.File.WriteAllText(It.IsAny<string>(), It.IsAny<string>()));

			// Act
			var result = _objectUnderTests.SaveContactsToFile("samplePath", listToWriteIntoFile);

			// Assert
			Assert.IsTrue(result);
			_fileSystem.Verify(x => x.File.WriteAllText(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
		}

		[Test]
		public void fail_on_saving_contacts_to_XML_file_in_not_existing_directory()
		{
			// Arrange
			var listToWriteIntoFile = new List<Contact>() { _sampleContact };
			_fileSystem.Setup(x => x.File.WriteAllText(It.IsAny<string>(), It.IsAny<string>())).Throws(new DirectoryNotFoundException());

			// Act
			var result = _objectUnderTests.SaveContactsToFile("samplePath", listToWriteIntoFile);

			// Assert
			Assert.IsFalse(result);
			_fileSystem.Verify(x => x.File.WriteAllText(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
		}

		#endregion
	}
}
