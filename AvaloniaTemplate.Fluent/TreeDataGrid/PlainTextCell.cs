using Avalonia.Controls.Models.TreeDataGrid;

namespace AvaloniaTemplate.Fluent.TreeDataGrid;

internal class PlainTextCell : ICell
{
	public PlainTextCell(string? value)
	{
		Value = value;
	}

	public bool CanEdit => false;

	public string? Value { get; }

	object? ICell.Value => Value;
}
