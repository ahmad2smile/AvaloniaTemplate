using Microsoft.Extensions.Caching.Memory;
using NBitcoin;
using Nito.AsyncEx;
using System.Threading;
using System.Threading.Tasks;
using AvaloniaTemplate.Logging;
using AvaloniaTemplate.Rpc;
using AvaloniaTemplate.Services;
using AvaloniaTemplate.Services.Terminate;

namespace AvaloniaTemplate.Fluent;

public class Global
{
	/// <remarks>Use this variable as a guard to prevent touching <see cref="StoppingCts"/> that might have already been disposed.</remarks>
	private volatile bool _disposeRequested;

	public Global(string dataDir, Config config, UiConfig uiConfig)
	{
		DataDir = dataDir;
		Config = config;
		UiConfig = uiConfig;

		HostedServices = new HostedServices();

		Cache = new MemoryCache(new MemoryCacheOptions
		{
			SizeLimit = 1_000,
			ExpirationScanFrequency = TimeSpan.FromSeconds(30)
		});
	}

	public const string ThemeBackgroundBrushResourceKey = "ThemeBackgroundBrush";
	public const string ApplicationAccentForegroundBrushResourceKey = "ApplicationAccentForegroundBrush";

	/// <summary>Lock that makes sure the application initialization and dispose methods do not run concurrently.</summary>
	private AsyncLock InitializationAsyncLock { get; } = new();

	/// <summary>Cancellation token to cancel <see cref="InitializeNoWalletAsync(TerminateService)"/> processing.</summary>
	private CancellationTokenSource StoppingCts { get; } = new();

	public string DataDir { get; }
	public Config Config { get; }
	public HostedServices HostedServices { get; }

	public UiConfig UiConfig { get; }

	public Network Network => Config.Network;

	public MemoryCache Cache { get; private set; }

	public JsonRpcServer? RpcServer { get; private set; }

	public async Task DisposeAsync()
	{
		// Dispose method may be called just once.
		if (!_disposeRequested)
		{
			_disposeRequested = true;
			StoppingCts.Cancel();
		}
		else
		{
			return;
		}

		using (await InitializationAsyncLock.LockAsync())
		{
			Logger.LogWarning("Process is exiting.", nameof(Global));

			try
			{
				try
				{
					using var dequeueCts = new CancellationTokenSource(TimeSpan.FromMinutes(6));
				}
				catch (Exception ex)
				{
				}

				if (RpcServer is { } rpcServer)
				{
					using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(21));
					await rpcServer.StopAsync(cts.Token).ConfigureAwait(false);
					Logger.LogInfo($"{nameof(RpcServer)} is stopped.", nameof(Global));
				}

				if (HostedServices is { } backgroundServices)
				{
					using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(21));
					await backgroundServices.StopAllAsync(cts.Token).ConfigureAwait(false);
					backgroundServices.Dispose();
					Logger.LogInfo("Stopped background services.");
				}
			}
			catch (Exception ex)
			{
				Logger.LogWarning(ex);
			}
			finally
			{
				StoppingCts.Dispose();
				Logger.LogTrace("Dispose finished.");
			}
		}
	}
}
