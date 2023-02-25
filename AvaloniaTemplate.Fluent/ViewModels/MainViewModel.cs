using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Avalonia.Controls;
using NBitcoin;
using ReactiveUI;
using AvaloniaTemplate.Fluent.ViewModels.Dialogs.Base;
using AvaloniaTemplate.Fluent.ViewModels.HelpAndSupport;
using AvaloniaTemplate.Fluent.ViewModels.NavBar;
using AvaloniaTemplate.Fluent.ViewModels.Navigation;
using AvaloniaTemplate.Fluent.ViewModels.OpenDirectory;
using AvaloniaTemplate.Fluent.ViewModels.SearchBar;
using AvaloniaTemplate.Fluent.ViewModels.SearchBar.Sources;
using AvaloniaTemplate.Fluent.ViewModels.Settings;

namespace AvaloniaTemplate.Fluent.ViewModels;

public partial class MainViewModel : ViewModelBase
{
	private readonly SettingsPageViewModel _settingsPage;
	private readonly PrivacyModeViewModel _privacyMode;
	[AutoNotify] private DialogScreenViewModel _dialogScreen;
	[AutoNotify] private DialogScreenViewModel _fullScreen;
	[AutoNotify] private DialogScreenViewModel _compactDialogScreen;
	[AutoNotify] private NavBarViewModel _navBar;
	[AutoNotify] private string _title = "Avalonia Template";
	[AutoNotify] private WindowState _windowState;
	[AutoNotify] private bool _isOobeBackgroundVisible;
	[AutoNotify] private bool _isCoinJoinActive;

	public string DemoText { get; set; } = "Demo Text Here";

	public MainViewModel()
	{
		ApplyUiConfigWindowSate();

		_dialogScreen = new DialogScreenViewModel();
		_fullScreen = new DialogScreenViewModel(NavigationTarget.FullScreen);
		_compactDialogScreen = new DialogScreenViewModel(NavigationTarget.CompactDialogScreen);
		MainScreen = new TargettedNavigationStack(NavigationTarget.HomeScreen);
		NavigationState.Register(MainScreen, DialogScreen, FullScreen, CompactDialogScreen);

		UiServices.Initialize();

		_settingsPage = new SettingsPageViewModel();
		_privacyMode = new PrivacyModeViewModel();
		_navBar = new NavBarViewModel();

		NavigationManager.RegisterType(_navBar);
		RegisterViewModels();

		RxApp.MainThreadScheduler.Schedule(async () => await _navBar.InitialiseAsync());

		this.WhenAnyValue(x => x.WindowState)
			.Where(state => state != WindowState.Minimized)
			.ObserveOn(RxApp.MainThreadScheduler)
			.Subscribe(state => Services.UiConfig.WindowState = state.ToString());

		IsMainContentEnabled = this.WhenAnyValue(
				x => x.DialogScreen.IsDialogOpen,
				x => x.FullScreen.IsDialogOpen,
				x => x.CompactDialogScreen.IsDialogOpen,
				(dialogIsOpen, fullScreenIsOpen, compactIsOpen) => !(dialogIsOpen || fullScreenIsOpen || compactIsOpen))
			.ObserveOn(RxApp.MainThreadScheduler);

		this.WhenAnyValue(
				x => x.DialogScreen.CurrentPage,
				x => x.CompactDialogScreen.CurrentPage,
				x => x.FullScreen.CurrentPage,
				x => x.MainScreen.CurrentPage,
				(dialog, compactDialog, fullScreenDialog, mainScreen) => compactDialog ?? dialog ?? fullScreenDialog ?? mainScreen)
			.WhereNotNull()
			.ObserveOn(RxApp.MainThreadScheduler)
			.Do(page => page.SetActive())
			.Subscribe();

		IsOobeBackgroundVisible = Services.UiConfig.Oobe;

		RxApp.MainThreadScheduler.Schedule(() =>
		{
			IsOobeBackgroundVisible = false;
		});

		SearchBar = CreateSearchBar();

		NetworkBadgeName = Services.Config.Network == Network.Main ? "" : Services.Config.Network.Name;
	}

	public IObservable<bool> IsMainContentEnabled { get; }

	public string NetworkBadgeName { get; }

	public TargettedNavigationStack MainScreen { get; }

	public SearchBarViewModel SearchBar { get; }

	public static MainViewModel Instance { get; } = new();

	public bool IsBusy =>
		MainScreen.CurrentPage is { IsBusy: true } ||
		DialogScreen.CurrentPage is { IsBusy: true } ||
		FullScreen.CurrentPage is { IsBusy: true } ||
		CompactDialogScreen.CurrentPage is { IsBusy: true };

	public void ClearStacks()
	{
		MainScreen.Clear();
		DialogScreen.Clear();
		FullScreen.Clear();
		CompactDialogScreen.Clear();
	}

	public void Initialize()
	{
		if (Services.Config.Network != Network.Main)
		{
			Title += $" - {Services.Config.Network}";
		}
	}

	private void RegisterViewModels()
	{
		PrivacyModeViewModel.Register(_privacyMode);
		SettingsPageViewModel.Register(_settingsPage);

		GeneralSettingsTabViewModel.RegisterLazy(() =>
		{
			_settingsPage.SelectedTab = 0;
			return _settingsPage;
		});

		AdvancedSettingsTabViewModel.RegisterLazy(() =>
		{
			_settingsPage.SelectedTab = 2;
			return _settingsPage;
		});

		AboutViewModel.RegisterLazy(() => new AboutViewModel());
		UserSupportViewModel.RegisterLazy(() => new UserSupportViewModel());
		BugReportLinkViewModel.RegisterLazy(() => new BugReportLinkViewModel());
		DocsLinkViewModel.RegisterLazy(() => new DocsLinkViewModel());
		OpenDataFolderViewModel.RegisterLazy(() => new OpenDataFolderViewModel());
		OpenLogsViewModel.RegisterLazy(() => new OpenLogsViewModel());
		OpenConfigFileViewModel.RegisterLazy(() => new OpenConfigFileViewModel());
	}

	public void ApplyUiConfigWindowSate()
	{
		WindowState = (WindowState)Enum.Parse(typeof(WindowState), Services.UiConfig.WindowState);
	}

	[System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "CA2000:Dispose objects before losing scope", Justification = "Same lifecycle as the application. Won't be disposed separately.")]
	private SearchBarViewModel CreateSearchBar()
	{
		// This subject is created to solve the circular dependency between the sources and SearchBarViewModel
		var filterChanged = new Subject<string>();

		var source = new CompositeSearchSource(
			new ActionsSearchSource(filterChanged),
			new SettingsSearchSource(_settingsPage, filterChanged));

		var searchBar = new SearchBarViewModel(source.Changes);

		searchBar
			.WhenAnyValue(a => a.SearchText)
			.WhereNotNull()
			.Subscribe(filterChanged);

		return searchBar;
	}
}
