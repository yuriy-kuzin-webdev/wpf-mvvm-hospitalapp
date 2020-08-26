using Hospital.Models;
using Hospital.Models.Modals;
using Hospital.Services;
using Hospital.Utility;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Linq;

namespace Hospital.ViewModels
{
    class DoctorsViewModel : ObservableObject, ILoad
    {
        private HospitalContext _hospitalContext;
        public ObservableCollection<Doctor> Doctors { get; private set; }
        private DoctorModal _modal;
        public DoctorModal Modal
        {
            get => _modal;
            set => OnPropertyChanged(ref _modal, value);
        }
        public ICommand ShowModal { get; }
        public ICommand ApplyChanges { get; }
        public ICommand DiscardChanges { get; }

        public DoctorsViewModel(HospitalContext hospitalContext)
        {
            _hospitalContext = hospitalContext;
            ShowModal = new RelayCommand<int>(LoadModel);
            ApplyChanges = new RelayCommand<int>(SaveModel);
            DiscardChanges = new RelayCommand<object>(_ => Modal.Visibility = System.Windows.Visibility.Collapsed);
            Load();
        }

        private void SaveModel(int id)
        {
            if (id != 0)
            {
                Doctors.First(dep => dep.Id == id).Name = String.Copy(Modal.Name);
            }
            else
            {
                _hospitalContext.Doctors.Add(new Doctor { Name = string.Copy(Modal.Name) });
                Load();
            }
            _hospitalContext.SaveChanges();
            Modal.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void LoadModel(int id)
        {
            if (id != 0)
            {
                Doctor selected = Doctors.First(dep => dep.Id == id);
                Modal = new DoctorModal
                {
                    Id = selected.Id,
                    Name = selected.Name,
                };
                Modal.Visibility = System.Windows.Visibility.Visible;
                return;
            }
            //otherwise
            Modal = new DoctorModal
            {
                Visibility = System.Windows.Visibility.Visible,
                Name = string.Empty,
                Id = 0
            };
        }
        public void Load()
        {
            Modal = new DoctorModal
            {
                Visibility = System.Windows.Visibility.Collapsed,
                Name = String.Empty
            };
            Doctors = new ObservableCollection<Doctor>(_hospitalContext.Doctors);
            OnPropertyChanged("Diseases");
        }
    }
}