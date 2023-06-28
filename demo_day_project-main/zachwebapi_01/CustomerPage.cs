using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace webapi_01
{
    public class CustomerPage
    {
        public string? RestaurantName { get; set; }
        public string? DealName{ get; set; }
        public string? DealDay { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
        public string? Website { get; set; }
        public int CustomerDealCount { get; set; }


        public CustomerPage()
        {
        }

        public CustomerPage(string restaurantName, string dealName, string dealDay, string startDate, string endDate, string webSite)
        {
            RestaurantName = restaurantName;
            DealName = dealName;
            DealDay = dealDay;
            StartDate = startDate;
            EndDate = endDate;
            Website = webSite;
        }

        //got rid of redundant constructor right here

        public static List<CustomerPage> GetDeals(SqlConnection sqlConnection)
        {
            List<CustomerPage> customerPage = new List<CustomerPage>();

            string sql = "select DealName, DealDay, StartDate, EndDate from Deals;";
            SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.Text;
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                CustomerPage customerPages = new CustomerPage();

                customerPages.DealName = sqlDataReader["DealName"].ToString();
                customerPages.DealDay = sqlDataReader["DealDay"].ToString();
                customerPages.StartDate = (sqlDataReader["StartDate"].ToString());
                customerPages.EndDate = (sqlDataReader["EndDate"].ToString());


                customerPage.Add(customerPages);
            }

            return customerPage;
        }

        public static List<CustomerPage> SearchDeals(SqlConnection sqlConnection, string search = "", int pageSize = 10, int pageNumber = 1)
        {
            List<CustomerPage> customerPage = new List<CustomerPage>();

            string sql = "select d.DealName, r.RestaurantName, "  
            + " dw.DayName as DealDay, d.StartDate, d.EndDate, r.Website, p.[Count] " 
            + " from (Select d.DealId, count(*) over () as [Count] "
            + " From Deals d "
            + " join DaysOfWeek dw on dw.DayOfWeekId = d.DayOfWeekId "
            + " join Restaurants r on r.RestaurantId = d.RestaurantId "  
            + " where d.DealName like '%' + @Search + '%' or dw.DayName like '%' + @Search + '%' or r.RestaurantName like '%' + @Search + '%' " 
            + " order by DealId offset @PageSize * (@PageNumber - 1) rows fetch next @PageSize rows only) p "
            + " join Deals d on d.DealId = p.DealId " 
            + " join Restaurants r on r.RestaurantId = d.RestaurantId "
            + " join DaysOfWeek dw on dw.DayOfWeekId = d.DayOfWeekId "
            + " order by 1;";

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
                CustomerPage customerPages = new CustomerPage();

                customerPages.RestaurantName = (sqlDataReader["RestaurantName"].ToString());
                customerPages.DealName = sqlDataReader["DealName"].ToString();
                customerPages.DealDay = sqlDataReader["DealDay"].ToString();
                customerPages.StartDate = (sqlDataReader["StartDate"].ToString());
                customerPages.EndDate = (sqlDataReader["EndDate"].ToString());
                customerPages.Website = (sqlDataReader["Website"].ToString());


                customerPage.Add(customerPages);
            }

            return customerPage;
        }

        public static List<CustomerPage> SearchTodaysDeals(SqlConnection sqlConnection, string search = "", int pageSize = 20, int pageNumber = 1)
        {
            int day = (int)DateTime.Now.DayOfWeek + 1;

            List<CustomerPage> customerPage = new List<CustomerPage>();

            string sql = "select d.DealName, r.RestaurantName, "  
            + " dw.DayName as DealDay, d.StartDate, d.EndDate, r.Website, p.[Count] " 
            + " from (Select d.DealId, count(*) over () as [Count] "
            + " From Deals d "
            + " join DaysOfWeek dw on dw.DayOfWeekId = d.DayOfWeekId "
            + " join Restaurants r on r.RestaurantId = d.RestaurantId "  
            + " where (d.DealName like '%' + @Search + '%' or dw.DayName like '%' + @Search + '%' or r.RestaurantName like '%' + @Search + '%') "
            + " and (d.DayOfWeekId = @Day or d.DayOfWeekId = 8)" 
            + " order by DealId offset @PageSize * (@PageNumber - 1) rows fetch next @PageSize rows only) p "
            + " join Deals d on d.DealId = p.DealId " 
            + " join Restaurants r on r.RestaurantId = d.RestaurantId "
            + " join DaysOfWeek dw on dw.DayOfWeekId = d.DayOfWeekId "
            + " order by 1;";

            SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.Text;

            SqlParameter paramSearch = new SqlParameter("@Search", search);
            SqlParameter paramPageSize = new SqlParameter("@PageSize", pageSize);
            SqlParameter paramPageNumber = new SqlParameter("@PageNumber", pageNumber);
            SqlParameter paramDay = new SqlParameter("@Day", day);

            paramSearch.DbType = System.Data.DbType.String;
            paramPageSize.DbType = System.Data.DbType.Int32;
            paramPageNumber.DbType = System.Data.DbType.Int32;
            paramDay.DbType = System.Data.DbType.Int32;

            sqlCommand.Parameters.Add(paramSearch);
            sqlCommand.Parameters.Add(paramPageSize);
            sqlCommand.Parameters.Add(paramPageNumber);
            sqlCommand.Parameters.Add(paramDay);

            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                CustomerPage customerPages = new CustomerPage();

                customerPages.RestaurantName = (sqlDataReader["RestaurantName"].ToString());
                customerPages.DealName = sqlDataReader["DealName"].ToString();
                customerPages.DealDay = sqlDataReader["DealDay"].ToString();
                customerPages.StartDate = (sqlDataReader["StartDate"].ToString());
                customerPages.EndDate = (sqlDataReader["EndDate"].ToString());
                customerPages.Website = (sqlDataReader["Website"].ToString());


                customerPage.Add(customerPages);
            }

            return customerPage;
        }

    }

}