using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Hospital.Models.Modals
{
    class DoctorModal : Doctor
    {
        private Visibility _visibility = Visibility.Collapsed;
        public Visibility Visibility
        {
            get => _visibility;
            set => OnPropertyChanged(ref _visibility, value);
        }
    }
}
