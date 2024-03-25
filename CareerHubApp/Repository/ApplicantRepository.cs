using CareerHubApp.Models;
using CareerHubApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerHubApp.Repository
{
    public class ApplicantRepository : IApplicantService
    {
        private IDatabaseManager _databaseManager;

        public ApplicantRepository(IDatabaseManager databaseManager)
        {
            _databaseManager = databaseManager;
        }
        void IApplicantService.ApplyForJob(string applicantFirstName, string applicantLastName, int jobID, string coverLetter)
        {
            // Retrieve the applicant ID using the provided first name and last name
            int applicantID = GetApplicantID(applicantFirstName, applicantLastName);

            if (applicantID == -1)
            {
                Console.WriteLine("Error: Applicant not found.");
                return;
            }

            // Create a new job application object
            var newApplication = new JobApplication
            {
               
                JobID = jobID,
                ApplicantID = applicantID,
                ApplicationDate = DateTime.Now,
                CoverLetter = coverLetter
                
            };

            // Insert the new job application into the database
            _databaseManager.InsertJobApplication(newApplication);
        }

        private int GenerateApplicationID()
        {
            int lastUsedID = 10000;//Initial value
            lastUsedID++;
            return lastUsedID;
        }

        private int GetApplicantID(string applicantFirstName, string applicantLastName)
        {
            int applicantID = _databaseManager.GetApplicantID(applicantFirstName, applicantLastName);

            return applicantID;
        }

        Applicant IApplicantService.CreateProfile(string firstName, string lastName, string email, string phone, string resume)
        {
            // Create a new Applicant object
            var newApplicant = new Applicant
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Phone = phone,
                Resume = resume         
            };

            // Insert new applicant to dat
            _databaseManager.InsertApplicant(newApplicant);

            // Return the newly created applicant object
            return newApplicant;
        }


    }
}
