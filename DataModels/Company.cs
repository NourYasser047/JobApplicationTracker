using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobApplicationTracker.DataModels;
namespace JobApplicationTracker.DataModels
{
    

    public enum CompanyStatus
    {
        Pending, Approved, Rejected
    }
    public class Company:IEntity
    {
        public int Id { get; set; }//unique
        public string Name;//required
       public  string Industry;
       public string City;
        public CompanyStatus status = CompanyStatus.Pending;//start as pending

        public static string Header =>
           $"{"Id",-4}{"Name",-14}{"Industry",-26}{"City",-10}Status";

        public override string ToString() =>
            $"{Id,-4}{Name,-14}{Industry,-26}{City,-10}{status}";
    }
}
