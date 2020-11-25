using System;
using System.Windows;

namespace MAF_HostView
{
    public interface ITabProblem : IDisposable
    {
        FrameworkElement GetWpfControl();
    }
}