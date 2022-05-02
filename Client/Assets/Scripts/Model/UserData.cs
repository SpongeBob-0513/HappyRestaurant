namespace Model
{
    public class UserData
    {
        public UserData(string username, int totalCount, int maxScore)
        {
            this.Username = username;
            this.TotalCount = totalCount;
            this.MaxScore = maxScore;
        }
        public string Username { get; private set; }
        public int TotalCount { get; private set; }
        public int MaxScore { get; private set; }
    }
}