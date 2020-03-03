// **************************************************************
// * File: DialogYesNoViewModel.cs								*
// * Author: Kamil Wilkosz										*
// **************************************************************

using System.Windows;
using System.Windows.Input;
using WPFUI.Dialogs.DialogService;

namespace WPFUI.Dialogs.DialogYesNo
{
	/// <summary>
	/// The view model of yes/no dialog typ.
	/// </summary>
	class DialogYesNoViewModel : DialogViewModelBase
	{
		// Yes and No command properties
		public ICommand YesCommand { get; set; }
		public ICommand NoCommand { get; set; }

		/// <summary>
		/// Construct the view model of yes/no dialog.
		/// </summary>
		/// <param name="title">Dialog title</param>
		/// <param name="message">Message to be included in the dialog</param>
		public DialogYesNoViewModel(string title, string message)
			: base(title, message)
		{
			YesCommand = new RelayCommand(OnYesClicked);
			NoCommand = new RelayCommand(OnNoClicked);
		}

		/// <summary>
		/// Dialog when closed with "Yes" decision returns "Yes" result.
		/// </summary>
		/// <param name="parameter">Owner of the dialog</param>
		private void OnYesClicked(object parameter)
		{
			CloseDialogWithResult(parameter as Window, DialogResult.Yes);
		}

		/// <summary>
		/// Dialog when closed with "No" decision returns "No" result.
		/// </summary>
		/// <param name="parameter">Owner of the dialog</param>
		private void OnNoClicked(object parameter)
		{
			CloseDialogWithResult(parameter as Window, DialogResult.No);
		}
	}
}
