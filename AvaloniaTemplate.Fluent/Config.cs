using NBitcoin;
using Newtonsoft.Json;
using System.ComponentModel;
using AvaloniaTemplate.Bases;
using AvaloniaTemplate.Exceptions;
using AvaloniaTemplate.Helpers;
using AvaloniaTemplate.JsonConverters;
using AvaloniaTemplate.Models;

namespace AvaloniaTemplate.Fluent;

[JsonObject(MemberSerialization.OptIn)]
public class Config : ConfigBase
{
	public const int DefaultJsonRpcServerPort = 37128;
	public static readonly Money DefaultDustThreshold = Money.Coins(Constants.DefaultDustThreshold);

	private Uri? _backendUri;
	private Uri? _coordinatorUri;

	/// <summary>
	/// Constructor for config population using Newtonsoft.JSON.
	/// </summary>
	public Config() : base()
	{
		ServiceConfiguration = null!;
	}

	public Config(string filePath) : base(filePath)
	{
	}

	[JsonProperty(PropertyName = "Network")]
	[JsonConverter(typeof(NetworkJsonConverter))]
	public Network Network { get; internal set; } = Network.Main;

	[DefaultValue("https://avaloniawallet.io/")]
	[JsonProperty(PropertyName = "MainNetBackendUri", DefaultValueHandling = DefaultValueHandling.Populate)]
	public string MainNetBackendUri { get; private set; } = "https://avaloniawallet.io/";

	[DefaultValue("https://avaloniawallet.co/")]
	[JsonProperty(PropertyName = "TestNetClearnetBackendUri", DefaultValueHandling = DefaultValueHandling.Populate)]
	public string TestNetBackendUri { get; private set; } = "https://avaloniawallet.co/";

	[DefaultValue("http://localhost:37127/")]
	[JsonProperty(PropertyName = "RegTestBackendUri", DefaultValueHandling = DefaultValueHandling.Populate)]
	public string RegTestBackendUri { get; private set; } = "http://localhost:37127/";

	[JsonProperty(PropertyName = "MainNetCoordinatorUri", DefaultValueHandling = DefaultValueHandling.Ignore)]
	public string? MainNetCoordinatorUri { get; private set; }

	[JsonProperty(PropertyName = "TestNetCoordinatorUri", DefaultValueHandling = DefaultValueHandling.Ignore)]
	public string? TestNetCoordinatorUri { get; private set; }

	[JsonProperty(PropertyName = "RegTestCoordinatorUri", DefaultValueHandling = DefaultValueHandling.Ignore)]
	public string? RegTestCoordinatorUri { get; private set; }

	[DefaultValue(true)]
	[JsonProperty(PropertyName = "UseTor", DefaultValueHandling = DefaultValueHandling.Populate)]
	public bool UseTor { get; internal set; } = true;

	[DefaultValue(false)]
	[JsonProperty(PropertyName = "TerminateTorOnExit", DefaultValueHandling = DefaultValueHandling.Populate)]
	public bool TerminateTorOnExit { get; internal set; } = false;

	[DefaultValue(true)]
	[JsonProperty(PropertyName = "DownloadNewVersion", DefaultValueHandling = DefaultValueHandling.Populate)]
	public bool DownloadNewVersion { get; internal set; } = true;

	[DefaultValue(false)]
	[JsonProperty(PropertyName = "JsonRpcServerEnabled", DefaultValueHandling = DefaultValueHandling.Populate)]
	public bool JsonRpcServerEnabled { get; internal set; }

	[DefaultValue("")]
	[JsonProperty(PropertyName = "JsonRpcUser", DefaultValueHandling = DefaultValueHandling.Populate)]
	public string JsonRpcUser { get; internal set; } = "";

	[DefaultValue("")]
	[JsonProperty(PropertyName = "JsonRpcPassword", DefaultValueHandling = DefaultValueHandling.Populate)]
	public string JsonRpcPassword { get; internal set; } = "";

	[JsonProperty(PropertyName = "JsonRpcServerPrefixes")]
	public string[] JsonRpcServerPrefixes { get; internal set; } = new[]
	{
			"http://127.0.0.1:37128/",
			"http://localhost:37128/"
		};

	[JsonProperty(PropertyName = "EnableGpu")]
	public bool EnableGpu { get; internal set; } = true;

	[DefaultValue("CoinJoinCoordinatorIdentifier")]
	[JsonProperty(PropertyName = "CoordinatorIdentifier", DefaultValueHandling = DefaultValueHandling.Populate)]
	public string CoordinatorIdentifier { get; set; } = "CoinJoinCoordinatorIdentifier";

	public ServiceConfiguration ServiceConfiguration { get; private set; }

	public Uri GetBackendUri()
	{
		if (_backendUri is { })
		{
			return _backendUri;
		}

		if (Network == Network.Main)
		{
			_backendUri = new Uri(MainNetBackendUri);
		}
		else if (Network == Network.TestNet)
		{
			_backendUri = new Uri(TestNetBackendUri);
		}
		else if (Network == Network.RegTest)
		{
			_backendUri = new Uri(RegTestBackendUri);
		}
		else
		{
			throw new NotSupportedNetworkException(Network);
		}

		return _backendUri;
	}

	public Uri GetCoordinatorUri()
	{
		if (_coordinatorUri is { })
		{
			return _coordinatorUri;
		}

		var result = Network switch
		{
			{ } n when n == Network.Main => MainNetCoordinatorUri,
			{ } n when n == Network.TestNet => TestNetCoordinatorUri,
			{ } n when n == Network.RegTest => RegTestCoordinatorUri,
			_ => throw new NotSupportedNetworkException(Network)
		};

		_coordinatorUri = result is null ? GetBackendUri() : new Uri(result);
		return _coordinatorUri;
	}
}
