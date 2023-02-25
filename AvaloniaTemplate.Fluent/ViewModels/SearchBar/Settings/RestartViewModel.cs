using System.Windows.Input;
using ReactiveUI;
using AvaloniaTemplate.Fluent.Helpers;

namespace AvaloniaTemplate.Fluent.ViewModels.SearchBar.Settings;

public class RestartViewModel : ViewModelBase
{
	public RestartViewModel(string message)
	{
		Message = message;
		RestartCommand = ReactiveCommand.Create(() => AppLifetimeHelper.Shutdown(withShutdownPrevention: true, restart: true));
	}

	public string Message { get; }

	public ICommand RestartCommand { get; }
}
