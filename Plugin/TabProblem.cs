using MAF_PluginView;
using System.AddIn;
using System.Windows;

namespace Plugin
{
    [AddIn(nameof(TabProblem))]
    public class TabProblem : ITabProblem
    {
        private TabProblemView view;
        private TabProblemViewModel viewModel;

        public void Dispose()
        {
            viewModel?.Dispose();
            viewModel = null;
            view = null;
        }

        public FrameworkElement GetWpfControl()
        {
            if (viewModel is null)
            {
                viewModel = new TabProblemViewModel();
            }

            if (view is null)
            {
                view = new TabProblemView { DataContext = viewModel };
            }

            return view;
        }
    }
}