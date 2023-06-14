using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace webapi_01.Controllers;

[ApiController]
[Route("[controller]")]
public class DealsController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;

    public DealsController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    [Route("/SearchDeals")]
    public Response SearchDeals(string pageSize = "10", string pageNumber = "1", string search = "")
    {
        Response response = new Response();
        try
        {
            List<Deals> deals = new List<Deals>();

            string connectionString = GetConnectionString();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                deals = Deals.SearchDeals(sqlConnection, search, Convert.ToInt32(pageSize), Convert.ToInt32(pageNumber));
            }

            string message = "";

            if (deals.Count() > 0)
            {
                int dealCount = deals[0].DealCount;
                message = $"Found {dealCount} deals!";
            }
            else
            {
                message = "No deals met your search criteria.";
            }

            response.Result = "success";
            response.Message = message;
            response.Deals = deals;
        }
        catch (Exception e)
        {
            response.Result = "failure";
            response.Message = e.Message;
        }
        return response;
    }

    // [HttpGet]
    // [Route("/InsertEmployee")]
    // public Response InsertEmployee(string lastName, string firstName, string salary)
    // {
    //     Response response = new Response();
    //     try
    //     {
    //         List<Employee> employees = new List<Employee>();

    //         Employee employee = new Employee(lastName, firstName, Convert.ToDecimal(salary));

    //         int rowsAffected = 0;

    //         string connectionString = GetConnectionString();
    //         using (SqlConnection sqlConnection = new SqlConnection(connectionString))
    //         {
    //             sqlConnection.Open();
    //             rowsAffected = Employee.InsertEmployee(employee, sqlConnection);
    //             employees = Employee.SearchEmployees(sqlConnection);
    //         }

    //         response.Result = (rowsAffected == 1) ? "success" : "failure";
    //         response.Message = $"{rowsAffected} rows affected.";
    //         response.Employees = employees;
    //     }
    //     catch (Exception e)
    //     {
    //         response.Result = "failure";
    //         response.Message = e.Message;
    //     }

    //     return response;
    // }

    // [HttpGet]
    // [Route("/UpdateEmployee")]
    // public Response UpdateEmployee(string employeeId, string lastName, string firstName, string salary)
    // {
    //     Response response = new Response();

    //     try
    //     {
    //         List<Employee> employees = new List<Employee>();
    //         Employee employee = new Employee(Convert.ToInt32(employeeId), lastName, firstName, Convert.ToDecimal(salary));

    //         int rowsAffected = 0;

    //         string connectionString = GetConnectionString();
    //         using (SqlConnection sqlConnection = new SqlConnection(connectionString))
    //         {
    //             sqlConnection.Open();
    //             rowsAffected = Employee.UpdateEmployee(employee, sqlConnection);
    //             employees = Employee.SearchEmployees(sqlConnection);
    //         }

    //         response.Result = (rowsAffected == 1) ? "success" : "failure";
    //         response.Message = $"{rowsAffected} rows affected.";
    //         response.Employees = employees;
    //     }
    //     catch (Exception e)
    //     {
    //         response.Result = "failure";
    //         response.Message = e.Message;
    //     }

    //     return response;
    // }

    // [HttpGet]
    // [Route("/DeleteEmployee")]
    // public Response DeleteEmployee(string employeeId)
    // {
    //     Response response = new Response();

    //     try
    //     {
    //         List<Employee> employees = new List<Employee>();
    //         int rowsAffected = 0;

    //         string connectionString = GetConnectionString();
    //         using (SqlConnection sqlConnection = new SqlConnection(connectionString))
    //         {
    //             sqlConnection.Open();
    //             rowsAffected = Employee.DeleteEmployee(Convert.ToInt32(employeeId), sqlConnection);
    //             employees = Employee.SearchEmployees(sqlConnection);
    //         }

    //         response.Result = (rowsAffected == 1) ? "success" : "failure";
    //         response.Message = $"{rowsAffected} rows affected.";
    //         response.Employees = employees;
    //     }
    //     catch (Exception e)
    //     {
    //         response.Result = "failure";
    //         response.Message = e.Message;
    //     }

    //     return response;
    // }

    static string GetConnectionString()
    {
        string serverName = @"DESKTOP-S1T2KVQ\SQLEXPRESS"; //Change to the "Server Name" you see when you launch SQL Server Management Studio.
        string databaseName = "db_01"; //Change to the database where you created your Employee table.
        string connectionString = $"data source={serverName}; database={databaseName}; Integrated Security=true;";
        return connectionString;
    }

}