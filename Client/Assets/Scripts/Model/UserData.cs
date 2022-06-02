namespace Model
{
    public class UserData
    {
        public UserData(string userData)
        {
            string[] strs = userData.Split(',');
            this.Id = int.Parse(strs[0]);
            this.Username = strs[1];
            this.TotalCount = int.Parse(strs[2]);
            this.MaxScore = int.Parse(strs[3]);
        }
        public UserData(string username, int totalCount, int maxScore)
        {
            this.Username = username;
            this.TotalCount = totalCount;
            this.MaxScore = maxScore;
        }
        
        public UserData(int id, string username, int totalCount, int maxScore)
        {
            this.Id = id;
            this.Username = username;
            this.TotalCount = totalCount;
            this.MaxScore = maxScore;
        }
        
        public int Id { get; private set; }
        public string Username { get; private set; }
        public int TotalCount { get; set; }
        public int MaxScore { get; set; }
    }
}