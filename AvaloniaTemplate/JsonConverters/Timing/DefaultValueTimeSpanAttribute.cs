using System.ComponentModel;

namespace AvaloniaTemplate.JsonConverters.Timing;

public class DefaultValueTimeSpanAttribute : DefaultValueAttribute
{
	public DefaultValueTimeSpanAttribute(string json) : base(TimeSpanJsonConverter.Parse(json))
	{
	}
}
