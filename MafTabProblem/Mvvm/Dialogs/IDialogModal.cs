using System;

namespace MafTabProblem.Mvvm.Dialogs
{
    public interface IDialogModal
    {
        event EventHandler<DialogButtonEventArgs> ButtonPressed;
    }
}