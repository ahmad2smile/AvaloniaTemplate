using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace AvaloniaTemplate.Fluent.Views.Settings;

public class ThemeChangeView : UserControl
{
	public ThemeChangeView()
	{
		InitializeComponent();
	}

	private void InitializeComponent()
	{
		AvaloniaXamlLoader.Load(this);
	}
}
