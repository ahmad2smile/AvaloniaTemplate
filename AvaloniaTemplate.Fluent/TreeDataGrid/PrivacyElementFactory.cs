using Avalonia.Controls;
using Avalonia.Controls.Primitives;

namespace AvaloniaTemplate.Fluent.TreeDataGrid;

internal class PrivacyElementFactory : TreeDataGridElementFactory
{
	protected override IControl CreateElement(object? data)
	{
		return data is PrivacyTextCell ?
			new TreeDataGridPrivacyTextCell() :
			base.CreateElement(data);
	}

	protected override string GetDataRecycleKey(object data)
	{
		return data is PrivacyTextCell ?
			typeof(TreeDataGridPrivacyTextCell).FullName! :
			base.GetDataRecycleKey(data);
	}
}
