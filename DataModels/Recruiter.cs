using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobApplicationTracker.DataModels
{
    public class Recruiter:IEntity
    {
      public  int Id { get; set; }
       public string Name; //required
       public string Email; //Valid format
       public int CompanyId;

        public static string Header =>
           $"{"Id",-4}{"Name",-16}{"Email",-26}CompanyId";

        public override string ToString() =>
            $"{Id,-4}{Name,-16}{Email,-26}{CompanyId}";
    }
}
