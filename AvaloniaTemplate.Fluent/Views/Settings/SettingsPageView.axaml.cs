using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace AvaloniaTemplate.Fluent.Views.Settings;

public class SettingsPageView : UserControl
{
	public SettingsPageView()
	{
		InitializeComponent();
	}

	private void InitializeComponent()
	{
		AvaloniaXamlLoader.Load(this);
	}
}
