using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobApplicationTracker.DataModels;
namespace JobApplicationTracker.Services
{
    public class CompanyService
    {
        

        public DataStore<Company> Store { get; } = new DataStore<Company>();

      
        private readonly Stack<(int CompanyId, CompanyStatus OldStatus)> history =  new Stack<(int CompanyId, CompanyStatus OldStatus)>();

        public void SeedData()
        {
            Store.Add(new Company { Id = 1, Name = "Edita", Industry = "Food Industry", City = "Alex", status = CompanyStatus.Approved });
            Store.Add(new Company { Id = 2, Name = "Pharco", Industry = "Pharmaceutical Industry", City = "Cairo", status = CompanyStatus.Approved });
            Store.Add(new Company { Id = 3, Name = "Papia", Industry = "Paper Tissues Industry", City = "Giza", status = CompanyStatus.Pending });
        }

        public void List()
        {
            Console.WriteLine(Company.Header);
            foreach (Company c in Store.GetAll()) Console.WriteLine(c);
        }

        public void GetcombanyBystatus(CompanyStatus status)
        {
            Console.WriteLine(Company.Header);
            foreach (Company c in Store.GetAll()) Console.WriteLine(c);
        }

        public string ChangeStatus(int CompanyId, CompanyStatus Nwestatus)
        {
            var item = Store.GetById(CompanyId);
            if (item != null)
            {
                history.Push((item.Id, item.status));
                item.status = Nwestatus;

                return "Status Changed" + $"Company " + item.Name + " is now " + Nwestatus;
            }
            else
                return "status unChanged";

        }

        public string Undo()
        {
            if (history.Count == 0) return "Nothing to undo.";
          
            var entry = history.Pop();
            Company c = Store.GetById(entry.CompanyId);
            if (c == null) return "That company no longer exists.";

            c.status = entry.OldStatus;
            return $"Undo done";


        }
        
    }
}
