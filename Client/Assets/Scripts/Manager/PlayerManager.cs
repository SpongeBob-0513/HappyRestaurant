using Model;

namespace Manager
{
    public class PlayerManager:BaseManager
    {
        private UserData userData;
        
        public PlayerManager(GameFacade gameFacade) : base(gameFacade)
        {
        }

        public UserData UserData
        {
            get => userData;
            set => userData = value;
        }
    }
}