using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using AvaloniaTemplate.Fluent.Screenshot;

namespace AvaloniaTemplate.Fluent.Views;

public class MainWindow : Window
{
	public MainWindow()
	{
		InitializeComponent();
	}

	private void InitializeComponent()
	{
		AvaloniaXamlLoader.Load(this);
#if DEBUG
		this.AttachDevTools();
		this.AttachCapture();
#endif
	}
}
