using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using Ck2ScriptObjects;
using Ck2ScriptsParser;
using Ck2ScriptsParser.SyntaxUnits;
using Ck2ScriptsParser.TreeModel;
using Microsoft.Win32;
using Sprache;

namespace Ck2ScriptsViewer
{
	/// <summary>
	///     Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private readonly object _sync = new object();
		private LocalizationHelper _helper;

		public MainWindow()
		{
			InitializeComponent();
			Task.Factory.StartNew(() =>
			{
				lock (_sync)
				{
					_helper = new LocalizationHelper(@"E:\SteamLibrary\steamapps\common\Crusader Kings II\localisation\");
					Application.Current.Properties[typeof (LocalizationHelper)] = _helper;
				}
			});
		}

		internal LocalizationHelper Helper
		{
			get
			{
				lock (_sync)
				{
					return _helper;
				}
			}
		}

		private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
		{
			var openFileDialog = new OpenFileDialog {Multiselect = true};
			if (openFileDialog.ShowDialog() == true)
			{
				string fileName = string.Empty;
				try
				{
					var list = new List<SyntaxUnit>();
					foreach (string fn in openFileDialog.FileNames)
					{
						fileName = fn;
						string text = File.ReadAllText(fn);
						List<SyntaxUnit> su = Parser.ListParser.Token().End().Parse(text);
						list.Add(new Pair(new Symbol(fileName), new Table(su)));
					}
					Node node = Node.FromSyntaxUnit(new Table(list));

					//set up metadata
					node.Metadata.IsRoot = true;
					foreach (Node child in node.Children)
					{
						child.Metadata.IsFile = true;
					}

					ScriptView.DataContext = node;
				}
				catch (Exception exception)
				{
					ScriptView.DataContext = null;
					MessageBox.Show("Error in " + fileName + Environment.NewLine + Environment.NewLine + exception, "Error");
				}
			}
		}
	}
}