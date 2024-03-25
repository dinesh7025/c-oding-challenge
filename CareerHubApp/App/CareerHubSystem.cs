using CareerHubApp.Exceptions;
using CareerHubApp.Models;
using CareerHubApp.Repository;
using CareerHubApp.Services;

namespace CareerHubApp.App
{
    public class CareerHubSystem
    {
        private IDatabaseManager _databaseManager;
        private IApplicantService _applicantService;

        public CareerHubSystem(IDatabaseManager databaseManager)
        {
            _databaseManager = databaseManager;
            _applicantService = new ApplicantRepository(databaseManager);
        }
        public void App()
        {
            while (true)
            {
                Console.WriteLine("Menu:");
                Console.WriteLine("1. User");
                Console.WriteLine("2. Company");
                Console.WriteLine("3. Exit");
                Console.Write("Enter your choice: ");

                int choice;
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    switch (choice)
                    {
                        case 1:
                            UserMenu();
                            break;
                        case 2:
                            CompanyMenu();
                            break;
                        case 3:
                            Console.WriteLine("Exiting...");
                            return;
                        default:
                            Console.WriteLine("Invalid choice. Please enter a number between 1 and 3.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please enter a number between 1 and 3.");
                }

                Console.WriteLine();
            }
        }

        private void CompanyMenu()
        {
            while (true)
            {
                Console.WriteLine("Company Menu:");
                Console.WriteLine("1. Create Company");
                Console.WriteLine("2. Post Job");
                Console.WriteLine("3. View Applicant List");
                Console.WriteLine("4. Back to Main Menu");
                Console.Write("Enter your choice: ");

                int choice;
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    switch (choice)
                    {
                        case 1:
                            CreateCompany();
                            break;
                        case 2:
                            PostJob();
                            break;
                        case 3:
                            ViewApplicantList();
                            break;
                        case 4:
                            return;
                        default:
                            Console.WriteLine("Invalid choice. Please enter a number between 1 and 4.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please enter a number between 1 and 4.");
                }

                Console.WriteLine();
            }
        }

        private void ViewApplicantList()
        {
            List<Applicant> applicants = _databaseManager.GetApplicants();

            if (applicants.Count == 0)
            {
                Console.WriteLine("No applicants found.");
                return;
            }

            // Print list
            Console.WriteLine("List of Applicants:");
            foreach (var applicant in applicants)
            {
                Console.WriteLine($"Applicant ID: {applicant.ApplicantID}");
                Console.WriteLine($"First Name: {applicant.FirstName}");
                Console.WriteLine($"Last Name: {applicant.LastName}");
                Console.WriteLine($"Email: {applicant.Email}");
                Console.WriteLine($"Phone: {applicant.Phone}");
                Console.WriteLine($"Resume: {applicant.Resume}");
                Console.WriteLine();
            }
        }

        private void PostJob()
        {
            Console.WriteLine("Enter the job title:");
            string jobTitle = Console.ReadLine();

            Console.WriteLine("Enter the job description:");
            string jobDescription = Console.ReadLine();

            Console.WriteLine("Enter the job location:");
            string jobLocation = Console.ReadLine();

            // salary
            decimal salary;
            while (true)
            {
                Console.WriteLine("Enter the salary:");
                if (!decimal.TryParse(Console.ReadLine(), out salary))
                {
                    Console.WriteLine("Invalid input. Please enter a valid salary.");
                    continue;
                }

                // if salary is negative
                if (salary < 0)
                {
                    throw new NegativeSalaryException("Salary cannot be negative.");
                }

                break;
            }
            Console.WriteLine("Enter the job type:");
            string jobType = Console.ReadLine();
            Console.WriteLine("Enter the company ID:");
            int companyID =Convert.ToInt32(Console.ReadLine());

            _databaseManager.PostJob(jobTitle, jobDescription, jobLocation, salary, jobType, companyID);

            Console.WriteLine("Job posted successfully.");
        }

        private void CreateCompany()
        {
            Console.WriteLine("Enter the company name:");
            string companyName = Console.ReadLine();

            Console.WriteLine("Enter the company location:");
            string location = Console.ReadLine();

            Company newCompany = new Company
            {
                CompanyName = companyName,
                Location = location
            };

            _databaseManager.InsertCompany(newCompany);

            Console.WriteLine("Company inserted successfully.");
        }

        private void UserMenu()
        {
            while (true)
            {
                Console.WriteLine("User Menu:");
                Console.WriteLine("1. Create Profile");
                Console.WriteLine("2. Apply for Job");
                Console.WriteLine("3. Back to Main Menu");
                Console.Write("Enter your choice: ");

                int choice;
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    switch (choice)
                    {
                        case 1:
                            CreateProfile();
                            break;
                        case 2:
                            ApplyForJob();
                            break;
                        case 3:
                            return;
                        default:
                            Console.WriteLine("Invalid choice. Please enter a number between 1 and 3.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please enter a number between 1 and 3.");
                }

                Console.WriteLine();
            }
        }

        private void ApplyForJob()
        {
            Console.WriteLine("Enter your first name:");
            string applicantFirstName = Console.ReadLine();

            Console.WriteLine("Enter your last name:");
            string applicantLastName = Console.ReadLine();

            Console.WriteLine("Enter the job ID you want to apply for:");
            int jobID;
            while (!int.TryParse(Console.ReadLine(), out jobID))
            {
                Console.WriteLine("Invalid input. Please enter a valid job ID:");
            }

            // Get user input for cover letter
            Console.WriteLine("Enter your cover letter:");
            string coverLetter = Console.ReadLine();

            // Call ApplyForJob method with user input
            _applicantService.ApplyForJob(applicantFirstName, applicantLastName, jobID, coverLetter);
        }

        private void CreateProfile()
        {
            Console.WriteLine("Enter first name:");
            string firstName = Console.ReadLine();

            Console.WriteLine("Enter last name:");
            string lastName = Console.ReadLine();

            Console.WriteLine("Enter email address:");
            string email = Console.ReadLine();

            // Validate email address
            if (!IsValidEmail(email))
            {
                throw new InvalidEmailFormatException("Invalid email format.");
            }

            Console.WriteLine("Enter phone number:");
            string phone = Console.ReadLine();

            Console.WriteLine("Enter resume:");
            string resume = Console.ReadLine();

            // Create a new applicant
            var newApplicant = new Applicant
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Phone = phone,
                Resume = resume
            };

            // Call createProfile method from _applicantService
            _applicantService.CreateProfile(firstName,lastName,email,phone,resume);
        }

        private bool IsValidEmail(string? email)
        {
            return email.Contains("@");
        }
    }
}
