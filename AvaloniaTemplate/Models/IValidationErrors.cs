namespace AvaloniaTemplate.Models;

public interface IValidationErrors
{
	void Add(ErrorSeverity severity, string error);
}
