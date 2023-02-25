using System.ComponentModel;

namespace AvaloniaTemplate.JsonConverters;

public class DefaultValueIntegerArrayAttribute : DefaultValueAttribute
{
	public DefaultValueIntegerArrayAttribute(string json) : base(IntegerArrayJsonConverter.Parse(json))
	{
	}
}
