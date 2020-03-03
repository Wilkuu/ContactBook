// **************************************************************
// * File: ShellViewModel.cs									*
// * Author: Kamil Wilkosz										*
// **************************************************************

using Caliburn.Micro;
using DataSourceManagement;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using WPFUI.Dialogs.DialogService;
using WPFUI.Dialogs.DialogYesNo;

namespace WPFUI.ViewModels
{
	/// <summary>
	/// The ShellViewModel class is a view model class in MVVM pattern. It
	/// manages the contact book visualization.
	/// </summary>
	public class ShellViewModel : Screen
	{
		#region Fields and properties
		
		// Field used to store the location of XML file which contains contacts
		private string _contactsFilePath;

		// Field used to manage the saving and loading contacts to/from XML file
		private IDataSourceManager _dataSourceManager;

		private IDialogService _dialogService;

		// Save and cancel dialogs
		public ICommand SaveConfirmationDialogCommand { get; set; }
		public ICommand CancelConfirmationDialogCommand { get; set; }
		
		private BindableCollection<Contact> _contacts;
		/// <summary>
		/// Collection of contacts.
		/// </summary>
		public BindableCollection<Contact> Contacts
		{
			get	{ return _contacts;	}
			set
			{
				_contacts = value;
				NotifyOfPropertyChange(() => Contacts);
			}
		}

		private bool _savingActive;
		/// <summary>
		/// Flag indicates if contacts can or cannot be saved.
		/// </summary>
		public bool SavingActive
		{
			get { return _savingActive; }
			set
			{
				_savingActive = value;
				NotifyOfPropertyChange(() => SavingActive);
			}
		}

		private bool _cancellingActive;
		/// <summary>
		/// Flag indicates if contacts can or cannot be canceled.
		/// </summary>
		public bool CancellingActive
		{
			get	{ return _cancellingActive; }
			set
			{
				_cancellingActive = value;
				NotifyOfPropertyChange(() => CancellingActive);
			}
		}

		#endregion
		
		/// <summary>
		/// Construct a ShellViewModel.
		/// </summary>
		/// 
		public ShellViewModel(IDataSourceManager DataSourceManager, IDialogService DialogService)
		{
			_contactsFilePath = $"{ Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) }\\Contacts.xml";
			_dataSourceManager = DataSourceManager;
			_dialogService = DialogService;
			Contacts = new BindableCollection<Contact>(_dataSourceManager.LoadContactsFromFile(_contactsFilePath));
			SaveConfirmationDialogCommand = new RelayCommand(Save);
			CancelConfirmationDialogCommand = new RelayCommand(Cancel);
		}

		public ShellViewModel() :
			this(new DataSourceManager(), new DialogService())
		{ }
		
		/// <summary>
		/// Set an activity of Save and Cancel buttons.
		/// </summary>
		public void SetCancelAndSaveActivity()
		{
			CancellingActive = true;

			foreach (var contact in Contacts)
			{
				if (!contact.IsValid)
				{
					SavingActive = false;
					return;
				}
			}
			SavingActive = true;
		}

		/// <summary>
		/// Open saving dialog and save contacts in case of "Yes" or do nothing in case of "No".
		/// </summary>
		/// <param name="parameter">Specify the owner of saving dialog</param>
		public void Save(object parameter)
		{
			DialogViewModelBase dialogViewModel = new DialogYesNoViewModel("Save contacts", $"Are you sure you want to save contacts in file: { _contactsFilePath }?");
			if (_dialogService.OpenDialog(dialogViewModel, parameter as Window) == DialogResult.Yes)
			{
				_dataSourceManager.SaveContactsToFile(_contactsFilePath, Contacts.ToList());
				SavingActive = false;
				CancellingActive = false;
			}
		}

		/// <summary>
		/// Open cancelling dialog and load contacts in case of "Yes" or do nothing in case of "No".
		/// </summary>
		/// <param name="parameter">Specify the owner of cancelling dialog</param>
		public void Cancel(object parameter)
		{
			DialogViewModelBase dialogViewModel = new DialogYesNoViewModel("Cancel changes", $"Are you sure you want to cancel your changes and load contacts from file: { _contactsFilePath }?");
			if (_dialogService.OpenDialog(dialogViewModel, parameter as Window) == DialogResult.Yes)
			{
				Contacts = new BindableCollection<Contact>(_dataSourceManager.LoadContactsFromFile(_contactsFilePath));
				SavingActive = false;
				CancellingActive = false;
			}
		}
	}
}