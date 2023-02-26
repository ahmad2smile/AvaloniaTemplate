# Avalonia Template

Extracted from [Wallet Wasabi](https://github.com/zkSNACKs/WalletWasabi). Stripped most of the things to make it more simple.

## Usage:

- First Windows has view from `AvaloniaTemplate.Fluent/Views/Shell/MainScreen.axaml`, which is referenced in `Shell.axaml` which is used in `../MainWindow.axaml`.
- `[AutoNotify]` is an attribute to generate some `INotify` boilerplate for `ViewModel`s property. That's why if this is used the model class should be `partial` and field it's used on should not be readonly.

```csharp
	[AutoNotify] private string _myValue = string.Empty;
```
