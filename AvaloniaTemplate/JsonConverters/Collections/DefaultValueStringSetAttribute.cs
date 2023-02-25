using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;

namespace AvaloniaTemplate.JsonConverters.Collections;

public class DefaultValueStringSetAttribute : DefaultValueAttribute
{
	public DefaultValueStringSetAttribute(string json)
		: base(JsonConvert.DeserializeObject<ISet<string>>(json))
	{
	}
}
