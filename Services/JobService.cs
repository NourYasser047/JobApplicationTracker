using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobApplicationTracker.DataModels;
using static System.Formats.Asn1.AsnWriter;

namespace JobApplicationTracker.Services
{
    public class JobService
    {
        

        public DataStore<Job> Jobs { get;}=new DataStore<Job>();


        private readonly Dictionary<int, Job> byId = new Dictionary<int, Job>();

        private readonly CompanyService Companies;
        private readonly RecruiterService _Recruiters;

        public JobService(CompanyService companies, RecruiterService recruiters)
        {
            Companies = companies;
            _Recruiters = recruiters;
        }

        public void SeedData()
        {
            SeedOne(new Job { Id= 1, Title="Food Technologist", Description= "Food Technologist", Salary= 9000, Location="Alex", IsOpen= true, PostedDate= new DateTime(2026, 11, 1), CompanyId= 1, RecruiterId= 1 });
            SeedOne(new Job{ Id= 2, Title= "QC Analyst", Description= "QC Analyst", Salary= 8000, Location= "Alex", IsOpen=true, PostedDate = new DateTime(2026, 10, 5), CompanyId= 1, RecruiterId=1 });
            SeedOne(new Job { Id = 3, Title = "Production Supervisor", Description = "Production Supervisor", Salary = 11000, Location = "Alex", IsOpen = false, PostedDate = new DateTime(2026, 6, 1), CompanyId = 1, RecruiterId = 1 });
            SeedOne(new Job { Id = 4, Title = "Pharmacist", Description = "Pharmacist", Salary = 12000, Location = "Cairo", IsOpen = true, PostedDate = new DateTime(2026, 8, 9), CompanyId = 2, RecruiterId = 2 });
            SeedOne(new Job { Id = 5, Title = "Lab Technician", Description = "Lab Technician", Salary = 8500, Location = "Cairo", IsOpen = true, PostedDate = new DateTime(2026, 4, 4), CompanyId = 2, RecruiterId = 2 });
            SeedOne(new Job { Id = 6, Title = "HSE Engineer", Description = "HSE Engineer", Salary = 14000, Location = "Cairo", IsOpen = true, PostedDate = new DateTime(2026, 5, 1), CompanyId = 2, RecruiterId = 2 });
        }

        private void SeedOne(Job job)
        {
            Jobs.Add(job);
            byId[job.Id] = job;   
        }

        public Job FindbyId(int id)
        {
            Job j;

            byId.TryGetValue(id, out j);
            return j;
        }

        public string AddJob(Job job)
        {
            if (string.IsNullOrEmpty(job.Title))
            {  return " Title should have a value"; }
            if (job.Salary <= 0) 
            { return "Salary must be above 0."; }

            Company company = Companies.Store.GetById(job.CompanyId);
            if (company == null) { return "Company not found."; }
            if (company.status != CompanyStatus.Approved)
            {
                return  "Refused: company "+company.Name+" is"+ company.status+". Only Approved companies can post jobs.";
                
            }

            Recruiter r = _Recruiters.Recruiters.GetById(job.RecruiterId);
            if (r == null) { return  "Recruiter not found."; }
            if (r.CompanyId != job.CompanyId)
            {
                return  "Refused: this recruiter does not belong to that company.";
               
            }

            job.Id = Jobs.NextId();
            job.PostedDate = DateTime.Today;
            job.IsOpen = true;
            Jobs.Add(job);
            byId[job.Id] = job;
            return "Job added with Id "+job.Id;
            

        }

      public bool Update(Job job)
        {
            if (!Jobs.Update(job)) return false;
            byId[job.Id] = job;
            return true;
        }

        public bool Remove(int id)
        {
            if (!Jobs.Remove(id)) return false;
            byId.Remove(id);
            return true;
        }

        public void List()
        {
            Console.WriteLine(Job.Header);
            foreach (Job j in Jobs.GetAll()) Console.WriteLine(j);
        }
    }
}
