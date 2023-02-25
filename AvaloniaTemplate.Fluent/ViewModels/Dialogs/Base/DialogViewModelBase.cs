using AvaloniaTemplate.Fluent.ViewModels.NavBar;

namespace AvaloniaTemplate.Fluent.ViewModels.Dialogs.Base;

/// <summary>
/// CommonBase class.
/// </summary>
public abstract partial class DialogViewModelBase : NavBarItemViewModel
{
	[AutoNotify] private bool _isDialogOpen;
}
