using System.Windows.Input;
using AvaloniaTemplate.Fluent.ViewModels.Navigation;

namespace AvaloniaTemplate.Fluent.ViewModels;

public abstract class TriggerCommandViewModel : RoutableViewModel
{
	public abstract ICommand TargetCommand { get; }
}
