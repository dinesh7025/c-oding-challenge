using CareerHubApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerHubApp.Services
{
    public interface IDatabaseManager
    {
        void InsertJobListing(JobListing job);
        void InsertCompany(Company company);
        void InsertApplicant(Applicant applicant);
        void InsertJobApplication(JobApplication application);
        List<JobListing> GetJobListings();
        List<Company> GetCompanies();
        List<Applicant> GetApplicants();
        List<JobApplication> GetApplicationsForJob(int jobID);
       

        int GetApplicantID(string firstName, string lastName);
        List<JobListing> GetJobsForCompany(int companyID);
        void PostJob(string jobTitle, string jobDescription, string jobLocation, decimal salary, string jobType, int companyID);
        void Apply(int jobID, int applicantID, string coverLetter);
        List<Applicant> GetApplicantsForJob(int jobID);
    }
}
