using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace AvaloniaTemplate.Fluent.Views.Shell;

public class MainScreen : UserControl
{
	public MainScreen()
	{
		InitializeComponent();
	}

	private void InitializeComponent()
	{
		AvaloniaXamlLoader.Load(this);
	}
}
