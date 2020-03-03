// **************************************************************
// * File: DialogService.cs										*
// * Author: Kamil Wilkosz										*
// **************************************************************

using System.Windows;

namespace WPFUI.Dialogs.DialogService
{
	/// <summary>
	/// Possible dialog results.
	/// </summary>
	public enum DialogResult
	{
		Undefined,
		Yes,
		No
	}

	/// <summary>
	/// Provide dialog handling when opened.
	/// </summary>
	public class DialogService : IDialogService
	{
		/// <summary>
		/// Open dialog of specific type based on view model.
		/// </summary>
		/// <param name="viewModel">View model of a dialog in use</param>
		/// <param name="owner">The owner of the dialog</param>
		/// <returns></returns>
		public DialogResult OpenDialog(DialogViewModelBase viewModel, Window owner)
		{
			DialogWindow dialog = new DialogWindow();
			if (owner != null)
			{
				dialog.Owner = owner;
			}
			dialog.DataContext = viewModel;
			dialog.ShowDialog();
			DialogResult result = (dialog.DataContext as DialogViewModelBase).UserDialogResult;
			return result;
		}
	}
}
