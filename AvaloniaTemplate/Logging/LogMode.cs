using System.Diagnostics;

namespace AvaloniaTemplate.Logging;

public enum LogMode
{
	/// <summary>It uses <see cref="Console.Write(string)"/>.</summary>
	Console,

	/// <summary>It uses <see cref="Debug.Write(string?)"/>.</summary>
	Debug,

	/// <summary>Logs to <see cref="Logger.FilePath"/> file.</summary>
	File
}
