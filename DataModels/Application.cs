using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobApplicationTracker.DataModels
{
    public enum AppStatus
    {
        Submitted, UnderReview, Interview, Accepted, Rejected
    }
    public class Application:IEntity
    {
        public int Id { get; set; }
        public int JobId;
        public int CandidateId;
        public DateTime AppliedDate;
        public AppStatus Status;

        public static string Header =>
          $"{"Id",-4}{"JobId",-7}{"CandidateId",-13}{"Applied",-12}Status";

        public override string ToString() =>
            $"{Id,-4}{JobId,-7}{CandidateId,-13}{AppliedDate:yyyy-MM-dd}  {Status}";
    }
}
