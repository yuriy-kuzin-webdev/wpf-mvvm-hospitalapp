using Hospital.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Models
{
    class History : ObservableObject
    {
        private int _id;
        private DateTime? _start;
        private DateTime? _end;

        //Relations
        public int? DepartmentId { get; set; }
        public int? DiseaseId { get; set; }
        public int? PatientId { get; set; }
        public int? DoctorId { get; set; }
        public virtual Department Department { get; set; }
        public virtual Disease Disease { get; set; }
        public virtual Patient Patient { get; set; }
        public virtual Doctor Doctor { get; set; }
        //


        public int Id
        {
            get => _id;
            set => OnPropertyChanged(ref _id, value);
        }
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
