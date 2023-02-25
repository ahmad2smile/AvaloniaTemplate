using Microsoft.Win32;
using System.IO;
using System.Runtime.InteropServices;
using AvaloniaTemplate.Helpers;

namespace AvaloniaTemplate.Fluent.Helpers;

public static class WindowsStartupHelper
{
	private const string KeyPath = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";

	public static void AddOrRemoveRegistryKey(bool runOnSystemStartup)
	{
		if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
		{
			throw new InvalidOperationException("Registry modification can only be done on Windows.");
		}

		string pathToExeFile = EnvironmentHelpers.GetExecutablePath();

		string pathToExecWithArgs = $"{pathToExeFile} {StartupHelper.SilentArgument}";

		if (!File.Exists(pathToExeFile))
		{
			throw new InvalidOperationException($"Path: {pathToExeFile} does not exist.");
		}

		using RegistryKey key = Registry.CurrentUser.OpenSubKey(KeyPath, writable: true) ?? throw new InvalidOperationException("Registry operation failed.");
		if (runOnSystemStartup)
		{
			key.SetValue(nameof(AvaloniaTemplate), pathToExecWithArgs);
		}
		else
		{
			key.DeleteValue(nameof(AvaloniaTemplate), false);
		}
	}
}
