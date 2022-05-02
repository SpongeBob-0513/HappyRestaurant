using System;
using GameServer.Model;
using MySql.Data.MySqlClient;

namespace GameServer.DAO
{
    public class ResultDAO
    {
        public Result GetResultByUserId(MySqlConnection sqlConnection, int userId)
        {
            MySqlDataReader reader = null;
            try
            {
                //带参数的sql语句 @后代表参数名
                MySqlCommand cmd =
                    new MySqlCommand("Select * from result where userid = @userid", sqlConnection);
                cmd.Parameters.AddWithValue("userid", userId);
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    int id = reader.GetInt32("id");
                    int totalCount = reader.GetInt32("totalcount");
                    int maxScore = reader.GetInt32("maxscore");
                    
                    Result result = new Result(id, userId, totalCount, maxScore);
                    return result;
                }
                else
                {
                    Result result = new Result(-1, userId, 0, 0);
                    return result;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("在 GetResultByUserId 的时候出现异常：" + e);
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
    }
}