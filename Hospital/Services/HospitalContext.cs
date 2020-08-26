using Hospital.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Services
{
    class HospitalContext : DbContext
    {
        public HospitalContext() : base("hospitalDb")
        {
            //Seeder();
        }
        private void Seeder()
        {
            Doctor doc1 = new Doctor { Name = "Josef Elevarde" };
            Doctor doc2 = new Doctor { Name = "Monique Applehouse" };
            Doctor doc3 = new Doctor { Name = "Gregor Buzkevich" };
            Doctors.AddRange(new[] { doc1, doc2, doc3 });

            Disease dis1 = new Disease { Name = "Cancer" };
            Disease dis2 = new Disease { Name = "Covid" };
            Disease dis3 = new Disease { Name = "Malaria" };
            Diseases.AddRange(new[] { dis1, dis2, dis3 });

            Department dep1 = new Department { Name = "Allergology" };
            Department dep2 = new Department { Name = "Intensive Therapy" };
            Department dep3 = new Department { Name = "Cardiology" };
            Departments.AddRange(new[] { dep1, dep2, dep3 });

            Patient pat1 = new Patient
            {
                FName = "Brann",
                LName = "Tannard",
                MName = "Messia",
                Phone = "+17644440811",
                Dob = new DateTime(1977, 05, 15),
                Department = dep1,
                Disease = dis1,
                Doctor = doc1
            };
            pat1.Histories.Add(new History
            {
                Department = dep1,
                Disease = dis1,
                Doctor = doc1,
                Start = new DateTime(2020, 02, 02),
                End = new DateTime(2020,04,22)
            });
            Patients.Add(pat1);

            Patient pat2 = new Patient
            {
                FName = "Hugo",
                LName = "Moss",
                MName = "Pherine",
                Phone = "+17602993434",
                Dob = new DateTime(1990, 08, 11),
                Department = dep2,
                Disease = dis3,
                Doctor = doc3
            };
            pat2.Histories.Add(new History
            {
                Department = dep2,
                Disease = dis3,
                Doctor = doc3,
                Start = new DateTime(2020, 05, 10),
                End = new DateTime(2020, 07, 01)
            });
            Patients.Add(pat2);

            Patient pat3 = new Patient
            {
                FName = "Jericho",
                LName = "Estrasa",
                MName = "Puerre",
                Phone = "+17602200011",
                Dob = new DateTime(1978, 01, 01),
                Department = dep3,
                Disease = dis2,
                Doctor = doc2
            };
            pat3.Histories.Add(new History
            {
                Department = dep3,
                Disease = dis2,
                Doctor = doc2,
                Start = new DateTime(2020, 05, 02),
                End = new DateTime(2020, 05, 29)
            });
            Patients.Add(pat3);

            SaveChanges();
        }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Disease> Diseases { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<History> Histories { get; set; } // Cплошные проблемы с этой таблицей
    }
}
