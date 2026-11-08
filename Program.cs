using System;
using JobApplicationTracker.DataModels;
using JobApplicationTracker.Services;

namespace JobApplicationTracker
{
    internal class Program
    {
        static CompanyService companies = new CompanyService();
        static RecruiterService recruiters = new RecruiterService();
        static CandidateService candidates = new CandidateService();
        static JobService jobs;
        static ApplicationService applications;

        static void Main(string[] args)
        {
            jobs = new JobService(companies, recruiters);
            applications = new ApplicationService(jobs, candidates, companies);

            // E1: seed data
            companies.SeedData();
            recruiters.SeedData();
            candidates.SeedData();
            jobs.SeedData();

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("===== Job Application Tracker =====");
                Console.WriteLine("1. Companies");
                Console.WriteLine("2. Recruiters");
                Console.WriteLine("3. Jobs");
                Console.WriteLine("4. Candidates");
                Console.WriteLine("5. Applications");
             
                Console.WriteLine("0. Exit");
                Console.Write("Choose: ");
                string choice = Console.ReadLine();
                if (choice == null) return;

                switch (choice.Trim())
                {
                    case "1": CompanyMenu(); break;
                    case "2": RecruiterMenu(); break;
                    case "3": JobMenu(); break;
                    case "4": CandidateMenu(); break;
                    case "5": ApplicationMenu(); break;
                   
                    case "0": Console.WriteLine("Goodbye!"); return;
                    default: Console.WriteLine("Invalid choice, please pick a number from the menu."); break;
                }
            }
        }

        // ---------- Companies ----------
        static void CompanyMenu()
        {
            while (true)
            {
                Console.WriteLine("\n--- Companies ---");
                Console.WriteLine("1. Add  2. Update  3. List  4. Remove");
                Console.WriteLine("5. Change status  6. Undo status change  7. List by status  0. Back");
                Console.Write("Choose: ");
                string c = Console.ReadLine();
                if (c == null) return;

                switch (c.Trim())
                {
                    case "1": AddCompany(); break;
                    case "2": UpdateCompany(); break;
                    case "3": companies.List(); break;
                    case "4": RemoveCompany(); break;
                    case "5": ChangeCompanyStatus(); break;
                    case "6": Console.WriteLine(companies.Undo()); break;
                    case "7": ListCompaniesByStatus(); break;
                    case "0": return;
                    default: Console.WriteLine("Invalid choice."); break;
                }
            }
        }

        static void AddCompany()
        {
            string name = Input.Text("Name: ");
            if (name == "") { Console.WriteLine("Name is required."); return; }
            Company c = new Company
            {
                Id = companies.Store.NextId(),
                Name = name,
                Industry = Input.Text("Industry: "),
                City = Input.Text("City: "),
                status = CompanyStatus.Pending
            };
            companies.Store.Add(c);
            Console.WriteLine($"Company added.");
        }

        static void UpdateCompany()
        {
            int id;
            if (!Input.TryInt("Company Id: ", out id)) return;
            Company c = companies.Store.GetById(id);
            if (c == null) { Console.WriteLine("Company not found."); return; }

            Console.WriteLine("Leave a field empty to keep the old value.");
            string name = Input.Text($"Name ({c.Name}): ");
            string industry = Input.Text($"Industry ({c.Industry}): ");
            string city = Input.Text($"City ({c.City}): ");
            if (name != "") c.Name = name;
            if (industry != "") c.Industry = industry;
            if (city != "") c.City = city;
            companies.Store.Update(c);
            Console.WriteLine("Company updated.");
        }

        static void RemoveCompany()
        {
            int id;
            if (!Input.TryInt("Company Id: ", out id)) return;
            Console.WriteLine(companies.Store.Remove(id) ? "Company removed." : "Company not found.");
        }

        static void ChangeCompanyStatus()
        {
            int id;
            if (!Input.TryInt("Company Id: ", out id)) return;
            CompanyStatus status;
            if (!ReadCompanyStatus(out status)) return;
            string message;
           message= companies.ChangeStatus(id, status);
            Console.WriteLine(message);
        }

        static void ListCompaniesByStatus()
        {
            CompanyStatus status;
            if (!ReadCompanyStatus(out status)) return;
            companies.GetcombanyBystatus(status);
        }

        static bool ReadCompanyStatus(out CompanyStatus status)
        {
            status = CompanyStatus.Pending;
            int n;
            if (!Input.TryInt("Status (1=Pending, 2=Approved, 3=Rejected): ", out n)) return false;
            switch (n)
            {
                case 1: status = CompanyStatus.Pending; return true;
                case 2: status = CompanyStatus.Approved; return true;
                case 3: status = CompanyStatus.Rejected; return true;
                default: Console.WriteLine("Please choose 1, 2 or 3."); return false;
            }
        }

