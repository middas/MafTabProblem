using System;
using System.ComponentModel;

namespace Plugin
{
    public class TabProblemViewModel : INotifyPropertyChanged, IDisposable
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void Dispose()
        {
            PropertyChanged = null;
        }
    }
}