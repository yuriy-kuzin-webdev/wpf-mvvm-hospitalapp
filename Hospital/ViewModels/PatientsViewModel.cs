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
    class PatientsViewModel : ObservableObject, ILoad
    {
        private HospitalContext _hospitalContext;
        private PatientModal _modal;
        public ICommand ShowModal { get; }
        public ICommand ApplyChanges { get; }
        public ICommand DiscardChanges { get; }
        public ICommand SortByProperty { get; }

        public ObservableCollection<Patient> Patients { get; private set; }
        public ObservableCollection<Doctor> Doctors { get; private set; }
        public ObservableCollection<Disease> Diseases { get; private set; }
        public ObservableCollection<Department> Departments { get; private set; }
        public PatientModal Modal
        {
            get => _modal;
            set => OnPropertyChanged(ref _modal, value);
        }
        public PatientsViewModel(HospitalContext hospitalContext)
        {
            _hospitalContext = hospitalContext;
            SortByProperty = new RelayCommand<string>(Sort);
            ShowModal = new RelayCommand<int>(LoadModel);
            ApplyChanges = new RelayCommand<int>(SaveModel);
            DiscardChanges = new RelayCommand<object>(_ => Modal.Visibility = System.Windows.Visibility.Collapsed);
            Load();
        }

        private void SaveModel(int id)
        {
            if (id != 0)
            {
                Patient patient = Patients.First(dep => dep.Id == id);
                patient.FName = String.Copy(Modal.FName);
                patient.LName = String.Copy(Modal.LName);
                patient.MName = String.Copy(Modal.MName);
                patient.Phone = String.Copy(Modal.Phone);
                patient.Dob = Modal.Dob;
                patient.DepartmentId = Modal.Department.Id;
                patient.Department = Modal.Department;
                patient.DiseaseId = Modal.Disease.Id;
                patient.Disease = Modal.Disease;
                patient.DoctorId = Modal.Doctor.Id;
                patient.Doctor = Modal.Doctor;
                //patient.Histories.LastOrDefault().Start = Modal.Start;
                //patient.Histories.LastOrDefault().End = Modal.End;
            }
            else
            {
                Patient patient = new Patient
                {
                    FName = String.Copy(Modal.FName),
                    LName = String.Copy(Modal.LName),
                    MName = String.Copy(Modal.MName),
                    Phone = String.Copy(Modal.Phone),
                    Dob = Modal.Dob,
                    Department = Modal.Department,
                    Disease = Modal.Disease,
                    Doctor = Modal.Doctor
                };
                //patient.Histories.Add(new History
                //{
                //    Department = Modal.Department,
                //    Disease = Modal.Disease,
                //    Doctor = Modal.Doctor,
                //    //Start = Modal.Start,
                //    //End = Modal.End
                //});

                _hospitalContext.Patients.Add(patient);
                Load();
            }
            _hospitalContext.SaveChanges();
            Modal.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void LoadModel(int id)
        {
            if (id != 0)
            {
                Patient selected = Patients.First(pat => pat.Id == id);
                Modal = new PatientModal
                {
                    Id = selected.Id,
                    FName = selected.FName,
                    LName = selected.LName,
                    MName = selected.MName,
                    Phone = selected.Phone,
                    Dob = selected.Dob,
                    Department = selected.Department,
                    Doctor = selected.Doctor,
                    Disease = selected.Disease,
                    //Start = selected.Histories.FirstOrDefault().Start,
                    //End = selected.Histories.FirstOrDefault().End
                };
                Modal.Visibility = System.Windows.Visibility.Visible;
                return;
            }
            //otherwise
            Modal = new PatientModal
            {
                Id = 0,
                FName = String.Empty,
                LName = String.Empty,
                MName = String.Empty,
                Phone = String.Empty,
                Dob = new DateTime(1980, 01, 01),
                //Histories = new List<History> { new History { Start = DateTime.Now } },
                Visibility = System.Windows.Visibility.Visible,
            };
        }

        private void Sort(string propName)
        {
            switch(propName)
            {
                case "FName":
                    Patients = new ObservableCollection<Patient>(_hospitalContext.Patients.OrderBy(pat => pat.FName));
                    break;
                case "MName":
                    Patients = new ObservableCollection<Patient>(_hospitalContext.Patients.OrderBy(pat => pat.MName));
                    break;
                case "LName":
                    Patients = new ObservableCollection<Patient>(_hospitalContext.Patients.OrderBy(pat => pat.LName));
                    break;
                case "Phone":
                    Patients = new ObservableCollection<Patient>(_hospitalContext.Patients.OrderBy(pat => pat.Phone));
                    break;
                case "Doctor":
                    Patients = new ObservableCollection<Patient>(_hospitalContext.Patients.OrderBy(pat => pat.Doctor.Name));
                    break;
                case "Department":
                    Patients = new ObservableCollection<Patient>(_hospitalContext.Patients.OrderBy(pat => pat.Department.Name));
                    break;
                case "Disease":
                    Patients = new ObservableCollection<Patient>(_hospitalContext.Patients.OrderBy(pat => pat.Disease.Name));
                    break;
            }
            
            OnPropertyChanged("Patients");
        }

        public void Load()
        {
            Patients = new ObservableCollection<Patient>(_hospitalContext.Patients);
            Departments = new ObservableCollection<Department>(_hospitalContext.Departments);
            Diseases = new ObservableCollection<Disease>(_hospitalContext.Diseases);
            Doctors = new ObservableCollection<Doctor>(_hospitalContext.Doctors);
            OnPropertyChanged("Patients");
            OnPropertyChanged("Departments");
            OnPropertyChanged("Diseases");
            OnPropertyChanged("Doctors");
            Modal = new PatientModal { Id = 0 };
        }        
    }
}
