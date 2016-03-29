using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Ck2ScriptsViewer
{
	public class TreeViewEx : TreeView
	{
		public TreeViewEx()
		{
			SelectedItemChanged += OnSelectedItemChanged;
		}

		private void OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> routedPropertyChangedEventArgs)
		{
			if (SelectedItem != null)
			{
				SetValue(SelectedItemExProperty, SelectedItem);
			}
		}

		public object SelectedItemEx
		{
			get { return GetValue(SelectedItemExProperty); }
			set { SetValue(SelectedItemExProperty, value); }
		}

		public static readonly DependencyProperty SelectedItemExProperty = DependencyProperty.Register(
			"SelectedItemEx",
			typeof (object),
			typeof (TreeViewEx),
			new UIPropertyMetadata(null));
	}
}
