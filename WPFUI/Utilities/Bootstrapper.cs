// **************************************************************
// * File: Bootsrapper.cs										*
// * Author: Kamil Wilkosz										*
// **************************************************************

using Caliburn.Micro;
using System.Windows;
using WPFUI.ViewModels;

namespace WPFUI
{
	/// <summary>
	/// Bootstrapper required to start visualization based on Caliburn Micro.
	/// </summary>
	public class Bootstrapper : BootstrapperBase
	{
		public Bootstrapper()
		{
			Initialize();
		}

		protected override void OnStartup(object sender, StartupEventArgs e)
		{
			DisplayRootViewFor<ShellViewModel>();
		}
	}
}
