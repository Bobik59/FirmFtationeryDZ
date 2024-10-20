using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
namespace ConsoleApp1
{
    internal class Program
    {
        SqlConnection conn;
        SqlDataAdapter adapter;
        DataSet ds;

        public Program()
        {
            conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["MsSqlConnLibrary"].ConnectionString;
        }

        static void Main(string[] args)
        {
            Program pr = new Program();
            //pr.Task4("SELECT Top 1 CF.Name, SUM(S.SalePrice * S.QuantitySold) AS TotalSpent\r\nFROM [CustomerFirm] CF\r\nJOIN Sale S ON CF.Id = S.CustomerFirmId\r\nGROUP BY CF.Name\r\nORDER BY TotalSpent DESC\r\n;");
            //pr.Task5("Select Top 1 Tp.[TypeName], S.[QuantitySold]\r\nFrom [ProductType] TP \r\njoin [Product] P on P.[TypeId] = Tp.[Id]\r\njoin [Sale] S on S.[ProductId] = P.[Id]\r\nORDER BY S.[QuantitySold] DESC");
            //pr.Task6("Select Top 1 Tp.[TypeName], SUM((S.SalePrice - P.CostPrice) * s.QuantitySold) AS TotalProfit\r\nFrom [ProductType] TP \r\njoin [Product] P on P.[TypeId] = Tp.[Id]\r\njoin [Sale] S on S.[ProductId] = P.[Id]\r\nGROUP BY Tp.[TypeName]\r\nORDER BY TotalProfit DESC\r\n\r\n");
            //pr.Task7("SELECT p.Name, SUM(s.QuantitySold) AS TotalUnitsSold\r\nFROM Product p\r\nJOIN Sale s ON p.Id = s.ProductId\r\nGROUP BY p.Name\r\nORDER BY TotalUnitsSold DESC");
            pr.Task8("SELECT p.Name\r\nFROM Product p\r\nLEFT JOIN Sale s ON p.Id = s.ProductId AND s.SaleDate > @DataNO \r\nWHERE s.Id IS NULL;", "2024-10-10");
        }

