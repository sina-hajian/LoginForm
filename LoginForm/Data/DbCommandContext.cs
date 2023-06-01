using Core.Common.Encript;
using LoginForm.Model;
using LoginForm.Model.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace LoginForm.Data
{
    public static class DbCommandContext
    {
        private static readonly string conString = Constant.ConnectionString;
        public static void SqlRegisterCommand()
        {
            

            string queryStringCreateUserTable = "" +
                 "CREATE TABLE Users (" +
                 "Id int  IDENTITY(1,1) PRIMARY KEY," +
                  "Name varchar(20) NOT NULL," +
                  "FamilyName varchar(20) NOT NULL," +
                  "NormalizedUserName varchar(20) NOT NULL," +
                  "Password varchar(200) NOT NULL," +
                  "Email varchar(60) NOT NULL," +
                  "PhoneNumber varchar(60) NOT NULL," +
                  "SecurityStamp int NOT NULL," +
                   "Salt varchar(80) NOT NULL," +
                 "CreationDate bigint NOT NULL," +
                 "UpdateDate bigint NOT NULL," +
                 ");";

            string queryStringCreateRoleTable = "" +
                "CREATE TABLE Roles (" +
                "Id int  IDENTITY(1,1) PRIMARY KEY," +
                 "RoleName varchar(20) NOT NULL," +
                "CreationDate bigint NOT NULL," +
                "UpdateDate bigint NOT NULL," +
                ");";

            string queryStringCreateRoleToUserTable = "" +
               "CREATE TABLE UserToRoles (" +
               "Id int  IDENTITY(1,1) PRIMARY KEY," +
                "RoleId  int FOREIGN KEY REFERENCES Roles(Id)," +
                 "UserId int FOREIGN KEY REFERENCES Users(Id)," +
               "CreationDate bigint NOT NULL," +
               "UpdateDate bigint NOT NULL," +
               ");";

            var queryCreateDatabase = queryStringCreateUserTable + queryStringCreateRoleTable + queryStringCreateRoleToUserTable;


            SqlConnection con = new SqlConnection(conString);


            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand(queryCreateDatabase, con);
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();



                reader.Close();

            }




        }
       
        public static void dropDatabase()
        {

            string conStringToWhole = Constant.ConnectionStringWhole;

            string queryFirst = " DROP DATABASE TestSinaHajian;";

            SqlConnection con = new SqlConnection(conStringToWhole);


            using (SqlConnection connection = new SqlConnection(conStringToWhole))
            {
                SqlCommand cmd = new SqlCommand(queryFirst, con);
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                reader.Close();
            }
        }
        public static void CreatDatabase()
        {
            string conStringToWhole = Constant.ConnectionStringWhole;

            string queryFirst = "CREATE DATABASE TestSinaHajian;";

            SqlConnection con = new SqlConnection(conStringToWhole);


            using (SqlConnection connection = new SqlConnection(conStringToWhole))
            {
                SqlCommand cmd = new SqlCommand(queryFirst, con);
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                reader.Close();
            }
        }
   
        public static bool Register(string name , string familyName ,string normalizedUserName ,string password , string email , string phoneNumber , int securityStamp , string salt)
        {
            string conString = Constant.ConnectionString;
            string query = "insert INTO Users (Name ," +
                "FamilyName," +
                "NormalizedUserName," +
                "Password," +
                "Email," +
                "PhoneNumber ," +
                "SecurityStamp," +
                "Salt," +
                "CreationDate," +
                "UpdateDate)" +
                "VALUES" +
                $"('{name}' ," +
                $"'{familyName}' ," +
                $"'{normalizedUserName}'," +
                $"'{password}' ," +
                $"'{email}'," +
                $"'{phoneNumber}' ," +
                $"{securityStamp}," +
                $"'{salt}'," +
                $"{DateTime.Now.Ticks}," +
                $"{DateTime.Now.Ticks});";

            SqlConnection con = new SqlConnection(conString);


            using (SqlConnection connection = new SqlConnection(conString))
            {
               
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                try
                { 
                    SqlDataReader writer = cmd.ExecuteReader();
                    writer.Close();
                    return true;
                } catch {
                    return false;
                }
                finally
                {
                    con.Close();
                }
               



                

            }




        }
   
        public static bool IsDupplicate(string userNme)
        {
            var query = $"select * from Users WHERE NormalizedUserName='{userNme}';";

            SqlConnection con = new SqlConnection(conString);
            SqlCommand command = new SqlCommand(query, con);
           
            object obj = (object)null;
            try
            {
                con.Open();
                obj = command.ExecuteScalar();

              
                if (obj != null)
                {
                    return true;
                }


            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                con.Close();
            }
            return false;


        }
  

        public static bool Login(string userName , string password)
        {
            var query = $"select * from Users WHERE NormalizedUserName='{userName}';";
            
            SqlConnection con = new SqlConnection(conString);
            SqlCommand command = new SqlCommand(query, con);
            try
            {
                con.Open();
                SqlDataReader reader = command.ExecuteReader();
                var slt = "";
                var pass = "";
                while (reader.Read())
                {

                     slt = reader["Salt"].ToString();
                     pass = reader["Password"].ToString();
                }
                reader.Close();
                if(slt != "")
                {
                    return pass.Equals(Encrypter.GetHash(password, slt)); ;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                con.Close();
            }
            return false;

        }
    }
}
