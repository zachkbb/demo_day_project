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

    [HttpGet]
    [Route("/InsertDeal")]
    public Response InsertDeal(int RestaurantId, int DayOfWeekId, string DealName, string DealDay, string StartDate, string EndDate)
    {
        Response response = new Response();
        try
        {
            List<Deals> deals = new List<Deals>();
            
            Deals deal = new Deals(RestaurantId, DayOfWeekId, DealName, DealDay, StartDate, EndDate);

            int rowsAffected = 0;

            string connectionString = GetConnectionString();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                rowsAffected = Deals.InsertDeal(deal, sqlConnection);
                deals = Deals.SearchDeals(sqlConnection);
            }

            response.Result = (rowsAffected == 1) ? "success" : "failure";
            response.Message = $"{rowsAffected} rows affected.";
            response.Deals = deals;
        }
        catch (Exception e)
        {
            response.Result = "failure";
            response.Message = e.Message;
        }

        return response;
    }

    [HttpGet]
    [Route("/UpdateDeals")]
    public Response UpdateDeals(int DealId, int RestaurantId, int DayOfWeekId, string DealName, string DealDay, string StartDate, string EndDate)
    {
        Response response = new Response();

        try
        {
            List<Deals> deals = new List<Deals>();
            Deals deal = new Deals(Convert.ToInt32(DealId), Convert.ToInt32(RestaurantId), Convert.ToInt32(DayOfWeekId), DealName, DealDay, StartDate, EndDate);

            int rowsAffected = 0;

            string connectionString = GetConnectionString();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                rowsAffected = Deals.UpdateDeals(deal, sqlConnection);
                deals = Deals.SearchDeals(sqlConnection);
            }

            response.Result = (rowsAffected == 1) ? "success" : "failure";
            response.Message = $"{rowsAffected} rows affected.";
            response.Deals = deals;
        }
        catch (Exception e)
        {
            response.Result = "failure";
            response.Message = e.Message;
        }

        return response;
    }

    [HttpGet]
    [Route("/DeleteDeals")]
    public Response DeleteDeals(string DealId)
    {
        Response response = new Response();

        try
        {
            List<Deals> deals = new List<Deals>();
            int rowsAffected = 0;

            string connectionString = GetConnectionString();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                rowsAffected = Deals.DeleteDeal(Convert.ToInt32(DealId), sqlConnection);
                deals = Deals.SearchDeals(sqlConnection);
            }

            response.Result = (rowsAffected == 1) ? "success" : "failure";
            response.Message = $"{rowsAffected} rows affected.";
            response.Deals = deals;
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