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

        public int Id { get; set; }

        public string Name { get; set; }


        private bool _checkedToDelete;
        public bool CheckedToDelete
        {
            get
            {
                return _checkedToDelete;
            }

            set
            {
                SetProperty(ref _checkedToDelete, value);
            }
        }
        public FileStream Source { get; private set; }
    }
}