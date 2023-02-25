using AvaloniaTemplate.Helpers;
using AvaloniaTemplate.Services;

namespace AvaloniaTemplate.Fluent;

public static class Services
{
	public static string DataDir { get; private set; } = null!;

	public static Config Config { get; private set; } = null!;

	public static HostedServices HostedServices { get; private set; } = null!;

	public static UiConfig UiConfig { get; private set; } = null!;

	public static SingleInstanceChecker SingleInstanceChecker { get; private set; } = null!;

	public static bool IsInitialized { get; private set; }

	/// <summary>
	/// Initializes global services used by fluent project.
	/// </summary>
	/// <param name="global">The global instance.</param>
	/// <param name="singleInstanceChecker">The singleInstanceChecker instance.</param>
	public static void Initialize(Global global, SingleInstanceChecker singleInstanceChecker)
	{
		Guard.NotNull(nameof(global.DataDir), global.DataDir);
		Guard.NotNull(nameof(global.Config), global.Config);
		Guard.NotNull(nameof(global.HostedServices), global.HostedServices);
		Guard.NotNull(nameof(global.UiConfig), global.UiConfig);

		DataDir = global.DataDir;
		Config = global.Config;
		HostedServices = global.HostedServices;
		UiConfig = global.UiConfig;
		SingleInstanceChecker = singleInstanceChecker;
		IsInitialized = true;
	}
}
