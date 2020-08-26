using Hospital.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Models
{
    class Template : ObservableObject, ITemplate
    {
        protected int _id;
        protected string _name;

        //Relations
        public virtual ICollection<Patient> Patients { get; set; }
        public virtual ICollection<History> Histories { get; set; }
        //

        public Template()
        {
            Patients = new List<Patient>();
            Histories = new List<History>();
        }

        public int Id
        {
            get => _id;
            set => OnPropertyChanged(ref _id, value);
        }
        public string Name
        {
            get => _name;
            set => OnPropertyChanged(ref _name, value);
        }
    }
}
