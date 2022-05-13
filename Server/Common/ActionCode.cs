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
        StartGame,
    }
}