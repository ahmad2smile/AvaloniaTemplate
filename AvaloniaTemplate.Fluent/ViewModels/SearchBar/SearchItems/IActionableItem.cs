using System.Threading.Tasks;

namespace AvaloniaTemplate.Fluent.ViewModels.SearchBar.SearchItems;

public interface IActionableItem : ISearchItem
{
	Func<Task> OnExecution { get; }
}
