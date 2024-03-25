using CareerHubApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerHubApp.Services
{
    internal interface ICompanyService
    {
        List<JobListing> GetJobs(int companyId);
        void PostJob(string jobTitle, string jobDescription, string jobLocation, decimal salary, string jobType, int companyID);
    }
}
