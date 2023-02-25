using ReactiveUI;
using AvaloniaTemplate.Fluent.ViewModels.Navigation;

namespace AvaloniaTemplate.Fluent.ViewModels;

[NavigationMetaData(Title = "Success")]
public partial class SuccessViewModel : RoutableViewModel
{
	public SuccessViewModel(string successText)
	{
		SuccessText = successText;
		NextCommand = ReactiveCommand.Create(() => Navigate().Clear());

		SetupCancel(enableCancel: false, enableCancelOnEscape: true, enableCancelOnPressed: true);
	}

	public string SuccessText { get; }
}
