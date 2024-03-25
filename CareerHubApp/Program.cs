using CareerHubApp.App;
using CareerHubApp.Exceptions;
using CareerHubApp.Repository;
using CareerHubApp.Services;

namespace CareerHubApp
{
    internal class Program
    {
        static void Main(string[] args)
        {

            try
            {
                IDatabaseManager databaseManager = new DatabaseManager();
                CareerHubSystem careerHubSystem = new CareerHubSystem(databaseManager);
                careerHubSystem.App();
            }
           
            catch (InvalidEmailFormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch(DatabaseConnectionException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch(NegativeSalaryException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }
    }
}
