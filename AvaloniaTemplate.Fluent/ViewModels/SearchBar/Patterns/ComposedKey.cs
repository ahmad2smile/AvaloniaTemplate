using System.Collections.Generic;

namespace AvaloniaTemplate.Fluent.ViewModels.SearchBar.Patterns;

public class ComposedKey : ValueObject
{
	public ComposedKey(params object[] keys)
	{
		Keys = keys;
	}

	public object[] Keys { get; }

	protected override IEnumerable<object> GetEqualityComponents()
	{
		return Keys;
	}
}
