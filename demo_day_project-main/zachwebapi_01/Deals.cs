using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace webapi_01
{
    public class Deals
    {
        public int DealId { get; set; }
        public int RestaurantId { get; set; }
        public int DayOfWeekId { get; set; }
        public string? DealName{ get; set; }
        public string? DealDay { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
        public int DealCount { get; set; }

        public Deals()
        {
        }

        public Deals(int restaurantId, int dayOfWeekId, string dealName, string dealDay, string startDate, string endDate)
        {
            RestaurantId = restaurantId;
            DayOfWeekId = dayOfWeekId;
            DealName = dealName;
            DealDay = dealDay;
            StartDate = startDate;
            EndDate = endDate;
        }

        public Deals(int dealId, int restaurantId, int dayOfWeekId, string dealName, string dealDay, string startDate, string endDate)
        {
            DealId = dealId;
            RestaurantId = restaurantId;
            DayOfWeekId = dayOfWeekId;
            DealName = dealName;
            DealDay = dealDay;
            StartDate = startDate;
            EndDate = endDate;
        }

        public static List<Deals> GetDeals(SqlConnection sqlConnection)
        {
            List<Deals> deals = new List<Deals>();

            string sql = "select RestaurantId, DayOfWeekId, DealName, DealDay, StartDate, EndDate from Deals;";
            SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.Text;
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                Deals deal = new Deals();

                deal.RestaurantId = Convert.ToInt32(sqlDataReader["RestaurantId"].ToString());
                deal.DayOfWeekId = Convert.ToInt32(sqlDataReader["DayOfWeekId"].ToString());
                deal.DealName = sqlDataReader["DealName"].ToString();
                deal.DealDay = sqlDataReader["DealDay"].ToString();
                deal.StartDate = (sqlDataReader["StartDate"].ToString());
                deal.EndDate = (sqlDataReader["EndDate"].ToString());


                deals.Add(deal);
            }

            return deals;
        }

        public static List<Deals> SearchDeals(SqlConnection sqlConnection, string search = "", int pageSize = 20, int pageNumber = 1)
        {
            List<Deals> deals = new List<Deals>();

            string sql = "select p.DealId, d.RestaurantId, d.DayOfWeekId, d.DealName, "  
            + "dw.DayName as DealDay, d.StartDate, d.EndDate, p.[Count] from (Select d.DealId, count(*) over () as [Count] "
            + "From Deals d "
            + "join DaysOfWeek dw on dw.DayOfWeekId = d.DayOfWeekId "  
            + "where d.DealName like '%' + @Search + '%' or dw.DayName like '%' + @Search + '%' " 
            + "order by DealId offset @PageSize * (@PageNumber - 1) rows fetch next @PageSize rows only) p " 
            + "join Deals d on p.DealId = d.DealId "
            + "join DaysOfWeek dw on dw.DayOfWeekId = d.DayOfWeekId "
            + "order by 1;";

            SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.Text;

            SqlParameter paramSearch = new SqlParameter("@Search", search);
            SqlParameter paramPageSize = new SqlParameter("@PageSize", pageSize);
            SqlParameter paramPageNumber = new SqlParameter("@PageNumber", pageNumber);

            paramSearch.DbType = System.Data.DbType.String;
            paramPageSize.DbType = System.Data.DbType.Int32;
            paramPageNumber.DbType = System.Data.DbType.Int32;

            sqlCommand.Parameters.Add(paramSearch);
            sqlCommand.Parameters.Add(paramPageSize);
            sqlCommand.Parameters.Add(paramPageNumber);

            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                Deals deal = new Deals();

                deal.DealId = Convert.ToInt32(sqlDataReader["DealId"].ToString());
                deal.RestaurantId = Convert.ToInt32(sqlDataReader["RestaurantId"].ToString());
                deal.DayOfWeekId = Convert.ToInt32(sqlDataReader["DayOfWeekId"].ToString());
                deal.DealName = sqlDataReader["DealName"].ToString();
                deal.DealDay = sqlDataReader["DealDay"].ToString();
                deal.StartDate = (sqlDataReader["StartDate"].ToString());
                deal.EndDate = (sqlDataReader["EndDate"].ToString());
                deal.DealCount = Convert.ToInt32(sqlDataReader["Count"].ToString());

                deals.Add(deal);
            }

            return deals;
        }

        public static int InsertDeal(Deals deals, SqlConnection sqlConnection)
        {
            string sql = "insert into Deals (RestaurantId, DayOfWeekId, DealName, DealDay, StartDate, EndDate) values (@RestaurantId, @DayOfWeekId, @DealName, @DealDay, @StartDate, @EndDate);";

            SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.Text;

            SqlParameter paramRestaurantId = new SqlParameter("@RestaurantId", deals.RestaurantId);
            SqlParameter paramDayOfWeekId = new SqlParameter("@DayOfWeekId", deals.DayOfWeekId);
            SqlParameter paramDealName = new SqlParameter("@DealName", deals.DealName);
            SqlParameter paramDealDay = new SqlParameter("@DealDay", deals.DealDay);
            SqlParameter paramStartDate = new SqlParameter("@StartDate", deals.StartDate);
            SqlParameter paramEndDate = new SqlParameter("@EndDate", deals.EndDate);

            paramRestaurantId.DbType = System.Data.DbType.Int32;
            paramDayOfWeekId.DbType = System.Data.DbType.Int32;
            paramDealName.DbType = System.Data.DbType.String;
            paramDealDay.DbType = System.Data.DbType.String;
            paramStartDate.DbType = System.Data.DbType.String; //may need to change this variable type
            paramEndDate.DbType = System.Data.DbType.String; //may need to change this variable type

            sqlCommand.Parameters.Add(paramRestaurantId);
            sqlCommand.Parameters.Add(paramDayOfWeekId);
            sqlCommand.Parameters.Add(paramDealName);
            sqlCommand.Parameters.Add(paramDealDay);
            sqlCommand.Parameters.Add(paramStartDate);
            sqlCommand.Parameters.Add(paramEndDate);

            int rowsAffected = sqlCommand.ExecuteNonQuery();
            return rowsAffected;
        }

        public static int UpdateDeals(Deals deals, SqlConnection sqlConnection)
        {
            string sql = "update Deals set RestaurantId = @RestaurantId, DayOfWeekId = @DayOfWeekId, DealName = @DealName, DealDay = @DealDay, StartDate = @StartDate, EndDate = @EndDate where DealId = @DealId;";


            SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.Text;

            SqlParameter paramDealId = new SqlParameter("@DealId", deals.DealId);
            SqlParameter paramRestaurantId = new SqlParameter("@RestaurantId", deals.RestaurantId);
            SqlParameter paramDayOfWeekId = new SqlParameter("@DayOfWeekId", deals.DayOfWeekId);
            SqlParameter paramDealName = new SqlParameter("@DealName", deals.DealName);
            SqlParameter paramDealDay = new SqlParameter("@DealDay", deals.DealDay);
            SqlParameter paramStartDate = new SqlParameter("@StartDate", deals.StartDate);
            SqlParameter paramEndDate = new SqlParameter("@EndDate", deals.EndDate);

            paramDealId.DbType = System.Data.DbType.Int32;
            paramRestaurantId.DbType = System.Data.DbType.Int32;
            paramDayOfWeekId.DbType = System.Data.DbType.Int32;
            paramDealName.DbType = System.Data.DbType.String;
            paramDealDay.DbType = System.Data.DbType.String;
            paramStartDate.DbType = System.Data.DbType.String; //may need to change this variable type
            paramEndDate.DbType = System.Data.DbType.String; //may need to change this variable type

            sqlCommand.Parameters.Add(paramDealId);
            sqlCommand.Parameters.Add(paramRestaurantId);
            sqlCommand.Parameters.Add(paramDayOfWeekId);
            sqlCommand.Parameters.Add(paramDealName);
            sqlCommand.Parameters.Add(paramDealDay);
            sqlCommand.Parameters.Add(paramStartDate);
            sqlCommand.Parameters.Add(paramEndDate);

            int rowsAffected = sqlCommand.ExecuteNonQuery();
            return rowsAffected;
        }

        public static int DeleteDeal(int DealId, SqlConnection sqlConnection)
        {
            string sql = "delete from Deals where DealId = @DealId;";

            SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.Text;

            SqlParameter paramDealId = new SqlParameter("@DealId", DealId);
            paramDealId.DbType = System.Data.DbType.Int32;
            sqlCommand.Parameters.Add(paramDealId);

            int rowsAffected = sqlCommand.ExecuteNonQuery();
            return rowsAffected;
        }

        // public void ShowEmployee()
        // {
        //     Console.WriteLine($"{EmployeeId}, {LastName}, {FirstName}, {Salary}");
        // }

        // public static void ShowEmployees(List<Employee> employees)
        // {
        //     foreach (Employee employee in employees)
        //     {
        //         employee.ShowEmployee();
        //     }
        // }

    }
}