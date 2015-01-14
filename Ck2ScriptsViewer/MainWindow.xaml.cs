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
		public MainWindow()
		{
			InitializeComponent();
			string source = @"
	{
		#ololo comment
		a = b
		c = 5.0.12#this is comment too
		d = {d = d}
		r = { sy }
	}
";
			var c = Parser.TableParser.Token().Parse(source);
			var node = new Node("ROOT", (Ck2ScriptsParser.SyntaxUnits.Table) c);
			ScriptView.DataContext = node;
		}

	    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
	    {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    var text = File.ReadAllText(openFileDialog.FileName);
                    var su = Parser.ListParser.Token().End().Parse(text);
                    var node = Node.FromSyntaxUnit(new Ck2ScriptsParser.SyntaxUnits.Table(su));
                    ScriptView.DataContext = node;
                }
                catch (Exception exception)
                {
                    ScriptView.DataContext = null;
                    MessageBox.Show(exception.ToString(), "Error");
                }
            }
	    }
	}
}
