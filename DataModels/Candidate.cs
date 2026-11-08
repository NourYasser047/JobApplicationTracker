using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace JobApplicationTracker.DataModels
{
    public class Candidate:IEntity
    {
        public  int Id { get; set; }
        public string FullName; //required
        public string Email; // valid format, unique
        public string Phone;
        public int YearsOfExperience; //   0 or more

        public static string Header =>
            $"{"Id",-4}{"Name",-18}{"Email",-28}{"Phone",-14}Exp";

        public override string ToString() =>
            $"{Id,-4}{FullName,-18}{Email,-28}{Phone,-14}{YearsOfExperience}";
    }
}
