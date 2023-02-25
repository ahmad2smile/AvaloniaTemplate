using NBitcoin.Crypto;
using NBitcoin;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;
using System.Threading;
using System.Linq;

namespace AvaloniaTemplate.Helpers;

public class AvaloniaSignerHelpers
{
	public static async Task SignSha256SumsFileAsync(string sha256SumsAscFilePath, Key avaloniaPrivateKey)
	{
		var computedHash = await GetShaComputedBytesOfFileAsync(sha256SumsAscFilePath).ConfigureAwait(false);

		ECDSASignature signature = avaloniaPrivateKey.Sign(new uint256(computedHash));

		string base64Signature = Convert.ToBase64String(signature.ToDER());
		var avaloniaSignatureFilePath = Path.ChangeExtension(sha256SumsAscFilePath, "avaloniasig");

		await File.WriteAllTextAsync(avaloniaSignatureFilePath, base64Signature).ConfigureAwait(false);
	}

	public static async Task VerifySha256SumsFileAsync(string sha256SumsAscFilePath)
	{
		// Read the content file
		byte[] hash = await GetShaComputedBytesOfFileAsync(sha256SumsAscFilePath).ConfigureAwait(false);

		// Read the signature file
		var avaloniaSignatureFilePath = Path.ChangeExtension(sha256SumsAscFilePath, "avaloniasig");
		string signatureText = await File.ReadAllTextAsync(avaloniaSignatureFilePath).ConfigureAwait(false);
		byte[] sigBytes = Convert.FromBase64String(signatureText);
		var avaloniaSignature = ECDSASignature.FromDER(sigBytes);

		var pubKey = Constants.AvaloniaPubKey;

		if (!pubKey.Verify(new uint256(hash), avaloniaSignature))
		{
			throw new InvalidOperationException("Invalid avalonia signature.");
		}
	}

	public static async Task GeneratePrivateAndPublicKeyToFileAsync(string avaloniaPrivateKeyFilePath, string avaloniaPublicKeyFilePath)
	{
		if (File.Exists(avaloniaPrivateKeyFilePath))
		{
			throw new ArgumentException("Private key file already exists.");
		}

		IoHelpers.EnsureContainingDirectoryExists(avaloniaPrivateKeyFilePath);

		using Key key = new();
		await File.WriteAllTextAsync(avaloniaPrivateKeyFilePath, key.ToString(Network.Main)).ConfigureAwait(false);
		await File.WriteAllTextAsync(avaloniaPublicKeyFilePath, key.PubKey.ToString()).ConfigureAwait(false);
	}

	public static async Task<Key> GetPrivateKeyFromFileAsync(string avaloniaPrivateKeyFilePath)
	{
		string keyFileContent = await File.ReadAllTextAsync(avaloniaPrivateKeyFilePath).ConfigureAwait(false);
		BitcoinSecret secret = new(keyFileContent, Network.Main);
		return secret.PrivateKey;
	}

	public static async Task VerifyInstallerFileHashesAsync(string[] finalFiles, string sha256SumsFilePath)
	{
		string[] lines = await File.ReadAllLinesAsync(sha256SumsFilePath).ConfigureAwait(false);
		var hashWithFileNameLines = lines.Where(line => line.Contains("Avalonia-"));

		foreach (var installerFilePath in finalFiles)
		{
			string installerName = Path.GetFileName(installerFilePath);
			string installerExpectedHash = hashWithFileNameLines.Single(line => line.Contains(installerName)).Split(" ")[0];

			var bytes = await GetShaComputedBytesOfFileAsync(installerFilePath).ConfigureAwait(false);
			string installerRealHash = Convert.ToHexString(bytes).ToLower();

			if (installerExpectedHash != installerRealHash)
			{
				throw new InvalidOperationException("Installer file's hash doesn't match expected hash.");
			}
		}
	}

	/// <summary>
	/// This function returns a SHA256 computed byte array of a file on the provided file path.
	/// </summary>
	/// <exception cref="FileNotFoundException"></exception>
	public static async Task<byte[]> GetShaComputedBytesOfFileAsync(string filePath, CancellationToken cancellationToken = default)
	{
		byte[] bytes = await File.ReadAllBytesAsync(filePath, cancellationToken).ConfigureAwait(false);
		using SHA256 sha = SHA256.Create();
		byte[] computedHash = sha.ComputeHash(bytes);
		return computedHash;
	}
}
