using System.Text.Json.Serialization;

namespace AvaloniaTemplate.Packager;

/// <summary>
/// Contains information about environment and source code that was used to produce a Avalonia Template build.
/// </summary>
public class BuildInfo
{
	[JsonConstructor]
	public BuildInfo(string netRuntimeVersion, string netSdkVersion, string gitCommitHash)
	{
		NetRuntimeVersion = netRuntimeVersion;
		NetSdkVersion = netSdkVersion;
		GitCommitHash = gitCommitHash;
	}

	[JsonPropertyName("NetRuntimeVersion")]
	public string NetRuntimeVersion { get; }

	[JsonPropertyName("NetSdkVersion")]
	public string NetSdkVersion { get; }

	/// <summary>Git commit hash corresponding with the code that was used to produce a Avalonia Template build.</summary>
	[JsonPropertyName("GitCommitHash")]
	public string GitCommitHash { get; }
}
