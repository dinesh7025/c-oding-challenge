using CareerHubApp.Models;
using CareerHubApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerHubApp.Repository
{
    internal class CompanyRepository : ICompanyService
    {
        private IDatabaseManager _databaseManager;

        public CompanyRepository(IDatabaseManager databaseManager)
        {
            _databaseManager = databaseManager;
        }
        List<JobListing> ICompanyService.GetJobs(int companyId)
        {
            return _databaseManager.GetJobsForCompany(companyId);
        }

         void ICompanyService.PostJob(string jobTitle, string jobDescription, string jobLocation, decimal salary, string jobType, int companyID)
        {
            _databaseManager.PostJob(jobTitle, jobDescription, jobLocation, salary, jobType, companyID);
        }
    }
}
