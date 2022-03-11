namespace Manager
{
    public class BaseManager
    {
        protected GameFacade _gameFacade; // protected 类型是为了在子类也可以进行访问

        public BaseManager(GameFacade gameFacade)
        {
            this._gameFacade = gameFacade;
        }
        
        public virtual void OnInit(){}
        public virtual void Update(){}
        public virtual void OnDestroy(){}
    }
}