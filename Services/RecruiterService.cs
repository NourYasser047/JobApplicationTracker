using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobApplicationTracker.DataModels;
using static System.Formats.Asn1.AsnWriter;

namespace JobApplicationTracker.Services
{
    public class RecruiterService
    { 

        public DataStore<Recruiter> Recruiters { get;  }=new DataStore<Recruiter>();

        public void SeedData()
        {
            Recruiters.Add(new Recruiter { Id = 1, Name = "Mona Samir", Email = "mona@edita.com", CompanyId = 1 });
            Recruiters.Add(new Recruiter { Id = 2, Name = "Khaled Adel", Email = "khaled@pharco.com", CompanyId = 2 });
            Recruiters.Add(new Recruiter { Id = 3, Name = "Nour Hassan", Email = "nour@papia.com", CompanyId = 3 });
        }

        public void List()
        {
            Console.WriteLine(Recruiter.Header);
            foreach (Recruiter r in Recruiters.GetAll()) Console.WriteLine(r);
        }
      

    }
}
