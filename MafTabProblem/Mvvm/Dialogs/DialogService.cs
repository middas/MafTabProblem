using System;
using System.Windows;

namespace MafTabProblem.Mvvm.Dialogs
{
    public class DialogService : IDialogService
    {
        private Window currentDialog = null;

        public bool? Show<T>(T viewModel, Func<T, Window> dialogWindowCreator) where T : IDialogModal
        {
            if (currentDialog is null)
            {
                currentDialog = dialogWindowCreator(viewModel);
                currentDialog.Owner = Application.Current.MainWindow;
                currentDialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;

                viewModel.ButtonPressed += (sender, args) =>
                {
                    currentDialog.DialogResult = !args.Canceled;
                    currentDialog.Close();
                };

                var result = currentDialog.ShowDialog();
                currentDialog = null;

                return result;
            }

            return null;
        }
    }
}