using CareerHubApp.Models;
using CareerHubApp.Services;
using CareerHubApp.Utility;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerHubApp.Repository
{
    public class DatabaseManager : IDatabaseManager
    {
        public void Apply(int jobID, int applicantID, string coverLetter)
        { 

            using (SqlConnection connection = new SqlConnection(DBUtil.GetConnectionString()))
            {
                string query = @"INSERT INTO JobApplications ( job_id, applicant_id, application_date, cover_letter)
                                 VALUES (@JobID, @ApplicantID, @ApplicationDate, @CoverLetter)";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@JobID", jobID);
                command.Parameters.AddWithValue("@ApplicantID", applicantID);
                command.Parameters.AddWithValue("@ApplicationDate", DateTime.Now);
                command.Parameters.AddWithValue("@CoverLetter", coverLetter);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        private int GenerateApplicationID()
        {
            int lastId = 100;
            lastId++;
            return lastId;
        }

        int IDatabaseManager.GetApplicantID(string firstName, string lastName)
        {
            int applicantID = -1;

            //Coneect Sql with using
            using (SqlConnection connection = new SqlConnection(DBUtil.GetConnectionString()))
            {
                string query = "SELECT applicant_id FROM Applicants WHERE first_name = @FirstName AND last_name = @LastName";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@FirstName", firstName);
                command.Parameters.AddWithValue("@LastName", lastName);

                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    applicantID = Convert.ToInt32(result);
                }
            }

            return applicantID;
        }

        List<Applicant> IDatabaseManager.GetApplicants()
        {
            List<Applicant> applicants = new List<Applicant>();

            using (SqlConnection connection = new SqlConnection(DBUtil.GetConnectionString()))
            {
                string query = @"SELECT * FROM Applicants";

                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Applicant applicant = new Applicant
                    {
                        ApplicantID = (int)reader["applicant_id"],
                        FirstName = (string)reader["first_name"],
                        LastName = (string)reader["last_name"],
                        Email = (string)reader["email"],
                        Phone = (string)reader["phone"],
                        Resume = (string)reader["resume"]
                    };

                    applicants.Add(applicant);
                }
            }

            return applicants;
        }

        List<Applicant> IDatabaseManager.GetApplicantsForJob(int jobID)
        {
            List<Applicant> applicants = new List<Applicant>();

            using (SqlConnection connection = new SqlConnection(DBUtil.GetConnectionString()))
            {
                string query = @"SELECT a.* FROM Applicants a
                                 INNER JOIN JobApplications j ON a.applicant_id = j.applicant_id
                                 WHERE j.job_id = @JobID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@JobID", jobID);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Applicant applicant = new Applicant
                    {
                        ApplicantID = (int)reader["applicant_id"],
                        FirstName = (string)reader["first_name"],
                        LastName = (string)reader["last_name"],
                        Email = (string)reader["email"],
                        Phone = (string)reader["phone"],
                        Resume = (string)reader["resume"]
                    };

                    applicants.Add(applicant);
                }
            }

            return applicants;
        }

        List<JobApplication> IDatabaseManager.GetApplicationsForJob(int jobID)
        {
            List<JobApplication> applications = new List<JobApplication>();

            using (SqlConnection connection = new SqlConnection(DBUtil.GetConnectionString()))
            {
                string query = @"SELECT * FROM JobApplications WHERE job_id = @JobID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@JobID", jobID);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    JobApplication application = new JobApplication
                    {
                        ApplicationID = (int)reader["application_id"],
                        JobID = (int)reader["job_id"],
                        ApplicantID = (int)reader["applicant_id"],
                        ApplicationDate = (DateTime)reader["application_date"],
                        CoverLetter = (string)reader["cover_letter"]
                        
                    };

                    applications.Add(application);
                }
            }

            return applications;
        }

        List<Company> IDatabaseManager.GetCompanies()
        {
            List<Company> companies = new List<Company>();

            using (SqlConnection connection = new SqlConnection(DBUtil.GetConnectionString()))
            {
                string query = @"SELECT * FROM Companies";

                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Company company = new Company
                    {
                        CompanyID = (int)reader["company_id"],
                        CompanyName = (string)reader["company_name"],
                        Location = (string)reader["location"]
                    };

                    companies.Add(company);
                }
            }
            return companies;
        }

        List<JobListing> IDatabaseManager.GetJobListings()
        {
            List<JobListing> jobListings = new List<JobListing>();

            using (SqlConnection connection = new SqlConnection(DBUtil.GetConnectionString()))
            {
                string query = @"SELECT * FROM JobListing";

                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    JobListing jobListing = new JobListing
                    {
                        JobID = (int)reader["job_id"],
                        CompanyID = (int)reader["company_id"],
                        JobTitle = (string)reader["job_title"],
                        JobDescription = (string)reader["job_description"],
                        JobLocation = (string)reader["job_location"],
                        Salary = (decimal)reader["salary"],
                        JobType = (string)reader["job_type"],
                        PostedDate = (DateTime)reader["posted_date"]    
                    };

                    jobListings.Add(jobListing);
                }
            }

            return jobListings;
        }

        List<JobListing> IDatabaseManager.GetJobsForCompany(int companyID)
        {

            List<JobListing> jobs = new List<JobListing>();

            using (SqlConnection connection = new SqlConnection(DBUtil.GetConnectionString()))
            {
                string query = "SELECT * FROM JobListing WHERE company_id = @CompanyID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@CompanyID", companyID);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    JobListing job = new JobListing
                    {
                        JobID = (int)reader["job_id"],
                        CompanyID = (int)reader["company_id"], 
                        JobTitle = (string)reader["job_title"], 
                        JobDescription = (string)reader["job_description"], 
                        JobLocation = (string)reader["job_location"],
                        Salary = (decimal)reader["salary"],
                        JobType= (string)reader["job_type"],
                        PostedDate = (DateTime)reader["posted_date"] 
                    };

                    jobs.Add(job);
                }
            }

            return jobs;
        }

        void IDatabaseManager.InsertApplicant(Applicant applicant)
        {
            using(SqlConnection connection = new SqlConnection(DBUtil.GetConnectionString()))
            {
                string query = @"INSERT INTO Applicants (first_name, last_name, email, phone, resume)
                                 VALUES (@FirstName, @LastName, @Email, @Phone, @Resume)";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@FirstName", applicant.FirstName);
                command.Parameters.AddWithValue("@LastName", applicant.LastName);
                command.Parameters.AddWithValue("@Email", applicant.Email);
                command.Parameters.AddWithValue("@Phone", applicant.Phone);
                command.Parameters.AddWithValue("@Resume", applicant.Resume);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        void IDatabaseManager.InsertCompany(Company company)
        {
            using (SqlConnection connection = new SqlConnection(DBUtil.GetConnectionString()))
            {
                string query = @"INSERT INTO Companies (company_name, location)
                                 VALUES (@CompanyName, @Location)";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@CompanyName", company.CompanyName);
                command.Parameters.AddWithValue("@Location", company.Location);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        void IDatabaseManager.InsertJobApplication(JobApplication application)
        {
            using (SqlConnection connection = new SqlConnection(DBUtil.GetConnectionString()))
            {
                string query = @"INSERT INTO JobApplications (job_id, applicant_id, application_date, cover_letter)
                                 VALUES (@JobID, @ApplicantID, @ApplicationDate, @CoverLetter)";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@JobID", application.JobID);
                command.Parameters.AddWithValue("@ApplicantID", application.ApplicantID);
                command.Parameters.AddWithValue("@ApplicationDate", application.ApplicationDate);
                command.Parameters.AddWithValue("@CoverLetter", application.CoverLetter);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
   
        void IDatabaseManager.InsertJobListing(JobListing job)
        {
            using (SqlConnection connection = new SqlConnection(DBUtil.GetConnectionString()))
            {
                string query = @"INSERT INTO JobListing (company_id, job_title, job_description, job_location, salary, job_type, posted_date)
                                 VALUES (@CompanyID, @JobTitle, @JobDescription, @JobLocation, @Salary, @JobType, @PostedDate)";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@CompanyID", job.CompanyID);
                command.Parameters.AddWithValue("@JobTitle", job.JobTitle);
                command.Parameters.AddWithValue("@JobDescription", job.JobDescription);
                command.Parameters.AddWithValue("@JobLocation", job.JobLocation);
                command.Parameters.AddWithValue("@Salary", job.Salary);
                command.Parameters.AddWithValue("@JobType", job.JobType);
                command.Parameters.AddWithValue("@PostedDate", job.PostedDate);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        void IDatabaseManager.PostJob(string jobTitle, string jobDescription, string jobLocation, decimal salary, string jobType, int companyID)
        {
            using (SqlConnection connection = new SqlConnection(DBUtil.GetConnectionString()))
            {
                string query = @"INSERT INTO JobListing (company_id, job_title, job_description, job_location, salary, job_type, posted_date)
                                 VALUES (@CompanyID, @JobTitle, @JobDescription, @JobLocation, @Salary, @JobType, @PostedDate)";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@CompanyID", companyID);
                command.Parameters.AddWithValue("@JobTitle", jobTitle);
                command.Parameters.AddWithValue("@JobDescription", jobDescription);
                command.Parameters.AddWithValue("@JobLocation", jobLocation);
                command.Parameters.AddWithValue("@Salary", salary);
                command.Parameters.AddWithValue("@JobType", jobType);
                command.Parameters.AddWithValue("@PostedDate", DateTime.Now);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}


