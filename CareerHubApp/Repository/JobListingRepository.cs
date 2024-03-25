using CareerHubApp.Models;
using CareerHubApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerHubApp.Repository
{
    public class JobListingRepository : IJobListingService
    {
        private IDatabaseManager _databaseManager;

        public JobListingRepository(IDatabaseManager databaseManager)
        {
            _databaseManager = databaseManager;
        }
        void IJobListingService.Apply(int jobID, int applicantID, string coverLetter)
        {
            _databaseManager.Apply(jobID, applicantID, coverLetter);
        }

        List<Applicant> IJobListingService.GetApplicants(int jobId)
        {
            List<Applicant> applicantList = _databaseManager.GetApplicantsForJob(jobId);
            return applicantList;
        }
    }
}
