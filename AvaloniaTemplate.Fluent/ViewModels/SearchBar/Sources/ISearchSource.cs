using DynamicData;
using AvaloniaTemplate.Fluent.ViewModels.SearchBar.Patterns;
using AvaloniaTemplate.Fluent.ViewModels.SearchBar.SearchItems;

namespace AvaloniaTemplate.Fluent.ViewModels.SearchBar.Sources;

public interface ISearchSource
{
	IObservable<IChangeSet<ISearchItem, ComposedKey>> Changes { get; }
}
