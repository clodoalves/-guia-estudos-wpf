using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFSample.App.ViewModels
{
    public class ViewModelBaseImp : INotifyPropertyChanged, IDisposable
    {
        protected ViewModelBaseImp() { }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;

            if (handler != null)
            {
                var propertyChanged = new PropertyChangedEventArgs(propertyName);
                handler(this, propertyChanged);
            }
        }

        public void Dispose()
        {
            this.OnDispose();
        }

        protected virtual void OnDispose()
        {
        }
    }
}
