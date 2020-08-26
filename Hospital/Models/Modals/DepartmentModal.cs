using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Hospital.Models.Modals
{
    class DepartmentModal : Department
    {
        private Visibility _visibility = Visibility.Visible;
        public Visibility Visibility
        {
            get => _visibility;
            set => OnPropertyChanged(ref _visibility, value);
        }
    }
}
