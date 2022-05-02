namespace GameServer.Model
{
    public class Result
    {
        public Result(int id, int userId, int totalCount, int maxScore)
        {
            this.Id = id;
            this.UserId = userId;
            this.TotalCount = totalCount;
            this.MaxScore = maxScore;
        }
        
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TotalCount { get; set; }
        public int MaxScore { get; set; }
    }
}