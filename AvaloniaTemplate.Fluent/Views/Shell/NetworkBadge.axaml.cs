using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace AvaloniaTemplate.Fluent.Views.Shell;

public class NetworkBadge : UserControl
{
	public NetworkBadge()
	{
		InitializeComponent();
	}

	private void InitializeComponent()
	{
		AvaloniaXamlLoader.Load(this);
	}
}
