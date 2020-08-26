using Hospital.Services;
using Hospital.Utility;
using System.Windows.Media.Animation;

namespace Hospital.ViewModels
{
    class AppViewModel : ObservableObject
    {
        private HospitalContext _hospitalContext;
        private object _currentView;
        public object CurrentView
        {
            get => _currentView;
            set => OnPropertyChanged(ref _currentView, value);
        }
        public DepartmentsViewModel DepartmentsVM { get; }
        public DiseasesViewModel DiseasesVM { get; }
        public DoctorsViewModel DoctorsVM { get; }
        public PatientsViewModel PatientsVM { get; }
        public AppViewModel()
        {
            _hospitalContext = new HospitalContext();
            DepartmentsVM = new DepartmentsViewModel(_hospitalContext);
            DiseasesVM = new DiseasesViewModel(_hospitalContext);
            DoctorsVM = new DoctorsViewModel(_hospitalContext);
            PatientsVM = new PatientsViewModel(_hospitalContext);
            ChangeViewCommand = new RelayCommand<object>(ChangeView);
            CurrentView = DepartmentsVM;
        }
        public RelayCommand<object> ChangeViewCommand { get; }
        private void ChangeView(object param)
        {
            CurrentView = param;
            (param as ILoad).Load();
        }
    }
}