        // ---------- Recruiters ----------
        static void RecruiterMenu()
        {
            while (true)
            {
                Console.WriteLine("\n--- Recruiters ---");
                Console.WriteLine("1. Add  2. Update  3. List  4. Remove  0. Back");
                Console.Write("Choose: ");
                string c = Console.ReadLine();
                if (c == null) return;

                switch (c.Trim())
                {
                    case "1": AddRecruiter(); break;
                    case "2": UpdateRecruiter(); break;
                    case "3": recruiters.List(); break;
                    case "4": RemoveRecruiter(); break;
                    case "0": return;
                    default: Console.WriteLine("Invalid choice."); break;
                }
            }
        }

        static void AddRecruiter()
        {
            string name = Input.Text("Name: ");
            if (name == "") { Console.WriteLine("Name is required."); return; }
            string email = Input.Text("Email: ");
            
            int companyId;
            if (!Input.TryInt("Company Id: ", out companyId)) return;
            if (companies.Store.GetById(companyId) == null) { Console.WriteLine("Company not found."); return; }

            Recruiter r = new Recruiter { Id = recruiters.Recruiters.NextId(), Name = name, Email = email, CompanyId = companyId };
            recruiters.Recruiters.Add(r);
            Console.WriteLine($"Recruiter added .");
        }

        static void UpdateRecruiter()
        {
            int id;
            if (!Input.TryInt("Recruiter Id: ", out id)) return;
            Recruiter r = recruiters.Recruiters.GetById(id);
            if (r == null) { Console.WriteLine("Recruiter not found."); return; }

            Console.WriteLine("Leave a field empty to keep the old value.");
            string name = Input.Text($"Name ({r.Name}): ");
            string email = Input.Text($"Email ({r.Email}): ");
           
            if (name != "") r.Name = name;
            if (email != "") r.Email = email;
            recruiters.Recruiters.Update(r);
            Console.WriteLine("Recruiter updated.");
        }

        static void RemoveRecruiter()
        {
            int id;
            if (!Input.TryInt("Recruiter Id: ", out id)) return;
            Console.WriteLine(recruiters.Recruiters.Remove(id) ? "Recruiter removed." : "Recruiter not found.");
        }

        // ---------- Jobs ----------
        static void JobMenu()
        {
            while (true)
            {
                Console.WriteLine("\n--- Jobs ---");
                Console.WriteLine("1. Add  2. Update  3. List  4. Remove");
                Console.WriteLine("5. Find by Id  6. Close job  0. Back");
                Console.Write("Choose: ");
                string c = Console.ReadLine();
                if (c == null) return;

                switch (c.Trim())
                {
                    case "1": AddJob(); break;
                    case "2": UpdateJob(); break;
                    case "3": jobs.List(); break;
                    case "4": RemoveJob(); break;
                    case "5": FindJob(); break;
                    case "6": CloseJob(); break;
                    case "0": return;
                    default: Console.WriteLine("Invalid choice."); break;
                }
            }
        }

        static void AddJob()
        {
            int companyId, recruiterId;
            decimal salary;
            if (!Input.TryInt("Company Id: ", out companyId)) return;
            if (!Input.TryInt("Recruiter Id: ", out recruiterId)) return;
            string title = Input.Text("Title: ");
            string description = Input.Text("Description: ");
            if (!Input.TryDecimal("Salary: ", out salary)) return;
            string location = Input.Text("Location: ");

            Job job = new Job
            {
                Title = title,
                Description = description,
                Salary = salary,
                Location = location,
                CompanyId = companyId,
                RecruiterId = recruiterId
            };
            string message;
            message= jobs.AddJob(job);
            Console.WriteLine(message);
        }

        static void UpdateJob()
        {
            int id;
            if (!Input.TryInt("Job Id: ", out id)) return;
            Job job = jobs.FindbyId(id);
            if (job == null) { Console.WriteLine("Job not found."); return; }

            Console.WriteLine("Leave a field empty to keep the old value.");
            string title = Input.Text($"Title ({job.Title}): ");
            string description = Input.Text($"Description ({job.Description}): ");
            string salaryText = Input.Text($"Salary ({job.Salary}): ");
            string location = Input.Text($"Location ({job.Location}): ");

            if (salaryText != "")
            {
                decimal salary;
                if (!decimal.TryParse(salaryText, out salary) || salary <= 0)
                {
                    Console.WriteLine("Invalid salary.");
                    return;
                }
                job.Salary = salary;
            }
            if (title != "") job.Title = title;
            if (description != "") job.Description = description;
            if (location != "") job.Location = location;

            jobs.Update(job);
            Console.WriteLine("Job updated.");
        }

