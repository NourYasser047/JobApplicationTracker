using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobApplicationTracker.DataModels
{
    public class Job:IEntity
    {
        public int Id { get; set; }
        public string Title;//required
        public string Description;
        public decimal Salary; //above 0
        public string Location;
        public bool IsOpen=true;   // default true
        public DateTime PostedDate;
        public int CompanyId;
        public int RecruiterId;

        public static string Header =>
           $"{"Id",-4}{"Title",-24}{"Salary",-9}{"Location",-11}{"State",-8}{"Posted",-12}{"Company",-8}Recruiter";

        public override string ToString() =>
            $"{Id,-4}{Title,-24}{Salary,-9}{Location,-11}{(IsOpen ? "Open" : "Closed"),-8}{PostedDate:yyyy-MM-dd}  {CompanyId,-8}{RecruiterId}";
    }
}
