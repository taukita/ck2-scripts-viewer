using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using Ck2ScriptObjects;

namespace Ck2ScriptsViewer
{
	public class NodeToTextConverter : MarkupExtension, IValueConverter
	{
		public NodeToTextConverter()
		{
		}

		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			return this;
		}

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var node = value as Ck2ScriptsParser.TreeModel.Node;
			return node != null && !node.Metadata.IsRoot && !node.Metadata.IsFile
				? node.TryLocalize(Application.Current.Properties[typeof (LocalizationHelper)] as LocalizationHelper)
				: string.Empty;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}