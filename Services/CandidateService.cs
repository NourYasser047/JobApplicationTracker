using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using JobApplicationTracker.DataModels;
using static System.Formats.Asn1.AsnWriter;

namespace JobApplicationTracker.Services
{
    public class CandidateService
    {
        public DataStore<Candidate> Candidates { get; }= new DataStore<Candidate>();

        private readonly HashSet<string> emails = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        public void SeedData()
        {
            Candidates.Add(new Candidate { Id= 1, FullName= "Ahmed Ali", Email="ahmed@gmail.com", Phone="0103232500001", YearsOfExperience= 2 });
            Candidates.Add(new Candidate { Id = 2, FullName = "Sara Omar", Email = "sara@gmail.com", Phone = "0110800331302", YearsOfExperience = 4 });
            Candidates.Add(new Candidate { Id = 3, FullName = "Youssef Hany", Email = "youssef@gmail.com", Phone = "01502135209", YearsOfExperience = 1 });
            Candidates.Add(new Candidate { Id = 4, FullName = "Laila Fathy", Email = "laila@gmail.com", Phone = "012265656257", YearsOfExperience = 6 });
            Candidates.Add(new Candidate { Id = 4, FullName = "Omar Tarek", Email = "omar@gmail.com", Phone = "010005656587", YearsOfExperience = 5 });

          
        }

      

        public string Add(Candidate c)
        {
            if (string.IsNullOrEmpty(c.FullName)) { return "Name is required.";  }
           
            if (c.YearsOfExperience < 0) { return  "Years of experience must be 0 or more.";   }
            if (! emails.Add(c.Email)) { return "Refused, this email already exists."; }

            c.Id = Candidates.NextId();
            Candidates.Add(c);
            return  "Candidate added.";
             
        }

        public string Update(Candidate updated, string oldEmail)
        {
            if (string.IsNullOrEmpty(updated.FullName)) { return  "Name is required.";   }
            
            if (updated.YearsOfExperience < 0) { return  "Years of experience must be 0 or more.";   }

            if (!Candidates.Update(updated)) { return  "Candidate not found.";  }

            return  "Candidate updated.";
            
        }

        public bool Remove(int id)
        {
            Candidate c = Candidates.GetById(id);
            if (c == null) return false;
            emails.Remove(c.Email);
            return Candidates.Remove(id);
        }

        public void List()
        {
            Console.WriteLine(Candidate.Header);
            foreach (Candidate c in Candidates.GetAll()) Console.WriteLine(c);
        }
       

    }
}
