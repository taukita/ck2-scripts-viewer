using System;
using System.Collections.Generic;
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
using Ck2ScriptsParser;
using Ck2ScriptsParser.TreeModel;
using Sprache;

namespace Ck2ScriptsViewer
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			string source = @"
	{
		#ololo comment
		a = b
		c = 5.0.12#this is comment too
		d = {d = d}
	}
";
			var c = Parser.TableParser.Token().Parse(source);
			var node = new Node("ROOT", (Ck2ScriptsParser.SyntaxUnits.Table) c);
			ScriptView.DataContext = node;
		}
	}
}
