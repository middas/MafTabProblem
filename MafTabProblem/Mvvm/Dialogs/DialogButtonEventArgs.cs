using System;

namespace MafTabProblem.Mvvm.Dialogs
{
    public class DialogButtonEventArgs : EventArgs
    {
        public DialogButtonEventArgs(bool canceled)
        {
            Canceled = canceled;
        }

        public bool Canceled { get; set; }
    }
}