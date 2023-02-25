using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace AvaloniaTemplate.Fluent.Views.Dialogs;

public class ShuttingDownView : UserControl
{
	public ShuttingDownView()
	{
		InitializeComponent();
	}

	private void InitializeComponent()
	{
		AvaloniaXamlLoader.Load(this);
	}
}
