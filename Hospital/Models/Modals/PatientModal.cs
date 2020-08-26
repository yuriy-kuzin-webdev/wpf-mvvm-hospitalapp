using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Hospital.Models.Modals
{
    class PatientModal : Patient
    {
        private Visibility _visibility = Visibility.Collapsed;
        public Visibility Visibility
        {
            get => _visibility;
            set => OnPropertyChanged(ref _visibility, value);
        }
        private Visibility _visibilityIfNew = Visibility.Collapsed;
        public Visibility VisibilityIfNew
        {
            get => _visibility;
            set => OnPropertyChanged(ref _visibilityIfNew, value);
        }
        private DateTime? _start;
        private DateTime? _end;
        public DateTime? Start
        {
            get => _start;
            set => OnPropertyChanged(ref _start, value);
        }
        public DateTime? End
        {
            get => _end;
            set => OnPropertyChanged(ref _end, value);
        }
    }
}
