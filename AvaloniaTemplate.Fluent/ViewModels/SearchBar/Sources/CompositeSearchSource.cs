using System.Linq;
using System.Reactive.Linq;
using DynamicData;
using AvaloniaTemplate.Fluent.ViewModels.SearchBar.Patterns;
using AvaloniaTemplate.Fluent.ViewModels.SearchBar.SearchItems;

namespace AvaloniaTemplate.Fluent.ViewModels.SearchBar.Sources;

public class CompositeSearchSource : ISearchSource
{
	private readonly ISearchSource[] _sources;

	public CompositeSearchSource(params ISearchSource[] sources)
	{
		_sources = sources;
	}

	public IObservable<IChangeSet<ISearchItem, ComposedKey>> Changes => _sources.Select(r => r.Changes).Merge();
}
