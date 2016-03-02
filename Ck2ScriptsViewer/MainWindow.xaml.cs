using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Ck2ScriptObjects;
using Ck2ScriptsParser;
using Ck2ScriptsParser.SyntaxUnits;
using Ck2ScriptsParser.TreeModel;
using Microsoft.Win32;
using Sprache;

namespace Ck2ScriptsViewer
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
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
                    foreach (var fn in openFileDialog.FileNames)
                    {
	                    fileName = fn;
	                    var text = File.ReadAllText(fn);
	                    var su = Parser.ListParser.Token().End().Parse(text);
	                    list.Add(new Pair(new Symbol(fileName), new Ck2ScriptsParser.SyntaxUnits.Table(su)));
                    }
                    var node = Node.FromSyntaxUnit(new Ck2ScriptsParser.SyntaxUnits.Table(list));
                    ScriptView.DataContext = node;
                }
                catch (Exception exception)
                {
                    ScriptView.DataContext = null;
	                MessageBox.Show("Error in " + fileName + Environment.NewLine + Environment.NewLine + exception, "Error");
                }
            }
	    }

		private void L_OnClick(object sender, RoutedEventArgs e)
		{
			var node = (Node) (((FrameworkElement) sender)).DataContext;
			MessageBox.Show(node.Localize(Helper));
		}
	}
}
