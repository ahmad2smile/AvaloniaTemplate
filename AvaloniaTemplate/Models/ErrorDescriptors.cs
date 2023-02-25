using System.Collections.Generic;

namespace AvaloniaTemplate.Models;

public class ErrorDescriptors : List<ErrorDescriptor>, IValidationErrors
{
	public static ErrorDescriptors Empty = Create();

	private ErrorDescriptors() : base()
	{
	}

	public static ErrorDescriptors Create()
	{
		return new ErrorDescriptors();
	}

	void IValidationErrors.Add(ErrorSeverity severity, string error)
	{
		Add(new ErrorDescriptor(severity, error));
	}
}
