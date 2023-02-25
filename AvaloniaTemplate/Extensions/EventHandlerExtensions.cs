using AvaloniaTemplate.Logging;

namespace AvaloniaTemplate.Extensions;

public static class EventHandlerExtensions
{
	public static void SafeInvoke<T>(this EventHandler<T>? handler, object sender, T args) where T : class
	{
		try
		{
			handler?.Invoke(sender, args);
		}
		catch (Exception e)
		{
			Logger.LogError(e);
		}
	}
}
