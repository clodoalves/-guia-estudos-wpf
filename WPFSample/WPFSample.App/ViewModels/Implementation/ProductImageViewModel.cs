using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFSample.App.ViewModels.Implementation
{
    public class ProductImageViewModel : BindableBase
    {
        public ProductImageViewModel(FileStream source)
        {
            Source = source;
            Name = source.Name;
        }

        private int _id;

        public int Id
        {
            get
            {
                return _id;
            }

            set
            {
                SetProperty(ref _id, value);
            }
        }

        private string _name;

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                SetProperty(ref _name, value);
            }
        }

        private bool _checkeddToDelete;

        public int CheckedToDelete 
        { get; set; }
        public DelegateCommand DeleteImage => _removeImages ?? (_removeImages = new DelegateCommand(ExecuteDeleteImage));

        private DelegateCommand _removeImages;

        private void ExecuteDeleteImage()
        {

        }

        public FileStream Source { get; private set; }
    }
}
