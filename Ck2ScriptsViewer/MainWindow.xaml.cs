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
		}

	    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
	    {
            var openFileDialog = new OpenFileDialog {Multiselect = true};
	        if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    var list = new List<SyntaxUnit>();
                    foreach (var su in openFileDialog.FileNames.Select(File.ReadAllText).Select(text => Parser.ListParser.Token().End().Parse(text)))
                    {
                        list.AddRange(su);
                    }
                    var node = Node.FromSyntaxUnit(new Ck2ScriptsParser.SyntaxUnits.Table(list));
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
