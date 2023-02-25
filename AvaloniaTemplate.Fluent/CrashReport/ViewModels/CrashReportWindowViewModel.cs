using ReactiveUI;
using System.Windows.Input;
using Avalonia;
using AvaloniaTemplate.Fluent.Helpers;
using AvaloniaTemplate.Fluent.ViewModels;
using AvaloniaTemplate.Fluent.ViewModels.HelpAndSupport;
using AvaloniaTemplate.Models;
using AvaloniaTemplate.Helpers;

namespace AvaloniaTemplate.Fluent.CrashReport.ViewModels;

public class CrashReportWindowViewModel : ViewModelBase
{
	public CrashReportWindowViewModel(SerializableException serializedException)
	{
		SerializedException = serializedException;
		CancelCommand = ReactiveCommand.Create(() => AppLifetimeHelper.Shutdown(withShutdownPrevention: false, restart: true));
		NextCommand = ReactiveCommand.Create(() => AppLifetimeHelper.Shutdown(withShutdownPrevention: false, restart: false));

		OpenGitHubRepoCommand = ReactiveCommand.CreateFromTask(async () => await IoHelpers.OpenBrowserAsync(AboutViewModel.UserSupportLink));

		CopyTraceCommand = ReactiveCommand.CreateFromTask(async () =>
		{
			if (Application.Current is { Clipboard: { } clipboard })
			{
				await clipboard.SetTextAsync(Trace);
			}
		});
	}

	public SerializableException SerializedException { get; }

	public ICommand OpenGitHubRepoCommand { get; }

	public ICommand NextCommand { get; }

	public ICommand CancelCommand { get; }

	public ICommand CopyTraceCommand { get; }

	public string Caption => $"A problem has occurred and Avalonia is unable to continue.";

	public string Trace => SerializedException.ToString();

	public string Title => "Avalonia has crashed";
}
