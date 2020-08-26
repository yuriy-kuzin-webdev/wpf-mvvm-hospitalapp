using Hospital.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Models
{
    class Patient : ObservableObject
    {
        private int _id;
        private string _fname;
        private string _lname;
        private string _mname;
        private string _phone;
        private DateTime _dob;

        //Relations
        public int? DepartmentId { set; get; }
        public int? DiseaseId { get; set; }
        public int? DoctorId { get; set; }
        public virtual Department Department { get; set; }
        public virtual Disease Disease { get; set; }
        public virtual Doctor Doctor { get; set; }
        public virtual ICollection<History> Histories { get; set; }
        //

        public Patient() => Histories = new List<History>();

        public int Id
        {
            get => _id;
            set => OnPropertyChanged(ref _id, value);
        }
        public string FName
        {
            get => _fname;
            set => OnPropertyChanged(ref _fname, value);
        }
        public string MName
        {
            get => _mname;
            set => OnPropertyChanged(ref _mname, value);
        }
        public string LName
        {
            get => _lname;
            set => OnPropertyChanged(ref _lname, value);
        }
        public string Phone
        {
            get => _phone;
            set => OnPropertyChanged(ref _phone, value);
        }
        public DateTime Dob
        {
            get => _dob;
            set => OnPropertyChanged(ref _dob, value);
        }
    }
}
