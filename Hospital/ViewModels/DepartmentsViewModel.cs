using Hospital.Models;
using Hospital.Models.Modals;
using Hospital.Services;
using Hospital.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Hospital.ViewModels
{
    class DepartmentsViewModel : ObservableObject, ILoad
    {
        private HospitalContext _hospitalContext;
        public ObservableCollection<Department> Departments{ get; private set; }
        private DepartmentModal _modal;
        public DepartmentModal Modal
        {
            get => _modal;
            set => OnPropertyChanged(ref _modal, value);
        }
        public ICommand ShowModal { get; }
        public ICommand ApplyChanges { get; }
        public ICommand DiscardChanges { get; }
        public DepartmentsViewModel(HospitalContext hospitalContext)
        {
            _hospitalContext = hospitalContext;
            ShowModal = new RelayCommand<int>(LoadModel);
            ApplyChanges = new RelayCommand<int>(SaveModel);
            DiscardChanges = new RelayCommand<object>(_=> Modal.Visibility = System.Windows.Visibility.Collapsed);
            Load();
        }


        private void SaveModel(int id)
        {
            if(id != 0)
            {
                Departments.First(dep => dep.Id == id).Name = String.Copy(Modal.Name);
            }
            else
            {
                _hospitalContext.Departments.Add(new Department { Name = string.Copy(Modal.Name) });
                Load();
            }
            _hospitalContext.SaveChanges();
            Modal.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void LoadModel(int id)
        {
            if(id != 0)
            {
                Department selected = Departments.First(dep => dep.Id == id);
                Modal = new DepartmentModal
                {
                    Id = selected.Id,
                    Name = selected.Name,
                };
                Modal.Visibility = System.Windows.Visibility.Visible;
                return;
            }
            //otherwise
            Modal = new DepartmentModal
            {
                Visibility = System.Windows.Visibility.Visible,
                Name = string.Empty,
                Id = 0
            };
        }

        public void Load()
        {
            Modal = new DepartmentModal
            {
                Visibility = System.Windows.Visibility.Collapsed,
                Name = String.Empty
            };
            Departments = new ObservableCollection<Department>(_hospitalContext.Departments);
            OnPropertyChanged("Departments");
        }
    }
}
