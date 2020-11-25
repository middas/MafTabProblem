using MafTabProblem.Mvvm;
using MafTabProblem.Mvvm.Dialogs;
using System;
using System.Windows;
using System.Windows.Input;

namespace MafTabProblem
{
    public class PluginWindowViewModel : IDialogModal
    {
        public PluginWindowViewModel()
        {
        }

        public PluginWindowViewModel(FrameworkElement pluginUi)
            : this()
        {
            PluginUI = pluginUi;
        }

        public event EventHandler<DialogButtonEventArgs> ButtonPressed;

        public ICommand CloseCommand { get => new DelegateCommand(() => ButtonPressed?.Invoke(this, new DialogButtonEventArgs(false))); }

        public FrameworkElement PluginUI { get; set; }
    }
}