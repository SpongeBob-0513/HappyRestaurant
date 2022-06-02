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

        // 更新结果表  如果是新玩家则是增加一条结果
        public void UpdateOrAddResult(MySqlConnection sqlConnection, Result result)
        {
            try
            {
                MySqlCommand cmd = null;
                if (result.Id <= -1)
                {
                    cmd = new MySqlCommand(
                        "Insert into result set totalcount = @totalcount, maxscore = @maxscore, userid = @userid",
                        sqlConnection);
                }
                else
                {
                    cmd = new MySqlCommand(
                        "Update result set totalcount = @totalcount, maxscore = @maxscore where userid = @userid",
                        sqlConnection);
                }

                cmd.Parameters.AddWithValue("totalcount", result.TotalCount);
                cmd.Parameters.AddWithValue("maxscore", result.MaxScore);
                cmd.Parameters.AddWithValue("userid", result.UserId);
                cmd.ExecuteNonQuery();
                if (result.Id <= -1) // 如果新增的result对应的玩家又继续进行游戏，游戏结束后需要更新result 为了避免重复增加result 此时result.id进行更新
                                        // 这样下次做判断的时候 result.Id 就是存在的了，只会执行更新语句
                {
                    Result tempRes = GetResultByUserId(sqlConnection, result.UserId);
                    result.Id = tempRes.Id;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("在 UpdateOrAddResult 的时候出现异常：" + e);
            }
        }
    }
}