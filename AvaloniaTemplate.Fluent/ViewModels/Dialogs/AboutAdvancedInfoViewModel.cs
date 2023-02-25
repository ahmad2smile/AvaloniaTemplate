using System.Reactive;
using AvaloniaTemplate.Fluent.ViewModels.Dialogs.Base;
using AvaloniaTemplate.Helpers;

namespace AvaloniaTemplate.Fluent.ViewModels.Dialogs;

[NavigationMetaData(Title = "About")]
public partial class AboutAdvancedInfoViewModel : DialogViewModelBase<Unit>
{
	public AboutAdvancedInfoViewModel()
	{
		SetupCancel(enableCancel: false, enableCancelOnEscape: true, enableCancelOnPressed: true);

		EnableBack = false;

		NextCommand = CancelCommand;
	}

	public Version BitcoinCoreVersion => Constants.BitcoinCoreVersion;

	public Version HwiVersion => Constants.HwiVersion;

	public string BackendCompatibleVersions => Constants.ClientSupportBackendVersionText;

	public string CurrentBackendMajorVersion => "0.0.1";

	protected override void OnDialogClosed()
	{
	}
}
