namespace Common
{
    public enum ActionCode
    {
        None,
        
        Login,
        Register,
        
        ListRoom, // 获取房间 list
        CreateRoom, // 创建房间
        JoinRoom, // 加入房间
        UpdateRoom, // 更新房间 有新的客户端加入房间
        QuitRoom,
        
        StartGame, // 开始按钮点击
        ShowTimer, // 显示倒计时
        StartPlay, // 开始玩游戏
        Move,
        MakeFood,
        Score,
        GameOver,
        UpdateResult,
        QuitPlaying,
    }
}