using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace Ck2ScriptsViewer
{
	public class LocalizableToVisibilityConverter : MarkupExtension, IValueConverter
	{
		public LocalizableToVisibilityConverter()
		{
		}

		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			return this;
		}

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var node = value as Ck2ScriptsParser.TreeModel.Node;
			return node != null && node.IsLocalizable ? Visibility.Visible : Visibility.Collapsed;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}