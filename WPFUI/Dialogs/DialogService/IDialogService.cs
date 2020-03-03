using System.Windows;

namespace WPFUI.Dialogs.DialogService
{
	public interface IDialogService
	{
		DialogResult OpenDialog(DialogViewModelBase viewModel, Window owner);
	}
}
