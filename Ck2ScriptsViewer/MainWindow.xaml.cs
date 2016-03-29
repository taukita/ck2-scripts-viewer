using System.Threading.Tasks;
using System.Windows;
using Ck2ScriptObjects;
using Ck2ScriptsParser.TreeModel;
using Ck2ScriptsViewer.Filters;

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
			DataContext = new MainViewModel();
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
	}
}