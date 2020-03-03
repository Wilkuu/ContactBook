// **************************************************************
// * File: ShellViewModel_UnitTests.cs							*
// * Author: Kamil Wilkosz										*
// **************************************************************

using NUnit.Framework;
using Moq;
using WPFUI.Dialogs.DialogService;
using WPFUI.ViewModels;
using DataSourceManagement;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Windows;

namespace ShellViewModelUnitTests
{
	[TestFixture]
	public class ShellViewModel_UnitTests_should
	{
		private static readonly Contact _sampleContact = new Contact()
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

		private Mock<IDataSourceManager> _dataSourceManagerMock;
		private Mock<IDialogService> _dialogServiceMock;
		private ShellViewModel _objectUnderTests;

		[SetUp]
		public void SetupObjectUnderTest()
		{
			_dataSourceManagerMock = new Mock<IDataSourceManager>();
			_dialogServiceMock = new Mock<IDialogService>();
		}

		[Test]
		public void successfully_load_contacts_on_startup()
		{
			// Arrange
			var expectedResult = new BindableCollection<Contact>() { _sampleContact };
			_dataSourceManagerMock.Setup(x => x.LoadContactsFromFile(It.IsAny<string>())).Returns(new List<Contact> { _sampleContact });

			// Act
			_objectUnderTests = new ShellViewModel(_dataSourceManagerMock.Object, _dialogServiceMock.Object);
			var result = _objectUnderTests.Contacts;

			// Assert
			Assert.AreEqual(expectedResult, result);
			_dataSourceManagerMock.Verify(x => x.LoadContactsFromFile(It.IsAny<string>()), Times.Once);
		}

		private static object[] _changesCancellingCases =
		{
			new object[] { new List<Contact>() { _sampleContact }, DialogResult.Yes, 2 },
			new object[] { new List<Contact>() { }, DialogResult.No, 1 }
		};

		[TestCaseSource("_changesCancellingCases")]
		public void correctly_load_contacts_based_on_user_decision(List<Contact> expectedContactList, DialogResult userDecision, int amountOfLoadingCalls)
		{
			// Arrange
			var expectedResult = new BindableCollection<Contact>(expectedContactList);
			_dataSourceManagerMock.Setup(x => x.LoadContactsFromFile(It.IsAny<string>())).Returns(new Queue<List<Contact>>(new[] { new List<Contact>(), new List<Contact> { _sampleContact } }).Dequeue);
			_dialogServiceMock.Setup(x => x.OpenDialog(It.IsAny<DialogViewModelBase>(), It.IsAny<Window>())).Returns(userDecision);

			// Act
			_objectUnderTests = new ShellViewModel(_dataSourceManagerMock.Object, _dialogServiceMock.Object);
			_objectUnderTests.Cancel(null);
			var result = _objectUnderTests.Contacts;

			// Assert
			Assert.AreEqual(expectedResult, result);
			_dataSourceManagerMock.Verify(x => x.LoadContactsFromFile(It.IsAny<string>()), Times.Exactly(amountOfLoadingCalls));
			_dialogServiceMock.Verify(x => x.OpenDialog(It.IsAny<DialogViewModelBase>(), It.IsAny<Window>()), Times.Once);
		}

		private static object[] _contactSavingCases =
		{
			new object[] { DialogResult.Yes, 1 },
			new object[] { DialogResult.No, 0 }
		};

		[TestCaseSource("_contactSavingCases")]
		public void correctly_save_contacts_based_on_user_decision(DialogResult userDecision, int amountOfSavingCalls)
		{
			// Arrange
			var contactList = new List<Contact> { _sampleContact };
			_dataSourceManagerMock.Setup(x => x.LoadContactsFromFile(It.IsAny<string>())).Returns(contactList);
			_dialogServiceMock.Setup(x => x.OpenDialog(It.IsAny<DialogViewModelBase>(), It.IsAny<Window>())).Returns(userDecision);

			// Act
			_objectUnderTests = new ShellViewModel(_dataSourceManagerMock.Object, _dialogServiceMock.Object);
			_objectUnderTests.Save(null);

			// Assert
			_dataSourceManagerMock.Verify(x => x.LoadContactsFromFile(It.IsAny<string>()), Times.Once);
			_dataSourceManagerMock.Verify(x => x.SaveContactsToFile(It.IsAny<string>(), contactList), Times.Exactly(amountOfSavingCalls));
			_dialogServiceMock.Verify(x => x.OpenDialog(It.IsAny<DialogViewModelBase>(), It.IsAny<Window>()), Times.Once);
		}

		private static object[] _buttonActivityCases =
		{
			new object[] { _sampleContact, true, true },
			new object[] { new Contact(), true, false }
		};

		[TestCaseSource("_buttonActivityCases")]
		public void correctly_set_button_availability_based_on_contacts_validity(Contact contact, bool cancellingActivity, bool savingActivity)
		{
			// Arrange
			_dataSourceManagerMock.Setup(x => x.LoadContactsFromFile(It.IsAny<string>())).Returns(new List<Contact> { contact });

			// Act
			_objectUnderTests = new ShellViewModel(_dataSourceManagerMock.Object, _dialogServiceMock.Object);
			_objectUnderTests.SetCancelAndSaveActivity();

			// Assert
			Assert.AreEqual(cancellingActivity, _objectUnderTests.CancellingActive);
			Assert.AreEqual(savingActivity, _objectUnderTests.SavingActive);
			_dataSourceManagerMock.Verify(x => x.LoadContactsFromFile(It.IsAny<string>()), Times.Once);
		}
	}
}