        public void InsertQueryProductType()
        {
            try
            {
                conn.Open();
                string insertString = @"INSERT INTO ProductType (TypeName) VALUES ('Adhesives'), ('Organizational Tools');";
                SqlCommand cmd =
                new SqlCommand(insertString, conn);
                cmd.ExecuteNonQuery();
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }
        public void InsertQueryProduct()
        {
            try
            {
                conn.Open();
                string insertString = @"INSERT INTO Product (Name, TypeId, CostPrice) VALUES 
                                        ('Glue Stick', 4, 0.90);";
                SqlCommand cmd =
                new SqlCommand(insertString, conn);
                cmd.ExecuteNonQuery();
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }
        public void InsertQuerySalesManager()
        {
            try
            {
                conn.Open();
                string insertString = @"INSERT INTO SalesManager (Name) VALUES 
                                        ('Alice Green');";
                SqlCommand cmd =
                new SqlCommand(insertString, conn);
                cmd.ExecuteNonQuery();
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public void InsertQueryCustomerFirm()
        {
            try
            {
                conn.Open();
                string insertString = @"CustomerFirm (Name) VALUES 
                                        ('Global Office Supplies');";
                SqlCommand cmd = new SqlCommand(insertString, conn);
                cmd.ExecuteNonQuery();
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public void UpdateProduct()
        {
            try
            {
                conn.Open();
                string updateString = @"UPDATE Product 
                                SET Name = 'Super Glue', TypeId = 3, CostPrice = 1.20 
                                WHERE Id = 1;";
                SqlCommand cmd = new SqlCommand(updateString, conn);
                cmd.ExecuteNonQuery();
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public void UpdateProductType()
        {
            try
            {
                conn.Open();
                string updateString = @"UPDATE ProductType 
                                SET TypeName = 'Writing Tools' 
                                WHERE Id = 2;";
                SqlCommand cmd = new SqlCommand(updateString, conn);
                cmd.ExecuteNonQuery();
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public void UpdateSalesManager()
        {
            try
            {
                conn.Open();
                string updateString = @"UPDATE SalesManager 
                                SET Name = 'John Doe' 
                                WHERE Id = 1;";
                SqlCommand cmd = new SqlCommand(updateString, conn);
                cmd.ExecuteNonQuery();
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public void UpdateCustomerFirm()
        {
            try
            {
                conn.Open();
                string updateString = @"UPDATE CustomerFirm 
                                SET Name = 'Office Essentials' 
                                WHERE Id = 1;";
                SqlCommand cmd = new SqlCommand(updateString, conn);
                cmd.ExecuteNonQuery();
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public void DeleteProduct(int productId)
        {
            try
            {
                conn.Open();
                string deleteString = @"DELETE FROM Product WHERE Id = @ProductId;";
                SqlCommand cmd = new SqlCommand(deleteString, conn);
                cmd.Parameters.AddWithValue("@ProductId", productId);
                cmd.ExecuteNonQuery();
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public void DeleteSalesManager(int salesManagerId)
        {
            try
            {
                conn.Open();
                string deleteString = @"DELETE FROM SalesManager WHERE Id = @SalesManagerId;";
                SqlCommand cmd = new SqlCommand(deleteString, conn);
                cmd.Parameters.AddWithValue("@SalesManagerId", salesManagerId);
                cmd.ExecuteNonQuery();
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }
        public void DeleteProductType(int productTypeId)
        {
            try
            {
                conn.Open();
                string deleteString = @"DELETE FROM ProductType WHERE Id = @ProductTypeId;";
                SqlCommand cmd = new SqlCommand(deleteString, conn);
                cmd.Parameters.AddWithValue("@ProductTypeId", productTypeId);
                cmd.ExecuteNonQuery();
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public void DeleteCustomerFirm(int customerFirmId)
        {
            try
            {
                conn.Open();
                string deleteString = @"DELETE FROM CustomerFirm WHERE Id = @CustomerFirmId;";
                SqlCommand cmd = new SqlCommand(deleteString, conn);
                cmd.Parameters.AddWithValue("@CustomerFirmId", customerFirmId);
                cmd.ExecuteNonQuery();
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public void Task1()
        {
            try
            {
                conn.Open();
                string queryString = @"
                                        SELECT TOP 1 SM.[Name]
                                        FROM [SalesManager] SM
                                        JOIN [Sale] S ON S.[ManagerId] = SM.[Id]
                                        ORDER BY S.[QuantitySold] DESC;";

                SqlCommand cmd = new SqlCommand(queryString, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader["Name"]}");
                    }
                }
                reader.Close();
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }
        public void Task2()
        {
            try
            {
                conn.Open();
                string queryString = @"SELECT Top 1 SM.Name, SUM((s.SalePrice - p.CostPrice) * s.QuantitySold) AS TotalProfit
                                       FROM [SalesManager] SM
                                       JOIN [Sale] S ON SM.Id = S.ManagerId
                                       JOIN [Product] P ON S.ProductId = P.Id
                                       GROUP BY SM.Name
                                       ORDER BY TotalProfit DESC";

                SqlCommand cmd = new SqlCommand(queryString, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader["Name"]} - {reader["TotalProfit"]} ");
                    }
                }
                reader.Close();
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }
        public void Task3(string Data1, string Data2)
        {
            try
            {
                conn.Open();
                string queryString = @"SELECT TOP 1 sm.Name, SUM((s.SalePrice - p.CostPrice) * s.QuantitySold) AS TotalProfit
                                    FROM SalesManager sm
                                    JOIN Sale s ON sm.Id = s.ManagerId
                                    JOIN Product p ON s.ProductId = p.Id
                                    WHERE s.SaleDate BETWEEN @Data1 AND @Data2 
                                    GROUP BY sm.Name
                                    ORDER BY TotalProfit DESC";


                SqlCommand cmd = new SqlCommand(queryString, conn);
                cmd.Parameters.AddWithValue("@Data1", Data1);
                cmd.Parameters.AddWithValue("@Data2", Data2);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader["Name"]} - {reader["TotalProfit"]} ");
                    }
                }
                reader.Close();
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }
        public void Task4(string query)
        {
            try
            {
                conn.Open();
                string queryString = query;

                SqlCommand cmd = new SqlCommand(queryString, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader["Name"]} - {reader["TotalSpent"]} ");
                    }
                }
                reader.Close();
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public void Task5(string query)
        {
            try
            {
                conn.Open();
                string queryString = query;

                SqlCommand cmd = new SqlCommand(queryString, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader["TypeName"]} - {reader["QuantitySold"]} ");
                    }
                }
                reader.Close();
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }
        public void Task6(string query)
        {
            try
            {
                conn.Open();
                string queryString = query;

                SqlCommand cmd = new SqlCommand(queryString, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader["TypeName"]} - {reader["TotalProfit"]} ");
                    }
                }
                reader.Close();
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public void Task7(string query)
        {
            try
            {
                conn.Open();
                string queryString = query;

                SqlCommand cmd = new SqlCommand(queryString, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader["Name"]} - {reader["TotalUnitsSold"]}");
                    }
                }
                reader.Close();
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public void Task8(string query, string DataNO)
        {
            try
            {
                conn.Open();
                string queryString = query;

                SqlCommand cmd = new SqlCommand(queryString, conn);
                cmd.Parameters.AddWithValue("@DataNO", DataNO);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader["Name"]}");
                    }
                }
                reader.Close();
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }
    }
}