        static void RemoveJob()
        {
            int id;
            if (!Input.TryInt("Job Id: ", out id)) return;
            if (jobs.Remove(id))
            {
                applications.RemoveForJob(id);
                Console.WriteLine("Job removed.");
            }
            else Console.WriteLine("Job not found.");
        }

        static void FindJob()
        {
            int id;
            if (!Input.TryInt("Job Id: ", out id)) return;
            Job job = jobs.FindbyId(id);   
            if (job == null) { Console.WriteLine("Job not found."); return; }
            Console.WriteLine(Job.Header);
            Console.WriteLine(job);
        }

        static void CloseJob()
        {
            int id;
            if (!Input.TryInt("Job Id: ", out id)) return;
            Job job = jobs.FindbyId(id);
            if (job == null) { Console.WriteLine("Job not found."); return; }
            job.IsOpen = false;
            jobs.Update(job);
            Console.WriteLine("Job closed.");
        }

        // ---------- Candidates ----------
        static void CandidateMenu()
        {
            while (true)
            {
                Console.WriteLine("\n--- Candidates ---");
                Console.WriteLine("1. Add  2. Update  3. List  4. Remove  0. Back");
                Console.Write("Choose: ");
                string c = Console.ReadLine();
                if (c == null) return;

                switch (c.Trim())
                {
                    case "1": AddCandidate(); break;
                    case "2": UpdateCandidate(); break;
                    case "3": candidates.List(); break;
                    case "4": RemoveCandidate(); break;
                    case "0": return;
                    default: Console.WriteLine("Invalid choice."); break;
                }
            }
        }

        static void AddCandidate()
        {
            int years;
            string name = Input.Text("Full name: ");
            string email = Input.Text("Email: ");
            string phone = Input.Text("Phone: ");
            if (!Input.TryInt("Years of experience: ", out years)) return;

            Candidate cand = new Candidate { FullName = name, Email = email, Phone = phone, YearsOfExperience = years };
            string message;
            message= candidates.Add(cand);
            Console.WriteLine(message);
        }

        static void UpdateCandidate()
        {
            int id;
            if (!Input.TryInt("Candidate Id: ", out id)) return;
            Candidate old = candidates.Candidates.GetById(id);
            if (old == null) { Console.WriteLine("Candidate not found."); return; }

            Console.WriteLine("Leave a field empty to keep the old value.");
            string name = Input.Text($"Name ({old.FullName}): ");
            string email = Input.Text($"Email ({old.Email}): ");
            string phone = Input.Text($"Phone ({old.Phone}): ");
            string yearsText = Input.Text($"Years ({old.YearsOfExperience}): ");

            int years = old.YearsOfExperience;
            if (yearsText != "" && !int.TryParse(yearsText, out years))
            {
                Console.WriteLine("Invalid number.");
                return;
            }

            Candidate updated = new Candidate
            {
                Id = old.Id,
                FullName = name != "" ? name : old.FullName,
                Email = email != "" ? email : old.Email,
                Phone = phone != "" ? phone : old.Phone,
                YearsOfExperience = years
            };
            string message;
            message= candidates.Update(updated, old.Email);
            Console.WriteLine(message);
        }

        static void RemoveCandidate()
        {
            int id;
            if (!Input.TryInt("Candidate Id: ", out id)) return;
            if (candidates.Remove(id))
            {
                applications.RemoveForCandidate(id);
                Console.WriteLine("Candidate removed.");
            }
            else Console.WriteLine("Candidate not found.");
        }

        // ---------- Applications ----------
        static void ApplicationMenu()
        {
            while (true)
            {
                Console.WriteLine("\n--- Applications ---");
                Console.WriteLine("1. Apply to a job  2. List  3. Remove  0. Back");
                Console.Write("Choose: ");
                string c = Console.ReadLine();
                if (c == null) return;

                switch (c.Trim())
                {
                    case "1": Apply(); break;
                    case "2": applications.List(); break;
                    case "3": RemoveApplication(); break;
                    case "0": return;
                    default: Console.WriteLine("Invalid choice."); break;
                }
            }
        }

        static void Apply()
        {
            int candidateId, jobId;
            if (!Input.TryInt("Candidate Id: ", out candidateId)) return;
            if (!Input.TryInt("Job Id: ", out jobId)) return;
            string message;
            message= applications.Apply(jobId, candidateId);
            Console.WriteLine(message);
        }

        static void RemoveApplication()
        {
            int id;
            if (!Input.TryInt("Application Id: ", out id)) return;
            Console.WriteLine(applications.Remove(id) ? "Application removed." : "Application not found.");
        }
    }
}