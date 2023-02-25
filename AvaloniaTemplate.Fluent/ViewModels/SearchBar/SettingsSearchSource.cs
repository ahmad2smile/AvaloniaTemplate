using System.Collections.Generic;
using System.Reactive.Linq;
using DynamicData;
using AvaloniaTemplate.Fluent.ViewModels.SearchBar.Patterns;
using AvaloniaTemplate.Fluent.ViewModels.SearchBar.SearchItems;
using AvaloniaTemplate.Fluent.ViewModels.SearchBar.Settings;
using AvaloniaTemplate.Fluent.ViewModels.SearchBar.Sources;
using AvaloniaTemplate.Fluent.ViewModels.Settings;

namespace AvaloniaTemplate.Fluent.ViewModels.SearchBar;

public class SettingsSearchSource : ISearchSource
{
	private readonly SettingsPageViewModel _settingsPage;

	public SettingsSearchSource(SettingsPageViewModel settingsPage, IObservable<string> query)
	{
		_settingsPage = settingsPage;

		var filter = query.Select(SearchSource.DefaultFilter);

		Changes = GetSettingsItems()
			.ToObservable()
			.ToObservableChangeSet(x => x.Key)
			.Filter(filter);
	}

	public IObservable<IChangeSet<ISearchItem, ComposedKey>> Changes { get; }

	private IEnumerable<ISearchItem> GetSettingsItems()
	{
		return new ISearchItem[]
		{
			new NonActionableSearchItem(new Setting<GeneralSettingsTabViewModel, bool>(_settingsPage.GeneralSettingsTab, b => b.DarkModeEnabled), "Dark mode", "Appearance", new List<string> { "Black", "White", "Theme", "Dark", "Light" }, "nav_settings_regular") { IsDefault = false },
			new NonActionableSearchItem(new Setting<GeneralSettingsTabViewModel, bool>(_settingsPage.GeneralSettingsTab, b => b.AutoCopy), "Auto copy addresses", "Settings", new List<string>(), "nav_settings_regular") { IsDefault = false },
			new NonActionableSearchItem(new Setting<GeneralSettingsTabViewModel, bool>(_settingsPage.GeneralSettingsTab, b => b.AutoPaste), "Auto paste addresses", "Settings", new List<string>(), "nav_settings_regular") { IsDefault = false },
			new NonActionableSearchItem(new Setting<GeneralSettingsTabViewModel, bool>(_settingsPage.GeneralSettingsTab, b => b.HideOnClose), "Run in background when closed", "Settings", new List<string>() { "hide", "tray" }, "nav_settings_regular") { IsDefault = false },
			new NonActionableSearchItem(new Setting<GeneralSettingsTabViewModel, bool>(_settingsPage.GeneralSettingsTab, b => b.RunOnSystemStartup), "Run Avalonia when computer starts", "Settings", new List<string>() { "startup", "boot" }, "nav_settings_regular") { IsDefault = false },
			new NonActionableSearchItem(new Setting<GeneralSettingsTabViewModel, bool>(_settingsPage.GeneralSettingsTab, b => b.UseTor), "Network anonymization (Tor)", "Settings", new List<string>(), "nav_settings_regular") { IsDefault = false },
			new NonActionableSearchItem(new Setting<GeneralSettingsTabViewModel, bool>(_settingsPage.GeneralSettingsTab, b => b.TerminateTorOnExit), "Terminate Tor when Avalonia shuts down", "Settings", new List<string>(), "nav_settings_regular") { IsDefault = false },
			new NonActionableSearchItem(new Setting<AdvancedSettingsTabViewModel, bool>(_settingsPage.AdvancedSettingsTab, b => b.EnableGpu), "Enable GPU", "Settings", new List<string>(), "nav_settings_regular") { IsDefault = false },
		};
	}
}
