using System;
using System.AddIn.Pipeline;
using System.Windows;

namespace MAF_PluginView
{
    [AddInBase]
    public interface ITabProblem : IDisposable
    {
        FrameworkElement GetWpfControl();
    }
}