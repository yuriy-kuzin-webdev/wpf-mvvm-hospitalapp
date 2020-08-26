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
    class DiseasesViewModel : ObservableObject, ILoad
    {
        private HospitalContext _hospitalContext;
        public ObservableCollection<Disease> Diseases{ get; private set; }
        private DiseaseModal _modal;
        public DiseaseModal Modal
        {
            get => _modal;
            set => OnPropertyChanged(ref _modal, value);
        }
        public ICommand ShowModal { get; }
        public ICommand ApplyChanges { get; }
        public ICommand DiscardChanges { get; }

        public DiseasesViewModel(HospitalContext hospitalContext)
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
                Diseases.First(dep => dep.Id == id).Name = String.Copy(Modal.Name);
            }
            else
            {
                _hospitalContext.Diseases.Add(new Disease { Name = string.Copy(Modal.Name) });
                Load();
            }
            _hospitalContext.SaveChanges();
            Modal.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void LoadModel(int id)
        {
            if (id != 0)
            {
                Disease selected = Diseases.First(dep => dep.Id == id);
                Modal = new DiseaseModal
                {
                    Id = selected.Id,
                    Name = selected.Name,
                };
                Modal.Visibility = System.Windows.Visibility.Visible;
                return;
            }
            //otherwise
            Modal = new DiseaseModal
            {
                Visibility = System.Windows.Visibility.Visible,
                Name = string.Empty,
                Id = 0
            };
        }
        public void Load()
        {
            Modal = new DiseaseModal
            {
                Visibility = System.Windows.Visibility.Collapsed,
                Name = String.Empty
            };
            Diseases = new ObservableCollection<Disease>(_hospitalContext.Diseases);
            OnPropertyChanged("Diseases");
        }
    }
}
