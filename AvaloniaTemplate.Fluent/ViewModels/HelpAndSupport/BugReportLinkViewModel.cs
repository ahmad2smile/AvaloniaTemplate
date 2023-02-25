using System.Windows.Input;
using ReactiveUI;
using AvaloniaTemplate.Helpers;

namespace AvaloniaTemplate.Fluent.ViewModels.HelpAndSupport;

[NavigationMetaData(
	Title = "Report a Bug",
	Caption = "Open Avalonia's GitHub issues website",
	Order = 1,
	Category = "Help & Support",
	Keywords = new[]
	{
		"Support", "Website", "Bug", "Report"
	},
	IconName = "bug_regular")]
public partial class BugReportLinkViewModel : TriggerCommandViewModel
{
	public override ICommand TargetCommand =>
		ReactiveCommand.CreateFromTask(async () => await IoHelpers.OpenBrowserAsync(AboutViewModel.BugReportLink));
}
