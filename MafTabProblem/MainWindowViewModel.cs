using MAF_HostView;
using MafTabProblem.Mvvm;
using MafTabProblem.Mvvm.Dialogs;
using MafTabProblem.Plugin;
using System.AddIn.Hosting;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MafTabProblem
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly IDialogService dialogService;
        private bool loadInAppDomain;
        private AddInToken plugin;
        private bool pluginsLoaded;
        private string pluginState;

        public MainWindowViewModel()
        {
            dialogService = new DialogService();
            LoadPlugins();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public bool LoadInAppDomain
        {
            get => loadInAppDomain;
            set
            {
                loadInAppDomain = value;

                NotifyPropertyChanged(nameof(LoadInAppDomain));
            }
        }

        public ICommand OpenPluginCommand { get => new DelegateCommand(OpenPlugin); }

        public bool PluginsLoaded
        {
            get => pluginsLoaded;
            set
            {
                pluginsLoaded = value;

                NotifyPropertyChanged(nameof(PluginsLoaded));
            }
        }

        public string PluginState
        {
            get => pluginState;
            set
            {
                pluginState = value;

                NotifyPropertyChanged(nameof(PluginState));
            }
        }

        private void LoadPlugins()
        {
            PluginState = "Loading";

            Task.Run(() =>
            {
                var pluginHelper = new PluginHelper();
                var plugins = pluginHelper.GetRegisteredPlugins();

                if (plugins.Any())
                {
                    plugin = plugins.First();

                    PluginsLoaded = true;
                    PluginState = "Ready";
                }
            });
        }

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OpenPlugin()
        {
            if (plugin != null)
            {
                var pluginHelper = new PluginHelper();
                using (var p = pluginHelper.ActivateUninitializedPlugin<ITabProblem>(plugin, LoadInAppDomain))
                {
                    dialogService.Show(new PluginWindowViewModel(p.Plugin.GetWpfControl()), (vm) => new PluginWindow { DataContext = vm });
                }
            }
        }
    }
}