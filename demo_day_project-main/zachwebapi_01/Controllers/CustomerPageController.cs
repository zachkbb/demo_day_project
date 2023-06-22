using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace webapi_01.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomerPageController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;

    public CustomerPageController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    [Route("/SearchCustomerDeals")]
    public Response SearchDeals(string pageSize = "10", string pageNumber = "1", string search = "")
    {
        Response response = new Response();
        try
        {
            List<CustomerPage> customerPage = new List<CustomerPage>();

            string connectionString = GetConnectionString();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                customerPage = CustomerPage.SearchDeals(sqlConnection, search, Convert.ToInt32(pageSize), Convert.ToInt32(pageNumber));
            }

            string message = "";

            if (customerPage.Count() > 0)
            {
                int customerDealCount = customerPage[0].CustomerDealCount;
                message = $"Found {customerDealCount} deals!";
            }
            else
            {
                message = "No deals met your search criteria.";
            }

            response.Result = "success";
            response.Message = message;
            response.CustomerPage = customerPage;
        }
        catch (Exception e)
        {
            response.Result = "failure";
            response.Message = e.Message;
        }
        return response;
    }

    [HttpGet]
    [Route("/SearchTodaysDeals")]
    public Response SearchTodaysDeals(string pageSize = "10", string pageNumber = "1", string search = "")
    {
        Response response = new Response();
        try
        {
            List<CustomerPage> customerPage = new List<CustomerPage>();

            string connectionString = GetConnectionString();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                customerPage = CustomerPage.SearchTodaysDeals(sqlConnection, search, Convert.ToInt32(pageSize), Convert.ToInt32(pageNumber));
            }

            string message = "";

            if (customerPage.Count() > 0)
            {
                int customerDealCount = customerPage[0].CustomerDealCount;
                message = $"Found {customerDealCount} deals!";
            }
            else
            {
                message = "No deals met your search criteria.";
            }

            response.Result = "success";
            response.Message = message;
            response.CustomerPage = customerPage;
        }
        catch (Exception e)
        {
            response.Result = "failure";
            response.Message = e.Message;
        }
        return response;
    }

    static string GetConnectionString()
    {
        string serverName = @"DESKTOP-S1T2KVQ\SQLEXPRESS"; //Change to the "Server Name" you see when you launch SQL Server Management Studio.
        string databaseName = "db_01"; //Change to the database where you created your Employee table.
        string connectionString = $"data source={serverName}; database={databaseName}; Integrated Security=true;";
        return connectionString;
    }

}