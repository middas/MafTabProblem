using System;
using System.Windows;

namespace MafTabProblem.Mvvm.Dialogs
{
    public interface IDialogService
    {
        bool? Show<T>(T viewModel, Func<T, Window> dialogWindowCreator) where T : IDialogModal;
    }
}