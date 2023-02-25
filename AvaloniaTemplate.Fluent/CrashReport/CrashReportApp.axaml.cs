using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using AvaloniaTemplate.Fluent.CrashReport.ViewModels;
using AvaloniaTemplate.Models;
using AvaloniaTemplate.Fluent.CrashReport.Views;

namespace AvaloniaTemplate.Fluent.CrashReport;

public class CrashReportApp : Application
{
	private readonly SerializableException? _serializableException;

	public CrashReportApp()
	{
		Name = "Avalonia Template Crash Report";
	}

	public CrashReportApp(SerializableException exception) : this()
	{
		_serializableException = exception;
	}

	public override void Initialize()
	{
		AvaloniaXamlLoader.Load(this);
	}

	public override void OnFrameworkInitializationCompleted()
	{
		if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop && _serializableException is { })
		{
			desktop.MainWindow = new CrashReportWindow
			{
				DataContext = new CrashReportWindowViewModel(_serializableException)
			};
		}

		base.OnFrameworkInitializationCompleted();
	}
}
