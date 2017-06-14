using System.Configuration;
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
					var localizationPath = ConfigurationManager.AppSettings["LocalizationPath"];
					_helper = string.IsNullOrEmpty(localizationPath) ? new LocalizationHelper() : new LocalizationHelper(localizationPath);
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