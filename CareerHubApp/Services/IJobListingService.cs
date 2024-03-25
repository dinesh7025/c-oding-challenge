using CareerHubApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerHubApp.Services
{
    public interface IJobListingService
    {
        List<Applicant> GetApplicants(int jobId);
        void Apply(int jobID, int applicantID, string coverLetter);
    }
}
