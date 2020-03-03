// **************************************************************
// * File: DialogViewModelBase.cs								*
// * Author: Kamil Wilkosz										*
// **************************************************************

using System.Windows;

namespace WPFUI.Dialogs.DialogService
{
	/// <summary>
	/// A base for all view models of all dialog types.
	/// </summary>
	public abstract class DialogViewModelBase
	{
		public DialogResult UserDialogResult { get; private set; }
		public string Title { get; private set; }
		public string Message {	get; private set; }

		/// <summary>
		/// Construct a view model of dialog.
		/// </summary>
		/// <param name="title">Dialog title</param>
		/// <param name="message">Message to be included in the dialog</param>
		public DialogViewModelBase(string title, string message)
		{
			Title = title;
			Message = message;
		}

		/// <summary>
		/// When dialog closes then returns a result.
		/// </summary>
		/// <param name="dialog">Owner of the dialog</param>
		/// <param name="result">Dialog result</param>
		public void CloseDialogWithResult(Window dialog, DialogResult result)
		{
			UserDialogResult = result;
			if (dialog != null)
			{
				dialog.DialogResult = true;
			}
		}
	}
}