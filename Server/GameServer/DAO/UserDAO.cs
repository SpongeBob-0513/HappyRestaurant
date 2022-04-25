using System;
using GameServer.Model;
using MySql.Data.MySqlClient;

namespace GameServer.DAO
{
    public class UserDAO
    {
        public User VerifyUser(MySqlConnection sqlConnection, string username, string password)
        {
            MySqlDataReader reader = null;
            try
            {
                //带参数的sql语句 @后代表参数名
                MySqlCommand cmd =
                    new MySqlCommand("Select * from user where username = @username and password = @password",
                        sqlConnection);
                cmd.Parameters.AddWithValue("username", username);
                cmd.Parameters.AddWithValue("password", password);
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    int id = reader.GetInt32("id");
                    User user = new User(id, username, password);
                    return user;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("在 VerifyUser 的时候出现异常：" + e);
            }
            finally // 必须要执行的语句
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }

            return null;
        }

        public bool GetUsername(MySqlConnection sqlConnection, string username)
        {
            MySqlDataReader reader = null;
            try
            {
                //带参数的sql语句 @后代表参数名
                MySqlCommand cmd =
                    new MySqlCommand("Select * from user where username = @username",
                        sqlConnection);
                cmd.Parameters.AddWithValue("username", username);
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("在 GetUsername 的时候出现异常：" + e);
            }
            finally // 必须要执行的语句
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }

            return false;
        }

        public void AddUser(MySqlConnection sqlConnection, string username, string password)
        {
            try
            {
                //带参数的sql语句 @后代表参数名
                MySqlCommand cmd = new MySqlCommand("insert into user set username = @username , password = @password",
                    sqlConnection);
                cmd.Parameters.AddWithValue("username", username);
                cmd.Parameters.AddWithValue("password", password);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine("在 AddUser 的时候出现异常：" + e);
            }
        }
    }
}