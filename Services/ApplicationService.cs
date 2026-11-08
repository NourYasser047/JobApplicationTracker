using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobApplicationTracker.DataModels;
using static System.Formats.Asn1.AsnWriter;
using static System.Reflection.Metadata.BlobBuilder;

namespace JobApplicationTracker.Services
{
    public class ApplicationService
    {
       
        public DataStore<Application> Applications { get; }    = new DataStore<Application>();

        // H1: one entry per (job, candidate)
        private readonly HashSet<(int JobId, int CandidateId)> pairs =
            new HashSet<(int JobId, int CandidateId)>();

        private readonly JobService Jobs;
        private readonly CandidateService Candidates;
        private readonly CompanyService Companies;

        public ApplicationService(JobService jobs, CandidateService candidates, CompanyService companies)
        {
            Jobs = jobs;
            Candidates = candidates;
            Companies = companies;
        }

        public string Apply(int jobId, int candidateId)
        {
            if (Candidates.Candidates.GetById(candidateId) == null) { return  "Candidate not found."; }

            Job job = Jobs.FindbyId(jobId);
            if (job == null) { return  "Job not found.";  }
            if (!job.IsOpen) { return "Refused: this job is closed.";   }

            Company company = Companies.Store.GetById(job.CompanyId);
            if (company == null || company.status != CompanyStatus.Approved)
            {
                return  "Refused: the company of this job is not Approved.";
                 
            }

            if (! pairs.Add((jobId, candidateId)))
            {
                return  "Refused: this candidate already applied to this job.";
                 
            }

            Application app = new Application
            {
                Id = Applications.NextId(),
                JobId = jobId,
                CandidateId = candidateId,
                AppliedDate = DateTime.Today,
                Status = AppStatus.Submitted
            };
            Applications.Add(app);
            return  "Application "+app.Id+"submitted.";
            
        }

        public bool Remove(int id)
        {
            Application app = Applications.GetById(id);
            if (app == null) return false;
            pairs.Remove((app.JobId, app.CandidateId));
            return Applications.Remove(id);
        }

        public void RemoveForJob(int jobId)
        {
            List<int> ids = new List<int>();
            foreach (Application a in Applications.GetAll())
                if (a.JobId == jobId) ids.Add(a.Id);
            foreach (int id in ids) Remove(id);
        }

        public void RemoveForCandidate(int candidateId)
        {
            List<int> ids = new List<int>();
            foreach (Application a in Applications.GetAll())
                if (a.CandidateId == candidateId) ids.Add(a.Id);
            foreach (int id in ids) Remove(id);
        }

        public void List()
        {
            Console.WriteLine(Application.Header);
            foreach (Application a in Applications.GetAll()) Console.WriteLine(a);
        }

    }
}
