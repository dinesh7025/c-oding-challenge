using CareerHubApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerHubApp.Services
{
    public interface IApplicantService
    {
        Applicant CreateProfile(string email, string firstName, string lastName, string phone, string resume                                                         );
        void ApplyForJob(string applicantFirstName, string applicantLastName, int jobID, string coverLetter);
    }
}
