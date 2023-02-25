using System.Collections.Generic;
using AvaloniaTemplate.Models;

namespace AvaloniaTemplate.Extensions;

public static class ImmutableValueSequenceExtensions
{
	public static ImmutableValueSequence<T> ToImmutableValueSequence<T>(this IEnumerable<T> list) where T : IEquatable<T>
		=> new(list);
}
