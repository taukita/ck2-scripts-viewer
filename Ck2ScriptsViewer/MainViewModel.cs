using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Ck2ScriptsParser;
using Ck2ScriptsParser.SyntaxUnits;
using Ck2ScriptsParser.TreeModel;
using Ck2ScriptsViewer.Filters;
using Ck2ScriptsViewer.MicroMvvm;
using Microsoft.Win32;
using Sprache;

namespace Ck2ScriptsViewer
{
	internal class MainViewModel : ObservableObject
	{
		private Node _root;

		public MainViewModel()
		{
			EditCommand = new RelayCommand(Edit);
			FilterCommand = new RelayCommand(Filter);
			LoadCommand = new RelayCommand(Load);
		}

		public Node Root
		{
			get { return _root; }
			set
			{
				if (value != _root)
				{
					_root = value;
					RaisePropertyChanged();
				}
			}
		}

		public Node SelectedNode { get; set; }

		public ICommand LoadCommand { get; private set; }

		public ICommand EditCommand { get; private set; }

		public ICommand FilterCommand { get; private set; }

		private void Filter()
		{
			var node = SelectedNode ?? Root;
			if (node != null)
			{
				INodeFilter filter = new PictureFilter();
				var filtered = filter.Filter(node.Children);
				Root = new Node("FILTERED", filtered);
			}			
		}

		private void Edit()
		{
			if (SelectedNode != null && !SelectedNode.Metadata.IsFile)
			{
				var nodeWindow = new NodeWindow
				{
					Owner = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive),
					DataContext = SelectedNode
				};
				nodeWindow.ShowDialog();
			}			
		}

		private void Load()
		{
			var openFileDialog = new OpenFileDialog { Multiselect = true };
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

					Root = node;
				}
				catch (Exception exception)
				{
					Root = null;
					MessageBox.Show("Error in " + fileName + Environment.NewLine + Environment.NewLine + exception, "Error");
				}
			}			
		}
	}
}
