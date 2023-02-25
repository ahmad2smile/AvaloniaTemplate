using System.Linq;
using Avalonia;
using Avalonia.Markup.Xaml.Styling;

namespace AvaloniaTemplate.Fluent.Helpers;

public enum Theme
{
	Dark,
	Light
}

public static class ThemeHelper
{
	public static Theme CurrentTheme { get; private set; }

	public static void ApplyTheme(Theme theme)
	{
		if (Application.Current is { })
		{
			var currentTheme = Application.Current.Styles.Select(x => (StyleInclude)x)
				.FirstOrDefault(x => x.Source is { } && x.Source.AbsolutePath.Contains("Themes"));

			if (currentTheme is { })
			{
				var themeIndex = Application.Current.Styles.IndexOf(currentTheme);

				var newTheme = new StyleInclude(new Uri("avares://AvaloniaTemplate.Fluent/App.axaml"))
				{
					Source = new Uri($"avares://AvaloniaTemplate.Fluent/Styles/Themes/Base{theme}.axaml")
				};

				CurrentTheme = theme;
				Application.Current.Styles[themeIndex] = newTheme;
			}
		}
	}
}
